<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmCargos.aspx.cs" Inherits="ListasSarlaft.Formularios.Parametrizacion.Admin.PrmAdmCargos" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CPC" TagName="Cargos" Src="~/UserControls/Parametrizacion/Cargos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CPC:Cargos ID="Cargos" runat="server"/>
</asp:Content>
