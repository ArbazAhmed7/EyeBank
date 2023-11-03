using System;
using System.IO;
using System.Web;
using System.Web.Services;

namespace TransportManagement
{
    public partial class WebCam : System.Web.UI.Page
    {
        [WebMethod]
        public static bool SaveCapturedImage(string data)
        {
            string hfName = string.Empty;
            string fileName = string.Empty;

            if (HttpContext.Current.Request.QueryString["ImageName_Student"] != null)
            {
                hfName = Convert.ToString(HttpContext.Current.Request.QueryString["ImageName_Student"]);
                fileName = hfName + " - " + DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

                //Save the Byte Array as Image File.
                string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/UploadedStudentImages/{0}.jpg", fileName));
                File.WriteAllBytes(filePath, imageBytes);
            }
            else if (HttpContext.Current.Request.QueryString["ImageName_Teacher"] != null)
            {
                hfName = Convert.ToString(HttpContext.Current.Request.QueryString["ImageName_Teacher"]);
                fileName = hfName + " - " + DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

                //Save the Byte Array as Image File.
                string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/UploadedTeacherImages/{0}.jpg", fileName));
                File.WriteAllBytes(filePath, imageBytes);
            }
            else
            {
                fileName = "Picture Captured at " + DateTime.Now.ToString("dd-MM-yy hh-mm-ss");

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(data.Split(',')[1]);

                //Save the Byte Array as Image File.
                string filePath = HttpContext.Current.Server.MapPath(string.Format("~/Captures/{0}.jpg", fileName));
                File.WriteAllBytes(filePath, imageBytes);
            }

            return true;
        }
    }
}