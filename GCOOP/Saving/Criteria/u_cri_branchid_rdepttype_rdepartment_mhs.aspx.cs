﻿using System;
using CoreSavingLibrary;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using DataLibrary;
using Sybase.DataWindow;

namespace Saving.Criteria
{
    public partial class u_cri_branchid_rdepttype_rdepartment_mhs : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");


            tdw_criteria = new DwThDate(dw_criteria, this);
            //OP
            //outputReport = WebUtil.JsPostBack(this, "outputReport");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            //InitJsPostBack();
            //this.ConnectSQLCA();
            //dw_criteria.SetTransaction(sqlca);
            DwUtil.RetrieveDDDW(dw_criteria, "branch_id", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "start_depttype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "end_depttype", "criteria.pbl", null);
            DwUtil.RetrieveDDDW(dw_criteria, "start_department", "criteria.pbl", state.SsCoopControl);
            DwUtil.RetrieveDDDW(dw_criteria, "end_department", "criteria.pbl", state.SsCoopControl);

            if (IsPostBack)
            {
                dw_criteria.RestoreContext();


            }
            else
            {
                //default values.
                    dw_criteria.InsertRow(0);
                    dw_criteria.SetItemString(1, "branch_id", state.SsCoopId);
                    string[] minmax = ReportUtil.GetMinMaxDepttype();
                    dw_criteria.SetItemString(1, "start_depttype", minmax[0]);
                    dw_criteria.SetItemString(1, "end_depttype", minmax[1]);
                    string[] minmaxx = ReportUtil.GetMinMaxMembgroup();
                    dw_criteria.SetItemString(1, "start_department", minmaxx[0]);
                    dw_criteria.SetItemString(1, "end_department", minmaxx[1]);

                    tdw_criteria.Eng2ThaiAllRow();
                    
            }

            //--- Page Arguments
            try
            {
                app = Request["app"].ToString();
            }
            catch { }
            if (app == null || app == "")
            {
                app = state.SsApplication;
            }
            try
            {
                gid = Request["gid"].ToString();
            }
            catch { }
            try
            {
                rid = Request["rid"].ToString();
            }
            catch { }

            //Report Name.
            try
            {
                Sta ta = new Sta(state.SsConnectionString);
                String sql = "";
                sql = @"SELECT REPORT_NAME  
                    FROM WEBREPORTDETAIL  
                    WHERE ( GROUP_ID = '" + gid + @"' ) AND ( REPORT_ID = '" + rid + @"' )";
                Sdt dt = ta.Query(sql);
                ReportName.Text = dt.Rows[0]["REPORT_NAME"].ToString();
                ta.Close();
            }
            catch
            {
                ReportName.Text = "[" + rid + "]";
            }

            //Link back to the report menu.
            LinkBack.PostBackUrl = String.Format("~/ReportDefault.aspx?app={0}&gid={1}", app, gid);
        }

        //public void CheckJsPostBack(string eventArg)

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == "runProcess")
            {
                RunProcess();
            }
            else if (eventArg == "popupReport")
            {
                PopupReport();
            }
            else if (eventArg == "post")
            {
                DwUtil.RetrieveDDDW(dw_criteria, "start_department_1", "criteria.pbl", null);
                DwUtil.RetrieveDDDW(dw_criteria, "end_department_1", "criteria.pbl", null);
            }
        }
        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {

        }

        #endregion
        #region Report Process

        //OP
        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.
            String branch_id = dw_criteria.GetItemString(1, "branch_id");
            String start_depttype = dw_criteria.GetItemString(1, "start_depttype");
            String end_depttype = dw_criteria.GetItemString(1, "end_depttype");
            String start_membgroup = dw_criteria.GetItemString(1, "start_department").Trim();
            String end_membgroup = dw_criteria.GetItemString(1, "end_department").Trim();
            String coop_name = state.SsCoopName;

            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(branch_id, ArgumentType.String);
            lnv_helper.AddArgument(start_depttype, ArgumentType.String);
            lnv_helper.AddArgument(end_depttype, ArgumentType.String);
            lnv_helper.AddArgument(start_membgroup, ArgumentType.String);
            lnv_helper.AddArgument(end_membgroup, ArgumentType.String);




            //------------------------------------------------------------------------------

            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                String criteriaXML = lnv_helper.PopArgumentsXML();
                string printer = dw_criteria.GetItemString(1, "printer");
                outputProcess = WebUtil.runProcessingReport(state, app, gid, rid, criteriaXML, pdfFileName, printer);
            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage(ex);
                return;
            }





        }

        public void PopupReport()
        {
            //เด้ง Popup ออกรายงานเป็น PDF.
            String pop = "Gcoop.OpenPopup('" + pdf + "')";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "DsReport", pop, true);
        }

        #endregion

    }
}
