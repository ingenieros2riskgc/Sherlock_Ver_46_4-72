<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmProcedimiento.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmProcedimiento" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PP" TagName="Procedimiento" Src="~/UserControls/Proceso/Param/Procedimientos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PP:Procedimiento ID="PP" runat="server" />
</asp:Content>
