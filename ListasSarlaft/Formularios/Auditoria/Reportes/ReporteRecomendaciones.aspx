<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="ReporteRecomendaciones.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.ReporteRecomendaciones" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCRA2" TagName="ReporteRecomendacioness" Src="~/UserControls/MAuditoria/Reportes/ReporteRecomendaciones.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA2:ReporteRecomendacioness ID="ReporteRecomendacioness" runat="server" />
</asp:Content>

