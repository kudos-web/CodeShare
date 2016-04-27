<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentWithLeftBlockquoteAndButton.ascx.cs" Inherits="CMSWebParts_Custom_ContentWithLeftBlockquoteAndButton_ContentWithLeftBlockquoteAndButton" %>

<asp:PlaceHolder ID="plcContentWithLeftBlockquoteAndButton" runat="server">
	<div class="editor-content editor-content--left-quote">
		<div class="editor-content__quote">
			<blockquote>
				<%= Blockquote %>
				<div><a href="<%= ButtonLink %>" <%= OpenInNewWindow %>><%= ButtonText %></a></div>
			</blockquote>
		</div>
		<div class="editor-content__text">
			<p><%= ContentText %></p>
		</div>
	</div>
</asp:PlaceHolder>