<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmEstandar.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmEstandar" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CEA" TagName="Estandar" Src="~/UserControls/MAuditoria/Estandar.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CEA:Estandar ID="Estandar" runat="server" />
</asp:Content>
