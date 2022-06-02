<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="GeneradorEvento.aspx.cs" Inherits="ListasSarlaft.Formularios.Eventos.GeneradorEvento" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="GeneradorEventoo" Src="~/UserControls/Eventos/GeneradorEvento.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:GeneradorEventoo ID="GeneradorEventoo" runat="server" />
</asp:Content>
