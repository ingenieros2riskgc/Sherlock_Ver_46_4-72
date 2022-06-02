<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Sitio.Master" AutoEventWireup="true" CodeBehind="ReporteConsolidado.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.ReporteConsolidado" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="ReporteConsolidadoo" Src="~/UserControls/Eventos/ReporteConsolidado.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:ReporteConsolidadoo ID="ReporteConsolidadoo" runat="server" />
</asp:Content>
