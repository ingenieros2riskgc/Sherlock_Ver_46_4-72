<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdminRegistroNoConformidad.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdminRegistroNoConformidad" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RNC" TagName="RegistroNoConformidad" Src="~/UserControls/Proceso/Procesos/RegistroNoConformidad.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RNC:RegistroNoConformidad ID="RNC" runat="server" />
</asp:Content>
