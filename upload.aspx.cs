using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class upload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpPostedFile file = Request.Files["file"];
        //check file was submitted
        if (file != null && file.ContentLength > 0)
        {
            string fname = Path.GetFileName(file.FileName);
            file.SaveAs(Server.MapPath(Path.Combine("~/App_Data/", fname)));
        }
    }

}