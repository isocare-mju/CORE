<%@ Page Title="" Language="C#" MasterPageFile="~/Report.Master" AutoEventWireup="true" CodeBehind="u_cri_coopid_depttype_rcondition.aspx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_depttype_rcondition.u_cri_coopid_depttype_rcondition" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool;

        function SheetLoadComplete() {
            var condition = dsMain.GetItem(0, "condition");  
            if (condition == 1) {
                dsMain.GetElement(0, "end_amt").disabled = false;
            } else {
                dsMain.GetElement(0, "end_amt").disabled = true;
            }
        }
        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "condition") {
                if (v == 1) {
                    dsMain.GetElement(0, "end_amt").disabled = false;
                } else {
                    dsMain.SetItem(0, "end_amt",0);
                    dsMain.GetElement(0, "end_amt").disabled = true;
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