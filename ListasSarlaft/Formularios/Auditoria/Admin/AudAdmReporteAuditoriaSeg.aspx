<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Sitio.Master" AutoEventWireup="true" CodeBehind="AudAdmReporteAuditoriaSeg.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReporteAuditoriaSeg" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCRA2" TagName="ReporteAuditoriaSeg2" Src="~/UserControls/MAuditoria/ReporteAuditoriaSeg2.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA2:ReporteAuditoriaSeg2 ID="ReporteAuditoriaSeg2" runat="server" />
</asp:Content>
