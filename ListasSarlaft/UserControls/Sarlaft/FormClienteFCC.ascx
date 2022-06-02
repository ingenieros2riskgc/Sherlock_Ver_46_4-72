<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormClienteFCC.ascx.cs"
    Inherits="ListasSarlaft.UserControls.FormClienteFCC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" style="background-color: #EFEEDE">
    <ContentTemplate>
        <div>
        <table align="center">
            <tr>
                <td>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Icons/TituloFCC.png" />
                </td>
            </tr>
        </table>
            </div>
            <div>
                <div>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Tipo Identificación" Font-Names="Arial" Font-Bold="false" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Número de Documento" Font-Names="Arial" Font-Bold="false" Font-Size="Small" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DDLTipoIden" runat="server" Font-Names="Arial" DataSourceID="SqlDataSource_TipoIden" DataTextField="Descripcion" DataValueField="IdTipoDoc" Width="300px" Height="30px"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_TipoIden" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SELECT * FROM dbo.TipoDocumento"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RvfTipoIden" runat="server" ControlToValidate="DDLTipoIden"
                                                InitialValue="-1" Font-Bold="true" ForeColor="Red" ValidationGroup="BuscarWillis">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNumeroIden" runat="server" Font-Names="Arial" Width="300px" Height="30px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RfvNumeIden" runat="server" ControlToValidate="TxtNumeroIden"
                                                ForeColor="Red" ValidationGroup="BuscarWillis">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="DDLTipoPersona" runat="server" Font-Names="Arial" DataSourceID="SqlDataSource_TipoPersona" DataTextField="Descripcion" DataValueField="IdTipoPersona" Width="300px" Height="30px"></asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource_TipoPersona" runat="server" ConnectionString="<%$ ConnectionStrings:Sherlock_TestConnectionString %>" SelectCommand="SELECT * FROM dbo.TipoPersona"></asp:SqlDataSource>
                                <asp:RequiredFieldValidator ID="RfvTipoPersona" runat="server" ControlToValidate="DDLTipoPersona"
                                                InitialValue="-1" Font-Bold="true" ForeColor="Red" ValidationGroup="BuscarWillis">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                
                                <asp:ImageButton ID="ImgBtnBuscar" runat="server" ImageUrl="~/Imagenes/Icons/BuscarWillis.png" ValidationGroup="BuscarWillis" OnClick="ImgBtnBuscar_Click" />
                                
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    
                </div>
            </div>
        <table id="IframeFCC" runat="server" align="center" visible="false" width="100%">
            <tr runat="server" id="trIframe" align="center" >
                <td align="center">
                    <br />
                    <iframe runat="server" id="IFrame_1" style="width: 100%; height: 1200px; border: none;"></iframe>
                    <div runat="server" id="dvIframe" width="100%" height="100%"></div>
                </td>
            </tr>
        </table>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" TargetControlID="btndummy"
            PopupControlID="pnlMsgBox" OkControlID="btnAceptar" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BackColor="#FFFFFF" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #5D7B9D">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Arial" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Arial" Font-Size="Small" Font-Bold="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Arial" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
