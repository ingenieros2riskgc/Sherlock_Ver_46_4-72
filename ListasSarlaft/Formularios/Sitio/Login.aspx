<%@ Page Title="Login" ValidateRequest="false" Language="C#" MasterPageFile="~/MastersPages/Sitio.Master"
    AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ListasSarlaft.Formularios.Sitio.Login" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CCL" TagName="Login2" Src="~/UserControls/Sitio/Login.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <CCL:Login2 ID="Login2" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="ContentPlaceHolder6" runat="server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="ContentPlaceHolder7" runat="server">
</asp:Content>
