using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_rloantype_principal_rperiod
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.DataTable1DataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DataTable1;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void DdCoopId()
        {
            String sql = @"select coop_id, coop_name from cmcoopmaster ";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }

        public void DdSloan()
        {
            String sql = @"select loantype_code,loantype_code+'--'+loantype_desc as loantype_all,1 as sorter from lnloantype
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "S_LOANTYPE", "loantype_all", "loantype_code");
        }

        public void DdEloan()
        {
            String sql = @"select loantype_code,loantype_code+'--'+loantype_desc as loantype_all,1 as sorter from lnloantype
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "E_LOANTYPE", "loantype_all", "loantype_code");
        }
        public void DdSmembgroup()
        {
            String sql = @"select membgroup_code,membgroup_code+'--'+membgroup_desc as membgroup_all,1 as sorter from mbucfmembgroup
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "S_MEMBGROUP", "membgroup_all", "membgroup_code");
        }

        public void DdEmembgroup()
        {
            String sql = @"select membgroup_code,membgroup_code+'--'+membgroup_desc as membgroup_all,1 as sorter from mbucfmembgroup
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "E_MEMBGROUP", "membgroup_all", "membgroup_code");
        }
    }
}