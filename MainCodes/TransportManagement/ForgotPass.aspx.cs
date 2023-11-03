using System;
using System.Linq;
using System.Net.Mail;
using System.Web.UI;


namespace TransportManagement
{
    public partial class ForgotPass : System.Web.UI.Page
    {
        secoffEntities EDX = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PnlForgotPass.Visible = true;
                pnlEnterPin.Visible = false;
                pnlChangePass.Visible = false;
            }

        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            string UserID = txtUseID.Text.Trim();
            if (UserID == "")
            {
                lblErrorForgotpass.Text = "Please enter your login user id";
                return;
            }

            hfUserID.Value = UserID;

            var result = EDX.sp_ForgotPassGetUserEmail(UserID).FirstOrDefault();

            if ((bool)result.ResponseStatus == false)
            {
                lblErrorForgotpass.Text = "Please enter your valid user id.";
                return;
            }

            if ((bool)result.ResponseStatus == true)
            {
                if (String.IsNullOrWhiteSpace(result.Email))
                {
                    lblErrorForgotpass.Text = "Email Address against your user id is not defined. Please contact system administrator.";
                    return;
                }
            }

            string PIN = GetRandomPIN();
            var savePinres = EDX.sp_ForgotPassSavePIN(UserID, PIN).FirstOrDefault();

            if ((bool)savePinres.ResponseStatus == true)
            {
                bool res = SendEmail(UserID, PIN, result.Email);

                if (res)
                {
                    PnlForgotPass.Visible = false;
                    pnlChangePass.Visible = false;
                    pnlEnterPin.Visible = true;
                    //lblErrorForgotpass.Text = "Email sent to your Email id.";
                }
            }
            else
            {
                lblErrorForgotpass.Text = "PIN couldn't be generated.";
            }
        }



        #region ForgotPass        
        private bool SendEmail(string UserID, string PinCode, string ToEmail)
        {

            string to = ToEmail; //To address    
            string from = "info@eyescreening.viftechuat.com	"; //From address    
            MailMessage message = new MailMessage(from, to);

            string subject = "Password Change Request";
            string mailbody = GetEmailBody(UserID, PinCode);
            message.Subject = subject;
            message.Body = mailbody;

            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("eyescreening.viftechuat.com", 26); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("info@eyescreening.viftechuat.com", "GUdyGM7YDKV8YMQM");
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            client.Timeout = 50000;
            try
            {
                client.Send(message);
                return true;
            }

            catch (Exception ex)
            {
                lblErrorForgotpass.Text = ex.Message;
                return false;
            }
        }

        private string GetEmailBody(string UserID, string PinCode)
        {
            string body = @"<html>
                        <body>
                        <p>Hello!</p>
                        <p><b>" + UserID + @"</b></p>

                        <p>You have recently request for resetting your password.</p>

                        <p>You can enter the following password Recovery pin: </p>

                        <p><b>" + PinCode + @"</b></br>

                        <p>Note: This Pin code will expire after 1 day</p>

                        <p>If you did not request this change, please ignore this email.</p>

                        <p>*** This is a system generated email ***</p>
                        </body>
                        </html>";

            return body;
        }

        private string GetRandomPIN()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000).ToString("D6");
        }
        #endregion

        protected void lnkSubmitPin_Click(object sender, EventArgs e)
        {
            string userID = hfUserID.Value;

            var res = EDX.sp_ForgotPassValidatePIN(userID, txtPinCode.Text.Trim()).FirstOrDefault();
            if ((bool)res.ResponseStatus == true)
            {
                PnlForgotPass.Visible = false;
                pnlEnterPin.Visible = false;
                pnlChangePass.Visible = true;
            }
            else
            {
                lblErrorPIN.Text = res.Msg;
            }
        }

        protected void lnkBtnChangePass_Click(object sender, EventArgs e)
        {
            string NewPass = txtNewPass.Text;
            string NewPassConfirm = txtNewPassConfirm.Text;

            if (NewPass.Trim() == "")
            {
                lblErrorChangePass.Text = "Please enter New Password";
                return;
            }

            if (NewPass.Trim() == "")
            {
                lblErrorChangePass.Text = "Please enter 'Confirm Password'";
                return;
            }

            if (NewPass != NewPassConfirm)
            {
                lblErrorChangePass.Text = "'Confirm Password' did not match with your 'New Password'";
                return;
            }

            var res = EDX.sp_ForgotPass_ChangePass(hfUserID.Value, NewPassConfirm).FirstOrDefault();

            if (res.ResponseCode == 1)
            {
                lblErrorChangePass.Text = "Password changed successfully, now redirecting to Login page.";
                lblErrorChangePass.ForeColor = System.Drawing.ColorTranslator.FromHtml("#037203");
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                "setTimeout(function() { window.location.replace('Login.aspx') }, 5000);", true);
            }
            else
            {
                lblErrorChangePass.Text = res.RetMessage;
            }

        }

    }
}