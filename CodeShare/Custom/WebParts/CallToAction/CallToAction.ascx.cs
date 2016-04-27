using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.Helpers;

public partial class Custom_WebParts_CallToAction_CallToAction : CMSAbstractWebPart
{

	public string Title
	{
		get
		{
			return ValidationHelper.GetString(GetValue("Title"), null);
		}
		set
		{
			SetValue("Title", value);
		}
	}


	public string Url
	{
		get
		{
			return ValidationHelper.GetString(GetValue("Url"), null);
		}
		set
		{
			SetValue("Url", value);
		}
	}


	public bool NewTab
	{
		get
		{
			return ValidationHelper.GetBoolean(GetValue("NewTab"), false);
		}
		set
		{
			SetValue("NewTab", value);
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
		var Html = "<p class='content-section'><a href='{0}' class='btn' {1}>{2}</a></p>";

		if (!String.IsNullOrEmpty(Url))
		{
			var Target = String.Empty;

			if (NewTab)
			{
				Target = "target='_blank'";
			}

			Html = String.Format(Html, Url, Target, Title);

			Content.Text = Html;
		}
		else
		{
			Content.Visible = false;
		}
	}

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}