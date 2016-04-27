using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using CMS.PortalControls;
using CMS.Helpers;

public partial class CMSWebParts_Custom_ContentWithLeftBlockquoteAndButton_ContentWithLeftBlockquoteAndButton : CMSAbstractWebPart
{
	public string Blockquote
	{
		get
		{
			return ValidationHelper.GetString(GetValue("Blockquote"), "");
		}
	}

	public string ContentText
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ContentText"), "");
		}
	}

	public string ButtonLink
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ButtonLink"), "");
		}
	}

	public string ButtonText
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ButtonText"), "");
		}
	}

	public string OpenInNewWindow
	{
		get
		{
			var openNewWindow = ValidationHelper.GetBoolean(GetValue("OpenNewWindow"), false);

			return (openNewWindow) ? "target=\"_blank\"" : "";
		}
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}