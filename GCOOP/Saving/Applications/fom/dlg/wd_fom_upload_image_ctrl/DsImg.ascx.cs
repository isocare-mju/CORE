using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CoreSavingLibrary;
using DataLibrary;


namespace Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl
{
    public partial class DsImg : DataSourceFormView
    {
        public DataSet1.DsImgDataTable DATA { get; private set; }

        public void InitDsImg(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsImg;
            this.InitDataSource(pw, FormView1, this.DATA, "dsImg");
            this.EventItemChanged = "OnDsImgItemChanged";
            this.EventClicked = "OnDsImgClicked";
            this.Register();
        }
    }
}