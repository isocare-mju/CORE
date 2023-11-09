﻿using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl
{
    public partial class DsLoan : DataSourceFormView
    {
        public DataSet1.DsDataDataTable DATA { get; private set; }

        public void InitDsLoan(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DsData;
            this.InitDataSource(pw, FormView1, this.DATA, "dsLoan");
            this.EventItemChanged = "OnDsLoanItemChanged";
            this.EventClicked = "OnDsLoanClicked";
            this.Register();
        }
    }
}