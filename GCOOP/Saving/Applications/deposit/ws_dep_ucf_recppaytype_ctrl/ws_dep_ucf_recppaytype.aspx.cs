using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataLibrary;

namespace Saving.Applications.deposit.ws_dep_ucf_recppaytype_ctrl
{
    public partial class ws_dep_ucf_recppaytype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string postMoneytypeSta { get; set; }
        [JsPostBack]
        public string JsPostCashAccid { get; set; }
        

        public void InitJsPostBack()
        {
            dsList.InitDs(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                dsList.Retrieve();
                dsList.DDMoneytype();
                int row = dsList.RowCount;
                int rows = 0;
                if (row > 0)
                {
                    for (int i = 1; i <= row; i++)
                    {
                        rows = i - 1;
                        dsList.DDAccountid(dsList.DATA[rows].MONEYTYPE_SUPPORT, rows);
                    }
                }
           }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostNewRow)
            {
                dsList.InsertLastRow();
                dsList.DATA[dsList.RowCount - 1].COOP_ID = state.SsCoopId;
                dsList.DATA[dsList.RowCount - 1].RECPPAYTYPE_FLAG = 1;
                dsList.DATA[dsList.RowCount - 1].ACCNATURE_FLAG = 1;
                dsList.DATA[dsList.RowCount - 1].GROUP_ITEMTPE = "0";
                dsList.DDMoneytype();
                int row = dsList.RowCount;
                int rows = 0;
                if (row > 0)
                {
                    for (int i = 1; i <= row; i++)
                    {
                        rows = i - 1;
                        dsList.DDAccountid(dsList.DATA[rows].MONEYTYPE_SUPPORT, rows);
                    }
                }   
            }
            else if (eventArg == PostDelRow)
            {
                int rowDel = dsList.GetRowFocus();
                string recppaytype_code = dsList.DATA[rowDel].RECPPAYTYPE_CODE;
                //check delete
                if (of_checkdelete(recppaytype_code))
                {
                    LtServerMessage.Text = WebUtil.WarningMessage("ไม่สามารถลบได้เนื่องจากมีการใช้งานอยู่");
                }
                else
                {
                    dsList.DeleteRow(rowDel);
                }
                dsList.DDMoneytype();
                int row = dsList.RowCount;
                int rows = 0;
                if (row > 0)
                {
                    for (int i = 1; i <= row; i++)
                    {
                        rows = i - 1;
                        dsList.DDAccountid(dsList.DATA[rows].MONEYTYPE_SUPPORT, rows);
                    }
                }
            }
            else if (eventArg == "PostMoneytypeSta")
            {
                //string moneytype_status = dsList.DATA2[0].MONEYTYPE_STATUS;
                dsList.DDMoneytype();
                int row = dsList.RowCount;
                int rows = 0;
                if (row > 0)
                {
                    for (int i = 1; i <= row; i++)
                    {
                        rows = i - 1;
                        dsList.DDAccountid(dsList.DATA[rows].MONEYTYPE_SUPPORT, rows);
                    }
                }
            }
            else if (eventArg == "JsPostCashAccid")
            {
                int row = dsList.GetRowFocus();
                dsList.DDAccountid(dsList.DATA[row].MONEYTYPE_SUPPORT, row);              
            }
        }
        public bool of_checkdelete(string recppaytype_code)
        {
            bool returnvalue = false;
            try
            {
                string sql = "SELECT DEPTITEMTYPE_CODE FROM DPDEPTSTATEMENT WHERE DEPTITEMTYPE_CODE={0} ";
                sql = WebUtil.SQLFormat(sql, recppaytype_code);
                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    returnvalue = true;
                }
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex.Message);
            }
            return returnvalue;
        }
        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                int result = exe.Execute();
                dsList.ResetRow();
                dsList.Retrieve();
                dsList.DDMoneytype();
                int row = dsList.RowCount;
                int rows = 0;
                if (row > 0)
                {
                    for (int i = 1; i <= row; i++)
                    {
                        rows = i - 1;
                        dsList.DDAccountid(dsList.DATA[rows].MONEYTYPE_SUPPORT, rows);
                    }
                }
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลสำเร็จ (" + result + ")");
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
            }
        }

        public void WebSheetLoadEnd()
        {
        }
    }
}