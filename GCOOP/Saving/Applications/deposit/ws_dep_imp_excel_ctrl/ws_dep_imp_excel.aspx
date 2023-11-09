<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true"
    CodeBehind="ws_dep_imp_excel.aspx.cs" Inherits="Saving.Applications.deposit.ws_dep_imp_excel_ctrl.ws_dep_imp_excel" %>
<%@ Register Src="DsMain.ascx" TagName="DsMain" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       var dsMain = new DataSourceTool;
      
       function OnDsMainClicked(s, r, c) {
           if (c == "b_process") {
               var type_code = dsMain.GetItem(0, "type_code");
               if (type_code == null) {
                   alert("กรุณาเลือกประเภทรายการ!"); return;
               }
               var isConfirm = confirm("ยืนยันการ Import ข้อมูล");
               if (isConfirm) {
                   JsPostProcess();
               }
           } else if (c == "b_delete") {
               var type_code = dsMain.GetItem(0, "type_code");
               if (type_code == null) {
                   alert("กรุณาเลือกประเภทรายการ!"); return;
               }
               var isConfirm = confirm("ยืนยันการลบข้อมูลที่ Import");
               if (isConfirm) {
                   JsPostDelete();
               }
           } else if (c == "b_checkdata") { 
                var type_code = dsMain.GetItem(0, "type_code");
               if (type_code == null) {
                   alert("กรุณาเลือกประเภทรายการ!"); return;
               }
               JsPostChkData(); 
           }
       }
	   function OnDsMainItemChanged(s, r, c,v) {
			if(c=="keep_status"){
				if(v=="0"){
					 dsMain.GetElement(0, "ai_year").disabled = true;
					 dsMain.GetElement(0, "ai_month").disabled = true;
				 }else{
					dsMain.GetElement(0, "ai_year").disabled = false;
					 dsMain.GetElement(0, "ai_month").disabled = false;
				 }
			}
	   }
		function SheetLoadComplete() { 
			if (dsMain.GetItem(0, "coop_control") =="060001") {
                $('#cb_keeptemp').show(); $('#t_year').show(); $('#t_month').show(); 
                dsMain.GetElement(0, "ai_year").style.display = 'block';
				dsMain.GetElement(0, "ai_month").style.display = 'block';
				if(dsMain.GetItem(0, "keep_status") =="0"){
					 dsMain.GetElement(0, "ai_year").disabled = true;
					 dsMain.GetElement(0, "ai_month").disabled = true;
				 }
				 else
				 {
					dsMain.GetElement(0, "ai_year").disabled = false;
					 dsMain.GetElement(0, "ai_month").disabled = false;
				 }
            } else {
				$('#cb_keeptemp').hide();$('#t_year').hide();$('#t_month').hide();
                dsMain.GetElement(0, 'keep_status').style.display = 'none';
                dsMain.GetElement(0, 'ai_year').style.display = 'none';
				dsMain.GetElement(0, 'ai_month').style.display = 'none';
            }	
       }
       function exportErrordata() {
           JsPostExportErrdata();
        }       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:Literal ID="LtServerMessage" runat="server"></asp:Literal>
    <uc1:DsMain ID="dsMain" runat="server" />
    <table class="DataSourceFormView">
         <tr>
            <td style="width:180px;">
                <span>File Path : </span>
            </td>
            <td colspan="2">
                <asp:FileUpload ID="txtInput" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <span>*** หมายเหตุ : </span>
            </td>
            <td style="width:180px;">
                <div style="font-size:16px;background-color:khaki"><a href="../../../filecommon/Sample_Deposit_Excal.xlsx">ดาวน์โหลดไฟล์ตัวอย่าง</a></div>
            </td>
            <td>
                **** ไฟล์ที่จะ Import จะต้องเป็นไฟล์ .xlsx เท่านั้น
            </td>
        </tr>
        <tr>
            <td>
                 <span>ข้อมูลที่ไม่ถูกต้อง : </span>   
            </td>
            <td>
                <button style="width:200px;font-size:14px;background-color:antiquewhite" onclick="exportErrordata()">ดาวน์โหลดไฟล์</button>
            </td>
            <td style="float:left;">
                <asp:Literal ID="Lt_errordata_fileexcel" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
    <%--<table class="DataSourceFormView">
        <tr>
            <td style="width: 150px;">
                <span>วันที่ : </span>
            </td>
            <td>
                <asp:TextBox ID="entry_date" runat="server" Width="100px" Style="text-align: center;"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <span>ประเภทรายการ : </span>
            </td>
            <td>
                <asp:DropDownList ID="type_code" runat="server" Width="244px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <span>File Path : </span>
            </td>
            <td>
                <asp:FileUpload ID="txtInput" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <span>*** หมายเหตุ : </span>
            </td>
            <td>
                ไฟล์ที่จะ Import จะต้องเป็นไฟล์ .xlsx เท่านั้น
            </td>
        </tr>
        <tr>
            <td>
                <span>ลบข้อมูลที่ Import : </span>
            </td>
            <td>
                <asp:Button ID="b_delete" runat="server" Text="ลบข้อมูลที่ Import" Style="width: 244px;"
                    OnClientClick="return fnConfirmDelete()" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <a href="../../../filecommon/Sample_Deposit_Excal.xlsx">ไฟล์ตัวอย่าง</a>
            </td>
        </tr>
    </table>--%>

    <asp:Panel ID="Panel1" runat="server" BorderStyle="Ridge" ScrollBars="Auto" Height="100%"
        Width="850px">
        <table  cellspacing="0" rules="all" class="DataSourceRepeater" border="1" style="border-collapse: collapse; width:850px;">
            <tr>
                <th width="7%" style="position:sticky;">
                    ลำดับ
                </th>
                <th width="19%" style="position:sticky;">
                    เลขบัญชี
                </th>
                <th width="19%"  style="position:sticky;">
                    เลขสมาชิก
                </th>
                <th style="position:sticky;">
                    จำนวนเงิน
                </th>
            </tr>
        </table>
        <div style="overflow-y: scroll; overflow-x: hidden; height: 450px; width:850px;">
            <asp:GridView ID="GridView1" runat="server"
                  ShowHeader="false"  class="DataSourceRepeater" AutoGenerateColumns="False" >

                <Columns>
                <asp:TemplateField ItemStyle-Width="7%" HeaderText="ลำดับ">
                    <ItemTemplate>
                        <asp:TextBox ID="num" class="Rnum" runat="server" Text="<%# Container.DataItemIndex + 1 %>"
                            Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="เลขบัญชี" ItemStyle-Width="19%">
                    <ItemTemplate>
                        <asp:TextBox ID="deptaccount_no" runat="server" Text='<%# Bind("deptaccount_no") %>' Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="เลขสมาชิก" ItemStyle-Width="19%">
                    <ItemTemplate>
                        <asp:TextBox ID="member_no" runat="server" Text='<%# Bind("member_no") %>' Style="text-align: center" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="จำนวนเงิน" >
                    <ItemTemplate>
                        <asp:TextBox ID="item_payment" runat="server" Text='<%# Bind("item_payment","{0:N}") %>'
                            Style="text-align: right" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>
</asp:Content>
