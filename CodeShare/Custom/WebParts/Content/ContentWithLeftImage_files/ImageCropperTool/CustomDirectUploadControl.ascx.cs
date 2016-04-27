using System;

using CMS.DocumentEngine;
using CMS.FormControls;
using CMS.FormEngine;
using CMS.Helpers;
using CMS.SiteProvider;
using System.Web;
using CMS.IO;
using CMS.MediaLibrary;
using CMS.Membership;
using CMS.EventLog;
using CMS.DataEngine;

public partial class Custom_FormControls_ImageCropperTool_CustomDirectUploadControl : FormEngineUserControl
{
    #region "Properties"
    /// <summary>
    /// Gets or sets form control value.
    /// </summary>
    public override object Value
    {
        get
        {
			return txtPath.Text;
        }
        set
        {
			txtPath.Text = ValidationHelper.GetString(value, "");
		}
    }

	public override FormFieldInfo FieldInfo
	{
		get
		{
			return base.FieldInfo;
		}
		set
		{
			base.FieldInfo = value;
		}
	}

	private TreeNode _currentNode = new TreeNode();
	private TreeNode CurrentNode
	{
		get
		{
			if (_currentNode.NodeID == 0)
			{ 
				var currentNodeID = QueryHelper.GetInteger("nodeid", 0);

				if (currentNodeID > 0)
					_currentNode = TreeHelper.SelectSingleNode(currentNodeID);
			}

			return _currentNode;
		}
	}

	private string _libraryName = "";
	private string LibraryName
    {
        get
        {
			if (string.IsNullOrEmpty(_libraryName))
				_libraryName = GetSettingsValue("LibraryName");

			return _libraryName;
        }
    }

	private string _folderDestinationPath = "";
    private string FolderDestinationPath
    {
        get
        {
			if (string.IsNullOrEmpty(_folderDestinationPath))
				_folderDestinationPath = GetSettingsValue("LibraryDestinationPath");

			return _folderDestinationPath;
        }
    }

    private int CropWidth
    {
        get
        {
            return ValidationHelper.GetInteger(GetSettingsValue("CropWidth"), 0);
        }
    }

    private int CropHeight
    {
        get
        {
            return ValidationHelper.GetInteger(GetSettingsValue("CropHeight"), 0);
        }
    }

    private bool CropAllowResize
    {
        get
        {
            return ValidationHelper.GetBoolean(GetValue("CropAllowResize"), true);
        }
    }

    private string AllowedExtensions
    {
        get
        {
            return ValidationHelper.GetString(GetValue("allowed_extensions"), "");
        }
    }

	private string CurrentImageFilePath
	{
		get
		{
			return ValidationHelper.GetString(Value, "");
		}
	}

	private bool UseCustomSettings
	{
		get
		{
			return ValidationHelper.GetBoolean(GetValue("UseCustomSettings"), false);
		}
	}

	private bool FormIsValid = true;
	private string ErrorMessage = "";
	private Guid CurrentImageGUID = Guid.Empty;

    #endregion


    #region "Methods"

	private string GetSettingsValue(string property)
	{
		if (!string.IsNullOrEmpty(property) && UseCustomSettings && CurrentNode != null && CurrentNode.NodeID > 0)
		{
			var className = CurrentNode.ClassName.Replace(".", "_");
			var settingsCodeName = string.Format("{0}_{1}_{2}", className, FieldInfo.Name, property);

			var settingsValue = SettingsKeyInfoProvider.GetStringValue(SiteContext.CurrentSiteName + "." + settingsCodeName);

			return settingsValue;
		}

		return ValidationHelper.GetString(GetValue(property), "");
	}

    /// <summary>
    /// Page load.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckFieldEmptiness = false;

        if (customFileUpload.PostedFile != null)
        {
            //var PostedFile = customFileUpload.PostedFile.FileName;
            //var filename = customFileUpload.FileName;

            var currentMediaFilePath = UploadMedia(customFileUpload.PostedFile, FolderDestinationPath);
			if (CurrentImageGUID != Guid.Empty)
				Value = string.Format("~{0}", currentMediaFilePath.Replace("~", ""));

            //hdnImageGuid.Value = ValidationHelper.GetString(Value, "");
        }

		var mediaBaseURL = string.Format("/{0}/media/", SiteContext.CurrentSiteName);

		if (CurrentImageFilePath.Replace("~", "").StartsWith(mediaBaseURL))
		{ 
			var ImageGUID = GetImageGuid(CurrentImageFilePath);

			//var mediaInfo = MediaFileInfoProvider.GetMediaFileInfo(ImageGUID, CurrentSite.SiteName);
			//txtPath.Text = MediaFileURLProvider.GetMediaFileUrl(mediaInfo, CurrentSite.SiteName, LibraryName);

			txtPath.Text = CurrentImageFilePath;

			string baseUrl;

			// Dialog URL for image editing
			baseUrl = string.Format("{0}/Custom/FormControls/ImageCropperTool/Modules/CustomImageEditor.aspx", URLHelper.GetFullApplicationUrl());

			// Create security hash
			string parameters = string.Format("mediafileguid={0}&cropWidth={1}&cropHeight={2}&libraryName={3}", ImageGUID, CropWidth, CropHeight, LibraryName);
			parameters += "&hash=" + QueryHelper.GetHash("?" + parameters, true);

			lnkEdit.NavigateUrl = string.Format("{0}/Custom/FormControls/ImageCropperTool/Modules/CustomImageCropper.aspx?{1}", URLHelper.GetFullApplicationUrl(), parameters);

			lnkEdit.Visible = true;
			lnkEdit.Enabled = (!string.IsNullOrEmpty(txtPath.Text));
		}
		else
		{
			lnkEdit.Visible = false;

		}

        ResetDisplay();

		this.StopProcessing = true;
    }

	private Guid GetImageGuid(string filePath)
	{
		var mediaRootFolderPath = string.Format("/{0}/media/{1}/", SiteContext.CurrentSiteName, LibraryName);

		var libraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(LibraryName, SiteContext.CurrentSiteName);

		if (libraryInfo != null && !string.IsNullOrEmpty(filePath))
		{
			filePath = URLHelper.RemoveQuery(filePath).Replace("~", "").Replace(mediaRootFolderPath, "");

			var mediaFileInfo = MediaFileInfoProvider.GetMediaFileInfo(libraryInfo.LibraryID, filePath);

			if (mediaFileInfo != null)
				return mediaFileInfo.FileGUID;
		}

		return Guid.Empty;
	}

    private void ResetDisplay()
    {
        plcImageEdit.Visible = (!string.IsNullOrEmpty(txtPath.Text));
        plcFileUpload.Visible = !plcImageEdit.Visible;
    }

    /// <summary>
    ///		Uploads posted file to a library folder
    /// </summary>
    /// <param name="file">Posted file</param>
    /// <param name="libraryFolder">Destination folder</param>
    /// <returns>Permanent path to uploaded file</returns>
    private string UploadMedia(HttpPostedFile postedfile, string libraryFolder)
    {
        var PostedMedia = postedfile;

        string MediaPath = String.Empty;

        if (PostedMedia != null)
        {
            try
            {
                string Filename = Path.GetFileName(PostedMedia.FileName).Replace(" ", "_").Replace("(", "_").Replace(")", "_");
                string fileExt = Path.GetExtension(PostedMedia.FileName);

				if (!string.IsNullOrEmpty(Filename))
				{ 
					if (string.Format(";{0};", AllowedExtensions.ToLower()).Contains(string.Format(";{0};", fileExt.Replace(".", "").ToLower())))
					{
						litError.Text = "";

						string FilePath = HttpContext.Current.Server.MapPath("~/" + CurrentSite.SiteName + "/media/" + LibraryName + "/" + libraryFolder + "/" + Filename);
                    
						MediaLibraryInfo libraryInfo = MediaLibraryInfoProvider.GetMediaLibraryInfo(LibraryName, SiteContext.CurrentSiteName);
						MediaFileInfo mediaFile = null;

						if (File.Exists(FilePath))
						{
							FormIsValid = false;
							ErrorMessage = "A file with this name already exists. Please use a different file or rename your file.";
							CurrentImageGUID = Guid.Empty;

							//FilePath = libraryFolder + "/" + Filename;
							//mediaFile = MediaFileInfoProvider.GetMediaFileInfo(libraryInfo.LibraryID, FilePath);
							//CurrentImageGUID = mediaFile.FileGUID;
							//return FilePath;
						}
						else
						{
							PostedMedia.SaveAs(FilePath);
							mediaFile = new MediaFileInfo(FilePath, libraryInfo.LibraryID, libraryFolder);

							FileInfo file = FileInfo.New(FilePath);

							if (file != null)
							{

								mediaFile.FileExtension = file.Extension;
								mediaFile.FileSiteID = SiteContext.CurrentSiteID;
								mediaFile.FileLibraryID = libraryInfo.LibraryID;
								mediaFile.FileSize = file.Length;

								MediaFileInfoProvider.SetMediaFileInfo(mediaFile, false);

								txtPath.Text = MediaFileURLProvider.GetMediaFileUrl(mediaFile, CurrentSite.SiteName, LibraryName);

								CurrentImageGUID = mediaFile.FileGUID;
								return txtPath.Text;
							}
						}
					}
					else
					{
						FormIsValid = false;
						ErrorMessage = string.Format("Invalid file type. Allowed file extensions: {0}", AllowedExtensions.Replace(";", ", "));
					}
				}
            }
            catch (Exception ex)
            {
				FormIsValid = false;
				ErrorMessage = "An error occurred when saving data. Please see event log for more details.";

				EventLogProvider.LogEvent(
					EventType.ERROR, 
					"Image", 
					"CustomImageCropperTool", 
					EventLogProvider.GetExceptionLogMessage(ex), 
					RequestContext.RawURL, 
					CurrentUser.UserID, 
					CurrentUser.UserName, 
					(CurrentNode != null && CurrentNode.NodeID > 0) ? CurrentNode.NodeID : 0,
					(CurrentNode != null && CurrentNode.NodeID > 0) ? CurrentNode.DocumentName : null, 
					RequestContext.UserHostAddress, 
					SiteContext.CurrentSiteID);
            }
        }

        return string.Empty;
    }
    
	public override bool IsValid()
	{
		this.ValidationError = ErrorMessage;
		return FormIsValid;
	}

    protected void btnClear_Click(object sender, EventArgs e)
    {
        hdnImageGuid.Value = string.Empty;
        txtPath.Text = string.Empty;
		
		ResetDisplay();
    }

    #endregion
}