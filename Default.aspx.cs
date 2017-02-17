using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.Session["UploadDetail"] = new UploadDetail { IsReady = false };
        }
    }

    [System.Web.Services.WebMethod]
    [System.Web.Script.Services.ScriptMethod]
    public static object GetUploadStatus()
    {
        UploadDetail info = (UploadDetail)HttpContext.Current.Session["UploadDetail"];
        if (info != null && info.IsReady)
        {
            int soFar = info.UploadedLength;
            int total = info.ContentLength;
            int percentComplete = (int)Math.Ceiling((double)soFar / (double)total * 100);
            return new
            {
                percentComplete = percentComplete,
            };
        }
               return null;
    }

    protected void dvDestination_TextChanged(object sender, EventArgs e)
    {
        UploadDetail info = (UploadDetail)HttpContext.Current.Session["UploadDetail"];
        info.DestinationPath = dvDestination != null ? dvDestination.Text: "";
    }
}