<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmReportePlanAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReportePlanAuditoria" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCRA" TagName="ReporteAuditoria" Src="~/UserControls/MAuditoria/ReportePlanAuditoria.ascx" %>
<%@ PreviousPageType VirtualPath="~/Formularios/Auditoria/Admin/AudAdmPlanAuditoria.aspx" %> 
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA:ReporteAuditoria ID="ReporteAuditoria" runat="server" />
</asp:Content>
