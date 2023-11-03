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
    public partial class FollowUpVisitDispensGlasses_OldCopy : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitForm();

                rdoOldNewTest_SelectedIndexChanged(null, null);
                txtTestDate.Text = Utilities.GetDate();

                txtSchoolName.Focus();
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
                if (ValidateInput())
                {
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    int iVisionwithGlasses_RightEye = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iVisionwithGlasses_RightEye = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }

                    int iVisionwithGlasses_LeftEye = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iVisionwithGlasses_LeftEye = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }

                    int iStudentSatisficaion = -1;
                    if (rdoStudentSatisficaion.SelectedValue != "")
                    {
                        iStudentSatisficaion = int.Parse(rdoStudentSatisficaion.SelectedValue);
                    }

                    int iListStudentNotSatisfiedOpt = -1;
                    if (rdoListStudentNotSatisfiedOpt.SelectedValue != "")
                    {
                        iListStudentNotSatisfiedOpt = int.Parse(rdoListStudentNotSatisfiedOpt.SelectedValue);
                    }

                    int iRefractionReason = -1;
                    if (rdoRefractionReason.SelectedValue != "")
                    {
                        iRefractionReason = int.Parse(rdoRefractionReason.SelectedValue);
                    }

                    string sSpherical_RightEyeType = string.Empty;
                    string sCyclinderical_RightEyeType = string.Empty;
                    string sNear_RightEyeType = string.Empty;

                    string sSpherical_LeftEyeType = string.Empty;
                    string sCyclinderical_LeftEyeType = string.Empty;
                    string sNear_LeftEyeType = string.Empty;

                    if (ddlSpherical_RightEye.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType = "P";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType = "N";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType = "O";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType = "E";
                    }

                    if (ddlCyclinderical_RightEye.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    else { sCyclinderical_RightEyeType = "N"; }

                    if (ddlNear_RightEye.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    else { sNear_RightEyeType = "N"; }

                    decimal dtxtSpherical_RightEye = 0;
                    decimal dCyclinderical_RightEye = 0;
                    decimal dNear_RightEye = 0;
                    try
                    {
                        dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                        dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());
                        dNear_RightEye = decimal.Parse(txtNear_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dtxtSpherical_RightEye = 0;
                        dCyclinderical_RightEye = 0;
                        dNear_RightEye = 0;
                    }

                    int dAxixA_RightEye = 0;
                    int dAxixB_RightEye = 0;
                    try
                    {
                        dAxixA_RightEye = int.Parse(txtAxixA_RightEye.Text.Trim());
                        dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_RightEye = 0;
                        dAxixB_RightEye = 0;
                    }

                    if (ddlSpherical_LeftEye.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType = "P";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType = "N";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType = "O";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType = "E";
                    }

                    if (ddlCyclinderical_LeftEye.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    else { sCyclinderical_LeftEyeType = "N"; }

                    if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    else { sNear_LeftEyeType = "N"; }

                    decimal dtxtSpherical_LeftEye = 0;
                    decimal dCyclinderical_LeftEye = 0;
                    decimal dNear_LeftEye = 0;

                    try
                    {
                        dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                        dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());
                        dNear_LeftEye = decimal.Parse(txtNear_LeftEye.Text.Trim());
                    }
                    catch
                    {
                        dtxtSpherical_LeftEye = 0;
                        dCyclinderical_LeftEye = 0;
                        dNear_LeftEye = 0;
                    }

                    int dAxixA_LeftEye = 0;
                    int dAxixB_LeftEye = 0;

                    try
                    {
                        dAxixA_LeftEye = int.Parse(txtAxixA_LeftEye.Text.Trim());
                        dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_LeftEye = 0;
                        dAxixB_LeftEye = 0;
                    }

                    int iFollowUpVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iFollowUpVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iFollowUpVisit = 1;
                    }

                    var res = dx.sp_tblGlassDespenseStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                            Convert.ToInt32(hfStudentIDPKID.Value), iVisionwithGlasses_RightEye, iVisionwithGlasses_LeftEye, iStudentSatisficaion, iListStudentNotSatisfiedOpt, txtStudentNotSatisfiedOtherComment.Text,
                                            iRefractionReason, sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                                            sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,
                                            iFollowUpVisit, strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblStudentImage tbl = new tblStudentImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.StudentAutoId = int.Parse(res.StudentAutoId.ToString());
                            tbl.StudentPic = bytUpfile; //getting complete url  
                            tbl.FileType = "";
                            tbl.FileSize = bytUpfile.Length;
                            tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                            tbl.CaptureRemarks = txtCaptureRemarks.Text;
                            tbl.UserId = strLoginUserID;
                            tbl.EntDate = DateTime.Now;
                            tbl.EntOperation = "";
                            tbl.EntTerminal = strTerminalId;
                            tbl.EntTerminalIP = strTerminalIP;
                            dx.tblStudentImages.Add(tbl);
                            dx.SaveChanges();
                        }

                        lbl_error.Text = res.RetMessage;

                        ClearForm(false);
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

        private byte[] GetUploadedImage()
        {
            byte[] imageBytes = Convert.FromBase64String(hfImageBytes.Value.Split(',')[1]);

            return imageBytes;
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

                    int iVisionwithGlasses_RightEye = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iVisionwithGlasses_RightEye = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }

                    int iVisionwithGlasses_LeftEye = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iVisionwithGlasses_LeftEye = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }

                    int iStudentSatisficaion = -1;
                    if (rdoStudentSatisficaion.SelectedValue != "")
                    {
                        iStudentSatisficaion = int.Parse(rdoStudentSatisficaion.SelectedValue);
                    }

                    int iListStudentNotSatisfiedOpt = -1;
                    if (rdoListStudentNotSatisfiedOpt.SelectedValue != "")
                    {
                        iListStudentNotSatisfiedOpt = int.Parse(rdoListStudentNotSatisfiedOpt.SelectedValue);
                    }

                    int iRefractionReason = -1;
                    if (rdoRefractionReason.SelectedValue != "")
                    {
                        iRefractionReason = int.Parse(rdoRefractionReason.SelectedValue);
                    }

                    string sSpherical_RightEyeType = string.Empty;
                    string sCyclinderical_RightEyeType = string.Empty;
                    string sNear_RightEyeType = string.Empty;

                    string sSpherical_LeftEyeType = string.Empty;
                    string sCyclinderical_LeftEyeType = string.Empty;
                    string sNear_LeftEyeType = string.Empty;

                    if (ddlSpherical_RightEye.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType = "P";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType = "N";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType = "O";
                    }
                    else if (ddlSpherical_RightEye.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType = "E";
                    }

                    if (ddlCyclinderical_RightEye.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    else { sCyclinderical_RightEyeType = "N"; }

                    if (ddlNear_RightEye.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    else { sNear_RightEyeType = "N"; }

                    decimal dtxtSpherical_RightEye = 0;
                    decimal dCyclinderical_RightEye = 0;
                    decimal dNear_RightEye = 0;
                    try
                    {
                        dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                        dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());
                        dNear_RightEye = decimal.Parse(txtNear_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dtxtSpherical_RightEye = 0;
                        dCyclinderical_RightEye = 0;
                        dNear_RightEye = 0;
                    }

                    int dAxixA_RightEye = 0;
                    int dAxixB_RightEye = 0;
                    try
                    {
                        dAxixA_RightEye = int.Parse(txtAxixA_RightEye.Text.Trim());
                        dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_RightEye = 0;
                        dAxixB_RightEye = 0;
                    }

                    if (ddlSpherical_LeftEye.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType = "P";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType = "N";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType = "O";
                    }
                    else if (ddlSpherical_LeftEye.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType = "E";
                    }

                    if (ddlCyclinderical_LeftEye.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    else { sCyclinderical_LeftEyeType = "N"; }

                    if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    else { sNear_LeftEyeType = "N"; }

                    decimal dtxtSpherical_LeftEye = 0;
                    decimal dCyclinderical_LeftEye = 0;
                    decimal dNear_LeftEye = 0;

                    try
                    {
                        dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                        dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());
                        dNear_LeftEye = decimal.Parse(txtNear_LeftEye.Text.Trim());
                    }
                    catch
                    {
                        dtxtSpherical_LeftEye = 0;
                        dCyclinderical_LeftEye = 0;
                        dNear_LeftEye = 0;
                    }

                    int dAxixA_LeftEye = 0;
                    int dAxixB_LeftEye = 0;

                    try
                    {
                        dAxixA_LeftEye = int.Parse(txtAxixA_LeftEye.Text.Trim());
                        dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_LeftEye = 0;
                        dAxixB_LeftEye = 0;
                    }

                    int iFollowUpVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iFollowUpVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iFollowUpVisit = 1;
                    }


                    var res = dx.sp_tblGlassDespenseStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                                                Convert.ToInt32(hfStudentIDPKID.Value), iVisionwithGlasses_RightEye, iVisionwithGlasses_LeftEye, iStudentSatisficaion, iListStudentNotSatisfiedOpt, txtStudentNotSatisfiedOtherComment.Text,
                                                                iRefractionReason, sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                                                                sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,
                                                                iFollowUpVisit, strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] ImageData = GetUploadedImage();

                            tblStudentImage tbl = new tblStudentImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.StudentAutoId = int.Parse(res.StudentAutoId.ToString());
                            tbl.StudentPic = ImageData; //getting complete url  
                            tbl.FileType = "";
                            tbl.FileSize = ImageData.Length;
                            tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                            tbl.CaptureRemarks = txtCaptureRemarks.Text;
                            tbl.UserId = strLoginUserID;
                            tbl.EntDate = DateTime.Now;
                            tbl.EntOperation = "";
                            tbl.EntTerminal = strTerminalId;
                            tbl.EntTerminalIP = strTerminalIP;
                            dx.tblStudentImages.Add(tbl);
                            dx.SaveChanges();

                        }

                        lbl_error.Text = res.RetMessage;

                        ClearForm(false);
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
                if (Convert.ToInt32(hfStudentIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblGlassDespenseStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm(true);
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
            DateTime dtTestDate;
            try
            {
                dtTestDate = DateTime.Parse(txtTestDate.Text);
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Date Format.";
                txtTestDate.Focus();
                return false;
            }
            if (txtSchoolCode.Text.Trim() == "")
            {
                lbl_error.Text = "School Code is required.";
                txtSchoolCode.Focus();
                return false;
            }
            if (txtSchoolName.Text.Trim() == "")
            {
                lbl_error.Text = "School Name is required.";
                txtSchoolName.Focus();
                return false;
            }

            if (txtStudentName.Text.Trim() == "")
            {
                lbl_error.Text = "Student Name is required.";
                txtStudentName.Focus();
                return false;
            }


            if (chkNotRequired.Checked == false)
            {
                if (StudentImage.ImageUrl == "~/Captures/StudentDefaultImage.jpg")
                {
                    lbl_error.Text = "Student Picture is required.";
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
            return true;
        }

        private void ClearForm(bool bValue)
        {
            InitForm();

            ClearValidation();

            hfSchoolIDPKID.Value = "0";
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";

            txtTestDate.Text = Utilities.GetDate();

            hfAutoRefTestIDPKID.Value = "0";
            hfStudentIDPKID.Value = "0";

            txtStudentCode.Text = "";
            txtStudentName.Text = "";

            lblFatherName_Student.Text = "";
            lblAge_Student.Text = "";
            lblDecreasedVision_Student.Text = "";
            lblGender_Student.Text = "";
            lblClass_Student.Text = "";
            lblSchoolName_Student.Text = "";

            rdoOldNewTest.SelectedValue = "0";
            rdoOldNewTest_SelectedIndexChanged(null, null);

            ddlOptometristTestDate.Items.Clear();
            ddlOptometristTestDate.DataSource = null;
            ddlOptometristTestDate.DataBind();

            lblSpherical_RightEye.Text = "";
            lblCylinderical_RightEye.Text = "";
            lblAxix_RightEye.Text = "";
            lblNearAdd_RightEye.Text = "";
            lblSpherical_LeftEye.Text = "";
            lblCylinderical_LeftEye.Text = "";
            lblAxix_LeftEye.Text = "";
            lblNearAdd_LeftEye.Text = "";

            hfLookupResultSchool.Value = "0";
            hfLookupResultStudent.Value = "0";

            rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
            rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;

            rdoFollowupAfterSixMonths.Checked = false;
            rdoNotRequiredFollowup.Checked = false;

            rdoListStudentNotSatisfiedOpt.SelectedIndex = -1;
            rdoStudentSatisficaion.SelectedIndex = -1;

            rdoRefractionReason.SelectedIndex = -1;

            ddlSpherical_RightEye.SelectedIndex = 0;
            txtSpherical_RightEye.Text = "";

            ddlCyclinderical_RightEye.SelectedIndex = 0;
            txtCyclinderical_RightEye.Text = "";

            txtAxixA_RightEye.Text = "";
            txtAxixB_RightEye.Text = "";

            ddlNear_RightEye.SelectedIndex = 0;
            txtNear_RightEye.Text = "";

            ddlSpherical_LeftEye.SelectedIndex = 0;
            txtSpherical_LeftEye.Text = "";

            ddlCyclinderical_LeftEye.SelectedIndex = 0;
            txtCyclinderical_LeftEye.Text = "";

            txtAxixA_LeftEye.Text = "";
            txtAxixB_LeftEye.Text = "";

            ddlNear_LeftEye.SelectedIndex = 0;
            txtNear_LeftEye.Text = "";

            hfImageBytes.Value = "";

            txtCaptureDate.Text = "";
            txtCaptureRemarks.Text = "";

            StudentImage.ImageUrl = null;
            StudentImage.ImageUrl = ResolveUrl(@"~/Captures/StudentDefaultImage.jpg");

            txtSchoolCode.Focus();
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

        protected void btnWebCam_Click(object sender, EventArgs e)
        {
            if (txtStudentName.Text.Trim() == "")
            {
                lbl_error.Text = "Student Name is required.";
                txtStudentName.Focus();
                return;
            }

            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('WebCam2.aspx?vFormName=WebCam.aspx&hfName=" + hfImageBytes.ID + "&Type=1&ImageName_Student=" + txtStudentName.Text.Trim() + "','.','height=800,width=800,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popReport", jsReport, false);
        }

        protected void btnLookupSchool_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("School")
                                  select a).ToList().ToDataTable();

                hfLookupResultSchool.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "School Code";
                Session["Name"] = "School Name";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResultSchool.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupSchool, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfSchoolIDPKID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string SchoolIDPKID = string.Empty;
                SchoolIDPKID = hfSchoolIDPKID.Value;

                if (Convert.ToUInt32(SchoolIDPKID) > 0)
                {
                    var dt = dx.sp_tblSchool_GetDetail(Convert.ToInt32(SchoolIDPKID)).SingleOrDefault();

                    txtSchoolCode.Text = dt.SchoolCode;
                    txtSchoolName.Text = dt.SchoolName;
                }

                //var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
                //txtStudentCode.Text = studentCode;

                lbl_error.Text = "";

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResultSchool_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultSchool.Value;
            hfSchoolIDPKID.Value = selectedPKID; //to allow update mode

            LoadSchoolDetail(selectedPKID);
            lbl_error.Text = "";
        }

        private void LoadSchoolDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblSchool_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    txtSchoolCode.Text = dt.SchoolCode;
                    txtSchoolName.Text = dt.SchoolName;
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnLookupStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Student_GlassDispense(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
                                  select a).ToList().ToDataTable();

                hfLookupResultStudent.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Student Code";
                Session["Name"] = "Student Name";
                Session["Description"] = "School Name";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlMultiColumn.aspx?winTitle=Select User&hfName=" + hfLookupResultStudent.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(btnLookupStudent, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfStudentIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string StudentIDPKID = string.Empty;
            StudentIDPKID = hfStudentIDPKID.Value;

            LoadStudentDetail(StudentIDPKID);
            hfLookupResultStudent.Value = "0";

            rdoOldNewTest.SelectedValue = "0";
            rdoOldNewTest_SelectedIndexChanged(null, null);

            lbl_error.Text = "";
        }

        protected void hfLookupResultStudent_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultStudent.Value;
            hfStudentIDPKID.Value = selectedPKID; //to allow update mode

            LoadStudentDetail(selectedPKID);
            lbl_error.Text = "";
        }

        private void LoadStudentDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblStudent_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

                    if (txtSchoolCode.Text.Trim() == "")
                    {
                        txtSchoolCode.Text = dt.SchoolCode;
                        txtSchoolName.Text = dt.SchoolName;
                        hfSchoolIDPKID.Value = dt.SchoolAutoId.ToString();
                    }

                    string classSection = dt.ClassCode + " - " + dt.ClassSection;
                    if (dt.ClassSection.Trim() == "") { classSection = dt.ClassCode; }

                    txtStudentCode.Text = dt.StudentCode;
                    txtStudentName.Text = dt.StudentName;
                    lblFatherName_Student.Text = dt.FatherName;
                    lblAge_Student.Text = dt.Age.ToString();
                    lblGender_Student.Text = dt.Gender;
                    lblClass_Student.Text = classSection;
                    lblSchoolName_Student.Text = dt.SchoolName;
                    lblWearingGlasses_Student.Text = dt.WearGlasses == 0 ? "No" : "Yes";
                    lblDecreasedVision_Student.Text = dt.DecreasedVision == 0 ? "No" : "Yes";

                    if (ddlPreviousTestResult.Visible == false)
                    {
                        var dtGlassDespensePreviousData = dx.sp_tblGlassDespenseStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtGlassDespensePreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtGlassDespensePreviousData;
                                ddlPreviousTestResult.DataValueField = "GlassDespenseStudentId";
                                ddlPreviousTestResult.DataTextField = "GlassDespenseStudentTransDate";
                                ddlPreviousTestResult.DataBind();

                                ListItem item = new ListItem();
                                item.Text = "Select";
                                item.Value = "0";
                                ddlPreviousTestResult.Items.Insert(0, item);
                            }
                            else
                            {
                                ddlPreviousTestResult.Items.Clear();
                                ddlPreviousTestResult.DataSource = null;
                                ddlPreviousTestResult.DataBind();
                            }
                        }
                        catch (Exception ex)
                        {
                            string str = ex.Message + " - " + ex.Source;
                        }
                    }

                    var dtPreviousData = dx.sp_tblOptometristMasterStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlOptometristTestDate.DataSource = dtPreviousData;
                            ddlOptometristTestDate.DataValueField = "OptometristStudentId";
                            ddlOptometristTestDate.DataTextField = "OptometristStudentTransDate";
                            ddlOptometristTestDate.DataBind();

                            ListItem item = new ListItem();
                            item.Text = "Select";
                            item.Value = "0";
                            ddlOptometristTestDate.Items.Insert(0, item);

                            ddlOptometristTestDate.SelectedIndex = 1;
                            ddlOptometristTestDate_SelectedIndexChanged(null, null);
                        }
                        else
                        {
                            ddlOptometristTestDate.Items.Clear();
                            ddlOptometristTestDate.DataSource = null;
                            ddlOptometristTestDate.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            if (txtStudentName.Text.Trim() == "")
            {
                lbl_error.Text = "Student Name is required.";

                StudentImage.ImageUrl = null;
                StudentImage.ImageUrl = ResolveUrl(@"~/Captures/StudentDefaultImage.jpg");

                txtStudentName.Focus();
                return;
            }

            Boolean fileOK = false;
            string path = Server.MapPath("~/Captures/UploadedStudentImages/");
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
                    txtStudentName.Focus();
                    return;
                }

                var filename = Server.MapPath(Path.Combine(@"~/Captures/UploadedStudentImages/", txtStudentName.Text + ".jpg"));
                //var filename = Server.MapPath( Path.Combine(@"~/Captures/UploadedStudentImages/", "123.jpg"));
                if (File.Exists(filename))
                    File.Delete(filename);
                btnBrowse.SaveAs(filename);


                StudentImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedStudentImages/" + txtStudentName.Text + ".jpg");
                //StudentImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedStudentImages/123.jpg");
            }
            else
            {
                StudentImage.ImageUrl = null;
                StudentImage.ImageUrl = ResolveUrl(@"~/Captures/StudentDefaultImage.jpg");
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
            ClearForm(true);
        }

        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string sStudentName = txtStudentName.Text;

            txtStudentName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sStudentName.ToLower());

            //var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
            //txtStudentCode.Text = studentCode;

            ddlOptometristTestDate.Focus();
        }

        protected void hfImageBytes_ValueChanged(object sender, EventArgs e)
        {

            string strimgBytes = hfImageBytes.Value;

            byte[] imageBytes = Convert.FromBase64String(strimgBytes.Split(',')[1]);

            if (imageBytes.Length > 0)
            {

            }
            StudentImage.ImageUrl = strimgBytes;
        }

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            string strLoginUserID = Utilities.GetLoginUserID();
            string strTerminalId = Utilities.getTerminalId();
            string strTerminalIP = Utilities.getTerminalIP();

            if (hfImageBytes.Value.Length > 0)
            {
                byte[] bytUpfile = GetUploadedImage();

                tblStudentImage tbl = new tblStudentImage();
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                tbl.StudentAutoId = int.Parse(hfStudentIDPKID.Value);
                tbl.StudentPic = bytUpfile; //getting complete url  
                tbl.FileType = "";
                tbl.FileSize = bytUpfile.Length;
                tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                tbl.CaptureRemarks = txtCaptureRemarks.Text;
                tbl.UserId = strLoginUserID;
                tbl.EntDate = DateTime.Now;
                tbl.EntOperation = "";
                tbl.EntTerminal = strTerminalId;
                tbl.EntTerminalIP = strTerminalIP;
                dx.tblStudentImages.Add(tbl);
                dx.SaveChanges();
            }
        }

        protected void hfAutoRefTestIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string autoRefTestIDPKID = hfAutoRefTestIDPKID.Value;

            if (Convert.ToUInt32(autoRefTestIDPKID) > 0)
            {
                var dt = dx.sp_tblGlassDespenseStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                hfAutoRefTestTransID.Value = dt.GlassDespenseStudentId.ToString();
                //hfAutoRefTestTransDate.Value = dt.GlassDespenseStudentTransDate.ToString();

                LoadStudentDetail(dt.StudentAutoId.ToString());

                if (dt.VisionwithGlasses_RightEye.ToString() == "-1")
                {
                    rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                }
                else
                {
                    rdoDistanceVision_RightEye_WithGlasses.SelectedValue = dt.VisionwithGlasses_RightEye.ToString();
                }

                if (dt.VisionwithGlasses_LeftEye.ToString() == "-1")
                {
                    rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                }
                else
                {
                    rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = dt.VisionwithGlasses_LeftEye.ToString();
                }

                if (dt.StudentSatisficaion.ToString() == "-1")
                {
                    rdoStudentSatisficaion.SelectedIndex = -1;
                }
                else
                {
                    rdoStudentSatisficaion.SelectedValue = dt.StudentSatisficaion.ToString();
                }

                if (dt.Unsatisfied.ToString() == "-1")
                {
                    rdoListStudentNotSatisfiedOpt.SelectedIndex = -1;
                }
                else
                {
                    rdoListStudentNotSatisfiedOpt.SelectedValue = dt.Unsatisfied.ToString();
                }

                txtStudentNotSatisfiedOtherComment.Text = dt.Unsatisfied_Remarks.ToString();

                if (dt.Unsatisfied_Reason.ToString() == "-1")
                {
                    rdoRefractionReason.SelectedIndex = -1;
                }
                else
                {
                    rdoRefractionReason.SelectedValue = dt.Unsatisfied_Reason.ToString();
                }

                if (dt.Right_Spherical_Status == "P")
                {
                    ddlSpherical_RightEye.SelectedIndex = 0;
                }
                else if (dt.Right_Spherical_Status == "N")
                {
                    ddlSpherical_RightEye.SelectedIndex = 1;
                }
                else if (dt.Right_Spherical_Status == "O")
                {
                    ddlSpherical_RightEye.SelectedIndex = 2;
                }
                else if (dt.Right_Spherical_Status == "E")
                {
                    ddlSpherical_RightEye.SelectedIndex = 3;
                }

                if (dt.Right_Spherical_Points != null)
                {
                    if (decimal.Parse(dt.Right_Spherical_Points.ToString()) < 0)
                    {
                        txtSpherical_RightEye.Text = "";
                    }
                    else
                    {
                        txtSpherical_RightEye.Text = dt.Right_Spherical_Points.ToString();
                    }

                    if (dt.Right_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Right_Cyclinderical_Points.ToString()) < 0)
                    {
                        txtCyclinderical_RightEye.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_RightEye.Text = dt.Right_Cyclinderical_Points.ToString();
                    }

                    if (int.Parse(dt.Right_Axix_From.ToString()) < 0)
                    {
                        txtAxixA_RightEye.Text = "";
                    }
                    else
                    {
                        txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    }
                    //txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    txtAxixB_RightEye.Text = dt.Right_Axix_To.ToString();

                    if (dt.Right_Near_Status == "P")
                    {
                        ddlNear_RightEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNear_RightEye.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Right_Near_Points.ToString()) < 0)
                    {
                        txtNear_RightEye.Text = "";
                    }
                    else
                    {
                        txtNear_RightEye.Text = dt.Right_Near_Points.ToString();
                    }

                    if (dt.Left_Spherical_Status == "P")
                    {
                        ddlSpherical_LeftEye.SelectedIndex = 0;
                    }
                    else if (dt.Left_Spherical_Status == "N")
                    {
                        ddlSpherical_LeftEye.SelectedIndex = 1;
                    }
                    else if (dt.Left_Spherical_Status == "O")
                    {
                        ddlSpherical_LeftEye.SelectedIndex = 2;
                    }
                    else if (dt.Left_Spherical_Status == "E")
                    {
                        ddlSpherical_LeftEye.SelectedIndex = 3;
                    }
                    if (decimal.Parse(dt.Left_Spherical_Points.ToString()) < 0)
                    {
                        txtSpherical_LeftEye.Text = "";
                    }
                    else
                    {
                        txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();
                    }
                    //txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    if (dt.Left_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) < 0)
                    {
                        txtCyclinderical_LeftEye.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();
                    }
                    //txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    if (int.Parse(dt.Left_Axix_From.ToString()) < 0)
                    {
                        txtAxixA_LeftEye.Text = "";
                    }
                    else
                    {
                        txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    }
                    //txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    txtAxixB_LeftEye.Text = dt.Left_Axix_To.ToString();

                    if (dt.Left_Near_Status == "P")
                    {
                        ddlNear_LeftEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNear_LeftEye.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Left_Near_Points.ToString()) < 0)
                    {
                        txtNear_LeftEye.Text = "";
                    }
                    else
                    {
                        txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();
                    }
                }
                rdoNotRequiredFollowup.Checked = false;
                rdoFollowupAfterSixMonths.Checked = false;

                if (dt.FollowupRequired.ToString() == "0")
                {
                    rdoNotRequiredFollowup.Checked = true;
                }
                else if (dt.FollowupRequired.ToString() == "1")
                {
                    rdoFollowupAfterSixMonths.Checked = true;
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;
                //btnSaveImage.Visible = true;
            }
        }

        protected void ddlOptometristTestDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = ddlOptometristTestDate.SelectedValue;
            if (ID != "0")
            {
                var dtLastData = dx.sp_tblOptometristMasterStudent_GetTransactionData(Convert.ToInt32(hfStudentIDPKID.Value), DateTime.Parse(ddlOptometristTestDate.SelectedItem.Text)).SingleOrDefault();
                try
                {
                    if (dtLastData != null)
                    {
                        string Right_Spherical_Points = dtLastData.Right_Spherical_Points.ToString();
                        if (dtLastData.Right_Spherical_Status == "Plano")
                        {
                            Right_Spherical_Points = "";
                        }
                        else if (dtLastData.Right_Spherical_Status == "Error")
                        {
                            Right_Spherical_Points = "";
                        }
                        lblSpherical_RightEye.Text = dtLastData.Right_Spherical_Status + Right_Spherical_Points;
                        lblCylinderical_RightEye.Text = dtLastData.Right_Cyclinderical_Status + dtLastData.Right_Cyclinderical_Points.ToString();
                        lblAxix_RightEye.Text = dtLastData.Right_Axix_From.ToString(); // + " to " + dtLastData.Right_Axix_To.ToString();
                        lblNearAdd_RightEye.Text = dtLastData.Right_Near_Status + dtLastData.Right_Near_Points.ToString();

                        string Left_Spherical_Points = dtLastData.Left_Spherical_Points.ToString();
                        if (dtLastData.Left_Spherical_Status == "Plano")
                        {
                            Left_Spherical_Points = "";
                        }
                        else if (dtLastData.Left_Spherical_Status == "Error")
                        {
                            Left_Spherical_Points = "";
                        }
                        lblSpherical_LeftEye.Text = dtLastData.Left_Spherical_Status + Left_Spherical_Points;
                        lblCylinderical_LeftEye.Text = dtLastData.Left_Cyclinderical_Status + dtLastData.Left_Cyclinderical_Points.ToString();
                        lblAxix_LeftEye.Text = dtLastData.Left_Axix_From.ToString(); // + " to " + dtLastData.Left_Axix_To.ToString();
                        lblNearAdd_LeftEye.Text = dtLastData.Left_Near_Status + dtLastData.Left_Near_Points.ToString();
                    }
                    else
                    {
                        //txtTestDate.Text = Utilities.GetDate();
                        //txtTestDate.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    string str = ex.Message + " - " + ex.Source;
                }
            }
        }

        protected void rdoOldNewTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoOldNewTest.SelectedValue == "0")
            {
                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;
            }
            else
            {
                txtTestDate.Visible = false;
                ddlPreviousTestResult.Visible = true;
            }
        }

        protected void ddlPreviousTestResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPreviousTestResult = ddlPreviousTestResult.SelectedValue;
            hfAutoRefTestIDPKID.Value = strPreviousTestResult;
            hfAutoRefTestIDPKID_ValueChanged(null, null);
        }
    }
}