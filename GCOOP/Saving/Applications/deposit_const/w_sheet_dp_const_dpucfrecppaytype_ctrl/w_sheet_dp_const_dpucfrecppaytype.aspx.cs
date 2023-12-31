﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_dpucfrecppaytype_ctrl
{
    public partial class w_sheet_dp_const_dpucfrecppaytype : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public string PostNewRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }


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
                dsList.InsertAtRow(0);
                dsList.SetItem(0, dsList.DATA.COOP_IDColumn, state.SsCoopControl);
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
                int row = dsList.GetRowFocus();
                dsList.DeleteRow(row);
                dsList.DDMoneytype();
                row = row - 1;
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

        public void SaveWebSheet()
        {
            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);
                exe.AddRepeater(dsList);
                int result = exe.Execute();
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
                LtServerMessage.Text = WebUtil.ErrorMessage (ex);
            }

        }

        public void WebSheetLoadEnd()
        {
        }
    }
}