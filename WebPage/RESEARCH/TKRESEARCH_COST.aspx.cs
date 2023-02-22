using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ede.Uof.Utility.Data;
using Ede.Uof.Utility.Page.Common;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.Globalization;

public partial class CDS_WebPage_RESEARCH_TKRESEARCH_COST : Ede.Uof.Utility.Page.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SETYEARSWEEKS();
            //BindGrid1("","","");
            //BindGrid2("", "", "");

        }
        else
        {


        }




    }
    #region FUNCTION
  
    //private void BindDropDownList()
    //{
    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("ID", typeof(String));
    //    dt.Columns.Add("NAMES", typeof(String));

    //    string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
    //    Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

    //    string cmdTxt = @"SELECT '未完成' AS KINDS UNION ALL SELECT '全部' ";

    //    dt.Load(m_db.ExecuteReader(cmdTxt));

    //    if (dt.Rows.Count > 0)
    //    {
    //        DropDownList1.DataSource = dt;
    //        DropDownList1.DataTextField = "KINDS";
    //        DropDownList1.DataValueField = "KINDS";
    //        DropDownList1.DataBind();

    //    }
    //    else
    //    {

    //    }
    //}


    public void SETYEARSWEEKS()
    {
        txtDate1.Text = DateTime.Now.ToString("yyyy");
       
    }

    private void BindGrid1(string YM, string MB001, string MB002)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder SQUERY = new StringBuilder();



        //查詢條件
        if (!string.IsNullOrEmpty(MB001))
        {
            SQUERY.AppendFormat(@" AND TA001 LIKE '%{0}%' ", MB001);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }

        if (!string.IsNullOrEmpty(MB002))
        {
            SQUERY.AppendFormat(@" AND MB002 LIKE '%{0}%' ", MB002);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }
      
        if (!string.IsNullOrEmpty(YM) && !string.IsNullOrEmpty(SQUERY.ToString()))
        {
            cmdTxt.AppendFormat(@"
                              SELECT *
                                FROM 
                                (
                                SELECT TA002 AS '年月',TA001 AS '品號',MB002 AS '品名',MB003 AS '規格',生產入庫數,ME005 在製約量_材料,本階人工成本,本階製造費用,ME007 材料成本,ME008 人工成本,ME009 製造費用,ME010 加工費用
                                ,CONVERT(DECIMAL(16,2),((ME007+ME008+ME009+ME010)/(生產入庫數+ME005))) 單位成本, CONVERT(DECIMAL(16,2),((ME007)/(生產入庫數+ME005))) 單位材料成本, CONVERT(DECIMAL(16,2),((ME008)/(生產入庫數+ME005))) 單位人工成本,CONVERT(DECIMAL(16,2),((ME009)/(生產入庫數+ME005))) 單位製造成本,CONVERT(DECIMAL(16,2),((ME010)/(生產入庫數+ME005))) 單位加工成本
                                ,MB068
                                ,(CASE WHEN MB068 IN ('09') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均包裝人工成本
                                ,(CASE WHEN MB068 IN ('09') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均包裝製造費用
                                ,(CASE WHEN MB068 IN ('03') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均小線人工成本
                                ,(CASE WHEN MB068 IN ('03') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均小線製造費用
                                ,(CASE WHEN MB068 IN ('02') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END ) 平均大線人工成本
                                ,(CASE WHEN MB068 IN ('02') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END ) 平均大線製造費用
                                FROM 
                                (
                                SELECT TA002,TA001,SUM(TA012) '生產入庫數',SUM(TA016-TA019) AS '本階人工成本',SUM(TA017-TA020) AS '本階製造費用'
                                FROM [TK].dbo.CSTTA
                                WHERE TA002 LIKE '{0}%'
                                GROUP BY TA002,TA001
                                ) AS TEMP
                                LEFT JOIN [TK].dbo.CSTME ON ME001=TA001 AND ME002=TA002
                                LEFT JOIN [TK].dbo.INVMB ON MB001=TA001
                                WHERE 1=1
                                {1}

                                AND (生產入庫數+ME005)>0
                                UNION ALL

                                SELECT '小計' AS '年月',TA001 AS '品號',MB002 AS '品名',MB003 AS '規格',SUM(生產入庫數),AVG(ME005) 在製約量_材料,AVG(本階人工成本),AVG(本階製造費用),AVG(ME007) 材料成本,AVG(ME008) 人工成本,AVG(ME009) 製造費用,AVG(ME010) 加工費用
                                ,AVG(CONVERT(DECIMAL(16,2),((ME007+ME008+ME009+ME010)/(生產入庫數+ME005)))) 單位成本
                                ,AVG(CONVERT(DECIMAL(16,2),((ME007)/(生產入庫數+ME005)))) 單位材料成本
                                ,AVG(CONVERT(DECIMAL(16,2),((ME008)/(生產入庫數+ME005)))) 單位人工成本
                                ,AVG(CONVERT(DECIMAL(16,2),((ME009)/(生產入庫數+ME005)))) 單位製造成本
                                ,AVG(CONVERT(DECIMAL(16,2),((ME010)/(生產入庫數+ME005)))) 單位加工成本
                                ,MB068
                                ,AVG((CASE WHEN MB068 IN ('09') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END )) 平均包裝人工成本
                                ,AVG((CASE WHEN MB068 IN ('09') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END )) 平均包裝製造費用
                                ,AVG((CASE WHEN MB068 IN ('03') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END )) 平均小線人工成本
                                ,AVG((CASE WHEN MB068 IN ('03') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END )) 平均小線製造費用
                                ,AVG((CASE WHEN MB068 IN ('02') THEN 本階人工成本/(生產入庫數+ME005) ELSE 0 END )) 平均大線人工成本
                                ,AVG((CASE WHEN MB068 IN ('02') THEN 本階製造費用/(生產入庫數+ME005) ELSE 0 END )) 平均大線製造費用
                                FROM 
                                (
                                SELECT TA002,TA001,SUM(TA012) '生產入庫數',SUM(TA016-TA019) AS '本階人工成本',SUM(TA017-TA020) AS '本階製造費用'
                                FROM [TK].dbo.CSTTA
                                WHERE TA002 LIKE '{0}%'
                                GROUP BY TA002,TA001
                                ) AS TEMP
                                LEFT JOIN [TK].dbo.CSTME ON ME001=TA001 AND ME002=TA002
                                LEFT JOIN [TK].dbo.INVMB ON MB001=TA001
                                WHERE 1=1
                                {1}

                                AND (生產入庫數+ME005)>0
                                GROUP BY TA001,MB002,MB003,MB068
                                ) AS TEMP2
                                ORDER BY  品號,年月

 

                                    ", YM, SQUERY.ToString());

            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            Grid1.DataSource = dt;
            Grid1.DataBind();

        }
        else
        {

        }


    
    }




    protected void grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string KINDS = e.Row.Cells[0].Text.ToString();
            if (KINDS.Equals("小計"))
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
        }

    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        //if (e.CommandName == "GWButton1")
        //{

        //    BindGrid1("");

        //}
      
       
    }


    public void OnBeforeExport1(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        SETEXCEL();

    }

    private void BindGrid2(string YM, string MB001, string MB002)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder SQUERY = new StringBuilder();



        //查詢條件
        if (!string.IsNullOrEmpty(MB001))
        {
            //SQUERY.AppendFormat(@"   AND 成品品號 LIKE '%{0}%'", MB001);
            SQUERY.AppendFormat(@"
                                AND 成品品號 IN
                                    (
                                    SELECT TA001 
                                    FROM 
                                    (
                                    SELECT TA002,TA001,SUM(TA012) '生產入庫數'
                                    FROM [TK].dbo.CSTTA
                                    WHERE TA002 LIKE '{0}%'
                                    GROUP BY TA002,TA001
                                    ) AS TEMP
                                    LEFT JOIN [TK].dbo.CSTME ON ME001=TA001 AND ME002=TA002
                                    LEFT JOIN [TK].dbo.INVMB ON MB001=TA001
                                    WHERE 1=1
                                    AND MB001 LIKE '%{1}%'

                                    AND (生產入庫數+ME005)>0
                                    GROUP BY TA001
                                    )
                                    ", YM, MB001);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }

        if (!string.IsNullOrEmpty(MB002))
        {
            //SQUERY.AppendFormat(@"  AND 成品品名 LIKE '%{0}%'", MB002);

            SQUERY.AppendFormat(@"
                                AND 成品品號 IN
                                    (
                                    SELECT TA001 
                                    FROM 
                                    (
                                    SELECT TA002,TA001,SUM(TA012) '生產入庫數'
                                    FROM [TK].dbo.CSTTA
                                    WHERE TA002 LIKE '{0}%'
                                    GROUP BY TA002,TA001
                                    ) AS TEMP
                                    LEFT JOIN [TK].dbo.CSTME ON ME001=TA001 AND ME002=TA002
                                    LEFT JOIN [TK].dbo.INVMB ON MB001=TA001
                                    WHERE 1=1
                                    AND MB002 LIKE '%{1}%'

                                    AND (生產入庫數+ME005)>0
                                    GROUP BY TA001
                                    )
                                    ", YM, MB002);
        }
        else
        {
            SQUERY.AppendFormat(@"");
        }

        if (!string.IsNullOrEmpty(YM) && !string.IsNullOrEmpty(SQUERY.ToString()))
        {
            cmdTxt.AppendFormat(@"


                                   SELECT *
                                    ,CONVERT(NVARCHAR,CONVERT(DECIMAL(16,4),(CASE WHEN 總成品平均成本>0 THEN 分攤成本/總成品平均成本 ELSE 0 END))*100)+'%' AS '各百分比' 
                                    ,CONVERT(DECIMAL(16,2),分攤成本) AS 分攤成本
                                    FROM 
                                    (
                                    SELECT '{0}'AS '年度',MC001 AS '成品品號',MB1.MB002  AS '成品品名' ,MC004,MD003  AS '使用品號',MB2.MB002  AS '使用品名',MD006,MD007
                                    ,總成品平均成本
                                    ,材料平均成本
                                    ,人工平均成本
                                    ,製造平均成本
                                    ,加工平均成本
                                    ,各採購單位成本
                                    ,總採購單位成本
                                    ,總半成品重
                                    ,(CASE WHEN 總成品平均成本>0 THEN (CASE WHEN (MB2.MB001 LIKE '3%' OR MB2.MB001 LIKE '4%')THEN ((材料平均成本-總採購單位成本)*MD006/MD007/總半成品重) ELSE 各採購單位成本*MD006/MD007/MC004 END) ELSE 0 END) AS '分攤成本' 
                                    ,(CASE WHEN MD003 LIKE '1%' THEN '1原料'  WHEN MD003 LIKE '2%' THEN '2物料' WHEN (MD003 LIKE '3%' OR MD003 LIKE '4%') THEN '3半成品'END ) AS '分類'
                                    FROM
                                    (
                                    SELECT MC001,MC004,MD003,MD006,MD007
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MD001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '總成品平均成本'
                                    ,ISNULL((SELECT AVG((ME007)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MD001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '材料平均成本'
                                    ,ISNULL((SELECT AVG((ME008)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MD001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '人工平均成本'
                                    ,ISNULL((SELECT AVG((ME009)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MD001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '製造平均成本'
                                    ,ISNULL((SELECT AVG((ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MD001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '加工平均成本'
                                    ,(CASE WHEN ( MB2.MB001 LIKE '1%' OR MB2.MB001 LIKE '2%') AND MB2.MB064>0 AND MB2.MB065 >0 THEN MB2.MB065/MB2.MB064*MD006/MD007/MC004 ELSE MB2.MB050*MD006/MD007/MC004 END ) AS '各採購單位成本'
                                    ,(SELECT SUM (CASE WHEN  ( MB001 LIKE '1%' OR MB001 LIKE '2%') AND MB064>0 AND MB065 >0 THEN MB065/MB064*MD006/MD007/MC004 ELSE MB050*MD006/MD007/MC004 END) FROM [TK].dbo.BOMMC MC, [TK].dbo.BOMMD MD ,[TK].dbo.INVMB MB WHERE  MC.MC001=MD.MD001 AND MD.MD003=MB.MB001 AND MC.MC001=BOMMC.MC001)   AS '總採購單位成本'
                                    ,ISNULL((SELECT SUM (MD006/MD007) FROM [TK].dbo.BOMMC MC, [TK].dbo.BOMMD MD ,[TK].dbo.INVMB MB WHERE  MC.MC001=MD.MD001 AND MD.MD003=MB.MB001 AND MC.MC001=BOMMC.MC001 AND (MB.MB001 LIKE '3%' OR MB.MB001 LIKE '4%')),0)  AS '總半成品重'
                                    FROM [TK].dbo.BOMMC
                                    LEFT JOIN [TK].dbo.INVMB MB1 ON MB1.MB001=BOMMC.MC001
                                    , [TK].dbo.BOMMD
                                    LEFT JOIN [TK].dbo.INVMB MB2 ON MB2.MB001=BOMMD.MD003
                                    WHERE MC001=MD001
                                    ) AS TEMP
                                    LEFT JOIN [TK].dbo.INVMB MB1 ON MB1.MB001=TEMP.MC001
                                    LEFT JOIN [TK].dbo.INVMB MB2 ON MB2.MB001=TEMP.MD003
                                    UNION ALL
                                    SELECT '{0}',MC001 AS '成品品號',MB002  AS '成品品名',0 ,''  AS '使用品號','' AS '使用品名',0,0
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '總成品平均成本'
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,ISNULL((SELECT AVG((ME008)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '成本'
                                    ,'4人工' AS '分類'
                                    FROM [TK].dbo.BOMMC,[TK].dbo.INVMB
                                    WHERE  MC001=MB001
                                    AND (MC001 LIKE '3%' OR MC001 LIKE '4%' OR MC001 LIKE '5%') 
                                    UNION ALL
                                    SELECT '{0}',MC001 AS '成品品號',MB002  AS '成品品名',0 ,''  AS '使用品號','' AS '使用品名',0,0
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '總成品平均成本'
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,ISNULL((SELECT AVG((ME009)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '成本'
                                    ,'5製造' AS '分類'
                                    FROM [TK].dbo.BOMMC,[TK].dbo.INVMB
                                    WHERE  MC001=MB001
                                    AND (MC001 LIKE '3%' OR MC001 LIKE '4%' OR MC001 LIKE '5%') 
                                    UNION ALL
                                    SELECT '{0}',MC001 AS '成品品號',MB002  AS '成品品名',0 ,''  AS '使用品號','' AS '使用品名',0,0
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '總成品平均成本'
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,ISNULL((SELECT AVG((ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '成本'
                                    ,'6加工' AS '分類'
                                    FROM [TK].dbo.BOMMC,[TK].dbo.INVMB
                                    WHERE  MC001=MB001
                                    AND (MC001 LIKE '3%' OR MC001 LIKE '4%' OR MC001 LIKE '5%') 

                                    UNION ALL
                                    SELECT '{0}',MC001 AS '成品品號',MB002  AS '成品品名',0 ,''  AS '使用品號','' AS '使用品名',0,0
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '總成品平均成本'
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,0
                                    ,ISNULL((SELECT AVG((ME007+ME008+ME009+ME010)/(ME003+ME005+ME004)) FROM [TK].dbo.CSTME WHERE  ME001=MC001 AND (ME003+ME005+ME004)>0 AND (ME007+ME008+ME009+ME010)>0 AND ME002 LIKE '{0}%'),0) AS '成本'
                                    ,'9合計' AS '分類'
                                    FROM [TK].dbo.BOMMC,[TK].dbo.INVMB
                                    WHERE  MC001=MB001
                                    AND (MC001 LIKE '3%' OR MC001 LIKE '4%' OR MC001 LIKE '5%') 
                                ) AS TEMP2
                                WHERE 1=1
                                {1}
                                ORDER BY 成品品號,分類,使用品號

                                    ", YM, SQUERY.ToString());

            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            Grid2.DataSource = dt;
            Grid2.DataBind();


        }
        else
        {

        }



    }

    protected void grid2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           string KINDS= e.Row.Cells[5].Text.ToString();
            if (KINDS.Equals("9合計"))
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
            }
        }

    }
    protected void Grid2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        //if (e.CommandName == "GWButton1")
        //{

        //    BindGrid1("");

        //}


    }

    public void OnBeforeExport2(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {

        

    }

    private void BindGrid3(string MB001)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ERPconnectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder SQUERY = new StringBuilder();



        ////查詢條件
        //if (!string.IsNullOrEmpty(MB001))
        //{
        //    //SQUERY.AppendFormat(@"   AND 成品品號 LIKE '%{0}%'", MB001);     
        //}
        //else
        //{
        //    SQUERY.AppendFormat(@"");
        //}

       

        if (!string.IsNullOrEmpty(MB001))
        {
            cmdTxt.AppendFormat(@"
                               SELECT MC001 AS '成品品號',MB1.MB002 AS '成品品名',MD003 AS '組件品號',MB2.MB002 AS '組件品名',MB2.MB004 AS '組件單位',CONVERT(decimal(16,4),MB2.MB050) AS '最近進價',MB2.MB102  AS '進價是否含稅',MC004 AS '標準批量',MD006 AS '組成用量',MD007 AS '底數',MD008 AS '損秏率'
                                ,(SELECT TOP 1 '最近進貨日:'+TG003+' 廠商:'+TG005+' '+MA002 FROM [TK].dbo.PURTG,[TK].dbo.PURTH,[TK].dbo.PURMA WHERE TG001=TH001 AND TG002=TH002 AND TG005=MA001 AND TH004=MD003 ORDER BY TG003 DESC) AS 'MA002'
                                , CONVERT(decimal(16,4),MB2.MB050*MD006/MD007*(1+MD008)/MC004) AS '分攤單位進貨成本'
                                ,CONVERT(decimal(16,4),(SELECT SUM(MB050*MD006/MD007*(1+MD008)/MC004) FROM [TK].dbo.BOMMC MC,[TK].dbo.BOMMD MD,[TK].dbo.INVMB MB WHERE MC.MC001=MD.MD001 AND MB.MB001=MD.MD003 AND MD.MD001=BOMMC.MC001 ))  AS '成品單位進貨成本'

                                FROM [TK].dbo.BOMMC
                                LEFT JOIN [TK].dbo.INVMB MB1 ON MB1.MB001=MC001
                                ,[TK].dbo.BOMMD
                                LEFT JOIN [TK].dbo.INVMB MB2 ON MB2.MB001=MD003
                                WHERE MC001=MD001
                                AND (MC001 LIKE '%{0}%' OR MB1.MB002 LIKE '%{0}%')
                                ORDER BY MC001,MD003






                                    ", MB001);

            //m_db.AddParameter("@SDATE", SDATE);
            //m_db.AddParameter("@EDATE", EDATE);

            DataTable dt = new DataTable();

            dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

            Grid3.DataSource = dt;
            Grid3.DataBind();


        }
        else
        {

        }



    }

    protected void grid3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Grid1.PageIndex = e.NewPageIndex;
        //BindGrid();
    }
    protected void Grid3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       

    }
    protected void Grid3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = -1;

        //if (e.CommandName == "GWButton1")
        //{

        //    BindGrid1("");

        //}


    }

    public void OnBeforeExport3(object sender, Ede.Uof.Utility.Component.BeforeExportEventArgs e)
    {



    }


    public void SETEXCEL()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
        Ede.Uof.Utility.Data.DatabaseHelper m_db = new Ede.Uof.Utility.Data.DatabaseHelper(connectionString);

        StringBuilder cmdTxt = new StringBuilder();
        StringBuilder QUERYS = new StringBuilder();

        //
        //if (!string.IsNullOrEmpty(TextBox1.Text))
        //{
        //    QUERYS.AppendFormat(@"", TextBox1.Text);
        //}


        cmdTxt.AppendFormat(@" 
                            
                               
                                ", QUERYS.ToString());



        DataTable dt = new DataTable();

        dt.Load(m_db.ExecuteReader(cmdTxt.ToString()));

        if (dt.Rows.Count > 0)
        {
            //檔案名稱
            var fileName = "資料" + DateTime.Now.ToString("yyyy-MM-dd--hh-mm-ss") + ".xlsx";
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知

            using (var excel = new ExcelPackage(new FileInfo(fileName)))
            {

                // 建立分頁
                var ws = excel.Workbook.Worksheets.Add("list" + DateTime.Now.ToShortDateString());


                //預設行高
                //ws.DefaultRowHeight = 60;

                // 寫入資料試試
                //ws.Cells[2, 1].Value = "測試測試";
                int ROWS = 2;
                int COLUMNS = 1;


                //excel標題
                ws.Cells[1, 1].Value = "目前簽核者";
                ws.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //欄位置中
                ws.Cells[1, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                ws.Cells[1, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


                foreach (DataRow od in dt.Rows)
                {
                    ws.Cells[ROWS, 1].Value = od["NAME"].ToString();
                    ws.Cells[ROWS, 1].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center; //高度置中
                    ws.Cells[ROWS, 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); //儲存格框線


                    ROWS++;
                }




                ////預設列寬、行高
                //sheet.DefaultColWidth = 10; //預設列寬
                //sheet.DefaultRowHeight = 30; //預設行高

                //// 遇\n或(char)10自動斷行
                //ws.Cells.Style.WrapText = true;

                //自適應寬度設定
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                //自適應高度設定
                ws.Row(1).CustomHeight = true;



                //儲存Excel
                //Byte[] bin = excel.GetAsByteArray();
                //File.WriteAllBytes(@"C:\TEMP\" + fileName, bin);

                //儲存和歸來的Excel檔案作為一個ByteArray
                var data = excel.GetAsByteArray();
                HttpResponse response = HttpContext.Current.Response;
                Response.Clear();

                //輸出標頭檔案　　
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + "");
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
                //package.Save();//這個方法是直接下載到本地
            }
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知
            //                                                            // 沒設置的話會跳出 Please set the excelpackage.licensecontext property


            ////var file = new FileInfo(fileName);
            //using (var excel = new ExcelPackage(file))
            //{

            //}
        }

    }

  

    public void MsgBox(String ex, Page pg, Object obj)
    {
        string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
        Type cstype = obj.GetType();
        ClientScriptManager cs = pg.ClientScript;
        cs.RegisterClientScriptBlock(cstype, s, s.ToString());
    }
    #endregion

    #region BUTTON

    protected void btn1_Click(object sender, EventArgs e)
    {
        BindGrid1(txtDate1.Text.ToString(), TextBox1.Text.ToString(), TextBox2.Text.ToString());
        BindGrid2(txtDate1.Text.ToString(), TextBox1.Text.ToString(), TextBox2.Text.ToString());
    }

    protected void btn2_Click(object sender, EventArgs e)
    {
        BindGrid3( TextBox4.Text.ToString());
    }


    #endregion
}