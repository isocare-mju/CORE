<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsDate.ascx.cs" Inherits="Saving.Applications.fom.dlg.wd_fom_upload_image_ctrl.DsDate" %>
<link id="css1" runat="server" href="../../../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView" style="width: 400px">
            <tr>
                <td width="20%" align="right">
                    <span>วันที่ :</span>
                </td>
                <td align="center">
                    <asp:TextBox ID="sdate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
                <td>
                    <span align="center">&nbsp; &nbsp; &nbsp;ถึง&nbsp; &nbsp; &nbsp; &nbsp;</span>
                </td>
                <td align="center">
                    <asp:TextBox ID="edate" runat="server" Style="text-align: center;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button ID="b_search" runat="server" Text="ค้นหาข้อมูล" Width="100px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
