<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="SubServicios.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.SubServicios" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCP" TagName="SubServicioss" Src="~/UserControls/Eventos/SubServicios.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCP:SubServicioss ID="SubServicioss" runat="server"/>
</asp:Content>

