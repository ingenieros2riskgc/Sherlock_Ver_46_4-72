<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmSalida.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmSalida" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PS" TagName="Salida" Src="~/UserControls/Proceso/Param/Salida.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PS:Salida ID="PS" runat="server" />
</asp:Content>
