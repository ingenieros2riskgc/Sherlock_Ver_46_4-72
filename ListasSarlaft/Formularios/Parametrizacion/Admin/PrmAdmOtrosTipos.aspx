<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrmAdmOtrosTipos.aspx.cs" Inherits="ListasSarlaft.UserControls.Parametrizacion.PrmAdmOtrosTipos" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="POT" TagName="OtrosTipos" Src="~/UserControls/Parametrizacion/OtrosTipos.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <POT:OtrosTipos ID="OtrosTipos" runat="server" />
</asp:Content>

