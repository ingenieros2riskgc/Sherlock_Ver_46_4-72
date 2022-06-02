<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmMacroproceso.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmMacroproceso" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PMP" TagName="Macroproceso" Src="~/UserControls/Proceso/Param/Macroproceso.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PMP:Macroproceso ID="PMP" runat="server" />
</asp:Content>
