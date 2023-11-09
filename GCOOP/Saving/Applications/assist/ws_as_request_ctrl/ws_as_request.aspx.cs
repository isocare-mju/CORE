using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreSavingLibrary;
using DataLibrary;
using CoreSavingLibrary.WcfNCommon;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Globalization;

namespace Saving.Applications.assist.ws_as_request_ctrl
{
    public partial class ws_as_request : PageWebSheet, WebSheet
    {
        Sdt dt = new Sdt();
        [JsPostBack]
        public string PostMemberNo { get; set; }
        [JsPostBack]
        public string PostAssistType { get; set; }
        [JsPostBack]
        public string PostAssistYear { get; set; }
        [JsPostBack]
        public string PostCalage { get; set; }
        [JsPostBack]
        public string PostRetriveBankBranch { get; set; }
        [JsPostBack]
        public string PostLinkAddress { get; set; }
        [JsPostBack]
        public string PostGetChildAge { get; set; }
        [JsPostBack]
        public string PostCardPerson { get; set; }
        [JsPostBack]
        public string PostCardPersonParent { get; set; }
        [JsPostBack]
        public string PostInsertRow { get; set; }
        [JsPostBack]
        public string PostDelRow { get; set; }
        [JsPostBack]
        public string PostInitPermiss { get; set; }
        [JsPostBack]
        public string PostCalMedDay { get; set; }
        [JsPostBack]
        public string PostAssistPay { get; set; }
        [JsPostBack]
        public string CheckCardperson { get; set; }
        [JsPostBack]
        public string InitHistory { get; set; }
        [JsPostBack]
        public string CheckDocdate { get; set; }
        [JsPostBack]
        public string PostReqOld { get; set; }
        [JsPostBack]
        public string JsPostDispay { get; set; }
        [JsPostBack]
        public string JsCheckMemdate { get; set; }
        [JsPostBack]
        public string JsCheckMemdate_date { get; set; }
        [JsPostBack]
        public string JsSetdisamt { get; set; }
        [JsPostBack]
        public string JsPostDlgdeptno { get; set; }
        
        public string sqlStr;

        decimal gdc_membage = 0, gdc_age = 0;

        public void InitJsPostBack()
        {
            dsMain.InitDsMain(this);
            dsDecease.InitDsDecease(this);
            dsFamilydecease.InitDsFamilydecease(this);
            dsEducation.InitDsEducation(this);
            dsDisaster.InitDsDisaster(this);
            dsPatronize.InitDsPatronize(this);
            dsMedical.InitDsMedical(this);
            dsAmount.InitDsAmount(this);
            dsList.InitDsList(this);
            dsGain.InitDsGain(this);
        }

        public void WebSheetLoadBegin()
        {
            if (!IsPostBack)//show data first  
            {
                //dsMain.DATA[0].tokenmain = ""; 
                //string tokan = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                //if (dsMain.DATA[0].tokenmain == "")
                //{
                //    dsMain.DATA[0].tokenmain = tokan;
                //    HdTokenIMG.Value = tokan;
                //}
                //HdTokenIMG.Value = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                dsEducation.Visible = false;
                dsDecease.Visible = false;
                dsDisaster.Visible = false;
                dsPatronize.Visible = false;
                dsFamilydecease.Visible = false;
                dsGain.Visible = false;

                this.of_setdefaultassist();
            }
        }

        public void CheckJsPostBack(string eventArg)
        {
            if (eventArg == PostMemberNo)
            {
                string ls_memno = WebUtil.MemberNoFormat(dsMain.DATA[0].MEMBER_NO);
                string ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
                dsMain.DATA[0].MEMBER_NO = ls_memno;
                this.of_reset();
                this.of_settap();
                this.of_initpermiss();
               
            }
            else if (eventArg == PostAssistType)
            {
                if (dsMain.DATA[0].ASSIST_DOCNO == "")
                {
                    this.of_reset();
                    this.of_settap();
                    this.of_initpermiss();
                }
                else
                {
                    this.of_settap();
                    this.of_initpermiss();
                }
            }
            else if (eventArg == PostCalage)
            {
                this.of_calagemb();
                if (string.IsNullOrEmpty(dsMain.DATA[0].MEMBER_NO))
                {
                    return;
                }
                else
                {
                    this.of_setpermiss(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ASSISTTYPE_CODE);
                }
            }
            else if (eventArg == PostRetriveBankBranch)
            {
                dsAmount.RetrieveBranch(dsAmount.DATA[0].EXPENSE_BANK);
            }
            else if (eventArg == PostLinkAddress)
            {
                dsDisaster.DATA[0].DIS_ADDR = dsMain.DATA[0].mbaddr;
            }
            else if (eventArg == PostGetChildAge)
            {
                this.of_getchildage();
                this.of_ChkChildAge();
            }
            else if (eventArg == PostCardPersonParent)
            {
                this.of_postparentidcard();
            }
            else if (eventArg == PostCardPerson)
            {
                this.of_postrcvidcard();
            }
            else if (eventArg == PostInsertRow)
            {
                //int row = Convert.ToInt16(Hd_row.Value);
                string ls_mingaincode = "";
                dsGain.InsertLastRow();
                dsGain.DATA[dsGain.RowCount - 1].SEQ_NO = dsGain.RowCount;
                dsGain.DdGainRelationRow(dsGain.RowCount, ref ls_mingaincode);
                dsGain.DATA[dsGain.RowCount - 1].GAINRELATION_CODE = ls_mingaincode;
            }
            else if (eventArg == PostDelRow)
            {
                int row = dsGain.GetRowFocus();
                dsGain.DeleteRow(row);
                for (int i = 0; i < dsGain.RowCount; i++)
                {
                    dsGain.DATA[i].SEQ_NO = i + 1;
                }
            }
            else if (eventArg == PostAssistYear)
            {
                this.of_initpermiss();
            }
            else if (eventArg == PostInitPermiss)
            {
                this.of_initpermiss();
            }
            else if (eventArg == PostCalMedDay)
            {
                this.of_calmedicalday();
            }
            else if (eventArg == PostReqOld)
            {
                this.of_postreqold();
            }
            else if (eventArg == PostAssistPay)
            {
                this.of_setpermiss(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ASSISTTYPE_CODE);
            }
            else if (eventArg == JsPostDispay)
            {
                string ls_house_status = "", ls_minpaytype = "";
                ls_house_status = dsDisaster.DATA[0].DIS_HOUSE_STATUS;
                dsDisaster.DdAsspaytype(dsMain.DATA[0].ASSISTTYPE_CODE, ls_house_status, ref ls_minpaytype);
                dsDisaster.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
                this.of_setpermiss(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ASSISTTYPE_CODE);
            }
           
            else if (eventArg == CheckDocdate)
            {
                string ls_assgrp = hdassgrp.Value;
                DateTime old_date = state.SsWorkDate;
                Int32 docdate_num = 0;
                string sql = @"select docdate_num                       
                         from assucfassisttype 
		                 where assisttype_code = {0} and coop_id = {1}";
                sql = WebUtil.SQLFormat(sql, dsMain.DATA[0].ASSISTTYPE_CODE, state.SsCoopId);

                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {
                    docdate_num = dt.GetInt32("docdate_num");
                }

                if (docdate_num > 0)
                {
                    TimeSpan a = TimeSpan.FromDays(0);
                    TimeSpan b = TimeSpan.FromDays(docdate_num);


                    if (ls_assgrp == "02")
                    {
                        old_date = dsDecease.DATA[0].DEC_DEADDATE;
                        a = dsMain.DATA[0].REQ_DATE - dsDecease.DATA[0].DEC_DEADDATE;
                        if (b < a)
                        {
                            dsDecease.DATA[0].DEC_DEADDATE = old_date;
                            LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ยื่นเอกสารเกินกว่ากำหนดที่ตั้งไว้ " + docdate_num.ToString() + " วัน");
                        }
                    }
                    else if (ls_assgrp == "03")
                    {
                        old_date = dsFamilydecease.DATA[0].FAM_DOCDATE;
                        a = dsMain.DATA[0].REQ_DATE - dsFamilydecease.DATA[0].FAM_DOCDATE;
                        if (b < a)
                        {
                            dsFamilydecease.DATA[0].FAM_DOCDATE = old_date;
                            LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ยื่นเอกสารเกินกว่ากำหนดที่ตั้งไว้ " + docdate_num.ToString() + " วัน");
                        }
                    }
                    else if (ls_assgrp == "04")
                    {
                        old_date = dsDisaster.DATA[0].DIS_DISDATE;
                        a = dsMain.DATA[0].REQ_DATE - dsDisaster.DATA[0].DIS_DISDATE;
                        if (b < a)
                        {
                            dsDisaster.DATA[0].DIS_DISDATE = old_date;
                            LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ยื่นเอกสารเกินกว่ากำหนดที่ตั้งไว้ " + docdate_num.ToString() + " วัน");
                        }
                    }
                    else if (ls_assgrp == "05")
                    {
                        old_date = dsPatronize.DATA[0].FAM_DOCDATE;
                        a = dsMain.DATA[0].REQ_DATE - dsPatronize.DATA[0].FAM_DOCDATE;
                        if (b < a)
                        {
                            dsPatronize.DATA[0].FAM_DOCDATE = old_date;
                            LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ยื่นเอกสารเกินกว่ากำหนดที่ตั้งไว้ " + docdate_num.ToString() + " วัน");
                        }
                    }
                }
            }
            else if (eventArg == CheckCardperson)
            {
                string ls_assgrp = hdassgrp.Value;
                string PID = "";
                if (ls_assgrp == "01")
                {
                    PID = dsEducation.DATA[0].ASS_RCVCARDID;
                }
                else if (ls_assgrp == "03")
                {
                    PID = dsFamilydecease.DATA[0].ASS_RCVCARDID;
                }

                if (PID.Length != 13)
                {
                    dsEducation.DATA[0].ASS_RCVCARDID = "";
                    dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                    LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ครบ 13 หลัก");
                }
                else
                {
                    Int32 pidchk = 0;
                    Int32 dig = 0;
                    Int32 fdig = 0;
                    String lasttext = "";
                    try
                    {
                        pidchk = (Convert.ToInt32(PID.Substring(0, 1)) * 13) + (Convert.ToInt32(PID.Substring(1, 1)) * 12) + (Convert.ToInt32(PID.Substring(2, 1)) * 11) + (Convert.ToInt32(PID.Substring(3, 1)) * 10) + (Convert.ToInt32(PID.Substring(4, 1)) * 9) + (Convert.ToInt32(PID.Substring(5, 1)) * 8) + (Convert.ToInt32(PID.Substring(6, 1)) * 7) + (Convert.ToInt32(PID.Substring(7, 1)) * 6) + (Convert.ToInt32(PID.Substring(8, 1)) * 5) + (Convert.ToInt32(PID.Substring(9, 1)) * 4) + (Convert.ToInt32(PID.Substring(10, 1)) * 3) + (Convert.ToInt32(PID.Substring(11, 1)) * 2);
                        dig = pidchk % 11;
                        fdig = 11 - dig;
                        lasttext = fdig.ToString();
                        if (PID.Substring(12, 1) == WebUtil.Right(lasttext, 1))
                        {
                            string checkcard = "";
                            if (checkcard != "")
                            {
                                dsEducation.DATA[0].ASS_RCVCARDID = "";
                                dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                                LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง" + checkcard);
                            }
                        }
                        else
                        {
                            dsEducation.DATA[0].ASS_RCVCARDID = "";
                            dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                            LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง");
                        }
                    }
                    catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }

                }
            }
            else if (eventArg == InitHistory)
            {

                String ls_reqno = "", ls_assgrp = "";
                ls_reqno = dsList.DATA[0].assistreq_docno;

                ls_assgrp = hdassgrp.Value;
                ls_reqno = hdAssreqno.Value;

                string sql = @"select *	                       
                         from assreqmaster asm
		                 where asm.assist_docno = {0} and asm.coop_id = {1}";
                sql = WebUtil.SQLFormat(sql, ls_reqno, state.SsCoopId);

                Sdt dt = WebUtil.QuerySdt(sql);
                if (dt.Next())
                {


                    if (ls_assgrp == "01")
                    {
                        dsEducation.DATA[0].ASS_RCVNAME = dt.GetString("ass_rcvname");
                        dsEducation.DATA[0].ASS_RCVCARDID = dt.GetString("ass_rcvcardid");
                        dsEducation.DATA[0].ASS_PRCARDID = dt.GetString("ass_prcardid");
                        dsEducation.DATA[0].EDU_CHILDBIRTHDATE = dt.GetDate("edu_childbirthdate");
                        dsEducation.DATA[0].EDU_SCHOOL = dt.GetString("edu_school");
                        dsEducation.DATA[0].EDU_LEVELCODE = dt.GetString("edu_levelcode"); ;
                        dsEducation.DATA[0].EDU_GPA = dt.GetDecimal("edu_gpa");

                        of_getchildage();
                    }
                    else if (ls_assgrp == "02")
                    {
                        dsDecease.DATA[0].DEC_DEADDATE = dt.GetDate("dec_deaddate");
                        dsDecease.DATA[0].DEC_CAUSE = dt.GetString("dec_cause"); ;
                    }
                    else if (ls_assgrp == "03")
                    {
                        dsFamilydecease.DATA[0].FAM_DOCDATE = dt.GetDate("fam_docdate");
                        dsFamilydecease.DATA[0].ASS_RCVNAME = dt.GetString("ass_rcvname");
                        dsFamilydecease.DATA[0].ASS_RCVCARDID = dt.GetString("ass_rcvcardid");
                    }
                    else if (ls_assgrp == "04")
                    {
                        dsDisaster.DATA[0].DIS_ADDR = dt.GetString("dis_addr");
                        dsDisaster.DATA[0].DIS_DISDATE = dt.GetDate("dis_disdate");
                        dsDisaster.DATA[0].DIS_DISAMT = dt.GetDecimal("dis_disamt");
                        dsDisaster.DATA[0].disaster_code = dt.GetString("disaster_code");
                        dsDisaster.DATA[0].dis_homedoc = dt.GetString("dis_homedoc");
                    }
                    else if (ls_assgrp == "05")
                    {
                        dsPatronize.DATA[0].FAM_DOCDATE = dt.GetDate("fam_docdate");
                    }
                    else if (ls_assgrp == "06")
                    {

                        dsMedical.DATA[0].MED_HPNAME = dt.GetString("med_hpname");
                        dsMedical.DATA[0].MED_CAUSE = dt.GetString("med_cause"); ;
                        dsMedical.DATA[0].MED_STARTDATE = dt.GetDate("med_startdate");
                        dsMedical.DATA[0].MED_ENDDATE = dt.GetDate("med_enddate");
                        dsMedical.DATA[0].MED_DAY = dt.GetDecimal("med_day"); ;
                    }

                }

            }
            else if (eventArg == JsSetdisamt)
            {
                if (dsDisaster.DATA[0].DIS_DISAMT < dsAmount.DATA[0].ASSIST_AMT && dsDisaster.DATA[0].DIS_DISAMT > 0)
                {
                    dsAmount.DATA[0].ASSISTNET_AMT = dsDisaster.DATA[0].DIS_DISAMT;
                }
                else
                {
                    dsAmount.DATA[0].ASSISTNET_AMT = dsAmount.DATA[0].ASSIST_AMT;
                }
            }
            else if (eventArg == JsPostDlgdeptno)
            {
                dsAmount.DddeptaccountnoDlg(dsMain.DATA[0].MEMBER_NO, dsAmount.DATA[0].DEPTACCOUNT_NO);
            }
        }

        private void of_reset()
        {
            dsEducation.ResetRow();
            dsDecease.ResetRow();
            dsFamilydecease.ResetRow();
            dsDisaster.ResetRow();
            dsPatronize.ResetRow();
            dsAmount.ResetRow();
            dsGain.ResetRow();
            dsList.ResetRow();
        }

        private void of_setdefaultassist()
        {
            Sdt dt1;

            dsMain.AssistType();
            dsMain.GetAssYear();
            dsAmount.RetrieveMoneyType();
            dsAmount.DATA[0].MONEYTYPE_CODE = "CSH";

            sqlStr = @"select max(ass_year) ass_year from assucfyear where coop_id = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
            dt1 = WebUtil.QuerySdt(sqlStr);
            dt1.Next();
            dsMain.DATA[0].ASSIST_YEAR = dt1.GetInt32("ass_year");

            sqlStr = @"select min(assisttype_code) as assisttype_code from assucfassisttype where coop_id = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl);
            dt1 = WebUtil.QuerySdt(sqlStr);
            dt1.Next();

            dsMain.DATA[0].ASSISTTYPE_CODE = dt1.GetString("assisttype_code");
            dsMain.DATA[0].reqstatus_desc = "ปกติ";

            dsMain.DATA[0].REQ_DATE = state.SsWorkDate;
            dsMain.DATA[0].CALAGE_DATE = state.SsWorkDate;
            dsMain.DATA[0].COOP_ID = state.SsCoopId;

            this.of_settap();
        }

        private void of_settap()
        {
            string ls_asstype = "", ls_assgrp = "", ls_minpaytype = "", ls_mingaincode = "", ls_disaster = "", ls_invcode = "", ls_unitcode = "", ls_minunitcode = "";
            string ls_acttap = "0", invt_id = "", ls_housestatus = "", ls_minedulevel = "";

            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;

            if (string.IsNullOrEmpty(ls_asstype) || ls_asstype == "00")
            {
                return;
            }

            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, ls_asstype);
            Sdt dt = WebUtil.QuerySdt(sqlStr);

            if (dt.Next())
            {
                ls_assgrp = dt.GetString("assisttype_group");

                hdasscondition.Value = Convert.ToString(dt.GetDecimal("calculate_flag"));
                hdassgrp.Value = ls_assgrp;
                hdDateflag.Value = Convert.ToString(dt.GetDecimal("date_flag"));
                HdDocdate_num.Value = Convert.ToString(dt.GetDecimal("docdate_num"));
            }

            if (ls_assgrp == "01") //ทุนการศึกษา
            {
                dsEducation.Visible = true;
                dsDecease.Visible = false;
                dsGain.Visible = false;
                dsFamilydecease.Visible = false;
                dsDisaster.Visible = false;
                dsPatronize.Visible = false;
                dsMedical.Visible = false;                

                ls_acttap = "0";

                dsEducation.DdEducatLevel(ref ls_minedulevel);
                dsEducation.DdAsspaytype(ls_asstype, ref ls_minpaytype);
                dsEducation.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
                dsEducation.DATA[0].EDU_GPA = 4;
                dsEducation.DATA[0].EDU_LEVELCODE = ls_minedulevel;
            }
            else if (ls_assgrp == "02") //เสียชีวิต
            {
                dsEducation.Visible = false;
                dsDecease.Visible = true;
                dsGain.Visible = true;
                dsFamilydecease.Visible = false;
                dsDisaster.Visible = false;
                dsPatronize.Visible = false;
                dsMedical.Visible = false;

                ls_acttap = "1";

                dsDecease.DdAsspaytype(ls_asstype, ref ls_minpaytype);
                dsDecease.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
                dsGain.InsertLastRow();
                dsGain.DATA[dsGain.RowCount - 1].SEQ_NO = dsGain.RowCount;
                dsGain.DdGainRelation(ref ls_mingaincode);
                dsGain.DATA[dsGain.RowCount - 1].GAINRELATION_CODE = ls_mingaincode;
        
            }
            else if (ls_assgrp == "03") //ครอบครัวสมาชิก
            {
                dsEducation.Visible = false;
                dsDecease.Visible = false;
                dsGain.Visible = false;
                dsFamilydecease.Visible = true;
                dsDisaster.Visible = false;
                dsPatronize.Visible = false;
                dsMedical.Visible = false;

                ls_acttap = "2";

                dsFamilydecease.DdAsspaytype(ls_asstype, ref ls_minpaytype);
                dsFamilydecease.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
            }
            else if (ls_assgrp == "04") //ประสบภัยพิบัติ
            {
                dsEducation.Visible = false;
                dsDecease.Visible = false;
                dsGain.Visible = false;
                dsFamilydecease.Visible = false;
                dsDisaster.Visible = true;
                dsPatronize.Visible = false;
                dsMedical.Visible = false;

                ls_acttap = "3";

                
                dsDisaster.Disaster(ref ls_disaster);
                dsDisaster.DATA[0].disaster_code = ls_disaster;
                dsDisaster.HouseStatus(ref ls_housestatus);
                dsDisaster.DATA[0].DIS_HOUSE_STATUS = ls_housestatus;
                dsDisaster.DdAsspaytype(ls_asstype, ls_housestatus, ref ls_minpaytype);
                dsDisaster.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
            }
            else if (ls_assgrp == "05") //เกื้อกูลสมาชิก
            {
                dsEducation.Visible = false;
                dsDecease.Visible = false;
                dsGain.Visible = false;
                dsFamilydecease.Visible = false;
                dsDisaster.Visible = false;
                dsPatronize.Visible = true;
                dsMedical.Visible = false;

                ls_acttap = "4";

                dsPatronize.DdAsspaytype(ls_asstype,ref ls_minpaytype);
                dsPatronize.DATA[0].ASSISTPAY_CODE = ls_minpaytype;

            }
            else if (ls_assgrp == "06") //รักษาพายาบาล
            {
                dsEducation.Visible = false;
                dsDecease.Visible = false;
                dsGain.Visible = false;
                dsFamilydecease.Visible = false;
                dsDisaster.Visible = false;
                dsPatronize.Visible = false;
                dsMedical.Visible = true;

                ls_acttap = "5";

                dsMedical.DdAsspaytype(ls_asstype, ref ls_minpaytype);
                dsMedical.DATA[0].ASSISTPAY_CODE = ls_minpaytype;
            }

            hdTabIndex.Value = ls_acttap;

        }


        private void of_initpermiss()
        {
            string ls_memno = "", ls_asstype = "", ls_reqno = "";
            decimal li_year = 0;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            li_year = dsMain.DATA[0].ASSIST_YEAR;

            if (string.IsNullOrEmpty(ls_memno))
            {
                return;
            }

            // ตรวจว่ามีใบคำขอหรือยัง ถ้ามีไปเปิด
            if (this.of_haveoldreq(ls_memno, ls_asstype,li_year, ref ls_reqno))
            {
                this.of_retrieve(ls_memno, ls_asstype, ls_reqno);
                return;
            }

            // ตรวจสอบว่าเป็นสมาชิกหรือมีสิทธิ์ได้รับสวัสดิการมั้ย
            if (!this.of_chkassistmb(ls_memno, ls_asstype))
            {
                //dsMain.DATA[0].MEMBER_NO = "";
                //dsMain.DATA[0].mbname = "";

                //Decimal ass_year = dsMain.DATA[0].ASSIST_YEAR;
                //String ass_code = dsMain.DATA[0].ASSISTTYPE_CODE;
                //of_settap();
                dsMain.ResetRow();
                this.of_setdefaultassist();
                //dsMain.DATA[0].ASSIST_YEAR = ass_year;
                //dsMain.DATA[0].ASSISTTYPE_CODE = ass_code;
                //dsMain.AssistType();
                //dsMain.GetAssYear();
                return;
            }
            this.of_setmbinfo(ls_memno);
            this.of_setshare_loan(ls_memno, ls_asstype);
            this.of_setpermiss(ls_memno, ls_asstype);

            dsAmount.RetrieveBank();
            dsAmount.RetrieveBranch(dsAmount.DATA[0].EXPENSE_BANK);
            dsAmount.RetrieveMoneyType();
            //dsAmount.RetrieveDeptaccount(ls_memno);
            dsAmount.DddeptaccountnoDlg(ls_memno, dsAmount.DATA[0].DEPTACCOUNT_NO);

            //dsGain.DdGainRelation(ref ls_mingaincode);
            //dsGain.DATA[0].GAINRELATION_CODE = ls_mingaincode;

            dsList.RetrieveHistory(ls_memno, ls_asstype);
        }

        private void of_setshare_loan(string as_memno, string as_asstype)
        {
            string loantype_code = "";
            decimal principal_cal = 0, loan_percent = 0, ldc_sharevalue = 0, princal = 0;
            int loan_flag = 0;

            //set หุ้น
            string sql = @" select shsharemaster.sharestk_amt as sharestk_amt , shsharemaster.sharestk_amt * shsharetype.unitshare_value as share_value
                        from shsharemaster
                        left join shsharetype on shsharemaster.sharetype_code = shsharetype.sharetype_code and shsharemaster.coop_id = shsharetype.coop_id
                        where shsharemaster.coop_id = {0} and shsharemaster.member_no = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_memno);
            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {

                dsAmount.DATA[0].SHARE_VALUE = dt.GetDecimal("share_value");
                dsAmount.DATA[0].SHARESTK_AMT = dt.GetDecimal("sharestk_amt");
                ldc_sharevalue = dt.GetDecimal("share_value");

            }

            //set หนี้
            sql = @" select sum( principal_balance ) as principal_balance
                        from lncontmaster
                        where coop_id = {0} and member_no = {1} and contract_status <> -1 and principal_balance > 0";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_memno);
            dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsAmount.DATA[0].PRINCIPAL_BALANCE = dt.GetDecimal("principal_balance");
            }


            sql = @" select loan_flag , loan_percent
                        from assucfassisttype
                        where coop_id = {0} and assisttype_code = {1} ";
            sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_asstype);
            dt = WebUtil.QuerySdt(sql);

            if (dt.Next())
            {
                loan_flag = dt.GetInt32("loan_flag");
                loan_percent = dt.GetDecimal("loan_percent");
            }

            if (loan_flag == 1) //ตรวจสอบการหักชำระหนี้
            {
                //set หนี้ที่นำมาหักสวัสดิการ
                sql = @" select assisttype_loan 
                        from assucfassisttypeloan
                        where coop_id = {0} and assisttype_code = {1} ";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, as_asstype);
                dt = WebUtil.QuerySdt(sql);
                while (dt.Next())
                {
                    loantype_code = dt.GetString("assisttype_loan");

                    string sql2 = @" select sum( principal_balance ) as principal_cal
                        from lncontmaster
                        where coop_id = {0} and member_no = {1} and contract_status <> -1 and principal_balance > 0
                        and loantype_code = {2}";
                    sql2 = WebUtil.SQLFormat(sql2, state.SsCoopId, as_memno, loantype_code);
                    Sdt dt2 = WebUtil.QuerySdt(sql2);
                    if (dt2.Next())
                    {
                        principal_cal += dt2.GetDecimal("principal_cal");
                    }

                }
            }

            //ตรวจสอบมูลค่าหนี้ว่ามากกว่าหุ้นตามจำนวน % ที่ตั้งไว้หรือไม่

            princal = principal_cal;
            ldc_sharevalue = ldc_sharevalue * (( 100 + loan_percent) / 100);

            if (princal > ldc_sharevalue)
            {
                princal = princal - ldc_sharevalue;
                dsAmount.DATA[0].PRINCIPAL_CAL = princal;
            }
            else
            {
                dsAmount.DATA[0].PRINCIPAL_CAL = 0;
            }

        }

        private void of_setmbinfo(string as_memno)
        {
            string ls_assgrp = "", ls_asstype = "", ls_membage = "", ls_membyear = "", ls_membmonth = "", ls_mingaincode = "", ls_tranmemage = "", ls_tranyear = "";
            string ls_birthage = "", ls_birthyear = "", ls_birthmonth = "", default_paytype = "", ls_tranmonth = "";
            DateTime ldtm_reqdate;

            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            ldtm_reqdate = dsMain.DATA[0].CALAGE_DATE;

            string sql = @"select 
		                        m.member_no,
		                        dbo.ft_getmemname(m.coop_id,m.member_no) as mbname,
		                        ltrim( m.membgroup_code ) + ' : ' + rtrim( mbgroup.membgroup_desc ) as mbgroup,
                                m.membtype_code,
		                        t.membtype_code + ' ' + t.membtype_desc mbtypedesc,
                                '0' + convert(varchar(2),t.member_type)  as membcat_code,
		                        m.birth_date,
		                        m.member_date,
                                m.membtrans_date,
                                isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2) ) , 0 ) as  birth_age,
		                        isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)), 0 ) as member_age,
                                isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)), 0 ) as tranmem_age,
                                CONVERT(VARCHAR, (round( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as  lsbirth_age,
		                         CONVERT(VARCHAR, (round( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as lsmember_age,
                                 CONVERT(VARCHAR, (round( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as lstranmem_age,
                                isnull(DATEDIFF(MONTH,m.birth_date,'" + ldtm_reqdate + @"') , 0)  as age_birthmth,
                                isnull(DATEDIFF(MONTH,m.membtrans_date,'" + ldtm_reqdate + @"') , 0)  as age_tranmth,
                                isnull(DATEDIFF(month,m.member_date,'" + ldtm_reqdate + @"'), 0) as age_membmth,
                                round( ( DATEDIFF(month, m.member_date, '" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ) as mth_memage, 
                                round( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 ) / 12 as year_memage,
                                m.salary_amount,
		                        m.card_person,
                                dbo.ft_getmbaddr(m.coop_id, m.member_no, 1) as mbaddr,
                                expense_code, 
		                        expense_bank, 
		                        expense_branch, 
		                        case expense_code when 'TRN' then '' else expense_accid end expense_accid,
                                case expense_code when 'TRN' then 'DEP' else '' end as send_system,
		                        case expense_code when 'TRN' then expense_accid else '' end deptaccount_no,
                                ' ' as mate_cardperson,
                                case m.mariage_status when 1 then 'โสด' when 2 then 'สมรส' when 3 then 'หย่า' when 4 then 'หม้าย' else 'ไม่ระบุ' end as mariage_desc,
                                'ปกติ' as reqstatus_desc,m.retry_date
                         from mbmembmaster m
                              join mbucfmembtype t on t.membtype_code = m.membtype_code
                              join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                 where m.member_no = {0} ";
           
            sql = WebUtil.SQLFormat(sql, as_memno);

            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].mbname = dt.GetString("mbname");
                dsMain.DATA[0].mbgroup = dt.GetString("mbgroup");
                dsMain.DATA[0].membtype_code = dt.GetString("membtype_code");
                dsMain.DATA[0].mbtypedesc = dt.GetString("mbtypedesc");
                dsMain.DATA[0].membcat_code = dt.GetString("membcat_code");
                dsMain.DATA[0].birth_date = dt.GetDate("birth_date");
                dsMain.DATA[0].member_date = dt.GetDate("member_date");
                dsMain.DATA[0].tranmem_date = dt.GetDate("membtrans_date");
                dsMain.DATA[0].birth_age = dt.GetDecimal("birth_age");
                dsMain.DATA[0].member_age = dt.GetDecimal("member_age");
                dsMain.DATA[0].tranmem_age = dt.GetDecimal("tranmem_age");
                dsMain.DATA[0].salary_amount = dt.GetDecimal("salary_amount");
                dsMain.DATA[0].card_person = dt.GetString("card_person");
                dsMain.DATA[0].mbaddr = dt.GetString("mbaddr");
                dsMain.DATA[0].age_membmth = dt.GetInt32("age_membmth");
                dsMain.DATA[0].age_tranmth = dt.GetInt32("age_tranmth");
                dsMain.DATA[0].mariage_desc = dt.GetString("mariage_desc");
                dsMain.DATA[0].reqstatus_desc = dt.GetString("reqstatus_desc");
                dsMain.DATA[0].age_birthmth = dt.GetInt32("age_birthmth");
                dsMain.DATA[0].mth_memage = dt.GetDecimal("mth_memage");
                dsMain.DATA[0].year_memage = dt.GetDecimal("year_memage");

                dsAmount.DATA[0].MONEYTYPE_CODE = dt.GetString("expense_code");
                dsAmount.DATA[0].EXPENSE_BANK = dt.GetString("expense_bank");
                dsAmount.DATA[0].EXPENSE_BRANCH = dt.GetString("expense_branch");
                dsAmount.DATA[0].EXPENSE_ACCID = dt.GetString("expense_accid");
                dsAmount.DATA[0].DEPTACCOUNT_NO = dt.GetString("deptaccount_no");
                dsAmount.DATA[0].SEND_SYSTEM = dt.GetString("send_system");

                ls_membage = dt.GetString("lsmember_age");
                ls_birthage = dt.GetString("lsbirth_age");
                ls_tranmemage = dt.GetString("lstranmem_age");
                dsMain.DATA[0].RETIRE_DATE = dt.GetDate("retry_date");
            }
            //แปลงวันที่เป็นข้อความ
            //string[] ls_age = ls_birthage.Split('.');
            //ls_birthyear = ls_age[0] + " ปี ";
            //ls_birthmonth = ls_age[1] + " เดือน";
            dsMain.DATA[0].birthdate_th = ls_birthage;

            //string[] ls_memage = ls_membage.Split('.');
            //ls_membyear = ls_memage[0] + " ปี ";
            //ls_membmonth = ls_memage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_membage;

            //string[] ls_tranage = ls_tranmemage.Split('.');
            //ls_tranyear = ls_tranage[0] + " ปี ";
            //ls_tranmonth = ls_tranage[1] + " เดือน";
            dsMain.DATA[0].tranmem_th = ls_tranmemage;

            //วันที่เกษียณ
            if (dsMain.DATA[0].RETIRE_DATE.Year > 1900)
            {
                if (dsMain.DATA[0].RETIRE_DATE > DateTime.Now)
                {
                    dsMain.DATA[0].cp_retire_age = WebUtil.GetAge(DateTime.Now, dsMain.DATA[0].RETIRE_DATE, 2, 1);
                }
                else
                {
                    dsMain.DATA[0].cp_retire_age = "เกษียณแล้ว";
                }
            }
            if (dsMain.DATA[0].RETIRE_DATE == null || dsMain.DATA[0].RETIRE_DATE.Year == 1500) { dsMain.DATA[0].RETIRE_DATE_TEXT = ""; } else if (dsMain.DATA[0].RETIRE_DATE != null) { dsMain.DATA[0].RETIRE_DATE_TEXT = dsMain.DATA[0].RETIRE_DATE.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH")); }


            sql = @" select stm_flag, stmpay_num, stmpay_type, default_paytype from assucfassisttype where assisttype_code = '" + ls_asstype + "' ";
            dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                Int32 li_stmflag, li_stmtype, li_stmnum;

                li_stmflag = dt.GetInt32("stm_flag");
                li_stmnum = dt.GetInt32("stmpay_num");
                li_stmtype = dt.GetInt32("stmpay_type");
                default_paytype = dt.GetString("default_paytype");

                dsAmount.DATA[0].STM_FLAG = li_stmflag;

                if (li_stmflag == 1)
                {
                    // ถ้าเป็นรับทุนปีแต่ระบุเดือนเป็น 0 ให้เอาเดือนที่ขอตั้ง
                    if (li_stmtype == 2 && li_stmnum == 0)
                    {
                        li_stmnum = ldtm_reqdate.Month;
                    }
                    dsAmount.DATA[0].STMPAY_NUM = li_stmnum;
                    dsAmount.DATA[0].STMPAY_TYPE = li_stmtype;
                }

                //เชคการ default การจ่าย
                if (default_paytype == "TRN")
                {
                    string sqldeptno = "select min(deptaccount_no) as deptaccount_no from dpdeptmaster where member_no = {0} and deptclose_status = 0 and coop_id = {1}";
                    sqldeptno = WebUtil.SQLFormat(sqldeptno, as_memno, state.SsCoopId);
                    Sdt dt2 = WebUtil.QuerySdt(sqldeptno);
                    if (dt2.Next())
                    {
                        dsAmount.DATA[0].MONEYTYPE_CODE = "TRN";
                        dsAmount.DATA[0].EXPENSE_BANK = "";
                        dsAmount.DATA[0].EXPENSE_BRANCH = "";
                        dsAmount.DATA[0].EXPENSE_ACCID = "";
                        dsAmount.DATA[0].DEPTACCOUNT_NO = dt2.GetString("deptaccount_no");
                        dsAmount.DATA[0].SEND_SYSTEM = "DEP";
                    }
                }
                else if (default_paytype == "CSH")
                {
                    dsAmount.DATA[0].MONEYTYPE_CODE = "CSH";
                    dsAmount.DATA[0].EXPENSE_BANK = "";
                    dsAmount.DATA[0].EXPENSE_BRANCH = "";
                    dsAmount.DATA[0].EXPENSE_ACCID = "";
                    dsAmount.DATA[0].DEPTACCOUNT_NO = "";
                    dsAmount.DATA[0].SEND_SYSTEM = "";
                }

            }

            ls_assgrp = hdassgrp.Value;

            if (ls_assgrp == "02")
            {
                dsGain.RetrieveGainMb(as_memno);
                dsGain.DdGainRelation(ref ls_mingaincode);
            }
            else if (ls_assgrp == "01")
            {
                dsEducation.DATA[0].ASS_PRCARDID = dt.GetString("mate_cardperson");
            }
        }

        private void of_calagemb()
        {
            string ls_memno, ls_membage, ls_birthage, ls_birthyear, ls_birthmonth, ls_membyear, ls_membmonth, ls_tranage, ls_tranyear, ls_tranmonth;
            DateTime ldtm_caldate;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            if (string.IsNullOrEmpty(ls_memno))
            {
                return;
            }
            string sql = "";
            if (Sta.IS_OLEDB_MODE)
            {
                sql = @"select isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2) ) , 0 ) as  birth_age,
		                        isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)), 0 ) as member_age,
                                isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)), 0 ) as tran_age,
                                CONVERT(VARCHAR, (round( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as  lsbirth_age,
		                         CONVERT(VARCHAR, (round( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as lsmember_age,
                                 CONVERT(VARCHAR, (round( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, m.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as lstranmem_age,
                                isnull( DATEDIFF(month,m.membtrans_date,{1}) , 0 ) as age_tranmth,
                                isnull( DATEDIFF(month,m.member_date,{1}), 0) as age_membmth,
                                isnull( DATEDIFF(month,m.birth_date,{1}), 0) as age_birthmth,
                                m.retry_date
                         from mbmembmaster m
                              join mbucfmembtype t on t.membtype_code = m.membtype_code
                              join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                 where m.member_no = {0}";
            }
            else
            {
                sql = @"select 
		                        ftcm_calagemth(m.birth_date,{1}) birth_age,
		                        ftcm_calagemth(m.member_date,{1}) as member_age,
                                ftcm_calagemth(m.membtrans_date,{1}) as tran_age,
                                nvl( trunc(months_between({1},m.membtrans_date)) , 0 ) as age_tranmth,
                                nvl( trunc(months_between({1},m.member_date)), 0) as age_membmth,
                                nvl( trunc(months_between({1},m.birth_date)), 0) as age_birthmth,
                                m.retry_date
                         from mbmembmaster m
                              join mbucfmembtype t on t.membtype_code = m.membtype_code
                              join mbucfmembgroup mbgroup on m.membgroup_code = mbgroup.membgroup_code
		                 where m.member_no = {0} ";
            }
            sql = WebUtil.SQLFormat(sql, ls_memno, ldtm_caldate);

            Sdt dt = WebUtil.QuerySdt(sql);
            if (dt.Next())
            {
                dsMain.DATA[0].birth_age = dt.GetDecimal("birth_age");
                dsMain.DATA[0].member_age = dt.GetDecimal("member_age");
                dsMain.DATA[0].age_membmth = dt.GetInt32("age_membmth");
                dsMain.DATA[0].tranmem_age = dt.GetDecimal("tran_age");
                dsMain.DATA[0].age_tranmth = dt.GetInt32("age_tranmth");
                dsMain.DATA[0].RETIRE_DATE = dt.GetDate("retry_date");
                dsMain.DATA[0].age_birthmth = dt.GetInt32("age_birthmth");
            }

            //ls_membage = Convert.ToString(dt.GetDecimal("member_age"));
            //ls_birthage = Convert.ToString(dt.GetDecimal("birth_age"));
            //ls_tranage = Convert.ToString(dt.GetDecimal("tran_age"));
            ls_membage = dt.GetString("lsmember_age");
            ls_birthage = dt.GetString("lsbirth_age");
            ls_tranage = dt.GetString("lstranmem_age");

            //แปลงวันที่เป็นข้อความ
            //string[] ls_age = ls_birthage.Split('.');
            //ls_birthyear = ls_age[0] + " ปี ";
            //ls_birthmonth = ls_age[1] + " เดือน";
            dsMain.DATA[0].birthdate_th = ls_birthage;

            //string[] ls_memage = ls_membage.Split('.');
            //ls_membyear = ls_memage[0] + " ปี ";
            //ls_membmonth = ls_memage[1] + " เดือน";
            dsMain.DATA[0].membdate_th = ls_membage;

            //string[] ls_tranmemage = ls_tranage.Split('.');
            //ls_tranyear = ls_tranmemage[0] + " ปี ";
            //ls_tranmonth = ls_tranmemage[1] + " เดือน";
            dsMain.DATA[0].tranmem_th = ls_tranage;

            //วันที่เกษียณ
            if (dsMain.DATA[0].RETIRE_DATE.Year > 1900)
            {
                if (dsMain.DATA[0].RETIRE_DATE > DateTime.Now)
                {
                    dsMain.DATA[0].cp_retire_age = WebUtil.GetAge(DateTime.Now, dsMain.DATA[0].RETIRE_DATE, 2, 1);
                }
                else
                {
                    dsMain.DATA[0].cp_retire_age = "เกษียณแล้ว";
                }
            }
            if (dsMain.DATA[0].RETIRE_DATE == null || dsMain.DATA[0].RETIRE_DATE.Year == 1500) { dsMain.DATA[0].RETIRE_DATE_TEXT = ""; } else if (dsMain.DATA[0].RETIRE_DATE != null) { dsMain.DATA[0].RETIRE_DATE_TEXT = dsMain.DATA[0].RETIRE_DATE.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("th-TH")); }

        }

        private void of_calmedicalday()
        {
            DateTime ldtm_start = dsMedical.DATA[0].MED_STARTDATE;
            DateTime ldtm_end = dsMedical.DATA[0].MED_ENDDATE;
            Int32 li_medday = 0;

            li_medday = Convert.ToInt32((ldtm_end - ldtm_start).TotalDays);

            if (li_medday > 0)
            {
                dsMedical.DATA[0].MED_DAY = li_medday;
                of_setpermiss(dsMain.DATA[0].MEMBER_NO, dsMain.DATA[0].ASSISTTYPE_CODE);
                //if (hdasscondition.Value == "6")
                //{
                //    this.of_initpermiss();
                //}
            }

        }

        public void SaveWebSheet()
        {

            string ls_reqno = "", ls_assgrp = "";
            Boolean lb_isnew = false;

            ls_reqno = dsMain.DATA[0].ASSIST_DOCNO;

            // เป็นคำขอใหม่
            if (string.IsNullOrEmpty(ls_reqno))
            {
                ls_reqno = wcf.NCommon.of_getnewdocno(state.SsWsPass, state.SsCoopId, "ASSISTDOCNO");
                lb_isnew = true;
            }

            dsAmount.DATA[0].COOP_ID = state.SsCoopControl;
            dsAmount.DATA[0].ASSIST_DOCNO = ls_reqno;
            dsAmount.DATA[0].MEMBER_NO = dsMain.DATA[0].MEMBER_NO;
            dsAmount.DATA[0].ASSISTTYPE_CODE = dsMain.DATA[0].ASSISTTYPE_CODE;
            dsAmount.DATA[0].ASSIST_YEAR = dsMain.DATA[0].ASSIST_YEAR;
            dsAmount.DATA[0].REQ_DATE = dsMain.DATA[0].REQ_DATE;
            dsAmount.DATA[0].CALAGE_DATE = dsMain.DATA[0].CALAGE_DATE;
            dsAmount.DATA[0].ASS_RCVNAME = dsMain.DATA[0].mbname;
            dsAmount.DATA[0].ASS_RCVCARDID = dsMain.DATA[0].card_person;

            dsAmount.DATA[0].REQ_STATUS = 8;
            dsAmount.DATA[0].ENTRY_ID = state.SsUsername;

            //////////////////////////init ค่าไม่ให้ null/////////////////////////////////////////

            dsAmount.DATA[0].ASS_RCVNAME = "";
            dsAmount.DATA[0].ASS_RCVCARDID = "";
            dsAmount.DATA[0].ASS_PRCARDID = "";

            dsAmount.DATA[0].EDU_SCHOOL = "";
            dsAmount.DATA[0].EDU_LEVELCODE = "";
            dsAmount.DATA[0].EDU_GPA = 0;
            dsAmount.DATA[0].ASSISTPAY_CODE = "";

            dsAmount.DATA[0].DEC_CAUSE = "";


            dsAmount.DATA[0].DIS_ADDR = "";
            dsAmount.DATA[0].DIS_DISAMT = 0;
            dsAmount.DATA[0].disaster_code = "";//////////////////////////////////////////////////////////////////
            dsAmount.DATA[0].dis_homedoc = "";//////////////////////////////////////////////////////////////////

            dsAmount.DATA[0].MED_HPNAME = "";
            dsAmount.DATA[0].MED_CAUSE = "";
            dsAmount.DATA[0].MED_DAY = 0;
            string savetype = "";

            //////////////////////////////////////////////////////////////////

            ls_assgrp = hdassgrp.Value;

            if (ls_assgrp == "01")
            {
                dsAmount.DATA[0].ASS_RCVNAME = dsEducation.DATA[0].ASS_RCVNAME;
                dsAmount.DATA[0].ASS_RCVCARDID = dsEducation.DATA[0].ASS_RCVCARDID;
                dsAmount.DATA[0].ASS_PRCARDID = dsEducation.DATA[0].ASS_PRCARDID;
                dsAmount.DATA[0].EDU_CHILDBIRTHDATE = dsEducation.DATA[0].EDU_CHILDBIRTHDATE;
                dsAmount.DATA[0].EDU_SCHOOL = dsEducation.DATA[0].EDU_SCHOOL;
                dsAmount.DATA[0].EDU_LEVELCODE = dsEducation.DATA[0].EDU_LEVELCODE;
                dsAmount.DATA[0].EDU_GPA = dsEducation.DATA[0].EDU_GPA;
                dsAmount.DATA[0].ASSISTPAY_CODE = dsEducation.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "02")
            {
                dsAmount.DATA[0].DEC_DEADDATE = dsDecease.DATA[0].DEC_DEADDATE;
                dsAmount.DATA[0].DEC_CAUSE = dsDecease.DATA[0].DEC_CAUSE;
                dsAmount.DATA[0].ASSISTPAY_CODE = dsDecease.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "03")
            {
                dsAmount.DATA[0].FAM_DOCDATE = dsFamilydecease.DATA[0].FAM_DOCDATE;
                dsAmount.DATA[0].ASS_RCVNAME = dsFamilydecease.DATA[0].ASS_RCVNAME;
                dsAmount.DATA[0].ASS_RCVCARDID = dsFamilydecease.DATA[0].ASS_RCVCARDID;
                dsAmount.DATA[0].ASSISTPAY_CODE = dsFamilydecease.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "04")
            {
                dsAmount.DATA[0].DIS_ADDR = dsDisaster.DATA[0].DIS_ADDR;
                dsAmount.DATA[0].DIS_DISDATE = dsDisaster.DATA[0].DIS_DISDATE;
                dsAmount.DATA[0].DIS_DISAMT = dsDisaster.DATA[0].DIS_DISAMT;
                /////////////////////////////////////////////////////////////////////////////////
                dsAmount.DATA[0].disaster_code = dsDisaster.DATA[0].disaster_code;
                if (dsAmount.DATA[0].disaster_code == "")
                {
                    dsAmount.DATA[0].disaster_code = "01";
                }
                dsAmount.DATA[0].dis_homedoc = dsDisaster.DATA[0].dis_homedoc;
                dsAmount.DATA[0].DIS_HOUSE_STATUS = dsDisaster.DATA[0].DIS_HOUSE_STATUS;    
                /////////////////////////////////////////////////////////////////////////////////
                dsAmount.DATA[0].ASSISTPAY_CODE = dsDisaster.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "05")
            {
                dsAmount.DATA[0].ASSISTPAY_CODE = dsPatronize.DATA[0].ASSISTPAY_CODE;
                dsAmount.DATA[0].FAM_DOCDATE = dsPatronize.DATA[0].FAM_DOCDATE;
                //dsAmount.DATA[0].DIS_DISDATE = dsPatronize.DATA[0].DIS_DISDATE;
            }
            else if (ls_assgrp == "06")
            {
                dsAmount.DATA[0].ASSISTPAY_CODE = dsMedical.DATA[0].ASSISTPAY_CODE;

                dsAmount.DATA[0].MED_HPNAME = dsMedical.DATA[0].MED_HPNAME;
                dsAmount.DATA[0].MED_CAUSE = dsMedical.DATA[0].MED_CAUSE;
                dsAmount.DATA[0].MED_STARTDATE = dsMedical.DATA[0].MED_STARTDATE;
                dsAmount.DATA[0].MED_ENDDATE = dsMedical.DATA[0].MED_ENDDATE;
                dsAmount.DATA[0].MED_DAY = dsMedical.DATA[0].MED_DAY;
                savetype = " ,MED_HPNAME = '" + dsAmount.DATA[0].MED_HPNAME + "' ,MED_CAUSE = '" + dsAmount.DATA[0].MED_CAUSE + "'" +
                    " ,MED_STARTDATE = to_date('" + dsAmount.DATA[0].MED_STARTDATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')  , MED_ENDDATE = to_date('" + dsAmount.DATA[0].MED_ENDDATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy') , MED_DAY = '"+dsAmount.DATA[0].MED_DAY+"' " ;
            }

            try
            {
                ExecuteDataSource exe = new ExecuteDataSource(this);

                if (lb_isnew)
                {
                    string sqlStr = @"INSERT INTO ASSREQMASTER
                    (COOP_ID 			            , ASSIST_DOCNO 		            , ASSISTTYPE_CODE               , ASSIST_YEAR                   , REQ_DATE 
                    , CALAGE_DATE                   , MEMBER_NO                     , ASSISTPAY_CODE                , ASSIST_AMT                    , ASSISTMAX_AMT
                    , ASSISTEVER_AMT                , ASSISTCUT_AMT                 , ASSISTNET_AMT                 , REMARK                        , REQ_STATUS 
                    , ENTRY_ID                     
                    , MONEYTYPE_CODE                , EXPENSE_BANK                  , EXPENSE_BRANCH                , EXPENSE_ACCID                 , SEND_SYSTEM 
                    , DEPTACCOUNT_NO                , ASS_RCVNAME                   , ASS_RCVCARDID                 , ASS_PRCARDID                  , EDU_CHILDBIRTHDATE 
                    , EDU_SCHOOL                    , EDU_LEVELCODE                 , EDU_GPA                       , DEC_DEADDATE                  , DEC_CAUSE 
                    , FAM_DOCDATE                   , DIS_ADDR                      , DIS_DISDATE                   , DIS_DISAMT                    , MED_HPNAME 
                    , MED_CAUSE                     , MED_STARTDATE                 , MED_ENDDATE                   , MED_DAY                       , STM_FLAG
                    , STMPAY_TYPE                   , STMPAY_NUM                    , PRINCIPAL_BALANCE             , PRINCIPAL_CAL                 ,  SHARESTK_AMT
                    , SHARE_VALUE                   , dis_homedoc                   , dis_distype                   , dis_house_status
                    )
                    values
                    ({0}                            , {1}                           , {2}                           , {3}                           , {4} 
                    , {5}                           , {6}                           , {7}                           , {8}                           , {9}  
                    , {10}                          , {11}                          , {12}                          , {13}                          , {14}   
                    , {15}                         
                    , {16}                          , {17}                          , {18}                          , {19}                          , {20}
                    , {21}                          , {22}                          , {23}                          , {24}                          , {25}
                    , {26}                          , {27}                          , {28}                          , {29}                          , {30}
                    , {31}                          , {32}                          , {33}                          , {34}                          , {35}
                    , {36}                          , {37}                          , {38}                          , {39}                          , {40}
                    , {41}                          , {42}                          , {43}                          , {44}                          , {45}
                    , {46}                          , {47}                          , {48}                          , {49}
                    )";

                    sqlStr = WebUtil.SQLFormat(sqlStr
                  , state.SsCoopId, ls_reqno, dsAmount.DATA[0].ASSISTTYPE_CODE, dsAmount.DATA[0].ASSIST_YEAR, dsAmount.DATA[0].REQ_DATE
                  , dsAmount.DATA[0].CALAGE_DATE, dsAmount.DATA[0].MEMBER_NO, dsAmount.DATA[0].ASSISTPAY_CODE, dsAmount.DATA[0].ASSIST_AMT, dsAmount.DATA[0].ASSISTMAX_AMT
                  , dsAmount.DATA[0].ASSISTEVER_AMT, dsAmount.DATA[0].ASSISTCUT_AMT, dsAmount.DATA[0].ASSISTNET_AMT, dsAmount.DATA[0].REMARK, dsAmount.DATA[0].REQ_STATUS
                  , state.SsUsername
                  , dsAmount.DATA[0].MONEYTYPE_CODE, dsAmount.DATA[0].EXPENSE_BANK, dsAmount.DATA[0].EXPENSE_BRANCH, dsAmount.DATA[0].EXPENSE_ACCID, dsAmount.DATA[0].SEND_SYSTEM
                  , dsAmount.DATA[0].DEPTACCOUNT_NO, dsAmount.DATA[0].ASS_RCVNAME, dsAmount.DATA[0].ASS_RCVCARDID, dsAmount.DATA[0].ASS_PRCARDID, dsAmount.DATA[0].EDU_CHILDBIRTHDATE
                  , dsAmount.DATA[0].EDU_SCHOOL, dsAmount.DATA[0].EDU_LEVELCODE, dsAmount.DATA[0].EDU_GPA, dsAmount.DATA[0].DEC_DEADDATE, dsAmount.DATA[0].DEC_CAUSE
                  , dsAmount.DATA[0].FAM_DOCDATE, dsAmount.DATA[0].DIS_ADDR, dsAmount.DATA[0].DIS_DISDATE, dsAmount.DATA[0].DIS_DISAMT, dsAmount.DATA[0].MED_HPNAME
                  , dsAmount.DATA[0].MED_CAUSE, dsAmount.DATA[0].MED_STARTDATE, dsAmount.DATA[0].MED_ENDDATE, dsAmount.DATA[0].MED_DAY, dsAmount.DATA[0].STM_FLAG
                  , dsAmount.DATA[0].STMPAY_TYPE, dsAmount.DATA[0].STMPAY_NUM, dsAmount.DATA[0].PRINCIPAL_BALANCE, dsAmount.DATA[0].PRINCIPAL_CAL, dsAmount.DATA[0].SHARESTK_AMT
                  , dsAmount.DATA[0].SHARE_VALUE, dsAmount.DATA[0].dis_homedoc, dsAmount.DATA[0].disaster_code, dsAmount.DATA[0].DIS_HOUSE_STATUS
                  );


                    WebUtil.ExeSQL(sqlStr);
                }
                else

                {
                    string sqlStr_update = "";
                    if (Sta.IS_OLEDB_MODE)
                    {
                        sqlStr_update = "UPDATE ASSREQMASTER SET" +
                            " ASSISTTYPE_CODE   = '" + dsAmount.DATA[0].ASSISTTYPE_CODE + "', ASSISTNET_AMT     = '" + dsAmount.DATA[0].ASSISTNET_AMT + "'  , ASSISTCUT_AMT     = '" + dsAmount.DATA[0].ASSISTCUT_AMT + "'" +
                            ", ASSISTMAX_AMT    = '" + dsAmount.DATA[0].ASSISTMAX_AMT + "'	, ASSIST_AMT        = '" + dsAmount.DATA[0].ASSIST_AMT + "'     , ASSISTPAY_CODE    = '" + dsAmount.DATA[0].ASSISTPAY_CODE + "'" +
                            ", REMARK           = '" + dsAmount.DATA[0].REMARK + "'         , ENTRY_ID          = '" + state.SsUsername + "'	            , MONEYTYPE_CODE 	= '" + dsAmount.DATA[0].MONEYTYPE_CODE + "'" +
                            ", EXPENSE_BRANCH   = '" + dsAmount.DATA[0].EXPENSE_BRANCH + "' , EXPENSE_ACCID     = '" + dsAmount.DATA[0].EXPENSE_ACCID + "'  , DEPTACCOUNT_NO    = '" + dsAmount.DATA[0].DEPTACCOUNT_NO + "'" +
                            ", EXPENSE_BANK     = '" + dsAmount.DATA[0].EXPENSE_BANK + "'   , ASS_RCVNAME       = '" + dsAmount.DATA[0].ASS_RCVNAME + "'    , ASS_RCVCARDID     = '" + dsAmount.DATA[0].ASS_RCVCARDID + "'" +
                            ", EDU_SCHOOL       = '" + dsAmount.DATA[0].EDU_SCHOOL + "'     , EDU_GPA           = '" + dsAmount.DATA[0].EDU_GPA + "'        , SEND_SYSTEM       = '" + dsAmount.DATA[0].SEND_SYSTEM + "'" +
                            ", DIS_DISAMT       = '" + dsAmount.DATA[0].DIS_DISAMT + "'     , CALAGE_DATE       = to_date('" + dsAmount.DATA[0].CALAGE_DATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')    , EDU_CHILDBIRTHDATE  =  to_date('" + dsAmount.DATA[0].EDU_CHILDBIRTHDATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')" +
                            ",DIS_ADDR          ='" + dsAmount.DATA[0].DIS_ADDR + "'" +
                            ", DIS_HOUSE_STATUS = '" + dsAmount.DATA[0].DIS_HOUSE_STATUS + "'  , EDU_LEVELCODE    = '" + dsAmount.DATA[0].EDU_LEVELCODE + "'" + savetype +
                            " WHERE ASSIST_DOCNO = '" + dsAmount.DATA[0].ASSIST_DOCNO + "' and coop_id = '" + state.SsCoopId + "'";
                       
                    }
                    else
                    {
                        sqlStr_update = "UPDATE ASSREQMASTER SET" +
                            " ASSISTTYPE_CODE   = '" + dsAmount.DATA[0].ASSISTTYPE_CODE + "', ASSISTNET_AMT     = '" + dsAmount.DATA[0].ASSISTNET_AMT + "'  , ASSISTCUT_AMT     = '" + dsAmount.DATA[0].ASSISTCUT_AMT + "'" +
                            ", ASSISTMAX_AMT    = '" + dsAmount.DATA[0].ASSISTMAX_AMT + "'	, ASSIST_AMT        = '" + dsAmount.DATA[0].ASSIST_AMT + "'     , ASSISTPAY_CODE    = '" + dsAmount.DATA[0].ASSISTPAY_CODE + "'" +
                            ", REMARK           = '" + dsAmount.DATA[0].REMARK + "'         , ENTRY_ID          = '" + state.SsUsername + "'	            , MONEYTYPE_CODE 	= '" + dsAmount.DATA[0].MONEYTYPE_CODE + "'" +
                            ", EXPENSE_BRANCH   = '" + dsAmount.DATA[0].EXPENSE_BRANCH + "' , EXPENSE_ACCID     = '" + dsAmount.DATA[0].EXPENSE_ACCID + "'  , DEPTACCOUNT_NO    = '" + dsAmount.DATA[0].DEPTACCOUNT_NO + "'" +
                            ", EXPENSE_BANK     = '" + dsAmount.DATA[0].EXPENSE_BANK + "'   , ASS_RCVNAME       = '" + dsAmount.DATA[0].ASS_RCVNAME + "'    , ASS_RCVCARDID     = '" + dsAmount.DATA[0].ASS_RCVCARDID + "'" +
                            ", EDU_SCHOOL       = '" + dsAmount.DATA[0].EDU_SCHOOL + "'     , EDU_GPA           = '" + dsAmount.DATA[0].EDU_GPA + "'        , SEND_SYSTEM       = '" + dsAmount.DATA[0].SEND_SYSTEM + "'" +
                            ", DIS_DISAMT       = '" + dsAmount.DATA[0].DIS_DISAMT + "'     , CALAGE_DATE       = to_date('" + dsAmount.DATA[0].CALAGE_DATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')    , EDU_CHILDBIRTHDATE  =  to_date('" + dsAmount.DATA[0].EDU_CHILDBIRTHDATE.ToString("dd/MM/yyyy") + @"','dd/MM/yyyy')" +
                            ",DIS_ADDR          ='" + dsAmount.DATA[0].DIS_ADDR + "'" +
                            ", DIS_HOUSE_STATUS = '" + dsAmount.DATA[0].DIS_HOUSE_STATUS + "'  , EDU_LEVELCODE    = '" + dsAmount.DATA[0].EDU_LEVELCODE + "'" + savetype +
                            " WHERE ASSIST_DOCNO = '" + dsAmount.DATA[0].ASSIST_DOCNO + "' and coop_id = '" + state.SsCoopId + "'";
                        
                    }
                    Sdt sql_update = WebUtil.QuerySdt(sqlStr_update);
                }

                // ถ้าเป็นหมวดตาย บันทึกผู้รับโอนด้วย
                if (ls_assgrp == "02")
                {
                    string ls_gainname = "", ls_relation = "";
                    string sqldel = "", sqlInsert = "";
                    Int32 li_seqno = 0;
                    decimal ldc_gainamt = 0;

                    sqldel = "delete assreqmastergain where assist_docno ='" + ls_reqno + "'";
                    exe.SQL.Add(sqldel);

                    for (int i = 0; i < dsGain.RowCount; i++)
                    {
                        ls_gainname = dsGain.DATA[i].GAIN_NAME;
                        ls_relation = dsGain.DATA[i].GAINRELATION_CODE;
                        ldc_gainamt = dsGain.DATA[i].GAINASSIST_AMT;

                        li_seqno++;

                        sqlInsert = @"insert into assreqmastergain
                                    (coop_id, assist_docno, seq_no, gain_name, gainassist_amt, gainrelation_code)
                                  values ({0},{1},{2},{3},{4},{5})";
                        sqlInsert = WebUtil.SQLFormat(sqlInsert, state.SsCoopControl, ls_reqno, li_seqno, ls_gainname, ldc_gainamt, ls_relation);
                        exe.SQL.Add(sqlInsert);
                    }
                }

                exe.Execute();

            }
            catch (Exception ex)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บันทึกไม่สำเร็จ" + ex);
                return;
            }
            dsMain.SaveImg(dsAmount.DATA[0].ASSIST_DOCNO);
            LtServerMessage.Text = WebUtil.CompleteMessage("บันทึกใบคำขอเลขที่ " +ls_reqno+" สำเร็จ");
            //string tokan = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            //HdTokenIMG.Value = tokan;
            //dsMain.DATA[0].tokenmain = tokan;
            dsMain.ResetRow();
            dsList.ResetRow();
            dsEducation.ResetRow();
            dsDecease.ResetRow();
            dsFamilydecease.ResetRow();
            dsDisaster.ResetRow();
            dsPatronize.ResetRow();
            dsAmount.ResetRow();
            dsGain.ResetRow();

            this.of_setdefaultassist();
        }

        private void of_ChkChildAge()
        {
            string ls_chkchild = "", ls_child_year = "";
            decimal ldc_chkchild = 0, ldc_child_age = 0;
            ls_chkchild = HdChkChildAge.Value; // มากกว่า 0 แปลว่าเช็คอายุบุตร

            if (ls_chkchild != "0")
            {
                ldc_chkchild = Convert.ToDecimal(ls_chkchild);
                ldc_child_age = dsEducation.DATA[0].edu_childage;

                if (ldc_child_age > ldc_chkchild)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("อายุบุตรเกินกว่าที่กำหนดไว้ " + ls_chkchild + " ปี กรุณาตรวจสอบ");
                }

            }
        }

        public void of_getchildage()// ตรวจสอบ อายุบุตร
        {
            string ls_brithdate = "", ls_childage = "" , ls_year = "" , ls_month = "";
            DateTime? ldtm_birth = null;

            ldtm_birth = dsEducation.DATA[0].EDU_CHILDBIRTHDATE;
            ls_brithdate = dsEducation.DATA[0].EDU_CHILDBIRTHDATE.ToString("dd/MM/yyyy");

            if (ldtm_birth.HasValue && ls_brithdate != "01/01/1500")
            {
                if (Sta.IS_OLEDB_MODE)
                {
                    //sqlStr = "select dbo.FT_CALAGE(convert(datetime," + ls_brithdate + ",106), convert(datetime, getdate()), 4)  child_age";
                    sqlStr = "select isnull( CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2) ) , 0 ) as  child_age,
                                 CONVERT(VARCHAR, (round( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						        + '.'+
						        CONVERT(VARCHAR, round( ( DATEDIFF(month, '" + ldtm_birth + "','" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 ))  as lschild_age ";
                }
                else {
                    sqlStr = "select FT_CALAGE(to_date('" + ls_brithdate + "','dd/mm/yyyy'), sysdate, 4) child_age from dual ";
                }
                Sdt dt = WebUtil.QuerySdt(sqlStr);

                if (dt.Next())
                {
                    dsEducation.DATA[0].edu_childage = dt.GetDecimal("child_age");
                    ls_childage = dt.GetString("lschild_age");//Convert.ToString(dt.GetDecimal("child_age"));
                }

                //string[] child_age = ls_childage.Split('.');
                //ls_year = child_age[0] + " ปี ";
                //ls_month = child_age[1] + " เดือน";
                dsEducation.DATA[0].childage_th = ls_childage;
            } 
            else
            {
                dsEducation.DATA[0].edu_childage = 0;
            }

            if (dsEducation.DATA[0].edu_childage < 3 )
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("บุตรมีอายุต่ำกว่า 3 ปี กรุณาตรวจสอบ");
            }

        }

        public void WebSheetLoadEnd()
        {
            if (dsAmount.DATA[0].MONEYTYPE_CODE == "CSH" || dsAmount.DATA[0].MONEYTYPE_CODE == "")
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = false;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = false;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = false;
                dsAmount.FindDropDownList(0, "send_system").Enabled = false;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = false;
            }
            else if (dsAmount.DATA[0].MONEYTYPE_CODE == "TRN")
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = false;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = false;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = false;
                dsAmount.FindDropDownList(0, "send_system").Enabled = true;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = true;
            }
            else
            {
                dsAmount.FindDropDownList(0, "expense_bank").Enabled = true;
                dsAmount.FindTextBox(0, "expense_accid").Enabled = true;
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = true;
                dsAmount.FindDropDownList(0, "send_system").Enabled = false;
                dsAmount.FindDropDownList(0, "deptaccount_no").Enabled = false;
            }


            if (hdassgrp.Value == "02")
            {
                String ls_mingaincode = "";
                dsGain.DdGainRelation(ref ls_mingaincode);
                this.SetOnLoadedScript("document.getElementById('insertRow').style.display = 'block',+$('#ctl00_ContentPlace_dsAmount_FormView1_b_search').show()");
            }
            else
            {
                this.SetOnLoadedScript("$('#ctl00_ContentPlace_dsAmount_FormView1_b_search').hide()");
            }

            if (state.SsCoopId == "056001") {
                //user ให้เปิดช่อง สาขา 9/03/2021  
                dsAmount.FindDropDownList(0, "expense_branch").Enabled = true;
            }
        }

        // สิทธิ์สวัสดิการ
        private decimal of_getpermissmax(string as_memno, string as_asscode, string as_paycode, decimal adc_rank, ref decimal adc_max)
        {
            Int32 li_mbtypechk = 1, li_limnum = 0, li_mbcattypechk = 1, li_formula_flag = 0, li_rcvnum = 0; 
            decimal ldc_maxamt = 0, ldc_maxloop = 0, ldc_permiss = 0, ldc_maxnextpay = 0, ldc_minfirstpay = 0, ldc_maxfirstpay = 0; 
            decimal ldc_mbrank = 0, ldc_calpermiss = 0;
            int li_year = 0;
            Sdt dtd;

            // ดูเงื่อนไขกาารให้สวัสดิการ
            sqlStr = @"select limitreq_num, limitmax_amt , max_nextpayamt, limrcvreq_num , formula_flag, min_firstpayamt, max_firstpayamt from assucfassisttype where coop_id = {0} and assisttype_code = {1}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode);
            dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_limnum = dtd.GetInt32("limitreq_num");
                ldc_maxloop = dtd.GetDecimal("limitmax_amt");
                ldc_maxnextpay = dtd.GetDecimal("max_nextpayamt");
                li_rcvnum = dtd.GetInt32("limrcvreq_num");
                li_formula_flag = dtd.GetInt32("formula_flag");
                ldc_minfirstpay = dtd.GetDecimal("min_firstpayamt");
                ldc_maxfirstpay = dtd.GetDecimal("max_firstpayamt");
            }

            // ตรวจสอบก่อนว่ามีการแยกกลุ่มสมาชิกหรือไม่
            sqlStr = @"select count(1) as membtype_flag from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and  membcat_code = 'AL' ";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR);
            dtd = WebUtil.QuerySdt(sqlStr);

             if (dtd.Next())
            {
                li_mbcattypechk = dtd.GetInt32("membtype_flag");
            }

             if (li_mbcattypechk == 0) //0 = เช็คกลุ่มสมาชิก,   มากกว่า 0 คือ ไม่เช็ค
             {

                 //เช็คตัวหลักก่อน
                 sqlStr = "select membtype_code from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and membcat_code = {3}";
                 sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR, dsMain.DATA[0].membcat_code);
                 dtd = WebUtil.QuerySdt(sqlStr);

                 if (dtd.GetRowCount() == 0)
                 {
                     li_mbtypechk = 0;
                 }
                 else //กรณ๊มีสิทธิ์ ต้องมาเช็คประเภทย่อยอีกว่ามีสิทธิ์ได้รับสวัสดิการหรือไม่
                 {
                     //ตรวจสอบประเภทสมาชิกว่าแยกประเภทมั้ย
                     sqlStr = @"select count(1) as membtype_flag from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and membcat_code = {3} and membtype_code = 'AL' ";
                     sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR, dsMain.DATA[0].membcat_code);
                     dtd = WebUtil.QuerySdt(sqlStr);

                     if (dtd.Next())
                     {
                         li_mbtypechk = dtd.GetInt32("membtype_flag");
                     }
                 }
             }

            ldc_mbrank = adc_rank;
            li_year = Convert.ToInt16( dsMain.DATA[0].ASSIST_YEAR);

            // ตรวจสอบว่าอยู่ในสิทธิ์ช่วงไหน
            sqlStr = @"select max_payamt,max_check
                        from assucfassisttypedet 
                        where assisttype_code = '" + as_asscode + "' and assist_year = '" + li_year  + "' and assistpay_code = '" + as_paycode + @"' 
                        and " + Convert.ToString(ldc_mbrank) + " between min_check and max_check ";

            if (li_mbcattypechk == 0) //0 = เช็คกลุ่มสมาชิก,   มากกว่า 0 คือ ไม่เช็ค
            {
                sqlStr = sqlStr + " and membcat_code = '" + dsMain.DATA[0].membcat_code + "' ";
            }
            if (li_mbtypechk == 0) //0 = เช็คประเภทสมาชิก,   มากกว่า 0 คือ ไม่เช็ค
            {
                sqlStr = sqlStr + " and membtype_code = '" + dsMain.DATA[0].membtype_code + "' ";
            }

            dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ldc_maxamt = dtd.GetDecimal("max_payamt");
            }
            else
            {
                if (as_asscode.Trim() == "60")
                {
                    string sqlmax = @" select max(max_check ) as max_check,max(max_payamt) as max_payamt from assucfassisttypedet   where assisttype_code = '" + as_asscode + "' and assist_year = '" + li_year + "' and assistpay_code = '" + as_paycode + "' ";
                  Sdt dtm = WebUtil.QuerySdt(sqlmax);
                  if (dtm.Next())
                  {
                      //dsMedical.DATA[0].MED_DAY = dtm.GetDecimal("max_check");
                      ldc_maxamt = dtm.GetDecimal("max_payamt");
                  }
                }
                else
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบสิทธิ์สวัสดิการของสมาชิกคนนี้ กรุณาตรวจสอบ");
                    return 0;
                }
                
            }

            adc_max = ldc_maxamt;

            // กรณีที่รับได้หลายครั้งและมีการกำหนดวงเงินเอาไว้
            if (li_limnum > 1 && ldc_maxloop > 0)
            {
                //adc_max = ldc_maxloop;
                ldc_maxamt = ldc_maxnextpay;
            }

            ldc_permiss = ldc_maxamt;

            //กรณีคำนวณพิเศษ
            if (li_formula_flag == 1)
            {
                //สิทธิ์ได้สวัสดิการ คือ อายุสมาชิก(ปี) * จำนวนหุ้น / 100
                //หาสูตร   01 = อายุการเป็นสมาชิก , 02 = อายุสมาชิก , 03 = จำนวนหุ้น , 04 = มูลค่าหุ้น , 05 = ค่าคงที่ 100
                // 1 บวก, 2 ลบ , 3 คูณ , 4 หาร
                string ls_caltype = "";
                int li_calflag = 0;
                decimal ldc_formula_permiss = 0, ldc_sharevalue = 0, ldc_sharestk = 0;
                decimal cal1 = 0, cal2 = 0 , ldc_paybalance = 0;

                string sql = @"select *
                        from asscalculatorassist
                        where coop_id = {0} 
                        and assisttype_code = {1}
                        order by sort_order";
                sql = WebUtil.SQLFormat(sql, state.SsCoopId, dsMain.DATA[0].ASSISTTYPE_CODE);
                Sdt dt = WebUtil.QuerySdt(sql);

                int row = dt.Rows.Count;


                for (int i = 0; i < row; i++)
                {
                    ls_caltype = dt.Rows[i].Field<string>("calculator_type");
                    li_calflag = Convert.ToInt32(dt.Rows[i].Field<Decimal>("operation_type"));

                    if (ls_caltype == "01") { cal1 = gdc_membage; }
                    else if (ls_caltype == "02") { cal1 = gdc_age; }
                    else if (ls_caltype == "03")
                    {
                        string sqlshr = @" select shsharemaster.sharestk_amt as sharestk_amt , shsharemaster.sharestk_amt * shsharetype.unitshare_value as share_value
                        from shsharemaster
                        left join shsharetype on shsharemaster.sharetype_code = shsharetype.sharetype_code and shsharemaster.coop_id = shsharetype.coop_id
                        where shsharemaster.coop_id = {0} and shsharemaster.member_no = {1} ";
                        sqlshr = WebUtil.SQLFormat(sqlshr, state.SsCoopId, as_memno);
                        Sdt dtshr = WebUtil.QuerySdt(sqlshr);
                        if (dtshr.Next())
                        {
                            ldc_sharestk = dtshr.GetDecimal("sharestk_amt");
                            //ldc_sharevalue = dtshr.GetDecimal("share_value");
                        }

                        cal1 = ldc_sharestk;
                    }
                    else if (ls_caltype == "04")
                    {
                        string sqlshr = @" select shsharemaster.sharestk_amt as sharestk_amt , shsharemaster.sharestk_amt * shsharetype.unitshare_value as share_value
                        from shsharemaster
                        left join shsharetype on shsharemaster.sharetype_code = shsharetype.sharetype_code and shsharemaster.coop_id = shsharetype.coop_id
                        where shsharemaster.coop_id = {0} and shsharemaster.member_no = {1} ";
                        sqlshr = WebUtil.SQLFormat(sqlshr, state.SsCoopId, as_memno);
                        Sdt dtshr = WebUtil.QuerySdt(sqlshr);
                        if (dtshr.Next())
                        {
                            //ldc_sharestk = dtshr.GetDecimal("sharestk_amt");
                            ldc_sharevalue = dtshr.GetDecimal("share_value");
                        }

                        cal1 = ldc_sharevalue;
                    }
                    else if (ls_caltype == "05") { cal1 = 100; }


                    if (li_calflag == 1)
                    {
                        ldc_formula_permiss = ldc_formula_permiss + cal1;
                    }
                    else if (li_calflag == 2)
                    {
                        ldc_formula_permiss = ldc_formula_permiss - cal1;
                    }
                    else if (li_calflag == 3)
                    {
                        ldc_formula_permiss = ldc_formula_permiss * cal1;
                    }
                    else if (li_calflag == 4)
                    {
                        ldc_formula_permiss = ldc_formula_permiss / cal1;
                    }
                }

                // จบ for loop


                //กรณีสวัสดิการที่จ่ายต่อเนื่องตรวจสอบว่าเคยรับสวัสดิการนี้แล้วรึยัง
//                if (dsAmount.DATA[0].STM_FLAG != 0)
//                {
//                    string sqlasscont = @"select pay_balance from asscontmaster ass
//                      where ass.member_no = '" + as_memno + "' and ass.assisttype_code='" + as_asscode + "' and ass.asscont_status <> -9  and ass.withdraw_able > 0 ";
//                    //sqlasscont = WebUtil.SQLFormat(sqlasscont, state.SsCoopId, as_memno);
//                    Sdt dtasscont = WebUtil.QuerySdt(sqlasscont);
//                    if (dtasscont.Next())

//                    while (dtasscont.Next())
//                    {
//                        ldc_paybalance = dtasscont.GetDecimal("pay_balance");  //ถ้ามากกว่า 0 แปลว่าเคยรับสวัสดิการแล้ว
//                        //ldc_usedamt = ldc_usedamt + ldc_apvamt;
//                        if (ldc_formula_permiss < ldc_minfirstpay)
//                        {
//                            ldc_formula_permiss = ldc_minfirstpay;
//                        }

//                        if (ldc_formula_permiss > ldc_maxfirstpay)
//                        {
//                            ldc_formula_permiss = ldc_maxfirstpay;
//                        }
//                    }
//                }

                if (state.SsCoopControl == "056001")
                {
                    
                if (ldc_formula_permiss >= ldc_permiss * 2)
                {
                    ldc_permiss = ldc_permiss;
                }
                else
                {
                    ldc_permiss = dsAmount.DATA[0].SHARE_VALUE / 2;
                }


                if (ldc_permiss < 10000) { ldc_permiss = 10000; }
                    
                }
                else
                {
                    ldc_permiss = ldc_formula_permiss;
                }
            }

            //สวัสดิการ 60 ปี ใช้หุ้นคำนวณด้วย
            if (state.SsCoopControl == "061001" && dsMain.DATA[0].ASSISTTYPE_CODE == "52")
            {
                Decimal ldc_sharevalue = dsAmount.DATA[0].SHARE_VALUE;
                Decimal ldc_mthmemage = dsMain.DATA[0].mth_memage;
                Decimal ldc_yearmemage = dsMain.DATA[0].year_memage;
                Decimal ldc_permiss_memage = 0;
                if (ldc_sharevalue >= 100000 && ldc_sharevalue <= 250000)
                {
                    ldc_permiss = 5000;
                }
                else if (ldc_sharevalue >= 250010 && ldc_sharevalue <= 500000)
                {
                    ldc_permiss = 6000;
                }
                else if (ldc_sharevalue >= 500010 && ldc_sharevalue <= 750000)
                {
                    ldc_permiss = 7000;
                }
                else if (ldc_sharevalue >= 750010 && ldc_sharevalue <= 1000000)
                {
                    ldc_permiss = 8000;
                }
                else if (ldc_sharevalue >= 1000010 )
                {
                    ldc_permiss = 9000;
                }

                //ตรวจสอบสิทธิ์จากอายุสมาชิก
                if (ldc_mthmemage >= 6)
                {
                    ldc_yearmemage = ldc_yearmemage + 1;
                }
                ldc_permiss_memage = ldc_yearmemage * 300;

                if (ldc_permiss_memage > ldc_permiss)
                {
                    ldc_permiss = ldc_permiss_memage;
                   
                }

            }

            return ldc_permiss;
        }

        private decimal of_getpermissever(string as_memno, string as_asscode)
        {
            string ls_sqlext = "";
            Int32 li_numper = 0, li_perunit = 0, li_cuttype = 0;
            decimal ldc_usedamt = 0, ldc_apvamt = 0;

            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
                li_cuttype = dtd.GetInt32("limitcut_type");
            }

            if (li_cuttype == 0) //ไม่หักสิทธิ์ที่เบิกไปแล้ว
            {
                return 0;
            }

            // หักสิทธิที่เบิกไปแล้ว หาก่อนว่าช่วงมันเป็นแบบไหน
            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            ls_sqlext = "";// this.of_getsqlrank(li_perunit, li_numper, "ass");

            sqlStr = @"select * from asscontmaster ass
                      where ass.member_no = '" + as_memno + "' and ass.assisttype_code='" + as_asscode + "' and ass.asscont_status <> -9 ";
            sqlStr = sqlStr + ls_sqlext;
            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ldc_apvamt = dtd.GetDecimal("pay_balance");
                ldc_usedamt = ldc_usedamt + ldc_apvamt;
            }

            return ldc_usedamt;
        }

        private decimal of_getpermisscut(string as_memno, string as_asscode)
        {
            string ls_sqlext = "";
            Int32 li_numper = 0, li_perunit = 0;
            decimal ldc_cutamt = 0, ldc_apvamt = 0;

            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
            }

            // หักสิทธิที่เบิกไปแล้ว หาก่อนว่าช่วงมันเป็นแบบไหน
            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");

            sqlStr = @"select * from asscontmaster ass
                       where exists ( select 1 from assucfassisttypecut acut where acut.assisttype_code = '" + as_asscode + @"' and ass.assisttype_code = acut.assisttype_cut )
                       and ass.member_no = '" + as_memno + "' and ass.asscont_status <> -9 ";
            sqlStr = sqlStr + ls_sqlext;
            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ldc_apvamt = dtd.GetDecimal("pay_balance");
                ldc_cutamt = ldc_cutamt + ldc_apvamt;
            }

            return ldc_cutamt;
        }

        private Boolean of_chkassistmb(string as_memno, string as_asscode)
        {
            string ls_assgrp = "", ls_mbtypecode = "", ls_mbtypedesc = "", ls_sqlext = "", ls_sqlunion = "", ls_sqlextrq = "", ls_mbcatcode = "";
            string ls_cardid = "", ls_rcvtype = "", ls_memrcv = "", ls_asspausetype = "", ls_assinstype = "";
            Int32 li_resignstt = 0, li_deadstt = 0, li_mbtypechk = 0 , li_mbcattypechk = 0;
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1, li_round = 0;
            Int32 li_cntass = 0, li_family = 0, li_age = 0 , li_loan = 0, li_membdate = 0;
            Decimal ldc_memage = 0, ldc_age = 0, ldc_agenum = 0, ldc_tranage;
            DateTime ldtm_memdate , ldtm_membdate;

            // ตรวจสอบสถานะของสมาชิกที่ทำการขอสวัสดิการ
            string sqlStr = "";
            if (Sta.IS_OLEDB_MODE)
            {
//                sqlStr = @" select mb.card_person, mb.resign_status, mb.dead_status, mb.membtype_code, mt.membtype_desc ,  '0' + convert(varchar(2),mt.member_type)  as membcat_code, mb.member_date,
//                        dbo.ft_calage(mb.birth_date, '"+dsMain.DATA[0].CALAGE_DATE+@"',4) birth_age,
//		                dbo.ft_calage(mb.member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"',4) as member_age,
//                        dbo.ft_calage(mb.membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"',4) as tran_age
//                        from mbmembmaster mb 
//                            join mbucfmembtype mt on mb.membtype_code = mt.membtype_code 
//                        where mb.member_no = {0}	";

                sqlStr = @" select mb.card_person, mb.resign_status, mb.dead_status, mb.membtype_code, mt.membtype_desc ,  '0' + convert(varchar(2),mt.member_type)  as membcat_code, mb.member_date,
                        CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						+ '.'+
						CONVERT(VARCHAR, round( ( DATEDIFF(month, birth_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2) ) as  birth_age,
		                CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						+ '.'+
						CONVERT(VARCHAR, round( ( DATEDIFF(month, member_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)) as member_age,
                        CAST( CONVERT(VARCHAR, (round( DATEDIFF(month, membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') - ( DATEDIFF(month, membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ) , 0 )/12) ) 
						+ '.'+
						CONVERT(VARCHAR, round( ( DATEDIFF(month, membtrans_date,'" + dsMain.DATA[0].CALAGE_DATE + @"') % 12 ), 0 )) AS DECIMAL(4,2)) as tran_age
                        from mbmembmaster mb 
                            join mbucfmembtype mt on mb.membtype_code = mt.membtype_code 
                        where mb.member_no = {0}	";
            }
            else
            {
                sqlStr = @" select mb.card_person, mb.resign_status, mb.dead_status, mb.membtype_code, mt.membtype_desc , mg.membcat_code, mb.member_date,
                        ftcm_calagemth(mb.birth_date, {1} ) birth_age,
		                ftcm_calagemth(mb.member_date,{1}) as member_age,
                        ftcm_calagemth(mb.membtrans_date,{1}) as tran_age
                        from mbmembmaster mb 
                            join mbucfmembtype mt on mb.membtype_code = mt.membtype_code 
                            join mbucfcategory mg on mt.membcat_code = mg.membcat_code
                        where mb.member_no = {0}";
            }
            sqlStr = WebUtil.SQLFormat(sqlStr, as_memno, dsMain.DATA[0].CALAGE_DATE);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ls_cardid = dtd.GetString("card_person");
                li_resignstt = dtd.GetInt32("resign_status");
                li_deadstt = dtd.GetInt32("dead_status");
                ls_mbtypecode = dtd.GetString("membtype_code");
                ls_mbtypedesc = dtd.GetString("membtype_desc");
                ls_mbcatcode = dtd.GetString("membcat_code");
                ldc_age = dtd.GetDecimal("birth_age");
                ldc_memage = dtd.GetDecimal("member_age");
                ldc_tranage = dtd.GetDecimal("tran_age");
                ldtm_membdate = dtd.GetDate("member_date");
            }
            else
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("ไม่พบข้อมูลสมาชิกเลขที่ " + as_memno + " กรุณาตรวจสอบ ");
                return false;
            }


            if (li_round == 0) ////0 ไม่ปัด , 1 ปัดเต็มปี  2 ปัดทิ้ง
            {
                gdc_membage = ldc_memage;
                gdc_age = ldc_age;
            }
            else if (li_round == 1)
            {
                //round_age = Math.Ceiling(ldc_age);
                gdc_membage = Math.Ceiling(ldc_memage);
                gdc_age = Math.Ceiling(ldc_age);
            }
            else
            {
                gdc_membage = Math.Floor(ldc_memage);
                gdc_age = Math.Floor(ldc_age);
            }

            if (li_resignstt == 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ " + as_memno + " ได้ลาออกไปแล้วไม่สามารถขอสวัสดิการได้อีก ");
                return false;
            }

            // ตรวจสอบว่าประเภทสมาชิกนี้ได้รับสิทธิ์ในสวัสดิการนี้หรือไม่
            //1. ตรวจสอบก่อนว่ามีการกลุ่มสมาชิกหรือไม่
            sqlStr = @"select count(1) as membtype_flag from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and  membcat_code = 'AL' ";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR);
            dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_mbcattypechk = dtd.GetInt32("membtype_flag");
            }

            if (li_mbcattypechk == 0) //0 = เช็คกลุ่มสมาชิก,   มากกว่า 0 คือ ไม่เช็ค
            {

                //เช็คตัวหลักก่อน
                sqlStr = "select membtype_code from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and membcat_code = {3}";
                sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR, ls_mbcatcode);
                dtd = WebUtil.QuerySdt(sqlStr);

                if (dtd.GetRowCount() == 0)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้เป็นสมาชิกประเภท " + ls_mbtypedesc + " ไม่ได้รับสิทธิ์ในสวัสดิการประเภทนี้ ");
                    return false;
                }
                else //กรณ๊มีสิทธิ์ ต้องมาเช็คประเภทย่อยอีกว่ามีสิทธิ์ได้รับสวัสดิการหรือไม่
                {
                    //ตรวจสอบประเภทสมาชิกว่าแยกประเภทมั้ย
                    sqlStr = @"select count(1) as membtype_flag from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and membcat_code = {3} and membtype_code = 'AL' ";
                    sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR, ls_mbcatcode);
                    dtd = WebUtil.QuerySdt(sqlStr);

                    if (dtd.Next())
                    {
                        li_mbtypechk = dtd.GetInt32("membtype_flag");
                    }
                    if (li_mbtypechk == 0) //0 = เช็คประเภทสมาชิก,   มากกว่า 0 คือ ไม่เช็ค
                    {
                        sqlStr = "select membtype_code from assucfassisttypedet where coop_id = {0} and assisttype_code = {1} and assist_year = {2} and membcat_code = {3} and membtype_code = {4}";
                        sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_asscode, dsMain.DATA[0].ASSIST_YEAR, ls_mbcatcode, ls_mbtypecode);
                        dtd = WebUtil.QuerySdt(sqlStr);
                        if (dtd.GetRowCount() == 0)
                        {
                            LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้เป็นสมาชิกประเภท " + ls_mbtypedesc + " ไม่ได้รับสิทธิ์ในสวัสดิการประเภทนี้ ");
                            return false;
                        }
                    }
                
                }
            }


            // config สำหรับการตรวจสอบสวัสดิการ
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ls_assgrp = dtd.GetString("assisttype_group");

                li_family = dtd.GetInt32("family_flag");

                li_numreq = dtd.GetInt32("limitreq_num");
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
                li_age = dtd.GetInt32("age_flag");
                li_loan = dtd.GetInt32("loan_flag");
                ldc_agenum = dtd.GetDecimal("age_num");            
                li_round = dtd.GetInt32("ageround_flag");   //0 ไม่ปัด , 1 ปัดเต็มปี  2 ปัดทิ้ง   
                li_membdate = dtd.GetInt32("membdate_flag"); //0 ไม่ตรวจ  1 เป็นสมาชิกก่อนวันที่, 2 เป็นสมาชิกหลังวันที่ , 3 ขอสวัสดิการได้ถึงวันที่
            }

            if (li_age == 1) //ตรวจสอบอายุสมาชิก
            {
                Decimal round_age = 0;  

                if( li_round == 0)
                {
                    round_age = ldc_age;
                }
                else if( li_round == 1)
                {
                    round_age = Math.Ceiling(ldc_age);  
                }
                else
                {
                    round_age = Math.Floor(ldc_age);  
                }

                if (ldc_agenum > round_age)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้อายุไม่ถึงตามเกณฑ์ที่กำหนดไว้ " + ldc_agenum+ " ปี กรุณาตรวจสอบ");
                    return false;
                }
            }
            else if (li_age == 2)
            {
                Decimal round_age = 0;  

                if( li_round == 0)
                {
                    round_age = ldc_memage;
                }
                else if( li_round == 1)
                {
                    round_age = Math.Ceiling(ldc_memage);  
                }
                else
                {
                    round_age = Math.Floor(ldc_memage);  
                }

                if (ldc_agenum > round_age)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้อายุการเป็นสมาชิกไม่ถึงตามเกณฑ์ที่กำหนดไว้ " + ldc_agenum+ " ปี กรุณาตรวจสอบ");
                    return false;
                }
                
            }

            if (li_membdate == 1) //เป็นสมาชิกก่อนวันที่
            {
                try
                {
                    ldtm_memdate = dtd.GetDate("member_date");
                    if (ldtm_membdate > ldtm_memdate)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้วันที่เป็นสมาชิกไม่ตรงตามเกณฑ์ที่กำหนดไว้ กรุณาตรวจสอบ");
                        return false;
                    }
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบการตั้งข้อมูลการตรวจสอบวันที่เป็นสมาชิกได้ กรุณาตรวจสอบ");
                }

            }
            else if (li_membdate == 2) //เป็นสมาชิกตั้งแต่วันที่
            {
                try
                {
                    ldtm_memdate = dtd.GetDate("member_date");
                    if (ldtm_membdate <= ldtm_memdate)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้วันที่เป็นสมาชิกไม่ตรงตามเกณฑ์ที่กำหนดไว้ กรุณาตรวจสอบ");
                        return false;
                    }
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบการตั้งข้อมูลการตรวจสอบวันที่เป็นสมาชิกได้ กรุณาตรวจสอบ");
                }
            }
            else if (li_membdate == 3) //ขอสวัสดิการได้ถึงวันที่
            {
                try
                {
                    ldtm_memdate = dtd.GetDate("member_date");
                    ldtm_membdate = dsMain.DATA[0].REQ_DATE;
                    if (ldtm_membdate > ldtm_memdate)
                    {
                        LtServerMessage.Text = WebUtil.ErrorMessage("วันที่ขอสวัสดิการ เกินกำหนดที่ตั้งไว้ กรุณาตรวจสอบ");
                        return false;
                    }
                }
                catch
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ไม่สามารถตรวจสอบการตั้งข้อมูลการตรวจสอบวันที่เป็นสมาชิกได้ กรุณาตรวจสอบ");
                }
            }
               

            //if (ls_assgrp == "02" && li_deadstt == 0)
            //{
            //    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ยังไม่มีสถานะเป็นเสียชีวิต " + as_memno + " กรุณาปรับสถานะก่อน");
            //    return false;
            //}
            //else
                if (ls_assgrp != "02" && li_deadstt == 1)
            {
                LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้เสียชีวิตแล้ว " + as_memno + " ไม่สามารถขอสวัสดิการได้อีก");
                return false;
            }

            // ของเขตการหา
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");
            ls_sqlextrq = this.of_getsqlrankreq(li_perunit, li_numper, "req");

            sqlStr = @" select 'OWN' as rcvtype, ass.member_no from asscontmaster ass 
                        where ass.member_no = '" + as_memno + @"' 
                        and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext + @"
                        union
                        select 'OWN' as rcvtype, req.member_no from assreqmaster req
                        where req.member_no = '" + as_memno + @"' 
                        and req.assisttype_code='" + as_asscode + "' and req.req_status = 8 " + ls_sqlextrq;

            // ถ้าเป็นทุนการศึกษาและต้องตรวจสอบครอบครัว
            if (li_family == 1 && ls_assgrp == "01")
            {
                ls_sqlunion = @"
                        union
                        select 'PR' as rcvtype, ass.member_no from asscontmaster ass
                        where ass.ass_prcardid = '" + ls_cardid + @"'
                        and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext + @"
                        union
                        select 'PR' as rcvtype, req.member_no from assreqmaster req
                        where req.ass_prcardid = '" + ls_cardid + @"'
                        and req.assisttype_code='" + as_asscode + "' and req.req_status = 8 " + ls_sqlextrq;

                sqlStr = sqlStr + ls_sqlunion;
            }


            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                li_cntass++;
                ls_rcvtype = dtd.GetString("rcvtype");
                ls_memrcv = dtd.GetString("member_no");

                if (li_cntass >= li_numreq)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ได้เคยขอสวัสดิการประเภทนี้ไปแล้ว  และยังไม่ถึงเวลาที่จะขอสวัสดิการได้");
                    return false;
                }
            }

            //ตรวจสอบประเภทการงดขอสวัสดิการ

                sqlStr = "select assisttype_pause from assucfassisttypepause where assisttype_code = '" + as_asscode + "' and coop_id = '"+ state.SsCoopId + "'";
                dtd = WebUtil.QuerySdt(sqlStr);

                while (dtd.Next())
                {
                    
                        ls_asspausetype = dtd.GetString("assisttype_pause");

                        string sqlcheck = @" select  ass.member_no 
                        from asscontmaster ass where ass.assisttype_code='" + ls_asspausetype + @"' and ass.asscont_status <> -9 and ass.member_no = '" + as_memno + @"'
                        union
                        select req.member_no from assreqmaster req
                        where req.assisttype_code='" + ls_asspausetype + "' and req.req_status = 8 and req.member_no = '" + as_memno + "'";
                        Sdt dtcheck = WebUtil.QuerySdt(sqlcheck);
                        if (dtcheck.Next())
                        {
                            string assist_desc = "";
                            string sqlChkass = "select assisttype_desc from assucfassisttype where assisttype_code = {0} and coop_id = {1}";
                            sqlChkass = WebUtil.SQLFormat(sqlChkass, ls_asspausetype, state.SsCoopId);
                            Sdt chkass = WebUtil.QuerySdt(sqlChkass);

                            if (chkass.Next())
                            {
                                assist_desc = chkass.GetString("assisttype_desc");
                            }

                            LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ถูกงดสวัสดิการประเภทนี้ เนื่องจากเคยได้รับสวัสดิการประเภท " + ls_asspausetype + "-" + assist_desc + " แล้ว");
                            return false;
                        }
                    
                }

            //ตรวจสอบประกัน

                sqlStr = "select assisttype_ins from assucfassisttypeins where assisttype_code = '" + as_asscode + "' and coop_id = '"+ state.SsCoopId +"'";
                dtd = WebUtil.QuerySdt(sqlStr);

                while (dtd.Next())
                {
                        ls_assinstype = dtd.GetString("assisttype_ins");

                        string sqlcheck = @" select insurance_id from insinsuremaster where  insuretype_code = '" + ls_assinstype + "' and member_no = '" + as_memno + "' and insurance_status = 1 and coop_id = '" + state.SsCoopId + "'";
                        Sdt dtcheck = WebUtil.QuerySdt(sqlcheck);
                        if (dtcheck.Next())
                        {
                            string ins_desc = "";
                            string sqlChkins = "select insuretype_desc from insinsuretype where insuretype_code = {0} and coop_id = {1}";
                            sqlChkins = WebUtil.SQLFormat(sqlChkins, ls_assinstype, state.SsCoopId);
                            Sdt chkins = WebUtil.QuerySdt(sqlChkins);

                            if (chkins.Next())
                            {
                                ins_desc = chkins.GetString("insuretype_desc");
                            }

                            LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ถูกงดสวัสดิการประเภทนี้ เนื่องจากได้ทำประกันประเภท " + ls_assinstype + "-" + ins_desc + " ไว้แล้ว");
                            return false;
                        }
                }

            return true;
        }

        private void of_chkassistrcvidcardpause(string as_rcvcardid, string as_asscode)
        {
            string ls_asspausetype = "";
            sqlStr = "select assisttype_pause from assucfassisttypepause where assisttype_code = '" + as_asscode + "' and coop_id = '" + state.SsCoopId + "'";
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_asspausetype = dtd.GetString("assisttype_pause");

               Boolean result = of_chkassistrcvidcard(as_rcvcardid, ls_asspausetype);
               if (result == false)
               {
                   string ls_assgrp = hdassgrp.Value;

                   if (ls_assgrp == "01")
                   {
                       dsEducation.DATA[0].ASS_RCVCARDID = "";
                   }
                   else if (ls_assgrp == "03")
                   {
                       dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                   }
                   //LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกคนนี้ถูกงดสวัสดิการประเภทนี้ เนื่องจากเคยได้รับสวัสดิการประเภท " + ls_asspausetype + " แล้ว");
                   LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกครอบครัวคนนี้ ได้เคยขอสวัสดิการประเภท " + ls_asspausetype + " ไปแล้ว");

               }


            }   
        
        }



        private Boolean of_chkassistrcvidcard(string as_rcvcardid, string as_asscode)
        {
            string ls_sqlext = "", ls_sqlextrq = "", ls_memno = "", ls_assgrp = "";
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1;
            Int32 li_cntass = 0;

            // config สำหรับการตรวจสอบสวัสดิการ
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                ls_assgrp = dtd.GetString("assisttype_group");
                li_numreq = dtd.GetInt32("limrcvreq_num");
                li_numper = dtd.GetInt32("limrcvper_num");
                li_perunit = dtd.GetInt32("limrcvper_unit");
            }

            // ระยะเวลาสำหรับการหา
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");
            ls_sqlextrq = this.of_getsqlrankreq(li_perunit, li_numper, "req");

            // ตรวจสอบจากบัตรประชาชนที่ขอ
            sqlStr = @"select member_no from asscontmaster ass 
                       where ass.ass_rcvcardid = '" + as_rcvcardid + "' and ass.assisttype_code = '" + as_asscode + @"'
                       and ass.asscont_status <> -9 " + ls_sqlext + @"
                       union
                       select member_no from assreqmaster req 
                       where req.ass_rcvcardid = '" + as_rcvcardid + "' and req.assisttype_code='" + as_asscode + @"'
                       and req.req_status = 8 " + ls_sqlextrq;


            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_memno = dtd.GetString("member_no");

                li_cntass++;

                if (li_cntass >= li_numreq)
                {
                    if (ls_assgrp == "01")
                    {
                        dsEducation.DATA[0].ASS_RCVCARDID = "";
                    }
                    else if (ls_assgrp == "03")
                    {
                        dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                    }
                    LtServerMessage.Text = WebUtil.ErrorMessage("สมาชิกครอบครัวคนนี้ ได้เคยขอสวัสดิการประเภทนี้ไปแล้ว สมาชิกที่ขอ (" + ls_memno + ") ยังไม่ถึงเวลาที่จะขอสวัสดิการได้อีก");
                    return false;
                }
            }

            return true;
        }

        private Boolean of_chkassistparentidcard(string as_prcardid, string as_asscode)
        {
            string ls_sqlext = "", ls_sqlextrq = "", ls_memno = "", ls_memlist = "", ls_rcvtype = "", ls_sqlunion = "";
            Int32 li_numreq = 0, li_numper = 0, li_perunit = 1;
            Int32 li_cntass = 0, li_family = 0;

            // config สำหรับการตรวจสอบสวัสดิการ
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, as_asscode);
            Sdt dtd = WebUtil.QuerySdt(sqlStr);

            if (dtd.Next())
            {
                li_family = dtd.GetInt32("family_flag");

                li_numreq = dtd.GetInt32("limitreq_num");
                li_numper = dtd.GetInt32("limitper_num");
                li_perunit = dtd.GetInt32("limitper_unit");
            }

            if (li_family != 1)
            {
                return true;
            }

            // ระยะเวลาสำหรับการหา
            ls_sqlext = this.of_getsqlrank(li_perunit, li_numper, "ass");
            ls_sqlextrq = this.of_getsqlrankreq(li_perunit, li_numper, "req");

            // ตรวจสอบจากบัตรประชาชนที่ขอ
            sqlStr = @" select 'CO' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where ass.ass_prcardid = '" + as_prcardid + "' and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext + @"
                        union
                        select 'OWN' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where ass.ass_rcvcardid = '" + as_prcardid + "' and ass.assisttype_code='" + as_asscode + @"' and ass.asscont_status <> -9 " + ls_sqlext;

            ls_sqlunion = @" select 'CO' as rcvtype, req.member_no 
                        from assreqmaster req
                        where req.ass_prcardid = '" + as_prcardid + "' and req.assisttype_code='" + as_asscode + @"' and req.req_status = 8 " + ls_sqlextrq + @"
                        union
                        select 'OWN' as rcvtype, ass.member_no 
                        from asscontmaster ass
                        where req.ass_rcvcardid = '" + as_prcardid + "' and req.assisttype_code='" + as_asscode + @"' and req.req_status = 8 " + ls_sqlextrq;

            dtd = WebUtil.QuerySdt(sqlStr);

            while (dtd.Next())
            {
                ls_rcvtype = dtd.GetString("rcvtype");
                ls_memno = dtd.GetString("member_no");

                if (ls_rcvtype == "OWN")
                {
                    ls_memlist = ls_memlist + ls_memno + "(ขอเอง) ";
                }
                else
                {
                    ls_memlist = ls_memlist + ls_memno + "(ผู้ปกครอง) ";
                }

                li_cntass++;

                if (li_cntass >= li_numreq)
                {
                    LtServerMessage.Text = WebUtil.ErrorMessage("ผู้ปกครองคนนี้ ได้เคยขอสวัสดิการประเภทนี้ไปแล้ว สมาชิกที่ขอ (" + ls_memlist + ") ยังไม่ถึงเวลาที่จะขอสวัสดิการได้อีก");
                }
            }

            return true;
        }

        private string of_getsqlrank(Int32 ai_perunit, Int32 ai_numper, string as_table)
        {
            string ls_lastdate, ls_sqlext = "";
            Int32 li_assyear = 0;
            DateTime ldtm_caldate;

            li_assyear = Convert.ToInt32(dsMain.DATA[0].ASSIST_YEAR);
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            if (ai_perunit == 1)
            {
                li_assyear = li_assyear - ai_numper;
                ls_sqlext = " and " + as_table + ".assist_year > " + Convert.ToString(li_assyear);
            }
            else if (ai_perunit == 2)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMM");
                ls_sqlext = " and to_char(" + as_table + ".approve_date,'yyyymm') > '" + ls_lastdate + "'";
            }
            else if (ai_perunit == 3)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMMdd");
                ls_sqlext = " and " + as_table + ".approve_date > to_date('" + ls_lastdate + "','yyyymmdd')";
            }

            return ls_sqlext;
        }

        private string of_getsqlrankreq(Int32 ai_perunit, Int32 ai_numper, string as_table)
        {
            string ls_lastdate, ls_sqlext = "";
            Int32 li_assyear = 0;
            DateTime ldtm_caldate;

            li_assyear = Convert.ToInt32(dsMain.DATA[0].ASSIST_YEAR);
            ldtm_caldate = dsMain.DATA[0].CALAGE_DATE;

            // 1=นับปี 2=นับเดือน(ชนเดือน) 3=นับเดือน(ชนวัน)
            if (ai_perunit == 1)
            {
                li_assyear = li_assyear - ai_numper;
                ls_sqlext = " and " + as_table + ".assist_year > " + Convert.ToString(li_assyear);
            }
            else if (ai_perunit == 2)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMM");
                ls_sqlext = " and to_char(" + as_table + ".calage_date,'yyyymm') > '" + ls_lastdate + "'";
            }
            else if (ai_perunit == 3)
            {
                ls_lastdate = ldtm_caldate.AddMonths(ai_numper * -1).ToString("yyyyMMdd");
                ls_sqlext = " and " + as_table + ".calage_date > to_date('" + ls_lastdate + "','yyyymmdd')";
            }

            return ls_sqlext;
        }

        private void of_setpermiss(string as_memno, string as_asstype)
        {
            string ls_assgrp = "", ls_sql = "", ls_dateflag = "";
            string ls_paycode = "", ls_paychoose = "", ls_asscond = "0";
            decimal ldc_rank = 0, ldc_maxamt = 0, ldc_permiss = 0, ldc_everamt = 0, ldc_cutamt = 0, ldc_netamt = 0;
            Sdt dtchk, dtchs;

            ls_assgrp = hdassgrp.Value;
            ls_asscond = hdasscondition.Value;
            ls_dateflag = hdDateflag.Value;  //0 = วันสมัครสมาชิก , 1 = วันโอนย้าย

            //เช็คเงื่อนไข กรณีที่ไม่มีวันโอนย้าย จะคิดจากวันที่เป็นสมาชิก
            if (ls_dateflag == "1" && dsMain.DATA[0].age_tranmth == 0)
            {
                ls_dateflag = "0";
            }

            // เงื่อนไขประกอบ
            if (ls_asscond == "2")  // อายุ
            {
                ldc_rank = dsMain.DATA[0].age_birthmth;
            }
            else if (ls_asscond == "3") // อายุสมาชิก
            {
                if (ls_dateflag == "0")
                {
                    ldc_rank = dsMain.DATA[0].age_membmth;
                }
                else
                {
                    ldc_rank = dsMain.DATA[0].age_tranmth;
                }
            }
            else if (ls_asscond == "4") // เงินเดือน
            {
                ldc_rank = dsMain.DATA[0].salary_amount;
            }

            // ดูเงื่อนไขจากกลุ่มสวัสดิการอีกรอบ
            if (ls_assgrp == "01")  // ทุนการศึกษา
            {
                ls_paycode = dsEducation.DATA[0].ASSISTPAY_CODE;

                if (ls_asscond == "1")
                {
                    ldc_rank = dsEducation.DATA[0].EDU_GPA;
                }
            }
            else if (ls_assgrp == "02") // ตาย
            {
                ls_paycode = dsDecease.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "03") // ครอบครัวสมาชิก
            {
                ls_paycode = dsFamilydecease.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "04") // ประสบภัย
            {
                ls_paycode = dsDisaster.DATA[0].ASSISTPAY_CODE;
                if (ls_asscond == "5")
                {
                    ldc_rank = dsDisaster.DATA[0].DIS_DISAMT;
                }
                
            }
            else if (ls_assgrp == "05") // เกื้อกูลสมาชิก
            {
                ls_paycode = dsPatronize.DATA[0].ASSISTPAY_CODE;
            }
            else if (ls_assgrp == "06") // รักษาพยาบาล
            {
                ls_paycode = dsMedical.DATA[0].ASSISTPAY_CODE;
                if (ls_asscond == "6")
                {
                    ldc_rank = dsMedical.DATA[0].MED_DAY;
                }

                if (ls_asscond == "6" && ldc_rank == 0)
                {
                    return;
                }
            }

            if (string.IsNullOrEmpty(ls_paycode))
            {
                ls_sql = " select count(assistpay_code) as cntpay from assucfassisttypepay where assisttype_code = '" + as_asstype + "' ";

                dtchk = WebUtil.QuerySdt(ls_sql);
                if (dtchk.Next())
                {
                    if (dtchk.GetInt32("cntpay") == 1)
                    {
                        ls_sql = " select assistpay_code from assucfassisttypepay where assisttype_code = '" + as_asstype + "' ";

                        dtchs = WebUtil.QuerySdt(ls_sql);
                        if (dtchs.Next())
                        {
                            ls_paychoose = dtchs.GetString("assistpay_code");
                        }
                    }
                }

                if (string.IsNullOrEmpty(ls_paychoose))
                {
                    return;
                }

                ls_paycode = ls_paychoose;

                if (ls_assgrp == "01")  // ทุนการศึกษา
                {
                    dsEducation.DATA[0].ASSISTPAY_CODE = ls_paycode;
                    if (ls_asscond == "1" && ldc_rank == 0)
                    {
                        return;
                    }
                }
                else if (ls_assgrp == "02") // ตาย
                {
                    dsDecease.DATA[0].ASSISTPAY_CODE = ls_paycode;
                }
                else if (ls_assgrp == "03") // ครอบครัวสมาชิก
                {
                    dsFamilydecease.DATA[0].ASSISTPAY_CODE = ls_paycode;
                }
                else if (ls_assgrp == "04") // ประสบภัย
                {
                    dsDisaster.DATA[0].ASSISTPAY_CODE = ls_paycode;
                    if (ls_asscond == "5" && ldc_rank == 0)
                    {
                        return;
                    }
                }
                else if (ls_assgrp == "05") // เกื้อกูลสมาชิก
                {
                    dsPatronize.DATA[0].ASSISTPAY_CODE = ls_paycode;
                }
                else if (ls_assgrp == "06") // รักษาพยาบาล
                {
                    dsMedical.DATA[0].ASSISTPAY_CODE = ls_paycode;
                    if (ls_asscond == "6" && ldc_rank == 0)
                    {
                        return;
                    }
                }
 
            }



            ldc_permiss = this.of_getpermissmax(as_memno, as_asstype, ls_paycode, ldc_rank, ref ldc_maxamt);
            ldc_everamt = this.of_getpermissever(as_memno, as_asstype);
            ldc_cutamt = this.of_getpermisscut(as_memno, as_asstype);

            //ประเภทอุทกภัยจ่ายตามความเสียหาย
            if (ls_asscond == "5")
            {
                ldc_permiss = ldc_rank;
            }

            dsAmount.DATA[0].ASSISTMAX_AMT = ldc_maxamt;
            dsAmount.DATA[0].ASSIST_AMT = ldc_permiss;
            dsAmount.DATA[0].ASSISTEVER_AMT = ldc_everamt;
            dsAmount.DATA[0].ASSISTCUT_AMT = ldc_cutamt;

            if (ldc_permiss + ldc_everamt > ldc_maxamt)
            {
                ldc_netamt = ldc_maxamt - ldc_everamt;
            }
            else
            {
                ldc_netamt = ldc_permiss;
            }

            ldc_netamt = ldc_netamt - ldc_cutamt - ldc_everamt;

            dsAmount.DATA[0].ASSISTNET_AMT = ldc_netamt;

            if (ls_assgrp == "04" && ls_asscond != "5")
            {
                if (dsDisaster.DATA[0].DIS_DISAMT < dsAmount.DATA[0].ASSIST_AMT && dsDisaster.DATA[0].DIS_DISAMT > 0 )
                {
                    dsAmount.DATA[0].ASSISTNET_AMT = dsDisaster.DATA[0].DIS_DISAMT;
                }
            }
        }

        private Boolean of_haveoldreq(string as_memno, string as_assistcode, decimal ai_asststyear ,ref string as_reqno)
        {
            //ตรวจสอบว่าเคยขอสวัสดิการนี้ไปละยัง
            sqlStr = @"select * from assreqmaster 
                    where req_status = 8 and coop_id = {0} and member_no = {1} and assisttype_code = {2} and assist_year = {3}";
            sqlStr = WebUtil.SQLFormat(sqlStr, state.SsCoopControl, as_memno, as_assistcode, ai_asststyear);
            Sdt dt = WebUtil.QuerySdt(sqlStr);
            if (dt.Next())
            {
                as_reqno = dt.GetString("assist_docno");
                LtServerMessage.Text = WebUtil.WarningMessage(as_memno + " มีใบคำขอสวัสดิการประเภทนี้อยู่ ระบบได้ทำการเปิดใบคำขอให้");
                return true;
            }

            return false;
        }


        private void of_retrieve(string as_memno, string as_asstype, string as_reqno)
        {
            string ls_assgrp = "", ls_birthyear = "", ls_birthmonth = "", ls_membage = "", ls_birthage = "", ls_membyear = "", ls_membmonth = "", ls_minpaytype = "", ls_mingaincode = "";
            string ls_housestatus = "", ls_edulevel = "";
            dsMain.Retrieve(as_reqno);

            //แปลงวันที่เป็นข้อความ

            ls_membage = Convert.ToString(dsMain.DATA[0].member_age);
            ls_birthage = Convert.ToString(dsMain.DATA[0].birth_age);
            //string[] ls_age = ls_birthage.Split('.');
            //ls_birthyear = ls_age[0] + " ปี ";
            //ls_birthmonth = ls_age[1] + " เดือน";
            //dsMain.DATA[0].birthdate_th = ls_birthyear + ls_birthmonth;

            //string[] ls_memage = ls_membage.Split('.');
            //ls_membyear = ls_memage[0] + " ปี ";
            //ls_membmonth = ls_memage[1] + " เดือน";
            //dsMain.DATA[0].membdate_th = ls_membyear + ls_membmonth;
            ////////
            dsAmount.Retrieve(as_reqno);

            dsMain.AssistType();
            dsMain.GetAssYear();
            dsAmount.RetrieveMoneyType();
            dsAmount.RetrieveBank();
            //dsAmount.RetrieveDeptaccount(as_memno);
            dsAmount.DddeptaccountnoDlg(as_memno, dsAmount.DATA[0].DEPTACCOUNT_NO);

            ls_assgrp = hdassgrp.Value;

            if (ls_assgrp == "01")
            {
                dsEducation.Retrieve(as_reqno);
                this.of_getchildage();
                dsEducation.DdAsspaytype(as_asstype, ref ls_minpaytype);
                dsEducation.DdEducatLevel(ref ls_edulevel);
            }
            else if (ls_assgrp == "02")
            {
                dsDecease.Retrieve(as_reqno);
                dsDecease.DdAsspaytype(as_asstype, ref ls_minpaytype);
                dsGain.RetrieveGainAss(as_reqno);
                dsGain.DdGainRelation(ref ls_mingaincode);
            }
            else if (ls_assgrp == "03")
            {
                dsFamilydecease.Retrieve(as_reqno);
                dsFamilydecease.DdAsspaytype(as_asstype, ref ls_minpaytype);
            }
            else if (ls_assgrp == "04")
            {
                dsDisaster.Retrieve(as_reqno);
                ls_housestatus = dsDisaster.DATA[0].DIS_HOUSE_STATUS;
                dsDisaster.DdAsspaytype(as_asstype, ls_housestatus, ref ls_minpaytype);
                //dsDisaster.Disaster();
            }
            else if (ls_assgrp == "05")
            {
                dsPatronize.Retrieve(as_reqno);
                dsPatronize.DdAsspaytype(as_asstype, ref ls_minpaytype);
            }
            else if (ls_assgrp == "06")
            {
                dsMedical.Retrieve(as_reqno);
                dsMedical.DdAsspaytype(as_asstype, ref ls_minpaytype);
            }
        }


        private void of_postrcvidcard()
        {
            string ls_cardid = "", ls_asscode = "", ls_assgrp = "";

            ls_assgrp = hdassgrp.Value;

            if (ls_assgrp == "01")
            {
                ls_cardid = dsEducation.DATA[0].ASS_RCVCARDID;
            }
            else if (ls_assgrp == "03")
            {
                ls_cardid = dsFamilydecease.DATA[0].ASS_RCVCARDID;
            }

            ls_asscode = dsMain.DATA[0].ASSISTTYPE_CODE;
            this.of_Checkcardperson();

            this.of_chkassistrcvidcardpause(ls_cardid, ls_asscode);
 
            if (this.of_chkassistrcvidcard(ls_cardid, ls_asscode))
            {
                return;
            }
        }

        private void of_postparentidcard()
        {
            string ls_cardid = "", ls_asscode = "";

            ls_cardid = dsEducation.DATA[0].ASS_PRCARDID;
            ls_asscode = dsMain.DATA[0].ASSISTTYPE_CODE;

            if (this.of_chkassistparentidcard(ls_cardid, ls_asscode))
            {
                return;
            }
        }

        private void of_Checkcardperson()
        {
            string ls_assgrp = hdassgrp.Value;
            string PID = "";
            if (ls_assgrp == "01")
            {
                PID = dsEducation.DATA[0].ASS_RCVCARDID;
            }
            else if (ls_assgrp == "03")
            {
                PID = dsFamilydecease.DATA[0].ASS_RCVCARDID;
            }

            if (PID.Length != 13)
            {
                dsEducation.DATA[0].ASS_RCVCARDID = "";
                dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ครบ 13 หลัก");
            }
            else
            {
                Int32 pidchk = 0;
                Int32 dig = 0;
                Int32 fdig = 0;
                String lasttext = "";
                try
                {
                    pidchk = (Convert.ToInt32(PID.Substring(0, 1)) * 13) + (Convert.ToInt32(PID.Substring(1, 1)) * 12) + (Convert.ToInt32(PID.Substring(2, 1)) * 11) + (Convert.ToInt32(PID.Substring(3, 1)) * 10) + (Convert.ToInt32(PID.Substring(4, 1)) * 9) + (Convert.ToInt32(PID.Substring(5, 1)) * 8) + (Convert.ToInt32(PID.Substring(6, 1)) * 7) + (Convert.ToInt32(PID.Substring(7, 1)) * 6) + (Convert.ToInt32(PID.Substring(8, 1)) * 5) + (Convert.ToInt32(PID.Substring(9, 1)) * 4) + (Convert.ToInt32(PID.Substring(10, 1)) * 3) + (Convert.ToInt32(PID.Substring(11, 1)) * 2);
                    dig = pidchk % 11;
                    fdig = 11 - dig;
                    lasttext = fdig.ToString();
                    if (PID.Substring(12, 1) == WebUtil.Right(lasttext, 1))
                    {
                        string checkcard = "";
                        //checkcard = CheckCardPerson(PID, dsMain.DATA[0].MEMBCAT_CODE);
                        if (checkcard != "")
                        {
                            dsEducation.DATA[0].ASS_RCVCARDID = "";
                            dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                            //this.SetOnLoadedScript("alert('" + checkcard + "')");
                            LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง" + checkcard);
                        }
                    }
                    else
                    {
                        //  dsMain.DATA[0].IsCARD_PERSONNull();
                        dsEducation.DATA[0].ASS_RCVCARDID = "";
                        dsFamilydecease.DATA[0].ASS_RCVCARDID = "";
                        LtServerMessage.Text = WebUtil.ErrorMessage("เลขบัตรประชาชนไม่ถูกต้อง");
                    }
                }
                catch (Exception ex) { LtServerMessage.Text = WebUtil.ErrorMessage(ex); }
                //SetAppltype(dsMain.DATA[0].MEMBCAT_CODE, dsMain.DATA[0].CARD_PERSON);
            }
        }
        private void of_postreqold()
        {

            string ls_memno = "", ls_asstype = "", ls_reqno = "";
            decimal li_year = 0;

            ls_memno = dsMain.DATA[0].MEMBER_NO;
            ls_asstype = dsMain.DATA[0].ASSISTTYPE_CODE;
            li_year = dsMain.DATA[0].ASSIST_YEAR;
            ls_reqno = dsMain.DATA[0].ASSIST_DOCNO;

            this.of_settap();
            //set hdassgrp ตาม ls_asstype 
            this.of_setasstype(ls_asstype);
            this.of_retrieve(ls_memno, ls_asstype, ls_reqno);
            
            return;

        }

        public void of_setasstype(string ls_asstype)
        {
            string ls_assgrp = "";
            sqlStr = @"select * from ASSUCFASSISTTYPE where assisttype_code = {0}";
            sqlStr = WebUtil.SQLFormat(sqlStr, ls_asstype);
            Sdt dt = WebUtil.QuerySdt(sqlStr);

            if (dt.Next())
            {
                ls_assgrp = dt.GetString("assisttype_group");
                hdassgrp.Value = ls_assgrp;
            }
        }
    }
}