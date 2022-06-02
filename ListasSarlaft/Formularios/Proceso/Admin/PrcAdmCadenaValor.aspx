<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCadenaValor.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCadenaValor" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PCV" TagName="CadenaValor" Src="~/UserControls/Proceso/Param/CadenaValor.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PCV:CadenaValor ID="PCV" runat="server" />
</asp:Content>
