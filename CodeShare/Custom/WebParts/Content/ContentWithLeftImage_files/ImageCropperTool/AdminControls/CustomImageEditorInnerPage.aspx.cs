﻿using System;

using CMS.Helpers;
using CMS.UIControls;

public partial class Custom_FormControls_ImageCropperTool_AdminControls_CustomImageEditorInnerPage : LivePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("imageeditor.badhashtitle") + "&text=" + ResHelper.GetString("imageeditor.badhashtext")));
        }
        else
        {
            ScriptHelper.RegisterJQueryCrop(Page);
            ScriptHelper.RegisterScriptFile(Page, "~/Custom/FormControls/ImageCropperTool/AdminControls/CustomImageEditorInnerPage.js");
            CSSHelper.RegisterBootstrap(Page);

            string imgUrl = QueryHelper.GetString("imgurl", null);
            if (String.IsNullOrEmpty(imgUrl))
            {
                string url = URLHelper.ResolveUrl("~/CMSAdminControls/ImageEditor/GetImageVersion.aspx" + RequestContext.CurrentQueryString);

                url = URLHelper.RemoveParameterFromUrl(url, "hash");
				url = URLHelper.AddParameterToUrl(url, "hash", ValidationHelper.GetHashString(url, hashSalt: HashValidationSalts.GETIMAGEVERSION_PAGE));

                imgContent.ImageUrl = url;

                int imgwidth = QueryHelper.GetInteger("imgwidth", 0);
                int imgheight = QueryHelper.GetInteger("imgheight", 0);
                if ((imgwidth > 0) && (imgheight > 0))
                {
                    imgContent.Width =  imgwidth;
                    imgContent.Height = imgheight;
                }
            }
            else
            {
                imgContent.ImageUrl = imgUrl;
            }

            imgContent.Attributes.Add("style", "width: 100% !important; height: auto !important");
        }
    }
}