﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_ryears.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="40%">
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="2" width="60%">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <div>
                        <span>ปี (พ.ศ.):</span></div>
                </td>
                <td >
                    <asp:TextBox ID="as_syear" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td >
                    <div>
                        <span>ถึงปี (พ.ศ.):</span></div>
                </td>
                <td >
                    <asp:TextBox ID="as_eyear" runat="server" Style="text-align: center"></asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>
