using System;

namespace TransportManagement
{
    public partial class WebCam2 : System.Web.UI.Page
    {
        public string hfName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["hfName"] != null)
            {
                hfName = Convert.ToString(Request.QueryString["hfName"]);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string img = "'" + hiddenCapturedImage.Value + "'";
            string script3 = "window.opener.document.getElementById('" + hfName + "').value = " + img + "; " +
                            "window.opener.__doPostBack('hfLookupResult','');" +
                            "window.close()";

            ClientScript.RegisterStartupScript(this.GetType(), "onclick", script3, true);
        }
    }
}