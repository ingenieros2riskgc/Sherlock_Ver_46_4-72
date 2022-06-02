<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmEncuestas.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmEncuestas" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="Enc" TagName="Encuestas" Src="~/UserControls/Proceso/Param/Encuestas.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <Enc:Encuestas ID="Enc" runat="server" />
</asp:Content>
