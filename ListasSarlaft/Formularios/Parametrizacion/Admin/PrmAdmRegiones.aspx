<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmRegiones.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmRegiones" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCR" TagName="Regiones" Src="~/UserControls/Parametrizacion/Regiones.ascx" %>


    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CCR:Regiones ID="Regiones" runat="server" />
</asp:Content>
