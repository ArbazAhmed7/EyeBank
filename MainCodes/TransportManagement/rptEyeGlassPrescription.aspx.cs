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
    public partial class rptEyeGlassPrescription : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "EyeGlassPrescription"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                txtStudentCode.Focus();
            }
        }
        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        private void ClearForm()
        {
            if (rdoType.SelectedValue == "0")
            {
                txtStudentCode.Text = "";
                txtStudentName.Text = "";
                txtStudentCode_Bulk.Text = "";
                hfStudentIDPKID.Value = "0";

                txtTeacherCode.Text = "";
                txtTeacherName.Text = "";
                hfTeacherIDPKID.Value = "0";
                txtTeacherCode_Bulk.Text = "";

                txtStudentCode.Focus();
            }
            else
            {
                txtStudentCode.Text = "";
                txtStudentName.Text = "";
                txtStudentCode_Bulk.Text = "";
                hfStudentIDPKID.Value = "0";

                txtTeacherCode.Text = "";
                txtTeacherName.Text = "";
                hfTeacherIDPKID.Value = "0";
                txtTeacherCode_Bulk.Text = "";

                txtTeacherCode.Focus();
            }

            ClearValidation();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ClearValidation();
            if (rdoType.SelectedValue == "0")
            {
                if (ValidateInputStudent())
                {
                    string studentIds = string.Empty;
                    if (rdoStudentSelectionType.SelectedValue == "0")
                    {
                        studentIds = hfStudentIDPKID.Value;
                    }
                    else
                    {
                        string q = "Select * from tblStudent WHERE StudentCode IN (" + txtStudentCode_Bulk.Text.Trim() + ")";
                        DataTable dtStudent = dx.Database.SqlQuery<tblStudent>(q).ToList().ToDataTable();
                        foreach (DataRow dr in dtStudent.Rows)
                        {
                            studentIds += dr["StudentAutoId"].ToString() + ",";
                        }
                        //studentIds = "";
                    }

                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=EyeGlassPrescription_Student&StudentAutoId=" + studentIds + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                }
            }
            else
            {
                if (ValidateInputTeacher())
                {
                    string TeacherIds = string.Empty;
                    if (rdoTeacherSelectionType.SelectedValue == "0")
                    {
                        TeacherIds = hfTeacherIDPKID.Value;
                    }
                    else
                    {
                        string q = "Select * from tblTeacher WHERE TeacherCode IN (" + txtTeacherCode_Bulk.Text.Trim() + ")";
                        DataTable dtTeacher = dx.Database.SqlQuery<tblTeacher>(q).ToList().ToDataTable();
                        foreach (DataRow dr in dtTeacher.Rows)
                        {
                            TeacherIds += dr["TeacherAutoId"].ToString() + ",";
                        }
                        //TeacherIds = txtTeacherCode_Bulk.Text;
                    }

                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=EyeGlassPrescription_Teacher&TeacherAutoId=" + TeacherIds + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedValue == "0")
            {
                pnlStudent.Visible = true;
                pnlTeacher.Visible = false;

                txtStudentCode.Text = "";
                txtStudentName.Text = "";
                txtStudentCode_Bulk.Text = "";

                txtTeacherCode.Text = "";
                txtTeacherName.Text = "";
                txtTeacherCode_Bulk.Text = "";

                rdoStudentSelectionType.SelectedValue = "0";
                rdoStudentSelectionType_SelectedIndexChanged(null, null);


                ClearForm();
            }
            else
            {
                pnlStudent.Visible = false;
                pnlTeacher.Visible = true;

                txtStudentCode.Text = "";
                txtStudentName.Text = "";
                txtStudentCode_Bulk.Text = "";

                txtTeacherCode.Text = "";
                txtTeacherName.Text = "";
                txtTeacherCode_Bulk.Text = "";

                rdoTeacherSelectionType.SelectedValue = "0";
                rdoTeacherSelectionType_SelectedIndexChanged(null, null);

                ClearForm();
            }
        }

        private bool ValidateInputStudent()
        {
            if (rdoStudentSelectionType.SelectedValue == "0")
            {
                if (txtStudentCode.Text != "" && hfStudentIDPKID.Value == "0")
                {
                    lbl_error.Text = "Invalid Student Code.";
                    txtStudentCode.Focus();
                    return false;
                }

                if (txtStudentCode.Text.Trim() == "")
                {
                    lbl_error.Text = "Student Code is required.";
                    txtStudentCode.Focus();
                    return false;
                }
            }
            else
            {
                if (txtStudentCode_Bulk.Text.Trim() == "")
                {
                    lbl_error.Text = "Student Code is required.";
                    txtStudentCode_Bulk.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidateInputTeacher()
        {
            if (rdoTeacherSelectionType.SelectedValue == "0")
            {
                if (txtTeacherCode.Text != "" && hfTeacherIDPKID.Value == "0")
                {
                    lbl_error.Text = "Invalid Teacher Code.";
                    txtTeacherCode.Focus();
                    return false;
                }

                if (txtTeacherCode.Text.Trim() == "")
                {
                    lbl_error.Text = "Teacher Code is required.";
                    txtTeacherCode.Focus();
                    return false;
                }

            }
            else
            {
                if (txtTeacherCode_Bulk.Text.Trim() == "")
                {
                    lbl_error.Text = "Teacher Code is required.";
                    txtTeacherCode_Bulk.Focus();
                    return false;
                }
            }

            return true;
        }

        [WebMethod]
        public static AutoComplete[] AutoComplete(string Term, string TermType, string Id)
        {

            List<AutoComplete> lst = new List<AutoComplete>();
            //
            //if (Term.Length < 2)
            //    return lst.ToArray();

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

        protected void btnLookupStudent_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData_Student_GlassDispense(0, 0, 0)
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

                    string classSection = dt.ClassCode + " - " + dt.ClassSection;
                    if (dt.ClassSection.Trim() == "") { classSection = dt.ClassCode; }

                    txtStudentCode.Text = dt.StudentCode;
                    txtStudentName.Text = dt.StudentName;
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
                DataTable data = (from a in dx.sp_GetLookupData_Teacher_GlassDispense(0, 0, 0)
                                  select a).ToList().ToDataTable();

                hfLookupResultTeacher.Value = "0";
                Session["lookupData"] = data;

                Session["Code"] = "Teacher Code";
                Session["Name"] = "Teacher Name";
                Session["FatherName"] = "Husband / Father Name";
                Session["Description"] = "School Name";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlFatherName.aspx?winTitle=Select User&hfName=" + hfLookupResultTeacher.ID + "','.','height=600,width=650,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";

                ScriptManager.RegisterStartupScript(btnLookupTeacher, this.GetType(), "popup", jsReport, false);
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

                    txtTeacherCode.Text = dt.TeacherCode;
                    txtTeacherName.Text = dt.TeacherName;
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void rdoStudentSelectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoStudentSelectionType.SelectedValue == "0")
            {
                txtStudentCode.Enabled = true;
                txtStudentName.Enabled = true;

                txtStudentCode_Bulk.Enabled = false;

                ClearForm();

                txtStudentCode.Focus();
            }
            else
            {
                txtStudentCode.Enabled = false;
                txtStudentName.Enabled = false;

                txtStudentCode_Bulk.Enabled = true;

                ClearForm();

                txtStudentCode_Bulk.Focus();
            }
        }

        protected void rdoTeacherSelectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoTeacherSelectionType.SelectedValue == "0")
            {
                txtTeacherCode.Enabled = true;
                txtTeacherName.Enabled = true;

                txtTeacherCode_Bulk.Enabled = false;

                ClearForm();

                txtTeacherCode.Focus();
            }
            else
            {
                txtTeacherCode.Enabled = false;
                txtTeacherName.Enabled = false;

                txtTeacherCode_Bulk.Enabled = true;

                ClearForm();

                txtTeacherCode_Bulk.Focus();
            }
        }

    }
}