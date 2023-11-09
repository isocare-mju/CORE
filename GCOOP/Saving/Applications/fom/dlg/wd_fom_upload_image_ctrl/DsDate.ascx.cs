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
    public partial class DsDate : DataSourceFormView
    {
        public DataSet1.DsDateDataTable DATA { get; private set; }

        public void InitDsDate(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsDate;
            this.InitDataSource(pw, FormView1, this.DATA, "DsDate");
            this.EventItemChanged = "OnDsDateItemChanged";
            this.EventClicked = "OnDsDateClicked";
            this.Button.Add("b_search");
            this.Register();
        }
    }
}