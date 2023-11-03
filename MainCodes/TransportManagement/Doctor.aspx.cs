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
    public partial class Doctor : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "Doctor"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();
                BindHospitalDDL();

                txtDoctorID.Focus();
            }
        }

        private void InitForm()
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    int iSurgeon = 0;
                    if (chkSurgeon.Checked == true)
                    {
                        iSurgeon = 1;
                    }

                    int iOphthalmologist = 0;
                    if (chkOphthalmologist.Checked == true)
                    {
                        iOphthalmologist = 1;
                    }

                    int iOrthoptist = 0;
                    if (chkOrthoptist.Checked == true)
                    {
                        iOrthoptist = 1;
                    }

                    int iOptometrist = 0;
                    if (chkOptometrist.Checked == true)
                    {
                        iOptometrist = 1;
                    }

                    var res = dx.sp_tblDoctor_InsertUpdate(Convert.ToInt32(hfDoctorIDPKID.Value), txtDoctorDescription.Text.Trim()
                       , Convert.ToInt32(ddlHospital.SelectedValue), iSurgeon, iOphthalmologist, iOrthoptist, iOptometrist, txtContactNo.Text.Trim()
                       , txtMobile.Text, txtEmail.Text.Trim()).FirstOrDefault();

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
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    int iSurgeon = 0;
                    if (chkSurgeon.Checked == true)
                    {
                        iSurgeon = 1;
                    }

                    int iOphthalmologist = 0;
                    if (chkOphthalmologist.Checked == true)
                    {
                        iOphthalmologist = 1;
                    }

                    int iOrthoptist = 0;
                    if (chkOrthoptist.Checked == true)
                    {
                        iOrthoptist = 1;
                    }

                    int iOptometrist = 0;
                    if (chkOptometrist.Checked == true)
                    {
                        iOptometrist = 1;
                    }

                    var res = dx.sp_tblDoctor_InsertUpdate(Convert.ToInt32(hfDoctorIDPKID.Value), txtDoctorDescription.Text.Trim()
                       , Convert.ToInt32(ddlHospital.SelectedValue), iSurgeon, iOphthalmologist, iOrthoptist, iOptometrist, txtContactNo.Text.Trim()
                       , txtMobile.Text, txtEmail.Text.Trim()).FirstOrDefault();

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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfDoctorIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblDoctor_Delete(Convert.ToInt32(hfDoctorIDPKID.Value)).FirstOrDefault();

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

        private bool ValidateInput()
        {
            ClearValidation();
            if (txtDoctorDescription.Text.Trim() == "")
            {
                lbl_error.Text = "Doctor Name is required.";
                txtDoctorDescription.Focus();
                return false;
            }

            if (txtContactNo.Text.Trim() == "" && txtMobile.Text.Trim() == "")
            {
                lbl_error.Text = "Please enter Doctor's Contact detail.";
                txtContactNo.Focus();
                return false;
            }

            var trimmedEmail = txtEmail.Text.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                lbl_error.Text = "Invalid email address";
                txtEmail.Focus();
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                lbl_error.Text = "Invalid email address";
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            InitForm();
            ClearValidation();
            hfDoctorIDPKID.Value = "0";
            txtDoctorID.Text = "";
            txtDoctorDescription.Text = "";
            ddlHospital.SelectedIndex = 0;
            //rdoSpecialist.SelectedIndex = -1;
            chkSurgeon.Checked = false;
            chkOphthalmologist.Checked = false;
            chkOptometrist.Checked = false;
            chkOrthoptist.Checked = false;
            //txtCategory.Text = "";
            txtContactNo.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";

            txtDoctorID.Focus();
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        [WebMethod]
        public static AutoComplete[] AutoComplete(string Term, string TermType, string Id)
        {

            List<AutoComplete> lst = new List<AutoComplete>();

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

        protected void hfDoctorIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string ClassIDPKID = string.Empty;
            ClassIDPKID = hfDoctorIDPKID.Value;

            LoadDoctorDetail(ClassIDPKID);
            lbl_error.Text = "";
        }

        protected void hfLookupResult_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            //hfDoctorIDPKID.Value = "0";
            selectedPKID = hfLookupResult.Value;
            hfDoctorIDPKID.Value = selectedPKID; //to allow update mode

            LoadDoctorDetail(selectedPKID);
            lbl_error.Text = "";
        }

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable data = (from a in dx.sp_GetLookupData_Doctor()
                                  select a).ToList().ToDataTable();

                hfLookupResult.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Id";
                Session["Name"] = "Doctor Name";
                Session["FatherName"] = "Speciality";
                Session["Description"] = "Hospital";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlFatherName.aspx?winTitle=Select User&hfName=" + hfLookupResult.ID + "','.','height=600,width=650,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(btnLookup, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";

            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        private void LoadDoctorDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblDoctor_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    txtDoctorID.Text = dt.DoctorAutoId.ToString();
                    txtDoctorDescription.Text = dt.DoctorDescription;
                    try
                    {
                        ddlHospital.SelectedValue = Convert.ToString(dt.HospitalAutoID);
                    }
                    catch (Exception)
                    {
                        ddlHospital.SelectedIndex = 0;
                    }

                    //rdoSpecialist.SelectedValue = dt.SpecialistIn.ToString();
                    //txtCategory.Text = dt.Category.ToString();

                    int iSurgeon = int.Parse(dt.Surgeon.ToString());
                    if (iSurgeon == 1)
                    {
                        chkSurgeon.Checked = true;
                    }
                    else
                    {
                        chkSurgeon.Checked = false;
                    }

                    int iOphthalmologist = int.Parse(dt.Ophthalmologist.ToString());
                    if (iOphthalmologist == 1)
                    {
                        chkOphthalmologist.Checked = true;
                    }
                    else
                    {
                        chkOphthalmologist.Checked = false;
                    }

                    int iOrthoptist = int.Parse(dt.Orthoptist.ToString());
                    if (iOrthoptist == 1)
                    {
                        chkOrthoptist.Checked = true;
                    }
                    else
                    {
                        chkOrthoptist.Checked = false;
                    }

                    int iOptometrist = int.Parse(dt.Optometrist.ToString());
                    if (iOptometrist == 1)
                    {
                        chkOptometrist.Checked = true;
                    }
                    else
                    {
                        chkOptometrist.Checked = false;
                    }

                    txtContactNo.Text = dt.ContactNo.ToString();
                    txtMobile.Text = dt.MobileNo.ToString();
                    txtEmail.Text = dt.Email.ToString();

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
                hfLookupResult.Value = "0";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void BindHospitalDDL()
        {
            string q = "select * from tblHospital where HospitalDescription='N/A' Union select * from tblHospital where HospitalDescription <> 'N/A'";
            var data = dx.Database.SqlQuery<tblHospital>(q).ToList();
            ddlHospital.DataSource = data;
            ddlHospital.DataValueField = "HospitalAutoId";
            ddlHospital.DataTextField = "HospitalDescription";
            ddlHospital.DataBind();
        }

        protected void txtDoctorDescription_TextChanged(object sender, EventArgs e)
        {
            string sDoctorDescription = txtDoctorDescription.Text;

            txtDoctorDescription.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sDoctorDescription.ToLower());

            ddlHospital.Focus();
        }
    }
}