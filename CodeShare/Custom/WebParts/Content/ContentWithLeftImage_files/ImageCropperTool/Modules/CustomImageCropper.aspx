<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomImageCropper.aspx.cs" Inherits="Custom_FormControls_ImageCropperTool_Modules_CustomImageCropper" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Image editor content</title>
    <meta http-Equiv="Cache-Control" Content="no-cache">
    <meta http-Equiv="Pragma" Content="no-cache">
    <meta http-Equiv="Expires" Content="0">

	<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/bootstrap.css" type="text/css" rel="stylesheet">
	<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/bootstrap-additional.css" type="text/css" rel="stylesheet">
	<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/CMSDesk.css" type="text/css" rel="stylesheet">
	<link href="/CMSPages/GetResource.ashx?stylesheetfile=/App_Themes/Default/DesignMode.css" type="text/css" rel="stylesheet">

    <script src="/custom/formcontrols/imagecroppertool/scripts/jquery.js" type="text/javascript"></script>
    <script src="/custom/formcontrols/imagecroppertool/scripts/jquery.Jcrop.js" type="text/javascript"></script>
    <link href="/custom/formcontrols/imagecroppertool/styles/jquery.Jcrop.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
    	$(function () { //On DOM ready
            //Using the 'each' pattern allows multiple cropping image/link pairs per page.
            $('.image-cropper').each(function(unusedIndex, container) {
                container = $(container); //We were passed a DOM reference, convert it to a jquery object

                //Find the image inside 'container' by class ("image")
                var image = container.find("img.image");

                //Trim the querystring off the image URL.
                var path = image.attr('src'); if (path.indexOf('?') > 0) path = path.substr(0, path.indexOf('?'));

                //Define a function to execute when the cropping rectangle changes.
                var update = function (coords) {
                    if (parseInt(coords.w) <= 0 || parseInt(coords.h) <= 0) return; //Require valid width and height

                    //Build the URL based on the coordiantes. The resizing module will handle everything else.
                    var url = path + '?crop=(' + coords.x + ',' + coords.y + ',' + coords.x2 + ',' + coords.y2 +
                    ')&cropxunits=' + image.width() + '&cropyunits=' + image.height()

                    //Now, update the link 'href' (you could update a hidden field just as easily)
                    // container.find('a.result').attr('href', url);
                    container.find('.hdnResult').val(url);
                }

                //Start up jCrop on the image, specifying our function be called when the selection rectangle changes,
                // and that a 60% black shadow should cover the cropped regions.
                image.Jcrop({ 
                    onChange: update, 
                    onSelect: update, 
                    bgColor: 'black', 
                    bgOpacity: 0.6,
                    setSelect: [0, 0, <%= QueryHelper.GetInteger("cropWidth", 0) %>, <%= QueryHelper.GetInteger("cropHeight", 0) %>],
                    aspectRatio: <%= QueryHelper.GetInteger("cropWidth", 0) %> / <%= QueryHelper.GetInteger("cropHeight", 0) %>,
                    allowSelect: false
                });
            });
        });
    </script>
</head>
<body class="<%=mBodyClass%> image-editor-inner-page">
    <form id="form1" runat="server">
		<div class="ui-layout-pane ui-layout-pane-north" style="position:fixed; width:100%; z-index: 999;">
			<div class="dialog-header non-selectable">
				<div class="dialog-header-action-buttons">
					<div class="action-button">
						<div style="margin-right: 20px;">
							<asp:Button ID="btnCrop" CssClass="btn btn-primary" runat="server" Text="Crop" OnClick="btnCrop_Click" />
						</div>
					</div>
					<div class="action-button close-button">
						<a onclick="window.close();">
							<span class="sr-only">Close</span>
							<i class="icon-modal-close cms-icon-150"></i>
						</a>
					</div>
				</div>
				<h2 class="dialog-header-title">Crop Image</h2>
			</div>
		</div>

		<div style="padding-top: 50px;">
			<p style="color: yellowgreen"><asp:Literal ID="litMessage" runat="server" /></p>
			<div class="image-cropper">
				<asp:Image ID="imgContent" runat="server" EnableViewState="false" CssClass="image" />
				<input type="hidden" id="hdnResult" runat="server" class="hdnResult" />
				<div style="display: none;">
					<input type="button" id="btnClose" onclick="window.close();" value="Close" />
				</div>
			</div>
		</div>
    </form>
</body>
</html>
