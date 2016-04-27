using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.Helpers;

public partial class Custom_WebParts_ContentWithHeading_ContentWithHeading : CMSAbstractWebPart
{
    #region "Properties"

    public string Name
    {
        get
        {
            return ValidationHelper.GetString(GetValue("Name"), String.Empty);
        }
        set
        {
            SetValue("Name", value);
        }
    }

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
            ltHeading.Text = Name;
            ltDescription.Text = Description;
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



