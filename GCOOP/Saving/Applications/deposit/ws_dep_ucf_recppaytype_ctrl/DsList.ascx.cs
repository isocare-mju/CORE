using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit.ws_dep_ucf_recppaytype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {
        public DataSet1.DPUCFRECPPAYTYPEDataTable DATA { get; private set; }
        public void InitDs(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DPUCFRECPPAYTYPE;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("b_del");
            this.Register();
        }
        public void DDMoneytype()
        {
            string sql = @"
                    SELECT moneytype_code AS VALUE_CODE,   
                    moneytype_desc AS VALUE_DESC,
                    1 as sort
                    FROM CMUCFMONEYTYPE  ";
            sql = WebUtil.SQLFormat(sql);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            foreach (DataRow row in dt.Rows)
            {
                string ls_display = row["value_code"].ToString().Trim() + " - " + row["value_desc"].ToString().Trim();
                row["display"] = ls_display;
            }
            dt.Rows.Add(new Object[] { "", "", 0, "--กรุณาเลือก--" });
            dt.DefaultView.Sort = "sort asc, value_code asc";
            dt = dt.DefaultView.ToTable();
            this.DropDownDataBind(dt, "moneytype_support", "display", "value_code");
        }
        public void DDAccountid(string cash_type, int rows)
        {
            string sql = @" SELECT account_Id,   
                        account_desc as type_desc
                        FROM dpucftofromaccid  
                        WHERE cash_type = {1}  and  coop_id={0} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, cash_type);
            DataTable dt = WebUtil.Query(sql);
            dt.Columns.Add("display", typeof(System.String));
            dt.Columns.Add("sort", typeof(System.Int32));
            foreach (DataRow row in dt.Rows)
            {
                string ls_name = row["ACCOUNT_ID"].ToString() + " - " + row["type_desc"].ToString();
                row["display"] = ls_name;
                row["sort"] = 1;
            }
            dt.Rows.Add(new Object[] { "", "", "--กรุณาเลือก--", 0 });
            dt.DefaultView.Sort = "sort asc,account_Id";
            dt = dt.DefaultView.ToTable();

            this.DropDownDataBind(dt, "default_accid", "display", "account_Id", rows);
        }

        public void Retrieve()
        {
            string sql = "SELECT * FROM DPUCFRECPPAYTYPE where COOP_ID = '" + state.SsCoopControl + "' ORDER BY group_itemtpe,order_sort,RECPPAYTYPE_CODE ";

            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}