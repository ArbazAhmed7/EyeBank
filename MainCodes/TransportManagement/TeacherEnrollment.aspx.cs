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
    public partial class TeacherEnrollment : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "TeacherEnrollment"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();
                BindCombos();

                txtTestDate.Text = Utilities.GetDate();
                if (Utilities.GetLoginUserID().ToUpper().Trim() != "ADMIN")
                {
                    txtTeacherRegNo.Enabled = false;
                }
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

                    if (txtTeacherCode.Text == "")
                    {
                        var teacherCode = dx.sp_tblTeacher_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
                        txtTeacherCode.Text = teacherCode;
                    }

                    var res = dx.sp_tblTeacher_InsertUpdate(Convert.ToInt32(hfTeacherIDPKID.Value), Convert.ToInt32(hfSchoolIDPKID.Value),
                                            txtTeacherCode.Text, txtTeacherName.Text, txtTeacherRegNo.Text, Convert.ToInt32(ddlInLaw.SelectedValue), txtFatherName.Text, Convert.ToInt32(txtAge.Text),
                                            Convert.ToInt32(ddlGender.SelectedValue), iDecreaseVision,
                                            iWearGlasses, iReligion, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text, iChiefComplain, txtChiefComplain.Text, DateTime.Parse(txtTestDate.Text),
                                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblTeacherImage tbl = new tblTeacherImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.TeacherAutoId = res.TeacherAutoId;
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

                    var res = dx.sp_tblTeacher_InsertUpdate(Convert.ToInt32(hfTeacherIDPKID.Value), Convert.ToInt32(hfSchoolIDPKID.Value),
                                            txtTeacherCode.Text, txtTeacherName.Text, txtTeacherRegNo.Text, Convert.ToInt32(ddlInLaw.SelectedValue), txtFatherName.Text, Convert.ToInt32(txtAge.Text),
                                            Convert.ToInt32(ddlGender.SelectedValue), iDecreaseVision,
                                            iWearGlasses, iReligion, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text, iChiefComplain, txtChiefComplain.Text, DateTime.Parse(txtTestDate.Text),
                                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (hfImageBytes.Value.Length > 0)
                        {
                            byte[] bytUpfile = GetUploadedImage();

                            tblTeacherImage tbl = new tblTeacherImage();
                            var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                            tbl.TeacherAutoId = res.TeacherAutoId;
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


        private byte[] GetUploadedImage()
        {
            byte[] imageBytes = Convert.FromBase64String(hfImageBytes.Value.Split(',')[1]);

            //if (btnBrowse.HasFile && btnBrowse.FileName != "")
            //{
            //    string strTestFilePath = btnBrowse.PostedFile.FileName; // This gets the full file path on the client's machine ie: c:\test\myfile.txt
            //    string strTestFileName = Path.GetFileName(strTestFilePath); // use the System.IO Path.GetFileName method to get specifics about the file without needing to parse the path as a string
            //    string ext = Path.GetExtension(btnBrowse.FileName);
            //    Int32 intFileSize = btnBrowse.PostedFile.ContentLength;
            //    string strContentType = btnBrowse.PostedFile.ContentType;

            //    // Convert the uploaded file to a byte stream to save to your database. This could be a database table field of type Image in SQL Server
            //    Stream strmStream = btnBrowse.PostedFile.InputStream;
            //    Int32 intFileLength = (Int32)strmStream.Length;
            //    bytUpfile = new byte[intFileLength + 1];
            //    strmStream.Read(bytUpfile, 0, intFileLength);
            //    strmStream.Close();
            //}
            //else if (hfImageBytes.Value.Length > 0)
            //{

            //}

            return imageBytes;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfTeacherIDPKID.Value) > 0)
                {
                    var resImage = dx.sp_tblTeacherImage_Delete(Convert.ToInt32(hfTeacherIDPKID.Value)).FirstOrDefault();
                    var res = dx.sp_tblTeacher_Delete(Convert.ToInt32(hfTeacherIDPKID.Value)).FirstOrDefault();

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

            if (txtTeacherName.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Name is required.";
                txtTeacherName.Focus();
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
                if (int.Parse(txtAge.Text.Trim()) < 14)
                {
                    lbl_error.Text = "Invalid Age: minimum 14 years is allowed";
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

            if (ddlInLaw.SelectedItem.Text == "Select")
            {
                lbl_error.Text = "Title is required.";
                ddlInLaw.Focus();
                return false;
            }

            if (ddlInLaw.SelectedValue == "0")
            {
                lbl_error.Text = "Title is required.";
                ddlInLaw.Focus();
                return false;
            }

            if (hfTeacherIDPKID.Value == "0")
            {
                if (chkNotRequired.Checked == false)
                {
                    if (TeacherImage.ImageUrl == "~/Captures/TeacherDefaultImage.jpg")
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
                txtSchoolCode.Text = "";
                txtSchoolName.Text = "";

                txtTestDate.Text = Utilities.GetDate();
            }

            hfTeacherIDPKID.Value = "0";

            txtTeacherCode.Text = "";
            txtTeacherName.Text = "";
            txtTeacherRegNo.Text = "";

            ddlInLaw.SelectedIndex = 0;

            txtFatherName.Text = "";
            txtAge.Text = "";

            ddlGender.SelectedIndex = 0;
            chkDecreasedVision.Checked = false;

            chkWearGlasses.Checked = false;

            rdoReligion.SelectedIndex = -1;

            chkOccularHistory.Checked = false;
            txtOccularHistory.Text = "";

            chkMedicalHistory.Checked = false;
            txtMedicalHistory.Text = "";

            chkChiefComplain.Checked = false;
            txtChiefComplain.Text = "";

            BindCombos();
            hfImageBytes.Value = "";

            txtCaptureDate.Text = "";
            txtCaptureRemarks.Text = "";

            TeacherImage.ImageUrl = null;
            TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/TeacherDefaultImage.jpg");

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
                var dtGender = (from a in dx.tblGenders
                                select a).ToList();

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

                var dtInLawsPatent = (from a in dx.tblInLawsPatents
                                      select a).ToList();

                if (dtInLawsPatent.Count != 0)
                {
                    ddlInLaw.DataSource = dtInLawsPatent;
                    ddlInLaw.DataValueField = "InLawsPatentAutoId";
                    ddlInLaw.DataTextField = "InLawsPatentDescription";
                    ddlInLaw.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlInLaw.Items.Insert(0, item);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }

        }

        protected void btnWebCam_Click(object sender, EventArgs e)
        {
            if (txtTeacherName.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Name is required.";
                txtTeacherName.Focus();
                return;
            }
            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('WebCam2.aspx?vFormName=WebCam.aspx&hfName=" + hfImageBytes.ID + "&Type=1&ImageName_Student=" + txtTeacherName.Text.Trim() + "','.','height=800,width=800,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
            //string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('WebCam.aspx?vFormName=WebCam.aspx&Type=1&ImageName_Teacher=" + txtTeacherName.Text.Trim() + "','','height=800,width=800,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
            //ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
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
                //string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User','','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResultSchool.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupSchool, this.GetType(), "popup", jsReport, false);
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
                string schoolIDPKID = hfSchoolIDPKID.Value;

                if (Convert.ToUInt32(schoolIDPKID) > 0)
                {
                    var dt = dx.sp_tblSchool_GetDetail(Convert.ToInt32(schoolIDPKID)).SingleOrDefault();

                    txtSchoolCode.Text = dt.SchoolCode;
                    txtSchoolName.Text = dt.SchoolName;
                }

                var teacherCode = dx.sp_tblTeacher_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
                txtTeacherCode.Text = teacherCode;

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResultSchool_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = hfLookupResultSchool.Value;
            hfSchoolIDPKID.Value = selectedPKID; //to allow update mode

            LoadSchoolDetail(selectedPKID);
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

        protected void btnLookupTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData_Teacher_FatherName(Convert.ToInt32(hfSchoolIDPKID.Value))
                                  select a).ToList().ToDataTable();

                hfLookupResultTeacher.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Teacher Code";
                Session["Name"] = "Teacher Name";
                Session["FatherName"] = "Husband / Father Name";
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
            string TeacherIDPKID = hfTeacherIDPKID.Value;

            LoadTeacherDetail(TeacherIDPKID);
        }

        protected void hfLookupResultTeacher_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = hfLookupResultTeacher.Value;
            hfTeacherIDPKID.Value = selectedPKID; //to allow update mode

            LoadTeacherDetail(selectedPKID);
        }

        private void LoadTeacherDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblTeacher_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

                    txtTestDate.Text = DateTime.Parse(dt.TeacherTestDate.ToString()).ToString("dd-MMM-yyyy");

                    if (txtSchoolCode.Text.Trim() == "")
                    {
                        txtSchoolCode.Text = dt.SchoolCode;
                        txtSchoolName.Text = dt.SchoolName;
                        hfSchoolIDPKID.Value = dt.SchoolAutoId.ToString();
                    }

                    txtTeacherCode.Text = dt.TeacherCode;
                    txtTeacherName.Text = dt.TeacherName;
                    txtTeacherRegNo.Text = dt.TeacherRegNo;
                    ddlInLaw.SelectedValue = dt.InLawsPatentAutoId.ToString();
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

                    var dtImage = dx.sp_tblTeacherImage_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    if (dtImage != null)
                    {
                        byte[] imagem = (byte[])(dtImage.TeacherPic);
                        string base64String = Convert.ToBase64String(imagem);
                        TeacherImage.ImageUrl = String.Format("data:image/jpg;base64,{0}", base64String);

                        txtCaptureDate.Text = DateTime.Parse(dtImage.CaptureDate.ToString()).ToString("dd-MMM-yyyy");
                        txtCaptureRemarks.Text = dtImage.CaptureRemarks;

                        chkNotRequired.Checked = false;
                    }
                    else
                    {
                        txtCaptureDate.Text = "";
                        txtCaptureRemarks.Text = "";

                        TeacherImage.ImageUrl = null;
                        TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/TeacherDefaultImage.jpg");

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
            if (txtTeacherName.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Name is required.";

                TeacherImage.ImageUrl = null;
                TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/TeacherDefaultImage.jpg");

                txtTeacherName.Focus();
                return;
            }

            Boolean fileOK = false;
            string path = Server.MapPath("~/Captures/UploadedTeacherImages/");
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
                    txtTeacherName.Focus();
                    return;
                }

                var filename = Server.MapPath(Path.Combine(@"~/Captures/UploadedTeacherImages/", txtTeacherName.Text + ".jpg"));
                //var filename = Server.MapPath( Path.Combine(@"~/Captures/UploadedTeacherImages/", "123.jpg"));
                if (File.Exists(filename))
                    File.Delete(filename);
                btnBrowse.SaveAs(filename);


                TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedTeacherImages/" + txtTeacherName.Text + ".jpg");
                //TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/UploadedTeacherImages/123.jpg");
            }
            else
            {
                TeacherImage.ImageUrl = null;
                TeacherImage.ImageUrl = ResolveUrl(@"~/Captures/TeacherDefaultImage.jpg");
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

        protected void txtTeacherName_TextChanged(object sender, EventArgs e)
        {
            string sTeacherName = txtTeacherName.Text;

            txtTeacherName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sTeacherName.ToLower());

            var teacherCode = dx.sp_tblTeacher_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
            txtTeacherCode.Text = teacherCode;

            txtTeacherRegNo.Focus();
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
            TeacherImage.ImageUrl = strimgBytes;
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

                tblTeacherImage tbl = new tblTeacherImage();
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };
                tbl.TeacherAutoId = int.Parse(hfTeacherIDPKID.Value);
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
        }

        protected void btnViewAllPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/ViewPhotos.aspx?winTitle=View All Photos&FormID=TeacherEnrollment&AutoKeyID=" + hfTeacherIDPKID.Value + "','.','height=400,width=600,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
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