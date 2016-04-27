using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.Helpers;

public partial class Custom_WebParts_YoutubePopup : CMSAbstractWebPart
{
	#region "Properties"

	public string Image
	{
		get
		{
			return ValidationHelper.GetString(GetValue("Image"), String.Empty);
		}
		set
		{
			SetValue("Image", value);
		}
	}

	public string ImageAlt
	{
		get
		{
			return ValidationHelper.GetString(GetValue("ImageAlt"), String.Empty);
		}
		set
		{
			SetValue("ImageAlt", value);
		}
	}

	public string VideoUrl
	{
		get
		{
			return ValidationHelper.GetString(GetValue("VideoUrl"), String.Empty);
		}
		set
		{
			SetValue("VideoUrl", value);
		}
	}

	#endregion


	#region "Methods"

	/// <summary>
	/// Content loaded event handler.
	/// </summary>
	public override void OnContentLoaded()
	{
		base.OnContentLoaded();
		SetupControl();
	}


	/// <summary>
	/// Initializes the control properties.
	/// </summary>
	protected void SetupControl()
	{
		if (this.StopProcessing)
		{
			// Do not process
		}
		else
		{
			if (!String.IsNullOrEmpty(VideoUrl) && String.IsNullOrEmpty(Image))
			{
				//Image = CustomHelper.GetYoutubeImage(VideoUrl);
			}

			if (String.IsNullOrEmpty(Image))
			{
				Image = "http://placehold.it/480x295";
			}

			var Html = @"<a href='{0}' class='js-magnific-popup-video video-tile icon icon-play' data-effect='mfp-fade' target='_blank'>
						<img src='{1}' alt='{2}' />
					</a>";

			Content.Text = String.Format(Html, VideoUrl, Image, ImageAlt);
		}
	}


	/// <summary>
	/// Reloads the control data.
	/// </summary>
	public override void ReloadData()
	{
		base.ReloadData();

		SetupControl();
	}

	#endregion
}



