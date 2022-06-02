<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Sitio.Master" AutoEventWireup="true" CodeBehind="AudAdmReporteAuditoriaSeg1.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReporteAuditoriaSeg1" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCRA2" TagName="ReporteAuditoriaSeg2" Src="~/UserControls/MAuditoria/ReporteAuditoriaSeg3.ascx" %>
<%@ PreviousPageType VirtualPath="~/Formularios/Auditoria/Admin/AudAdmSeguimiento.aspx" %> 
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA2:ReporteAuditoriaSeg2 ID="ReporteAuditoriaSeg2" runat="server" />
</asp:Content>