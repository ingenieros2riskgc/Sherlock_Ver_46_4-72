<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="Parametrizacion.aspx.cs" Inherits="ListasSarlaft.UserControls.Gestion.Parametrizacion" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="POT" TagName="Parametrizacionn" Src="~/UserControls/Gestion/ParametrizacionOT.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <POT:Parametrizacionn ID="Parametrizacionn" runat="server" />
</asp:Content>

