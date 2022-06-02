<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" 
    CodeBehind="NtfAdmCorreosDestino.aspx.cs" Inherits="ListasSarlaft.Formularios.Notificaciones.NtfAdmCorreosDestino" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCD" TagName="CorreosDestino" Src="~/UserControls/Notificaciones/CorreosDestino.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCD:CorreosDestino ID="CorreosDestino" runat="server" />
</asp:Content>
