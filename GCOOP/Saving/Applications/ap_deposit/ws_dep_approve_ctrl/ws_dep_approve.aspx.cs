using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;
namespace Saving.Applications.ap_deposit.ws_dep_approve_ctrl
{
    public partial class ws_dep_approve : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostApvStatus { get; set; }
        
        public void InitJsPostBack()
        {
            dsMain.InitDs(this);
            dsList.InitList(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsMain.DATA[0].apv_status = 8;
                dsList.RetrieveList(8,state.SsWorkDate);
            }
        }
          
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "PostApvStatus")
            {
                decimal apv_status = dsMain.DATA[0].apv_status;
                dsList.RetrieveList(apv_status,state.SsWorkDate);
            }
        }
        public void SaveWebSheet()
        {
            try
            {                           
                String sqlStr = "";
                decimal apv_status = 0;
                string apv_docno="";
                DateTime work_date = state.SsWorkDate;
                string coopControl = state.SsCoopControl;
                for (int i = 0; i < dsList.RowCount; i++)
                {
                    apv_docno = ""; apv_status = 0;
                    apv_docno = dsList.DATA[i].apv_docno;
                    apv_status = dsList.DATA[i].apv_status;
                    try
                    {
                        sqlStr = @"update 	dpdeptapprove
	                                set	 	apv_status 		= {2},
				                            approve_date	= {3}
	                                where 	coop_id			= {0}
	                                and		apv_docno 		= {1}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, coopControl, apv_docno, apv_status, work_date);
                        WebUtil.ExeSQL(sqlStr);
                        
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเรียบร้อย");
            }
            catch(Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเสร็จ "+ ex);
            }
            dsList.RetrieveList(dsMain.DATA[0].apv_status,state.SsWorkDate);
        }

        public void WebSheetLoadEnd()
        {
        }

    }
}