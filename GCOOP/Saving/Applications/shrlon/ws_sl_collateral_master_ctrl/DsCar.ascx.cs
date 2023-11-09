using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;

namespace Saving.Applications.shrlon.ws_sl_collateral_master_ctrl
{
    public partial class DsCar : DataSourceFormView
    {
        public DataSet1.DT_CARDataTable DATA { get; set; }

        public void InitDsCar(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.DT_CAR;
            this.EventItemChanged = "OnDsCarItemChanged";
            this.EventClicked = "OnDsCarClicked";
            this.InitDataSource(pw, FormView1, this.DATA, "dsCar");
            this.TableName = "LNCOLLMASTER";
            this.Register();
        }
        public void Retrieve(String collmast_no)
        {
            String sql = @"select   coop_id,
                                    collmast_no,
                                    collmasttype_code,
                                    car_type,
                                    car_brand,
                                    car_model,
                                    car_serialno,
                                    car_chassisno,
                                    car_color,
                                    car_enginesize
                            from    lncollmaster 
                            where   coop_id = {0} and collmast_no = {1}";
            sql = WebUtil.SQLFormat(sql, state.SsCoopControl, collmast_no);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
        }
    }
}