<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dep_ucf_recppaytype.aspx.cs" Inherits="Saving.Applications.deposit.ws_dep_ucf_recppaytype_ctrl.ws_dep_ucf_recppaytype" %>

<%@ Register Src="DsList.ascx" TagName="DsList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function check_number() {
            e_k = event.keyCode//46 = .
            if (e_k != 13 && e_k != 46 && (e_k < 48) || (e_k > 57)) {
                event.returnValue = false;
                alert("กรุณากรอกเฉพาะตัวเลขเท่านั้น!");
            }
        }

        var dsList = new DataSourceTool();

        function OnDsListItemChanged(s, r, c, v) {
            if (c == "recppaytype_code") {
                for (var i = 0; i < dsList.GetRowCount(); i++) {
                    if (i != r) {
                        if (dsList.GetItem(i, "recppaytype_code") == v) {
                            dsList.SetItem(r, "recppaytype_code", "");
                            alert("รหัสรายการดังกล่าวมีการเพิ่มแล้ว"); return;
                        }
                    }
                }
            } else if (c == "moneytype_support") {
                dsList.SetItem(r, "default_accid", "");
                JsPostCashAccid();
            }
        }

        function OnDsListClicked(s, r, c) {
            if (c == "b_del") {
                dsList.SetRowFocus(r);               
                var currentRow = r;
                currentRow = parseInt(currentRow) + 1;
                var confirmText = "ยืนยันการลบแถวที่ " + currentRow;
                if (confirm(confirmText)) {
                    PostDelRow();
                }  
            }
        }
        function Validate() {
            var alertstr = "";
            var ck_row = 0;
            var row = dsList.GetRowCount();
            for (var n = 0; n < row; n++) {
                ck_row = n + 1;
                var recppaytype_code = dsList.GetItem(n, "recppaytype_code");
                if (recppaytype_code == null) {
                    alertstr += "- กรุณาเลือก ประเภทเงินแถวที่ "+ck_row+" \n";
                }
                var recppaytype_desc = dsList.GetItem(n, "recppaytype_desc");
                if (recppaytype_desc == null) {
                    alertstr += "- กรุณากรอก ชื่อประเภทการทำรายการ ของรหัส" + recppaytype_code + " \n";
                }
                var recppaytype_flag = dsList.GetItem(n, "recppaytype_flag");
                if (recppaytype_flag == null) {
                    alertstr += "- กรุณาเลือกฝั่งรายการ ของรหัส" + recppaytype_code + " \n";
                }
                var moneytype_support = dsList.GetItem(n, "moneytype_support");
                if (moneytype_support == null) {
                    alertstr += "- กรุณาเลือกประเภทเงิน ของรหัส" + recppaytype_code + " \n";
                }
                var group_itemtype = dsList.GetItem(n, "group_itemtpe");
                if (group_itemtype == "0") {
                    alertstr += "- กรุณาเลือกรหัสกลุ่ม ของรหัส" + recppaytype_code + " \n";                    
                }
                var active_flag = dsList.GetItem(n, "active_flag");
                if (active_flag == null) {
                    alertstr += "- กรุณาเลือกสถานะ ของรหัส" + recppaytype_code + " \n";
                }
                var order_sort = dsList.GetItem(n, "order_sort");
                if (order_sort == null) {
                    alertstr += "- กรุณากรอกลำดับ ของรหัส" + recppaytype_code + " \n";
                }
                var default_accid = dsList.GetItem(n, "default_accid");
                if (default_accid == null) {
                    alertstr += "- กรุณาเลือกคู่บัญชี ของรหัส" + recppaytype_code + " \n";
                }
                for (var i = 0; i < row; i++) {
                    if (i != n) {
                        if (dsList.GetItem(i, "recppaytype_code") == recppaytype_code) {
                            alert("รหัสรายการ " + recppaytype_code + " มีการเพิ่มซ้ำ"); return;
                        }
                    }
                }
            }
            if (alertstr == "") {
                return confirm("ยืนยันการบันทึกข้อมูล");
            } else {
                alert(alertstr);
                return false;
            }
        }

        function SheetLoadComplete() {
        }

        function OnClickNewRow() {
            PostNewRow();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <span class="NewRowLink" onclick="OnClickNewRow()">เพิ่มแถว</span>
    <uc1:DsList ID="dsList" runat="server" />
</asp:Content>
