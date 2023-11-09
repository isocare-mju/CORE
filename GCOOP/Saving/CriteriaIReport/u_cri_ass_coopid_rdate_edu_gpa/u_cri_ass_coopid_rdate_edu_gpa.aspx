<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" 
CodeBehind="u_cri_ass_coopid_rdate_edu_gpa.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_ass_coopid_rdate_edu_gpa.u_cri_ass_coopid_rdate_edu_gpa" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    var dsMain = new DataSourceTool();

    function OnDsMainItemChanged(s, r, c, v) {
        if (c == "gpa_max") {
            if (v > 4.00) {
                alert("กรุณากรอกข้อมูลเกรด 0-4");
                dsMain.SetItem(0, "gpa_max", 4.00);
                return;
            }
        } else if (c == "gpa_min") {
            if (v > 4.00) {
                alert("กรุณากรอกข้อมูลเกรด 0-4");
                dsMain.SetItem(0, "gpa_min", 0.00);
                return;
            }
        }
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <center>
        <asp:Label ID="ReportName" runat="server" Text="ชื่อรายงาน" Enabled="False" EnableTheming="False"
            Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Size="Large"
            Font-Underline="False"></asp:Label></center>
    <uc1:DsMain ID="dsMain" runat="server" />
</asp:Content>
