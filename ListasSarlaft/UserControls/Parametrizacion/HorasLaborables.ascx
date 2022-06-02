<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorasLaborables.ascx.cs" Inherits="ListasSarlaft.UserControls.Parametrizacion.HorasLaborables" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>

<style type="text/css">
    .gridViewHeader a:link  
    {
     text-decoration:none;
    }      
    .style1
    {
        width: 100%;
    }
</style>

<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [HorasDiarias] FROM [Parametrizacion].[HorasLaborables] WHERE [Parametrizacion].[Id] = 1"
    UpdateCommand="UPDATE [Parametrizacion].[HorasLaborables] SET [HorasDiarias] = @HorasDiarias WHERE [Id] = @Id" 
    DeleteCommand="DELETE FROM [Parametrizacion].[HorasLaborables] WHERE [Id] = @Id" 
    InsertCommand="INSERT INTO [Parametrizacion].[HorasLaborables] ([Id], [HorasDiarias]) VALUES (@Id, @HorasDiarias)" >
    <DeleteParameters>
        <asp:Parameter Name="Id" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="Id" Type="Int32" />
        <asp:Parameter Name="HorasDiarias" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="HorasDiarias" Type="Int32" />
        <asp:Parameter Name="Id" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <uc:OkMessageBox ID="omb" runat="server" /> 

        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" 
                            Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center" >
                        <asp:Image ID="imgInfo" runat="server" 
                            ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png"/>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" 
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
<%--                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Ok" OnClick="btnImgokEliminar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;"/>--%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox" BehaviorId="mypopup" 
            Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"  DropShadow="true" >
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" style="display:none"/>
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>

        <table align="center"  width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Horas Laborables" Font-Bold="False"
                        Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr  align="center" id="filaDetalle" runat="server" visible="true">
                <td bgcolor="#EEEEEE">

                        <table  class = "tabla" >
                            <tr>
                                <td align="left" bgcolor="#BBBBBB">
                                    <asp:Label ID="Label5" runat="server" Text="Horas Laborables Diarias:" 
                                        CssClass="Apariencia"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHL" runat="server" Width="100px" CssClass="Apariencia" MaxLength="3"></asp:TextBox>
                                </td>
                            </tr>
                             <tr align="center">
                                <td colspan="2">
                                    <table  class = "tablaSinBordes">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                    Style="text-align: right" onclick="btnImgActualizar_Click" 
                                                    ToolTip="Guardar"/>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>


<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>