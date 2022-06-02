<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IFcategorias.aspx.cs" Inherits="ListasSarlaft.UserControls.Riesgos.Iframe.IFcategorias" %>

<link href="../../../../Styles/AdminSitio.css" rel="stylesheet" type="text/css" />
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function fnClick(sender, e) {
            __doPostBack(sender, e);
        }
        function onClick() {
            parent.refresh();
        }
    </script>
    <style type="text/css">
        #Background {
            position: fixed;
            top: 0px;
            bottom: 0px;
            left: 0px;
            right: 0px;
            overflow: hidden;
            padding: 0;
            margin: 0;
            background: none;
            background-color:lightgrey;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 9988;
        }
        #Progress {
            position: fixed;
            left: 0px;
            top: 30%;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background-color: transparent;
            border: 0px solid Gray;
            background-image: url('./Imagenes/Icons/loader.gif');
            background-repeat: no-repeat;
            background-position: center;
        }
        .prevent-click-style {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 100%;">
                    <tr>
                        <td align="center">
                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="0">
                                <ProgressTemplate>
                                    <div id="Background">
                                    </div>
                                    <div id="Progress">
                                        <asp:Label ID="Lbl11" runat="server" Text="Procesando, por favor espere..." Font-Names="Calibri"
                                            Font-Size="Small"></asp:Label>
                                        <br />
                                        <asp:Image ID="Img11" runat="server" ImageUrl="~/Imagenes/Icons/loading.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <div>
                            <caption>
                                <h5 runat="server" id="lblActControlSuccess" visible="false" style="color: green">La calificación del control se actualizó con éxito</h5>
                                <h5 runat="server" id="lblActRiesgos" visible="false"></h5>
                                <h5 runat="server" id="lblActControlFailed" visible="false" style="color: indianred">Ocurrió un error al actualzar el control</h5>
                                <h5 style="color: grey">Sólo se muestran las variables que tienen algún peso configurado.</h5>
                                <table id="tbVariableCategoriaControl" runat="server" border="1" bordercolor="White" cellpadding="2" cellspacing="0" style="width: 100%">
                                </table>
                                <table border="1" bordercolor="White" cellpadding="2" cellspacing="0" style="width: 100%">
                                    <tr id="trTituloResultado" runat="server" align="center">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Resultado de la calificación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trResultado" runat="server" align="center">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td bgcolor="#BBBBBB">
                                                        <asp:Label ID="Label15" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Calificación del control"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="80px">
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
                                <table id="tbLimpiar" runat="server" border="1" bordercolor="White" cellpadding="2" cellspacing="0" style="width: 100%" hidden>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblTextClean" runat="server" Text="Para limpiar la calificación"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="ImbClean" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" OnClick="ImbClean_Click" ToolTip="Cancelar" />
                                        </td>
                                    </tr>
                                </table>
                                <table id="tbBotonesUpdate" runat="server" border="1" bordercolor="White" cellpadding="2" cellspacing="0" style="width: 100%">
                                    <tr>
                                        <td align="center">
                                            <asp:ImageButton ID="IBinsert" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" 
                                                OnClick="IBinsert_Click" Style="height: 20px" ToolTip="Guardar" 
                                                ValidationGroup="validateControl" Visible="false" ClientIDMode="Static" OnClientClick="this.classList.add('prevent-click-style');return true;"/>
                                            <asp:ImageButton ID="IBupdate" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png" OnClick="IBupdate_Click" ToolTip="Guardar" ValidationGroup="validateControl" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </caption>
                        </div>
                    </tr>
                    <table style="width: 100%" runat="server" id="tJustificacionCambios" visible="false">
                        <tr align="center">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label35" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Justificación De Los Cambios"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label36" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Justificación:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox16" runat="server" Font-Names="Calibri" Font-Size="Small" Height="80px" TextMode="MultiLine" Width="400px" Visible="false"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox16" Font-Bold="True" ForeColor="Red" ValidationGroup="validateControl">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png" ToolTip="Cancelar" Visible="False" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: center">
                            <asp:TabContainer ID="TabContainer2" runat="server" ActiveTabIndex="1" Font-Names="Calibri"
                                Font-Size="Small" Width="100%" Visible="false">
                                <asp:TabPanel ID="TabPanel3" runat="server" HeaderText="Justificación cambios" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <ContentTemplate>
                                        <table>
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
                                    </ContentTemplate>
                                </asp:TabPanel>
                                <asp:TabPanel ID="TabPanel4" runat="server" HeaderText="Documentos adjuntos" Font-Names="Calibri"
                                    Font-Size="Small">
                                    <ContentTemplate>
                                        <table style="width: 100%">
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
                                                                <asp:Label ID="Label38" runat="server" Text="Adjuntar documentoddddd:" Font-Names="Calibri"
                                                                    Font-Size="Small"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="FileUpload2" runat="server" Font-Names="Calibri" Font-Size="Small" PostBackUrl="../../../UserControls/Riesgos/Iframe/IFcategorias.aspx" />
                                                            </td>
                                                            <td>
                                                                <asp:ImageButton ID="btnAdjuntar" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                                    ToolTip="Adjuntar" OnClick="btnAdjuntar_Click" />
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
                </table>
                </body>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="IBinsert" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>
