<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="SubLNegocio.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.SubLNegocio" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCP" TagName="SubLNegocioo" Src="~/UserControls/Eventos/SubLNegocio.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCP:SubLNegocioo ID="SubLNegocioo" runat="server"/>
</asp:Content>

