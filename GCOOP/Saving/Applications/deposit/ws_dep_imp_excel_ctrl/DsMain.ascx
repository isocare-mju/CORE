<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.Applications.deposit.ws_dep_imp_excel_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormViewMain" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="12%">
                    <span>วันที่ : </span>
                </td>
                <td width="15%">
                    <asp:TextBox ID="entry_date" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td width="13%">
                    <span>ประเภทรายการ : </span>
                </td>
                <td width="20%">
                    <asp:DropDownList ID="type_code" runat="server">
                    </asp:DropDownList>
                </td>       
                  <td width="15%">
                    <asp:Button ID="b_checkdata" runat="server" Text="ตรวจสอบไฟล์"  style="width:100%"/>
                </td>
                <td width="15%">
                    <asp:Button ID="b_process" runat="server" Text="Import ข้อมูล" style="width:100%"/>
                </td>
                <td width="15%">
                    <asp:Button ID="b_delete" runat="server" Text="ลบข้อมูลที่ Import"  style="width:100%"/>
                </td>
            </tr>
            <tr>
                <td>
                     <div id="cb_keeptemp">
                        <asp:CheckBox ID="keep_status" runat="server" Text="นอกสังกัด" TextAlign="Right" Font-Bold="True" />
                    </div>
                </td>                
                <td >
                    <div id="t_year"><span>ปี:</span></div>                 
                </td>
                <td >
                        <asp:TextBox ID="ai_year" runat="server"  style="text-align:center"></asp:TextBox>
                </td>
                <td>
                    <div id="t_month"><span>เดือน:</span> </div>  
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ai_month" runat="server" style="text-align:center">
                        <asp:ListItem Value = "1" >มกราคม</asp:ListItem>
                        <asp:ListItem Value = "2" > กุมภาพันธ์ </asp:ListItem>       
                        <asp:ListItem Value = "3" > มีนาคม </asp:ListItem>      
                        <asp:ListItem Value = "4" > เมษายน </asp:ListItem>   
                        <asp:ListItem Value = "5" > พฤษภาคม </asp:ListItem>        
                        <asp:ListItem Value = "6" > มิถุนายน </asp:ListItem>      
                        <asp:ListItem Value = "7" > กรกฎาคม </asp:ListItem>      
                        <asp:ListItem Value = "8" > สิงหาคม </asp:ListItem>      
                        <asp:ListItem Value = "9" > กันยายน </asp:ListItem>      
                        <asp:ListItem Value = "10" > ตุลาคม </asp:ListItem>      
                        <asp:ListItem Value = "11" > พฤศจิกายน </asp:ListItem>      
                        <asp:ListItem Value = "12" > ธันวาคม </asp:ListItem>                      
                    </asp:DropDownList>
                </td>
            </tr> 
        </table>
    </EditItemTemplate>
</asp:FormView>