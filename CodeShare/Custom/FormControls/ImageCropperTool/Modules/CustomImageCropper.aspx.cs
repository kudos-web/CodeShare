using System;

using CMS.Helpers;
using CMS.UIControls;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System.Drawing;
using System.Net;
using CMS.IO;
using System.Web;

public partial class Custom_FormControls_ImageCropperTool_Modules_CustomImageCropper : LivePage
{
    private Guid ImageGUID 
    {
        get
        {
            return QueryHelper.GetGuid("mediafileguid", Guid.Empty);
        }
    }

    private int CropWidth
    {
        get
        {
            return QueryHelper.GetInteger("cropWidth", 0);
        }
    }

    private int CropHeight
    {
        get
        {
            return QueryHelper.GetInteger("cropHeight", 0);
        }
    }

    private string LibraryName
    {
        get
        {
            return QueryHelper.GetString("libraryName", "");
        }
    }

    private bool IsCropped
    {
        get
        {
            return QueryHelper.GetBoolean("cropped", false);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!QueryHelper.ValidateHash("hash"))
        {
            URLHelper.Redirect(ResolveUrl("~/CMSMessages/Error.aspx?title=" + ResHelper.GetString("imageeditor.badhashtitle") + "&text=" + ResHelper.GetString("imageeditor.badhashtext")));
        }
        else
        {
            //ScriptHelper.RegisterJQueryCrop(Page);
            //ScriptHelper.RegisterScriptFile(Page, "~/CMSAdminControls/Custom/CustomImageEditorInnerPage.js");
            //CSSHelper.RegisterBootstrap(Page);

            if (ImageGUID != Guid.Empty)
            {
                var mediaInfo = MediaFileInfoProvider.GetMediaFileInfo(ImageGUID, CurrentSiteName);

                if (mediaInfo == null)
                    return;

                var imgURL = MediaFileURLProvider.GetMediaFileUrl(mediaInfo, CurrentSiteName, LibraryName);

                imgContent.ImageUrl = string.Format("{0}?width=1600", imgURL.Replace("~", ""));

                if (IsCropped)
					litMessage.Text = ResHelper.GetString("VT.ImageCropConfirmationMessage");
            }
        }
    }
    protected void btnCrop_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnResult.Value))
        {
            var imgURL = string.Format("{0}{1}", URLHelper.GetFullApplicationUrl(), hdnResult.Value);

            byte[] data;
            using (WebClient client = new WebClient())
            {
                data = client.DownloadData(imgURL);
            }

            var appDomainPath = HttpRuntime.AppDomainAppPath;
            if (appDomainPath.EndsWith("\\"))
                appDomainPath = appDomainPath.Substring(0, appDomainPath.Length - 1);

            var fileURLToReplace = string.Format("{0}{1}", appDomainPath, URLHelper.RemoveQuery(hdnResult.Value).Replace("/", "\\"));

            File.WriteAllBytes(@fileURLToReplace, data);

            var parameters = string.Format("mediafileguid={0}&cropWidth={1}&cropHeight={2}&libraryName={3}&cropped=1", ImageGUID, CropWidth, CropHeight, LibraryName);
            parameters += "&hash=" + QueryHelper.GetHash("?" + parameters, true);

            var redirectURL = string.Format("{0}?{1}", URLHelper.RemoveQuery(Request.Url.ToString()), parameters);

            Response.Redirect(redirectURL, true);
        }
    }
}