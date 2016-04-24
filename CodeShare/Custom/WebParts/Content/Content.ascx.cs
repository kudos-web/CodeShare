using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.Helpers;

public partial class Custom_WebParts_Content_Content : CMSAbstractWebPart
{

	public string Description
	{
		get
		{
			return ValidationHelper.GetString(GetValue("Description"), String.Empty);
		}
		set
		{
			SetValue("Description", value);
		}
	}


	/// <summary>
	/// Content loaded event handler.
	/// </summary>
	public override void OnContentLoaded()
	{
		base.OnContentLoaded();
		SetupControl();
	}


	private void SetupControl()
	{
		if (!Description.Trim().StartsWith("<"))
		{
			Description = "<p>" + Description + "</p>";
		}

		Content.Text = String.Format("<div class='content-section editor-content'>{0}</div>", Description);
	}

	protected void Page_Load(object sender, EventArgs e)
	{

	}
}