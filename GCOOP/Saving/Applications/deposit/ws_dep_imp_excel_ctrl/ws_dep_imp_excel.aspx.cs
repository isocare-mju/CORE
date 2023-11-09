using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
//////////////////////////////////////////
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;
using System.Globalization;

/////////////////////////////////////////
using System.IO;
using System.Text;
using OfficeOpenXml;
/////////////////////////////////////////


namespace Saving.Applications.deposit.ws_dep_imp_excel_ctrl
{
    public partial class ws_dep_imp_excel : PageWebSheet, WebSheet
    {
        [JsPostBack]
        public String JsPostProcess { get; set; }
        [JsPostBack]
        public String JsPostDelete { get; set; }
        [JsPostBack]
        public String JsPostChkData { get; set; }
        [JsPostBack]
        public String JsPostExportErrdata { get; set; }
        
        public string fn;
        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)
            {
                Session["import"] = null;
                dsMain.DATA[0].coop_control = state.SsCoopControl;
                dsMain.DATA[0].entry_date = state.SsWorkDate;
                dsMain.DD_Recppaytype();
                dsMain.DATA[0].ai_month = state.SsWorkDate.Month;
                dsMain.DATA[0].ai_year = state.SsWorkDate.Year + 543;
            }
        }
        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "JsPostProcess")
            {
                PostProcess();
            }
            else if (eventArg == "JsPostDelete")
            {
                PostDelete();
            }
            else if (eventArg == "JsPostChkData")
            {
                CheckDatalExcel();
            }
            else if (eventArg == "JsPostExportErrdata")
            {
                PostExportErrdata();
            }
        }
        private void CheckDatalExcel()
        {
            string sql = "", member_no = "", ls_ck_memno = "", deptaccount_no = "", ls_ck_deptno = "", ls_type_detail="";
            decimal item_payment = 0, ld_seq_no=0;
            string ls_coopcontrol = state.SsCoopControl;
            string ls_coopid = state.SsCoopId;
            string ls_type = dsMain.DATA[0].type_code;
            DateTime entry_date = dsMain.DATA[0].entry_date;
            string into = Server.MapPath("~/WSRPDF/") + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + txtInput.FileName;
            txtInput.PostedFile.SaveAs(into);
            FileInfo excel = new FileInfo(into);
            DataTable dt = new DataTable();
            DataTable dt_error = new DataTable();
            DataRow dr;
            DataRow dr_error;
            Session["import"] = null;
            Session["errdata"] = null;
            using (var package = new ExcelPackage(excel))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.First();
                int rowCount = worksheet.Dimension.End.Row;
                string total = string.Empty;
                /*Format Excel 
                    - col 1 >>> deptaccount_no
                    - col 2 >>> deptitem_amt
                    - col 3 >>> system_code 
                    - col 4 >>> seq_no
                    - col 5 >>> member_no
                */
                dt.Columns.Add("DEPTACCOUNT_NO");
                dt.Columns.Add("ITEM_PAYMENT");
                dt.Columns.Add("MEMBER_NO"); 
                dt.Columns.Add("SYSTEM_CODE");
                dt.Columns.Add("SEQ_NO");
                //--------------
                dt_error.Columns.Add("DEPTACCOUNT_NO");
                dt_error.Columns.Add("ITEM_PAYMENT");
                dt_error.Columns.Add("MEMBER_NO");
                dt_error.Columns.Add("SYSTEM_CODE");
                dt_error.Columns.Add("SEQ_NO");
                for (int row = 2; row <= rowCount; row++)
                {
                    deptaccount_no = ""; item_payment = 0; ls_type_detail = ""; member_no = ""; ld_seq_no = 0;
                    try { deptaccount_no = worksheet.Cells[row, 1].Value.ToString().Trim(); }
                    catch
                    { row = rowCount;continue; }
                    
                    item_payment = Convert.ToDecimal(worksheet.Cells[row, 2].Value);
                    try
                    {
                        ls_type_detail = worksheet.Cells[row, 3].Text.ToString().Trim();
                    }
                    catch { ls_type_detail = ls_type; }
                    ls_type_detail = (ls_type_detail.Length < 1 ? ls_type : ls_type_detail);
                    try
                    {
                        ld_seq_no = Convert.ToDecimal(worksheet.Cells[row, 4].Text.Trim());
                    }
                    catch
                    {
                        sql = "";
                        if (Sta.IS_OLEDB_MODE)
                        {
                            sql = @"select isnull(max(seq_no),0) +1 as seq_no from dpdepttran where coop_id={0} and rtrim(ltrim(deptaccount_no)) ={1} and rtrim(ltrim(system_code)) ={2} and tran_date ={3}";
                        }
                        else
                        {
                            sql = @"select nvl(max(seq_no),0) +1 as seq_no from dpdepttran where coop_id={0} and trim(deptaccount_no) ={1} and trim(system_code) ={2} and tran_date ={3}";
                        }
                        sql = WebUtil.SQLFormat(sql, ls_coopid, deptaccount_no, ls_type_detail, entry_date);
                        Sdt dt_row = WebUtil.QuerySdt(sql);
                        if (dt_row.Next())
                        {
                            ld_seq_no = dt_row.GetDecimal("seq_no");
                        }
                    }
                    try { member_no = worksheet.Cells[row, 5].Value.ToString().Trim(); }
                    catch { member_no = ""; }
                    if (member_no.Length > 0)
                    {
                        member_no = WebUtil.MemberNoFormat(member_no);
                    }                    
                    dr = dt.NewRow();
                    dr["DEPTACCOUNT_NO"] = deptaccount_no;
                    dr["ITEM_PAYMENT"] =  Convert.ToDecimal(item_payment); 
                    dr["MEMBER_NO"] = member_no;
                    dr["SYSTEM_CODE"] = ls_type_detail;
                    dr["SEQ_NO"] = Convert.ToDecimal(ld_seq_no); 
                    dt.Rows.Add(dr);
                    //check deptaccount_no member_no
                    sql = ""; ls_ck_deptno="";
                    sql = @"select deptaccount_no from dpdeptmaster where coop_id={0} and deptaccount_no ={1} and deptclose_status=0";                    
                    sql = WebUtil.SQLFormat(sql, ls_coopcontrol, deptaccount_no);
                    Sdt dt_row2 = WebUtil.QuerySdt(sql);
                    if (dt_row2.Next())
                    {
                        ls_ck_deptno = dt_row2.GetString("deptaccount_no").Trim();
                    }
                    if (deptaccount_no != ls_ck_deptno)
                    {
                        dr_error = dt_error.NewRow();
                        dr_error["DEPTACCOUNT_NO"] = deptaccount_no;
                        dr_error["ITEM_PAYMENT"] = item_payment;
                        dr_error["MEMBER_NO"] = member_no;
                        dr_error["SYSTEM_CODE"] = ls_type_detail;
                        dr_error["SEQ_NO"] = Convert.ToDecimal(ld_seq_no);
                        dt_error.Rows.Add(dr_error);
                    }
                    else {
                        if (member_no.Length > 0) {
                            sql = @"select member_no from dpdeptmaster where coop_id={0} and deptaccount_no ={1} and deptclose_status=0";
                            sql = WebUtil.SQLFormat(sql, ls_coopcontrol, deptaccount_no);
                            dt_row2 = WebUtil.QuerySdt(sql);
                            if (dt_row2.Next())
                            {
                                ls_ck_memno = dt_row2.GetString("member_no").Trim();
                            }
                            if (member_no != ls_ck_memno)
                            {
                                dr_error = dt_error.NewRow();
                                dr_error["DEPTACCOUNT_NO"] = deptaccount_no;
                                dr_error["ITEM_PAYMENT"] = item_payment;
                                dr_error["MEMBER_NO"] = member_no;
                                dr_error["SYSTEM_CODE"] = ls_type_detail;
                                dr_error["SEQ_NO"] = Convert.ToDecimal(ld_seq_no);
                                dt_error.Rows.Add(dr_error);
                            }
                        }
                    }
                }
                GridView1.DataSource = dt_error;
                GridView1.DataBind();
                ViewState["import"] = dt;
                Session["import"] = dt;
                Session["errdata"] = dt_error;
                if (GridView1.Rows.Count < 1)
                {
                    LtServerMessage.Text = WebUtil.CompleteMessage("ไฟล์ข้อมูลถูกต้อง");
                }
            }
        }
        private void PostProcess()
        {
            try
            {
                string ls_coopControl = state.SsCoopControl;
                string ls_coopid = state.SsCoopId;
                DateTime entry_date = dsMain.DATA[0].entry_date;
                string ls_typecode = dsMain.DATA[0].type_code;
                int rowGrid = GridView1.Rows.Count;
                string error = "";
                if (rowGrid > 0) {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไฟล์ข้อมูลไม่ถูกต้อง");
                    return;
                }
                DataTable dt = Session["import"] as DataTable;
                if (dt == null)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาตรวจสอบไฟล์ก่อน import");return;
                }
                else if (dt.Rows.Count < 1 ) {
                    LtServerMessage.Text = WebUtil.ErrorMessage("กรุณาตรวจสอบไฟล์ก่อน import"); return;
                }
               
                String sql = "", ls_keep_deptno = "";
                string ls_deptno = "", ls_type= "",ls_member_no;
                decimal ld_amountamt = 0, ld_seq_no = 0;
                decimal ld_year = dsMain.DATA[0].ai_year;
                decimal ld_month = dsMain.DATA[0].ai_month;
                decimal ld_tranStatus = 0;
                string ls_pecv = ld_year.ToString() + ld_month.ToString("00");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls_deptno = ""; ld_amountamt = 0; ls_member_no = ""; ls_type = ""; ld_seq_no = 0;
                    ls_deptno = dt.Rows[i].Field<string>("DEPTACCOUNT_NO");
                    ld_amountamt = Convert.ToDecimal(dt.Rows[i].Field<string>("ITEM_PAYMENT"));
                    ls_member_no = dt.Rows[i].Field<string>("MEMBER_NO");
                    ls_type = dt.Rows[i].Field<string>("SYSTEM_CODE");
                    ld_seq_no = Convert.ToDecimal(dt.Rows[i].Field<string>("SEQ_NO"));
                    sql = @"insert into DPDEPTTRAN 
                    ( COOP_ID, DEPTACCOUNT_NO,  MEMBER_NO, SYSTEM_CODE,  TRAN_DATE, SEQ_NO, DEPTITEM_AMT,TRAN_STATUS,MEMCOOP_ID,TRAN_YEAR,BRANCH_OPERATE) 
                    values ({0},{1},(select member_no from dpdeptmaster where deptaccount_no = {1}) ,{2},{4},{5},{3},{6},{0},{7},'1')";
                    sql = WebUtil.SQLFormat(sql, ls_coopid, ls_deptno, ls_type, ld_amountamt, entry_date, ld_seq_no, ld_tranStatus, ld_year);
                    WebUtil.ExeSQL(sql);                    
                }
                Session["import"] = null;
                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                #region old
                //if (txtInput.HasFile)
                //{
                //    if (System.IO.Path.GetExtension(txtInput.FileName) == ".xlsx" || System.IO.Path.GetExtension(txtInput.FileName) == ".xls")
                //    {

                //        ExecuteDataSource exe = new ExecuteDataSource(this);
                //        string into = Server.MapPath("~/WSRPDF/") + DateTime.Now.ToString("ddMMyyyyHHmmss") + "_" + txtInput.FileName;
                //        txtInput.PostedFile.SaveAs(into);
                //        FileInfo excel = new FileInfo(into);
                //        using (var package = new ExcelPackage(excel))
                //        {
                //            var workbook = package.Workbook;
                //            var worksheet = workbook.Worksheets.First();

                //            for (int i = 2; i <= worksheet.Dimension.End.Row; i++)
                //            {
                //                try
                //                {
                //                    ls_deptno = worksheet.Cells[i, 1].Text.ToString().Trim();
                //                    ls_deptno = ls_deptno.Replace("-", "");
                //                    if (ls_deptno.Length > 0)
                //                    {
                //                        ld_amountamt = Convert.ToDecimal(worksheet.Cells[i, 2].Text.Trim());
                //                        try
                //                        {
                //                            ls_type_code_detail = worksheet.Cells[i, 3].Text.ToString().Trim();
                //                        }
                //                        catch { ls_type_code_detail = ls_typecode; }
                //                        ls_type_code_detail = (ls_type_code_detail.Length < 1 ? ls_typecode : ls_type_code_detail);
                //                        try
                //                        {
                //                            ld_seq_no = Convert.ToDecimal(worksheet.Cells[i, 4].Text.Trim());
                //                        }
                //                        catch
                //                        {
                //                            sql = "";
                //                            if (Sta.IS_OLEDB_MODE)
                //                            {
                //                                sql = @"select isnull(max(seq_no),0) +1 as seq_no from dpdepttran where coop_id={0} and rtrim(ltrim(deptaccount_no)) ={1} and rtrim(ltrim(system_code)) ={2} and tran_date ={3}";
                //                            }
                //                            else
                //                            {
                //                                sql = @"select nvl(max(seq_no),0) +1 as seq_no from dpdepttran where coop_id={0} and trim(deptaccount_no) ={1} and trim(system_code) ={2} and tran_date ={3}";
                //                            }
                //                            sql = WebUtil.SQLFormat(sql, ls_coopid, ls_deptno, ls_type_code_detail, entry_date);
                //                            Sdt dt = WebUtil.QuerySdt(sql);
                //                            if (dt.Next())
                //                            {
                //                                ld_seq_no = dt.GetDecimal("seq_no");
                //                            }
                //                        }
                //                        //update case นอกสังกัด
                //                        if (dsMain.DATA[0].keep_status == 1 && ls_typecode == "DTM" && ls_coopControl == "060001")
                //                        {
                //                            ls_keep_deptno = "";
                //                            sql = @"select loancontract_no from kptempreceivedet where coop_id={0} and keepitem_status=1 and posting_status=0 and RECV_PERIOD ={1} and loancontract_no = {2}";
                //                            sql = WebUtil.SQLFormat(sql, ls_coopControl, ls_pecv, ls_deptno);
                //                            Sdt dt = WebUtil.QuerySdt(sql);
                //                            if (dt.Next())
                //                            {
                //                                ls_keep_deptno = dt.GetString("loancontract_no").Trim();
                //                            }
                //                            ld_tranStatus = (ls_keep_deptno == null || ls_keep_deptno == "" ? 0 : 1);
                //                            sql = @"update kptempreceivedet set real_payment=item_payment,item_payment={3},keepitem_status = 8 where coop_id={0} and keepitem_status=1 and posting_status=0 and RECV_PERIOD ={1} and loancontract_no = {2}";
                //                            sql = WebUtil.SQLFormat(sql, ls_coopControl, ls_pecv, ls_deptno, ld_amountamt);
                //                            WebUtil.ExeSQL(sql);
                //                        }
                //                        sql = @"insert into DPDEPTTRAN 
                //                ( COOP_ID, DEPTACCOUNT_NO,  MEMBER_NO, SYSTEM_CODE,  TRAN_DATE, SEQ_NO, DEPTITEM_AMT,TRAN_STATUS) 
                //                values ({0},{1},(select member_no from dpdeptmaster where deptaccount_no = {1}) ,{2},{4},{5},{3},{6})";
                //                        sql = WebUtil.SQLFormat(sql, ls_coopid, ls_deptno, ls_type_code_detail, ld_amountamt, entry_date, ld_seq_no, ld_tranStatus);
                //                        WebUtil.ExeSQL(sql);
                //                    }
                //                }
                //                catch
                //                {
                //                    error += ls_deptno + ",";
                //                }
                //            }
                //            if (error == "")
                //            {
                //                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเสร็จสิ้น");
                //            }
                //            else
                //            {
                //                LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกข้อมูลเลขบัญชีดังนี้ไม่สำเร็จ " + error);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        LtServerMessage.Text = WebUtil.ErrorMessage("ต้องเป็น ไฟล์ .xlsx หรือ .xls เท่านั้น");
                //    }
                //}
                //else
                //{
                //    LtServerMessage.Text = WebUtil.ErrorMessage("เลือกข้อมูลที่จะนำเข้า");
                //}
                #endregion
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถ import ข้อมูลได้ " + ex);
            }
        }
        private void PostExportErrdata() {
            try
            {
                string filename = "ข้อมูลที่ไม่ถูกต้อง "+DateTime.Now.ToString("ddMMyyyyHHmmss")+ ".xlsx";
                fn = filename;
                var package = new ExcelPackage();
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cells["A1"].Value = "เลขที่บัญชี";
                worksheet.Cells["B1"].Value = "จำนวนเงิน";
                worksheet.Cells["C1"].Value = "รหัสทำรายการ";
                worksheet.Cells["D1"].Value = "ลำดับรายการ";
                worksheet.Cells["E1"].Value = "เลขสมาชิก";
                int row_excel = 2;
                string ls_deptno = "", ls_member_no = "", ls_type = "";
                decimal ld_amountamt = 0, ld_seq_no = 0; 
                DataTable dt = Session["errdata"] as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls_deptno = ""; ld_amountamt = 0; ls_member_no = ""; ls_type = ""; ld_seq_no = 0;
                    ls_deptno = dt.Rows[i].Field<string>("DEPTACCOUNT_NO");
                    ld_amountamt = Convert.ToDecimal(dt.Rows[i].Field<string>("ITEM_PAYMENT"));
                    ls_member_no = dt.Rows[i].Field<string>("MEMBER_NO");
                    ls_type = dt.Rows[i].Field<string>("SYSTEM_CODE");
                    ld_seq_no = Convert.ToDecimal(dt.Rows[i].Field<string>("SEQ_NO"));

                    worksheet.Cells["A" + row_excel.ToString()].Value = ls_deptno;
                    worksheet.Cells["B" + row_excel.ToString()].Value = ld_amountamt;
                    worksheet.Cells["C" + row_excel.ToString()].Value = ls_type;
                    worksheet.Cells["D" + row_excel.ToString()].Value = ld_seq_no;
                    worksheet.Cells["E" + row_excel.ToString()].Value = ls_member_no;
                    row_excel++;
                }

                string docPath = Server.MapPath("~/WSRPDF/");
                FileInfo fi = new FileInfo(docPath);
                File.Delete(docPath + fn);
                package.SaveAs(new FileInfo(Server.MapPath("~/WSRPDF/") + fn));
                string url_into = WebUtil.GetSavingUrlCurrent() + "WSRPDF/" + fn;
                Lt_errordata_fileexcel.Text = WebUtil.CompleteMessage("<a href=\"" + url_into + "\" target='_blank'> >>..SaveFile..<< </a>");
                
            } catch { }
        }
        private void PostDelete()
        {
            try
            {
                DateTime entry_date = dsMain.DATA[0].entry_date;
                string ls_typecode = dsMain.DATA[0].type_code;
                string sql = "delete from dpdepttran  where coop_id={0} and tran_date = {1} and tran_status = 0 and system_code = {2}";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, entry_date, ls_typecode);
                WebUtil.ExeSQL(sql);
                LtServerMessage.Text = WebUtil.CompleteMessage("ลบข้อมูลที่ Import เสร็จสิ้น");
            }
            catch
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ลบข้อมูลที่ Import ไม่สำเร็จ");
            }
        }
        public void SaveWebSheet()
        {
           
        }

        public void WebSheetLoadEnd()
        {


        }
    }
}