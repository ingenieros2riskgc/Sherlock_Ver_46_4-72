<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcCrlPropiedadClienteProveedor.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcCrlPropiedadClienteProveedor" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CPCP" TagName="CrlPropiedadClienteProveedor" Src="~/UserControls/Proceso/Procesos/CrlPropiedadClienteProveedor.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CPCP:CrlPropiedadClienteProveedor ID="CPCP" runat="server" />
</asp:Content>
