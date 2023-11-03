using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class SchoolEnrollment : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "SchoolEnrollment"))
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
                InitForm();
                BindCombos();

                txtCity.Text = "Karachi";
                txtEnrollmentDate.Focus();
            }
        }
        private void InitForm()
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;
            btnSaveImage.Visible = false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_error.Text = "";
                if (ValidateInput())
                {
                    int iPrimary = 0;
                    int iSecondary = 0;
                    if (chkPrimary.Checked == true)
                    {
                        iPrimary = 1;
                    }
                    if (chkSecondary.Checked == true)
                    {
                        iSecondary = 1;
                    }

                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();
                    if (txtSchoolCode.Text == "")
                    {
                        var schoolCode = dx.sp_tblSchool_GetMaxCode().SingleOrDefault();
                        txtSchoolCode.Text = schoolCode;
                    }

                    var res = dx.sp_tblSchool_InsertUpdate(Convert.ToInt32(hfSchoolIDPKID.Value), txtSchoolCode.Text.Trim(),
                                            txtSchoolName.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), txtDistrict.Text.Trim(), txtTown.Text.Trim(),
                                            txtCity.Text.Trim(), txtTelephoneLandline.Text.Trim(), txtTelephoneCell.Text.Trim(), iPrimary, iSecondary, int.Parse(ddlGender.SelectedValue.Trim()),
                                            txtRegisteredStudent.Text == "" ? 0 : int.Parse(txtRegisteredStudent.Text.Trim()),
                                            txtRegisteredTeacher.Text == "" ? 0 : int.Parse(txtRegisteredTeacher.Text.Trim()), txtPrincipalName.Text.Trim(),
                                            txtPrincipalMobile.Text.Trim(), DateTime.Parse(txtEnrollmentDate.Text), int.Parse(rdoInstitutionType.SelectedValue), strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP, int.Parse(ddlTitle.SelectedValue.Trim())).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblSchoolImage tbl = new tblSchoolImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.SchoolAutoId = res.SchoolAutoId;
                            tbl.SchoolPic = bytUpfile; //getting complete url  
                            tbl.FileType = "";
                            tbl.FileSize = bytUpfile.Length;
                            tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                            tbl.CaptureRemarks = txtCaptureRemarks.Text;
                            tbl.UserId = strLoginUserID;
                            tbl.EntDate = DateTime.Now;
                            tbl.EntOperation = "";
                            tbl.EntTerminal = strTerminalId;
                            tbl.EntTerminalIP = strTerminalIP;
                            dx.tblSchoolImages.Add(tbl);
                            dx.SaveChanges();
                        }

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
                    int iPrimary = 0;
                    int iSecondary = 0;
                    if (chkPrimary.Checked == true)
                    {
                        iPrimary = 1;
                    }
                    if (chkSecondary.Checked == true)
                    {
                        iSecondary = 1;
                    }

                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    var res = dx.sp_tblSchool_InsertUpdate(Convert.ToInt32(hfSchoolIDPKID.Value), txtSchoolCode.Text.Trim(),
                        txtSchoolName.Text.Trim(), txtAddress1.Text.Trim(), txtAddress2.Text.Trim(), txtDistrict.Text.Trim(), txtTown.Text.Trim(),
                        txtCity.Text.Trim(), txtTelephoneLandline.Text.Trim(), txtTelephoneCell.Text.Trim(), iPrimary, iSecondary, int.Parse(ddlGender.SelectedValue.Trim()),
                        txtRegisteredStudent.Text == "" ? 0 : int.Parse(txtRegisteredStudent.Text.Trim()),
                        txtRegisteredTeacher.Text == "" ? 0 : int.Parse(txtRegisteredTeacher.Text.Trim()), txtPrincipalName.Text.Trim(),
                        txtPrincipalMobile.Text.Trim(), DateTime.Parse(txtEnrollmentDate.Text), int.Parse(rdoInstitutionType.SelectedValue), strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP, int.Parse(ddlTitle.SelectedValue.Trim())).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblSchoolImage tbl = new tblSchoolImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.SchoolAutoId = res.SchoolAutoId;
                            tbl.SchoolPic = bytUpfile; //getting complete url  
                            tbl.FileType = "";
                            tbl.FileSize = bytUpfile.Length;
                            tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                            tbl.CaptureRemarks = txtCaptureRemarks.Text;
                            tbl.UserId = strLoginUserID;
                            tbl.EntDate = DateTime.Now;
                            tbl.EntOperation = "";
                            tbl.EntTerminal = strTerminalId;
                            tbl.EntTerminalIP = strTerminalIP;
                            dx.tblSchoolImages.Add(tbl);
                            dx.SaveChanges();
                        }

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
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfSchoolIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblSchool_Delete(Convert.ToInt32(hfSchoolIDPKID.Value)).FirstOrDefault();

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
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private bool ValidateInput()
        {
            ClearValidation();
            if (txtSchoolName.Text.Trim() == "")
            {
                lbl_error.Text = "School Name is required.";
                txtSchoolName.Focus();
                return false;
            }

            if (txtAddress1.Text.Trim() == "")
            {
                lbl_error.Text = "Address is required.";
                txtAddress1.Focus();
                return false;
            }

            if (txtDistrict.Text.Trim() == "")
            {
                lbl_error.Text = "District is required.";
                txtDistrict.Focus();
                return false;
            }

            if (txtTown.Text.Trim() == "")
            {
                lbl_error.Text = "Town is required.";
                txtTown.Focus();
                return false;
            }

            if (chkPrimary.Checked == false && chkSecondary.Checked == false)
            {
                lbl_error.Text = "School Level is required. Please select either Primary or Secondary or both options.";
                chkPrimary.Focus();
                return false;
            }

            if (txtRegisteredStudent.Text.Trim() == "")
            {
                lbl_error.Text = "Registered Student is required.";
                txtRegisteredStudent.Focus();
                return false;
            }

            if (txtRegisteredTeacher.Text.Trim() == "")
            {
                lbl_error.Text = "Registered Teacher is required.";
                txtRegisteredTeacher.Focus();
                return false;
            }

            if (txtPrincipalName.Text.Trim() == "")
            {
                lbl_error.Text = "Principal Name is required.";
                txtPrincipalName.Focus();
                return false;
            }

            if (ddlGender.SelectedItem.Text == "Select")
            {
                lbl_error.Text = "For Gender is required.";
                ddlGender.Focus();
                return false;
            }

            if (ddlTitle.SelectedItem.Text == "Select")
            {
                lbl_error.Text = "Title is required.";
                ddlTitle.Focus();
                return false;
            }

            if (hfSchoolIDPKID.Value == "0")
            {
                if (chkNotRequired.Checked == false)
                {
                    if (SchoolImage.ImageUrl == "/Captures/SchoolDefaultImage.jpg")
                    {
                        lbl_error.Text = "School Picture is required.";
                        chkNotRequired.Focus();
                        return false;
                    }

                    if (txtCaptureDate.Text.Trim() == "")
                    {
                        lbl_error.Text = "Capture Date is required.";
                        txtCaptureDate.Focus();
                        return false;
                    }

                    if (txtCaptureRemarks.Text.Trim() == "")
                    {
                        lbl_error.Text = "Capture Remarks is required.";
                        txtCaptureRemarks.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private void ClearForm()
        {
            InitForm();

            rdoInstitutionType.SelectedValue = "0";
            hfSchoolIDPKID.Value = "0";
            //hfLookupResult_School.Value = "0";
            ClearValidation();

            txtEnrollmentDate.Text = "";
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtDistrict.Text = "";
            txtTown.Text = "";
            txtCity.Text = "Karachi";
            txtTelephoneLandline.Text = "";
            txtTelephoneCell.Text = "";

            chkPrimary.Checked = false;
            chkSecondary.Checked = false;

            BindCombos();

            ddlGender.SelectedIndex = 0;

            txtRegisteredStudent.Text = "";
            txtRegisteredTeacher.Text = "";

            ddlTitle.SelectedIndex = 0;
            txtPrincipalName.Text = "";
            txtPrincipalMobile.Text = "";

            txtCaptureDate.Text = "";
            txtCaptureRemarks.Text = "";

            SchoolImage.ImageUrl = null;
            SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/SchoolDefaultImage.jpg");

            chkNotRequired.Checked = false;

            txtSchoolName.Focus();
        }

        private void ClearValidation()
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
                string str = ex.Message + " - " + ex.Source;
            }

            return lst.ToArray();

        }

        private void BindCombos()
        {

            try
            {
                var dtGenderForSchool = (from a in dx.tblGenderForSchools
                                         select a).ToList();

                if (dtGenderForSchool.Count != 0)
                {
                    ddlGender.DataSource = dtGenderForSchool;
                    ddlGender.DataValueField = "GenderSchoolAutoId";
                    ddlGender.DataTextField = "Gender";
                    ddlGender.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlGender.Items.Insert(0, item);
                }

                var dtTitle = (from a in dx.tblTitles
                               select a).ToList();

                if (dtTitle.Count != 0)
                {
                    ddlTitle.DataSource = dtTitle;
                    ddlTitle.DataValueField = "TitleAutoId";
                    ddlTitle.DataTextField = "Title";
                    ddlTitle.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlTitle.Items.Insert(0, item);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("School")
                                  select a).ToList().ToDataTable();

                hfLookupResult.Value = "0";

                Session["lookupData"] = data;
                Session["Code"] = "School Code";
                Session["Name"] = "School Name";
                //string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User','','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResult.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(btnLookup, this.GetType(), "popup", jsReport, false);
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResult_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedPKID = string.Empty;
                selectedPKID = hfLookupResult.Value;

                hfSchoolIDPKID.Value = selectedPKID; //to allow update mode

                LoadSchoolDetail(selectedPKID);

                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfSchoolIDPKID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string selectedPKID = string.Empty;
                selectedPKID = hfSchoolIDPKID.Value;

                LoadSchoolDetail(selectedPKID);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        private void LoadSchoolDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    txtEnrollmentDate.Text = "";
                    var dt = dx.sp_tblSchool_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

                    rdoInstitutionType.SelectedValue = dt.InstitutionType.ToString();

                    txtSchoolCode.Text = dt.SchoolCode;

                    txtSchoolName.Text = dt.SchoolName;
                    txtAddress1.Text = dt.Address1;
                    txtAddress2.Text = dt.Address2;
                    txtDistrict.Text = dt.District;
                    txtTown.Text = dt.Town;
                    txtCity.Text = dt.City;
                    txtTelephoneLandline.Text = dt.Telephone;
                    txtTelephoneCell.Text = dt.Cellphone;
                    if (dt.SchoolLevel_Primary == 0)
                    {
                        chkPrimary.Checked = false;
                    }
                    else
                    {
                        chkPrimary.Checked = true;
                    }

                    if (dt.SchoolLevel_Secondary == 0)
                    {
                        chkSecondary.Checked = false;
                    }
                    else
                    {
                        chkSecondary.Checked = true;
                    }
                    ddlGender.SelectedValue = dt.GenderAutoId.ToString();
                    txtRegisteredStudent.Text = dt.Registered_Students.ToString();
                    txtRegisteredTeacher.Text = dt.Registered_Teachers.ToString();

                    ddlTitle.SelectedValue = dt.TitleAutoId.ToString();
                    txtPrincipalName.Text = dt.Principal_Name;
                    txtPrincipalMobile.Text = dt.Principal_Mobile;

                    txtEnrollmentDate.Text = Convert.ToDateTime(dt.EnrollmentDate.ToString()).ToString("dd-MMM-yyyy");

                    var dtImage = dx.sp_tblSchoolImage_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    if (dtImage != null)
                    {
                        byte[] imagem = (byte[])(dtImage.SchoolPic);
                        string base64String = Convert.ToBase64String(imagem);
                        SchoolImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);

                        txtCaptureDate.Text = DateTime.Parse(dtImage.CaptureDate.ToString()).ToString("dd-MMM-yyyy");
                        txtCaptureRemarks.Text = dtImage.CaptureRemarks;
                        chkNotRequired.Checked = false;
                    }
                    else
                    {
                        txtCaptureDate.Text = "";
                        txtCaptureRemarks.Text = "";

                        SchoolImage.ImageUrl = null;
                        SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/SchoolDefaultImage.jpg");

                        chkNotRequired.Checked = true;
                    }

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSaveImage.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
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

        protected void txtSchoolName_TextChanged(object sender, EventArgs e)
        {
            string sSchoolName = txtSchoolName.Text;

            txtSchoolName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sSchoolName.ToLower());

            txtSchoolCode.Focus();
        }

        protected void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            string sAddress1 = txtAddress1.Text;

            txtAddress1.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sAddress1.ToLower());

            txtAddress2.Focus();
        }

        protected void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            string sAddress2 = txtAddress2.Text;

            txtAddress2.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sAddress2.ToLower());

            txtDistrict.Focus();
        }

        protected void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            string sDistrict = txtDistrict.Text;

            txtDistrict.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sDistrict.ToLower());

            txtTown.Focus();
        }

        protected void txtTown_TextChanged(object sender, EventArgs e)
        {
            string sTown = txtTown.Text;

            txtTown.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sTown.ToLower());

            txtCity.Focus();
        }

        protected void txtCity_TextChanged(object sender, EventArgs e)
        {
            string sCity = txtCity.Text;

            txtCity.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sCity.ToLower());

            txtTelephoneLandline.Focus();
        }

        protected void txtPrincipalName_TextChanged(object sender, EventArgs e)
        {
            string sPrincipalName = txtPrincipalName.Text;

            txtPrincipalName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sPrincipalName.ToLower());

            txtPrincipalMobile.Focus();
        }

        private byte[] GetUploadedImage()
        {
            byte[] imageBytes = Convert.FromBase64String(hfImageBytes.Value.Split(',')[1]);

            return imageBytes;
        }

        protected void hfImageBytes_ValueChanged(object sender, EventArgs e)
        {

            string strimgBytes = hfImageBytes.Value;

            byte[] imageBytes = Convert.FromBase64String(strimgBytes.Split(',')[1]);

            if (imageBytes.Length > 0)
            {

            }
            SchoolImage.ImageUrl = strimgBytes;
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (txtSchoolName.Text.Trim() == "")
            {
                lbl_error.Text = "School Name is required.";

                SchoolImage.ImageUrl = null;
                SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/SchoolDefaultImage.jpg");

                txtSchoolName.Focus();
                return;
            }

            Boolean fileOK = false;
            string path = Server.MapPath("~/Captures/UploadedSchoolImages/");
            if (btnBrowse.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(btnBrowse.FileName).ToLower();
                String[] allowedExtensions = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                    }
                }

                if (fileOK == false)
                {
                    lbl_error.Text = "Invalid file format. Selected file should be in (.jpg or .png) format.";
                    txtSchoolName.Focus();
                    return;
                }

                var filename = Server.MapPath(Path.Combine(@"~/Captures/UploadedSchoolImages/", txtSchoolName.Text + ".jpg"));
                //var filename = Server.MapPath( Path.Combine(@"~/Captures/UploadedSchoolImages/", "123.jpg"));
                if (File.Exists(filename))
                    File.Delete(filename);
                btnBrowse.SaveAs(filename);

                SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedSchoolImages/" + txtSchoolName.Text + ".jpg");
                //SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedSchoolImages/123.jpg");
            }
            else
            {
                SchoolImage.ImageUrl = null;
                SchoolImage.ImageUrl = ResolveUrl(@"~/Captures/SchoolDefaultImage.jpg");
            }
        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            string strLoginUserID = Utilities.GetLoginUserID();
            string strTerminalId = Utilities.getTerminalId();
            string strTerminalIP = Utilities.getTerminalIP();

            if (hfImageBytes.Value.Length > 0)
            {
                byte[] bytUpfile = GetUploadedImage();

                tblSchoolImage tbl = new tblSchoolImage();
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                tbl.SchoolAutoId = int.Parse(hfSchoolIDPKID.Value);
                tbl.SchoolPic = bytUpfile; //getting complete url  
                tbl.FileType = "";
                tbl.FileSize = bytUpfile.Length;
                tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                tbl.CaptureRemarks = txtCaptureRemarks.Text;
                tbl.UserId = strLoginUserID;
                tbl.EntDate = DateTime.Now;
                tbl.EntOperation = "";
                tbl.EntTerminal = strTerminalId;
                tbl.EntTerminalIP = strTerminalIP;
                dx.tblSchoolImages.Add(tbl);
                dx.SaveChanges();
            }
        }

        protected void btnViewAllPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/ViewPhotos.aspx?winTitle=View All Photos&FormID=SchoolEnrollment&AutoKeyID=" + hfSchoolIDPKID.Value + "','.','height=400,width=600,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookup, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }
    }
}