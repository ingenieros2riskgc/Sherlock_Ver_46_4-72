<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="NtfAdmCorreosEnviados.aspx.cs" Inherits="ListasSarlaft.Formularios.Notificaciones.Admin.NtfAdmCorreosEnviados" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCE" TagName="CorreosEnviados" Src="~/UserControls/Notificaciones/CorreosEnviados.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCE:CorreosEnviados ID="CorreosEnviados" runat="server" />
</asp:Content>
