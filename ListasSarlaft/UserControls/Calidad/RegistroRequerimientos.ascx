<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistroRequerimientos.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Calidad.RegistroRequerimientos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .ajax__html_editor_extender_texteditor {
        background-color: White;
        font-size: small;
        font-weight: lighter;
        font-family: Calibri;
        font-style: normal;
    }
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
    .no-visible {
        display: none
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Nuevo requerimiento"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" >
            <tr>
                <td align="center" class="RowsText" width="200px">
                    <asp:Label ID="Fecha" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Fecha:" Width="150px"></asp:Label>
                    <asp:Label ID="StrFechaRegistro" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="150px"></asp:Label>
                </td>
            </tr>
         </table>
        <br />
<div id="divGvGesReq" runat="server" visible="false" class="ColumnStyle">
    <table align="center">
        <tr align="center" >
            <td style="margin-left: 40px" >
                <asp:GridView ID="gvGesReq" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnPageIndexChanging="gvGesReq_PageIndexChanging" OnRowCommand="gvGesReq_RowCommand" 
                    ShowHeaderWhenEmpty="True" BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="StrId" HeaderText="IdRequerimiento" Visible="False"></asp:BoundField>
                        <asp:BoundField DataField="StrEmpresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrNombre" HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrNumReq" HeaderText="Número Requerimiento" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="StrFechaRegistro" HeaderText="Fecha Registro" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundField>
                        <asp:BoundField DataField="StrTipoFalla" HeaderText="Tipo de Fallo" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="DDLText" HeaderText="Detalle tipo de fallo" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
                        <asp:BoundField DataField="StrDescripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="StrRutaError" HeaderText="Ruta del error" ItemStyle-HorizontalAlign="Center" Visible="false" ></asp:BoundField>
                    </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="White" Font-Bold="True" ForeColor="White"></FooterStyle>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" CssClass="gridViewHeader"></HeaderStyle>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left"></RowStyle>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                        <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                        <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                        <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
        <br />
        <table align="center" >
            <tr >
                <td align="center" class="RowsText" >
                    <asp:Label ID="LblIdReq" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Id:" Width="300px" Visible="false"></asp:Label>
                    <asp:Label ID="StrId" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px" Visible="false"></asp:Label>
                </td>
            </tr>
         </table>
        <br />
        <table align="center">
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="Persona" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Usuario:" Width="300px"></asp:Label>
                    <asp:Label ID="StrNombre" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
         </table>
        <table align="center">
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="Requerimiento" runat="server" BackColor="Silver" CssClass="Apariencia" Text="N° Requerimiento:" Width="300px"></asp:Label>
                    <asp:Label ID="StrNumReq" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
         </table>
        <table align="center" >
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="Empresa" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Empresa:" Width="300px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="StrEmpresa" runat="server" Width="300px" CssClass="Apariencia"></asp:TextBox>
                </td>
            </tr>
            <caption>
                <br />
            </caption>
         </table>
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Tipo de requerimiento"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" runat="server">
            <tr>
                <td class="RowsText">
                    <asp:DropDownList ID="DDLopcionesRequerimientos" runat="server" Width="300px" Font-Names="Calibri"
                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DDLopcionesRequerimientos_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                        <asp:ListItem Value="1">Incidencia externa</asp:ListItem>
                        <asp:ListItem Value="2">Incidencia interna</asp:ListItem>
                        <asp:ListItem Value="3">Opción de mejora</asp:ListItem>
                        <asp:ListItem Value="4">Orden de trabajo</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
            <tr>
                <td align="center" class="RowsText">
                    <asp:DropDownList ID="DDLopcionesIncInt" runat="server" Width="300px" Font-Names="Calibri"
                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DDLopcionesIncInt_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                        <asp:ListItem Value="1">Falla de software</asp:ListItem>
                        <asp:ListItem Value="2">Falla de hardware</asp:ListItem>
                        <asp:ListItem Value="3">Otro...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <table align="center">
            <tr>
                <td>
                    <asp:DropDownList ID="DDLopcionesIncExt" runat="server" Width="300px" Font-Names="Calibri"
                    Font-Size="Small" AutoPostBack="True" OnSelectedIndexChanged="DDLopcionesIncExt_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                        <asp:ListItem Value="1">Falla en Sherlock</asp:ListItem>
                        <asp:ListItem Value="2">Falla en el servidor</asp:ListItem>
                        <asp:ListItem Value="3">Inconveniente con la base de datos</asp:ListItem>
                        <asp:ListItem Value="4">Otro...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Detalles del requerimiento"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table align="center">
         <tr class="visible">
                     <td align="left" bgcolor="#BBBBBB">
                         <asp:Label ID="Descripcion" runat="server" Text="Descripción:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="StrDescripcion" runat="server" Width="800px" CssClass="Apariencia" Height="80px"
                             Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                     </td>
                 </tr> 
         </table>
        <table align="center">
         <tr class="visible">
                     <td align="left" bgcolor="#BBBBBB">
                         <asp:Label ID="Error" runat="server" Text="Ruta del error:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="StrRutaError" runat="server" Width="800px" CssClass="Apariencia" Height="20px"
                             Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                     </td>
                 </tr> 
         </table>
        <br />
        <table align="center">
        <tr align="center">
             <td>
                 <table class="tablaSinBordes">
                     <tr>
                         <td>
                            <asp:ImageButton ID="btnImgInsertar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                OnClick="btnImgInsertar_Click" ToolTip="Guardar" ValidationGroup="iPerfil" />
                         </td>
                         <td>
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                         </td>
                     </tr>
                 </table>
             </td>
        </tr>
            <caption>
                <br />
                </table>
        <br />









        <div id="divTitle" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE" >
                <tr align="center" bgcolor="#333399">
                    
                    <td>
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="White" Text="Evidencia" Width="400px"></asp:Label>
                    </td>
                    <caption>
                        <br />
                        <tr align="center">
                            <td>
                                <br />
                                <asp:GridView ID="gvEvidencias" runat="server" AllowPaging="False" AutoGenerateColumns="False" BorderStyle="solid" CellPadding="4" EnableModelValidation="True" Font-Names="Calibri" Font-Size="Small" ForeColor="#333333" GridLines="Vertical" HorizontalAlign="Center" OnRowCommand="gvEvidencias_RowCommand" ShowHeaderWhenEmpty="true">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="StrIdEvidencia" HeaderText="IdEvidencia" InsertVisible="False" ReadOnly="True" SortExpression="IdArchivo" Visible="False" />
                                        <asp:BoundField DataField="urlArchivo" HeaderText="Archivo" SortExpression="urlArchivo" />
                                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HtmlEncode="False" HtmlEncodeFormatString="False" SortExpression="Descripcion" />
                                        <asp:BoundField DataField="StrFechaRegistroEvidencia" HeaderText="Fecha de Carga" SortExpression="FechaRegistro">
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario" Visible="False" />
                                        <%--<asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" Text="Descargar" CommandName="Descargar" />--%>
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
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <caption>
                            <br />
                            </table>
        </div>
       <br />

        <div id="divCargaPDF" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
                            <tr id="filaSubirAnexos" runat="server" align="center" visible="true">
                                <td>
                                    <br />
                                    <table class="tabla">
                                        <tr align="left">
                                            <td bgcolor="#BBBBBB">
                                                <asp:Label ID="LblNombreAdjunto" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Adjuntar documento:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" Width="400px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#BBBBBB">
                                                <asp:Label ID="LblDescripcion" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Descripción:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescArchivo" runat="server" MaxLength="100" Width="400"></asp:TextBox>
                                                &nbsp; </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </caption>
                    </caption>
                </tr>
            </caption>
        </table>
            </div>

        <div id="divBotones" runat="server" visible="false" class="ColumnStyle">
                    <table class="tablaSinBordes" align ="center">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                            <td>
                                <asp:ImageButton ID="BtnAdjuntar" runat="server" ImageUrl="~/Imagenes/Icons/uploads_folder (1).png"
                                    ToolTip="Adjuntar" OnClick="imgBtnAdjuntar_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnImgCancelarArchivo" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                    ToolTip="Cancelar" OnClick="btnImgCancelarArchivo_Click" />
                            </td>
                            </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID = "BtnAdjuntar" />
                                </Triggers>
                        </asp:UpdatePanel>
                    </table>
            </div>








        
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