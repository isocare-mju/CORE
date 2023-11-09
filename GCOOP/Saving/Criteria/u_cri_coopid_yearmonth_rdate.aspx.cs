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

namespace Saving.Criteria
{
    public partial class u_cri_coopid_yearmonth_rdate : PageWebSheet, WebSheet
    {
        protected String app;
        protected String gid;
        protected String rid;
        protected String pdf;
        protected String runProcess;
        protected String popupReport;
        private DwThDate tdw_criteria;
        //เพิ่ม 2.
        public String outputProcess = "";

        #region WebSheet Members

        public void InitJsPostBack()
        {
            HdOpenIFrame.Value = "False";
            runProcess = WebUtil.JsPostBack(this, "runProcess");
        }

        //protected void Page_Load(object sender, EventArgs e)
        public void WebSheetLoadBegin()
        {
            InitJsPostBack();
            if (IsPostBack)
            {
                dw_criteria.RestoreContext();
            }
            else
            {
                //default values.
                dw_criteria.InsertRow(0);
                int ln_year = state.SsWorkDate.Year + 543;
                dw_criteria.SetItemDecimal(1, "select_year", ln_year);
                DwUtil.RetrieveDDDW(dw_criteria, "printer", "criteria.pbl", state.SsCoopControl);
                dw_criteria.SetItemString(1, "printer", "PDF");
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
        }

        public void SaveWebSheet()
        {
        }

        public void WebSheetLoadEnd()
        {
        }

        #endregion

        #region Report Process

        private void RunProcess()
        {
            //อ่านค่าจากหน้าจอใส่ตัวแปรรอไว้ก่อน.

            string coop_id = state.SsCoopControl;
            Decimal year_old = dw_criteria.GetItemDecimal(1, "select_year") - 1;
            String strDte = year_old.ToString() + "12";
            Decimal year = dw_criteria.GetItemDecimal(1, "select_year");
            String month = dw_criteria.GetItemString(1, "select_month");
            DateTime accstart_date = state.SsWorkDate;
            DateTime accend_date = state.SsWorkDate;
            decimal year2 =  dw_criteria.GetItemDecimal(1, "select_year")- 543;

            String eDate = year2.ToString() + '-' + month + '-'+ "01";


            try
            {
                DataTable dt = WebUtil.Query("select accstart_date from cmaccountyear where account_year ='" + year + "' and coop_id = '"+ state.SsCoopControl+"'");
                if (dt.Rows.Count > 0)
                {
                    accstart_date = Convert.ToDateTime( dt.Rows[0]["accstart_date"]);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + year_old.ToString() + "");
                }
            }
            catch
            {
            }
            try
            {
                DataTable dt = WebUtil.Query("select DATEADD(d,-1, DATEADD(MONTH,DATEDIFF(MONTH,0, '" + eDate + "')+1,0)) as accend_date ");
                if (dt.Rows.Count > 0)
                {
                    accend_date = Convert.ToDateTime(dt.Rows[0]["accend_date"]);
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + year_old.ToString() + "");
                }
            }
            catch
            {
            }






            //แปลง Criteria ให้อยู่ในรูปแบบมาตรฐาน.
            ReportHelper lnv_helper = new ReportHelper();
            lnv_helper.AddArgument(coop_id, ArgumentType.String);
            lnv_helper.AddArgument(strDte, ArgumentType.String);
            lnv_helper.AddArgument(accstart_date.ToString(), ArgumentType.DateTime);
            lnv_helper.AddArgument(accend_date.ToString(), ArgumentType.DateTime);


            //ชื่อไฟล์ PDF = YYYYMMDDHHMMSS_<GID>_<RID>.PDF
            String pdfFileName = DateTime.Now.ToString("yyyyMMddHHmmss", WebUtil.EN);
            pdfFileName += "_" + gid + "_" + rid + ".pdf";
            pdfFileName = pdfFileName.Trim();

            //ส่งให้ ReportService สร้าง PDF ให้ {โดยปกติจะอยู่ใน C:\GCOOP\Saving\PDF\}.
            try
            {
                //เพิ่ม 3
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