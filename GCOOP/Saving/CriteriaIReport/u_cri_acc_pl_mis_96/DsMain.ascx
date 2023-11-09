<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_acc_pl_mis_96.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td >
                    <div>
                        <span>รหัสงบการเงิน :</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="moneysheet_code" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <div>
                        <span>ปี :</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:TextBox ID="year_1" runat="server"></asp:TextBox>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <span>เดือน :</span>
                    </div>
                </td>
                <td width="25%">
                    <div>
                        <asp:DropDownList ID="month_1_1" runat="server">
                        <asp:ListItem Value=""> --เลือกเดือน-- </asp:ListItem>
                            <asp:ListItem Value="1"> มกราคม</asp:ListItem>
                            <asp:ListItem Value="2"> กุมภาพันธ์</asp:ListItem>
                            <asp:ListItem Value="3"> มีนาคม</asp:ListItem>
                            <asp:ListItem Value="4"> เมษายน</asp:ListItem>
                            <asp:ListItem Value="5"> พฤษภาคม</asp:ListItem>
                            <asp:ListItem Value="6"> มิถุนายน</asp:ListItem>
                            <asp:ListItem Value="7"> กรกฎาคม</asp:ListItem>
                            <asp:ListItem Value="8"> สิงหาคม</asp:ListItem>
                            <asp:ListItem Value="9"> กันยายน</asp:ListItem>
                            <asp:ListItem Value="10"> ตุลาคม</asp:ListItem>
                            <asp:ListItem Value="11"> พฤษจิกายน</asp:ListItem>
                            <asp:ListItem Value="12"> ธันวาคม</asp:ListItem>
                        </asp:DropDownList>
                        
                       
                    </div>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
