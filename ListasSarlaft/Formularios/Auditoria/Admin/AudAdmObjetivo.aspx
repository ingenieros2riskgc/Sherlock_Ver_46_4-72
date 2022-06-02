<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="AudAdmObjetivo.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.AudAdmObjetivo" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CAO" TagName="Objetivo" Src="~/UserControls/MAuditoria/Objetivo.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CAO:Objetivo ID="Objetivo" runat="server" />
</asp:Content>
