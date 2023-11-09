using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl
{
    public partial class DsMain : DataSourceFormView
    {

      public DataSet1.DsMainDataTable DATA { get; set; }

        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsMain;
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.EventClicked = "OnDsMainClicked";
            this.EventItemChanged = "OnDsMainItemChanged";
            this.Register();
        }

        public void DdApp(string application)
        {
            string sql = @"select a.application,b.description from fomucfworkgroup a,amappstatus b where a.application = b.application and a.img_type_status = 1 group by  a.application,b.description";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "application", "description", "application");
        }
        public void DdApp_ALL(string application)
        {
            string sql = @"select a.application,b.description from fomucfworkgroup a,amappstatus b where a.application = b.application and a.img_type_status = 1 group by  a.application,b.description";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "application", "description", "application");
        }

        public void DdDate_Type(string application, string column_name,string Fix)
        {
            string sql = "";
            if (Fix == "")
            {
                if (column_name == "emp_no")
                {
                    sql = @"select img_type_code,img_type_desc from fomucfworkgroup where application = '" + application + "' and img_type_status = 1 and img_type_code between '001' and '006' order by img_type_code";
                }
                else
                {
                    sql = @"select img_type_code,img_type_desc from fomucfworkgroup where application = '" + application + "' and img_type_status = 1 order by img_type_code";
                }
            }
            else {
                sql = @"select img_type_code,img_type_desc from fomucfworkgroup where application = '" + application + "' and img_type_status = 1 and img_type_code = '" + Fix + "'";
            }

            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "img_type_code", "img_type_desc", "img_type_code");
        }
    }
}