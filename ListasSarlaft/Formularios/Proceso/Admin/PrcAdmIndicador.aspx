<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmIndicador.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmIndicador" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PI" TagName="Indicador" Src="~/UserControls/Proceso/Param/Indicador.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PI:Indicador ID="PI" runat="server" />
</asp:Content>
