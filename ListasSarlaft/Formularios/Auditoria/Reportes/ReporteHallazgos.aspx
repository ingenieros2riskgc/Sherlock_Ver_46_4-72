<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="ReporteHallazgos.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Reportes.ReporteHallazgos" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCRA2" TagName="ReporteHallazgoss" Src="~/UserControls/MAuditoria/Reportes/ReporteHallazgos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCRA2:ReporteHallazgoss ID="ReporteHallazgoss" runat="server" />
</asp:Content>

