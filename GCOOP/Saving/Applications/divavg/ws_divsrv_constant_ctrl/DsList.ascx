<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList.ascx.cs" Inherits="Saving.Applications.divavg.ws_divsrv_constant_ctrl.DsList" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <table class="DataSourceRepeater" align="center">
        <tr>
            <th style="width: 5%">
            </th>
            <th style="width: 10%">
                ปีปันผล
            </th>
            <th style="width: 10%">
                อัตราปันผล(%)
            </th>
            <th style="width: 10%">
                อัตราเฉลี่ยคืน(%)
            </th>
            <th style="width: 15%">
                ห้ามประมวล
            </th>
            <th style="width: 15%">
                แสดงผลผ่านมือถือ
            </th>
        </tr>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 5%">
                        <asp:TextBox ID="running_number" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="div_year" runat="server" Style="text-align: center;"></asp:TextBox>
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="divpercent_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.000"></asp:TextBox>
                    </td>
                    <td style="width: 10%">
                        <asp:TextBox ID="avgpercent_rate" runat="server" Style="text-align: right;" ToolTip="#,##0.000"></asp:TextBox>
                    </td>
                    <td style="width: 15%">
                        <asp:CheckBox ID="lockproc_flag" runat="server" />
                    </td>
                    <td style="width: 15%">
                        <asp:CheckBox ID="webshow_flag" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Panel>
