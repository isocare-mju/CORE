using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_coopid_rloantype_principal_rperiod
{
    public partial class u_cri_coopid_rloantype_principal_rperiod : PageWebReport, WebReport
    {
        protected String app;
        protected String gid;
        protected String rid;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
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
                dsMain.DATA[0].YEAR = (state.SsWorkDate.Year+543);// WebUtil.GetAccyear(state.SsCoopControl, state.SsWorkDate);
                dsMain.DdSloan(); 
                dsMain.DdEloan();
                dsMain.DdSmembgroup();
                dsMain.DdEmembgroup();
                setpage();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {
           
            string as_coopid = dsMain.DATA[0].COOP_ID;
            string S_LOANTYPE = dsMain.DATA[0].S_LOANTYPE;
            string E_LOANTYPE = dsMain.DATA[0].E_LOANTYPE;
            string S_MEMBGROUP = dsMain.DATA[0].S_MEMBGROUP;
            string E_MEMBGROUP = dsMain.DATA[0].E_MEMBGROUP;
            decimal S_CREDIT = dsMain.DATA[0].S_CREDIT;
            decimal E_CREDIT = dsMain.DATA[0].E_CREDIT;
            decimal S_PRINCIPAL = dsMain.DATA[0].S_PRINCIPAL;
            decimal E_PRINCIPAL = dsMain.DATA[0].E_PRINCIPAL;
            decimal S_LOANREQUEST = dsMain.DATA[0].S_LOANREQUEST;
            decimal E_LOANREQUEST = dsMain.DATA[0].E_LOANREQUEST;
            decimal S_PAYMENT = dsMain.DATA[0].S_PAYMENT;
            decimal E_PAYMENT = dsMain.DATA[0].E_PAYMENT;
            decimal S_PERIOD = dsMain.DATA[0].S_PERIOD;
            decimal E_PERIOD = dsMain.DATA[0].E_PERIOD;
            decimal as_sshare = dsMain.DATA[0].S_SHARE;
            decimal as_eshare = dsMain.DATA[0].E_SHARE;
            string sex = dsMain.DATA[0].SEX;
            decimal as_sage = dsMain.DATA[0].S_AGE;
            decimal as_eage = dsMain.DATA[0].E_AGE ;
            decimal as_ssalaly = dsMain.DATA[0].S_SALALY;
            decimal as_esalaly = dsMain.DATA[0].E_SALALY;

            
            try
            {
                iReportArgument arg = new iReportArgument();
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_sloantype", iReportArgumentType.String, S_LOANTYPE);
                arg.Add("as_eloantype", iReportArgumentType.String, E_LOANTYPE);
                arg.Add("as_smembgroup", iReportArgumentType.String, S_MEMBGROUP);
                arg.Add("as_emembgroup", iReportArgumentType.String, E_MEMBGROUP);
                arg.Add("as_scredit", iReportArgumentType.String,Convert.ToString(S_CREDIT));
                arg.Add("as_ecredit", iReportArgumentType.String,Convert.ToString(E_CREDIT));
                arg.Add("as_sprincipal", iReportArgumentType.String,Convert.ToString(S_PRINCIPAL));
                arg.Add("as_eprincipal", iReportArgumentType.String,Convert.ToString(E_PRINCIPAL));
                arg.Add("as_sloanrequest", iReportArgumentType.String,Convert.ToString(S_LOANREQUEST));
                arg.Add("as_eloanrequest", iReportArgumentType.String,Convert.ToString(E_LOANREQUEST));
                arg.Add("as_spayment", iReportArgumentType.String,Convert.ToString(S_PAYMENT));
                arg.Add("as_epayment", iReportArgumentType.String,Convert.ToString(E_PAYMENT));
                arg.Add("as_speriod", iReportArgumentType.String,Convert.ToString(S_PERIOD));
                arg.Add("as_eperiod", iReportArgumentType.String,Convert.ToString(E_PERIOD));
                arg.Add("as_sshare", iReportArgumentType.String,Convert.ToString(as_sshare));
                arg.Add("as_eshare", iReportArgumentType.String,Convert.ToString(as_eshare));
                arg.Add("as_sex", iReportArgumentType.String, sex);
                arg.Add("as_sage", iReportArgumentType.String,Convert.ToString(as_sage));
                arg.Add("as_eage", iReportArgumentType.String,Convert.ToString(as_eage)+".99");
                arg.Add("as_ssalaly",iReportArgumentType.String,Convert.ToString(as_ssalaly));
                arg.Add("as_esalaly", iReportArgumentType.String,Convert.ToString(as_esalaly));
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

        public void setpage()
        {

                string sql = "select min(loantype_code) as getminloantype from lnloantype";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId);
                Sdt resultmin = WebUtil.QuerySdt(sql);
                if (resultmin.Next())
                {
                   dsMain.DATA[0].S_LOANTYPE = resultmin.GetString("getminloantype");
                }

                string sql2 = "select max(loantype_code) as getmaxloantype from lnloantype";
                sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId);
                Sdt resultmax = WebUtil.QuerySdt(sql2);
                if (resultmax.Next())
                {
                    dsMain.DATA[0].E_LOANTYPE = resultmax.GetString("getmaxloantype");
                }
                string sqlsmem = "select min(membgroup_code) as getmingroup_code from mbucfmembgroup";
                sqlsmem = WebUtil.SQLFormat(sqlsmem, state.SsCoopId);
                Sdt memgroupmin = WebUtil.QuerySdt(sqlsmem);
                if (memgroupmin.Next())
                {
                    dsMain.DATA[0].S_MEMBGROUP = memgroupmin.GetString("getmingroup_code");
                }

                string sqlemem = "select max(membgroup_code) as getmaxgroup_code from mbucfmembgroup";
                sqlemem = WebUtil.SQLFormat(sqlemem, state.SsCoopId);
                Sdt memgroupmax = WebUtil.QuerySdt(sqlemem);
                if (memgroupmax.Next())
                {
                    dsMain.DATA[0].E_MEMBGROUP = memgroupmax.GetString("getmaxgroup_code");
                }

                dsMain.DATA[0].COOP_ID = state.SsCoopId;
                dsMain.DATA[0].S_CREDIT = 0 ;
                dsMain.DATA[0].E_CREDIT = 9999999 ;
                dsMain.DATA[0].S_PRINCIPAL = 0 ;
                dsMain.DATA[0].E_PRINCIPAL = 9999999 ;
                dsMain.DATA[0].S_LOANREQUEST = 0 ;
                dsMain.DATA[0].E_LOANREQUEST = 9999999 ;
                dsMain.DATA[0].S_PAYMENT = 0 ;
                dsMain.DATA[0].E_PAYMENT = 9999999 ;
                dsMain.DATA[0].S_PERIOD = 0 ;
                dsMain.DATA[0].E_PERIOD = 9999 ;
                dsMain.DATA[0].S_SHARE = 0 ;
                dsMain.DATA[0].E_SHARE = 999999;
                dsMain.DATA[0].SEX = "%";
                dsMain.DATA[0].S_AGE = 0 ;
                dsMain.DATA[0].E_AGE = 99 ;
                dsMain.DATA[0].S_SALALY = 0 ;
                dsMain.DATA[0].E_SALALY = 999999 ;
        }
    }
}