<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuCMI.ascx.cs" Inherits="ListasSarlaft.UserControls.Gestion.MenuCMI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>
<style type="text/css">
    #TbMenu
    {
        width: 193px;
        margin-right: 23px;
        height: 243px;
    }

    #Background
    {
        position: fixed;
        top: 0px;
        bottom: 0px;
        left: 0px;
        right: 0px;
        overflow: hidden;
        padding: 0;
        margin: 0;
        background: #EEEEEE;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 100000;
    }

    #Progress
    {
        position: fixed;
        top: 40%;
        left: 40%;
        height: 10%;
        width: 20%;
        z-index: 100001;
        background-color: #FFFFFF;
        border: 1px solid Gray;
        background-image: url('./Imagenes/Icons/loading.gif');
        background-repeat: no-repeat;
        background-position: center;
    }
</style>

<asp:SqlDataSource ID="SqlOverView11" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView11" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView12" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView12" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView13" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView13" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView14" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView14" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView15" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView15" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView16" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView16" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView21" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView21" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView22" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView22" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView23" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView23" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView24" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView24" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView25" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView25" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView26" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView26" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView31" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView31" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView32" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView32" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView33" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView33" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView34" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView34" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView35" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView35" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView36" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView36" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView41" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView41" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView42" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView42" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView43" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView43" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView44" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView44" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView45" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView45" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlOverView46" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
    SelectCommand="SP_OverView" SelectCommandType="StoredProcedure">
    <SelectParameters>
        <asp:ControlParameter ControlID="LabelOverView46" DefaultValue="0" Name="IdObjeto"
            PropertyName="Text" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table id="TbCMI" runat="server">
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
                <td valign="top">
                    <table id="CMI_Menu" width="200px">
                        <tr>
                            <td valign="top">
                                <table id="TbMenu" runat="server" bgcolor="#D7EBFF">
                                    <tr>
                                        <td>
                                            <br />
                                            <asp:Label ID="Label7" runat="server" ForeColor="#000066" Text="Cuadro de Mando Integral"
                                                Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnMapa" runat="server" BackColor="#5D7B9D" Text="Mapa Estratégico"
                                                Style="color: #D2D3D5" Width="160px" Font-Names="Calibri" Font-Size="Medium"
                                                OnClick="BtnMapa_Click" Height="30px" />
                                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnOBJ" runat="server" BackColor="#5D7B9D" Text="Objetivos Estratégicos"
                                                Style="color: #D2D3D5" Width="160px" Font-Names="Calibri" Font-Size="Medium"
                                                Height="30px" OnClick="BtnOBJ_Click" />
                                            <asp:Image ID="Image9" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnEST" runat="server" BackColor="#5D7B9D" Text="Estrategias" Style="color: #D2D3D5"
                                                Width="160px" Font-Names="Calibri" Font-Size="Medium" Height="30px" OnClick="BtnEST_Click" />
                                            <asp:Image ID="Image10" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnPA" runat="server" BackColor="#5D7B9D" Text="Planes de Acción"
                                                Style="color: #D2D3D5" Width="160px" Font-Names="Calibri" Font-Size="Medium"
                                                Height="30px" OnClick="BtnPA_Click" />
                                            <asp:Image ID="Image11" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnINDG" runat="server" BackColor="#5D7B9D" Text="Indicadores - Global"
                                                Style="color: #D2D3D5" Width="160px" Font-Names="Calibri" Font-Size="Medium"
                                                Height="30px" OnClick="BtnINDG_Click" />
                                            <asp:Image ID="Image12" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnINDD" runat="server" BackColor="#5D7B9D" Text="Indicadores - Detalle"
                                                Style="color: #D2D3D5" Width="160px" Font-Names="Calibri" Font-Size="Medium"
                                                Height="30px" OnClick="BtnINDD_Click" />
                                            <asp:Image ID="Image13" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnRES" runat="server" BackColor="#5D7B9D" Text="Responsables" Style="color: #D2D3D5"
                                                Width="160px" Font-Names="Calibri" Font-Size="Medium" Height="30px" OnClick="BtnRES_Click" />
                                            <asp:Image ID="Image14" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnOV" runat="server" BackColor="#5D7B9D" Text="OverView" Style="color: #D2D3D5"
                                                Width="160px" Font-Names="Calibri" Font-Size="Medium" Height="30px" OnClick="BtnOV_Click" />
                                            <asp:Image ID="Image15" runat="server" ImageUrl="~/Imagenes/Icons/select.png" Width="20px"
                                                Visible="false" />
                                        </td>
                                    </tr>
                                    <tr id="LbFecha" runat="server">
                                        <td align="center">
                                            <br />
                                            <hr />
                                            <asp:Label ID="Label65" runat="server" ForeColor="#000066" Text="Fecha" Font-Names="Calibri"
                                                Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="TrFecha" runat="server">
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownListFechaMes" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="25px" Width="100px" AutoPostBack="True">
                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                <asp:ListItem Value="9">Septiembre</asp:ListItem>
                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DropDownListFechaAno" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Height="25px" Width="60px" AutoPostBack="True">
                                                <asp:ListItem Value="2010">2010</asp:ListItem>
                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                                <asp:ListItem Value="2019">2019</asp:ListItem>
                                                <asp:ListItem Value="2020">2020</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td>
                                            <br />
                                            <hr />
                                            <asp:Label ID="Label1" runat="server" ForeColor="#000066" Text="Cumplimiento" Font-Names="Calibri"
                                                Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbCumplimiento" runat="server" align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                            Width="20px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="small"
                                                            Style="font-size: small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:TextBox ID="TextBox7" runat="server" TextMode="MultiLine" Font-Size="Small"
                                                            Font-Names="Calibri" ForeColor="#000066" BorderColor="#5D7B9D" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="95%" Visible="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox7"
                                                            Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Imagenes/Aplicacion/Igual.png"
                                                            Width="20px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label3" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="small"
                                                            Style="font-size: small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:TextBox ID="TextBox8" runat="server" TextMode="MultiLine" Font-Size="Small"
                                                            Font-Names="Calibri" ForeColor="#000066" BorderColor="#5D7B9D" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="95%" Visible="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox8"
                                                            Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Imagenes/Aplicacion/Abajo.png"
                                                            Width="20px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="small"
                                                            Style="font-size: small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="left">
                                                        <asp:TextBox ID="TextBox9" runat="server" TextMode="MultiLine" Font-Size="Small"
                                                            Font-Names="Calibri" ForeColor="#000066" BorderColor="#5D7B9D" BorderStyle="Solid"
                                                            BorderWidth="1px" Width="95%" Visible="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox9"
                                                            Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="Image19" runat="server" ImageUrl="~/Imagenes/Aplicacion/SinInfo.png"
                                                            Width="25px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label60" runat="server" ForeColor="#000066" Text="A la fecha no existen indicadores asociados"
                                                            Font-Names="Calibri" Font-Size="small" Style="font-size: small"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="Button2" runat="server" BackColor="#5D7B9D" Text="Parametrizar Colores"
                                                            ForeColor="White" Width="180px" Font-Names="Calibri" Font-Size="Small" Height="30px"
                                                            OnClick="Button2_Click" Font-Bold="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <table id="TbParColores" runat="server" visible="false" width="100%">
                                                            <tr bgcolor="#5D7B9D" align="center">
                                                                <td width="25%">
                                                                    <asp:Label ID="Label30" runat="server" Text="Color" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:Label ID="Label31" runat="server" Text="Mínimo" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:Label ID="Label32" runat="server" Text="Máximo" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td width="25%">
                                                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Imagenes/Aplicacion/Abajo.png"
                                                                        Width="20px" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox1" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBox1"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox2" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox2"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td width="25%">
                                                                    <asp:Image ID="Image6" runat="server" ImageUrl="~/Imagenes/Aplicacion/Igual.png"
                                                                        Width="20px" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox3" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBox3"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox4" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBox4"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td width="25%">
                                                                    <asp:Image ID="Image7" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="20px" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox5" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBox5"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:TextBox ID="TextBox6" runat="server" Width="40px" Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBox6"
                                                                        Display="Dynamic" ForeColor="Red" InitialValue="" ValidationGroup="color">*</asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" width="25%" align="center">
                                                                    <table align="center">
                                                                        <tr>
                                                                            <td align="center">
                                                                                <asp:ImageButton ID="BtnGuardarColor" runat="server" ImageUrl="~/Imagenes/Icons/guardar.png"
                                                                                    ToolTip="Guardar" OnClick="BtnGuardarColor_Click" ValidationGroup="color" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:ImageButton ID="BtnCancelaColor" runat="server" ImageUrl="~/Imagenes/Icons/cancel.png"
                                                                                    ToolTip="Cancelar" OnClick="BtnCancelaColor_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="right">
                                                                    <br />
                                                                    <br />
                                                                    <asp:Label ID="Label46" runat="server" Font-Names="Calibri" Text="Camilo Aponte"
                                                                        ForeColor="#C0C0C0" Font-Size="Small"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </caption>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table id="CMI_Contenido">
                        <tr>
                            <td>
                                <table id="TbDetalle" runat="server">
                                    <tr>
                                        <td>
                                            <table id="TbFiltroPE" runat="server" visible="false">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Plan Estratégico:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListPE" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Height="25px" Width="430px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListPE_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>&nbsp&nbsp&nbsp
                                                        <asp:Label ID="Label12" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Comienzo:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="LabelIni" runat="server" Width="80px" Font-Names="Calibri" Font-Size="Small"
                                                            Text="12/04/2012" Style="text-align: center"></asp:Label>
                                                    </td>
                                                    <td>&nbsp&nbsp&nbsp
                                                        <asp:Label ID="Label13" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Fin:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: center">
                                                        <asp:Label ID="LabelFin" runat="server" Width="80px" Font-Names="Calibri" Font-Size="Small"
                                                            Text="14/09/2012" Style="text-align: center"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr id="Trobj" runat="server">
                                                    <td>
                                                        <asp:Label ID="Label14" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Objetivo Estratégico:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListOBJ" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                            Height="25px" Width="430px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOBJ_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="TrEstrategias" runat="server">
                                                    <td>
                                                        <asp:Label ID="Label61" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Estrategia:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListEstrategas" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Height="25px" Width="430px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListEstrategas_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="TrResponsables" runat="server">
                                                    <td>
                                                        <asp:Label ID="Label33" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Responsable:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListResponsables" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Height="25px" Width="430px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListResponsables_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr id="TrIndicadorDetallado" runat="server">
                                                    <td>
                                                        <asp:Label ID="Label48" runat="server" Font-Names="Calibri" Font-Size="Small" Text="Indicador:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DropDownListIndicador" runat="server" Font-Names="Calibri"
                                                            Font-Size="Small" Height="25px" Width="430px" AutoPostBack="True" OnSelectedIndexChanged="DropDownListIndicador_SelectedIndexChanged">
                                                            <asp:ListItem Value="---">---</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Image ID="Image17" runat="server" ImageUrl="~/Imagenes/Icons/inf.png" Visible="false"
                                                            Width="20px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbInfoReporte" runat="server" width="1000px" visible="false">
                                                <tr>
                                                    <td colspan="4">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr align="center">
                                                    <td width="25%">
                                                        <asp:Label ID="Label5" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Label ID="Label6" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Label ID="Label9" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Label ID="Label10" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr border="1">
                                                    <td align="center">
                                                        <asp:Panel ID="pnlMain1" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Panel ID="pnlMain2" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Panel ID="pnlMain3" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                    <td align="center">
                                                        <asp:Panel ID="pnlMain4" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbObjetivos" runat="server" visible="false">
                                                <tr>
                                                    <td colspan="4">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td>
                                                        <asp:Label ID="Label15" runat="server" Text="Objetivo Estratégico" ForeColor="White"
                                                            Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label16" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label17" runat="server" Text="Perspectiva" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label18" runat="server" Text="Objetivo(s) Causa" ForeColor="White"
                                                            Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td width="40%">
                                                        <asp:Label ID="Label26" runat="server" Text="" ForeColor="#000066" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td align="center" width="4%">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" Height="22px" />&nbsp
                                                                    <asp:Image ID="Image4Hoy" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" Height="22px" />&nbsp
                                                                    <asp:Image ID="Image4Con" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" Height="22px" />&nbsp
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:Label ID="Label24" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="50%">
                                                        <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSourceObjetivoCausa">
                                                            <AlternatingItemTemplate>
                                                                <li style="">
                                                                    <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>'
                                                                        Font-Names="Calibri" Font-Size="Medium" ForeColor="#000066" />
                                                                    <br />
                                                                </li>
                                                            </AlternatingItemTemplate>
                                                            <EditItemTemplate>
                                                                <li style="">
                                                                    <asp:TextBox ID="DescripcionTextBox" runat="server" Text='<%# Bind("Descripcion") %>' />
                                                                    <br />
                                                                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Actualizar" />
                                                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancelar" />
                                                                </li>
                                                            </EditItemTemplate>
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="LabelSinDatos" runat="server" ForeColor="#FFFFFF" Font-Names="Calibri"
                                                                    Font-Size="Medium" Text="Sin Objetivos Causa"></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <InsertItemTemplate>
                                                                <li style="">
                                                                    <asp:TextBox ID="DescripcionTextBox" runat="server" Text='<%# Bind("Descripcion") %>'
                                                                        Font-Names="Calibri" Font-Size="Medium" ForeColor="#000066" />
                                                                    <br />
                                                                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insertar" />
                                                                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Borrar" />
                                                                </li>
                                                            </InsertItemTemplate>
                                                            <ItemSeparatorTemplate>
                                                                <br />
                                                            </ItemSeparatorTemplate>
                                                            <ItemTemplate>
                                                                <li style="">
                                                                    <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>'
                                                                        Font-Names="Calibri" Font-Size="Medium" ForeColor="#000066" />
                                                                    <br />
                                                                </li>
                                                            </ItemTemplate>
                                                            <LayoutTemplate>
                                                                <ul id="itemPlaceholderContainer" runat="server" style="">
                                                                    <li runat="server" id="itemPlaceholder" />
                                                                </ul>
                                                                <div style="">
                                                                </div>
                                                            </LayoutTemplate>
                                                            <SelectedItemTemplate>
                                                                <li style="">
                                                                    <asp:Label ID="DescripcionLabel" runat="server" Text='<%# Eval("Descripcion") %>'
                                                                        Font-Names="Calibri" Font-Size="Medium" ForeColor="#000066" />
                                                                    <br />
                                                                </li>
                                                            </SelectedItemTemplate>
                                                        </asp:ListView>
                                                        <asp:SqlDataSource ID="SqlDataSourceObjetivoCausa" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                                                            SelectCommand="SP_ObjetivoCausa" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="DropDownListOBJ" DefaultValue="1" Name="IdObjetivoEfecto"
                                                                    PropertyName="SelectedValue" Type="String" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <br />
                                                        <hr />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td colspan="2">
                                                        <asp:Label ID="Label21" runat="server" Text="Estrategias" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Large"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td>
                                                        <asp:Label ID="Label19" runat="server" Text="Descripción" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label20" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td colspan="2">
                                                        <asp:Panel ID="PanelEstrategia" runat="server">
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <hr />
                                                        <asp:Button ID="Button1" runat="server" BackColor="#5D7B9D" Font-Names="Calibri"
                                                            Font-Size="Medium" Height="30px" OnClick="Button1_Click" Style="color: #D2D3D5"
                                                            Text="Mapa Estratégico" Width="160px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbEstrategia" visible="false" width="100%" runat="server">
                                                <tr>
                                                    <td colspan="5">
                                                        <table id="checkperspectiva" width="100%" runat="server">
                                                            <tr>
                                                                <td colspan="4">
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            <tr align="center">
                                                                <td width="25%">
                                                                    <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                                                        ForeColor="#000066" OnCheckedChanged="RadioButton1_CheckedChanged" GroupName="Perspectiva"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                                                        ForeColor="#000066" OnCheckedChanged="RadioButton2_CheckedChanged" GroupName="Perspectiva"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                                                        ForeColor="#000066" OnCheckedChanged="RadioButton3_CheckedChanged" GroupName="Perspectiva"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                                <td width="25%">
                                                                    <asp:RadioButton ID="RadioButton4" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                                                        ForeColor="#000066" OnCheckedChanged="RadioButton4_CheckedChanged" GroupName="Perspectiva"
                                                                        AutoPostBack="True" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td width="35%">
                                                        <asp:Label ID="Label22" runat="server" Text="Objetivo Estratégico" ForeColor="White"
                                                            Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label25" runat="server" Text="Comienzo" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label27" runat="server" Text="Fin" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="35%">
                                                        <asp:Label ID="Label28" runat="server" Text="Estrategia" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label29" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td colspan="5">
                                                        <asp:Panel ID="PanelObjetivos" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        <hr />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbResponsables" runat="server" visible="false" width="100%">
                                                <tr>
                                                    <td colspan="6" width="100%">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td width="31%">
                                                        <asp:Label ID="Label35" runat="server" Text="Objetivo Estratégico" ForeColor="White"
                                                            Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="32%">
                                                        <asp:Label ID="Label36" runat="server" Text="Plan de Acción" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label37" runat="server" Text="Comienzo" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label38" runat="server" Text="Fin" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="10%">
                                                        <asp:Label ID="Label39" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="7%">
                                                        <asp:Label ID="Label40" runat="server" Text="Estado" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td width="100%" colspan="6">
                                                        <asp:Panel ID="PanelResponsables" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6">
                                                        <hr />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbPlanesdeAccion" runat="server" visible="false" width="100%">
                                                <tr>
                                                    <td width="100%">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="100%">
                                                        <br />
                                                        <asp:GridView ID="GridViewPlanAccion" runat="server" AutoGenerateColumns="False"
                                                            BorderStyle="None" CellPadding="9" Font-Names="Calibri" Font-Size="Small" ForeColor="DarkBlue"
                                                            GridLines="None" HeaderStyle-CssClass="gridViewHeader" OnRowCommand="GridViewPlanAccion_RowCommand"
                                                            ShowHeaderWhenEmpty="True" BackColor="#D7EBFF" BorderWidth="1px" CellSpacing="1">
                                                            <Columns>
                                                                <asp:BoundField DataField="IdPlanAccion" HeaderText="Id" Visible="false" />
                                                                <asp:BoundField DataField="Descripcion" HeaderText="Plan de Acción" />
                                                                <asp:BoundField DataField="FechaInicio" HeaderText="Comienzo" />
                                                                <asp:BoundField HeaderText="Fin" DataField="FechaFin" />
                                                                <asp:BoundField HeaderText="Estado" DataField="Abierto_SN" />
                                                                <asp:BoundField HeaderText="Responsable" DataField="Responsable" />
                                                                <asp:ButtonField ButtonType="Image" CommandName="Indicador" HeaderText="Indicadores"
                                                                    ImageUrl="~/Imagenes/Icons/select.png" Text="Indicador">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:ButtonField>
                                                                <asp:ButtonField ButtonType="Image" CommandName="Gestion" HeaderText="Gestiones"
                                                                    ImageUrl="~/Imagenes/Icons/select.png" Text="Gestiones">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:ButtonField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#5D7B9D" ForeColor="DarkBlue" />
                                                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" ForeColor="White" Font-Names="Calibri"
                                                                Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" />
                                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                            <SelectedRowStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#5D7B9D" />
                                                            <SortedAscendingHeaderStyle BackColor="#5D7B9D" />
                                                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                            <SortedDescendingHeaderStyle BackColor="#242121" />
                                                        </asp:GridView>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="TbIndicadoPlanAccion" runat="server" visible="false" width="70%">
                                                            <tr bgcolor="#5D7B9D" id="TrTitulo0" runat="server">
                                                                <td colspan="3">
                                                                    <asp:Label ID="Label67" runat="server" ForeColor="White" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                                </td>
                                                                <td align="center">
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Image ID="Image18" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                                    Width="25px" />
                                                                                <asp:Image ID="Image18Hoy" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                                    Width="25px" />
                                                                                <asp:Image ID="Image18Con" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                                    Width="25px" />
                                                                            </td>
                                                                            <%--<td>
                                                                    <asp:Label ID="Label63" runat="server" ForeColor="White" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                                </td>--%>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr id="TrTitulo1" align="center" bgcolor="#5D7B9D" runat="server">
                                                                <td width="30%">
                                                                    <asp:Label ID="Label57" runat="server" Text="Indicador" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Medium"></asp:Label>
                                                                </td>
                                                                <td width="15%">
                                                                    <asp:Label ID="Label58" runat="server" Text="Periodicidad" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Medium"></asp:Label>
                                                                </td>
                                                                <td width="5%">
                                                                    <asp:Label ID="Label59" runat="server" Text="Meta" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Medium"></asp:Label>
                                                                </td>
                                                                <td width="10%">
                                                                    <asp:Label ID="Label62" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                                        Font-Size="Medium"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr id="TrTitulo2" bgcolor="#D7EBFF" runat="server">
                                                                <td width="100%" colspan="4">
                                                                    <asp:Panel ID="PanelIndicadorPlanAccion" runat="server">
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                            <tr id="Gestiones" runat="server" visible="false">
                                                                <td width="100%" colspan="4">
                                                                    <asp:GridView ID="GridViewGestion" runat="server" AutoGenerateColumns="False" BackColor="#D7EBFF"
                                                                        BorderStyle="None" BorderWidth="1px" CellPadding="9" CellSpacing="1" Font-Names="Calibri"
                                                                        Font-Size="Small" ForeColor="DarkBlue" GridLines="None" HeaderStyle-CssClass="gridViewHeader"
                                                                        ShowHeaderWhenEmpty="True">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="IdPlanAccion" HeaderText="Id" Visible="false" />
                                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripción Gestión" />
                                                                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                                        </Columns>
                                                                        <FooterStyle BackColor="#5D7B9D" ForeColor="DarkBlue" />
                                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="False" Font-Names="Calibri" Font-Overline="False"
                                                                            Font-Size="Small" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                                        <SelectedRowStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                                        <SortedAscendingCellStyle BackColor="#5D7B9D" />
                                                                        <SortedAscendingHeaderStyle BackColor="#5D7B9D" />
                                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center">
                                                        <asp:Button ID="Button3" runat="server" BackColor="#5D7B9D" Font-Names="Calibri"
                                                            Font-Size="Medium" Height="30px" OnClick="Button3_Click" Style="color: #D2D3D5"
                                                            Text="Objetivos Estratégicos" Width="160px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbIndicadorGlobal" runat="server" visible="false" width="80%">
                                                <tr>
                                                    <td colspan="3" width="100%">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td width="40%">
                                                        <asp:Label ID="Label34" runat="server" Text="Plan de Acción" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                        <asp:Label ID="Label41" runat="server" Text="Indicador" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="20%">
                                                        <asp:Label ID="Label42" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <%--<td width="10%">
                                                        <asp:Label ID="Label43" runat="server" Text="Meta" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>--%>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td width="100%" colspan="3">
                                                        <asp:Panel ID="PanelIndicadorGlobal" runat="server" Width="100%">
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TbIndicadorDetalle" runat="server" visible="false" width="100%">
                                                <tr>
                                                    <td colspan="4" width="100%">
                                                        <hr />
                                                    </td>
                                                </tr>
                                                <tr align="center" bgcolor="#5D7B9D">
                                                    <td width="30%">
                                                        <asp:Label ID="Label11" runat="server" Text="Objetivo Estratégico" ForeColor="White"
                                                            Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="23%">
                                                        <asp:Label ID="Label44" runat="server" Text="Perspectiva" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                        <asp:Label ID="Label45" runat="server" Text="Plan Accion" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="12%">
                                                        <asp:Label ID="Label47" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr bgcolor="#D7EBFF">
                                                    <td width="30%">
                                                        <asp:Label ID="Label49" runat="server" Text="" ForeColor="#000066" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="23%">
                                                        <asp:Label ID="Label50" runat="server" Text="" ForeColor="#000066" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="30%">
                                                        <asp:Label ID="Label51" runat="server" Text="" ForeColor="#000066" Font-Names="Calibri"
                                                            Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td width="12%" align="center">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Image ID="Image16" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" />&nbsp
                                                                    <asp:Image ID="Image16Hoy" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" />&nbsp
                                                                    <asp:Image ID="Image16Con" runat="server" ImageUrl="~/Imagenes/Aplicacion/Arriba.png"
                                                                        Width="25px" />&nbsp
                                                                </td>
                                                                <%--<td>
                                                        <asp:Label ID="Label53" runat="server" ForeColor="#000066" Font-Names="Calibri" Font-Size="Medium"></asp:Label>
                                                    </td>--%>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <table id="TbGrafico" width="100%" runat="server">
                                                            <tr>
                                                                <td width="50%" align="center">
                                                                    <table id="TbTablaGrafico">
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <br />
                                                                                <hr />
                                                                            </td>
                                                                        </tr>
                                                                        <tr align="center" bgcolor="#000066">
                                                                            <td width="30%">
                                                                                <asp:Label ID="Label54" runat="server" Text="Periodo" ForeColor="White" Font-Names="Calibri"
                                                                                    Font-Size="Small" Style="font-weight: 700"></asp:Label>
                                                                            </td>
                                                                            <td width="10%">
                                                                                <asp:Label ID="Label55" runat="server" Text="Resultado" ForeColor="White" Font-Names="Calibri"
                                                                                    Font-Size="Small" Style="font-weight: 700"></asp:Label>
                                                                            </td>
                                                                            <td width="10%">
                                                                                <asp:Label ID="Label56" runat="server" Text="Cumplimiento" ForeColor="White" Font-Names="Calibri"
                                                                                    Font-Size="Small" Style="font-weight: 700"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" colspan="3">
                                                                                <asp:Panel ID="PanelTablaIndicadores" runat="server">
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td id="Td1" align="center" width="50%" runat="server">
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <asp:Chart ID="Chart2" runat="server" BorderlineColor="DarkBlue" DataSourceID="SqlDataSource1"
                                                                        Height="276px" Palette="None" Width="408px" BackColor="215, 235, 255" BorderlineDashStyle="Solid">
                                                                        <Series>
                                                                            <asp:Series BackGradientStyle="VerticalCenter" BackSecondaryColor="Silver" ChartType="StackedColumn"
                                                                                Color="0, 0, 64" Legend="Legend1" Name="Meta" XValueMember="Periodo" YValueMembers="Meta"
                                                                                LegendToolTip="Meta: #VAL\nPeriodo: #VALX" ToolTip="Meta:#VAL\nPeriodo:#VALX"
                                                                                Font="Calibri, 8.25pt">
                                                                            </asp:Series>
                                                                            <asp:Series BackImageAlignment="Top" BorderColor="White" BorderWidth="2" ChartArea="ChartArea1"
                                                                                ChartType="Spline" Color="RoyalBlue" IsValueShownAsLabel="True" LabelToolTip="Resultado: #VAL"
                                                                                Legend="Legend1" Name="Resultado" XValueMember="Periodo" YValueMembers="Resultado"
                                                                                Font="Calibri, 3.75pt" LabelBackColor="0, 0, 64" LabelBorderColor="0, 0, 64"
                                                                                LabelBorderDashStyle="NotSet" LabelBorderWidth="0" LabelForeColor="0, 0, 64">
                                                                                <SmartLabelStyle AllowOutsidePlotArea="Yes" CalloutBackColor="Maroon" />
                                                                            </asp:Series>
                                                                        </Series>
                                                                        <ChartAreas>
                                                                            <asp:ChartArea AlignmentOrientation="All" AlignmentStyle="None" BackColor="Transparent"
                                                                                BorderColor="Transparent" BorderWidth="5" Name="ChartArea1" ShadowColor="Transparent">
                                                                                <AxisY IsLabelAutoFit="False" LineColor="Gray">
                                                                                    <MajorGrid Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Months" LineColor="Silver" />
                                                                                    <LabelStyle Font="Calibri, 8.25pt" ForeColor="Gray" />
                                                                                    <ScaleBreakStyle LineColor="Transparent" />
                                                                                </AxisY>
                                                                                <AxisX TitleFont="Calibri, 8.25pt" TitleForeColor="Transparent" LineColor="Gray">
                                                                                    <MajorGrid Enabled="False" Interval="Auto" IntervalOffset="Auto" IntervalOffsetType="Auto"
                                                                                        IntervalType="Auto" />
                                                                                    <MinorGrid LineColor="Transparent" />
                                                                                    <MajorTickMark LineColor="Transparent" />
                                                                                    <MinorTickMark LineColor="Maroon" />
                                                                                    <LabelStyle Font="Calibri, 8.25pt" ForeColor="Gray" Interval="Auto" IntervalOffset="Auto"
                                                                                        IntervalOffsetType="Auto" IntervalType="Auto" IsStaggered="True" />
                                                                                    <ScaleBreakStyle BreakLineStyle="None" LineColor="Transparent" />
                                                                                </AxisX>
                                                                                <AxisX2>
                                                                                    <LabelStyle ForeColor="Transparent" />
                                                                                </AxisX2>
                                                                                <AxisY2 LineColor="White">
                                                                                    <MajorGrid LineColor="Transparent" />
                                                                                    <MajorTickMark Enabled="False" />
                                                                                </AxisY2>
                                                                                <Area3DStyle IsRightAngleAxes="False" LightStyle="None" />
                                                                            </asp:ChartArea>
                                                                        </ChartAreas>
                                                                        <Legends>
                                                                            <asp:Legend Alignment="Center" BackColor="Transparent" Docking="Top" Font="Calibri, 9.75pt"
                                                                                InterlacedRows="True" IsTextAutoFit="False" MaximumAutoSize="100" Name="Legend1">
                                                                            </asp:Legend>
                                                                        </Legends>
                                                                        <BorderSkin BackColor="Maroon" BorderColor="Maroon" />
                                                                    </asp:Chart>
                                                                    <br />
                                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:SarlaftConnectionString %>"
                                                                        SelectCommand="SP_ComportamientoIndicador" SelectCommandType="StoredProcedure">
                                                                        <SelectParameters>
                                                                            <asp:ControlParameter ControlID="DropDownListIndicador" DefaultValue="1" Name="IdIndicador"
                                                                                PropertyName="SelectedValue" Type="Int32" />
                                                                        </SelectParameters>
                                                                    </asp:SqlDataSource>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <%--<tr>
                            <td>
                                <asp:Chart ID="Chart1" runat="server" BackColor="LightGray" DataSourceID="SqlDataSource2"
                                    Height="120px" Width="190px">
                                    <Series>
                                        <asp:Series Name="Series1" ChartType="Spline" XValueMember="Periodo" YValueMembers="Cumplimiento"
                                            BorderWidth="3" Color="Gray">
                                        </asp:Series>
                                    </Series>
                                    <ChartAreas>
                                        <asp:ChartArea Name="ChartArea1" BackColor="LightGray">
                                            <AxisY LineColor="Transparent">
                                                <MajorGrid LineColor="Transparent" />
                                                <LabelStyle Enabled="False" />
                                            </AxisY>
                                            <AxisX LineColor="Transparent">
                                                <MajorGrid LineColor="Transparent" />
                                                <LabelStyle Enabled="False" />
                                            </AxisX>
                                        </asp:ChartArea>
                                    </ChartAreas>
                                </asp:Chart>
                            </td>
                        </tr>--%>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LabelOverView11" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView12" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView13" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView14" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView15" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView16" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView21" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView22" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView23" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView24" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView25" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView26" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView31" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView32" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView33" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView34" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView35" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView36" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView41" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView42" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView43" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView44" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView45" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
                                            <asp:Label ID="LabelOverView46" runat="server" Font-Names="Calibri" Font-Size="Small"
                                                Visible="False"></asp:Label>
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
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="tdCaption">&nbsp;
                        <asp:Label ID="lblCaption" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-webdev-ok.png" />
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
        <asp:ModalPopupExtender ID="mpeMsgBox1" runat="server" TargetControlID="btndummy1"
            PopupControlID="pnlMsgBox1" OkControlID="btnAceptar1" BackgroundCssClass="FondoAplicacion"
            Enabled="True" DropShadow="true">
        </asp:ModalPopupExtender>
        <asp:Button ID="btndummy1" runat="server" Text="Button1" Style="display: none" />
        <asp:Panel ID="pnlMsgBox1" runat="server" Width="400px" Style="display: none;" BorderColor="#575757"
            BorderWidth="1px" BackColor="White" BorderStyle="Solid">
            <table width="100%">
                <tr class="topHandle" style="background-color: #333399">
                    <td colspan="2" align="center" runat="server" id="td2">&nbsp;
                        <asp:Label ID="Label23" runat="server" Text="Atención" ForeColor="White" Font-Names="Calibri"
                            Font-Size="Small"></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 60px" valign="middle" align="center">
                        <asp:Image ID="imgInfo1" runat="server" ImageUrl="~/Imagenes/Icons/Alerta.png" />
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblMsgBox1" runat="server" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="2">
                        <asp:Button ID="btnAceptar1" runat="server" Text="Ok" Font-Names="Calibri" Font-Size="Small" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
