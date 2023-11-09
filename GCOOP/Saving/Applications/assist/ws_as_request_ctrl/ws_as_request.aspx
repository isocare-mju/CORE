<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_as_request.aspx.cs" Inherits="Saving.Applications.assist.ws_as_request_ctrl.ws_as_request" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsEducation.ascx" TagName="DsEducation" TagPrefix="uc2" %>
<%@ Register Src="DsDecease.ascx" TagName="DsDecease" TagPrefix="uc3" %>
<%@ Register Src="DsFamilydecease.ascx" TagName="DsFamilydecease" TagPrefix="uc4" %>
<%@ Register Src="DsDisaster.ascx" TagName="DsDisaster" TagPrefix="uc5" %>
<%@ Register Src="DsPatronize.ascx" TagName="DsPatronize" TagPrefix="uc6" %>
<%@ Register Src="DsMedical.ascx" TagName="DsMedical" TagPrefix="uc7" %>
<%@ Register Src="DsGain.ascx" TagName="DsGain" TagPrefix="uc8" %>
<%@ Register Src="DsAmount.ascx" TagName="DsAmount" TagPrefix="uc9" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc10" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //ประกาศตัวแปร ควรอยู่บริเวณบนสุดใน tag <script>
        var dsMain = new DataSourceTool();
        var dsDecease = new DataSourceTool();
        var dsFamilydecease = new DataSourceTool();
        var dsEducation = new DataSourceTool();
        var dsDisaster = new DataSourceTool();
        var dsPatronize = new DataSourceTool();
        var dsGain = new DataSourceTool();
        var dsAmount = new DataSourceTool();
        var dsList = new DataSourceTool();

        //เช็คบัตรประชาชน//////////////////
        function checkID(id) {
            if (id.length != 13) return false;
            return true;
        }

        function checkForm() {
            if (!checkID(dsFamilydecease.GetItem(0, "card_person"))) {
                alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                dsFamilydecease.SetItem(0, "card_person", "");
                return;
            }
        }

        function MenubarOpen() {
            Gcoop.OpenIFrame2(750, 600, 'wd_as_search_request_old.aspx', '')
        }
        ////////////////////////////////

        //ประกาศฟังก์ชันสำหรับ event ItemChanged
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "member_no") {
                var ls_memberno = dsMain.GetItem(0, "member_no");
                Gcoop.GetEl("HdMbChkmemdate").value = v;

                dsMain.SetItem(0, "member_no", ls_memberno);
                PostMemberNo();
            }
            else if (c == "assisttype_code") {
                PostAssistType();       
            } else if (c == "assist_year") {
                PostAssistYear();
            } else if (c == "calage_date") {
                PostCalage();
            }
        }

        //ประกาศฟังก์ชันสำหรับ event Clicked
        function OnDsMainClicked(s, r, c) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(650, 600, 'wd_as_member_search.aspx', '')
            }
            /*else if (c == "b_add") {
                Gcoop.OpenIFrame2Extend(650, 600, 'wd_fom_upload_image.aspx', '')
            }*/
        }

        function OnDsListClicked(s, r, c) {
            var ls_assreqno = dsList.GetItem(r, "assistreq_docno");
            Gcoop.GetEl("hdAssreqno").value = ls_assreqno;
            InitHistory();
        }
        

        function OnDsAmountItemChanged(s, r, c, v) {
            if (c == "expense_bank") {
                var ls_expensebank = dsAmount.GetItem(0, "expense_bank");
                dsAmount.SetItem(0, "expense_bank", ls_expensebank);
                dsAmount.SetItem(0, "expense_branch", "");
                PostRetriveBankBranch();
            } else if (c == "moneytype_code") {
                var ls_montype = dsAmount.GetItem(0, "moneytype_code");
                if (ls_montype == "CSH") {
                    dsAmount.SetItem(0, "expense_bank", "");
                    dsAmount.SetItem(0, "expense_accid", "");
                    dsAmount.SetItem(0, "expense_branch", "");
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = true;
                    dsAmount.GetElement(0, "expense_accid").disabled = true;
                    dsAmount.GetElement(0, "send_system").disabled = true;
                    dsAmount.GetElement(0, "expense_branch").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "send_system").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                } else if (ls_montype == "TRN") {
                    dsAmount.SetItem(0, "expense_bank", "");
                    dsAmount.SetItem(0, "expense_accid", "");
                    dsAmount.SetItem(0, "expense_branch", "");
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = true;
                    dsAmount.GetElement(0, "expense_accid").disabled = true;
                    dsAmount.GetElement(0, "expense_branch").disabled = true;
                    dsAmount.GetElement(0, "send_system").disabled = false;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "send_system").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                } else if (ls_montype == "TGP") {
                    dsAmount.SetItem(0, "expense_bank", "");
                    dsAmount.SetItem(0, "expense_accid", "");
                    dsAmount.SetItem(0, "expense_branch", "");
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = true;
                    dsAmount.GetElement(0, "expense_accid").disabled = true;
                    dsAmount.GetElement(0, "expense_branch").disabled = true;
                    dsAmount.GetElement(0, "send_system").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "send_system").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                } else {
                    dsAmount.SetItem(0, "send_system", "");
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "expense_bank").disabled = false;
                    dsAmount.GetElement(0, "expense_accid").disabled = false;
                    dsAmount.GetElement(0, "expense_branch").disabled = false;
                    dsAmount.GetElement(0, "send_system").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "expense_bank").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "expense_accid").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "expense_branch").style.background = "#FFFFFF";
                    dsAmount.GetElement(0, "send_system").style.background = "#CCCCCC";
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }
            } else if (c == "send_system") {
                var ls_sendsys = dsAmount.GetItem(0, "send_system");
                if (ls_sendsys == "DEP") {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = false;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#FFFFFF";
                } else if (ls_sendsys == "LON") {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }
                else {
                    dsAmount.SetItem(0, "deptaccount_no", "");
                    dsAmount.GetElement(0, "deptaccount_no").disabled = true;
                    dsAmount.GetElement(0, "deptaccount_no").style.background = "#CCCCCC";
                }

            }
        }

        function GetMembNoFromDlg(memberno) {
            var memb_no = Gcoop.GetEl("Hd_membno").value;
                dsMain.SetItem(0, "member_no", memberno.trim());
                PostMemberNo();
        }

        function GetMembNoFromDlgReqOld(memberno, reqdoc_no, assisttype_code) {
            dsMain.SetItem(0, "member_no", memberno.trim());
            dsMain.SetItem(0, "assist_docno", reqdoc_no.trim());
            dsMain.SetItem(0, "assisttype_code", assisttype_code.trim());
                PostReqOld();
            }


        function Validate() {

            var err_text = "";

            if (Gcoop.GetEl("hdassgrp").value == "01") {
                var ass_rcvcardid = dsEducation.GetItem(0, "ass_rcvcardid");
                var ass_rcvname = dsEducation.GetItem(0, "ass_rcvname");
                if (ass_rcvcardid == "" || ass_rcvcardid == null) {
                    err_text += "กรุณากรอกเลขที่บัตรบุตร ";
                }

                if (ass_rcvname == "" || ass_rcvname == null) {
                    err_text += "กรุณากรอกชื่อสมาชิกครอบครัว";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "02") {
                var dec_deaddate = dsDecease.GetItem(0, "dec_deaddate");
                if (dec_deaddate == "" || dec_deaddate == null || dec_deaddate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่ถึงแก่กรรม";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "03") {
                var fam_docdate = dsFamilydecease.GetItem(0, "fam_docdate");
                var ass_rcvname = dsFamilydecease.GetItem(0, "ass_rcvname");
                var ass_rcvcardid = dsFamilydecease.GetItem(0, "ass_rcvcardid");
                if (fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่ตามเอกสารราชการ ";
                }

                if (ass_rcvname == "" || ass_rcvname == null) {
                    err_text += "กรุณากรอกชื่อสมาชิกครอบครัว ";
                }

//                if (ass_rcvcardid == "" || ass_rcvcardid == null) {
//                    err_text += "กรุณากรอกเลขที่บัตรประชาชน ";
//                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "04") {
                var dis_disdate = dsDisaster.GetItem(0, "dis_disdate");
                if (dis_disdate == "" || dis_disdate == null || dis_disdate == "1500-01-01 00:00:00" ) {
                    err_text += "กรุณากรอกวันที่เริ่มประสบภัย ";
                }
            }
            else if (Gcoop.GetEl("hdassgrp").value == "05") {
                var fam_docdate = dsPatronize.GetItem(0, "fam_docdate");
                var coop_id = dsMain.GetItem(0, "coop_id");
                var assisttype_code = dsMain.GetItem(0, "assisttype_code");

                if (Gcoop.GetEl("HdDocdate_num").value > "0") {
                    if (fam_docdate == "" || fam_docdate == null || fam_docdate == "1500-01-01 00:00:00") {
                        err_text += "กรุณากรอกวันที่ตามเอกสาร ";
                    }
                }

            }
            else if (Gcoop.GetEl("hdassgrp").value == "06") {
            }




            ////////////////////////////////////////////
            var a = dsAmount.GetItem(0, "assist_amt");
            if (a <= 0)
            {
                err_text += "ไม่พบยอดเงินตามสิทธิ์ กรุณาตรวจสอบ";
            }
            /////////////////////////////////////////////
            var moneytype_code = dsAmount.GetItem(0, "moneytype_code");
            var send_system = dsAmount.GetItem(0, "send_system");
            var deptaccount_no = dsAmount.GetItem(0, "deptaccount_no");
            if (moneytype_code == "TRN" && send_system == "DEP" && ( deptaccount_no == "00" || deptaccount_no == null ) )
            {
                err_text += "กรุณาเลือกเลขบัญชีเงินฝากที่จะรับโอนเงิน";
            }


            if (err_text != "") {
                confirm(err_text);
                return false;
            }
            else {
                return confirm("ยืนยันการบันทึกข้อมูล");
            }

            
        }

        function OnDsEducationItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "edu_gpa") {
                if (v > 4 || v < 0 ) {
                    alert("ตรวจสอบเกรดเฉลี่ย");
                    dsEducation.SetItem(0, "edu_gpa", 0);
                    return;
                }

                if (Gcoop.GetEl("hdasscondition").value == "1") {
                    PostInitPermiss();
                }
            } else if (c == "edu_childbirthdate") {
                PostGetChildAge();
            } else if (c == "ass_rcvcardid") {

                var cardperson = dsMain.GetItem(0, "card_person");
                if (!checkID(v)) {
                    alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                    dsEducation.SetItem(0, "ass_rcvcardid", "");
                    return;
                }

                if (cardperson == v) {
                    alert("รหัสบัตรประชาชนตรงกับผู้ขอรับทุน กรุณาตรวจสอบ");
                    dsEducation.SetItem(0, "ass_rcvcardid", "");
                    return;
                }
                PostCardPerson();

            } else if (c == "ass_prcardid") {
                PostCardPersonParent();
            }
        }

        function OnDsDeceaseItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "dec_deaddate") {
                CheckDocdate();
            }
        }

        function OnDsFamilyItemChanged(s, r, c, v) {
            if (c == "ass_rcvcardid") {

                var cardperson = dsMain.GetItem(0, "card_person");
                if (!checkID(v)) {
                    alert("รหัสบัตรประชาชนไม่ถูกต้อง");
                    dsFamilydecease.SetItem(0, "fam_cardid", "");
                    return;
                }

                if (cardperson == v) {
                    alert("รหัสบัตรประชาชนตรงกับผู้ขอรับทุน กรุณาตรวจสอบ");
                    dsFamilydecease.SetItem(0, "ass_rcvcardid", "");
                    return;
                }
                PostCardPerson();
            } else if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "fam_docdate") {
                CheckDocdate();
            }
        }

        function OnDsDisasterClicked(s, r, c) {
            if (c == "b_linkaddress") {
                PostLinkAddress();
            }
        }

        function OnDsDisasterItemChanged(s, r, c) {

            if (c == "dis_disamt") {
                if (Gcoop.GetEl("hdasscondition").value == "5") {
                    PostInitPermiss();
                }
                else {
                    JsSetdisamt();
                }
            } else if (c == "assistpay_code") {
                PostAssistPay();
            }
            else if (c == "dis_disdate") {
                CheckDocdate();
            }
            else if (c == "dis_house_status") {
            JsPostDispay();
            }
        }

        function OnDsPatronizeItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            }
            else if (c == "fam_docdate") {
                CheckDocdate();
            }
        }

        function OnDsMedicalItemChanged(s, r, c, v) {
            if (c == "assistpay_code") {
                PostAssistPay();
            } else if (c == "med_startdate" || c == "med_enddate") {
                PostCalMedDay();
            }
        }

        function OnDsGainClicked(s, r, c, v) {
            if (c == "b_del") {
                dsGain.SetRowFocus(r);
                if (confirm("ลบข้อมูลผู้รับผลประโยชน์แถวที่ " + (r + 1) + " ?") == true) {
                    PostDelRow();
                }
            }
        }

        function PostInsertRow(s, r, c, v) {
            Gcoop.GetEl("Hd_row").value = r;
            PostInsertRow();
        }

        function SheetLoadComplete() {
            var stmflag = dsAmount.GetItem(0, "stm_flag");
            var assisttype_code = dsMain.GetItem(0, "assisttype_code");
            var coop_id = dsMain.GetItem(0, "coop_id");

            if (stmflag == 1) {
                dsAmount.GetElement(0, "");
            }
            for (var i = 0; i < dsList.GetRowCount(); i++) {
                if (i == r) {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFF99";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFF99";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFF99";
                }
                else {
                    dsList.GetElement(i, "assisttype_code").style.background = "#FFFFFF";
                    dsList.GetElement(i, "asscontract_no").style.background = "#FFFFFF";
                    dsList.GetElement(i, "approve_date").style.background = "#FFFFFF";
                }
            }
        }

        function OnDsAmountClicked(s, r, c, v) {
            if (c == "b_search") {
                Gcoop.OpenIFrame2(600, 500, "wd_as_search_deptaccount.aspx", "");
            }
        }

        function GetDeptNoFromDlg(deptno) {
            dsAmount.SetItem(0, "deptaccount_no", deptno);
            JsPostDlgdeptno();
        }


        Number.prototype.round = function (p) {
            p = p || 10;
            return parseFloat(this.toFixed(p));
        };

        $(function () {
            //alert($("#tabs").tabs()); //ชื่อฟิวส์

            var tabIndex = Gcoop.GetEl("hdTabIndex").value; // Gcoop.ParseInt($("#<%=hdTabIndex.ClientID%>").val());
            $("#tabs").tabs({
                active: tabIndex,
                activate: function (event, ui) {
                    $("#<%=hdTabIndex.ClientID%>").val(ui.newTab.index() + "");
                }
            });
            if (tabIndex == "0") {
                $('#tabs').tabs({ active: 0, disabled: [1, 2, 3, 4, 5, 6] });
            }
            else if (tabIndex == "1") {
                $('#tabs').tabs({ active: 1, disabled: [0, 2, 3, 4, 5, 6] });
            }
            else if (tabIndex == "2") {
                $('#tabs').tabs({ active: 2, disabled: [0,1, 3, 4, 5, 6] });
            }
            else if (tabIndex == "3") {
                $('#tabs').tabs({ active: 3, disabled: [0, 1,2, 4, 5, 6] });
            }
            else if (tabIndex == "4") {
                $('#tabs').tabs({ active: 4, disabled: [0, 1, 2,3,5, 6] });
            }
            else if (tabIndex == "5") {
                $('#tabs').tabs({ active: 5, disabled: [0, 1, 2, 3,4, 6] });
            }
            else if (tabIndex == "6") {
                $('#tabs').tabs({ active: 6, disabled: [0, 1, 2, 3, 4,5] });
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <div id="tabs">
        <ul style="font-size: 12px;">
            <li><a href="#tabs-1">ทุนการศึกษาบุตร</a></li>
            <li><a href="#tabs-2">สมาชิกถึงแก่กรรม</a></li>
            <li><a href="#tabs-3">ครอบครัวสมาชิก</a></li>
            <li><a href="#tabs-4">ประสบภัยพิบัติ</a></li>
            <li><a href="#tabs-5">เกื้อกูลสมาชิก</a></li>
            <li><a href="#tabs-6">รักษาพยาบาล</a></li>
        </ul>
        <div id="tabs-1">
            <uc2:DsEducation ID="dsEducation" runat="server" />
        </div>
        <div id="tabs-2">
            <uc3:DsDecease ID="dsDecease" runat="server" />
        </div>
        <div id="tabs-3">
            <uc4:DsFamilydecease ID="dsFamilydecease" runat="server" />
        </div>
        <div id="tabs-4">
            <uc5:DsDisaster ID="dsDisaster" runat="server" />
        </div>
        <div id="tabs-5">
            <uc6:DsPatronize ID="dsPatronize" runat="server" />
        </div>
        <div id="tabs-6">
            <uc7:DsMedical ID="dsMedical" runat="server" />
        </div>
    </div>
    <br />
    <div align="right" style="margin-right: 20px;">
        <span id="insertRow" style="display: none" class="NewRowLink" onclick="PostInsertRow()">
            เพิ่มแถว</span></div>
    <uc8:DsGain ID="dsGain" runat="server" />
    <uc9:DsAmount ID="dsAmount" runat="server" />
    <uc10:DsList ID="dsList" runat="server" />
    <asp:HiddenField ID="hdTabIndex" Value="0" runat="server" />
    <asp:HiddenField ID="hdSaveChk_GPA" runat="server" />
    <asp:HiddenField ID="hdInertRow" runat="server" />
    <asp:HiddenField ID="hdCalDay" runat="server" />
    <asp:HiddenField ID="hdActMemno" runat="server" />
    <asp:HiddenField ID="hdassgrp" runat="server" />
    <asp:HiddenField ID="hdasscondition" runat="server" />
    <asp:HiddenField ID="HdTokenIMG" runat="server" />
    <asp:HiddenField ID="Hd_membno" runat="server" />
    <asp:HiddenField ID="Hd_row" runat="server" />
    <asp:HiddenField ID="hdAssreqno" runat="server" />
    <asp:HiddenField ID="hdDateflag" runat="server" />
    <asp:HiddenField ID="HdMbChkmemdate" runat="server" />
    <asp:HiddenField ID="HdDocdate_num" runat="server" />
    <asp:HiddenField ID="HdChkChildAge" runat="server" />
</asp:Content>
