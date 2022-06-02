<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmReporteAuditoriaAnulada.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmReporteAuditoriaAnulada" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCRS" TagName="ReporteAuditoriaAnulada" Src="~/UserControls/MAuditoria/ReporteAuditoriaAnulada.ascx" %>
   

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRS:ReporteAuditoriaAnulada ID="ReporteAuditoriaAnulada" runat="server" />
</asp:Content>
