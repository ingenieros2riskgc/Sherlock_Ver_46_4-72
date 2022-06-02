<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmCrlDocument.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmCrlDocument" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="CDV" TagName="ControlDocumentoVersiones" Src="~/UserControls/Proceso/Procesos/ControlDocumentoVersiones.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CDV:ControlDocumentoVersiones ID="CDV" runat="server" />
</asp:Content>