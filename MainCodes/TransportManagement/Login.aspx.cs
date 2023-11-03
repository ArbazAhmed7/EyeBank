using System;
using System.Configuration;
using System.Linq;
using System.Web;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class Login : System.Web.UI.Page
    {
        secoffEntities EDX = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
        #if DEBUG
            if (!IsPostBack)
            {
                //Arbaz Work Start
                string myKey = System.Configuration.ConfigurationManager.AppSettings["CoreApplication"];
                var URL = $"{myKey}/Session/SessionStore/api/ClearSession";
                string jsReport = "<script type='text/javascript'>var win = window.open('" + URL + "', 'Login', 'height=1,width=1,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no'); if (win) { win.focus(); } else { alert('Please allow popups for this website'); }</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                //END
                //Response.Redirect("~/dashboard.aspx");
            }
        #endif
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            Session["MenuData"] = null;

            if (txtUserName.Text.Trim() == "" || txtpassword.Text.Trim() == "")
            {
                lbl_error.Text = "Please enter Required fields!";
                return;
            }

             var res = EDX.sp_UserLogin(txtUserName.Text, txtpassword.Text).SingleOrDefault();

            if (res.ResponseStatus == true)
            {
                HttpContext.Current.Session["LoginUserId_TM"] = res.UserId;
                HttpContext.Current.Session["LoginUserName_TM"] = res.userName;

                Response.Redirect("~/dashboard.aspx");
            }
            else {
                lbl_error.Text = "Inactive OR Invalid User Name or Password";
            }
        } 

        protected void lnkForgotPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ForgotPass.aspx");
        }
    }
}