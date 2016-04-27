<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomDirectUploadControl.ascx.cs" Inherits="Custom_FormControls_ImageCropperTool_CustomDirectUploadControl" %>

<asp:Literal ID="litError" runat="server" />

<asp:PlaceHolder ID="plcFileUpload" runat="server">
	<asp:FileUpload ID="customFileUpload" runat="server" />
</asp:PlaceHolder>
<asp:PlaceHolder ID="plcImageEdit" runat="server">
    <div id="ictWrapper" runat="server" class="editing-form-value-cell" style="width: 100%">
	    <div class="EditingFormControlNestedControl editing-form-control-nested-control">
		    <div>
			    <div id="pnlSelector" class="MediaSelector">
				    <div class="MediaSelectorHeader">
                        <asp:TextBox ID="txtPath" runat="server" CssClass="EditingFormMediaPathTextBox form-control" ReadOnly="true" />
                        <asp:HiddenField ID="hdnImageGuid" runat="server" />
					    <!-- <button type="submit" name="btnSelect" runat="server" value="Edit" id="btnSelect" class="EditingFormMediaPathButton btn btn-default">Edit</button> -->

                        <asp:HyperLink ID="lnkEdit" runat="server" CssClass="EditingFormMediaPathButton btn btn-default" Target="_blank">Edit</asp:HyperLink>

                        <asp:Button ID="btnClear" runat="server" CssClass="EditingFormMediaPathClearButton btn btn-default" Text="Clear" OnClick="btnClear_Click" />
				    </div>
			    </div>
		    </div>
	    </div>
    </div>
    <asp:HiddenField ID="hdnItemToColorize" runat="server" />
</asp:PlaceHolder>

