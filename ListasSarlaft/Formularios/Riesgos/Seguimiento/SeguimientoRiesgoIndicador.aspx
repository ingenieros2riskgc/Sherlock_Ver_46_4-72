<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MastersPages/Admin.Master" CodeBehind="SeguimientoRiesgoIndicador.aspx.cs" Inherits="ListasSarlaft.Formularios.Riesgos.Seguimiento.SeguimientoRiesgoIndicador" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="SRI" TagName="SeguimientoRiesgoIndicador" Src="~/UserControls/Riesgos/Seguimiento/SeguimientoRiesgoIndicador.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <SRI:SeguimientoRiesgoIndicador ID="SeguimientoRiesgoIndicadores" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>