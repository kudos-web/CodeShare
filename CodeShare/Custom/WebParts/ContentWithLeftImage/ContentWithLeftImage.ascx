<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContentWithLeftImage.ascx.cs" Inherits="CMSWebParts_Custom_ContentWithLeftImage_ContentWithLeftImage" %>

<asp:PlaceHolder ID="plcContentWithLeftImage" runat="server">
	<div class="editor-content editor-content--left-image">
		<img class="editor-content__image" src="<%= ImageLink %>" alt="<%= ImageTitle %>" title="<%= ImageTitle %>" />
		<p><%= ContentText %></p>
	</div>
</asp:PlaceHolder>