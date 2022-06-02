<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmPaises.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmPaises" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCP" TagName="Paises" Src="~/UserControls/Parametrizacion/Paises.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCP:Paises ID="Paises" runat="server"/>
</asp:Content>

