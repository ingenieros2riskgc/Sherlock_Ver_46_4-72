<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GestionRequerimientos.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Calidad.GestionRequerimientos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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

    .no-visible {
        display: none
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
         <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label61" runat="server" ForeColor="White" Text="Detalles del requerimiento"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>

        <br />
        <table align="center">
        <tr align="center">
            <td style="margin-left: 40px">
                <asp:GridView ID="gvGesReq" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnPageIndexChanging="gvGesReq_PageIndexChanging" OnRowCommand="gvGesReq_RowCommand" 
                    ShowHeaderWhenEmpty="True" BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="idREGREQ" HeaderText="Id Requerimiento" Visible="True" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="Empresa" HeaderText="Empresa" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="NumeroREQ" HeaderText="Número Requerimiento" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="FechaCreacionREGREQ" HeaderText="Fecha Registro" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundField>
                        <asp:BoundField DataField="TipoFalla" HeaderText="Tipo de Fallo" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundField>
                        <asp:BoundField DataField="DetallesTipoFalla" HeaderText="Detalle tipo de fallo" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundField>
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundField>
                        <asp:BoundField DataField="Ruta" HeaderText="Ruta del error" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundField>
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/select.png" Text="Detalles" HeaderText="Detalles"
                            CommandName="Detalles" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
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
        <br />
        <div id="divDatosGesReq" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblId" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Id del requerimiento:" Width="300px" Visible="true"></asp:Label>
                    <asp:Label ID="StrId" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px" Visible="true"></asp:Label>
                    <br />
                </td>
            </tr>
         
        
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblEmpresa" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Empresa:" Width="300px"></asp:Label>
                    <asp:Label ID="StrEmpresa" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblPersona" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Usuario:" Width="300px"></asp:Label>
                    <asp:Label ID="StrNombre" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
        
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblRequerimiento" runat="server" BackColor="Silver" CssClass="Apariencia" Text="N° Requerimiento:" Width="300px"></asp:Label>
                    <asp:Label ID="StrNumReq" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblTipoDeFalla" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Tipo de falla:" Width="300px"></asp:Label>
                    <asp:Label ID="StrTipoFalla" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
         

            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblDetalleFalla" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Detalle del tipo de falla: " Width="300px"></asp:Label>
                    <asp:Label ID="StrDetalleTipoFalla" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblFechaRegistro" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Fecha de registro: " Width="300px"></asp:Label>
                    <asp:Label ID="StrFechaRegistro" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblDescripcion" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Descripción:" Width="300px"></asp:Label>
                    <asp:Label ID="StrDescripcion" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="RowsText">
                    <asp:Label ID="lblRutaError" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Ruta del error:" Width="300px"></asp:Label>
                    <asp:Label ID="StrRutaError" runat="server" BackColor="#ffffff" CssClass="Apariencia" Width="300px"></asp:Label>
                </td>
            </tr>
         </table>
        </div>
        <br />
        <div id="div3" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label3" runat="server" ForeColor="White" Text="Evidencias específicas"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <table align="center">
        <tr>
            <td>
                <asp:GridView ID="gvEvidencias" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnRowCommand ="gvEvidencias_RowCommand" AutoPostBack="True"
                ShowHeaderWhenEmpty="True" BorderStyle="Solid" HeaderStyle-CssClass="gridViewHeader"
                HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="IdEvidencia" HeaderText="IdEvidencia" SortExpression="IdArchivo"
                            InsertVisible="False" ReadOnly="True" Visible="False" />
                        <asp:BoundField DataField="URLArchivo" HeaderText="Archivo" SortExpression="urlArchivo" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                            HtmlEncode="False" HtmlEncodeFormatString="False" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Carga" SortExpression="FechaRegistro">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                            Visible="False" />
                        <asp:ButtonField ButtonType="Image" ImageUrl="~/Imagenes/Icons/downloads.png" HeaderText="Descarga" 
                            Text="Descargar" CommandName="Descargar" ItemStyle-HorizontalAlign="Center" />

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
        </table>
       <br />
        <div id="div1" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Asignación de responsable"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <br />
        <table align="center">
        <tr align="center">
            <td style="margin-left: 40px">
                <asp:GridView ID="gvUpdData" runat="server" CellPadding="4" EnableModelValidation="True" 
                    ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                    OnPageIndexChanging="gvUpdData_PageIndexChanging" OnRowCommand="gvUpdData_RowCommand" 
                    ShowHeaderWhenEmpty="True" BorderStyle="Solid" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="idGESREQ" HeaderText="Id Requerimiento" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="NumeroREQ" HeaderText="Número Requerimiento" Visible="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField DataField="GrupoAsignado" HeaderText="GrupoAsignado" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Encargado" HeaderText="Encargado" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Center" ></asp:BoundField>
                        <asp:BoundField DataField="Criticidad" HeaderText="Criticidad" ItemStyle-HorizontalAlign="Center" Visible="true" ></asp:BoundField>
                        <asp:BoundField DataField="FechaVencimientoGESREQ" HeaderText="Fecha Vencimiento" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundField>
                        <asp:BoundField DataField="Comentario" HeaderText="Comentario" ItemStyle-HorizontalAlign="Center" Visible="true"></asp:BoundField>

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
        <br />
        <div id="divCrear" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="lblCrear" runat="server" ForeColor="White" Text="Crear control"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <div id="divActualizar" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
            </tr>
        </table>
        </div>
        <br />







        <div id="divDdlOptions" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
            <tr align="center">
                <td align="center" class="RowsText">
                    <asp:Label ID="GrupoAsignado" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Grupo asignado:" Width="300px"></asp:Label>
                    <asp:DropDownList ID="ddlGrupoAsignado" runat="server" CssClass="Apariencia" Width="300px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlGrupoAsignado_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr align="center">
                <td align="center" class="RowsText">
                    <asp:Label ID="Label6" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Encargado:" Width="300px"></asp:Label>
                    <asp:DropDownList ID="ddlEncargado" runat="server" CssClass="Apariencia" Width="300px"
                        OnSelectedIndexChanged="ddlEncargado_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr align="center">
                <td align="center" class="RowsText">
                    <asp:Label ID="Label8" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Estado:" Width="300px"></asp:Label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="Apariencia" Width="300px"
                        OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                        <asp:ListItem Value="1">Abierto</asp:ListItem>
                        <asp:ListItem Value="2">Asignado</asp:ListItem>
                        <asp:ListItem Value="3">En desarollo</asp:ListItem>
                        <asp:ListItem Value="4">En catalogación</asp:ListItem>
                        <asp:ListItem Value="5">En pruebas</asp:ListItem>
                        <asp:ListItem Value="6">Devuelto</asp:ListItem>
                        <asp:ListItem Value="7">Certificado</asp:ListItem>
                        <asp:ListItem Value="8">En producción</asp:ListItem>
                        <asp:ListItem Value="9">Cerrado</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr align="center">
                <td align="center" class="RowsText">
                    <asp:Label ID="Label10" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Criticidad:" Width="300px"></asp:Label>
                    <asp:DropDownList ID="ddlCriticidad" runat="server" CssClass="Apariencia" Width="300px"
                        OnSelectedIndexChanged="ddlCriticidad_SelectedIndexChanged">
                        <asp:ListItem Value="0">--Seleccione una opción--</asp:ListItem>
                        <asp:ListItem Value="1">Baja</asp:ListItem>
                        <asp:ListItem Value="2">Media</asp:ListItem>
                        <asp:ListItem Value="3">Alta</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
         </table>
        <br />
        <table align="center" >
            <tr align="center">
                <td align="center" class="RowsText" width="200px">
                    <asp:Label ID="lblFechaVencimiento" runat="server" BackColor="Silver" CssClass="Apariencia" Text="Fecha Vencimiento:" Width="295px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="StrFechaVencimiento" runat="server" MaxLength="25" Font-Names="Calibri"
                                    Font-Size="Small" Width="295px" ></asp:TextBox>
                    <asp:CalendarExtender ID="StrFechaVencimiento_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                        Enabled="True" TargetControlID="StrFechaVencimiento" ></asp:CalendarExtender>
                </td>
            </tr>
         </table>
            <table align="center" >
            <tr class="visible">
                     <td align="left" bgcolor="#BBBBBB">
                         <asp:Label ID="Comentario" runat="server" Text="Comentario:" CssClass="Apariencia" Width="120px" align="center"></asp:Label>
                     </td>
                     <td>
                         <asp:TextBox ID="StrComentario" runat="server" Width="650px" CssClass="Apariencia" Height="80px"
                             Columns="100" Rows="8" TextMode="MultiLine" Font-Size="10pt" Font-Bold="False" MaxLength="500"></asp:TextBox>
                     </td>
                 </tr> 
         </table>
      </div>







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
                         <%--<td>
                            <asp:ImageButton ID="btnImgActualizar" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                OnClick="btnImgActualizar_Click" ToolTip="Actualizar" ValidationGroup="iPerfil" />
                         </td>--%>
                         <td>
                            <asp:ImageButton ID="btnImgCancelar" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                OnClick="btnImgCancelar_Click" ToolTip="Cancelar" />
                         </td>
                     </tr>
                 </table>
             </td>
        </tr>
        </table>
        <br />
        <div id="div2" runat="server" visible="false" class="ColumnStyle">
        <table align="center" bgcolor="#EEEEEE">
            <tr align="center" bgcolor="#333399">
                <td>
                    <asp:Label ID="Label2" runat="server" ForeColor="White" Text="Comentarios"
                        Font-Bold="False" Font-Names="Calibri" Font-Size="Large" Width="400px"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <br />

        <table align="center">
            <tr>
                <td>
                    <asp:GridView ID="gvComentarios" runat="server" CellPadding="4" EnableModelValidation="True" 
                        ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="False"
                        OnRowCommand="gvComentarios_RowCommand" 
                        ShowHeaderWhenEmpty="True" BorderStyle="Solid"
                        HorizontalAlign="Center" Font-Names="Calibri" Font-Size="Small" >
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="IdComentario" HeaderText="IdEvidencia" SortExpression="IdArchivo"
                            InsertVisible="False" ReadOnly="True" Visible="False" />
                        <asp:BoundField DataField="URLArchivo" HeaderText="Archivo" SortExpression="urlArchivo" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" SortExpression="Descripcion"
                            HtmlEncode="False" HtmlEncodeFormatString="False" />
                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Carga" SortExpression="FechaRegistro">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IdUsuario" HeaderText="IdUsuario" SortExpression="IdUsuario"
                                Visible="False" />
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
        </table>
        <br />
       
    <div id="divComentarios" runat="server" visible="false" class="ColumnStyle">
        <table align="center">
            <tr runat="server" id="filaSubirAnexos" align="center" visible="true">
                <td>
                    <br />
                    <table class="tabla">
                        <tr align="left">
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="LblNombreAdjunto" runat="server" Text="Adjuntar documento:" Font-Names="Calibri"
                                    Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="Calibri" Font-Size="Small" Width="400px" />
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#BBBBBB">
                                <asp:Label ID="Label4" runat="server" Text="Descripción:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescArchivo" runat="server" Width="400" MaxLength="100"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="2">
                    <table class="tablaSinBordes">
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
                            </td>
                        </tr>
                    </table>
                    
                </td>
            </tr>
        </table>
    </div>

        <br />






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
