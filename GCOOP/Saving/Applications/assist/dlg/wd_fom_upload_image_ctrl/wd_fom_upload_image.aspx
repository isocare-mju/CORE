<%@ Page Title="" Language="C#" MasterPageFile="~/FrameDialog.Master" AutoEventWireup="true"
    CodeBehind="wd_fom_upload_image.aspx.cs" Inherits="Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl.wd_fom_upload_image" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsImg.ascx" TagName="DsImg" TagPrefix="uc2" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc3" %>
<%@ Register Src="DsList_Temp.ascx" TagName="DsList_Temp" TagPrefix="uc4" %>
<%@ Register Src="DsMbShr.ascx" TagName="DsMbShr" TagPrefix="uc5" %>
<%@ Register Src="DsDep.ascx" TagName="DsDep" TagPrefix="uc6" %>
<%@ Register Src="DsHR.ascx" TagName="DsHR" TagPrefix="uc7" %>
<%@ Register Src="DsRdc.ascx" TagName="DsRdc" TagPrefix="uc8" %>
<%@ Register Src="DsDate.ascx" TagName="DsDate" TagPrefix="uc9" %>
<%@ Register Src="DsList_Log.ascx" TagName="DsList_Log" TagPrefix="uc10" %>
<%@ Register Src="DsAss.ascx" TagName="DsAss" TagPrefix="uc11" %>
<%@ Register Src="DsElc.ascx" TagName="DsElc" TagPrefix="uc12" %>
<%@ Register Src="DsLoan.ascx" TagName="DsLoan" TagPrefix="uc13" %>
<%@ Register Src="DsInsurance.ascx" TagName="DsInsurance" TagPrefix="uc14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../../JsCss/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsImg = new DataSourceTool();
        var dsList = new DataSourceTool();
        var dsList_Temp = new DataSourceTool();
        var dsMbShr = new DataSourceTool();
        var dsDep = new DataSourceTool();
        var dsHR = new DataSourceTool();
        var dsRdc = new DataSourceTool();
        var dsDate = new DataSourceTool();
        var dsList_Log = new DataSourceTool();
        var dsAss = new DataSourceTool();
        var dsElc = new DataSourceTool();
        var dsLoan = new DataSourceTool();
        var dsInsurance = new DataSourceTool();

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "application") {
                PostApplication();
            } else if (c == "img_type_code") {
                PostIMGtypeCode();
            }
        }
        function OnDsList_LogClicked(s, r, c, v) {
            if (c == "b_show") {
                document.getElementById("ctl00_ContentPlace_HdRowList").value = r;
                PostShowIMG_Log();
            }
        }
        function OnDsDateClicked(s, r, c, v) {
            if (c == "b_search") {
                PostSearch();
            }
        }
        function OnDsListClicked(s, r, c, v) {
            if (c == "b_del") {
                if (confirm('ต้องการ ลบรูปภาพหรือไม่ ?')) {
                    document.getElementById("ctl00_ContentPlace_HdRowList").value = r;
                    PostDelIMG();
                }
            } else if (c == "b_show") {
                document.getElementById("ctl00_ContentPlace_HdRowList").value = r;
                PostShowIMG();
            }
        }
        function OnDsList_TempClicked(s, r, c, v) {
            if (c == "b_del") {
                if (confirm('ต้องการ ลบรูปภาพหรือไม่ ?')) {
                    document.getElementById("ctl00_ContentPlace_HdRowList").value = r;
                    PostDelIMG();
                }
            } else if (c == "b_show") {
                document.getElementById("ctl00_ContentPlace_HdRowList").value = r;
                PostShowIMG();
            }
        }
        function OnDsElcItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostElc();
            }
        }
        function OnDsMbShrItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostMemberNo();
            }
        }
        function OnDsDsHRItemChanged(s, r, c, v) {
            if (c == "member_no") {
                PostEmpNO();
            }
        }
        function OnDsDepItemChanged(s, r, c, v) {
            if (c == "deptaccount_no") {
                PostDeptaccountNo();
            }
        }
        $(document).ready(function () {
            var url = new URL(window.location.href);
            var c = url.searchParams.get("application");
            console.log(c);
            if (c != "") {
                document.getElementById("ctl00_ContentPlace_dsMain_FormView1_application").disabled = true;
                try {
                    if (c != elc) {
                        document.getElementById("ctl00_ContentPlace_dsMbShr_FormView1_member_no").disabled = true;
                        document.getElementById("ctl00_ContentPlace_dsMbShr_FormView1_full_name").disabled = true;
                    }
                }
                catch (err) {
                }
                try {
                    document.getElementById("ctl00_ContentPlace_dsDep_FormView1_deptaccount_no").disabled = true;
                    document.getElementById("ctl00_ContentPlace_dsDep_FormView1_deptaccount_name").disabled = true;
                }
                catch (err) {
                }
                try {
                    document.getElementById("ctl00_ContentPlace_dsDep_FormView1_deptaccount_no").disabled = true;
                    document.getElementById("ctl00_ContentPlace_dsDep_FormView1_deptaccount_name").disabled = true;
                }
                catch (err) {
                }
                try {
                    document.getElementById("ctl00_ContentPlace_dsHR_FormView1_member_no").disabled = true;
                    document.getElementById("ctl00_ContentPlace_dsHR_FormView1_full_name").disabled = true;
                }
                catch (err) {
                }
                try {
                    document.getElementById("ctl00_ContentPlace_dsRdc_FormView1_document_no").disabled = true;
                    document.getElementById("ctl00_ContentPlace_dsRdc_FormView1_document_title").disabled = true;
                }
                catch (err) {
                }
                try {
                    document.getElementById("ctl00_ContentPlace_dsInsurance_FormView1_insurance_no").disabled = true;
                }
                catch (err) {
                }
            }
            var s = url.searchParams.get("show");
            console.log(s);
            if (s == "0") {
                document.getElementById("ctl00_ContentPlace_dsList_Repeater1_ctl00_B_DEL").disabled = true;
                document.getElementById("ctl00_ContentPlace_dsList_Repeater1_ctl01_B_DEL").disabled = true;
                document.getElementById("ctl00_ContentPlace_dsList_Repeater1_ctl02_B_DEL").disabled = true;
                document.getElementById("ctl00_ContentPlace_dsList_Repeater1_ctl03_B_DEL").disabled = true;
                document.getElementById("ctl00_ContentPlace_dsList_Repeater1_ctl04_B_DEL").disabled = true;
            }
        });

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("ต้องการ เพิ่มรูปภาพหรือไม่ ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
        <table style="width: 800px;">
            <tr>
                <td style="width: 400px" align="center" valign="top">
                    <uc1:DsMain ID="dsMain" runat="server" />
                    <br />
                    <asp:Panel ID="P_show" Visible="true" runat="server">
                        <table class="DataSourceFormView" style="width: 400px">
                            <tr>
                                <td colspan="3">
                                    <div style="color: Black; font-size: 11pt;">
                                        <strong><u>กำหนดสำหรับอ้างถึง</u></strong></div>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="P_dsMbShr" Visible="false" runat="server">
                            <uc5:DsMbShr ID="dsMbShr" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="P_dsDep" Visible="false" runat="server">
                            <uc6:DsDep ID="dsDep" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="P_dsHR" Visible="false" runat="server">
                            <uc7:DsHR ID="dsHR" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="P_dsRdc" Visible="false" runat="server">
                            <uc8:DsRdc ID="dsRdc" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="P_dsAss" Visible="false" runat="server">
                            <uc11:DsAss ID="dsAss" runat="server" />
                        </asp:Panel>
                        <asp:Panel ID="P_dsElc" Visible="false" runat="server">
                            <uc12:DsElc ID="dsElc" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="P_dsLoan" Visible="false" runat="server">
                           <uc13:DsLoan ID="dsLoan" runat="server" />
                         </asp:Panel>
                         <asp:Panel ID="P_dsInsurance" Visible="false" runat="server">
                            <uc14:DsInsurance ID="dsInsurance" runat="server" />
                         </asp:Panel>
                        <asp:Panel ID="Panel1" Visible="true" runat="server">
                            <table class="DataSourceFormView" style="width: 400px">
                                <tr>
                                    <td width="35%" align="right">
                                        <div>
                                            <span>แฟ้มข้อมูล :</span>
                                        </div>
                                    </td>
                                    <td width="50%">
                                        <div>
                                            <asp:FileUpload ID="image_upload" runat="server" />
                                        </div>
                                    </td>
                                    <td width="15%">
                                        <asp:Button ID="B_upload" runat="server" Text="upload" Width="100%" OnClick="B_upload_Click"
                                            OnClientClick="Confirm()" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Panel ID="P_DsDate" Visible="false" runat="server">
                        <uc9:DsDate ID="dsDate" runat="server" />
                    </asp:Panel>
                </td>
                <td style="width: 300px;">
                    <center>
                        <asp:Literal ID="ltr_img" runat="server"></asp:Literal>
                        <uc2:DsImg ID="dsImg" runat="server" />
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <hr />
                    <br />
                    <asp:Panel ID="P_List" runat="server">
                        <uc3:DsList ID="dsList" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="P_List_Temp" runat="server">
                        <uc4:DsList_Temp ID="dsList_Temp" runat="server" />
                    </asp:Panel>
                    <asp:Panel ID="P_List_log" Visible="false" runat="server">
                        <uc10:DsList_Log ID="dsList_Log" runat="server" />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </center>
    <asp:HiddenField ID="HdToken" runat="server" />
    <asp:HiddenField ID="HdRowList" runat="server" />
    <asp:HiddenField ID="HdApp" runat="server" />
    <asp:HiddenField ID="HdColumnName" runat="server" />
    <asp:HiddenField ID="HdColumnData" runat="server" />
</asp:Content>
