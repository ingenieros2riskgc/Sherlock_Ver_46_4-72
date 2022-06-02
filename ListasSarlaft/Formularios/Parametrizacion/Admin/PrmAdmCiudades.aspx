<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmCiudades.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmCiudades" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCC" TagName="Ciudades" Src="~/UserControls/Parametrizacion/Ciudades.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCC:Ciudades ID="Ciudades" runat="server" />
</asp:Content>
