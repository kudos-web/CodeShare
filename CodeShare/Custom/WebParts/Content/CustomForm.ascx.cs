using System;

using CMS.PortalControls;
using CMS.Helpers;

namespace CMSWebParts.Custom
{
	public partial class CustomForm : CMSAbstractWebPart
	{
		public string BizFormName { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public enum DividerLocation { None, TopOnly, BottomOnly, Both }

		public string DividerClass { get; set; }

		protected override void OnPreRender(EventArgs e)
		{
			SetupControl();

			base.OnPreRender(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		protected void SetupControl()
		{
			BizFormName = GetStringValue("BizFormName", "");
			Title = GetStringValue("Title", "");
			Description = GetStringValue("Description", "");

			DividerLocation divider = DividerLocation.None;
			Enum.TryParse(GetStringValue("Divider", ""), out divider);
			switch (divider)
			{
				case DividerLocation.TopOnly:
					DividerClass = "divider-top-only";
					break;
				case DividerLocation.BottomOnly:
					DividerClass = "divider-bottom-only";
					break;
				case DividerLocation.Both:
					DividerClass = "divider-both";
					break;
				default:
					DividerClass = "";
					break;
			}

			if (!Page.IsPostBack)
			{ 
				bfCustomForm.FormName = BizFormName;
				bfCustomForm.IsLiveSite = IsLiveSite;
				bfCustomForm.ReloadData();
			}

			plcTitle.Visible = !string.IsNullOrEmpty(Title);
			plcDescription.Visible = !string.IsNullOrEmpty(Description);
		}
	}
}