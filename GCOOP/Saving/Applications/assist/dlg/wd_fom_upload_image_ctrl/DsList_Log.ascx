<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsList_Log.ascx.cs" Inherits="Saving.Applications.assist.dlg.wd_fom_upload_image_ctrl.DsList_Log" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<table class="DataSourceRepeater">
    <tr>
        <th width="5%">
            ลำดับ
        </th>
        <th width="15%">
            วันที่ทำรายการ
        </th>
        <th width="20%" colspan="2">
            แฟ้มข้อมูล (ชื่อ)
        </th>
        <th width="10%">
            วันที่หมดอายุ
        </th>
        <th width="10%">
            ผู้ทำรายการ
        </th>
        <th width="10%">
            ประเภท File
        </th>
        <th width="5%">
            สถานะ
        </th>
    </tr>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <tr>
                <td>
                    <asp:TextBox ID="running_number" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="date_now" runat="server" Style="text-align: center;" ReadOnly="true" ToolTip="dd/MM/yyyy HH:mm:ss"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="img_name" runat="server" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="B_Show" runat="server" Text="แสดง" />
                </td>
                <td>
                    <asp:TextBox ID="expire_date" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="entry_id" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="data_type" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="sql" runat="server" Style="text-align: center;" ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </ItemTemplate>
    </asp:Repeater>
    <tr>
    </tr>
</table>
