<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmProceso.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmProceso" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PPR" TagName="Proceso" Src="~/UserControls/Proceso/Param/Proceso.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PPR:Proceso ID="PPR" runat="server" />
</asp:Content>
