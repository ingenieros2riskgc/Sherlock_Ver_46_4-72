<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmTipoEstandar.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmTipoEstandar" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CTE" TagName="Ciclo" Src="~/UserControls/MAuditoria/TipoEstandar.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CTE:Ciclo ID="TipoEstandar" runat="server" />
</asp:Content>

