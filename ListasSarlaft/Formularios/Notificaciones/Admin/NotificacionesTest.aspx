<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" 
    CodeBehind="NotificacionesTest.aspx.cs" Inherits="ListasSarlaft.Formularios.Notificaciones.NotificacionesTest" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCD" TagName="TestNotificaciones" Src="~/UserControls/Notificaciones/NotificacionesTest.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCD:TestNotificaciones ID="TestNotificaciones" runat="server" />
</asp:Content>
