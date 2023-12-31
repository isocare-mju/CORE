﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.deposit.ws_dep_editinttax_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server">
    <table class="DataSourceRepeater">
        <tr>
            <th width="3%">
            </th>
            
            <th width="12%">
                เลขที่บัญชี
            </th>
            <th width="25%">
                ชื่อบัญชี
            </th>
            <th width="7%">
                รายการ
            </th>
            <th width="20%">
                ยอดทำรายการ
            </th>
             <th width="20%">
                ภาษี
            </th>
             <th width="20%">
                ยอดคงเหลือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td width="3%">
                        <asp:CheckBox ID="CHOOSE_FLAG" runat="server"  Style="text-align: center;" />
                    </td>                    
                    <td width="12%">
                        <asp:TextBox ID="DEPTACCOUNT_NO" runat="server" Style="text-align: center;" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="25%">
                        <asp:TextBox ID="DEPTACCOUNT_NAME" runat="server" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="7%">
                        <asp:TextBox ID="RECPPAYTYPE_CODE" runat="server" Style="text-align: center" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td width="20%">
                        <asp:TextBox ID="DEPTSLIP_NETAMT" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                     <td width="20%">
                        <asp:TextBox ID="TAX_AMT" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                     <td width="20%">
                        <asp:TextBox ID="PRNCBAL" runat="server" ToolTip="#,##0.00" Style="text-align: right"></asp:TextBox>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
