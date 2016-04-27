<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentWithLeftBlockquote.ascx.cs" Inherits="CMSWebParts_Custom_ContentWithLeftBlockquote" %>

<asp:PlaceHolder ID="plcContentWithBlockquote" runat="server">
	<div class="editor-content editor-content--left-quote">
		<div class="editor-content__quote">
			<blockquote><%= Blockquote %></blockquote>
		</div>
		<div class="editor-content__text">
			<p><%= ContentText %></p>
		</div>
	</div>
</asp:PlaceHolder>