<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Sitio.Master" AutoEventWireup="true" CodeBehind="AudAdmReporteSeguimiento.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReporteSeguimiento" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCRS" TagName="ReporteSeguimiento" Src="~/UserControls/MAuditoria/ReporteSeguimiento.ascx" %>
<%@ PreviousPageType VirtualPath="~/Formularios/Auditoria/Admin/AudAdmSeguimiento.aspx" %> 
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRS:ReporteSeguimiento ID="ReporteSeguimiento" runat="server" />
</asp:Content>
