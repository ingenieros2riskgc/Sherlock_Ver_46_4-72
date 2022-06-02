<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcCrlInfraestructura.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcCrlInfraestructura" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CI" TagName="ControlInfraestructura" Src="~/UserControls/Proceso/Procesos/ControlInfraestructura.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CI:ControlInfraestructura ID="CI" runat="server" />
</asp:Content>
