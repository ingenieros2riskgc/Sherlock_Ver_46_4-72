<%@ Page Title="" Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdmMapaProceso.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdmMapaProceso" %>

<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PMP" TagName="MapaProceso" Src="~/UserControls/Proceso/Procesos/ucMapaProcesos.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PMP:MapaProceso ID="PMP" runat="server" />
</asp:Content>
