<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmGruposAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmGruposAuditoria" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCGA" TagName="GruposAuditoria" Src="~/UserControls/MAuditoria/GruposAuditoria.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCGA:GruposAuditoria ID="GruposAuditoria" runat="server" />
</asp:Content>
