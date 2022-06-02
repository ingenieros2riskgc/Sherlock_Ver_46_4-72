<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="SubClases.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.SubClases" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCP" TagName="SubClasess" Src="~/UserControls/Eventos/SubClase.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCP:SubClasess ID="SubClasess" runat="server"/>
</asp:Content>

