<%@ Page Title="Sherlock" Language="C#" MasterPageFile="~/MastersPages/Admin.Master"
    AutoEventWireup="true" CodeBehind="ParRieCausas.aspx.cs" Inherits="ListasSarlaft.Formularios.Riesgos.Admin.ParRieCausas" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCPRC" TagName="Causas" Src="~/UserControls/Riesgos/ParRieCausas.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <CCPRC:Causas ID="Causas" runat="server" />
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>
