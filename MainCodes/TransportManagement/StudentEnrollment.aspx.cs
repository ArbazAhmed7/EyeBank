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
    public partial class StudentEnrollment : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "StudentEnrollment"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();
                BindCombos();

                txtTestDate.Text = Utilities.GetDate(); // DateTime.Now.AddHours(11).ToString("dd-MMM-yyyy");
                txtCaptureDate.Text = Utilities.GetDate();
                if (Utilities.GetLoginUserID().ToUpper().Trim() != "ADMIN")
                {
                    txtStudentRegNo.Enabled = false;
                }
                rdoReligion.SelectedIndex = -1;
                txtSchoolCode.Focus();
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
                    int iDecreaseVision = 0;
                    if (chkDecreasedVision.Checked == true)
                    {
                        iDecreaseVision = 1;
                    }

                    int iWearGlasses = 0;
                    if (chkWearGlasses.Checked == true)
                    {
                        iWearGlasses = 1;
                    }

                    int iOccularHistory = 0;
                    if (chkOccularHistory.Checked == true)
                    {
                        iOccularHistory = 1;
                    }

                    int iMedicalHistory = 0;
                    if (chkMedicalHistory.Checked == true)
                    {
                        iMedicalHistory = 1;
                    }

                    int iChiefComplain = 0;
                    if (chkChiefComplain.Checked == true)
                    {
                        iChiefComplain = 1;
                    }

                    int iReligion = 0;
                    if (rdoReligion.SelectedValue == "1")
                    {
                        iReligion = 1;
                    }

                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    if (txtStudentCode.Text == "")
                    {
                        var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
                        txtStudentCode.Text = studentCode;
                    }

                    var res = dx.sp_tblStudent_InsertUpdate(Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(hfSchoolIDPKID.Value),
                                            Convert.ToInt32(hfClassIDPKID.Value), Convert.ToInt32(hfSectionIDPKID.Value), txtStudentCode.Text, txtStudentName.Text, txtStudentRegNo.Text, txtFatherName.Text, Convert.ToInt32(txtAge.Text),
                                            Convert.ToInt32(ddlGender.SelectedValue), iDecreaseVision,
                                            iWearGlasses, iReligion, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text, iChiefComplain, txtChiefComplain.Text, DateTime.Parse(txtTestDate.Text),
                    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblStudentImage tbl = new tblStudentImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.StudentAutoId = res.StudentAutoId;
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
                    int iDecreaseVision = 0;
                    if (chkDecreasedVision.Checked == true)
                    {
                        iDecreaseVision = 1;
                    }

                    int iWearGlasses = 0;
                    if (chkWearGlasses.Checked == true)
                    {
                        iWearGlasses = 1;
                    }

                    int iOccularHistory = 0;
                    if (chkOccularHistory.Checked == true)
                    {
                        iOccularHistory = 1;
                    }

                    int iMedicalHistory = 0;
                    if (chkMedicalHistory.Checked == true)
                    {
                        iMedicalHistory = 1;
                    }

                    int iChiefComplain = 0;
                    if (chkChiefComplain.Checked == true)
                    {
                        iChiefComplain = 1;
                    }

                    int iReligion = 0;
                    if (rdoReligion.SelectedValue == "1")
                    {
                        iReligion = 1;
                    }

                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    var res = dx.sp_tblStudent_InsertUpdate(Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(hfSchoolIDPKID.Value),
                                            Convert.ToInt32(hfClassIDPKID.Value), Convert.ToInt32(hfSectionIDPKID.Value), txtStudentCode.Text, txtStudentName.Text, txtStudentRegNo.Text, txtFatherName.Text, Convert.ToInt32(txtAge.Text),
                                            Convert.ToInt32(ddlGender.SelectedValue), iDecreaseVision,
                                            iWearGlasses, iReligion, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text, iChiefComplain, txtChiefComplain.Text, DateTime.Parse(txtTestDate.Text),
                                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] ImageData = GetUploadedImage();

                            tblStudentImage tbl = new tblStudentImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.StudentAutoId = res.StudentAutoId;
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
                    var resImage = dx.sp_tblStudentImage_Delete(Convert.ToInt32(hfStudentIDPKID.Value)).FirstOrDefault();
                    var res = dx.sp_tblStudent_Delete(Convert.ToInt32(hfStudentIDPKID.Value)).FirstOrDefault();

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
            if (txtClassNo.Text.Trim() == "")
            {
                lbl_error.Text = "Class is required.";
                txtClassNo.Focus();
                return false;
            }

            if (txtStudentName.Text.Trim() == "")
            {
                lbl_error.Text = "Student Name is required.";
                txtStudentName.Focus();
                return false;
            }

            if (txtFatherName.Text.Trim() == "")
            {
                lbl_error.Text = "Father Name is required.";
                txtFatherName.Focus();
                return false;
            }

            if (txtAge.Text.Trim() == "")
            {
                lbl_error.Text = "Age is required.";
                txtAge.Focus();
                return false;
            }
            else
            {
                if (int.Parse(txtAge.Text.Trim()) < 4)
                {
                    lbl_error.Text = "Invalid Age: minimum 4 years is allowed";
                    txtAge.Focus();
                    return false;
                }
            }

            if (ddlGender.SelectedItem.Text == "Select")
            {
                lbl_error.Text = "Gender is required.";
                ddlGender.Focus();
                return false;
            }

            if (ddlGender.SelectedValue == "0")
            {
                lbl_error.Text = "Gender is required.";
                ddlGender.Focus();
                return false;
            }

            if (hfStudentIDPKID.Value == "0")
            {
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
            }

            if (rdoReligion.SelectedIndex == -1)
            {
                lbl_error.Text = "'Religion' is required.";
                rdoReligion.Focus();
                return false;
            }


            return true;
        }

        private void ClearForm(bool bValue)
        {
            InitForm();

            ClearValidation();
            if (bValue == true)
            {
                hfSchoolIDPKID.Value = "0";
                hfClassIDPKID.Value = "0";
                hfSectionIDPKID.Value = "0";
                txtSchoolCode.Text = "";
                txtSchoolName.Text = "";
                txtClassNo.Text = "";
                txtClassSection.Text = "";

                txtTestDate.Text = Utilities.GetDate();

            }
            hfStudentIDPKID.Value = "0";

            txtStudentCode.Text = "";
            txtStudentName.Text = "";
            txtStudentRegNo.Text = "";
            txtFatherName.Text = "";
            txtAge.Text = "";
            //ddlGender.SelectedIndex = 0;
            chkDecreasedVision.Checked = false;

            chkWearGlasses.Checked = false;

            rdoReligion.SelectedIndex = -1;

            chkOccularHistory.Checked = false;
            txtOccularHistory.Text = "";

            chkMedicalHistory.Checked = false;
            txtMedicalHistory.Text = "";

            chkChiefComplain.Checked = false;
            txtChiefComplain.Text = "";

            hfLookupResultSchool.Value = "0";
            hfLookupResultClass.Value = "0";
            hfLookupResultStudent.Value = "0";

            BindCombos();

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

        private void BindCombos()
        {
            try
            {
                var dtGender = (from a in dx.tblGenders select a).ToList();
                if (dtGender.Count != 0)
                {
                    ddlGender.DataSource = dtGender;
                    ddlGender.DataValueField = "GenderAutoId";
                    ddlGender.DataTextField = "Gender";
                    ddlGender.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlGender.Items.Insert(0, item);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }

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

                var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
                txtStudentCode.Text = studentCode;

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

        protected void btnLookupClass_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("Class")
                                  select a).ToList().ToDataTable();

                hfLookupResultClass.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Class No.";
                Session["Name"] = "";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResultClass.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupClass, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResultClass_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultClass.Value;
            hfClassIDPKID.Value = selectedPKID; //to allow update mode

            LoadClassDetail(selectedPKID);
            lbl_error.Text = "";
        }

        protected void btnLookupSection_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("Section")
                                  select a).ToList().ToDataTable();

                hfLookupResultClass.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Section Id";
                Session["Name"] = "Class Section";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResultSection.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupSection, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfClassIDPKID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ClassIDPKID = string.Empty;
                ClassIDPKID = hfClassIDPKID.Value;

                if (Convert.ToUInt32(ClassIDPKID) > 0)
                {
                    LoadClassDetail(ClassIDPKID);
                }

                hfLookupResultClass.Value = "0";
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        private void LoadClassDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblClass_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    //txtClassSection.Text = dt.SectionAutoId;
                    txtClassNo.Text = dt.ClassNo;
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfSectionIDPKID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string SectionIDPKID = string.Empty;
                SectionIDPKID = hfSectionIDPKID.Value;

                if (Convert.ToUInt32(SectionIDPKID) > 0)
                {
                    LoadSectionDetail(SectionIDPKID);
                }

                hfLookupResultSection.Value = "0";
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResultSection_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultSection.Value;
            hfSectionIDPKID.Value = selectedPKID; //to allow update mode

            LoadSectionDetail(selectedPKID);
            lbl_error.Text = "";
        }

        private void LoadSectionDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblSection_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    //txtClassSection.Text = dt.SectionAutoId;
                    txtClassSection.Text = dt.ClassSection;
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_FatherName(Convert.ToInt32(hfSchoolIDPKID.Value), 0)
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
                    txtTestDate.Text = Convert.ToDateTime(dt.StudentTestDate.ToString()).ToString("dd-MMM-yyyy");

                    if (txtSchoolCode.Text.Trim() == "")
                    {
                        txtSchoolCode.Text = dt.SchoolCode;
                        txtSchoolName.Text = dt.SchoolName;
                        hfSchoolIDPKID.Value = dt.SchoolAutoId.ToString();
                    }

                    if (txtClassNo.Text.Trim() == "")
                    {
                        txtClassNo.Text = dt.ClassCode;
                        txtClassSection.Text = dt.ClassSection;
                        hfClassIDPKID.Value = dt.ClassAutoId.ToString();
                        hfSectionIDPKID.Value = dt.SectionAutoId.ToString();
                    }
                    txtStudentCode.Text = dt.StudentCode;
                    txtStudentName.Text = dt.StudentName;
                    txtStudentRegNo.Text = dt.StudentRegNo;
                    txtFatherName.Text = dt.FatherName;
                    txtAge.Text = Convert.ToInt32(dt.Age).ToString();

                    ddlGender.SelectedValue = dt.GenderAutoId.ToString();
                    rdoReligion.SelectedValue = dt.Religion.ToString();

                    if (Convert.ToInt32(dt.DecreasedVision) == 0)
                    {
                        chkDecreasedVision.Checked = false;
                    }
                    else
                    {
                        chkDecreasedVision.Checked = true;
                    }

                    if (Convert.ToInt32(dt.WearGlasses) == 0)
                    {
                        chkWearGlasses.Checked = false;
                    }
                    else
                    {
                        chkWearGlasses.Checked = true;
                    }

                    if (Convert.ToInt32(dt.HasOccularHistory) == 0)
                    {
                        chkOccularHistory.Checked = false;
                    }
                    else
                    {
                        chkOccularHistory.Checked = true;
                    }

                    txtOccularHistory.Text = dt.OccularHistoryRemarks;

                    if (Convert.ToInt32(dt.HasMedicalHistory) == 0)
                    {
                        chkMedicalHistory.Checked = false;
                    }
                    else
                    {
                        chkMedicalHistory.Checked = true;
                    }

                    txtMedicalHistory.Text = dt.MedicalHistoryRemarks;

                    if (Convert.ToInt32(dt.HasChiefComplain) == 0)
                    {
                        chkChiefComplain.Checked = false;
                    }
                    else
                    {
                        chkChiefComplain.Checked = true;
                    }

                    txtChiefComplain.Text = dt.ChiefComplainRemarks;

                    var dtImage = dx.sp_tblStudentImage_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    if (dtImage != null)
                    {
                        byte[] imagem = (byte[])(dtImage.StudentPic);
                        string base64String = Convert.ToBase64String(imagem);
                        StudentImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);

                        txtCaptureDate.Text = DateTime.Parse(dtImage.CaptureDate.ToString()).ToString("dd-MMM-yyyy");
                        txtCaptureRemarks.Text = dtImage.CaptureRemarks;

                        chkNotRequired.Checked = false;
                    }
                    else
                    {
                        txtCaptureDate.Text = "";
                        txtCaptureRemarks.Text = "";

                        StudentImage.ImageUrl = null;
                        StudentImage.ImageUrl = ResolveUrl(@"~/Captures/StudentDefaultImage.jpg");

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

        protected void chkOccularHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOccularHistory.Checked == true)
            {
                txtOccularHistory.Enabled = true;
            }
            else
            {
                txtOccularHistory.Enabled = false;
            }
        }

        protected void chkMedicalHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedicalHistory.Checked == true)
            {
                txtMedicalHistory.Enabled = true;
            }
            else
            {
                txtMedicalHistory.Enabled = false;
            }
        }

        protected void chkChiefComplain_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChiefComplain.Checked == true)
            {
                txtChiefComplain.Enabled = true;
            }
            else
            {
                txtChiefComplain.Enabled = false;
            }
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

            var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
            txtStudentCode.Text = studentCode;

            txtStudentRegNo.Focus();
        }

        protected void txtFatherName_TextChanged(object sender, EventArgs e)
        {
            string sFatherName = txtFatherName.Text;

            txtFatherName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sFatherName.ToLower());

            txtAge.Focus();
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

        protected void chkWearGlasses_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWearGlasses.Checked == true)
            {
                chkDecreasedVision.Checked = true;
            }
            else
            {
                chkDecreasedVision.Checked = false;
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
            //if (hfImageBytes.Value.Length > 0)
            //{
            //    byte[] bytUpfile = GetUploadedImage();

            //    tblSchoolImage tbl = new tblSchoolImage();
            //    var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
            //    tbl.SchoolAutoId = int.Parse(hfStudentIDPKID.Value);
            //    tbl.SchoolPic = bytUpfile; //getting complete url  
            //    tbl.FileType = "";
            //    tbl.FileSize = bytUpfile.Length;
            //    tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
            //    tbl.CaptureRemarks = txtCaptureRemarks.Text;
            //    tbl.UserId = strLoginUserID;
            //    tbl.EntDate = DateTime.Now;
            //    tbl.EntOperation = "";
            //    tbl.EntTerminal = strTerminalId;
            //    tbl.EntTerminalIP = strTerminalIP;
            //    dx.tblSchoolImages.Add(tbl);
            //    dx.SaveChanges();
            //}
        }

        protected void btnViewAllPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/ViewPhotos.aspx?winTitle=View All Photos&FormID=StudentEnrollment&AutoKeyID=" + hfStudentIDPKID.Value + "','.','height=400,width=600,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupSchool, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }
    }
}