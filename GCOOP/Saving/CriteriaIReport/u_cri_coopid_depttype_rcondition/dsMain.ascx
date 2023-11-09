<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dsMain.ascx.cs" Inherits="Saving.CriteriaIReport.u_cri_coopid_depttype_rcondition.dsMain" %>
<link id="css1" runat="server" href="../../JsCss/DataSourceTool.css" rel="stylesheet"
    type="text/css" />
<asp:FormView ID="FormView1" runat="server" DefaultMode="Edit">
    <EditItemTemplate>
        <table class="iReportDataSourceFormView">
            
           <tr>
                <td>
                    <div>
                        <span>สาขา:</span>
                    </div>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="coop_id" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td>
                    <div>
                        <span>เงื่อนไข :</span>
                    </div>
                </td>
                <td>
                    <asp:DropDownList ID="condition" runat="server" style="text-align:center;width:30%">
                        <asp:ListItem Value="1">ช่วงเงิน</asp:ListItem>
                        <asp:ListItem Value="2"> > </asp:ListItem>
                        <asp:ListItem Value="3"> < </asp:ListItem>
                        <asp:ListItem Value="4"> = </asp:ListItem>
                        <asp:ListItem Value="5"> >= </asp:ListItem>
                        <asp:ListItem Value="6"> <= </asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="start_amt" runat="server" style="text-align:right;width:30%" ToolTip="#,##0.00"></asp:TextBox>
                    <span style="width:3%"> - </span>
                    <asp:TextBox ID="end_amt" runat="server" style="text-align:right;width:30%" ToolTip="#,##0.00"></asp:TextBox>
            </tr> 
            <tr>
                 <td width="30%">
                    <div>
                        <span>ตั้งแต่ประเภท :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="depttype_scode" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                 <td width="30%">
                    <div>
                        <span>ถึงประเภท :</span>
                    </div>
                </td>
                <td width="35%" colspan="2">
                    <asp:DropDownList ID="depttype_ecode" runat="server"></asp:DropDownList>
                </td>
            </tr>            
        </table>
     
    </EditItemTemplate>
</asp:FormView>
