<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomImageEditorInnerPage.aspx.cs"
    Inherits="Custom_FormControls_ImageCropperTool_AdminControls_CustomImageEditorInnerPage" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Image editor content</title>
</head>
<body class="<%=mBodyClass%> image-editor-inner-page">
    <form id="form1" runat="server">
    <asp:Image ID="imgContent" runat="server" EnableViewState="false" CssClass="image-editor-image" />
    </form>

    <script type="text/javascript" language="javascript">
        //<![CDATA[

        var getCropWidth = <%= QueryHelper.GetInteger("cropWidth", 0) %>;
        var getCropHeight = <%= QueryHelper.GetInteger("cropHeight", 0) %>;
        var getAllowResize = '<%= QueryHelper.GetString("allowResize", "true") %>';

        var jcrop_api = null;
        $cmsj(window).bind('load',
            function () {
                // Initialize crop after image is loaded
                if (window.parent.initializeCrop) {
                    if (getCropWidth > 0 && getCropHeight > 0) {
                        customInitCrop(getCropWidth, getCropHeight, getAllowResize);
                    }
                    else {
                        initCrop();
                    }
                }
            });
        //]]>
    </script>
    <style>
        .dialog-content {
            position: relative !important;
        }
    </style>
</body>
</html>
