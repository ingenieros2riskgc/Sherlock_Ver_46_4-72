<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmGestionAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmGestionAuditoria" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCGA" TagName="GestionAuditoria" Src="~/UserControls/MAuditoria/GestionAuditoria.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCGA:GestionAuditoria ID="GestionAuditoria" runat="server" />
</asp:Content>
