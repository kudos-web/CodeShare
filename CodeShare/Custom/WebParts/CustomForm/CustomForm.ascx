<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomForm.ascx.cs" Inherits="CMSWebParts_Custom_CustomForm_CustomForm" %>

<asp:PlaceHolder ID="plcCustomForm" runat="server">
	<div class="study-custom-form <%= DividerClass %>">
		<asp:PlaceHolder ID="plcTitle" runat="server">
			<h2><%= Title %></h2>
		</asp:PlaceHolder>
	
		<asp:PlaceHolder ID="plcDescription" runat="server">
			<%= Description %>
		</asp:PlaceHolder>
	
		<div class="custom-bizform">
			<cms:BizForm ID="bfCustomForm" runat="server" MarkRequiredFields="true" UseColonBehindLabel="false" />
		</div>
	</div>
</asp:PlaceHolder>