using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using CMS.PortalControls;
using CMS.Helpers;

public partial class CMSWebParts_Custom_ContentWithLeftImage : CMSAbstractWebPart
{
	public string ImageLink
	{
		get
		{
			var img = ValidationHelper.GetString(GetValue("ImageLink"), "");
			return !string.IsNullOrEmpty(img) ? img : "http://placehold.it/580x384";
		}
	}

	public string ImageTitle
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ImageTitle"), "");
		}
	}

	public string ContentText
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ContentText"), "");
		}
	}
	
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}