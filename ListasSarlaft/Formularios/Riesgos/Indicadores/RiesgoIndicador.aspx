<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MastersPages/Admin.Master" CodeBehind="RiesgoIndicador.aspx.cs" Inherits="ListasSarlaft.Formularios.Riesgos.Indicadores.RiesgoIndicador" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RI" TagName="RiesgoIndicador" Src="~/UserControls/Riesgos/Indicadores/RiesgoIndicador.ascx" %>
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
    <RI:RiesgoIndicador ID="RiesgoIndicadores" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>