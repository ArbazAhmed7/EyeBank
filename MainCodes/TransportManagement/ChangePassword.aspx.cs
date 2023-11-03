using System;
using System.Linq;
using System.Web.UI;

namespace TransportManagement
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        secoffEntities EDX = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hfLoginUserId.Value = (string)Session["LoginUserId_TM"];
                txtUserId.Text = hfLoginUserId.Value;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string OldPass = txtOldPassword.Text;
            string NewPass = txtNewPassword.Text;

            if (OldPass.Trim() == "")
            {
                lbl_error.Text = "Please enter 'Old Password'";
                return;
            }

            if (NewPass.Trim() == "")
            {
                lbl_error.Text = "Please enter 'New Password'";
                return;
            }

            var res = EDX.sp_ChangePassword(hfLoginUserId.Value, OldPass, NewPass).FirstOrDefault();

            if (res.ResponseCode == 1)
            {
                lbl_error.Text = res.RetMessage;
                lbl_error.ForeColor = System.Drawing.ColorTranslator.FromHtml("#037203");
            }
            else
            {
                lbl_error.Text = res.RetMessage;
            }
        }
    }
}