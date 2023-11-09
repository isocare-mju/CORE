using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.CriteriaIReport.u_cri_coopid_year_rdept
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

        public void DdSdept()
        {
            String sql = @"select depttype_code,depttype_code+'--'+depttype_desc as depttype_All,1 as sorter from dpdepttype
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "s_depttype", "depttype_All", "depttype_code");
        }

        public void DdEdept()
        {
            String sql = @"select depttype_code,depttype_code+'--'+depttype_desc as depttype_All,1 as sorter from dpdepttype
                            union
                            select '','',0  order by sorter";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "e_depttype", "depttype_All", "depttype_code");
        }
    }
}