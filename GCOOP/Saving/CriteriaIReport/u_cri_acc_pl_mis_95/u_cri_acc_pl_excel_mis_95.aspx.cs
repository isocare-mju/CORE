using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using System.Globalization;


namespace Saving.CriteriaIReport.u_cri_acc_pl_mis_95
{
    public partial class u_cri_acc_pl_excel_mis_95 : PageWebReport, WebReport
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
                dsMain.DdMoneysheetcode();
                dsMain.DATA[0].show_all = "1";
                dsMain.DATA[0].data_1 = "2";
                dsMain.DATA[0].data_2 = "2";
                dsMain.DATA[0].compare_b1_b3 = "0";
                dsMain.DATA[0].show_remark = "1";
                dsMain.DATA[0].total_show = "2";
                dsMain.DATA[0].percent_status = "1";
                dsMain.DATA[0].moneysheet_code = "95";
            }
        }

        public void CheckJsPostBack(string eventArg)
        {

        }

        public void RunReport()
        {

            string coop_id = state.SsCoopId;
            String coop_name = state.SsCoopName;

            string str_year_1 = "";
            string str_year_2 = "";
            string str_period_1 = "";
            string str_period_2 = "";
            int flag = 0;
            decimal year_fw = 0;
            decimal month_fw = 0;
            String szDescTitle = "";
            //string total_show_new = "";
            try
            {
                iReportArgument arg = new iReportArgument();
                string str_sheet = dsMain.DATA[0].moneysheet_code.Trim();

                try
                {
                    str_year_1 = dsMain.DATA[0].year_1;
                    str_period_1 = dsMain.DATA[0].month_1_1;
                    int li_year1 = Convert.ToInt16(str_year_1) - 543;
                    if (Convert.ToInt16(str_period_1) < 4)
                    {
                        li_year1 = li_year1 - 1;
                    }
                    DataTable dt = WebUtil.Query("select account_year from accaccountyear where account_year ='" + li_year1 + "'");
                    if (dt.Rows.Count > 0)
                    {
                        string acc_year1;
                        acc_year1 = dt.Rows[0]["account_year"].ToString();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + str_year_1 + "");
                        flag = 1;
                    }
                }
                catch
                {
                    str_year_1 = "";
                }
                try
                {
                    str_year_2 = dsMain.DATA[0].year_1;
                    str_period_2 = dsMain.DATA[0].month_1_1;
                    int li_year2 = Convert.ToInt16(str_year_1) - 543;
                    if (Convert.ToInt16(str_period_2) < 4)
                    {
                        li_year2 = li_year2 - 1;
                    }
                    DataTable dt2 = WebUtil.Query("select account_year from accaccountyear where account_year ='" + li_year2 + "'");
                    if (dt2.Rows.Count > 0)
                    {
                        string acc_year2;
                        acc_year2 = dt2.Rows[0]["account_year"].ToString();
                    }
                    else
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลในปี พ.ศ. " + str_year_2 + "");
                        flag = 1;
                    }

                }
                catch
                {
                    str_year_2 = "";
                }
                if (flag == 0)
                {

                    short str_month_1 = Convert.ToInt16(dsMain.DATA[0].month_1_1);
                    short str_month_2 = Convert.ToInt16(dsMain.DATA[0].month_1_1);
                    short str_show_all = Convert.ToInt16(dsMain.DATA[0].show_all);
                    short str_data_1 = Convert.ToInt16(dsMain.DATA[0].data_1);
                    short str_data_2 = Convert.ToInt16(dsMain.DATA[0].data_2);
                    short str_compare_b1_b3 = 0;
                    short str_show_remark = Convert.ToInt16(dsMain.DATA[0].show_remark);
                    short str_total_show = Convert.ToInt16(dsMain.DATA[0].total_show);
                    short percent_status = Convert.ToInt16(dsMain.DATA[0].percent_status);

                    short str_year_1_new = Convert.ToInt16(str_year_1);
                    short str_year_2_new = Convert.ToInt16(str_year_2);

                    int result = wcf.NAccount.of_gen_balance_sheet_excel(state.SsWsPass, str_sheet, str_year_1_new, str_year_2_new, str_month_1, str_month_2, str_show_all, str_data_1, str_data_2, str_compare_b1_b3, str_show_remark, state.SsCoopControl, str_total_show, percent_status);


                    ExecuteDataSource exed = new ExecuteDataSource(this);
                    string sqlmis = @"select * from ACCSHEETMONEYREPORTMIS where MONEYSHEET_CODE = {0} and YEAR = {1} and MONTH = {2}";
                    Sdt dtmis = WebUtil.QuerySdt(WebUtil.SQLFormat(sqlmis, str_sheet, str_year_1, str_period_1));

                    string sql = @"select * from ACCSHEETMONEYREPORT where MONEYSHEET_CODE = {0}";
                    Sdt dt = WebUtil.QuerySdt(WebUtil.SQLFormat(sql, str_sheet));
                    dt.Next();
                   
                    if (dtmis.Next()) // กรณีมี ให้อัปเดทค่า
                    {
                        while (dt.Next())
                        {

                            string sqlupdate = @"update ACCSHEETMONEYREPORTMIS set BALANCE = {0}
                                            where YEAR = {1} and MONTH = {2} and MONEYSHEET_CODE = {3} and MONEYSHEET_SEQ = {4}";
                            sqlupdate = WebUtil.SQLFormat(sqlupdate, dt.GetDecimal("BALANCE_1"), str_year_1, str_period_1, str_sheet, dt.GetDecimal("MONEYSHEET_SEQ"));
                            exed.SQL.Add(sqlupdate);
                        }
                    }
                    else
                    { // กรณีไม่มี ให้insert 
                        string sqlinsert = @"insert into ACCSHEETMONEYREPORTMIS (
                                                    MONEYSHEET_CODE,   MONEYSHEET_SEQ,  YEAR,     MONTH,          DATA_TYPE, 
                                                    DATA_DESC,         REMARK,          BALANCE,  OPERATE_NATURE ,TOTAL_SHOW,
                                                    COOP_ID)
                                             values ({0},{1},{2},{3},{4},
                                                     {5},{6},{7},{8},{9},
                                                     {10})";
                        sqlinsert = WebUtil.SQLFormat(sqlinsert,
                                     dt.GetString("MONEYSHEET_CODE"), dt.GetString("MONEYSHEET_SEQ"), dt.GetString("YEAR_1"), dt.GetString("MONTH_1"), dt.GetString("DATA_TYPE"),
                                     dt.GetString("DATA_DESC"), dt.GetString("REMARK"), dt.GetDecimal("BALANCE_1"), dt.GetDate("OPERATE_NATURE"), dt.GetDecimal("TOTAL_SHOW"),
                                     state.SsCoopId);
                        exed.SQL.Add(sqlinsert);
                    }
                    exed.Execute();

                    //************************************
                    
                    if (str_period_1 == "1")
                    {
                        month_fw = 12;
                        year_fw = Convert.ToDecimal(str_period_1) - 1;
                    }
                    else
                    {
                        year_fw = Convert.ToDecimal(str_year_1);
                        month_fw = Convert.ToDecimal(str_period_1) - 1;
                    }                   

                    ExecuteDataSource exed1 = new ExecuteDataSource(this);
                    string sqlmisbf = @"select * from ACCSHEETMONEYREPORTMIS where MONEYSHEET_CODE = {0} and YEAR = {1} and MONTH = {2} and MONEYSHEET_SEQ = '14'";
                    Sdt dtmisbf = WebUtil.QuerySdt(WebUtil.SQLFormat(sqlmisbf, str_sheet, year_fw, month_fw));
                    if (dtmisbf.Next())
                    {
                        string sqlupdate = @"update ACCSHEETMONEYREPORTMIS set BALANCE = {0}
                                            where YEAR = {1} and MONTH = {2} and MONEYSHEET_SEQ = '1'";
                        sqlupdate = WebUtil.SQLFormat(sqlupdate, dtmisbf.GetDecimal("BALANCE"), str_year_1, str_period_1);
                        exed1.SQL.Add(sqlupdate);
                        exed1.Execute();
                    }                    


                    if (str_total_show == 1)
                    {
                        szDescTitle = "r_mis_pl_excel_mju_95";
                    }
                    else
                    {
                        szDescTitle = "r_mis_pl_excel_mju_95";

                    }

                }
                arg.Add("as_coopid", iReportArgumentType.String, state.SsCoopControl);
                arg.Add("as_year", iReportArgumentType.String, str_year_1);
                arg.Add("as_month", iReportArgumentType.String, str_period_1);
                arg.Add("as_divyear", iReportArgumentType.String, Convert.ToString(year_fw));
                arg.Add("as_divmonth", iReportArgumentType.String, Convert.ToString(month_fw));

                iReportBuider report = new iReportBuider(this, arg, szDescTitle);

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