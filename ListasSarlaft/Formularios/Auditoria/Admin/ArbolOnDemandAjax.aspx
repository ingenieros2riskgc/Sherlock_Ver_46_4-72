<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArbolOnDemandAjax.aspx.cs" Inherits="ListasSarlaft.Formularios.Auditoria.Admin.ArbolOnDemandAjax" %>

<%@ Register assembly="Obout.Ajax.UI" namespace="Obout.Ajax.UI.TreeView" tagprefix="obout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>TreeView AJAX</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%= DateTime.Now.ToString("T") %>
        <hr />
        <asp:TreeView id="TreeView1" ExpandDepth="0" 
            OnTreeNodePopulate="TreeView1_TreeNodePopulate" Runat="server" 
            Font-Names="Calibri" Font-Size="Small" 
            LineImagesFolder="~/TreeLineImages" ForeColor="Black" >
            <SelectedNodeStyle BackColor="#FFFF99" BorderColor="#66CCFF" 
                BorderStyle="Inset" />
        </asp:TreeView>
    </div>
    </form>
</body>
</html>

