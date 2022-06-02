<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmAsocCaracterizacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmAsocCaracterizacion" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="AC" TagName="Caracterizacion" Src="~/UserControls/Proceso/Param/AsocCaracterizacion.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <AC:Caracterizacion ID="AC" runat="server" />
</asp:Content>
