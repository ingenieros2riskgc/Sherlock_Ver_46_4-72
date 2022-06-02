<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmCiclo.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmCiclo" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CIC" TagName="Ciclo" Src="~/UserControls/MAuditoria/Ciclo.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CIC:Ciclo ID="Ciclo" runat="server" />
</asp:Content>
