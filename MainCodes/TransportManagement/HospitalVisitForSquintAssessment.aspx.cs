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
    public partial class HospitalVisitForSquintAssessment : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "HospitalVisitForSquintAssessment"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                BindCombos();

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

                    string strSquintDiagRight = string.Empty;
                    for (int i = 0; i < chkListSquintDiagRight.Items.Count; i++)
                    {
                        if (chkListSquintDiagRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSquintDiagRight += chkListSquintDiagRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSquintDiagRight = strSquintDiagRight.TrimEnd(',');

                    string strSquintDiagLeft = string.Empty;
                    for (int i = 0; i < chkListSquintDiagLeft.Items.Count; i++)
                    {
                        if (chkListSquintDiagLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSquintDiagLeft += chkListSquintDiagLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSquintDiagLeft = strSquintDiagLeft.TrimEnd(',');


                    string strSuggestedTreatmentRight = string.Empty;
                    for (int i = 0; i < chkListSuggestedTreatmentRight.Items.Count; i++)
                    {
                        if (chkListSuggestedTreatmentRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSuggestedTreatmentRight += chkListSuggestedTreatmentRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSuggestedTreatmentRight = strSuggestedTreatmentRight.TrimEnd(',');

                    string strSuggestedTreatmentLeft = string.Empty;
                    for (int i = 0; i < chkListSuggestedTreatmentLeft.Items.Count; i++)
                    {
                        if (chkListSuggestedTreatmentLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSuggestedTreatmentLeft += chkListSuggestedTreatmentLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSuggestedTreatmentLeft = strSuggestedTreatmentLeft.TrimEnd(',');

                    DateTime dtRoutineCheck = DateTime.Parse("1900-01-01");
                    int iRoutineCheckup = 0;
                    if (chkRoutineCheckup.Checked == true)
                    {
                        iRoutineCheckup = 1;
                        if (txtRoutineCheckupDate.Text != "")
                        {
                            dtRoutineCheck = DateTime.Parse(txtRoutineCheckupDate.Text);
                        }
                    }

                    DateTime dtFundoscopy = DateTime.Parse("1900-01-01");
                    int iFundoscopy = 0;
                    if (chkFundoscopy.Checked == true)
                    {
                        iFundoscopy = 1;
                        if (txtFundoscopyDate.Text != "")
                        {
                            dtFundoscopy = DateTime.Parse(txtFundoscopyDate.Text);
                        }
                    }

                    DateTime dtSquintAssessment = DateTime.Parse("1900-01-01");
                    int iSquintAssessment = 0;
                    if (chkSquintAssessment.Checked == true)
                    {
                        iSquintAssessment = 1;
                        if (txtSquintAssessmentDate.Text != "")
                        {
                            dtSquintAssessment = DateTime.Parse(txtSquintAssessmentDate.Text);
                        }
                    }

                    DateTime dtFurtherAssessment = DateTime.Parse("1900-01-01");
                    int iFurtherAssessment = 0;
                    if (chkFurtherAssessment.Checked == true)
                    {
                        iFurtherAssessment = 1;
                        if (txtFurtherAssessmentDate.Text != "")
                        {
                            dtFurtherAssessment = DateTime.Parse(txtFurtherAssessmentDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitForSquintAssessmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                            Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                            Convert.ToInt32(ddlOrthoptist.SelectedValue), strSquintDiagRight, txtOtherSquintDiagRight.Text, strSquintDiagLeft, txtOtherSquintDiagLeft.Text,
                                            txtSquintDiagOthers.Text, strSuggestedTreatmentRight, strSuggestedTreatmentLeft, iRoutineCheckup, dtRoutineCheck, iFundoscopy, dtFundoscopy,
                                            iSquintAssessment, dtSquintAssessment, iFurtherAssessment, dtFurtherAssessment,
                                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

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

                    string strSquintDiagRight = string.Empty;
                    for (int i = 0; i < chkListSquintDiagRight.Items.Count; i++)
                    {
                        if (chkListSquintDiagRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSquintDiagRight += chkListSquintDiagRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSquintDiagRight = strSquintDiagRight.TrimEnd(',');

                    string strSquintDiagLeft = string.Empty;
                    for (int i = 0; i < chkListSquintDiagLeft.Items.Count; i++)
                    {
                        if (chkListSquintDiagLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSquintDiagLeft += chkListSquintDiagLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSquintDiagLeft = strSquintDiagLeft.TrimEnd(',');


                    string strSuggestedTreatmentRight = string.Empty;
                    for (int i = 0; i < chkListSuggestedTreatmentRight.Items.Count; i++)
                    {
                        if (chkListSuggestedTreatmentRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSuggestedTreatmentRight += chkListSuggestedTreatmentRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSuggestedTreatmentRight = strSuggestedTreatmentRight.TrimEnd(',');

                    string strSuggestedTreatmentLeft = string.Empty;
                    for (int i = 0; i < chkListSuggestedTreatmentLeft.Items.Count; i++)
                    {
                        if (chkListSuggestedTreatmentLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strSuggestedTreatmentLeft += chkListSuggestedTreatmentLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strSuggestedTreatmentLeft = strSuggestedTreatmentLeft.TrimEnd(',');

                    DateTime dtRoutineCheck = DateTime.Parse("1900-01-01");
                    int iRoutineCheckup = 0;
                    if (chkRoutineCheckup.Checked == true)
                    {
                        iRoutineCheckup = 1;
                        if (txtRoutineCheckupDate.Text != "")
                        {
                            dtRoutineCheck = DateTime.Parse(txtRoutineCheckupDate.Text);
                        }
                    }

                    DateTime dtFundoscopy = DateTime.Parse("1900-01-01");
                    int iFundoscopy = 0;
                    if (chkFundoscopy.Checked == true)
                    {
                        iFundoscopy = 1;
                        if (txtRoutineCheckupDate.Text != "")
                        {
                            dtFundoscopy = DateTime.Parse(txtRoutineCheckupDate.Text);
                        }
                    }

                    DateTime dtSquintAssessment = DateTime.Parse("1900-01-01");
                    int iSquintAssessment = 0;
                    if (chkSquintAssessment.Checked == true)
                    {
                        iSquintAssessment = 1;
                        if (txtSquintAssessmentDate.Text != "")
                        {
                            dtSquintAssessment = DateTime.Parse(txtSquintAssessmentDate.Text);
                        }
                    }

                    DateTime dtFurtherAssessment = DateTime.Parse("1900-01-01");
                    int iFurtherAssessment = 0;
                    if (chkFurtherAssessment.Checked == true)
                    {
                        iFurtherAssessment = 1;
                        if (txtFurtherAssessmentDate.Text != "")
                        {
                            dtFurtherAssessment = DateTime.Parse(txtFurtherAssessmentDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitForSquintAssessmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                            Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                            Convert.ToInt32(ddlOrthoptist.SelectedValue), strSquintDiagRight, txtOtherSquintDiagRight.Text, strSquintDiagLeft, txtOtherSquintDiagLeft.Text,
                                            txtSquintDiagOthers.Text, strSuggestedTreatmentRight, strSuggestedTreatmentLeft, iRoutineCheckup, dtRoutineCheck, iFundoscopy, dtFundoscopy,
                                            iSquintAssessment, dtSquintAssessment, iFurtherAssessment, dtFurtherAssessment,
                                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

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
                    var res = dx.sp_tblVisitForSquintAssessmentStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            if (ddlHospital.SelectedValue == "0")
            {
                lbl_error.Text = "Hospital is required.";
                ddlHospital.Focus();
                return false;
            }

            if (ddlOphthalmologist.SelectedValue == "0")
            {
                lbl_error.Text = "Ophthalmologist is required.";
                ddlOphthalmologist.Focus();
                return false;
            }

            if (ddlOrthoptist.SelectedValue == "0")
            {
                lbl_error.Text = "Orthoptist is required.";
                ddlOrthoptist.Focus();
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

            //chkListSquintDiagRight.Items.Clear();
            chkListSquintDiagRight.ClearSelection();
            //chkListSquintDiagRight.DataSource = null;
            //chkListSquintDiagRight.DataBind();

            chkListSquintDiagLeft.ClearSelection();
            //chkListSquintDiagLeft.Items.Clear();
            //chkListSquintDiagLeft.DataSource = null;
            //chkListSquintDiagLeft.DataBind();

            txtOtherSquintDiagRight.Text = "";
            txtOtherSquintDiagLeft.Text = "";
            txtSquintDiagOthers.Text = "";

            //chkListSuggestedTreatmentRight.Items.Clear();
            //chkListSuggestedTreatmentRight.DataSource = null;
            //chkListSuggestedTreatmentRight.DataBind();

            //chkListSuggestedTreatmentLeft.Items.Clear();
            //chkListSuggestedTreatmentLeft.DataSource = null;
            //chkListSuggestedTreatmentLeft.DataBind();

            chkListSuggestedTreatmentRight.ClearSelection();
            chkListSuggestedTreatmentLeft.ClearSelection();

            chkRoutineCheckup.Checked = false;
            txtRoutineCheckupDate.Text = "";

            chkFundoscopy.Checked = false;
            txtFundoscopyDate.Text = "";

            chkSquintAssessment.Checked = false;
            txtSquintAssessmentDate.Text = "";

            chkFurtherAssessment.Checked = false;
            txtFurtherAssessmentDate.Text = "";

            BindCombos();

            hfLookupResultSchool.Value = "0";
            hfLookupResultStudent.Value = "0";

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
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Student_School_SquintAssessment(iType)
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_SquintAssessment(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
                                  select a).ToList().ToDataTable();

                hfLookupResultStudent.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Student Code";
                Session["Name"] = "Student Name";
                Session["FatherName"] = "Father Name";
                Session["Description"] = "School Name";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlFatherName.aspx?winTitle=Select User&hfName=" + hfLookupResultStudent.ID + "','.','height=600,width=650,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

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

                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        var dtGlassDespensePreviousData = dx.sp_tblVisitForSquintAssessmentStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtGlassDespensePreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtGlassDespensePreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitForSquintAssessmentStudentId";
                                ddlPreviousTestResult.DataTextField = "VisitForSquintAssessmentStudentTransDate";
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
            Response.Redirect("~/HospitalVisitForSquintAssessment.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HospitalVisitForSquintAssessment.aspx");
        }

        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string sStudentName = txtStudentName.Text;

            txtStudentName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sStudentName.ToLower());

            txtTestDate.Focus();
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
                var dt = dx.sp_tblVisitForSquintAssessmentStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                hfAutoRefTestTransID.Value = dt.VisitForSquintAssessmentStudentId.ToString();
                //hfAutoRefTestTransDate.Value = dt.VisitforSurgeryStudentTransDate.ToString();

                LoadStudentDetail(dt.StudentAutoId.ToString());

                ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                ddlOphthalmologist.SelectedValue = dt.DoctorAutoId_Ophthalmologist.ToString();
                ddlOrthoptist.SelectedValue = dt.DoctorAutoId_Orthoptist.ToString();

                string strSquintDiagnosis_RightEye = dt.SquintDiagnosis_RightEye.ToString();
                string[] items_Right = strSquintDiagnosis_RightEye.Split(',');
                for (int i = 0; i < chkListSquintDiagRight.Items.Count; i++)
                {
                    if (items_Right.Contains(chkListSquintDiagRight.Items[i].Value))
                    {
                        chkListSquintDiagRight.Items[i].Selected = true;
                    }
                }

                txtOtherSquintDiagRight.Text = dt.SquintDiagnosisRemarks_RightEye.ToString();

                string strSquintDiagnosis_LeftEye = dt.SquintDiagnosis_LeftEye.ToString();
                string[] items_Left = strSquintDiagnosis_LeftEye.Split(',');
                for (int i = 0; i < chkListSquintDiagLeft.Items.Count; i++)
                {
                    if (items_Left.Contains(chkListSquintDiagLeft.Items[i].Value))
                    {
                        chkListSquintDiagLeft.Items[i].Selected = true;
                    }
                }

                txtOtherSquintDiagLeft.Text = dt.SquintDiagnosisRemarks_LeftEye.ToString();

                txtSquintDiagOthers.Text = dt.Remarks_Others.ToString();

                string strSuggestedTreatment_RightEye = dt.SuggestedTreatment_RightEye.ToString();
                string[] itemsSuggestedTreatment_Right = strSuggestedTreatment_RightEye.Split(',');
                for (int i = 0; i < chkListSuggestedTreatmentRight.Items.Count; i++)
                {
                    if (itemsSuggestedTreatment_Right.Contains(chkListSuggestedTreatmentRight.Items[i].Value))
                    {
                        chkListSuggestedTreatmentRight.Items[i].Selected = true;
                    }
                }

                string strSuggestedTreatment_LeftEye = dt.SuggestedTreatment_LeftEye.ToString();
                string[] itemsSuggestedTreatment_Left = strSuggestedTreatment_RightEye.Split(',');
                for (int i = 0; i < chkListSuggestedTreatmentLeft.Items.Count; i++)
                {
                    if (itemsSuggestedTreatment_Left.Contains(chkListSuggestedTreatmentLeft.Items[i].Value))
                    {
                        chkListSuggestedTreatmentLeft.Items[i].Selected = true;
                    }
                }

                if (Convert.ToInt32(dt.Visit_RoutineCheck.ToString()) == 0)
                {
                    chkRoutineCheckup.Checked = false;
                    txtRoutineCheckupDate.Text = "";
                }
                else
                {
                    chkRoutineCheckup.Checked = true;
                    txtRoutineCheckupDate.Text = dt.Visit_RoutineCheckDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.Visit_RoutineCheckDate.ToString()).ToString("dd-MMM-yyyy");
                }



                if (Convert.ToInt32(dt.Visit_FundoScopy.ToString()) == 0)
                {
                    chkFundoscopy.Checked = false;
                    txtFundoscopyDate.Text = "";
                }
                else
                {
                    chkFundoscopy.Checked = true;
                    txtFundoscopyDate.Text = dt.Visit_FundoScopyDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.Visit_FundoScopyDate.ToString()).ToString("dd-MMM-yyyy");
                }


                if (Convert.ToInt32(dt.Visit_SquintAssessment.ToString()) == 0)
                {
                    chkSquintAssessment.Checked = false;
                    txtSquintAssessmentDate.Text = "";
                }
                else
                {
                    chkSquintAssessment.Checked = true;
                    txtSquintAssessmentDate.Text = dt.SquintAssessmentDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.SquintAssessmentDate.ToString()).ToString("dd-MMM-yyyy");
                }



                if (Convert.ToInt32(dt.Visit_FurtherAssessment.ToString()) == 0)
                {
                    chkFurtherAssessment.Checked = false;
                    txtFurtherAssessmentDate.Text = "";
                }
                else
                {
                    chkFurtherAssessment.Checked = true;
                    txtFurtherAssessmentDate.Text = dt.FurtherAssessmentDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.FurtherAssessmentDate.ToString()).ToString("dd-MMM-yyyy");
                }




                btnEdit.Visible = true;
                btnDelete.Visible = true;
                btnSaveImage.Visible = true;
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

        private void BindCombos()
        {
            try
            {
                var dtHospital = (from a in dx.tblHospitals select a).ToList();
                if (dtHospital.Count != 0)
                {
                    ddlHospital.DataSource = dtHospital;
                    ddlHospital.DataValueField = "HospitalAutoId";
                    ddlHospital.DataTextField = "HospitalDescription";
                    ddlHospital.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlHospital.Items.Insert(0, item);
                }

                string q = "SELECT * FROM tblDoctor WHERE Ophthalmologist = 1";
                var dtOphthalmologist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtOphthalmologist.Count != 0)
                {
                    ddlOphthalmologist.DataSource = dtOphthalmologist;
                    ddlOphthalmologist.DataValueField = "DoctorAutoId";
                    ddlOphthalmologist.DataTextField = "DoctorDescription";
                    ddlOphthalmologist.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlOphthalmologist.Items.Insert(0, item);

                }

                q = "SELECT * FROM tblDoctor WHERE Orthoptist = 1";
                var dtOrthoptist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtOrthoptist.Count != 0)
                {
                    ddlOrthoptist.DataSource = dtOrthoptist;
                    ddlOrthoptist.DataValueField = "DoctorAutoId";
                    ddlOrthoptist.DataTextField = "DoctorDescription";
                    ddlOrthoptist.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlOrthoptist.Items.Insert(0, item);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }

        }

        protected void ddlHospital_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iHospital = int.Parse(ddlHospital.SelectedValue.ToString());

            string q = "SELECT * FROM tblDoctor where HospitalAutoID =" + iHospital + " AND Ophthalmologist = 1";
            var dtOphthalmologist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors where a.HospitalAutoID == iHospital select a).ToList();
            if (dtOphthalmologist.Count != 0)
            {
                ddlOphthalmologist.DataSource = dtOphthalmologist;
                ddlOphthalmologist.DataValueField = "DoctorAutoId";
                ddlOphthalmologist.DataTextField = "DoctorDescription";
                ddlOphthalmologist.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlOphthalmologist.Items.Insert(0, item);
            }
            else
            {
                ddlOphthalmologist.Items.Clear();
                ddlOphthalmologist.DataSource = null;
                ddlOphthalmologist.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlOphthalmologist.Items.Insert(0, item);
            }

            q = "SELECT * FROM tblDoctor where HospitalAutoID =" + iHospital + " AND Orthoptist = 1";
            var dtOrthoptist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors where a.HospitalAutoID == iHospital select a).ToList();
            if (dtOrthoptist.Count != 0)
            {
                ddlOrthoptist.DataSource = dtOrthoptist;
                ddlOrthoptist.DataValueField = "DoctorAutoId";
                ddlOrthoptist.DataTextField = "DoctorDescription";
                ddlOrthoptist.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlOrthoptist.Items.Insert(0, item);
            }
            else
            {
                ddlOrthoptist.Items.Clear();
                ddlOrthoptist.DataSource = null;
                ddlOrthoptist.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlOrthoptist.Items.Insert(0, item);
            }
        }
    }
}