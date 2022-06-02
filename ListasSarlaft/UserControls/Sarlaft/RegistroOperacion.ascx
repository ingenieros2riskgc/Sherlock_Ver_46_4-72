<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroOperacion.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Sarlaft.RegistroOperacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControls/Sitio/OkMessageBox.ascx" TagPrefix="uc" TagName="OkMessageBox" %>
<script type="text/javascript">
    function fnClick(sender, e) {
        __doPostBack(sender, e);
    }
</script>
<style type="text/css">
    .gridViewHeader a:link
    {
        text-decoration: none;
    }

    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }

    .scrollingControlContainer
    {
        overflow-x: hidden;
        overflow-y: scroll;
    }

    .scrollingCheckBoxList
    {
        border: 1px #808080 solid;
        margin: 10px 10px 10px 10px;
        height: 200px;
    }

    .popup
    {
        border: Silver 1px solid;
        color: #060F40;
        background: #ffffff;
    }

    .style1
    {
        height: 19px;
    }
</style>
<script>
    function pageLoad(sender, args) {
        //By JavaScript
        document.getElementsByClassName('ajax__fileupload_selectFileButton')[0].innerHTML = "Seleccione";
        document.getElementsByClassName('ajax__fileupload_uploadbutton')[0].innerHTML = "Cargar";
        document.getElementsByClassName('ajax__fileupload_dropzone')[0].innerHTML = "Descargue archivos aquí";
        document.getElementsByClassName('ajax__fileupload_topFileStatus')[0].innerHTML = "Por favor seleccione archivo(s) a cargar.";
    }
</script>
<asp:SqlDataSource ID="SqlDataSource200" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    DeleteCommand="DELETE FROM [Notificaciones].[CorreosEnviados] WHERE [IdCorreosEnviados] = @IdCorreosEnviados"
    InsertCommand="INSERT INTO [Notificaciones].[CorreosEnviados] ([IdEvento], [Destinatario], [Copia], [Otros], [Asunto], [Cuerpo], [Estado], [IdRegistro], [FechaEnvio], [FechaRegistro], [IdUsuario], [Tipo]) VALUES (@IdEvento, @Destinatario, @Copia, @Otros, @Asunto, @Cuerpo, @Estado, @IdRegistro, @FechaEnvio, @FechaRegistro, @IdUsuario, @Tipo) SET @NewParameter2=SCOPE_IDENTITY();"
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
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <uc:OkMessageBox ID="omb" runat="server" />
        <table align="center" width="700px">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Consulta registro operaciones"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tbConsulta" runat="server" width="100%" bgcolor="#EEEEEE">
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label24" runat="server" Text="Tipo registro" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label26" runat="server" Text="Estado" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label32" runat="server" Text="Factor riesgo" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label33" runat="server" Text="Indicador/Señal alerta" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left">
                            <td>
                                <asp:DropDownList ID="DropDownList3" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="155px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="155px">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="155px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList5_SelectedIndexChanged">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList6" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="155px">
                                    <asp:ListItem Value="---">---</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr runat="server" id="trArea" visible="false">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Área / Sucursal" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="3">

                                <asp:DropDownList ID="DropDownList1" runat="server" Font-Names="Calibri"
                                    Font-Size="Small" DataTextField="NombreArea" DataValueField="IdArea" 
                                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"  AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:Label ID="Label5" runat="server" Text="Código:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                                <asp:TextBox ID="TextBox8" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="50px" AutoPostBack="true" OnTextChanged="TextBox8_TextChanged"></asp:TextBox>

                            </td>
                        </tr>
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label34" runat="server" Text="Identificación" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label35" runat="server" Text="Fecha detección desde" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td colspan="2" bgcolor="#BBBBBB">
                                <asp:Label ID="Label36" runat="server" Text="Fecha detección hasta" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TextBox5" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TextBox6" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="TextBox6"
                                    Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="TextBox7" runat="server" Font-Names="Calibri" Font-Size="Small"
                                    Width="150px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" TargetControlID="TextBox7"
                                    Format="yyyy-MM-dd"></asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr align="left">
                            <td colspan="4">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button5" runat="server" Text="Consultar" ToolTip="Consultar" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button5_Click" Style="height: 25px" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button7" runat="server" Text="Limpiar" ToolTip="Limpiar" Font-Names="Calibri"
                                                Font-Size="Small" OnClick="Button7_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">
                                <table>
                                    <tr align="left">
                                        <td>
                                            <asp:Button ID="Button6" runat="server" Text="Exportar" ToolTip="Exportar" Font-Names="Calibri"
                                                Font-Size="Small" Visible="false" OnClick="Button6_Click" />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                OnRowCommand="GridView3_RowCommand" AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdRegistroOperacion" HeaderText="IdRegistroOperacion"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="UsuarioAsignado" HeaderText="Usuario Asignado" Visible="false" />
                                                    <asp:BoundField DataField="Identificacion" HeaderText="Identificacion" />
                                                    <asp:BoundField DataField="NombreApellido" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="NombreIndicador" HeaderText="Indicador/Señal alerta" />
                                                    <asp:BoundField DataField="Indicador" HeaderText="Indicador" Visible="false" />
                                                    <asp:BoundField DataField="MensajeCorreo" HeaderText="MensajeCorreo" Visible="false" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" Visible="false" />
                                                    <asp:BoundField DataField="FechaDeteccion" HeaderText="Fecha Deteccion" />
                                                    <asp:BoundField DataField="FechaPosibleSolucion" HeaderText="Fecha Posible Solucion"
                                                        Visible="false" />
                                                    <asp:BoundField DataField="RegistrosCargue" HeaderText="RegistrosCargue" Visible="false" />
                                                    <asp:BoundField DataField="RegistrosOperacion" HeaderText="RegistrosOperacion" Visible="false" />
                                                    <asp:BoundField DataField="NombreEstado" HeaderText="Estado" Visible="true" />
                                                    <asp:BoundField DataField="NombreTipoRegistro" HeaderText="Tipo Registro" Visible="true" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/edit.png" Text="Modificar"
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
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbResultadoConsulta" runat="server" visible="false" width="100%" bgcolor="#EEEEEE">
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label6" runat="server" Text="Detalle del registro" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td align="Right" colspan="2">Fecha del Sistema:
                                            <asp:TextBox ID="TxbFechaHoy" runat="server" Width="90px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label11" runat="server" Text="Tipo registro:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label12" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label13" runat="server" Text="Estado:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label14" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label15" runat="server" Text="Usuario asignado:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label16" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label17" runat="server" Text="Identificación:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label18" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label19" runat="server" Text="Nombre y apellido:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label20" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label21" runat="server" Text="Indicador:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label22" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label23" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox3" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                Font-Names="Calibri" Font-Size="Small" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label25" runat="server" Text="Mensaje:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox4" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                Font-Names="Calibri" Font-Size="Small" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label27" runat="server" Text="Fecha de registro:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label28" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label29" runat="server" Text="Fecha detección:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label30" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label31" runat="server" Text="Fecha posible solucion:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="160px" Enabled="False" ReadOnly="True"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="TextBox2"
                                                Format="yyyy-MM-dd"></asp:CalendarExtender>
                                            <asp:CompareValidator ID="CompareValidatorFechaDeteccion" Display="Dynamic" ForeColor="Red" Font-Names="Calibri"
                                    runat="server" ControlToValidate="TextBox2" ControlToCompare="TxbFechaHoy" Font-Size="Small"
                                    ErrorMessage="Fecha Inválida. Fecha posible solución no puede ser inferior a la Fecha de hoy"
                                    Type="Date" Operator="GreaterThanEqual" ValidationGroup="Guardar"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label37" runat="server" Text="Cantidad de registros del cargue:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label38" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label39" runat="server" Text="Cantidad de registros detectados:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label40" runat="server" Text="" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                            ToolTip="Guardar" Visible="false" ValidationGroup="Guardar" OnClick="ImageButton5_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" OnClick="ImageButton1_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB" class="style1">
                                <asp:Label ID="Label7" runat="server" Text="Comentarios" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table>
                                    <tr>
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label10" runat="server" Text="Comentario:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" Height="80px" TextMode="MultiLine" Width="400px"
                                                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                            ToolTip="Cancelar" Visible="false" OnClick="ImageButton4_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                            ToolTip="Agregar" OnClick="ImageButton3_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                                BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                                OnRowCommand="GridView2_RowCommand">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:BoundField DataField="IdComentario" HeaderText="IdComentario" Visible="false" />
                                                    <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                                    <asp:BoundField DataField="ComentarioCorto" HeaderText="Comentario" />
                                                    <asp:BoundField DataField="Comentario" HeaderText="Comentario" Visible="false" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Ver comentario"
                                                        CommandName="Ver" />
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
                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbAsignar" runat="server" visible="false" width="100%" bgcolor="#EEEEEE">
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label3" runat="server" Text="Asignar registro" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr align="left">
                                        <td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label123" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                        </td>
                                        <td bgcolor="#EEEEEE">
                                            <asp:TextBox ID="TextBox34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Width="450px" Enabled="False"></asp:TextBox>
                                            <asp:Label ID="lblIdDependencia1" runat="server" Visible="False" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                            <asp:ImageButton ID="imgDependencia1" runat="server" ImageUrl="~/Imagenes/Icons/Organization-Chart.png"
                                                OnClientClick="return false;" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" ControlToValidate="TextBox34"
                                                ForeColor="Red" ValidationGroup="validateAsignar">*</asp:RequiredFieldValidator>
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
                                        <td>
                                            <asp:Button ID="Button4" runat="server" Text="Asignar" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Asignar" ValidationGroup="validateAsignar" OnClick="Button4_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td>
                    <table id="tbDocumentado" runat="server" visible="false" width="100%" bgcolor="#EEEEEE">
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label2" runat="server" Text="Proceso de estudio y documentación terminado"
                                    Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Documentación finalizada" Font-Names="Calibri"
                                    Font-Size="Small" ToolTip="Documentación finalizada" OnClick="Button3_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr align="center">
                <td align="center">
                    <table id="tbAprobadoRechazado" runat="server" visible="false" width="100%" bgcolor="#EEEEEE">
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label1" runat="server" Text="Aprobar o rechazar el registro" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TrBotonesDefault" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Aprobar" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Aprobar" OnClick="Button1_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button2" runat="server" Text="Rechazar" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Rechazar" OnClick="Button2_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TrBotonesSegurosEstado" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnAutorizado" runat="server" Text="Autorizado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Autorizado" OnClick="BtnAutorizado_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnNegado" runat="server" Text="Negado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Negado" OnClick="BtnNegado_Click"  />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TrBotonesSegurosEstadoAutorizado" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnAutorizadoAmpliado" runat="server" Text="Autorizado-Ampliado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Autorizado-Ampliado" OnClick="BtnAutorizadoAmpliado_Click"  />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnAutorizadoCerrado" runat="server" Text="Autorizado-Cerrado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Autorizado-Cerrado" OnClick="BtnAutorizadoCerrado_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TrBotonesSegurosEstadoAutorizadoAmpliado" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button8" runat="server" Text="Autorizado-Ampliado-Cerrado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Autorizado-Ampliado-Cerrado" OnClick="Button8_Click"  />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button9" runat="server" Text="Autorizado-Ampliado-Ros" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Autorizado-Ampliado-Ros" OnClick="Button9_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TrBotonesSegurosEstadoNegado" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnNegadoAmpliado" runat="server" Text="Negado-Ampliado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Negado-Ampliado" OnClick="BtnNegadoAmpliado_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnNegadoCerrado" runat="server" Text="Negado-Cerrado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Negado-Cerrado" OnClick="BtnNegadoCerrado_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="TrBotonesSegurosEstadoNegadoAmpliado" runat="server" visible="false">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="Button10" runat="server" Text="Negado-Ampliado-Cerrado" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Negado-Ampliado-Cerrado" OnClick="Button10_Click"  />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button11" runat="server" Text="Negado-Ampliado-Ros" Font-Names="Calibri" Font-Size="Small"
                                                ToolTip="Negado-Ampliado-Ros" OnClick="Button11_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td bgcolor="#BBBBBB">
                    <asp:Label ID="Label8" runat="server" Text="Documentos adjuntos" Font-Names="Calibri"
                        Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td align="center">
                                <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" MaximumNumberOfFiles="50" AllowedFileTypes="pdf"
                                    OnUploadComplete="AjaxFileUploadEvent" Font-Names="Calibri" Width="600px" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="VerAdjuntos" runat="server" Text="Ver Adjuntos" OnClick="VerAdjuntos_Click" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="center">
                            <%--<td bgcolor="#BBBBBB">
                                            <asp:Label ID="Label9" runat="server" Text="Adjuntar documento .pdf:" Font-Names="Calibri"
                                                Font-Size="Small"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" />
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Imagenes/Icons/Add.png"
                                                ToolTip="Adjuntar" OnClick="ImageButton2_Click" />
                                        </td>--%>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="Vertical" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="gridViewHeader"
                                    BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small"
                                    OnRowCommand="GridView1_RowCommand">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="IdArchivo" HeaderText="IdArchivo" Visible="false" />
                                        <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre Usuario" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha Registro" />
                                        <asp:BoundField DataField="UrlArchivo" HeaderText="Nombre Archivo" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar"
                                            CommandName="Descargar" />
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
    <Triggers>
        <asp:PostBackTrigger ControlID="Button6" />
        <asp:PostBackTrigger ControlID="GridView1" />
    </Triggers>
</asp:UpdatePanel>
