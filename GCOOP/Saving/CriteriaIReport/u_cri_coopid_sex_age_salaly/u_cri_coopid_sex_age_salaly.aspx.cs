using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_sex_age_salaly
{
    public partial class u_cri_coopid_sex_age_salaly : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsMain.DATA[0].SEX = "%%";
        }

        public void WebSheetLoadBegin()
        {
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

            if (!IsPostBack)
            {
                dsMain.DdCoopId();
                dsMain.DATA[0].COOP_ID = state.SsCoopId;
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
            string as_coopid = dsMain.DATA[0].COOP_ID;
            string sex = dsMain.DATA[0].SEX;
            Int32 as_sage = dsMain.DATA[0].S_AGE;
            Int32 as_eage = dsMain.DATA[0].E_AEG;
            decimal as_ssalaly = dsMain.DATA[0].S_SALALY;
            decimal as_esalaly = dsMain.DATA[0].E_SALALY;
            if (as_sage == 0)
            {
                as_sage = 0;
            }
            if (as_eage == 0)
            {
                as_eage = 100;
            }
            if (as_ssalaly == 0)
            {
                as_ssalaly = 0;
            }
            if (as_esalaly == 0)
            {
                as_esalaly = 9999999;
            }

            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_sex", iReportArgumentType.String, sex);
                arg.Add("as_sage", iReportArgumentType.Integer, as_sage);
                arg.Add("as_eage", iReportArgumentType.Integer, as_eage);
                arg.Add("as_ssalaly", iReportArgumentType.Double, as_ssalaly);
                arg.Add("as_esalaly", iReportArgumentType.Double, as_esalaly);

                iReportBuider report = new iReportBuider(this, arg);

                report.Retrieve();
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