<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentWithRightImage.ascx.cs" Inherits="CMSWebParts_Custom_ContentWithRightImage_ContentWithRightImage" %>

<asp:PlaceHolder ID="plcContentWithRightImage" runat="server">
	<div class="editor-content editor-content--right-image">
		<img class="editor-content__image" src="<%= ImageLink %>" alt="<%= ImageTitle %>" title="<%= ImageTitle %>" />
		<p><%= ContentText %></p>
	</div>
</asp:PlaceHolder>