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
    public partial class AutoRefractionistInspection : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            string myKey = System.Configuration.ConfigurationManager.AppSettings["CoreApplication"];
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "AutoRefraction"))
            {
                var URL = $"{myKey}/Session/SessionStore/api/ClearSession";
                System.Web.UI.ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Open", "window.open('" + URL + "','Login','height=1,width=1,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no');", true);
                Response.Redirect("~/Login.aspx");

            }

            if (!IsPostBack)
            {
                InitForm();

                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                //pnlStudent_Sub.Visible = true;
                //pnlTeacher_Sub.Visible = true;

                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                txtTestDate.Text = Utilities.GetDate();

                var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                if (dtTestSummary != null)
                {
                    lblResultDate.Text = Utilities.GetDate();
                    lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                    lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                    lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                }

                DataTable dtTestDetail = dx.sp_AutoRefTest_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                if (dtTestDetail != null)
                {
                    gvRemainingList.DataSource = dtTestDetail;
                    gvRemainingList.DataBind();
                }

                txtStudentCode.Focus();
            }
        }

        private void InitForm()
        {
            btnDelete.Visible = false;
            btnEdit.Visible = false;
        }

        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedValue == "0")
            {
                pnlStudent.Visible = true;
                pnlStudent_Sub.Visible = true;

                pnlTeacher.Visible = false;
                pnlTeacher_Sub.Visible = false;

                pnlRightEye.Visible = false;
                pnlLeftEye.Visible = false;

                ClearForm();
            }
            else
            {
                pnlStudent.Visible = false;
                pnlStudent_Sub.Visible = false;

                pnlTeacher.Visible = true;
                pnlTeacher_Sub.Visible = true;

                pnlRightEye.Visible = false;
                pnlLeftEye.Visible = false;

                ClearForm();
            }

            //ClearForm();
            //ClearValidation();
        }

        protected void lblShowStudentDetail_Click(object sender, EventArgs e)
        {
            if (rdoType.SelectedValue == "0")
            {
                if (pnlStudent_Sub.Visible == false)
                {
                    pnlStudent_Sub.Visible = true;
                }
                else
                {
                    pnlStudent_Sub.Visible = false;
                }
            }
            else
            {
                if (pnlTeacher_Sub.Visible == false)
                {
                    pnlTeacher_Sub.Visible = true;
                }
                else
                {
                    pnlTeacher_Sub.Visible = false;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInputStudent())
                    {

                        string sSpherical_RightEyeType = string.Empty;
                        string sCyclinderical_RightEyeType = string.Empty;
                        string sSpherical_LeftEyeType = string.Empty;
                        string sCyclinderical_LeftEyeType = string.Empty;

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

                        decimal dtxtSpherical_RightEye = 0;
                        if (!(sSpherical_RightEyeType == "O"))
                        {
                            dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                        }

                        decimal dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());

                        int dAxixA_RightEye = int.Parse(txtAxixA_RightEye.Text.Trim());
                        int dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

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

                        decimal dtxtSpherical_LeftEye = 0;
                        if (!(sSpherical_LeftEyeType == "O"))
                        {
                            dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                        }
                        decimal dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());

                        int dAxixA_LeftEye = int.Parse(txtAxixA_LeftEye.Text.Trim());
                        int dAxixB_LeftEye = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                        var autoRefTransId = dx.sp_tblAutoRefTestStudent_GetMaxCode().SingleOrDefault();
                        hfAutoRefTestTransID.Value = autoRefTransId;

                        var res = dx.sp_tblAutoRefTestStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), hfAutoRefTestTransID.Value.Trim(), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfStudentIDPKID.Value),
                            sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye,
                            sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye,
                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
                            ShowConfirmAddMoreRecord();

                            txtTestDate.Text = Utilities.GetDate();
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

                        string sSpherical_RightEyeType = string.Empty;
                        string sCyclinderical_RightEyeType = string.Empty;
                        string sSpherical_LeftEyeType = string.Empty;
                        string sCyclinderical_LeftEyeType = string.Empty;

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

                        decimal dtxtSpherical_RightEye = 0;
                        if (!(sSpherical_RightEyeType == "O"))
                        {
                            dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                        }

                        //decimal dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                        decimal dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());

                        int dAxixA_RightEye = int.Parse(txtAxixA_RightEye.Text.Trim());
                        int dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

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

                        decimal dtxtSpherical_LeftEye = 0;
                        if (!(sSpherical_LeftEyeType == "O"))
                        {
                            dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                        }
                        //decimal dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                        decimal dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());

                        int dAxixA_LeftEye = int.Parse(txtAxixA_LeftEye.Text.Trim());
                        int dAxixB_LeftEye = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                        var autoRefTransId = dx.sp_tblAutoRefTestTeacher_GetMaxCode().SingleOrDefault();
                        hfAutoRefTestTransID.Value = autoRefTransId;

                        var res = dx.sp_tblAutoRefTestTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), hfAutoRefTestTransID.Value.Trim(), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfTeacherIDPKID.Value),
                            sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye,
                            sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye,
                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
                            ShowConfirmAddMoreRecord();

                            txtTestDate.Text = Utilities.GetDate();
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }

                var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                if (dtTestSummary != null)
                {
                    lblResultDate.Text = Utilities.GetDate();
                    lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                    lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                    lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                }

                DataTable dtTestDetail = dx.sp_AutoRefTest_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                if (dtTestDetail != null)
                {
                    gvRemainingList.DataSource = dtTestDetail;
                    gvRemainingList.DataBind();
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
                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                string sSpherical_RightEyeType = string.Empty;
                string sCyclinderical_RightEyeType = string.Empty;
                string sSpherical_LeftEyeType = string.Empty;
                string sCyclinderical_LeftEyeType = string.Empty;

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

                decimal dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEye.Text.Trim());
                decimal dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());

                int dAxixA_RightEye = int.Parse(txtAxixA_RightEye.Text.Trim());
                int dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

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

                decimal dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
                decimal dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());

                int dAxixA_LeftEye = int.Parse(txtAxixA_LeftEye.Text.Trim());
                int dAxixB_LeftEye = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInputStudent())
                    {
                        var res = dx.sp_tblAutoRefTestStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), hfAutoRefTestTransID.Value.Trim(), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfStudentIDPKID.Value),
                            sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye,
                            sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye,
                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
                            ShowConfirmAddMoreRecord();

                            txtTestDate.Text = Utilities.GetDate();
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
                        var res = dx.sp_tblAutoRefTestTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), hfAutoRefTestTransID.Value.Trim(), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfTeacherIDPKID.Value),
                            sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye,
                            sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye,
                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
                            ShowConfirmAddMoreRecord();

                            txtTestDate.Text = Utilities.GetDate();
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                }

                var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                if (dtTestSummary != null)
                {
                    lblResultDate.Text = Utilities.GetDate();
                    lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                    lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                    lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                }

                DataTable dtTestDetail = dx.sp_AutoRefTest_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                if (dtTestDetail != null)
                {
                    gvRemainingList.DataSource = dtTestDetail;
                    gvRemainingList.DataBind();
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
                if (Convert.ToInt32(hfAutoRefTestIDPKID.Value) > 0)
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        var res = dx.sp_tblAutoRefTestStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            string studentId = hfStudentIDPKID.Value;

                            ClearForm();

                            //hfStudentIDPKID.Value = studentId;

                            //LoadStudentDetail(hfStudentIDPKID.Value);

                            lbl_error.Text = res.RetMessage;
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                    else
                    {
                        var res = dx.sp_tblAutoRefTestTeacher_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            string teacherId = hfTeacherIDPKID.Value;

                            ClearForm();

                            //hfTeacherIDPKID.Value = teacherId;

                            //LoadTeacherDetail(hfTeacherIDPKID.Value);

                            lbl_error.Text = res.RetMessage;
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

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private bool ValidateInputStudent()
        {
            ClearValidation();

            if (txtStudentCode.Text != "" && hfStudentIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Student Code.";
                txtStudentCode.Focus();
                return false;
            }

            if (txtStudentName.Text != "" && hfStudentIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Student Name.";
                txtStudentName.Focus();
                return false;
            }

            try
            {
                DateTime dt = DateTime.Parse(txtTestDate.Text);
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Transaction Date.";
                txtTestDate.Focus();
                return false;
            }

            if (txtStudentCode.Text.Trim() == "")
            {
                lbl_error.Text = "Student Code is required.";
                txtStudentCode.Focus();
                return false;
            }

            if (ddlSpherical_RightEye.SelectedItem.Text != "Plano")
            {
                if (txtSpherical_RightEye.Text.Trim() == "")
                {
                    lbl_error.Text = "Spherical result for Right Eye is required.";
                    txtSpherical_RightEye.Focus();
                    return false;
                }
            }
            else
            {
                txtSpherical_RightEye.Text = "00.00";
            }

            try
            {
                decimal d = decimal.Parse(txtSpherical_RightEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Right Eye Points.";
                txtSpherical_RightEye.Focus();
                return false;
            }

            if (txtCyclinderical_RightEye.Text.Trim() == "")
            {
                lbl_error.Text = "Cyclinderical result for Right Eye is required.";
                txtCyclinderical_RightEye.Focus();
                return false;
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
                txtCyclinderical_RightEye.Focus();
                return false;
            }

            if (txtAxixA_RightEye.Text.Trim() == "")
            {
                lbl_error.Text = "Axix result for Right Eye is required.";
                txtAxixA_RightEye.Focus();
                return false;
            }

            //if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_RightEye.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_RightEye.Focus();
                return false;
            }

            if (ddlSpherical_LeftEye.SelectedItem.Text != "Plano")
            {
                if (txtSpherical_LeftEye.Text.Trim() == "")
                {
                    lbl_error.Text = "Spherical result for Left Eye is required.";
                    txtSpherical_LeftEye.Focus();
                    return false;
                }
            }
            else
            {
                txtSpherical_LeftEye.Text = "00.00";
            }

            try
            {
                decimal d = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Left Eye Points.";
                txtSpherical_LeftEye.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEye.Text.Trim() == "")
            {
                lbl_error.Text = "Cyclinderical result for Left Eye is required.";
                txtCyclinderical_LeftEye.Focus();
                return false;
            }

            if (txtAxixA_LeftEye.Text.Trim() == "")
            {
                lbl_error.Text = "Axix result for Left Eye is required.";
                txtAxixA_LeftEye.Focus();
                return false;
            }

            //if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_LeftEye.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_LeftEye.Focus();
                return false;
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
                txtCyclinderical_LeftEye.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateInputTeacher()
        {
            ClearValidation();

            // Invalid Code - Database Validation for Student / Teacher
            try
            {
                DateTime dt = DateTime.Parse(txtTestDate.Text);
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Transaction Date.";
                txtTestDate.Focus();
                return false;
            }

            if (txtTeacherCode.Text != "" && hfTeacherIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Teacher Code.";
                txtTeacherCode.Focus();
                return false;
            }

            if (txtTeacherName.Text != "" && hfTeacherIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Teacher Name.";
                txtTeacherName.Focus();
                return false;
            }

            if (txtTeacherCode.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Code is required.";
                txtTeacherCode.Focus();
                return false;
            }

            if (ddlSpherical_RightEye.SelectedItem.Text != "Plano")
            {
                if (txtSpherical_RightEye.Text.Trim() == "")
                {
                    lbl_error.Text = "Spherical result for Right Eye is required.";
                    txtSpherical_RightEye.Focus();
                    return false;
                }
            }
            else
            {
                txtSpherical_RightEye.Text = "00.00";
            }

            try
            {
                decimal d = decimal.Parse(txtSpherical_RightEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Right Eye Points.";
                txtSpherical_RightEye.Focus();
                return false;
            }

            if (txtCyclinderical_RightEye.Text.Trim() == "")
            {
                lbl_error.Text = "Cyclinderical result for Right Eye is required.";
                txtCyclinderical_RightEye.Focus();
                return false;
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_RightEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
                txtCyclinderical_RightEye.Focus();
                return false;
            }

            if (txtAxixA_RightEye.Text.Trim() == "")
            {
                lbl_error.Text = "Axix result for Right Eye is required.";
                txtAxixA_RightEye.Focus();
                return false;
            }

            //if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_RightEye.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_RightEye.Focus();
                return false;
            }

            if (ddlSpherical_LeftEye.SelectedItem.Text != "Plano")
            {
                if (txtSpherical_LeftEye.Text.Trim() == "")
                {
                    lbl_error.Text = "Spherical result for Left Eye is required.";
                    txtSpherical_LeftEye.Focus();
                    return false;
                }
            }
            else
            {
                txtSpherical_LeftEye.Text = "00.00";
            }

            try
            {
                decimal d = decimal.Parse(txtSpherical_LeftEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Left Eye Points.";
                txtSpherical_LeftEye.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEye.Text.Trim() == "")
            {
                lbl_error.Text = "Cyclinderical result for Left Eye is required.";
                txtCyclinderical_LeftEye.Focus();
                return false;
            }

            if (txtAxixA_LeftEye.Text.Trim() == "")
            {
                lbl_error.Text = "Axix result for Left Eye is required.";
                txtAxixA_LeftEye.Focus();
                return false;
            }

            //if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_LeftEye.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_LeftEye.Focus();
                return false;
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_LeftEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
                txtCyclinderical_LeftEye.Focus();
                return false;
            }

            return true;
        }
        private void ClearForm()
        {
            InitForm();

            hfAutoRefTestIDPKID.Value = "0";
            hfStudentIDPKID.Value = "0";
            hfTeacherIDPKID.Value = "0";

            txtTestDate.Text = Utilities.GetDate();

            txtStudentCode.Text = "";
            txtStudentName.Text = "";

            ddlPreviousTestResult.Items.Clear();
            ddlPreviousTestResult.DataSource = null;
            ddlPreviousTestResult.DataBind();

            lblFatherName_Student.Text = "";
            lblAge_Student.Text = "";
            lblDecreasedVision_Student.Text = "";
            lblWearingGlasses_Student.Text = "";
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

            ddlSpherical_RightEye.SelectedIndex = 0;
            txtSpherical_RightEye.Text = "";

            ddlCyclinderical_RightEye.SelectedIndex = 0;
            txtCyclinderical_RightEye.Text = "";

            txtAxixA_RightEye.Text = "";
            txtAxixB_RightEye.Text = "";

            ddlSpherical_LeftEye.SelectedIndex = 0;
            txtSpherical_LeftEye.Text = "";

            ddlCyclinderical_LeftEye.SelectedIndex = 0;
            txtCyclinderical_LeftEye.Text = "";

            txtAxixA_LeftEye.Text = "";
            txtAxixB_LeftEye.Text = "";

            ClearValidation();

            gvAutoRef.DataSource = null;
            gvAutoRef.DataBind();

            txtStudentCode.Focus();
        }

        private void ClearTransactionData()
        {
            ddlSpherical_RightEye.SelectedIndex = 0;
            txtSpherical_RightEye.Text = "";

            ddlCyclinderical_RightEye.SelectedIndex = 0;
            txtCyclinderical_RightEye.Text = "";

            txtAxixA_RightEye.Text = "";
            txtAxixB_RightEye.Text = "";

            ddlSpherical_LeftEye.SelectedIndex = 0;
            txtSpherical_LeftEye.Text = "";

            ddlCyclinderical_LeftEye.SelectedIndex = 0;
            txtCyclinderical_LeftEye.Text = "";

            txtAxixA_LeftEye.Text = "";
            txtAxixB_LeftEye.Text = "";
        }

        private void EnableDisableControls(bool enable)
        {
            ddlSpherical_RightEye.Enabled = enable;
            txtSpherical_RightEye.Enabled = enable;

            ddlCyclinderical_RightEye.Enabled = enable;
            txtCyclinderical_RightEye.Enabled = enable;

            txtAxixA_RightEye.Enabled = enable;
            txtAxixB_RightEye.Enabled = enable;

            ddlSpherical_LeftEye.Enabled = enable;
            txtSpherical_LeftEye.Enabled = enable;

            ddlCyclinderical_LeftEye.Enabled = enable;
            txtCyclinderical_LeftEye.Enabled = enable;

            txtAxixA_LeftEye.Enabled = enable;
            txtAxixB_LeftEye.Enabled = enable;
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

        protected void hfAutoRefTestIDPKID_ValueChanged(object sender, EventArgs e)
        {
            string autoRefTestIDPKID = hfAutoRefTestIDPKID.Value;
            if (Convert.ToUInt32(autoRefTestIDPKID) == 0)
            {
                ClearTransactionData();
            }

            if (Convert.ToUInt32(autoRefTestIDPKID) > 0)
            {
                if (rdoType.SelectedValue == "0")
                {
                    ClearTransactionData();

                    var dt = dx.sp_tblAutoRefTestStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransID.Value = dt.AutoRefStudentTransId.ToString();
                    hfAutoRefTestTransDate.Value = dt.AutoRefStudentTransDate.ToString();

                    //hfStudentIDPKID.Value = dt.StudentAutoId.ToString();
                    //hfStudentIDPKID_ValueChanged(null, null);

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

                    txtSpherical_RightEye.Text = dt.Right_Spherical_Points.ToString();

                    if (dt.Right_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 1;
                    }
                    txtCyclinderical_RightEye.Text = dt.Right_Cyclinderical_Points.ToString();

                    txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    txtAxixB_RightEye.Text = dt.Right_Axix_To.ToString();

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
                    txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    if (dt.Left_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 1;
                    }
                    txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    txtAxixB_LeftEye.Text = dt.Left_Axix_To.ToString();
                }
                else
                {
                    ClearTransactionData();

                    var dt = dx.sp_tblAutoRefTestTeacher_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransID.Value = dt.AutoRefTeacherTransId.ToString();
                    hfAutoRefTestTransDate.Value = dt.AutoRefTeacherTransDate.ToString();

                    //hfTeacherIDPKID.Value = dt.TeacherAutoId.ToString();
                    //hfTeacherIDPKID_ValueChanged(null, null);

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
                    txtSpherical_RightEye.Text = dt.Right_Spherical_Points.ToString();

                    if (dt.Right_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_RightEye.SelectedIndex = 1;
                    }
                    txtCyclinderical_RightEye.Text = dt.Right_Cyclinderical_Points.ToString();

                    txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    txtAxixB_RightEye.Text = dt.Right_Axix_To.ToString();

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
                    txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    if (dt.Left_Cyclinderical_Status == "P")
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_LeftEye.SelectedIndex = 1;
                    }
                    txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    txtAxixB_LeftEye.Text = dt.Left_Axix_To.ToString();
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        protected void btnLookupStudent_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData_Student_FatherName(0, 0)
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
                lbl_error.Text = "";

                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblStudent_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

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

                    //txtTestDate.Text = DateTime.Parse(dt.StudentTestDate.ToString()).ToString("dd-MMM-yyyy");

                    pnlRightEye.Visible = true;
                    pnlLeftEye.Visible = true;

                    var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                    if (dtTestSummary != null)
                    {
                        lblResultDate.Text = Utilities.GetDate();
                        lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                        lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                        lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                    }

                    DataTable dtTestDetail = dx.sp_AutoRefTest_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                    if (dtTestDetail != null)
                    {
                        gvRemainingList.DataSource = dtTestDetail;
                        gvRemainingList.DataBind();
                    }

                    var dtPreviousData = dx.sp_tblAutoRefTestStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "AutoRefStudentId";
                            ddlPreviousTestResult.DataTextField = "AutoRefStudentTransDate";
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

                    var dtGridData = dx.sp_tblAutoRefTestStudent_GetGridData(Convert.ToInt32(ID)).ToList();

                    try
                    {
                        if (dtGridData.Count != 0)
                        {
                            gvAutoRef.DataSource = dtGridData;
                            gvAutoRef.DataBind();
                        }
                        else
                        {
                            gvAutoRef.DataSource = null;
                            gvAutoRef.DataBind();
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
                DataTable data = (from a in dx.sp_GetLookupData_Teacher_FatherName(0)
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
                lbl_error.Text = "";

                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblTeacher_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();

                    txtTeacherCode.Text = dt.TeacherCode;
                    txtTeacherName.Text = dt.TeacherName;
                    lblFatherName_Teacher.Text = dt.FatherName;
                    lblAge_Teacher.Text = dt.Age.ToString();
                    lblGender_Teacher.Text = dt.Gender;
                    lblSchoolName_Teacher.Text = dt.SchoolName;
                    lblWearingGlasses_Teacher.Text = dt.WearGlasses == 0 ? "No" : "Yes";
                    lblDecreasedVision_Teacher.Text = dt.DecreasedVision == 0 ? "No" : "Yes";

                    //txtTestDate.Text = DateTime.Parse(dt.TeacherTestDate.ToString()).ToString("dd-MMM-yyyy");

                    pnlRightEye.Visible = true;
                    pnlLeftEye.Visible = true;

                    var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                    if (dtTestSummary != null)
                    {
                        lblResultDate.Text = Utilities.GetDate();
                        lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                        lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                        lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                    }

                    DataTable dtTestDetail = dx.sp_AutoRefTest_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                    if (dtTestDetail != null)
                    {
                        gvRemainingList.DataSource = dtTestDetail;
                        gvRemainingList.DataBind();
                    }

                    var dtPreviousData = dx.sp_tblAutoRefTestTeacher_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "AutoRefTeacherId";
                            ddlPreviousTestResult.DataTextField = "AutoRefTeacherTransDate";
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

                    var dtGridData = dx.sp_tblAutoRefTestTeacher_GetGridData(Convert.ToInt32(ID)).ToList();

                    try
                    {
                        if (dtGridData.Count != 0)
                        {
                            gvAutoRef.DataSource = dtGridData;
                            gvAutoRef.DataBind();
                        }
                        else
                        {
                            gvAutoRef.DataSource = null;
                            gvAutoRef.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }
                    //btnEdit.Visible = true;
                    //btnDelete.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void rdoOldNewTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoOldNewTest.SelectedIndex == 0)
            {
                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;
                EnableDisableControls(true);
                ClearTransactionData();
                InitForm();
            }
            else
            {
                txtTestDate.Visible = false;
                ddlPreviousTestResult.Visible = true;
                EnableDisableControls(true);
                ClearTransactionData();
                InitForm();
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

        protected void ddlPreviousTestResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPreviousTestResult = ddlPreviousTestResult.SelectedValue;
            hfAutoRefTestIDPKID.Value = strPreviousTestResult;
            hfAutoRefTestIDPKID_ValueChanged(null, null);

            EnableDisableControls(true);
            //InitForm();
            btnEdit.Visible = true;
            btnDelete.Visible = true;
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        //protected void ddlSpherical_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtSpherical_RightEye.Focus();
        //}

        //protected void ddlCyclinderical_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtCyclinderical_RightEye.Focus();
        //}

        //protected void ddlSpherical_LeftEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtSpherical_LeftEye.Focus();
        //}

        //protected void ddlCyclinderical_LeftEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtCyclinderical_LeftEye.Focus();
        //}

        //protected void txtSpherical_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(ddlSpherical_RightEye.SelectedItem.Text == "Plano")
        //        {
        //            txtSpherical_RightEye.Text = "00.00";
        //        }
        //        if (!(txtSpherical_RightEye.Text == "" || txtSpherical_RightEye.Text == "0.00"))
        //        {
        //            txtSpherical_RightEye.Text = decimal.Parse(txtSpherical_RightEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtSpherical_RightEye.Text = "0.0";
        //    }
        //    ddlCyclinderical_RightEye.Focus();
        //}

        //protected void txtCyclinderical_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!(txtCyclinderical_RightEye.Text == "" || txtCyclinderical_RightEye.Text == "0.00"))
        //        {
        //            txtCyclinderical_RightEye.Text = decimal.Parse(txtCyclinderical_RightEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtCyclinderical_RightEye.Text = "0.0";
        //    }
        //    txtAxixA_RightEye.Focus();
        //}

        //protected void txtSpherical_LeftEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlSpherical_LeftEye.SelectedItem.Text == "Plano")
        //        {
        //            txtSpherical_LeftEye.Text = "00.00";
        //        }
        //        if (!(txtSpherical_LeftEye.Text == "" || txtSpherical_LeftEye.Text == "0.00"))
        //        {
        //            txtSpherical_LeftEye.Text = decimal.Parse(txtSpherical_LeftEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtSpherical_LeftEye.Text = "0.0";
        //    }
        //    ddlCyclinderical_RightEye.Focus();
        //}

        //protected void txtCyclinderical_LeftEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!(txtCyclinderical_LeftEye.Text == "" || txtCyclinderical_LeftEye.Text == "0.00"))
        //        {
        //            txtCyclinderical_LeftEye.Text = decimal.Parse(txtCyclinderical_LeftEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtCyclinderical_LeftEye.Text = "0.0";
        //    }
        //    txtAxixA_LeftEye.Focus();
        //}

        //protected void txtAxixA_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (decimal.Parse(txtAxixA_RightEye.Text) == 0 ||decimal.Parse(txtAxixA_RightEye.Text) > 180)
        //        {
        //            lbl_error.Text = "Invalid Axix";
        //            txtAxixA_RightEye.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtAxixA_RightEye.Text = "0";
        //    }
        //    ddlSpherical_LeftEye.Focus();
        //}

        //protected void txtAxixA_LeftEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (decimal.Parse(txtAxixA_LeftEye.Text) == 0 || decimal.Parse(txtAxixA_LeftEye.Text) > 180)
        //        {
        //            lbl_error.Text = "Invalid Axix";
        //            txtAxixA_LeftEye.Focus();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtAxixA_LeftEye.Text = "0";
        //    }
        //    btnSave.Focus();
        //}

        //protected void ddlSpherical_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if(ddlSpherical_RightEye.SelectedItem.Text == "Plano")
        //    {
        //        txtSpherical_RightEye.Text = "0.00";
        //        txtSpherical_RightEye.Focus();
        //    }
        //    else
        //    {
        //        txtSpherical_RightEye.Focus();
        //    }
        //}

        //protected void ddlSpherical_LeftEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSpherical_LeftEye.SelectedItem.Text == "Plano")
        //    {
        //        txtSpherical_LeftEye.Text = "0.00";
        //        txtSpherical_LeftEye.Focus();
        //    }
        //    else
        //    {
        //        txtSpherical_LeftEye.Focus();
        //    }
        //}
    }
}