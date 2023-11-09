using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.ap_deposit.ws_dep_approve_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DT_DslistDataTable DATA { get; private set; }
        public void InitList(PageWeb pw)
        {
            css1.Visible = false;

            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_Dslist;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Register();

        }
        public void RetrieveList(decimal apv_status,DateTime work_date)
        {

            string sql = @"select * from dpdeptapprove
                        where coop_id = {0} and
                        apv_status = {1} and entry_date={2}
                        order by apv_docno";

            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, apv_status, work_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}