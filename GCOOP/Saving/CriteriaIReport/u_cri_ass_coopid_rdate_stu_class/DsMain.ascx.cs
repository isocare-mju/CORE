﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using DataLibrary;

namespace Saving.CriteriaIReport.u_cri_ass_coopid_rdate_stu_class
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
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "coop_id", "coop_name", "coop_id");
        }

        public void DdStartassisttype()
        {
            string sql = @"select assisttype_code,assisttype_code||'-'||assisttype_desc as assisttype_desc,1 as sorter from assucfassisttype where assisttype_group  = '01'
union
select '','--เลือกประเภท--',0 from dual order by sorter , assisttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_start", "assisttype_desc", "assisttype_code");
        }

        public void DdEndassisttype()
        {
            string sql = @"select assisttype_code,assisttype_code||'-'||assisttype_desc as assisttype_desc,1 as sorter from assucfassisttype where assisttype_group  = '01'
union
select '','--เลือกประเภท--',0 from dual order by sorter , assisttype_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_end", "assisttype_desc", "assisttype_code");
        }

        public void DefaultStartassisttype()
        {
            string sql = "select min(assisttype_code) as assisttype_code from assucfassisttype where assisttype_group  = '01'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].assisttype_start = dt.GetString("assisttype_code");
            }
        }
        public void DefaultEndassisttype()
        {
            string sql = "select max(assisttype_code) as assisttype_code from assucfassisttype where assisttype_group  = '01'";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].assisttype_end = dt.GetString("assisttype_code");
            }
        }

        public void DdStartEdulevel()
        {
            string sql = @"select edulevel_code,edulevel_code||'-'||edulevel_desc as edulevel_desc,1 as sorter from assucfedulevel 
union
select '','--เลือกประเภท--',0 from dual  order by sorter , edulevel_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "edulevel_start", "edulevel_desc", "edulevel_code");
        }

        public void DdEndEdulevel()
        {
            string sql = @"select edulevel_code,edulevel_code||'-'||edulevel_desc as edulevel_desc,1 as sorter from assucfedulevel 
union
select '','--เลือกประเภท--',0 from dual  order by sorter , edulevel_code";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "edulevel_end", "edulevel_desc", "edulevel_code");
        }

        public void DefaultStartEdulevel()
        {
            string sql = "select min(edulevel_code) as edulevel_code from assucfedulevel ";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].edulevel_start = dt.GetString("edulevel_code");
            }
        }

        public void DefaultEndEdulevel()
        {
            string sql = "select max(edulevel_code) as edulevel_code from assucfedulevel";
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                this.DATA[0].edulevel_end = dt.GetString("edulevel_code");
            }
        }
    }
}