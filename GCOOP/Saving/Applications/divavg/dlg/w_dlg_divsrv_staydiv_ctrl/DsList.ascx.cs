using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.divavg.dlg.w_dlg_divsrv_staydiv_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DTLISTDataTable DATA { get; set; }
        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DTLIST;
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.EventClicked = "OnDsListClicked";
            this.EventItemChanged = "OnDsListItemChanged";
            this.Register();
        }
        public void Retrive(string div_year)
        {
            string sql = @"
           select yrdivmethpay.member_no , 
dbo.ft_memname(yrdivmethpay.coop_id , yrdivmethpay.member_no ) as fullname , 
dbo.ft_memgrp(yrdivmethpay.coop_id , yrbgmaster.membgroup_code)  as fullgroup ,
yrdivmethpay.methpaytype_code + '-'+yrucfmethpay.methpaytype_desc as methpaytype  , 
yrdivmethpay.expense_amt as payment_amt 
from yrdivmethpay 
left join yrbgmaster on yrdivmethpay.member_no = yrbgmaster.member_no and yrdivmethpay.div_year = yrbgmaster.div_year 
left join yrucfmethpay on yrdivmethpay.methpaytype_code = yrucfmethpay.methpaytype_code 
where yrdivmethpay.ref_slipno is null 
and yrdivmethpay.expense_amt > 0 and yrdivmethpay.div_year = {0}
            ";
            sql = WebUtil.SQLFormat(sql,div_year); 
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}