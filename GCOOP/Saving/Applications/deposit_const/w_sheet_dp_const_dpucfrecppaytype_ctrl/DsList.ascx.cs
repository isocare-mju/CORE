using System;
using CoreSavingLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Saving.Applications.deposit_const.w_sheet_dp_const_dpucfrecppaytype_ctrl
{
    public partial class DsList : DataSourceRepeater
    {

        public DataSet1.DPUCFRECPPAYTYPEDataTable DATA { get; private set; }

        public void InitDs(PageWeb pw)
        {
            //ปิด css1 ตอนรันไทม์ เปลี่ยนไปใช้ css ของเฟรมแทน
            css1.Visible = false;
            //ประกาศ dataset
            DataSet1 ds = new DataSet1();
            //ตั้งค่า property DATA เอา DataTable จาก DataSet มาใช้
            this.DATA = ds.DPUCFRECPPAYTYPE;
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventItemChanged = "OnDsListItemChanged";
            //ตั้งชื่อ event ItemChanged ในรูปแบบ On<ชื่อ WebUserControl>ItemChanged
            this.EventClicked = "OnDsListClicked";

            //เรียกคำสั่งในคลาสแม่ให้การ init ก่อน
            //argument ที่ 2 ใส่ object ของ WebUserControl 
            //argument ที่ 4 ใส่ชื่อขึ้นต้นตัวเล็ก)
            this.InitDataSource(pw, Repeater1, this.DATA, "dsList");

            //กรณีมีปุ่มให้ใช้คำสั่งนี้ add เพื่อให้เกิด event clicked ที่ปุ่ม
            this.Button.Add("b_del");//ถ้าหน้าจอไม่มีปุ่มให้ลบหรือคอมเมนท์บรรทัดนี้

            //เรียกให้คลาสแม่ทำงานอีกครั้ง (เป็นคำสั่งการรวม object ต่างๆ เข้าด้วยกัน)
            this.Register();
        }

        public void Retrieve()
        {
            string sql = "SELECT * FROM DPUCFRECPPAYTYPE where COOP_ID = '" + state.SsCoopControl + "' ORDER BY   RECPPAYTYPE_CODE ";
            DataTable dt = WebUtil.Query(sql);
            //ทำยังไงก็ได้ให้ได้ DataTable หรือ Xml String ที่มีข้อมูล แล้วนำมาใช้คำสั่ง ImportData
            this.ImportData(dt);
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
    }
}