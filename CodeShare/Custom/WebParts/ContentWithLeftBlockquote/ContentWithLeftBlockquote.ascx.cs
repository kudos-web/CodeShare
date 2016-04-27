using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

using CMS.PortalControls;
using CMS.Helpers;

public partial class CMSWebParts_Custom_ContentWithLeftBlockquote_ContentWithLeftBlockquote : CMSAbstractWebPart
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
	
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}