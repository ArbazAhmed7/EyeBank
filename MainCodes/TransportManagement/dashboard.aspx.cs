using Microsoft.VisualBasic;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class dashboard : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Dashboard";

            //Arbaz Work Start
            string myKey = System.Configuration.ConfigurationManager.AppSettings["CoreApplication"];
            try
            {

                string Id = HttpContext.Current.Session["LoginUserId_TM"].ToString();
                var URL = $"{myKey}/Session/SessionStore/api/Get/{Utilities.GetEncrypt(Id)}";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Open", "var win = window.open('" + URL + "','Login','height=1,width=1,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no'); if (win) {win.focus();} else {alert('Please allow popups for this website');}", true);

            }
            catch (Exception ex) {
                var URL = $"{myKey}/Session/SessionStore/api/ClearSession";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page,Page.GetType(), "Open", "window.open('" + URL + "','Login','height=1,width=1,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no');", true);
                Response.Redirect("~/Login.aspx");
            }
            //End

            int a = Page.Session.Timeout;

            var dtDashboardRights = dx.sp_GetDashboardRights(Utilities.GetLoginUserID()).SingleOrDefault();
            if (dtDashboardRights != null)
            {
                pnlDashboard.Visible = true;
                var dt = dx.sp_Dashboard().SingleOrDefault();
                if (dt != null)
                {
                    lblSchoolScreened.Text = dt.SchoolScreened.ToString();

                    lblStudentScreened.Text = int.Parse(dt.StudentScreened.ToString()).ToString("#,##0");
                    lblGirlScreened.Text = int.Parse(dt.GirlsScreened.ToString()).ToString("#,##0");
                    lblBoyScreened.Text = int.Parse(dt.BoysScreened.ToString()).ToString("#,##0");
                    lblTeacherScreened.Text = int.Parse(dt.TeacherScreened.ToString()).ToString("#,##0");

                    lblPrescribedGlasses.Text = int.Parse(dt.PrescribedGlasses.ToString()).ToString("#,##0");

                    lblSurgeries.Text = int.Parse(dt.StudentforSurgery.ToString()).ToString("#,##0");

                    lblStudentTarget.Text = int.Parse(dt.Target.ToString()).ToString("#,##0");
                    lblStudentTargetAchieved.Text = int.Parse(dt.TargetAchieved.ToString()).ToString("#,##0");
                    lblStudentTobeAchieved.Text = int.Parse(dt.TargettobeAchieved.ToString()).ToString("#,##0");
                }
            }
            else
            {
                pnlDashboard.Visible = false;

                lblSchoolScreened.Text = "";
                lblStudentScreened.Text = "";
                lblGirlScreened.Text = "";
                lblBoyScreened.Text = "";
                lblTeacherScreened.Text = "";
                lblPrescribedGlasses.Text = "";
                lblSurgeries.Text = "";
                lblStudentTarget.Text = "";
                lblStudentTargetAchieved.Text = "";
                lblStudentTobeAchieved.Text = "";
            }
        }

      
    }

}
