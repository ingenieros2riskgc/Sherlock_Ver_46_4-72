<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmActividad.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmActividad" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PA" TagName="Actividad" Src="~/UserControls/Proceso/Param/Actividad.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PA:Actividad ID="PA" runat="server" />
</asp:Content>
