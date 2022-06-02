<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CorreosDestino.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Notificaciones.CorreosDestino" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }
    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }
    .ajax__html_editor_extender_texteditor
    {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
</style>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Parametrizacion].[Paises] WHERE [IdPais] = @IdPais"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosDestinatarios] ([IdEvento], [Destinatario], [AJefeInmediato], [AJefeMediato], [Copia], [Otros], [Asunto], [Cuerpo], [NroDiasRecordatorio], [IdUsuario],  [FechaRegistro])
                   VALUES (@IdEvento, @Destinatario, @AJefeInmediato, @AJefeMediato, @Copia, @Otros, @Asunto, @Cuerpo, @NroDiasRecordatorio, @IdUsuario, @FechaRegistro)"
    SelectCommand="SELECT E.IdEvento, E.NombreEvento, E.Modulo, CD.IdCorreosDestinatarios, CD.Destinatario, CD.AJefeInmediato, CD.AJefeMediato, CD.Copia, CD.Otros, CD.Asunto, CD.Cuerpo, CD.NroDiasRecordatorio, CD.IdUsuario, LU.Usuario, CONVERT(VARCHAR(10), CD.FechaRegistro,103) AS FechaRegistro, E.RequiereFechaCierre
                    FROM Notificaciones.Evento AS E
                    LEFT JOIN Notificaciones.CorreosDestinatarios AS CD ON CD.IdEvento = E.IdEvento
                    LEFT JOIN Listas.Usuarios AS LU ON LU.IdUsuario = CD.IdUsuario
                    WHERE E.Modulo IS NOT NULL
                    ORDER BY E.CodOrdenamiento" UpdateCommand="UPDATE [Notificaciones].[CorreosDestinatarios] 
                    SET [Destinatario] = @Destinatario, 
                        [AJefeInmediato] = @AJefeInmediato,
                        [AJefeMediato] = @AJefeMediato,
                        [Copia] = @Copia, 
                        [Otros]  = @Otros,
                        [Asunto] = @Asunto,
                        [Cuerpo] = @Cuerpo,
                        [NroDiasRecordatorio] = @NroDiasRecordatorio
                    WHERE [IdCorreosDestinatarios] = @IdCorreosDestinatarios">
    <DeleteParameters>
        <asp:Parameter Name="IdPais" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="IdEvento" Type="Int32" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="AJefeInmediato" Type="String" />
        <asp:Parameter Name="AJefeMediato" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
        <asp:Parameter Name="IdUsuario" Type="Int32" />
        <asp:Parameter Name="FechaRegistro" Type="DateTime" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="IdCorreosDestinatarios" Type="Int32" />
        <asp:Parameter Name="Destinatario" Type="String" />
        <asp:Parameter Name="AJefeInmediato" Type="String" />
        <asp:Parameter Name="AJefeMediato" Type="String" />
        <asp:Parameter Name="Copia" Type="String" />
        <asp:Parameter Name="Otros" Type="String" />
        <asp:Parameter Name="Asunto" Type="String" />
        <asp:Parameter Name="Cuerpo" Type="String" />
        <asp:Parameter Name="NroDiasRecordatorio" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SELECT [IdEvento], [NombreEvento] FROM [Notificaciones].[Evento]">
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <asp:Panel ID="pnlDependencia2" runat="server" CssClass="popup" Width="400px" Style="display: none">
            <table width="100%" class="tablaSinBordes">
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
                <tr align="right" bgcolor="#5D7B9D">
                    <td>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/dialog-close2.png"
                            OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TreeView ID="TreeView2" ExpandDepth="3" runat="server" Font-Names="Calibri"
                            Style="overflow: auto" Font-Size="Small" LineImagesFolder="~/TreeLineImages"
                            ForeColor="Black" ShowLines="True" Target="_self" OnSelectedNodeChanged="TreeView2_SelectedNodeChanged"
                            Font-Bold="False" Height="400px">
                            <SelectedNodeStyle BackColor="Silver" BorderColor="#66CCFF" BorderStyle="Inset" />
                        </asp:TreeView>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="Apariencia" OnClientClick="$find('popup1').hidePopup(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlMsgBox" runat="server" Width="400px" CssClass="modalPopup" Style="display: none;">
            <table width="100%">
                <tr class="topHandle">
                    <td colspan="2" align="center" runat="server" id="tdCaption">
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" Font-Bold="False" Font-Names="Tahoma"
                            Font-Size="11px"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-about.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox" runat="server" Font-Bold="False" Font-Names="Tahoma" Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnImgokEliminar" runat="server" Text="Ok" OnClick="btnImgokEliminar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClientClick="$find('mypopup').hide(); return false;" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsgBox" runat="server" PopupControlID="pnlMsgBox"
            BehaviorID="mypopup" Enabled="True" TargetControlID="btndummy" BackgroundCssClass="modalBackground"
            DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy" runat="server" Text="Button" Style="display: none" />
        <asp:Label ID="lblOk" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblCancelar" runat="server" Text="Label" Visible="False"></asp:Label>
        <table align="center" width="100%">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label13" runat="server" ForeColor="White" Text="Correos Destinatarios"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr align="center" bgcolor="#EEEEEE" id="filaGrid" runat="server" visible="true">
                <td bgcolor="#EEEEEE">
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" DataSourceID="SqlDataSource1"
                                    ForeColor="#333333" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True"
                                    HeaderStyle-CssClass="gridViewHeader" DataKeyNames="Usuario,IdEvento,IdCorreosDestinatarios,Destinatario,Copia,Otros,Asunto,Cuerpo,NroDiasRecordatorio,FechaRegistro,AJefeInmediato,AJefeMediato,RequiereFechaCierre"
                                    BorderStyle="Solid" GridLines="Vertical" CssClass="Apariencia" Font-Bold="False"
                                    OnRowCommand="GridView1_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdEvento" HeaderText="Código" InsertVisible="False" ReadOnly="True"
                                            SortExpression="IdEvento" Visible="False">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Modulo" HeaderText="Modulo" InsertVisible="False" ReadOnly="True"
                                            SortExpression="Módulo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NombreEvento" HeaderText="Etapa" InsertVisible="False"
                                            ReadOnly="True" SortExpression="NombreEvento" HtmlEncode="False" HtmlEncodeFormatString="False">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdCorreosDestinatarios" HeaderText="IdCorreosDestinatarios"
                                            SortExpression="IdCorreosDestinatarios" Visible="False" />
                                        <asp:BoundField DataField="Destinatario" HeaderText="Destinatario" SortExpression="Destinatario"
                                            Visible="False" />
                                        <asp:BoundField DataField="Copia" HeaderText="Copia" SortExpression="Copia" Visible="False" />
                                        <asp:BoundField DataField="Otros" HeaderText="Otros" SortExpression="Otros" Visible="False" />
                                        <asp:BoundField DataField="Asunto" HeaderText="Asunto" SortExpression="Asunto" Visible="False" />
                                        <asp:BoundField DataField="Cuerpo" HeaderText="Cuerpo" SortExpression="Cuerpo" Visible="False" />
                                        <asp:BoundField DataField="NroDiasRecordatorio" HeaderText="NroDiasRecordatorio"
                                            SortExpression="NroDiasRecordatorio" Visible="False" />
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                            Visible="False" />
                                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" SortExpression="Usuario"
                                            Visible="False" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Creación" SortExpression="FechaRegistro"
                                            Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AJefeInmediato" HeaderText="AJefeInmediato" SortExpression="AJefeInmediato"
                                            Visible="False" />
                                        <asp:BoundField DataField="AJefeMediato" HeaderText="AJefeMediato" SortExpression="AJefeMediato"
                                            Visible="False" />
                                        <asp:BoundField DataField="RequiereFechaCierre" HeaderText="RequiereFechaCierre"
                                            SortExpression="RequiereFechaCierre" Visible="False" />
                                        <asp:TemplateField ShowHeader="False" HeaderText="Acción">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select"
                                                    ImageUrl="~/Imagenes/Icons/edit.png" Text="Seleccionar" ToolTip="Editar" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                    <td align="center">
                        <asp:ImageButton ID="btnInsertarNuevo" runat="server" CausesValidation="False" CommandName="Insert"
                                    ImageUrl="~/Imagenes/Icons/Add.png" Text="Insert" OnClick="btnInsertarNuevo_Click" ToolTip="Insertar" Visible="false"/>
                    </td>
                </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr align="left" id="filaDetalle" runat="server" visible="false">
                <td bgcolor="#EEEEEE">
                    <table class="tabla" width="100%">
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Modulo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtModulo" runat="server" Enabled="False" Width="100px" CssClass="Apariencia"></asp:TextBox>
                                <asp:DropDownList ID="DDLmodulos" Visible="false" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDLmodulos_SelectedIndexChanged">
                                    <asp:ListItem Text="---" Value="--"></asp:ListItem>
                                    <asp:ListItem Text="AUDITORIA" Value="AUDITORIA"></asp:ListItem>
                                    <asp:ListItem Text="GESTION ESTRATEGICA" Value="GESTION_ESTRATEGICA"></asp:ListItem>
                                    <asp:ListItem Text="PERFILAMIENTO" Value="PERFILAMIENTO"></asp:ListItem>
                                    <asp:ListItem Text="RIESGOS" Value="RIESGOS"></asp:ListItem>
                                    <asp:ListItem Text="SARLAFT" Value="SARLAFT"></asp:ListItem>
                                    <asp:ListItem Text="SEGURIDAD" Value="SEGURIDAD"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblCodModulo" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" visible="false">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="LnombreEvento" runat="server" Text="Evento:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="TXnombreEvento" Width="300px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="LfechaCierre" runat="server" Text="Requiere Fecha de Cierre:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RBLrequiereFechaCierre" runat="server">
                                                <asp:ListItem Value="SI" Text="Si requiere"></asp:ListItem>
                                                <asp:ListItem Value="NO" Text="No requiere"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Etapa:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEtapa" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                                <asp:Label ID="lblIdControl" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblIdCorreosDestino" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label12" runat="server" Text="A:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkJefeInmediato" runat="server" Text="Jefe Inmediato" CssClass="Apariencia"
                                                Font-Bold="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkJefeMediato" runat="server" Text="Jefe Mediato" CssClass="Apariencia"
                                                Font-Bold="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Copia:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td id="Td1" runat="server">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="txtCopia1" runat="server" CssClass="Apariencia" Width="600px"
                                                Height="15px" OnTextChanged="txtCopia1_TextChanged"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="imgCopia1" runat="server" ImageUrl="~/Imagenes/Icons/Email2.png"
                                                OnClientClick="return false;" />
                                            <asp:PopupControlExtender ID="popupCopia1" runat="server" BehaviorID="popup1" DynamicServicePath=""
                                                Enabled="True" ExtenderControlID="" OffsetY="-100" PopupControlID="pnlDependencia2"
                                                Position="Right" TargetControlID="imgCopia1">
                                            </asp:PopupControlExtender>
                                            <asp:ImageButton ID="imgBorrarCopia1" runat="server" ImageUrl="~/Imagenes/Icons/deletemail.png"
                                                OnClick="imgBorrarCopia1_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label10" runat="server" Text="Otros:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOtros" runat="server" Width="602px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label8" runat="server" Text="Asunto:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAsunto" runat="server" Width="602px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label9" runat="server" Text="Cuerpo:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCuerpo" runat="server" Width="800px" CssClass="Apariencia" Columns="100"
                                    Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="filaNroDias" runat="server">
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label11" runat="server" Text="Nro Días Recordatorio:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNroDias" runat="server" Enabled="True" Width="50px" CssClass="Apariencia"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Usuario:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" bgcolor="#BBBBBB">
                                <asp:Label ID="Label5" runat="server" Text="Fecha de Creación:" CssClass="Apariencia"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFecha" runat="server" Width="100px" CssClass="Apariencia" Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                                <table class="tablaSinBordes">
                                    <tr>
                                        <td>
                                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Visible="False" OnClick="btnImgInsertar_Click" ToolTip="Insertar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                Style="text-align: right" OnClick="btnImgActualizar_Click" ToolTip="Actualizar" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
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
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="GridView1" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="TreeView2" EventName="SelectedNodeChanged" />
    </Triggers>
</asp:UpdatePanel>
<%--The fnClick javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
