﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" 
Inherits="Saving.Applications.app_finance.ws_fin_reprint_ctrl.DsMain" %>
<link id="css1" runat="server" href="../../../JsCss/DataSourceTool.css" rel="stylesheet" type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="DataSourceFormView">
            <tr>
                <td width="15%" >
                    <asp:DropDownList ID="member_flag" runat="server" style="background-color:#6DCDFF">
                        <asp:ListItem Value="1">สมาชิก</asp:ListItem>
                        <asp:ListItem Value="0">บุคคลภายนอก</asp:ListItem>
                        <asp:ListItem Value="3">อื่น ๆ</asp:ListItem>
                        <asp:ListItem Value="8">ออกใบเสร็จ</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <asp:TextBox ID="member_no" runat="server" ></asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>วันที่ใบเสร็จ:</span>
                    </div>
                </td>
                <td width="13%">
                    <asp:TextBox ID="slip_date_s" runat="server" ></asp:TextBox>
                </td>
                <td width="1%">
                    -
                </td>
                <td width="13%">
                    <asp:TextBox ID="slip_date_e" runat="server" > </asp:TextBox>
                </td>
                <td width="12%">
                    <div>
                        <span>ผู้ทำรายการ:</span>
                    </div>
                </td>
                <td width="18%">
                    <asp:DropDownList ID="entry_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>รายการ:</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="pay_recv_status" runat="server">
                        <asp:ListItem Value="1">รายการรับ</asp:ListItem>
                        <asp:ListItem Value="0">รายการจ่าย</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <div>
                        <span>ช่วงใบเสร็จ:</span>
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="slip_no_s" runat="server" ></asp:TextBox>
                </td>
                <td>
                    -
                </td>
                <td>
                    <asp:TextBox ID="slip_no_e" runat="server" ></asp:TextBox>
                </td>
                <td align="right" colspan="2">
                    <asp:Button ID="b_retrieve" runat="server" Text="ดึงข้อมูล" Width="55px" />
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
