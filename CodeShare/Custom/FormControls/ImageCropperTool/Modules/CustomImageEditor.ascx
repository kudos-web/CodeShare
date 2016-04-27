<%@ Control Language="C#" AutoEventWireup="true" Inherits="Custom_FormControls_ImageCropperTool_Modules_ImageEditor_Control" CodeFile="CustomImageEditor.ascx.cs" %>
<%@ Register Src="~/Custom/FormControls/ImageCropperTool/AdminControls/CustomBaseImageEditor.ascx" TagName="BaseImageEditor" TagPrefix="cms" %>
<asp:PlaceHolder ID="plcContent" runat="server">
    <cms:BaseImageEditor ID="baseImageEditor" runat="server" />
</asp:PlaceHolder>
