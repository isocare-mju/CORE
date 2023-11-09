<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dep_approve.aspx.cs" Inherits="Saving.Applications.ap_deposit.ws_dep_approve_ctrl.ws_dep_approve" %>

<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        var dsList = new DataSourceTool;
        var dsMain = new DataSourceTool;

        function Validate() {
            return confirm("ยืนยันการบันทึกข้อมูล");
        }

        function OnDsMainItemChanged(s, r, c, v) {
            if (c == "apv_status") {
                PostApvStatus();
            } 
        }
        
        function SheetLoadComplete() {


        }
        function OnClickNewRow() {         
            var moneytype_code = dsMain.GetItem(0, "moneytype_code");
            if (moneytype_code != null) {
                PostInsertRow();
            } else {
                alert("กรุณาเลือกประเภทเงินก่อนเพิ่มแถว");
                return false;
            }  
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <uc2:DsList ID="dsList" runat="server"/>
</asp:Content>
