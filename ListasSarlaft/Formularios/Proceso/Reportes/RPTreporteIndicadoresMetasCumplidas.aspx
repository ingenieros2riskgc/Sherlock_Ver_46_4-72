<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="RPTreporteIndicadoresMetasCumplidas.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Reportes.RPTreporteIndicadoresMetasCumplidas" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="RIMC" TagName="ReporteIndicadoresMetasCumplidas" Src="~/UserControls/Proceso/Reportes/ReporteIndicadoresMetasCumplidas.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <RIMC:ReporteIndicadoresMetasCumplidas ID="RIMC" runat="server" />
</asp:Content>