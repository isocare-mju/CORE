<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="ws_fin_reprint.aspx.cs" Inherits="Saving.Applications.app_finance.ws_fin_reprint_ctrl.ws_fin_reprint" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="dsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var dsMain = new DataSourceTool();
        var dsList = new DataSourceTool();

        function OnDsMainClicked(s, r, c) {
            if (c == "b_retrieve") {
                PostRetrieve();
            }
        }
        function OnDsMainItemChanged(s, r, c,v) {
            if (c == "member_flag") {
                if (v == 0 || v == 1) {
                    dsMain.GetElement(0, "member_no").disabled = false;
                } else {
                    dsMain.SetItem(0, "member_no", '');
                    dsMain.GetElement(0, "member_no").disabled = true;
                }
            }
        }
        function OnClickPrint() {
            PostPrint();
        }
        function SheetLoadComplete() {
            var member_flag = dsMain.GetItem(0, "member_flag"); 
            if (member_flag == "0" || member_flag == "1") {
                dsMain.GetElement(0, "member_no").disabled = false;
            } else {
                dsMain.SetItem(0,"member_no",'');
                dsMain.GetElement(0, "member_no").disabled = true;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
<asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <br />
    <input type="button" value="พิมพ์" style="width:100px;" onclick="OnClickPrint()" />
    <uc2:DsList ID="dsList" runat="server" />
</asp:Content>
