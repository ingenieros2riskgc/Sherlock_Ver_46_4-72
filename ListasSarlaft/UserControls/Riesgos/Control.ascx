<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control.ascx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Control" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
    function refresh() {
        location.reload();
    }
    function refreshIframe() {
        alert('aqui');
        document.getElementById('IFcategorias').contentWindow.location.reload();
    }
</script>
<script>
    function ValidateMe() {
        var ok = Page_ClientValidate();
        if (ok)
            return confirm("Si ha modificado la calificación del control es necesario guardar el cambio antes de continuar. \nDesea continuar de todos modos?");
        else
            return false;
    }
</script>
<style type="text/css">
    .gridViewHeader a:link {
        text-decoration: none;
    }

    .FondoAplicacion {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    iframe {
        display: block; /* iframes are inline by default */
        border: none; /* Reset default border */
        height: 80vh; /* Viewport-relative units */
    }

    .style1 {
        height: 74px;
    }
</style>

<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, 
@Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
    SelectCommand="SELECT [IdCorreosEnviados], [IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosEnviados]"
    UpdateCommand="UPDATE [Notificaciones].[CorreosEnviados] SET [FechaEnvio] = @FechaEnvio, [Estado] = @Estado WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    OnInserted="SqlDataSource200_On_Inserted">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEvento" Type="Decimal" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="Tipo" Type="String" />
        <asp:Parameter Name="IdRegistro" Type="Decimal" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
        <asp:Parameter Direction="Output" Name="NewParameter2" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaEnvio" Type="DateTime" />
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource201" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosRecordatorio] WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosRecordatorio] ([IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario]) VALUES (@IdCorreosEnviados, @NroDiasRecordatorio, CONVERT(datetime, @FechaFinal, 120), @Estado, @FechaRegistro, @IdUsuario)"
    SelectCommand="SELECT [IdCorreosRecordatorio], [IdCorreosEnviados], [NroDiasRecordatorio], [FechaFinal], [Estado], [FechaRegistro], [IdUsuario] FROM [Notificaciones].[CorreosRecordatorio]"
    UpdateCommand="UPDATE [Estado] = @Estado WHERE [IdCorreosRecordatorio] = @IdCorreosRecordatorio">
    <DeleteParameters>
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdCorreosEnviados" Type="Decimal" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="FechaFinal" Type="String" />
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
        <asp:Parameter Name="IdUsuario" Type="Decimal" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="Estado" Type="String" />
        <asp:Parameter Name="IdCorreosRecordatorio" Type="Decimal" />
    </UpdateParameters>
</asp:SqlDataSource>
<uc:OkMessageBox ID="omb" runat="server" />
<asp:ModalPopupExtender ID="mpeMsgBoxOkNo" runat="server" TargetControlID="btndummyOkNo"
    PopupControlID="pnlMsgBoxOkNo" OkControlID="btnCancelarOkNo" BackgroundCssClass="FondoAplicacion"
    Enabled="True" DropShadow="true">
</asp:ModalPopupExtender>
<asp:Button ID="btndummyOkNo" runat="server" Text="Button" Style="display: none" />
<asp:Label ID="lbldummyOkNo" runat="server" Text="" Visible="False"></asp:Label>
<asp:Panel ID="pnlMsgBoxOkNo" runat="server" Width="400px" Style="display: none;"
    BorderColor="#575757" BackColor="#FFFFFF" BorderStyle="Solid">
    <table width="100%">
        <tr class="topHandle" style="background-color: #5D7B9D">
            <td align="center" runat="server" id="tdCaptionOkNo" colspan="2">&nbsp;
                        <asp:Label ID="lblCaptionOkNo" runat="server" Text="Atención" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
        <tr>

            <td align="center" style="width: 60px" valign="middle">
                <asp:Image ID="imgInfoOkNo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
            </td>
            <td align="left" valign="middle">
                <asp:Label ID="lblMsgBoxOkNo" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
            </td>

        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
        <tr align="right">
            <td align="right" colspan="2">
                <asp:Button ID="btnAceptarOkNo" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small"
                    OnClick="btnAceptarOkNo_Click" ValidationGroup="Justificacion" />
                <asp:Button ID="btnCancelarOkNo" runat="server" Text="Cancelar" Font-Names="Calibri"
                    Font-Size="Small" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label111" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large"
                        ForeColor="White" Text="Controles"></asp:Label>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbGrid" runat="server">
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="Código:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox14" runat="server" Width="150px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label16" runat="server" Text="Nombre:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox15" runat="server" Width="300px" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label17" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox21" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="300px" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia3" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                            <asp:ImageButton ID="imgDependencia3" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupDependencia3" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia3" BehaviorID="popup3"
                                                PopupControlID="pnlDependencia3" OffsetY="-200">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlDependencia3" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                    <tr align="right" bgcolor="#5D7B9D">
                                                        <td>
                                                            <asp:ImageButton ID="btnClosepp3" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TreeView3" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView3_SelectedNodeChanged">
                                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Button ID="BtnOk3" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup3').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr align="center">
                                        <td>
                                            <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Imagenes/Icons/lupa.png"
                                                ToolTip="Consultar" OnClick="ImageButton12_Click" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton15_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    OnRowCommand="GridView1_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField HeaderText="Código" DataField="CodigoControl" />
                                        <asp:BoundField HeaderText="Nombre" DataField="NombreControl" />
                                        <asp:BoundField HeaderText="Fecha Creación" DataField="FechaRegistro" />
                                        <asp:BoundField HeaderText="Test" DataField="NombreTest" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                            CommandName="Modificar" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/delete.png" Text="Eliminar"
                                            CommandName="Borrar" Visible="false" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr align="right">
                            <td>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                    ToolTip="Insertar" OnClick="ImageButton3_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbCampos" runat="server" visible="false">
                        <tr>
                            <td>
                                <table>
                                    <tr align="center">
                                        <td colspan="4" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label1" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Información Del Control"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label2" runat="server" Text="Código:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox1" runat="server" Width="150px" Enabled="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label3" runat="server" Text="Nombre:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox2" runat="server" Width="584px" Font-Names="Calibri" Font-Size="Small" MaxLength="200"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox2"
                                                InitialValue="" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label4" runat="server" Text="Descripción:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td colspan="3" bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine" Width="584px" Height="50px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox3"
                                                InitialValue="" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label5" runat="server" Text="Objetivo Control:" Font-Size="Small"
                                                Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:TextBox ID="TextBox4" runat="server" Height="50px" TextMode="MultiLine" Width="584px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="Right">
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td>

                                                        <asp:Label ID="LblResponsableEjecucion" runat="server" Visible="false" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:Label ID="Label40" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Text="Jerarquía Organizacional"></asp:Label>
                                                        <asp:ImageButton ID="ImageButtonJerOrg" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" Height="24px" />
                                                        <asp:PopupControlExtender ID="PopupControlExtenderJerOrg" runat="server" DynamicServicePath=""
                                                            Enabled="True" ExtenderControlID="" TargetControlID="ImageButtonJerOrg" BehaviorID="popupJerOrg"
                                                            PopupControlID="pnlJerOrg" OffsetY="-200">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pnlJerOrg" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                <tr align="right" bgcolor="#5D7B9D">
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                            OnClientClick="$find('popupJerOrg').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="TreeViewJerarquiaOrg" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewJerarquiaOrg_SelectedNodeChanged">
                                                                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupJerOrg').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Label ID="Label41" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Text="Grupos de Trabajo"></asp:Label>
                                                        <asp:ImageButton ID="ImageButtonTablaParam" runat="server" ImageUrl="~/Imagenes/Icons/workflow (24).png"
                                                            OnClientClick="return false;" />
                                                        <asp:PopupControlExtender ID="PopupControlExtenderTablaParam" runat="server" DynamicServicePath=""
                                                            Enabled="True" ExtenderControlID="" TargetControlID="ImageButtonTablaParam" BehaviorID="popupTablaParam"
                                                            PopupControlID="pnlTablaParam" OffsetY="-200">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pnlTablaParam" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                <tr align="right" bgcolor="#5D7B9D">
                                                                    <td>
                                                                        <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                            OnClientClick="$find('popupTablaParam').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="TreeViewTablaParam" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeViewTablaParam_SelectedNodeChanged">
                                                                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Button ID="Button2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popupTablaParam').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Borrar Responsables Ejecución" OnClick="ImageButton1_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label39" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable Ejecución:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:TextBox ID="TxbResponsableEjecución" runat="server" Height="50px" TextMode="MultiLine" Width="584px"
                                                Font-Names="Calibri" Font-Size="Small" Enabled="false"></asp:TextBox>
                                            <%--<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Borrar responsables" OnClick="ImageButton1_Click"  />--%>


                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label123" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable Calificación:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:TextBox ID="TextBox34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="450px" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="TextBox34"
                                                ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                            <asp:PopupControlExtender ID="popupDependencia1" runat="server" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia1" BehaviorID="popup1"
                                                PopupControlID="pnlDependencia1" OffsetY="-200">
                                            </asp:PopupControlExtender>
                                            <asp:Panel ID="pnlDependencia1" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                    <tr align="right" bgcolor="#5D7B9D">
                                                        <td>
                                                            <asp:ImageButton ID="btnClosepp1" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                OnClientClick="$find('popup1').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TreeView ID="TreeView1" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" AutoGenerateDataBindings="False">
                                                                <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                            </asp:TreeView>
                                                        </td>
                                                    </tr>
                                                    <tr align="center">
                                                        <td>
                                                            <asp:Button ID="BtnOk1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label25" runat="server" Text="Periodicidad:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList6" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DropDownList6"
                                                InitialValue="---" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label9" runat="server" Text="Test:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList2" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="DropDownList2"
                                                InitialValue="---" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Reduce:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:DropDownList ID="DropDownList1" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small">
                                                <asp:ListItem Value="---">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="DropDownList1"
                                                InitialValue="---" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <asp:Label ID="LprocessText" runat="server" Text="Para generar la calificación del control has click en el botón:" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td align="center" colspan="4">
                                            <asp:ImageButton ID="IBprocess" runat="server" CausesValidation="true" CommandName="Procesar" ImageUrl="~/Imagenes/Aplicacion/Gears.png" OnClick="IBprocess_Click" Text="Procesar" ToolTip="Procesar" ValidationGroup="validateControl" />

                                        </td>
                                    </tr>
                                    <tr align="center" runat="server" id="trTituloCobertura" visible="false">
                                        <td colspan="4" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Cobertura Del Control"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr align="center" runat="server" id="trCobertura" visible="false">
                                        <td colspan="4">
                                            <table runat="server" id="tbVariableCategoriaControl" style="width: 100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                <!--<tr>
                                                    <td>
                                                        <div runat="server" id="divTable">
                                                            
                                                        </div>
                                                    </td>
                                                </tr>-->
                                            </table>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trIframe" visible="false">
                                        <td colspan="4">
                                            <iframe runat="server" id="IFcategorias"></iframe>
                                            <div runat="server" id="dvIframe"></div>
                                        </td>
                                    </tr>
                                    <tr align="left" runat="server" id="trClaseControl" visible="false">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label6" runat="server" Text="Clase de control:" Font-Size="Small"
                                                Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList9" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="calcularEficacia">
                                                <asp:ListItem Value="0" Enabled="false">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DropDownList9"
                                                InitialValue="0" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label7" runat="server" Text="Tipo Control:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList10" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="calcularEficacia">
                                                <asp:ListItem Value="0" Enabled="false">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="DropDownList10"
                                                InitialValue="0" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left" runat="server" id="trResponsableExp" visible="false">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label32" runat="server" Text="Responsable con experiencia:" Font-Size="Small"
                                                Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList8" runat="server" Width="155px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="calcularEficacia">
                                                <asp:ListItem Value="0" Enabled="false">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="DropDownList8"
                                                InitialValue="0" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label33" runat="server" Text="Documentación:" Font-Size="Small" Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:DropDownList ID="DropDownList11" runat="server" Width="328px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="calcularEficacia">
                                                <asp:ListItem Value="0" Enabled="false">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="DropDownList11"
                                                InitialValue="0" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left" runat="server" id="trResponsabilidad" visible="false">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label34" runat="server" Text="Responsabilidad:" Font-Size="Small"
                                                Font-Names="Calibri"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE" colspan="3">
                                            <asp:DropDownList ID="DropDownList12" runat="server" Width="590px" Font-Names="Calibri"
                                                Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="calcularEficacia">
                                                <asp:ListItem Value="0" Enabled="false">---</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="DropDownList12"
                                                InitialValue="0" Font-Bold="true" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="center" runat="server" id="trTituloResultado" visible="false">
                                        <td colspan="4" bgcolor="#BBBBBB">
                                            <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Resultado de la calificación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center" runat="server" id="trResultado" visible="false">
                                        <td colspan="4">
                                            <table>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Calificación del control"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel1" runat="server" Width="80px" Height="50px">
                                                            <table style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="Label14" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trComentariosArchivos" runat="server" visible="false" align="center">
                            <td>
                                <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                    Font-Size="Small" Width="600px">
                                    <asp:TabPanel ID="TabPanel1" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label28" runat="server" Text="Justificación De Los Cambios" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label29" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox12" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="TextBox12"
                                                                        Font-Bold="True" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="right">
                                                                <td colspan="2">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" Visible="False" OnClick="ImageButton9_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="2">
                                                                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                        ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView4_RowCommand">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="False" />
                                                                            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                            <asp:BoundField DataField="ComentarioCorto" HeaderText="Justificación" />
                                                                            <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="False" />
                                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                                                CommandName="Ver" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                    <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                                        Font-Size="Small">
                                        <ContentTemplate>
                                            <table>
                                                <tr align="center">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label26" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td>
                                                        <table>
                                                            <tr align="center">
                                                                <td bgcolor="#BBBBBB">
                                                                    <asp:Label ID="Label27" runat="server" Text="Adjuntar documento :" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                        ToolTip="Adjuntar" OnClick="ImageButton7_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td colspan="3">
                                                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                        ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView3_RowCommand">
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False" />
                                                                            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                            <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                            <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                            <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                CommandName="Descargar" />
                                                                        </Columns>
                                                                        <EditRowStyle BackColor="#999999" />
                                                                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:TabPanel>
                                </asp:TabContainer>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table id="tbBotonesUpdate" runat="server">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validateControl" OnClick="ImageButton4_Click"
                                                Style="height: 20px" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                ToolTip="Guardar" Visible="false" ValidationGroup="validateControl" OnClick="ImageButton5_Click" OnClientClick="return ValidateMe();" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                ToolTip="Cancelar" OnClick="ImageButton6_Click" />
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trGridDetalleEvaluacion" runat="server" visible="false">
                            <td>
                                <table width="100%">
                                    <tr align="center">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label13" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Planes de evaluación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                            BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                            OnRowCommand="GridView2_RowCommand">
                                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Estado" DataField="NombreEstadoPlanEvaluacion" />
                                                                <asp:BoundField HeaderText="Fecha Inicio" DataField="FechaInicio" />
                                                                <asp:BoundField HeaderText="Fecha Proyectada Fin" DataField="FechaProyectadaFin" />
                                                                <asp:BoundField HeaderText="Fecha Real Cierre" DataField="FechaRealCierre" />
                                                                <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Editar"
                                                                    CommandName="Modificar" />
                                                            </Columns>
                                                            <EditRowStyle BackColor="#999999" />
                                                            <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                                <tr align="right">
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton8" runat="server" ToolTip="Insertar" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            OnClick="ImageButton8_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trCamposDetalleEvaluacion" runat="server" visible="false" align="center">
                                        <td>
                                            <table>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label31" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#EEEEEE">
                                                        <asp:TextBox ID="TextBox33" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="450px" Enabled="False"></asp:TextBox>
                                                        <asp:Label ID="lblIdDependencia2" runat="server" Visible="False" Font-Names="Calibri"
                                                            Font-Size="Small"></asp:Label>
                                                        <asp:ImageButton ID="imgDependencia2" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                            OnClientClick="return false;" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ForeColor="Red"
                                                            ControlToValidate="TextBox33" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                        <asp:PopupControlExtender ID="popupDependencia2" runat="server" DynamicServicePath=""
                                                            Enabled="True" ExtenderControlID="" TargetControlID="imgDependencia2" BehaviorID="popup2"
                                                            PopupControlID="pnlDependencia2" OffsetY="-200">
                                                        </asp:PopupControlExtender>
                                                        <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
                                                            <table width="100%" border="1" cellspacing="0" cellpadding="2" bordercolor="White">
                                                                <tr align="right" bgcolor="#5D7B9D">
                                                                    <td>
                                                                        <asp:ImageButton ID="btnClosepp2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                                                                            OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TreeView ID="TreeView2" ExpandDepth="3" runat="server" Font-Names="Calibri"
                                                                            Font-Size="Small" LineImagesFolder="~/TreeLineImages" ForeColor="Black" ShowLines="True"
                                                                            AutoGenerateDataBindings="False" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged">
                                                                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                                                                        </asp:TreeView>
                                                                    </td>
                                                                </tr>
                                                                <tr align="center">
                                                                    <td>
                                                                        <asp:Button ID="BtnOk2" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup2').hidePopup(); return false;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label18" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Inicio"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="150px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="TextBox15_CalendarExtender" runat="server" Enabled="True"
                                                            TargetControlID="TextBox5" Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" ControlToValidate="TextBox5"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label19" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Proyectada Fin"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox6" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="150px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="TextBox6"
                                                            Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox6"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" id="trFechaRealCierre" runat="server" visible="false">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label24" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fecha Real Cierre"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox10" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="150px"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="TextBox10"
                                                            Format="yyyy-MM-dd"></asp:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="TextBox10"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label20" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Tipo Prueba"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="155px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="DropDownList5"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion" InitialValue="---">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label21" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descripcion Evaluacion"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox7" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="TextBox7"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label22" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Recursos"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox8" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="TextBox8"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left" id="trResultados" runat="server" visible="false">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label23" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Resultados"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox9" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="TextBox9"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr align="left">
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label30" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Estado"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownList7" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Width="155px">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="DropDownList7"
                                                            ForeColor="Red" ValidationGroup="DetalleEvaluacion" InitialValue="---">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trComArcPlanEva" runat="server" visible="false" align="center">
                                                    <td colspan="2">
                                                        <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="0" Font-Names="Calibri"
                                                            Font-Size="Small" Width="600px">
                                                            <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                                                                Font-Size="Small">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label35" runat="server" Text="Justificación De Los Cambios" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label36" runat="server" Text="Justificación:" Font-Names="Calibri"
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="TextBox16" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox16"
                                                                                                Font-Bold="True" ForeColor="Red" ValidationGroup="DetalleEvaluacion">*</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="right">
                                                                                        <td colspan="2">
                                                                                            <table>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                                            ToolTip="Cancelar" Visible="False" OnClick="ImageButton16_Click" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td colspan="2">
                                                                                            <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView5_RowCommand">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="False" />
                                                                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                                    <asp:BoundField DataField="ComentarioCorto" HeaderText="Justificación" />
                                                                                                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="False" />
                                                                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                                                                        CommandName="Ver" />
                                                                                                </Columns>
                                                                                                <EditRowStyle BackColor="#999999" />
                                                                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:TabPanel>
                                                            <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                                                                Font-Size="Small">
                                                                <ContentTemplate>
                                                                    <table>
                                                                        <tr align="center">
                                                                            <td bgcolor="#BBBBBB">
                                                                                <asp:Label ID="Label37" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                                                                                    Font-Size="Small"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center">
                                                                            <td>
                                                                                <table>
                                                                                    <tr align="center">
                                                                                        <td bgcolor="#BBBBBB">
                                                                                            <asp:Label ID="Label38" runat="server" Text="Adjuntar documento:" Font-Names="Calibri"
                                                                                                Font-Size="Small"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="Calibri" Font-Size="Small" />
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                                                ToolTip="Adjuntar" OnClick="ImageButton17_Click" />
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr align="center">
                                                                                        <td colspan="3">
                                                                                            <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                                                                                                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" OnRowCommand="GridView6_RowCommand">
                                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="False" />
                                                                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                                                                    <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                                                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                                                                                        CommandName="Descargar" />
                                                                                                </Columns>
                                                                                                <EditRowStyle BackColor="#999999" />
                                                                                                <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                                                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader" />
                                                                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                                                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ContentTemplate>
                                                            </asp:TabPanel>
                                                        </asp:TabContainer>
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td colspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" ValidationGroup="DetalleEvaluacion" Visible="False" OnClick="ImageButton13_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                        ToolTip="Guardar" ValidationGroup="DetalleEvaluacion" Visible="False" OnClick="ImageButton2_Click" />
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                        ToolTip="Cancelar" OnClick="ImageButton14_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Names="Calibri" Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
