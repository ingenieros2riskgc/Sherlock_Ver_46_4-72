<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmVerificacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmVerificacion" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCV" TagName="Verificacion" Src="~/UserControls/MAuditoria/Verificacion.ascx" %>
    

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCV:Verificacion ID="Verificacion" runat="server" />
</asp:Content>
