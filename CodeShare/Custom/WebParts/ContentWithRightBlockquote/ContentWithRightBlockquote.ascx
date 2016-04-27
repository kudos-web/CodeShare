<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentWithRightBlockquote.ascx.cs" Inherits="CMSWebParts_Custom_ContentWithRightBlockquote_ContentWithRightBlockquote" %>

<asp:PlaceHolder ID="plcContentWithRightBlockquote" runat="server">
	<div class="editor-content editor-content--right-quote">
		<div class="editor-content__quote">
			<blockquote><%= Blockquote %></blockquote>
		</div>
		<div class="editor-content__text">
			<p><%= ContentText %></p>
		</div>	
	</div>
</asp:PlaceHolder>