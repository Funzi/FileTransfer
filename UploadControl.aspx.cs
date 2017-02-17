using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadControl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.IsPostBack)
        {
            UploadDetail upload = (UploadDetail)this.Session["UploadDetail"];
            const string js = "window.parent.onComplete();";
            upload.IsReady = false;

            if (this.fileUpload.PostedFile != null && this.fileUpload.PostedFile.ContentLength > 0 && upload.DestinationPath != null)
            {
                
                string path = upload.DestinationPath.ToString();
                string fileName = Path.GetFileName(this.fileUpload.PostedFile.FileName);
               
                upload.ContentLength = this.fileUpload.PostedFile.ContentLength;
                upload.FileName = fileName;
                upload.UploadedLength = 0;
                upload.IsReady = true;
                                
                int bufferSize = 1;
                byte[] buffer = new byte[bufferSize];

                using (FileStream fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    while (upload.UploadedLength < upload.ContentLength)
                    {
                        int bytes = this.fileUpload.PostedFile.InputStream.Read(buffer, 0, bufferSize);
                        fs.Write(buffer, 0, bytes);
                        upload.UploadedLength += bytes;
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(UploadControl),"progress", js, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(UploadControl), "progress", js, true);
            }
            upload.IsReady = false;
        }
    }
}