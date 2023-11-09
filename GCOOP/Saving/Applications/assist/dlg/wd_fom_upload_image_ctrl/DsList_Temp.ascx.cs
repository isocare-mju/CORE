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
    public partial class DsList_Temp : DataSourceRepeater
    {
      public DataSet1.DsListDataTable DATA { get; private set; }

        public void InitDsList_Temp(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsList;
            this.EventItemChanged = "OnDsList_TempItemChanged";
            this.EventClicked = "OnDsList_TempClicked";
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList_Temp");
            this.Button.Add("B_DEL");
            this.Button.Add("B_Show");
            this.Register();
        }

        public void Retrieve(string token,string application, string img_type_code)
        {

            string sql = @"select  a.seq_no,a.img_type_code,b.img_type_desc,IMG_NAME as file_name,data_type,entry_date,entry_id,expire_date
                            from fomtempimages a
                            left join fomucfworkgroup b on b.img_type_code = a.img_type_code and a.system_code = b.application
                            where a.token = {1}
                            and a.system_code = {2} and a.coop_id = {0} ";
            if (img_type_code != "000")
            {
                sql += " and a.img_type_code = '" + img_type_code + "'";
            }
            sql += " order by b.img_type_code,a.seq_no";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, token, application);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }

       
    }
}