using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class UserCreation : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "UserCreation"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                InitForm();
            }
        }

        private void InitForm()
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            BindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    var res = dx.sp_tblUser_InsertUpdate(Convert.ToInt32(hfUserIDPKID.Value), txtUseID.Text.Trim()
                        , txtPassword.Text.Trim(), txtUserName.Text.Trim(), txtEmail.Text.Trim()).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();

                        ShowConfirmAddMoreRecord();
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    var res = dx.sp_tblUser_InsertUpdate(Convert.ToInt32(hfUserIDPKID.Value), txtUseID.Text.Trim()
                        , txtPassword.Text.Trim(), txtUserName.Text.Trim(), txtEmail.Text.Trim()).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
                        ShowConfirmAddMoreRecord();
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        private bool ValidateInput()
        {
            CleatValidation();
            if (txtUseID.Text.Trim() == "")
            {
                //txtUseID.Attributes.Add("class", "txtOnError");                                
                //txtPassword.Attributes.Add("style", "border: red 1px solid");
                lbl_error.Text = "User ID is required.";
                return false;
            }

            if (txtPassword.Text.Trim() == "")
            {
                lbl_error.Text = "Password is required.";
                return false;
            }

            if (txtUserName.Text.Trim() == "")
            {
                lbl_error.Text = "User Name is required.";
                return false;
            }

            if (txtEmail.Text.Trim() != "")
            {
                var trimmedEmail = txtEmail.Text.Trim();

                if (trimmedEmail.EndsWith("."))
                {
                    lbl_error.Text = "Invalid Email format.";
                    return false;
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                    return addr.Address == trimmedEmail;
                }
                catch
                {
                    lbl_error.Text = "Invalid Email format.";
                    return false;
                }
            }

            return true;
        }

        private void ClearForm()
        {
            InitForm();
            CleatValidation();
            hfUserIDPKID.Value = "0";
            txtUseID.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            txtEmail.Text = "";
        }

        private void CleatValidation()
        {
            lbl_error.Text = "";
        }

        [WebMethod]
        public static AutoComplete[] AutoComplete(string Term, string TermType, string Id)
        {

            List<AutoComplete> lst = new List<AutoComplete>();
            //
            if (Term.Length < 2)
                return lst.ToArray();

            int id = Convert.ToInt32(Id);

            try
            {
                using (secoffEntities dx = new secoffEntities())
                {
                    var dt = dx.sp_GetAutoCompleteESMS(Term, TermType, id).ToList();


                    foreach (var item in dt)
                    {
                        lst.Add(
                        new AutoComplete
                        {
                            label = item.Name,
                            val = item.Value
                        }
                        );
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return lst.ToArray();

        }


        protected void hfUserIDPKID_ValueChanged(object sender, EventArgs e)
        {

            string selectedPKID = hfUserIDPKID.Value;

            LoadUserDetail(selectedPKID);
        }

        private void LoadUserDetail(string ID)
        {
            if (Convert.ToUInt32(ID) > 0)
            {
                var dt = dx.sp_tblUser_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                txtUseID.Text = dt.UserId;
                txtPassword.Text = dt.Password;
                txtUserName.Text = dt.Username;
                txtEmail.Text = dt.email;

                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfUserIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblUser_Delete(Convert.ToInt32(hfUserIDPKID.Value)).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
                    }
                    else
                    {
                        lbl_error.Text = res.RetMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }


        public void BindGrid()
        {
            try
            {
                //var aa = (from b in EDX.tblAdminFormManagements select b).ToList(); ;

                var dtUsers = (from a in dx.sp_tblUser_GetSummary()
                               select a).ToList();
                if (dtUsers.Count > 0)
                {
                    gridUsers.DataSource = dtUsers;
                    gridUsers.DataBind();

                }
                else
                {
                    gridUsers.DataBind();
                }



            }
            catch (Exception ex)
            {

            }
        }


        protected void linkButtonLookup_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("UserCreation")
                                  select a).ToList().ToDataTable();

                hfLookupResult.Value = "0";
                Session["lookupData"] = data;
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;var myWindow = window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResult.ID + "','.','focus=true,height=400,width=450,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                //string jsReportv4 = "var w=window.open('LookupControl/LookupControl.aspx?hfName=" + hfLookupResult.ID + "&UT=I','AttAdjustmentPopup','height=400,width=450,menubar=no,status=no,location=no,top=100,left=400,scrollbars=yes,resizable=no'); w.focus();";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "popup", jsReport, true);
            }
            catch (Exception ex)
            {

            }


        }

        protected void hfLookupResult_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = hfLookupResult.Value;
            hfUserIDPKID.Value = selectedPKID; //to allow update mode

            LoadUserDetail(selectedPKID);
        }

        private void ShowConfirmAddMoreRecord()
        {
            string title = "Confirmation";
            string body = "Record Saved succussfully.<br/> Do you want to add more records?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopupAfterSaveConfirmation('" + title + "', '" + body + "');", true);
        }

        protected void btnConfirmYes_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            CleatValidation();
            int selRowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
            CheckBox cb = (CheckBox)gridUsers.Rows[selRowIndex].FindControl("chkIsActive");
            Label userIDLbl = (Label)gridUsers.Rows[selRowIndex].FindControl("lblUserID");

            string UserID = userIDLbl.Text;

            if (UserID == "Admin")
            {
                lbl_error.Text = "Not Allowed";
                return;
            }

            int UserAutoID = Convert.ToInt32(gridUsers.DataKeys[selRowIndex].Value);

            var res = dx.sp_tblUser_UpdateActiveStatus(UserAutoID, cb.Checked).FirstOrDefault();

            if (res.ResponseCode == 1)
            {
                string msg = "";

                if (cb.Checked)
                {
                    msg = "User activated successfully";
                }
                else
                {
                    msg = "User deactivated successfully";
                }

                //lbl_error.Text = "";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + msg + "');", true);
            }
            else
            {
                lbl_error.Text = res.RetMessage;
            }
        }

        protected void gridUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridUsers.PageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}