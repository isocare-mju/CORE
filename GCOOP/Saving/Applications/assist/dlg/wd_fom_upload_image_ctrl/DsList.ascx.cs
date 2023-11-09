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
    public partial class DsList : DataSourceRepeater
    {
      public DataSet1.DsListDataTable DATA { get; private set; }

        public void InitDsList(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsList;
            this.EventItemChanged = "OnDsListItemChanged";
            this.EventClicked = "OnDsListClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");
            this.Button.Add("B_DEL");
            this.Button.Add("B_Show");
            this.Register();
        }

        public void Retrieve(string application, string img_type_code, string column_name, string column_data)
        {
            string sql = @"select a.seq_no,a.img_name as file_name,a.update_id,a.update_date,a.start_date,a.expire_date,a.entry_id,
                            a.entry_date,a.img_type_code,a.column_name,a.column_data,b.img_type_desc,a.data_type
                            from fomimagemaster a
                            left join fomucfworkgroup b on b.img_type_code = a.img_type_code and a.system_code = b.application
                            where a.coop_id = {0} and a.system_code = {1} and a.column_name = {2}  and a.column_data = {3} and a.img_type_code = {4}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, application, column_name, column_data,img_type_code);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);

        }
    }
}