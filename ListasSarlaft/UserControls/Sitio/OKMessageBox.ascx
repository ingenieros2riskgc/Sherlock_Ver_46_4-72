<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OKMessageBox.ascx.cs"
    Inherits="ListasSarlaft.UserControls.Sitio.OKMessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    body, input
    {
        font-family: Tahoma;
        font-size: 11px;
    }
    
    .modalBackground
    {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
    }
    
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 1px;
        border-radius: 5px;
        border-style: solid;
        border-color: Gray;
        min-width: 250px;
        max-width: 400px;
    }
    
    .topHandle
    {
        background-color: #5D7B9D;
    }
    
    .table
    {
        padding: 0;
        margin: 0;
    }
</style>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk">
    <table width="100%">
        <tr class="topHandle">
            <td colspan="2" align="center" runat="server" id="tdCaption">
                <asp:Label ID="lblCaption" runat="server" Font-Names="Tahoma" Font-Size="11px" Font-Bold="False"></asp:Label><br />
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagenes/Icons/icontexto-message-types-alert-red.png" />
            </td>
            <td valign="middle" align="left">
                <asp:Label ID="lblMessage" runat="server" Font-Names="Tahoma" Font-Size="11px" Font-Bold="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground"
    DropShadow="true" DynamicServicePath="" Enabled="True" TargetControlID="pnlPopup"
    PopupControlID="pnlPopup">
</asp:ModalPopupExtender>
<%--<ajax:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground"
    TargetControlID="pnlPopup" PopupControlID="pnlPopup">
</ajax:ModalPopupExtender>--%>
<%--The fnClickOk javascript function will force the btnOk button to post back. 
(to actually achieve this we need to add a little piece of code in 
the Page_Load method of the user control.--%>
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
