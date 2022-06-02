<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmOficinas.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmOficinas" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCC" TagName="Oficinas" Src="~/UserControls/Parametrizacion/Oficinas.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCC:Oficinas ID="Oficinas" runat="server" />
</asp:Content>
