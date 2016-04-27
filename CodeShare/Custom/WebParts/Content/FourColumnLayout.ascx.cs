using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CMS.PortalControls;
using CMS.Helpers;
using CMS.PortalEngine;

public partial class CMSWebParts_Custom_FourColumnLayout : CMSAbstractLayoutWebPart
{
    #region "Properties"

    

    #endregion


    #region "Methods"

    protected override void PrepareLayout()
    {
        StartLayout();

        if (DesignMode || ViewMode == ViewModeEnum.Edit)
        {
            Append("<div style='height: 35px;'></div>");
        }

        Append("<div class='row row--collapsed content-section inline-block-wrapper'>");

        Append("<div class='span3 tablet6 mobile4 inline-block'>");

        // AddZone("Column1", "Column 1");
        AddZone(WebPartID + "_1", "Column 1");

        Append("</div>");

        Append("<div class='span3 tablet6 mobile4 inline-block'>");

        // AddZone("Column2", "Column 2");
        AddZone(WebPartID + "_2", "Column 2");

        Append("</div>");

        Append("<div class='span3 tablet6 mobile4 inline-block'>");

        // AddZone("Column3", "Column 3");
        AddZone(WebPartID + "_3", "Column 3");

        Append("</div>");

        Append("<div class='span3 tablet6 mobile4 inline-block'>");

        // AddZone("Column4", "Column 4");
        AddZone(WebPartID + "_4", "Column 4");

        Append("</div>");

        Append("</div>");


        FinishLayout();

    }

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



