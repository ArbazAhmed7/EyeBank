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
    public partial class HospitalVisitForSurgery : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "HospitalVisitForSurgery"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                BindCombos();

                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                rdoOldNewTest_SelectedIndexChanged(null, null);
                txtTestDate.Text = Utilities.GetDate();

                pnlTestSummary.Visible = false;

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
                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInput())
                    {
                        string strLoginUserID = Utilities.GetLoginUserID();
                        string strTerminalId = Utilities.getTerminalId();
                        string strTerminalIP = Utilities.getTerminalIP();

                        string strSurgeryRight = string.Empty;
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (chkListSurgeryRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryRight += chkListSurgeryRight.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryRight = strSurgeryRight.TrimEnd(',');

                        string strSurgeryLeft = string.Empty;
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (chkListSurgeryLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryLeft += chkListSurgeryLeft.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryLeft = strSurgeryLeft.TrimEnd(',');

                        var res = dx.sp_tblVisitforSurgeryStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                                Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                                Convert.ToInt32(ddlOrthoptist.SelectedValue), Convert.ToInt32(ddlSurgeon.SelectedValue), Convert.ToInt32(ddlOptometrist.SelectedValue),
                                                strSurgeryRight, txtOtherSurgeryRight.Text, strSurgeryLeft, txtOtherSurgeryLeft.Text, txtCommentsSurgeon.Text, DateTime.Parse(txtFollowupDate.Text),
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

                            pnlTestSummary.Visible = false;
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }
                else
                {
                    if (ValidateInputTeacher())
                    {
                        string strLoginUserID = Utilities.GetLoginUserID();
                        string strTerminalId = Utilities.getTerminalId();
                        string strTerminalIP = Utilities.getTerminalIP();

                        string strSurgeryRight = string.Empty;
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (chkListSurgeryRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryRight += chkListSurgeryRight.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryRight = strSurgeryRight.TrimEnd(',');

                        string strSurgeryLeft = string.Empty;
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (chkListSurgeryLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryLeft += chkListSurgeryLeft.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryLeft = strSurgeryLeft.TrimEnd(',');

                        var res = dx.sp_tblVisitforSurgeryTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                                Convert.ToInt32(hfTeacherIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                                Convert.ToInt32(ddlOrthoptist.SelectedValue), Convert.ToInt32(ddlSurgeon.SelectedValue), Convert.ToInt32(ddlOptometrist.SelectedValue),
                                                strSurgeryRight, txtOtherSurgeryRight.Text, strSurgeryLeft, txtOtherSurgeryLeft.Text, txtCommentsSurgeon.Text, DateTime.Parse(txtFollowupDate.Text),
                                                strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            if (hfImageBytes.Value.Length > 0)
                            {
                                byte[] bytUpfile = GetUploadedImage();

                                tblTeacherImage tbl = new tblTeacherImage();
                                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                                tbl.TeacherAutoId = int.Parse(res.TeacherAutoId.ToString());
                                tbl.TeacherPic = bytUpfile; //getting complete url  
                                tbl.FileType = "";
                                tbl.FileSize = bytUpfile.Length;
                                tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                                tbl.CaptureRemarks = txtCaptureRemarks.Text;
                                tbl.UserId = strLoginUserID;
                                tbl.EntDate = DateTime.Now;
                                tbl.EntOperation = "";
                                tbl.EntTerminal = strTerminalId;
                                tbl.EntTerminalIP = strTerminalIP;
                                dx.tblTeacherImages.Add(tbl);
                                dx.SaveChanges();
                            }

                            lbl_error.Text = res.RetMessage;

                            ClearForm(false);
                            ShowConfirmAddMoreRecord();

                            pnlTestSummary.Visible = false;
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
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
                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInput())
                    {
                        string strLoginUserID = Utilities.GetLoginUserID();
                        string strTerminalId = Utilities.getTerminalId();
                        string strTerminalIP = Utilities.getTerminalIP();

                        string strSurgeryRight = string.Empty;
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (chkListSurgeryRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryRight += chkListSurgeryRight.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryRight = strSurgeryRight.TrimEnd(',');

                        string strSurgeryLeft = string.Empty;
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (chkListSurgeryLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryLeft += chkListSurgeryLeft.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryLeft = strSurgeryLeft.TrimEnd(',');

                        var res = dx.sp_tblVisitforSurgeryStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                                Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                                Convert.ToInt32(ddlOrthoptist.SelectedValue), Convert.ToInt32(ddlSurgeon.SelectedValue), Convert.ToInt32(ddlOptometrist.SelectedValue),
                                                strSurgeryRight, txtOtherSurgeryRight.Text, strSurgeryLeft, txtOtherSurgeryLeft.Text, txtCommentsSurgeon.Text, DateTime.Parse(txtFollowupDate.Text),
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

                            pnlTestSummary.Visible = false;

                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }
                else
                {
                    if (ValidateInputTeacher())
                    {
                        string strLoginUserID = Utilities.GetLoginUserID();
                        string strTerminalId = Utilities.getTerminalId();
                        string strTerminalIP = Utilities.getTerminalIP();

                        string strSurgeryRight = string.Empty;
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (chkListSurgeryRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryRight += chkListSurgeryRight.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryRight = strSurgeryRight.TrimEnd(',');

                        string strSurgeryLeft = string.Empty;
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (chkListSurgeryLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                strSurgeryLeft += chkListSurgeryLeft.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        strSurgeryLeft = strSurgeryLeft.TrimEnd(',');

                        var res = dx.sp_tblVisitforSurgeryTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                                Convert.ToInt32(hfTeacherIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlOphthalmologist.SelectedValue),
                                                Convert.ToInt32(ddlOrthoptist.SelectedValue), Convert.ToInt32(ddlSurgeon.SelectedValue), Convert.ToInt32(ddlOptometrist.SelectedValue),
                                                strSurgeryRight, txtOtherSurgeryRight.Text, strSurgeryLeft, txtOtherSurgeryLeft.Text, txtCommentsSurgeon.Text, DateTime.Parse(txtFollowupDate.Text),
                                                strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            if (hfImageBytes.Value.Length > 0)
                            {
                                byte[] bytUpfile = GetUploadedImage();

                                tblTeacherImage tbl = new tblTeacherImage();
                                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                                tbl.TeacherAutoId = int.Parse(res.TeacherAutoId.ToString());
                                tbl.TeacherPic = bytUpfile; //getting complete url  
                                tbl.FileType = "";
                                tbl.FileSize = bytUpfile.Length;
                                tbl.CaptureDate = DateTime.Parse(txtCaptureDate.Text);
                                tbl.CaptureRemarks = txtCaptureRemarks.Text;
                                tbl.UserId = strLoginUserID;
                                tbl.EntDate = DateTime.Now;
                                tbl.EntOperation = "";
                                tbl.EntTerminal = strTerminalId;
                                tbl.EntTerminalIP = strTerminalIP;
                                dx.tblTeacherImages.Add(tbl);
                                dx.SaveChanges();
                            }

                            lbl_error.Text = res.RetMessage;

                            ClearForm(false);
                            ShowConfirmAddMoreRecord();

                            pnlTestSummary.Visible = false;
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
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
                if (rdoType.SelectedValue == "0")
                {
                    var res = dx.sp_tblVisitforSurgeryStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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
                else
                {
                    var res = dx.sp_tblVisitforSurgeryTeacher_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            if (ddlOphthalmologist.SelectedValue == "0"
                    && ddlOrthoptist.SelectedValue == "0"
                    && ddlOptometrist.SelectedValue == "0"
                    && ddlSurgeon.SelectedValue == "0")
            {
                lbl_error.Text = "Doctor Information is required.";
                ddlOphthalmologist.Focus();
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

        private bool ValidateInputTeacher()
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

            if (txtTeacherName.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Name is required.";
                txtTeacherName.Focus();
                return false;
            }

            if (ddlOphthalmologist.SelectedValue == "0"
                    && ddlOrthoptist.SelectedValue == "0"
                    && ddlOptometrist.SelectedValue == "0"
                    && ddlSurgeon.SelectedValue == "0")
            {
                lbl_error.Text = "Doctor Information is required.";
                ddlOphthalmologist.Focus();
                return false;
            }


            if (chkNotRequired.Checked == false)
            {
                if (StudentImage.ImageUrl == "~/Captures/StudentDefaultImage.jpg")
                {
                    lbl_error.Text = "Teacher Picture is required.";
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
            hfTeacherIDPKID.Value = "0";

            txtStudentCode.Text = "";
            txtStudentName.Text = "";

            lblFatherName_Student.Text = "";
            lblAge_Student.Text = "";
            lblDecreasedVision_Student.Text = "";
            lblGender_Student.Text = "";
            lblClass_Student.Text = "";
            lblSchoolName_Student.Text = "";

            txtTeacherCode.Text = "";
            txtTeacherName.Text = "";

            lblFatherName_Teacher.Text = "";
            lblAge_Teacher.Text = "";
            lblDecreasedVision_Teacher.Text = "";
            lblWearingGlasses_Teacher.Text = "";
            lblGender_Teacher.Text = "";
            //lblClass_Teacher.Text = "";
            lblSchoolName_Teacher.Text = "";

            rdoOldNewTest.SelectedValue = "0";
            rdoOldNewTest_SelectedIndexChanged(null, null);

            try
            {
                ddlSurgeon.SelectedValue = "0";
                ddlOptometrist.SelectedValue = "0";
                ddlOrthoptist.SelectedValue = "0";
                ddlOphthalmologist.SelectedValue = "0";
            }
            catch (Exception ex)
            {
                //do nothing
            }
            //chkListSurgeryRight.Items.Clear();
            //chkListSurgeryRight.DataSource = null;
            //chkListSurgeryRight.DataBind();

            //chkListSurgeryLeft.Items.Clear();
            //chkListSurgeryLeft.DataSource = null;
            //chkListSurgeryLeft.DataBind();

            chkListSurgeryRight.ClearSelection();
            chkListSurgeryLeft.ClearSelection();

            txtOtherSurgeryRight.Text = "";
            txtOtherSurgeryLeft.Text = "";
            txtCommentsSurgeon.Text = "";
            txtFollowupDate.Text = "";

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
                DataTable data = (from a in dx.sp_GetLookupData_Student_School_Surgery(iType)
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

                pnlTestSummary.Visible = false;

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

                pnlTestSummary.Visible = false;
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_Surgery(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
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


                    //string q = "SELECT * FROM tblTreatmentStudent WHERE StudentAutoId = " + Convert.ToInt32(ID) + " ORDER BY TreatmentStudentTransDate DESC ";
                    //var dtTreatment = dx.Database.SqlQuery<tblTreatmentStudent>(q).ToList();

                    var dtId = dx.sp_tblTreatmentStudent_GetPreviousTest(Convert.ToInt32(ID)).SingleOrDefault();
                    int iTreatmentId = dtId.TreatmentStudentId;

                    var dtTreatment = dx.sp_tblTreatmentStudent_GetDetail(iTreatmentId).SingleOrDefault();
                    if (dtTreatment != null)
                    {
                        string strSurgery_RightEye = dtTreatment.SurgeryDetail.ToString();
                        string[] items_Right = strSurgery_RightEye.Split(',');
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (items_Right.Contains(chkListSurgeryRight.Items[i].Value))
                            {
                                chkListSurgeryRight.Items[i].Selected = true;
                            }
                        }

                        string strSurgery_LeftEye = dtTreatment.SurgeryDetail.ToString();
                        string[] items_Left = strSurgery_LeftEye.Split(',');
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (items_Left.Contains(chkListSurgeryLeft.Items[i].Value))
                            {
                                chkListSurgeryLeft.Items[i].Selected = true;
                            }
                        }
                    }

                    var dtPreSurgeryDetail = dx.sp_tblVisitforPreSurgeryStudent_GetDetail_StudentWise(Convert.ToInt32(ID)).SingleOrDefault();
                    if (dtPreSurgeryDetail != null)
                    {
                        ddlHospital.SelectedValue = dtPreSurgeryDetail.HospitalAutoId.ToString();
                        ddlOphthalmologist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Ophthalmologist.ToString();
                        ddlOrthoptist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Orthoptist.ToString();
                        ddlSurgeon.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Surgeon.ToString();
                        ddlOptometrist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Optometrist.ToString();
                    }

                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        var dtGlassDespensePreviousData = dx.sp_tblVisitforSurgeryStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtGlassDespensePreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtGlassDespensePreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitforSurgeryStudentId";
                                ddlPreviousTestResult.DataTextField = "VisitforSurgeryStudentTransDate";
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

                    string lblAutoRefPrevVisitDate = string.Empty;
                    var dtAutoRefLastData = dx.sp_tblOptometristMasterStudent_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtAutoRefLastData != null)
                        {
                            lblAutoRefPrevVisitDate = DateTime.Parse(dtAutoRefLastData.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                            lblSubjectiveRefPrevVisitDate.Text = DateTime.Parse(dtAutoRefLastData.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtAutoRefTest_Student = dx.sp_AutoRefTest_StudentVisit(int.Parse(hfStudentIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate)).SingleOrDefault();
                    if (dtAutoRefTest_Student != null)
                    {
                        pnlTestSummary.Visible = true;

                        lblAutoRef_RightEye.Text = dtAutoRefTest_Student.Spherical__Right_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Right_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Right_Eye_.ToString();
                        lblAutoRef_LeftEye.Text = dtAutoRefTest_Student.Spherical__Left_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Left_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Left_Eye_.ToString();
                    }

                    int iOptometristStudentId = 0;

                    var dtOptometrist_Student = dx.sp_Optometrist_StudentVisit(int.Parse(hfStudentIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate)).SingleOrDefault();

                    if (dtOptometrist_Student != null)
                    {
                        pnlTestSummary.Visible = true;
                        iOptometristStudentId = dtOptometrist_Student.OptometristStudentId;
                        lblVisualAcuity_Unaided_RightEye.Text = dtOptometrist_Student.DistanceVision_RightEye_Unaided.ToString();
                        lblVisualAcuity_WithGlasses_RightEye.Text = dtOptometrist_Student.DistanceVision_RightEye_WithGlasses.ToString();
                        lblVisualAcuity_PinHole_RightEye.Text = dtOptometrist_Student.DistanceVision_RightEye_PinHole.ToString();
                        lblVisualAcuity_Near_RightEye.Text = dtOptometrist_Student.NearVision_RightEye.ToString();

                        lblVisualAcuity_Unaided_LeftEye.Text = dtOptometrist_Student.DistanceVision_LeftEye_Unaided.ToString();
                        lblVisualAcuity_WithGlasses_LeftEye.Text = dtOptometrist_Student.DistanceVision_LeftEye_WithGlasses.ToString();
                        lblVisualAcuity_PinHole_LeftEye.Text = dtOptometrist_Student.DistanceVision_LeftEye_PinHole.ToString();
                        lblVisualAcuity_Near_LeftEye.Text = dtOptometrist_Student.NearVision_LeftEye.ToString();

                        lblNeedsCycloRefraction_Status.Text = dtOptometrist_Student.NeedCycloRefractionRemarks_RightEye.ToString();

                        lblDistance_RightEye.Text = dtOptometrist_Student.Spherical__Right_Eye_.ToString() + " / " + dtOptometrist_Student.Cyclinderical__Right_Eye_.ToString() + " x " + dtOptometrist_Student.Axix__Right_Eye_.ToString();
                        Label24.Text = dtOptometrist_Student.Near_Add__Right_Eye_.ToString();

                        lblDistance_LeftEye.Text = dtOptometrist_Student.Spherical__Left_Eye_.ToString() + " / " + dtOptometrist_Student.Cyclinderical__Left_Eye_.ToString() + " x " + dtOptometrist_Student.Axix__Left_Eye_.ToString();
                        Label25.Text = dtOptometrist_Student.Near_Add__Left_Eye_.ToString();

                        lblDouchromeTest_Result.Text = dtOptometrist_Student.Douchrome_Test.ToString();

                        lblRetinoscopy_RightEye.Text = dtOptometrist_Student.Retinoscopy__Right_Eye_.ToString();
                        lblRetinoscopy_LeftEye.Text = dtOptometrist_Student.Retinoscopy__Left_Eye_.ToString();

                        lblCondition_Remarks_RightEye.Text = dtOptometrist_Student.Condition__Right_Eye_.ToString();
                        lblCondition_Remarks_LeftEye.Text = dtOptometrist_Student.Condition__Left_Eye_.ToString();

                        lblFinalPresentation_Results_RightEye.Text = dtOptometrist_Student.Final_Prescription__Right_Eye_.ToString();
                        lblFinalPresentation_Results_LeftEye.Text = dtOptometrist_Student.Final_Prescription__Left_Eye_.ToString();

                        lblOrthopticAssessment_RightEye.Text = dtOptometrist_Student.Condition__Right_Eye_.ToString();
                        lblOrthopticAssessment_LeftEye.Text = dtOptometrist_Student.Condition__Left_Eye_.ToString();

                        lblHirschberg_RightEye.Text = dtOptometrist_Student.Hirchberg_Distance.ToString();
                        lblHirschberg_LeftEye.Text = dtOptometrist_Student.Hirchberg_Near.ToString();

                        lblRedGlow_RightEye.Text = dtOptometrist_Student.Ophthalmoscope__Right_Eye_.ToString();
                        lblRedGlow_LeftEye.Text = dtOptometrist_Student.Ophthalmoscope__Left_Eye_.ToString();

                        lblPupilReflex_RightEye.Text = dtOptometrist_Student.Pupillary_Reactions__Right_Eye_.ToString();
                        lblPupilReflex_LeftEye.Text = dtOptometrist_Student.Pupillary_Reactions__Left_Eye_.ToString();

                        lblCoverUnCoverTest_RightEye.Text = dtOptometrist_Student.Cover_Uncovert_Test__Right_Eye_.ToString();
                        lblCoverUnCoverTest_LeftEye.Text = dtOptometrist_Student.Cover_Uncovert_Test__Left_Eye_.ToString();

                        lblOtherRemarks.Text = "";

                        lblExtraOccularMuscle_RightEye.Text = dtOptometrist_Student.Extra_Occular_Muscle_Test__Right_Eye_.ToString();
                        lblExtraOccularMuscle_LeftEye.Text = dtOptometrist_Student.Extra_Occular_Muscle_Test__Left_Eye_.ToString();
                    }

                    var dtTreatmentData = dx.sp_VisitSummary_Student(Convert.ToInt32(hfStudentIDPKID.Value), iOptometristStudentId).SingleOrDefault();
                    try
                    {
                        if (dtTreatmentData != null)
                        {
                            lblDiagnosis.Text = dtTreatmentData.Daignosis.ToString();
                            lblDiagnosisRemarks.Text = dtTreatmentData.DaignosisRemarks.ToString();
                            lblSubDiagnosis.Text = dtTreatmentData.SubDaignosis.ToString();

                            lblNextVisit.Text = dtTreatmentData.NextVisit.ToString();

                            lblSurgery.Text = dtTreatmentData.Surgery.ToString();
                            lblSurgeryDetail.Text = dtTreatmentData.SurgeryDetail.ToString();
                            lblSurgeryDetailRemarks.Text = dtTreatmentData.SurgeryDetailRemarks.ToString();

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
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }


        protected void btnLookupTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Teacher_Surgery(Convert.ToInt32(hfSchoolIDPKID.Value), iType)
                                  select a).ToList().ToDataTable();

                hfLookupResultTeacher.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Teacher Code";
                Session["Name"] = "Teacher Name";
                Session["FatherName"] = "Father / Husband Name";
                Session["Description"] = "School Name";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlFatherName.aspx?winTitle=Select User&hfName=" + hfLookupResultTeacher.ID + "','.','height=600,width=650,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(btnLookupTeacher, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfTeacherIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string TeacherIDPKID = string.Empty;
            TeacherIDPKID = hfTeacherIDPKID.Value;

            LoadTeacherDetail(TeacherIDPKID);
            hfLookupResultTeacher.Value = "0";

            rdoOldNewTest.SelectedValue = "0";
            rdoOldNewTest_SelectedIndexChanged(null, null);

            lbl_error.Text = "";
        }

        protected void hfLookupResultTeacher_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultTeacher.Value;
            hfTeacherIDPKID.Value = selectedPKID; //to allow update mode

            LoadTeacherDetail(selectedPKID);
            lbl_error.Text = "";
        }

        private void LoadTeacherDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblTeacher_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

                    if (txtSchoolCode.Text.Trim() == "")
                    {
                        txtSchoolCode.Text = dt.SchoolCode;
                        txtSchoolName.Text = dt.SchoolName;
                        hfSchoolIDPKID.Value = dt.SchoolAutoId.ToString();
                    }

                    txtTeacherCode.Text = dt.TeacherCode;
                    txtTeacherName.Text = dt.TeacherName;
                    lblFatherName_Teacher.Text = dt.FatherName;
                    lblAge_Teacher.Text = dt.Age.ToString();
                    lblGender_Teacher.Text = dt.Gender;
                    lblSchoolName_Teacher.Text = dt.SchoolName;
                    lblWearingGlasses_Teacher.Text = dt.WearGlasses == 0 ? "No" : "Yes";
                    lblDecreasedVision_Teacher.Text = dt.DecreasedVision == 0 ? "No" : "Yes";


                    //string q = "SELECT * FROM tblTreatmentTeacher WHERE TeacherAutoId = " + Convert.ToInt32(ID) + " ORDER BY TreatmentTeacherTransDate DESC ";
                    //var dtTreatment = dx.Database.SqlQuery<tblTreatmentTeacher>(q).ToList();

                    var dtId = dx.sp_tblTreatmentTeacher_GetPreviousTest(Convert.ToInt32(ID)).SingleOrDefault();
                    int iTreatmentId = dtId.TreatmentTeacherId;

                    var dtTreatment = dx.sp_tblTreatmentTeacher_GetDetail(iTreatmentId).SingleOrDefault();
                    if (dtTreatment != null)
                    {
                        string strSurgery_RightEye = dtTreatment.SurgeryDetail.ToString();
                        string[] items_Right = strSurgery_RightEye.Split(',');
                        for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                        {
                            if (items_Right.Contains(chkListSurgeryRight.Items[i].Value))
                            {
                                chkListSurgeryRight.Items[i].Selected = true;
                            }
                        }

                        string strSurgery_LeftEye = dtTreatment.SurgeryDetail.ToString();
                        string[] items_Left = strSurgery_LeftEye.Split(',');
                        for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                        {
                            if (items_Left.Contains(chkListSurgeryLeft.Items[i].Value))
                            {
                                chkListSurgeryLeft.Items[i].Selected = true;
                            }
                        }
                    }

                    //var dtPreSurgeryDetail = dx.sp_tblVisitforPreSurgeryTeacher_GetDetail_TeacherWise(Convert.ToInt32(ID)).SingleOrDefault();
                    //if (dtPreSurgeryDetail != null)
                    //{
                    //    ddlHospital.SelectedValue = dtPreSurgeryDetail.HospitalAutoId.ToString();
                    //    ddlOphthalmologist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Ophthalmologist.ToString();
                    //    ddlOrthoptist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Orthoptist.ToString();
                    //    ddlSurgeon.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Surgeon.ToString();
                    //    ddlOptometrist.SelectedValue = dtPreSurgeryDetail.DoctorAutoId_Optometrist.ToString();
                    //}

                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        var dtGlassDespensePreviousData = dx.sp_tblVisitforSurgeryTeacher_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtGlassDespensePreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtGlassDespensePreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitforSurgeryTeacherId";
                                ddlPreviousTestResult.DataTextField = "VisitforSurgeryTeacherTransDate";
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

                    string lblAutoRefPrevVisitDate = string.Empty;
                    var dtAutoRefLastData = dx.sp_tblOptometristMasterTeacher_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtAutoRefLastData != null)
                        {
                            lblAutoRefPrevVisitDate = DateTime.Parse(dtAutoRefLastData.AutoRefTeacherTransDate.ToString()).ToString("dd-MMM-yyyy");
                            lblSubjectiveRefPrevVisitDate.Text = DateTime.Parse(dtAutoRefLastData.AutoRefTeacherTransDate.ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtAutoRefTest_Teacher = dx.sp_AutoRefTest_TeacherVisit(int.Parse(hfTeacherIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate)).SingleOrDefault();
                    if (dtAutoRefTest_Teacher != null)
                    {
                        pnlTestSummary.Visible = true;

                        lblAutoRef_RightEye.Text = dtAutoRefTest_Teacher.Spherical__Right_Eye_.ToString() + " / " + dtAutoRefTest_Teacher.Cyclinderical__Right_Eye_.ToString() + " x " + dtAutoRefTest_Teacher.Axix__Right_Eye_.ToString();
                        lblAutoRef_LeftEye.Text = dtAutoRefTest_Teacher.Spherical__Left_Eye_.ToString() + " / " + dtAutoRefTest_Teacher.Cyclinderical__Left_Eye_.ToString() + " x " + dtAutoRefTest_Teacher.Axix__Left_Eye_.ToString();
                    }

                    int iOptometristTeacherId = 0;

                    var dtOptometrist_Teacher = dx.sp_Optometrist_TeacherVisit(int.Parse(hfTeacherIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate)).SingleOrDefault();

                    if (dtOptometrist_Teacher != null)
                    {
                        pnlTestSummary.Visible = true;
                        iOptometristTeacherId = dtOptometrist_Teacher.OptometristTeacherId;
                        lblVisualAcuity_Unaided_RightEye.Text = dtOptometrist_Teacher.DistanceVision_RightEye_Unaided.ToString();
                        lblVisualAcuity_WithGlasses_RightEye.Text = dtOptometrist_Teacher.DistanceVision_RightEye_WithGlasses.ToString();
                        lblVisualAcuity_PinHole_RightEye.Text = dtOptometrist_Teacher.DistanceVision_RightEye_PinHole.ToString();
                        lblVisualAcuity_Near_RightEye.Text = dtOptometrist_Teacher.NearVision_RightEye.ToString();

                        lblVisualAcuity_Unaided_LeftEye.Text = dtOptometrist_Teacher.DistanceVision_LeftEye_Unaided.ToString();
                        lblVisualAcuity_WithGlasses_LeftEye.Text = dtOptometrist_Teacher.DistanceVision_LeftEye_WithGlasses.ToString();
                        lblVisualAcuity_PinHole_LeftEye.Text = dtOptometrist_Teacher.DistanceVision_LeftEye_PinHole.ToString();
                        lblVisualAcuity_Near_LeftEye.Text = dtOptometrist_Teacher.NearVision_LeftEye.ToString();

                        lblNeedsCycloRefraction_Status.Text = dtOptometrist_Teacher.NeedCycloRefractionRemarks_RightEye.ToString();

                        lblDistance_RightEye.Text = dtOptometrist_Teacher.Spherical__Right_Eye_.ToString() + " / " + dtOptometrist_Teacher.Cyclinderical__Right_Eye_.ToString() + " x " + dtOptometrist_Teacher.Axix__Right_Eye_.ToString();
                        Label24.Text = dtOptometrist_Teacher.Near_Add__Right_Eye_.ToString();

                        lblDistance_LeftEye.Text = dtOptometrist_Teacher.Spherical__Left_Eye_.ToString() + " / " + dtOptometrist_Teacher.Cyclinderical__Left_Eye_.ToString() + " x " + dtOptometrist_Teacher.Axix__Left_Eye_.ToString();
                        Label25.Text = dtOptometrist_Teacher.Near_Add__Left_Eye_.ToString();

                        lblDouchromeTest_Result.Text = dtOptometrist_Teacher.Douchrome_Test.ToString();

                        lblRetinoscopy_RightEye.Text = dtOptometrist_Teacher.Retinoscopy__Right_Eye_.ToString();
                        lblRetinoscopy_LeftEye.Text = dtOptometrist_Teacher.Retinoscopy__Left_Eye_.ToString();

                        lblCondition_Remarks_RightEye.Text = dtOptometrist_Teacher.Condition__Right_Eye_.ToString();
                        lblCondition_Remarks_LeftEye.Text = dtOptometrist_Teacher.Condition__Left_Eye_.ToString();

                        lblFinalPresentation_Results_RightEye.Text = dtOptometrist_Teacher.Final_Prescription__Right_Eye_.ToString();
                        lblFinalPresentation_Results_LeftEye.Text = dtOptometrist_Teacher.Final_Prescription__Left_Eye_.ToString();

                        lblOrthopticAssessment_RightEye.Text = dtOptometrist_Teacher.Condition__Right_Eye_.ToString();
                        lblOrthopticAssessment_LeftEye.Text = dtOptometrist_Teacher.Condition__Left_Eye_.ToString();

                        lblHirschberg_RightEye.Text = dtOptometrist_Teacher.Hirchberg_Distance.ToString();
                        lblHirschberg_LeftEye.Text = dtOptometrist_Teacher.Hirchberg_Near.ToString();

                        lblRedGlow_RightEye.Text = dtOptometrist_Teacher.Ophthalmoscope__Right_Eye_.ToString();
                        lblRedGlow_LeftEye.Text = dtOptometrist_Teacher.Ophthalmoscope__Left_Eye_.ToString();

                        lblPupilReflex_RightEye.Text = dtOptometrist_Teacher.Pupillary_Reactions__Right_Eye_.ToString();
                        lblPupilReflex_LeftEye.Text = dtOptometrist_Teacher.Pupillary_Reactions__Left_Eye_.ToString();

                        lblCoverUnCoverTest_RightEye.Text = dtOptometrist_Teacher.Cover_Uncovert_Test__Right_Eye_.ToString();
                        lblCoverUnCoverTest_LeftEye.Text = dtOptometrist_Teacher.Cover_Uncovert_Test__Left_Eye_.ToString();

                        lblOtherRemarks.Text = "";

                        lblExtraOccularMuscle_RightEye.Text = dtOptometrist_Teacher.Extra_Occular_Muscle_Test__Right_Eye_.ToString();
                        lblExtraOccularMuscle_LeftEye.Text = dtOptometrist_Teacher.Extra_Occular_Muscle_Test__Left_Eye_.ToString();
                    }

                    var dtTreatmentData = dx.sp_VisitSummary_Teacher(Convert.ToInt32(hfTeacherIDPKID.Value), iOptometristTeacherId).SingleOrDefault();
                    try
                    {
                        if (dtTreatmentData != null)
                        {
                            lblDiagnosis.Text = dtTreatmentData.Daignosis.ToString();
                            lblDiagnosisRemarks.Text = dtTreatmentData.DaignosisRemarks.ToString();
                            lblSubDiagnosis.Text = dtTreatmentData.SubDaignosis.ToString();

                            lblNextVisit.Text = dtTreatmentData.NextVisit.ToString();

                            lblSurgery.Text = dtTreatmentData.Surgery.ToString();
                            lblSurgeryDetail.Text = dtTreatmentData.SurgeryDetail.ToString();
                            lblSurgeryDetailRemarks.Text = dtTreatmentData.SurgeryDetailRemarks.ToString();

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
            Response.Redirect("~/HospitalVisitForSurgery.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HospitalVisitForSurgery.aspx");
        }

        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string sStudentName = txtStudentName.Text;

            txtStudentName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sStudentName.ToLower());

            chkListSurgeryRight.Focus();
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
                pnlTestSummary.Visible = false;

                if (rdoType.SelectedValue == "0")
                {
                    var dt = dx.sp_tblVisitforSurgeryStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransID.Value = dt.VisitforSurgeryStudentId.ToString();
                    //hfAutoRefTestTransDate.Value = dt.VisitforSurgeryStudentTransDate.ToString();

                    LoadStudentDetail(dt.StudentAutoId.ToString());

                    ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                    ddlOphthalmologist.SelectedValue = dt.DoctorAutoId_Ophthalmologist.ToString();
                    ddlOrthoptist.SelectedValue = dt.DoctorAutoId_Orthoptist.ToString();
                    ddlSurgeon.SelectedValue = dt.DoctorAutoId_Surgeon.ToString();
                    ddlOptometrist.SelectedValue = dt.DoctorAutoId_Optometrist.ToString();

                    string strSurgery_RightEye = dt.Surgery_RightEye.ToString();
                    string[] items_Right = strSurgery_RightEye.Split(',');
                    for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                    {
                        if (items_Right.Contains(chkListSurgeryRight.Items[i].Value))
                        {
                            chkListSurgeryRight.Items[i].Selected = true;
                        }
                    }

                    txtOtherSurgeryRight.Text = dt.SurgeryRemarks_RightEye.ToString();

                    string strSurgery_LeftEye = dt.Surgery_LeftEye.ToString();
                    string[] items_Left = strSurgery_LeftEye.Split(',');
                    for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                    {
                        if (items_Left.Contains(chkListSurgeryLeft.Items[i].Value))
                        {
                            chkListSurgeryLeft.Items[i].Selected = true;
                        }
                    }

                    txtOtherSurgeryLeft.Text = dt.SurgeryRemarks_LeftEye.ToString();

                    txtCommentsSurgeon.Text = dt.Remarks_Surgeon.ToString();

                    txtFollowupDate.Text = DateTime.Parse(dt.FollowupDate.ToString()).ToString("dd-MMM-yyyy");
                }
                else
                {
                    var dt = dx.sp_tblVisitforSurgeryTeacher_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransID.Value = dt.VisitforSurgeryTeacherId.ToString();
                    //hfAutoRefTestTransDate.Value = dt.VisitforSurgeryTeacherTransDate.ToString();

                    LoadTeacherDetail(dt.TeacherAutoId.ToString());

                    ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                    ddlOphthalmologist.SelectedValue = dt.DoctorAutoId_Ophthalmologist.ToString();
                    ddlOrthoptist.SelectedValue = dt.DoctorAutoId_Orthoptist.ToString();
                    ddlSurgeon.SelectedValue = dt.DoctorAutoId_Surgeon.ToString();
                    ddlOptometrist.SelectedValue = dt.DoctorAutoId_Optometrist.ToString();

                    string strSurgery_RightEye = dt.Surgery_RightEye.ToString();
                    string[] items_Right = strSurgery_RightEye.Split(',');
                    for (int i = 0; i < chkListSurgeryRight.Items.Count; i++)
                    {
                        if (items_Right.Contains(chkListSurgeryRight.Items[i].Value))
                        {
                            chkListSurgeryRight.Items[i].Selected = true;
                        }
                    }

                    txtOtherSurgeryRight.Text = dt.SurgeryRemarks_RightEye.ToString();

                    string strSurgery_LeftEye = dt.Surgery_LeftEye.ToString();
                    string[] items_Left = strSurgery_LeftEye.Split(',');
                    for (int i = 0; i < chkListSurgeryLeft.Items.Count; i++)
                    {
                        if (items_Left.Contains(chkListSurgeryLeft.Items[i].Value))
                        {
                            chkListSurgeryLeft.Items[i].Selected = true;
                        }
                    }

                    txtOtherSurgeryLeft.Text = dt.SurgeryRemarks_LeftEye.ToString();

                    txtCommentsSurgeon.Text = dt.Remarks_Surgeon.ToString();

                    txtFollowupDate.Text = DateTime.Parse(dt.FollowupDate.ToString()).ToString("dd-MMM-yyyy");
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
                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";

                var dtHospital = (from a in dx.tblHospitals select a).ToList();
                if (dtHospital.Count != 0)
                {
                    ddlHospital.Items.Clear();

                    ddlHospital.DataSource = dtHospital;
                    ddlHospital.DataValueField = "HospitalAutoId";
                    ddlHospital.DataTextField = "HospitalDescription";
                    ddlHospital.DataBind();

                    ddlHospital.Items.Insert(0, item);
                }
                else
                {
                    ddlHospital.Items.Clear();
                    ddlHospital.DataSource = null;
                    ddlHospital.DataBind();

                    ddlHospital.Items.Insert(0, item);
                }

                string q = "SELECT * FROM tblDoctor WHERE Ophthalmologist = 1";
                var dtOphthalmologist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtOphthalmologist.Count != 0)
                {
                    ddlOphthalmologist.Items.Clear();

                    ddlOphthalmologist.DataSource = dtOphthalmologist;
                    ddlOphthalmologist.DataValueField = "DoctorAutoId";
                    ddlOphthalmologist.DataTextField = "DoctorDescription";
                    ddlOphthalmologist.DataBind();

                    ddlOphthalmologist.Items.Insert(0, item);
                }
                else
                {
                    ddlOphthalmologist.Items.Clear();
                    ddlOphthalmologist.DataSource = null;
                    ddlOphthalmologist.DataBind();

                    ddlOphthalmologist.Items.Insert(0, item);
                }

                q = "SELECT * FROM tblDoctor WHERE Orthoptist = 1";
                var dtOrthoptist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtOrthoptist.Count != 0)
                {
                    ddlOrthoptist.Items.Clear();

                    ddlOrthoptist.DataSource = dtOrthoptist;
                    ddlOrthoptist.DataValueField = "DoctorAutoId";
                    ddlOrthoptist.DataTextField = "DoctorDescription";
                    ddlOrthoptist.DataBind();

                    ddlOrthoptist.Items.Insert(0, item);

                }
                else
                {
                    ddlOrthoptist.Items.Clear();
                    ddlOrthoptist.DataSource = null;
                    ddlOrthoptist.DataBind();

                    ddlOrthoptist.Items.Insert(0, item);
                }

                q = "SELECT * FROM tblDoctor WHERE Surgeon = 1";
                var dtSurgeon = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtSurgeon.Count != 0)
                {
                    ddlSurgeon.Items.Clear();

                    ddlSurgeon.DataSource = dtSurgeon;
                    ddlSurgeon.DataValueField = "DoctorAutoId";
                    ddlSurgeon.DataTextField = "DoctorDescription";
                    ddlSurgeon.DataBind();

                    ddlSurgeon.Items.Insert(0, item);
                }
                else
                {
                    ddlSurgeon.Items.Clear();
                    ddlSurgeon.DataSource = null;
                    ddlSurgeon.DataBind();

                    ddlSurgeon.Items.Insert(0, item);
                }

                q = "SELECT * FROM tblDoctor WHERE Optometrist = 1";
                var dtOptometrist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
                if (dtOptometrist.Count != 0)
                {
                    ddlOptometrist.Items.Clear();

                    ddlOptometrist.DataSource = dtOptometrist;
                    ddlOptometrist.DataValueField = "DoctorAutoId";
                    ddlOptometrist.DataTextField = "DoctorDescription";
                    ddlOptometrist.DataBind();

                    ddlOptometrist.Items.Insert(0, item);
                }
                else
                {
                    ddlOptometrist.Items.Clear();
                    ddlOptometrist.DataSource = null;
                    ddlOptometrist.DataBind();

                    ddlOptometrist.Items.Insert(0, item);
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

            ListItem item = new ListItem();
            item.Text = "Select";
            item.Value = "0";

            string q = "SELECT * FROM tblDoctor WHERE HospitalAutoID = " + iHospital + " AND Ophthalmologist = 1";
            var dtOphthalmologist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
            if (dtOphthalmologist.Count != 0)
            {
                ddlOphthalmologist.DataSource = dtOphthalmologist;
                ddlOphthalmologist.DataValueField = "DoctorAutoId";
                ddlOphthalmologist.DataTextField = "DoctorDescription";
                ddlOphthalmologist.DataBind();

                ddlOphthalmologist.Items.Insert(0, item);
            }
            else
            {
                ddlOphthalmologist.Items.Clear();
                ddlOphthalmologist.DataSource = null;
                ddlOphthalmologist.DataBind();

                ddlOphthalmologist.Items.Insert(0, item);
            }

            q = "SELECT * FROM tblDoctor WHERE HospitalAutoID = " + iHospital + " AND Orthoptist = 1";
            var dtOrthoptist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
            if (dtOrthoptist.Count != 0)
            {

                ddlOrthoptist.DataSource = dtOrthoptist;
                ddlOrthoptist.DataValueField = "DoctorAutoId";
                ddlOrthoptist.DataTextField = "DoctorDescription";
                ddlOrthoptist.DataBind();

                ddlOrthoptist.Items.Insert(0, item);

            }
            else
            {
                ddlOrthoptist.Items.Clear();
                ddlOrthoptist.DataSource = null;
                ddlOrthoptist.DataBind();

                ddlOrthoptist.Items.Insert(0, item);
            }

            q = "SELECT * FROM tblDoctor WHERE HospitalAutoID = " + iHospital + " AND Surgeon = 1";
            var dtSurgeon = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
            if (dtSurgeon.Count != 0)
            {

                ddlSurgeon.DataSource = dtSurgeon;
                ddlSurgeon.DataValueField = "DoctorAutoId";
                ddlSurgeon.DataTextField = "DoctorDescription";
                ddlSurgeon.DataBind();

                ddlSurgeon.Items.Insert(0, item);
            }
            else
            {
                ddlSurgeon.Items.Clear();
                ddlSurgeon.DataSource = null;
                ddlSurgeon.DataBind();

                ddlSurgeon.Items.Insert(0, item);
            }

            q = "SELECT * FROM tblDoctor WHERE HospitalAutoID = " + iHospital + " AND Optometrist = 1";
            var dtOptometrist = dx.Database.SqlQuery<tblDoctor>(q).ToList();
            //var dtDoctor = (from a in dx.tblDoctors select a).ToList();
            if (dtOptometrist.Count != 0)
            {
                ddlOptometrist.DataSource = dtOptometrist;
                ddlOptometrist.DataValueField = "DoctorAutoId";
                ddlOptometrist.DataTextField = "DoctorDescription";
                ddlOptometrist.DataBind();

                ddlOptometrist.Items.Insert(0, item);
            }
            else
            {
                ddlOptometrist.Items.Clear();
                ddlOptometrist.DataSource = null;
                ddlOptometrist.DataBind();

                ddlOptometrist.Items.Insert(0, item);
            }
        }

        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedValue == "0")
            {
                pnlStudent.Visible = true;
                pnlStudent_Sub.Visible = true;
                pnlTeacher.Visible = false;
                pnlTeacher_Sub.Visible = false;
            }
            else
            {
                pnlStudent.Visible = false;
                pnlStudent_Sub.Visible = false;
                pnlTeacher.Visible = true;
                pnlTeacher_Sub.Visible = true;
            }

            ClearForm(true);
            ClearValidation();
        }

    }
}