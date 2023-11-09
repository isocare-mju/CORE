<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_year_rdept.DsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            <tr>
                <td width="40%">
                    <div>
                        <span>สหกรณ์:</span>
                    </div>
                </td>
                <td width="60%">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>            
            <tr>
                <td>
                    <div>
                        <span>ประเภทเงินฝาก:</span>
                    </div>
                </td>
                <td >
                    <asp:DropDownList ID="s_depttype" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <span>ถึงประเภทเงินฝาก:</span>
                    </div>
                </td>
                <td >
                    <asp:DropDownList ID="e_depttype" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>         
            <tr>
                <td >
                    <div>
                        <span>ปี (พ.ศ.):</span></div>
                </td>
                <td >
                    <asp:TextBox ID="year" runat="server" Style="text-align: center"></asp:TextBox>
                </td>                
            </tr>
        </table>
    </EditItemTemplate>
</asp:FormView>