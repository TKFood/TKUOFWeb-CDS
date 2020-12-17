using Ede.Uof.Utility.Page;
using Ede.Uof.Utility.Page.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class CDS_WKF_Fields_MaintainItem : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
        ((Master_DialogMasterPage)this.Master).Button1OnClick += CDS_WKF_Fields_MaintainItem_Button1OnClick;
        ((Master_DialogMasterPage)this.Master).Button2OnClick += CDS_WKF_Fields_MaintainItem_Button2OnClick;
    }

    //<Item id=xx txt1='' txt2='' dropdown='' />
    //<Item id=xx txt1='' txt2='' dropdown='' />
    //<Item id=xx txt1='' txt2='' dropdown='' />

    private void CDS_WKF_Fields_MaintainItem_Button2OnClick()
    {
        txtFieldValue.Text += GetXML(Guid.NewGuid().ToString());

        txt1.Text = "";
        txt2.Text = "";
        ddl.SelectedIndex = 0;

        Dialog.SetReturnValue2(txtFieldValue.Text);

    }

    public string GetXML(string id)
    {
       
        XElement xe = new XElement("Item",
            new XAttribute("id" ,id),
             new XAttribute("txt1", txt1.Text),
              new XAttribute("txt2", txt2.Text),
               new XAttribute("dropdown", ddl.SelectedValue)            );
        return xe.ToString();
    }

    private void CDS_WKF_Fields_MaintainItem_Button1OnClick()
    {
        txtFieldValue.Text += GetXML(Guid.NewGuid().ToString());

        Dialog.SetReturnValue2(txtFieldValue.Text);

        Dialog.Close(this);


    }
}