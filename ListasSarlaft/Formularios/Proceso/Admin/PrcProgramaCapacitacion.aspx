<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcProgramaCapacitacion.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcProgramaCapacitacion" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PC" TagName="ProgramaCapacitacion" Src="~/UserControls/Proceso/Procesos/ProgramaCapacitacion.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PC:ProgramaCapacitacion ID="PC" runat="server" />
</asp:Content>