using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl
{
    public partial class DsList_Log : DataSourceRepeater
    {
      public DataSet1.DsList_LogDataTable DATA { get; private set; }

        public void InitDsList_Log(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsList_Log;
            this.EventItemChanged = "OnDsList_LogItemChanged";
            this.EventClicked = "OnDsList_LogClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList_Log");
            this.Button.Add("B_Show");
            this.Register();
        }

        public void Retrieve(string application, string img_type_code, string ColumnName, string ColumnData,DateTime s_date,DateTime n_date)
        {
            string sql = @"select date_now,data_system,entry_id,sql,system_code,column_name,column_data,img_type_code,img_name,data_type,token
                           from fomhistory 
                           where  coop_id = {0}
                           and system_code  = {1} 
                           and column_name  =  {2} 
                           and column_data  = {3} 
                           and img_type_code = {4}
                           and trunc(date_now) between {5} and {6}
                          order by date_now";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, application, ColumnName, ColumnData, img_type_code, s_date, n_date);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

       
    }
}