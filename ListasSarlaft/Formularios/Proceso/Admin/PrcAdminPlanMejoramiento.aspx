<%@ Page Language="C#" MasterPageFile="~/MastersPages/Admin.Master" AutoEventWireup="true" CodeBehind="PrcAdminPlanMejoramiento.aspx.cs" Inherits="ListasSarlaft.Formularios.Proceso.Admin.PrcAdminPlanMejoramiento" %>
<%@ OutputCache Location="None" %>
<%@ Register TagPrefix="PM" TagName="PlanMejoramiento" Src="~/UserControls/Proceso/Procesos/PlanMejoramiento.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <PM:PlanMejoramiento ID="PM" runat="server" />
</asp:Content>