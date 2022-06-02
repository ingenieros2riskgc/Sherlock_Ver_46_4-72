<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmAuditoria.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmAuditoria" %>
<%@ OutputCache Location="None" %>

<%@ Register TagPrefix="CCAU" TagName="Auuditoriaa" Src="~/UserControls/MAuditoria/Auuditoriaa.ascx" %>
<%@ PreviousPageType VirtualPath="~/Formularios/Auditoria/Admin/AudAdmPlaneacion.aspx" %> 
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCAU:Auuditoriaa ID="Auuditoriaa" runat="server" />
</asp:Content>
