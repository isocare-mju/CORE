using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using System.Data;
using System.Globalization;
using DataLibrary;
using System.IO;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class DsMain : DataSourceFormView
    {
        public DataSet1.ASSREQMASTERDataTable DATA { get; set; }
        public void InitDsMain(PageWeb pw)
        {
            css1.Visible = false;
            DataSet1 ds = new DataSet1();
            this.DATA = ds.ASSREQMASTER;
            this.EventItemChanged = "OnDsMainItemChanged";
            this.EventClicked = "OnDsMainClicked";
            this.Button.Add("b_search");
            //this.Button.Add("b_add");
            this.InitDataSource(pw, FormView1, this.DATA, "dsMain");
            this.Register();
        }

        public void Retrieve(String as_reqno)
        {
            String sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select 
		                        m.member_no,
		                        dbo.ft_getmbname(m.coop_id,m.member_no) as mbname,
		                        m.membgroup_code + ' : ' + mbgroup.membgroup_desc as mbgroup,
                                m.membtype_code,
		                        t.membtype_code + ' ' + t.membtype_desc mbtypedesc,
		                        m.birth_date,
		                        m.member_date,
		                        dbo.ft_calage(m.birth_date,{1},4) birth_age,
		                        dbo.ft_calage(m.member_date,{1},4) as member_age,
                                DATEDIFF(month,m.member_date,{1}) as age_membmth,
                                m.salary_amount,
		                        m.card_person,
                                t.membcat_code,
                                dbo.ft_getmbaddr(m.coop_id, m.member_no, 1) as mbaddr,
                                case m.mariage_status when 1 then 'โสด' when 2 then 'สมรส' when 3 then 'หย่า' when 4 then 'หม้าย' else 'ไม่ระบุ' end as mariage_desc,
                                case rq.req_status when 1 then 'อนุมัติ' when 8 then 'รออนุมัติ' when -9 then 'ยกเลิก'  else 'ไม่ระบุ' end as reqstatus_desc,
                                rq.*, m.retire_date
                         from assreqmaster rq
                            join mbmembmaster m on rq.member_no = m.member_no
                            join mbucfmembtype t on t.membtype_code = m.membtype_code
                            join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                where rq.assist_docno = {0} and rq.coop_id = {2} ";
            }
            else
            {
                sql = @"select 
		                        m.member_no,
		                        ft_getmbname(m.coop_id,m.member_no) as mbname,
		                        m.membgroup_code || ' : ' || mbgroup.membgroup_desc as mbgroup,
                                m.membtype_code,
		                        t.membtype_code || ' ' || t.membtype_desc mbtypedesc,
		                        m.birth_date,
		                        m.member_date,
		                        ftcm_calagemth(m.birth_date,{1}) birth_age,
		                        ftcm_calagemth(m.member_date,{1}) as member_age,
                                trunc(months_between({1},m.member_date)) as age_membmth,
                                m.salary_amount,
		                        m.card_person,
                                t.membcat_code,
                                ft_getmbaddr(m.coop_id, m.member_no, 1) as mbaddr,
                                case m.mariage_status when 1 then 'โสด' when 2 then 'สมรส' when 3 then 'หย่า' when 4 then 'หม้าย' else 'ไม่ระบุ' end as mariage_desc,
                                case rq.req_status when 1 then 'อนุมัติ' when 8 then 'รออนุมัติ' when -9 then 'ยกเลิก'  else 'ไม่ระบุ' end as reqstatus_desc,
                                rq.*, m.retire_date
                         from assreqmaster rq
                            join mbmembmaster m on rq.member_no = m.member_no
                            join mbucfmembtype t on t.membtype_code = m.membtype_code
                            join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                where rq.assist_docno = {0} and rq.coop_id = {2} ";
            }

            sql = WebUtil.SQLFormat(sql, as_reqno, state.SsWorkDate , state.SsCoopId);
            DataTable dt = WebUtil.Query(sql);
            this.ImportData(dt);
            for (int i = 0; i < this.RowCount; i++)
            {
                if (this.DATA[i].RETIRE_DATE.Year > 1900)
                {
                    if (this.DATA[i].RETIRE_DATE > DateTime.Now)
                    {
                        this.DATA[i].cp_retire_age = WebUtil.GetAge(DateTime.Now, this.DATA[i].RETIRE_DATE, 2, 1);
                    }
                    else
                    {
                        this.DATA[i].cp_retire_age = "เกษียณแล้ว";
                    }
                }
                if (this.DATA[i].RETIRE_DATE == null || this.DATA[i].RETIRE_DATE.Year == 1500) { this.DATA[i].RETIRE_DATE_TEXT = ""; } else if (this.DATA[i].RETIRE_DATE != null) { this.DATA[i].RETIRE_DATE_TEXT = this.DATA[i].RETIRE_DATE.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH")); }

            }
        }

        public void AssistType()
        {
            String sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select * from
                        (
	                        select
		                        ASSISTTYPE_CODE as assisttype_code, 
		                        ASSISTTYPE_CODE + ' - '  + ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
	                        union
	                        select
		                        '00' as assisttype_code, 
		                        'กรุณาเลือกสวัสดิการ' as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE  
                        ) a
                        order by a.sorter, a.assisttype_code
                        ";
            }
            else
            {
                sql = @"select * from
                        (
	                        select
		                        ASSISTTYPE_CODE, 
		                        ASSISTTYPE_CODE||' - '||ASSISTTYPE_DESC as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE 
	                        union
	                        select
		                        '00', 
		                        'กรุณาเลือกสวัสดิการ' as display, 
		                        1 as sorter
	                        from ASSUCFASSISTTYPE where rownum = 1
                        )
                        order by sorter, assisttype_code
                        ";
            }
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assisttype_code", "display", "assisttype_code");

        }

        public void GetAssYear()
        {
            string sql = @"select ass_year + 543 ass_show, ass_year from assucfyear order by ass_year desc";
            DataTable dt = WebUtil.Query(sql);
            this.DropDownDataBind(dt, "assist_year", "ass_show", "ass_year");
        }
        public void SaveImg(string req_docno)
        {
            try //รูปโปรไฟล์สมาชิก
            {
                FileUpload fu = (FileUpload)this.FormView1.FindControl("UploadPic");
                if (fu.HasFile)
                {
                    string fileNameProfile = Path.GetFileName(fu.PostedFile.FileName);
                    fu.PostedFile.SaveAs(Server.MapPath("~/ImageMember/assist/") + "assist_req_" + req_docno + ".jpg");
                    //LtServerMessege.Text = WebUtil.CompleteMessage("บันทึกรูปโปรไฟล์สำเร็จ");
                    // Response.Redirect(state.SsUrl);
                }
            }
            catch (Exception ex)
            {
            }

        }
    }
}