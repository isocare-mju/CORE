using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;

namespace Saving.Applications.divavg.dlg.w_dlg_divsrv_staydiv_ctrl
{
    public partial class w_dlg_divsrv_staydiv : PageWebDialog, WebDialog
    {
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsList.InitDsList(this);
        }

        public void WebDialogLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].div_year = "2563";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void WebDialogLoadEnd()
        {

        }

        protected void BtSearch_Click(object sender, EventArgs e)
        {
            try
            {
               dsList.Retrive( dsMain.DATA[0].div_year) ;
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }
    }
}