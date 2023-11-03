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
    public partial class HospitalVisitForAfterSurgery : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "HospitalVisitForAfterSurgery"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

                rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

                //rdoNotRequiredFollowup.Checked = false;
                //rdoFollowupAfterSixMonths.Checked = false;

                pnlTestSummary.Visible = false;

                txtTestDate.Text = Utilities.GetDate();
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
                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                if (ValidateInputStudent())
                {
                    int iDistanceVision_RightEye_Unaided = -1;
                    if (rdoDistanceVision_RightEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_Unaided = int.Parse(rdoDistanceVision_RightEye_Unaided.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_Unaided = -1;
                    if (rdoDistanceVision_LeftEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);
                    }

                    int iDistanceVision_RightEye_WithGlasses = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_WithGlasses = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }

                    int iDistanceVision_RightEye_PinHole = -1;
                    if (rdoDistanceVision_RightEye_PinHole.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_Pinhole = -1;
                    if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);
                    }

                    int iNearVision_RightEye = 0;
                    iNearVision_RightEye = int.Parse(rdoNearVision_RightEye.SelectedValue);
                    int iNearVision_LeftEye = 0;
                    iNearVision_LeftEye = int.Parse(rdoNearVision_LeftEye.SelectedValue);

                    //string sSpherical_RightEyeType_AutoRef = string.Empty;
                    //string sCyclinderical_RightEyeType_AutoRef = string.Empty;
                    //string sSpherical_LeftEyeType_AutoRef = string.Empty;
                    //string sCyclinderical_LeftEyeType_AutoRef = string.Empty;

                    //if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Positive")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "P";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Negative")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "N";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Plano")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "O";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Error")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "E";
                    //}

                    //if (ddlCyclinderical_RightEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_RightEyeType_AutoRef = "P"; }
                    //else { sCyclinderical_RightEyeType_AutoRef = "N"; }

                    //decimal dtxtSpherical_RightEye_AutoRef = 0;
                    //if (!(sSpherical_RightEyeType_AutoRef == "O"))
                    //{
                    //    dtxtSpherical_RightEye_AutoRef = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
                    //}

                    //decimal dCyclinderical_RightEye_AutoRef = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());

                    //int dAxixA_RightEye_AutoRef = int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim());
                    //int dAxixB_RightEye_AutoRef = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

                    //if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Positive")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "P";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Negative")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "N";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Plano")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "O";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Error")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "E";
                    //}

                    //if (ddlCyclinderical_LeftEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_LeftEyeType_AutoRef = "P"; }
                    //else { sCyclinderical_LeftEyeType_AutoRef = "N"; }

                    //decimal dtxtSpherical_LeftEye_AutoRef = 0;
                    //if (!(sSpherical_LeftEyeType_AutoRef == "O"))
                    //{
                    //    dtxtSpherical_LeftEye_AutoRef = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
                    //}
                    //decimal dCyclinderical_LeftEye_AutoRef = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());

                    //int dAxixA_LeftEye_AutoRef = int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim());
                    //int dAxixB_LeftEye_AutoRef = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                    //string sSpherical_RightEyeType = string.Empty;
                    //string sCyclinderical_RightEyeType = string.Empty;
                    //string sNear_RightEyeType = string.Empty;

                    //string sSpherical_LeftEyeType = string.Empty;
                    //string sCyclinderical_LeftEyeType = string.Empty;
                    //string sNear_LeftEyeType = string.Empty;

                    //if (ddlSphericalRightEyeSR.SelectedValue == "Positive")
                    //{
                    //    sSpherical_RightEyeType = "P";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Negative")
                    //{
                    //    sSpherical_RightEyeType = "N";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Plano")
                    //{
                    //    sSpherical_RightEyeType = "O";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Error")
                    //{
                    //    sSpherical_RightEyeType = "E";
                    //}

                    //if (ddlCyclindericalRightEyeSR.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    //else { sCyclinderical_RightEyeType = "N"; }

                    //if (ddlNear_RightEyeSR.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    //else { sNear_RightEyeType = "N"; }

                    //decimal dtxtSpherical_RightEye = 0;
                    //decimal dCyclinderical_RightEye = 0;
                    //decimal dNear_RightEye = 0;
                    //try
                    //{
                    //    dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
                    //    dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
                    //    dNear_RightEye = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dtxtSpherical_RightEye = 0;
                    //    dCyclinderical_RightEye = 0;
                    //    dNear_RightEye = 0;
                    //}

                    //int dAxixA_RightEye = 0;
                    //int dAxixB_RightEye = 0;
                    //try
                    //{
                    //    dAxixA_RightEye = int.Parse(txtAxixA_RightEyeSR.Text.Trim());
                    //    dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dAxixA_RightEye = 0;
                    //    dAxixB_RightEye = 0;
                    //}

                    //if (ddlSphericalLeftEyeSR.SelectedValue == "Positive")
                    //{
                    //    sSpherical_LeftEyeType = "P";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Negative")
                    //{
                    //    sSpherical_LeftEyeType = "N";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Plano")
                    //{
                    //    sSpherical_LeftEyeType = "O";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Error")
                    //{
                    //    sSpherical_LeftEyeType = "E";
                    //}

                    //if (ddlCyclindericalLeftEyeSR.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    //else { sCyclinderical_LeftEyeType = "N"; }

                    //if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    //else { sNear_LeftEyeType = "N"; }

                    //decimal dtxtSpherical_LeftEye = 0;
                    //decimal dCyclinderical_LeftEye = 0;
                    //decimal dNear_LeftEye = 0;

                    //try
                    //{
                    //    dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
                    //    dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
                    //    dNear_LeftEye = decimal.Parse(txtNear_LeftEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dtxtSpherical_LeftEye = 0;
                    //    dCyclinderical_LeftEye = 0;
                    //    dNear_LeftEye = 0;
                    //}

                    //int dAxixA_LeftEye = 0;
                    //int dAxixB_LeftEye = 0;

                    //try
                    //{
                    //    dAxixA_LeftEye = int.Parse(txtAxixA_LeftEyeSR.Text.Trim());
                    //    dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dAxixA_LeftEye = 0;
                    //    dAxixB_LeftEye = 0;
                    //}

                    int iPostOPTCondition = -1;
                    if (rdoPostOPTCondition.SelectedValue != "")
                    {
                        iPostOPTCondition = int.Parse(rdoPostOPTCondition.SelectedValue);
                    }

                    int iSquintPostOPTCondition = -1;
                    if (rdoSquintPostOptCond.SelectedValue != "")
                    {
                        iSquintPostOPTCondition = int.Parse(rdoSquintPostOptCond.SelectedValue);
                    }

                    int iNextVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iNextVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iNextVisit = 1;
                    }
                    DateTime dtNextVisitDate = DateTime.Parse("1900-01-01");
                    if (iNextVisit == 1)
                    {
                        if (txtNextVisitDate.Text != "")
                        {
                            dtNextVisitDate = DateTime.Parse(txtNextVisitDate.Text);
                        }
                    }


                    DateTime dtTest;
                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        dtTest = DateTime.Parse(txtTestDate.Text);
                    }
                    else
                    {
                        dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                    }

                    var res = dx.sp_tblVisitAfterSurgeryStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye,
                        "", 0, "", 0, 0, 0,
                        "", 0, "", 0, 0, 0,
                        "", 0, "", 0, 0, 0, "", 0,
                        "", 0, "", 0, 0, 0, "", 0,
                        iPostOPTCondition, iSquintPostOPTCondition, txtMedicines.Text, txtSurgeonRemarks.Text, iNextVisit, dtNextVisitDate,
                        strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    //var res = dx.sp_tblVisitAfterSurgeryStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                    //    Convert.ToInt32(hfStudentIDPKID.Value),
                    //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye,
                    //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye,
                    //    sSpherical_RightEyeType_AutoRef, dtxtSpherical_RightEye_AutoRef, sCyclinderical_RightEyeType_AutoRef, dCyclinderical_RightEye_AutoRef, dAxixA_RightEye_AutoRef, dAxixB_RightEye_AutoRef,
                    //    sSpherical_LeftEyeType_AutoRef, dtxtSpherical_LeftEye_AutoRef, sCyclinderical_LeftEyeType_AutoRef, dCyclinderical_LeftEye_AutoRef, dAxixA_LeftEye_AutoRef, dAxixB_LeftEye_AutoRef,
                    //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                    //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,
                    //    iPostOPTCondition, iSquintPostOPTCondition, txtMedicines.Text, txtSurgeonRemarks.Text, DateTime.Parse(txtNextVisitDate.Text),
                    //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();


                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
                        ShowConfirmAddMoreRecord();

                        pnlTestSummary.Visible = false;

                        txtTestDate.Text = Utilities.GetDate();
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
                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                if (ValidateInputStudent())
                {
                    int iDistanceVision_RightEye_Unaided = -1;
                    if (rdoDistanceVision_RightEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_Unaided = int.Parse(rdoDistanceVision_RightEye_Unaided.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_Unaided = -1;
                    if (rdoDistanceVision_LeftEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);
                    }

                    int iDistanceVision_RightEye_WithGlasses = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_WithGlasses = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }

                    int iDistanceVision_RightEye_PinHole = -1;
                    if (rdoDistanceVision_RightEye_PinHole.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);
                    }

                    int iDistanceVision_LeftEye_Pinhole = -1;
                    if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);
                    }

                    int iNearVision_RightEye = 0;
                    iNearVision_RightEye = int.Parse(rdoNearVision_RightEye.SelectedValue);
                    int iNearVision_LeftEye = 0;
                    iNearVision_LeftEye = int.Parse(rdoNearVision_LeftEye.SelectedValue);

                    //string sSpherical_RightEyeType_AutoRef = string.Empty;
                    //string sCyclinderical_RightEyeType_AutoRef = string.Empty;
                    //string sSpherical_LeftEyeType_AutoRef = string.Empty;
                    //string sCyclinderical_LeftEyeType_AutoRef = string.Empty;

                    //if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Positive")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "P";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Negative")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "N";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Plano")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "O";
                    //}
                    //else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Error")
                    //{
                    //    sSpherical_RightEyeType_AutoRef = "E";
                    //}

                    //if (ddlCyclinderical_RightEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_RightEyeType_AutoRef = "P"; }
                    //else { sCyclinderical_RightEyeType_AutoRef = "N"; }

                    //decimal dtxtSpherical_RightEye_AutoRef = 0;
                    //if (!(sSpherical_RightEyeType_AutoRef == "O"))
                    //{
                    //    dtxtSpherical_RightEye_AutoRef = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
                    //}

                    //decimal dCyclinderical_RightEye_AutoRef = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());

                    //int dAxixA_RightEye_AutoRef = int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim());
                    //int dAxixB_RightEye_AutoRef = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

                    //if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Positive")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "P";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Negative")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "N";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Plano")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "O";
                    //}
                    //else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Error")
                    //{
                    //    sSpherical_LeftEyeType_AutoRef = "E";
                    //}

                    //if (ddlCyclinderical_LeftEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_LeftEyeType_AutoRef = "P"; }
                    //else { sCyclinderical_LeftEyeType_AutoRef = "N"; }

                    //decimal dtxtSpherical_LeftEye_AutoRef = 0;
                    //if (!(sSpherical_LeftEyeType_AutoRef == "O"))
                    //{
                    //    dtxtSpherical_LeftEye_AutoRef = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
                    //}
                    //decimal dCyclinderical_LeftEye_AutoRef = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());

                    //int dAxixA_LeftEye_AutoRef = int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim());
                    //int dAxixB_LeftEye_AutoRef = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                    //string sSpherical_RightEyeType = string.Empty;
                    //string sCyclinderical_RightEyeType = string.Empty;
                    //string sNear_RightEyeType = string.Empty;

                    //string sSpherical_LeftEyeType = string.Empty;
                    //string sCyclinderical_LeftEyeType = string.Empty;
                    //string sNear_LeftEyeType = string.Empty;

                    //if (ddlSphericalRightEyeSR.SelectedValue == "Positive")
                    //{
                    //    sSpherical_RightEyeType = "P";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Negative")
                    //{
                    //    sSpherical_RightEyeType = "N";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Plano")
                    //{
                    //    sSpherical_RightEyeType = "O";
                    //}
                    //else if (ddlSphericalRightEyeSR.SelectedValue == "Error")
                    //{
                    //    sSpherical_RightEyeType = "E";
                    //}

                    //if (ddlCyclindericalRightEyeSR.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    //else { sCyclinderical_RightEyeType = "N"; }

                    //if (ddlNear_RightEyeSR.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    //else { sNear_RightEyeType = "N"; }

                    //decimal dtxtSpherical_RightEye = 0;
                    //decimal dCyclinderical_RightEye = 0;
                    //decimal dNear_RightEye = 0;
                    //try
                    //{
                    //    dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
                    //    dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
                    //    dNear_RightEye = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dtxtSpherical_RightEye = 0;
                    //    dCyclinderical_RightEye = 0;
                    //    dNear_RightEye = 0;
                    //}

                    //int dAxixA_RightEye = 0;
                    //int dAxixB_RightEye = 0;
                    //try
                    //{
                    //    dAxixA_RightEye = int.Parse(txtAxixA_RightEyeSR.Text.Trim());
                    //    dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dAxixA_RightEye = 0;
                    //    dAxixB_RightEye = 0;
                    //}

                    //if (ddlSphericalLeftEyeSR.SelectedValue == "Positive")
                    //{
                    //    sSpherical_LeftEyeType = "P";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Negative")
                    //{
                    //    sSpherical_LeftEyeType = "N";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Plano")
                    //{
                    //    sSpherical_LeftEyeType = "O";
                    //}
                    //else if (ddlSphericalLeftEyeSR.SelectedValue == "Error")
                    //{
                    //    sSpherical_LeftEyeType = "E";
                    //}

                    //if (ddlCyclindericalLeftEyeSR.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    //else { sCyclinderical_LeftEyeType = "N"; }

                    //if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    //else { sNear_LeftEyeType = "N"; }

                    //decimal dtxtSpherical_LeftEye = 0;
                    //decimal dCyclinderical_LeftEye = 0;
                    //decimal dNear_LeftEye = 0;

                    //try
                    //{
                    //    dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
                    //    dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
                    //    dNear_LeftEye = decimal.Parse(txtNear_LeftEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dtxtSpherical_LeftEye = 0;
                    //    dCyclinderical_LeftEye = 0;
                    //    dNear_LeftEye = 0;
                    //}

                    //int dAxixA_LeftEye = 0;
                    //int dAxixB_LeftEye = 0;

                    //try
                    //{
                    //    dAxixA_LeftEye = int.Parse(txtAxixA_LeftEyeSR.Text.Trim());
                    //    dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    //}
                    //catch
                    //{
                    //    dAxixA_LeftEye = 0;
                    //    dAxixB_LeftEye = 0;
                    //}

                    int iPostOPTCondition = -1;
                    if (rdoPostOPTCondition.SelectedValue != "")
                    {
                        iPostOPTCondition = int.Parse(rdoPostOPTCondition.SelectedValue);
                    }

                    int iSquintPostOPTCondition = -1;
                    if (rdoSquintPostOptCond.SelectedValue != "")
                    {
                        iSquintPostOPTCondition = int.Parse(rdoSquintPostOptCond.SelectedValue);
                    }

                    int iNextVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iNextVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iNextVisit = 1;
                    }
                    DateTime dtNextVisitDate = DateTime.Parse("1900-01-01");
                    if (iNextVisit == 1)
                    {
                        if (txtNextVisitDate.Text != "")
                        {
                            dtNextVisitDate = DateTime.Parse(txtNextVisitDate.Text);
                        }
                    }


                    DateTime dtTest;
                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        dtTest = DateTime.Parse(txtTestDate.Text);
                    }
                    else
                    {
                        dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                    }

                    var res = dx.sp_tblVisitAfterSurgeryStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye,
                        "", 0, "", 0, 0, 0,
                        "", 0, "", 0, 0, 0,
                        "", 0, "", 0, 0, 0, "", 0,
                        "", 0, "", 0, 0, 0, "", 0,
                        iPostOPTCondition, iSquintPostOPTCondition, txtMedicines.Text, txtSurgeonRemarks.Text, iNextVisit, dtNextVisitDate,
                        strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    //sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                    //sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,


                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        ClearForm();
                        ShowConfirmAddMoreRecord();

                        pnlTestSummary.Visible = false;

                        txtTestDate.Text = Utilities.GetDate();
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
                if (Convert.ToInt32(hfAutoRefTestIDPKID.Value) > 0)
                {

                    var res = dx.sp_tblVisitAfterSurgeryStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        string studentId = hfStudentIDPKID.Value;

                        ClearForm();

                        lbl_error.Text = res.RetMessage;
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

        private bool ValidateInputStudent()
        {
            ClearValidation();

            if (txtStudentCode.Text.Trim() == "")
            {
                lbl_error.Text = "Student Code is required.";
                txtStudentCode.Focus();
                return false;
            }

            //if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "-1" || rdoDistanceVision_RightEye_Unaided.SelectedValue == "")
            //{
            //    lbl_error.Text = "Distance Vision Right Eye (Unaided) is required.";
            //    rdoDistanceVision_RightEye_Unaided.Focus();
            //    return false;
            //}

            //if (rdoDistanceVision_RightEye_WithGlasses.Enabled == true)
            //{
            //    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue == "-1" || rdoDistanceVision_RightEye_WithGlasses.SelectedValue == "")
            //    {
            //        lbl_error.Text = "Distance Vision Right Eye (With glasses) is required.";
            //        rdoDistanceVision_RightEye_WithGlasses.Focus();
            //        return false;
            //    }
            //}

            ////if (rdoDistanceVision_RightEye_PinHole.SelectedValue == "-1" || rdoDistanceVision_RightEye_PinHole.SelectedValue == "")
            ////{
            ////    lbl_error.Text = "Distance Vision Right Eye (Pin Hole) is required.";
            ////    rdoDistanceVision_RightEye_PinHole.Focus();
            ////    return false;
            ////}

            //if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "-1" || rdoDistanceVision_LeftEye_Unaided.SelectedValue == "")
            //{
            //    lbl_error.Text = "Distance Vision Left Eye (Unaided) is required.";
            //    rdoDistanceVision_LeftEye_Unaided.Focus();
            //    return false;
            //}

            //if (rdoDistanceVision_LeftEye_WithGlasses.Enabled == true)
            //{
            //    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue == "-1" || rdoDistanceVision_LeftEye_WithGlasses.SelectedValue == "")
            //    {
            //        lbl_error.Text = "Distance Vision Left Eye (With glasses) is required.";
            //        rdoDistanceVision_LeftEye_WithGlasses.Focus();
            //        return false;
            //    }
            //}

            if (hfAutoRefTestIDPKID.Value == "0")
            {
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
            }

            //if (txtSpherical_RightEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Spherical (RightEye) is required.";
            //    txtSpherical_RightEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_RightEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Cyclinderical (RightEye) is required.";
            //    txtCyclinderical_RightEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtAxixA_RightEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Axix (RightEye) is required.";
            //    txtAxixA_RightEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtSpherical_LeftEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Spherical (LeftEye) is required.";
            //    txtSpherical_LeftEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_LeftEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Cyclinderical (LeftEye) is required.";
            //    txtCyclinderical_LeftEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtAxixA_LeftEye_AutoRef.Text == "")
            //{
            //    lbl_error.Text = "Axix (LeftEye) is required.";
            //    txtAxixA_LeftEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtSpherical_RightEye_AutoRef.Text.Trim() == "")
            //{
            //    txtSpherical_RightEye_AutoRef.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Spherical Right Eye Points.";
            //    txtSpherical_RightEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_RightEye_AutoRef.Text.Trim() == "")
            //{
            //    txtCyclinderical_RightEye_AutoRef.Text = "0.00";
            //}

            //try
            //{
            //    decimal d = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
            //    txtCyclinderical_RightEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtAxixA_RightEye_AutoRef.Text.Trim() == "")
            //{
            //    txtAxixA_RightEye_AutoRef.Text = "0";
            //}

            ////if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            ////{
            ////    lbl_error.Text = "Invalid Axix.";
            ////    txtAxixA_RightEye.Focus();
            ////    return false;
            ////}

            //if (int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim()) > 180)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEye_AutoRef.Focus();
            //    return false;
            //}


            //if (txtSpherical_LeftEye_AutoRef.Text.Trim() == "")
            //{
            //    txtSpherical_LeftEye_AutoRef.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Spherical Left Eye Points.";
            //    txtSpherical_LeftEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_LeftEye_AutoRef.Text.Trim() == "")
            //{
            //    txtCyclinderical_LeftEye_AutoRef.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
            //    txtCyclinderical_LeftEye_AutoRef.Focus();
            //    return false;
            //}

            //if (txtAxixA_LeftEye_AutoRef.Text.Trim() == "")
            //{
            //    txtAxixA_LeftEye_AutoRef.Text = "0";
            //}

            ////if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            ////{
            ////    lbl_error.Text = "Invalid Axix.";
            ////    txtAxixA_LeftEye.Focus();
            ////    return false;
            ////}

            //if (int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim()) > 180)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEye_AutoRef.Focus();
            //    return false;
            //}


            //if (txtSpherical_RightEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Spherical (RightEye) is required.";
            //    txtSpherical_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_RightEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Cyclinderical (RightEye) is required.";
            //    txtCyclinderical_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtAxixA_RightEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Axix (RightEye) is required.";
            //    txtAxixA_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtNear_RightEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Near Add (RightEye) is required.";
            //    txtNear_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtSpherical_LeftEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Spherical (LeftEye) is required.";
            //    txtSpherical_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_LeftEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Cyclinderical (LeftEye) is required.";
            //    txtCyclinderical_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtAxixA_LeftEyeSR.Text == "")
            //{
            //    lbl_error.Text = "Axix (LeftEye) is required.";
            //    txtAxixA_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtNear_LeftEye.Text == "")
            //{
            //    lbl_error.Text = "Near Add (LeftEye) is required.";
            //    txtNear_LeftEye.Focus();
            //    return false;
            //}

            //if (txtSpherical_RightEyeSR.Text.Trim() == "")
            //{
            //    txtSpherical_RightEyeSR.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Spherical Right Eye Points.";
            //    txtSpherical_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_RightEyeSR.Text.Trim() == "")
            //{
            //    txtCyclinderical_RightEyeSR.Text = "0.00";
            //}

            //try
            //{
            //    decimal d = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
            //    txtCyclinderical_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtAxixA_RightEyeSR.Text.Trim() == "")
            //{
            //    txtAxixA_RightEyeSR.Text = "0";
            //}

            ////if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            ////{
            ////    lbl_error.Text = "Invalid Axix.";
            ////    txtAxixA_RightEye.Focus();
            ////    return false;
            ////}

            //if (int.Parse(txtAxixA_RightEyeSR.Text.Trim()) > 180)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtNear_RightEyeSR.Text.Trim() == "")
            //{
            //    txtNear_RightEyeSR.Text = "0.00";
            //}

            //try
            //{
            //    decimal d = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Near Right Eye Points.";
            //    txtNear_RightEyeSR.Focus();
            //    return false;
            //}

            //if (txtSpherical_LeftEyeSR.Text.Trim() == "")
            //{
            //    txtSpherical_LeftEyeSR.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Spherical Left Eye Points.";
            //    txtSpherical_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtCyclinderical_LeftEyeSR.Text.Trim() == "")
            //{
            //    txtCyclinderical_LeftEyeSR.Text = "0.00";
            //}
            //try
            //{
            //    decimal d = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
            //    txtCyclinderical_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtAxixA_LeftEyeSR.Text.Trim() == "")
            //{
            //    txtAxixA_LeftEyeSR.Text = "0";
            //}

            ////if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            ////{
            ////    lbl_error.Text = "Invalid Axix.";
            ////    txtAxixA_LeftEye.Focus();
            ////    return false;
            ////}

            //if (int.Parse(txtAxixA_LeftEyeSR.Text.Trim()) > 180)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEyeSR.Focus();
            //    return false;
            //}

            //if (txtNear_LeftEye.Text.Trim() == "")
            //{
            //    txtNear_LeftEye.Text = "0.00";
            //}

            //try
            //{
            //    decimal d = decimal.Parse(txtNear_LeftEye.Text.Trim());
            //}
            //catch (Exception ex)
            //{
            //    lbl_error.Text = "Invalid Near Left Eye Points.";
            //    txtNear_LeftEye.Focus();
            //    return false;
            //}

            return true;
        }

        private void ClearForm()
        {
            InitForm();

            hfAutoRefTestIDPKID.Value = "0";
            hfStudentIDPKID.Value = "0";
            hfSchoolIDPKID.Value = "0";

            txtTestDate.Text = Utilities.GetDate();

            txtStudentCode.Text = "";
            txtStudentName.Text = "";

            lblFatherName_Student.Text = "";
            lblAge_Student.Text = "";
            lblDecreasedVision_Student.Text = "";
            lblWearingGlasses_Student.Text = "";
            lblGender_Student.Text = "";
            lblClass_Student.Text = "";
            lblSchoolName_Student.Text = "";

            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";

            rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
            rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
            rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

            rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
            rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
            rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

            rdoNearVision_RightEye.SelectedValue = "0";
            rdoNearVision_LeftEye.SelectedValue = "0";

            lblSpherical_RightEye.Text = "";
            lblCylinderical_RightEye.Text = "";
            lblAxix_RightEye.Text = "";

            lblSpherical_LeftEye.Text = "";
            lblCylinderical_LeftEye.Text = "";
            lblAxix_LeftEye.Text = "";

            ddlSpherical_RightEye_AutoRef.SelectedIndex = 0;
            txtSpherical_RightEye_AutoRef.Text = "";

            ddlCyclinderical_RightEye_AutoRef.SelectedIndex = 0;
            txtCyclinderical_RightEye_AutoRef.Text = "";

            txtAxixA_RightEye_AutoRef.Text = "";
            txtAxixB_RightEye_AutoRef.Text = "";

            ddlSpherical_LeftEye_AutoRef.SelectedIndex = 0;
            txtSpherical_LeftEye_AutoRef.Text = "";

            ddlCyclinderical_LeftEye_AutoRef.SelectedIndex = 0;
            txtCyclinderical_LeftEye_AutoRef.Text = "";

            txtAxixA_LeftEye_AutoRef.Text = "";
            txtAxixB_LeftEye_AutoRef.Text = "";


            ddlSphericalRightEyeSR.SelectedIndex = 0;
            txtSpherical_RightEyeSR.Text = "";

            ddlCyclindericalRightEyeSR.SelectedIndex = 0;
            txtCyclinderical_RightEyeSR.Text = "";

            txtAxixA_RightEyeSR.Text = "";
            txtAxixB_RightEyeSR.Text = "";

            ddlNear_RightEyeSR.SelectedIndex = 0;
            txtNear_RightEyeSR.Text = "00.00";

            ddlSphericalLeftEyeSR.SelectedIndex = 0;
            txtSpherical_LeftEyeSR.Text = "";

            ddlCyclindericalLeftEyeSR.SelectedIndex = 0;
            txtCyclinderical_LeftEyeSR.Text = "";

            txtAxixA_LeftEyeSR.Text = "";
            txtAxixB_LeftEyeSR.Text = "";

            ddlNear_LeftEye.SelectedIndex = 0;
            txtNear_LeftEye.Text = "00.00";

            rdoPostOPTCondition.SelectedIndex = -1;
            rdoPostOPTCondition.SelectedValue = "-1";

            rdoSquintPostOptCond.SelectedIndex = -1;
            rdoSquintPostOptCond.SelectedValue = "-1";

            txtMedicines.Text = "";
            txtSurgeonRemarks.Text = "";

            txtNextVisitDate.Text = "";

            rdoOldNewTest.SelectedValue = "0";
            rdoOldNewTest_SelectedIndexChanged(null, null);

            ClearValidation();

            txtStudentCode.Focus();
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

            if (Convert.ToUInt32(autoRefTestIDPKID) > 0)
            {
                pnlTestSummary.Visible = false;

                var dt = dx.sp_tblVisitAfterSurgeryStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();
                if (dt != null)
                {
                    //hfAutoRefTestTransDate.Value = dt.VisitAfterSurgeryStudentTransDate.ToString(); //   AutoRefStudentTransDate.ToString();

                    //txtTestDate.Text = DateTime.Parse(hfAutoRefTestTransDate.Value).ToString("dd-MMM-yyyy");

                    if (dt.DistanceVision_RightEye_Unaided.ToString() == "-1")
                    {
                        rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_RightEye_Unaided.SelectedValue = dt.DistanceVision_RightEye_Unaided.ToString();
                    }

                    if (dt.DistanceVision_RightEye_WithGlasses.ToString() == "-1")
                    {
                        rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_RightEye_WithGlasses.SelectedValue = dt.DistanceVision_RightEye_WithGlasses.ToString();
                    }

                    if (dt.DistanceVision_RightEye_PinHole.ToString() == "-1")
                    {
                        rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_RightEye_PinHole.SelectedValue = dt.DistanceVision_RightEye_PinHole.ToString();
                    }

                    if (dt.NearVision_RightEye.ToString() == "-1")
                    {
                        rdoNearVision_RightEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoNearVision_RightEye.SelectedValue = dt.NearVision_RightEye.ToString();
                    }

                    if (dt.DistanceVision_LeftEye_Unaided.ToString() == "-1")
                    {
                        rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_LeftEye_Unaided.SelectedValue = dt.DistanceVision_LeftEye_Unaided.ToString();
                    }

                    if (dt.DistanceVision_LeftEye_WithGlasses.ToString() == "-1")
                    {
                        rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = dt.DistanceVision_LeftEye_WithGlasses.ToString();
                    }

                    if (dt.DistanceVision_LeftEye_PinHole.ToString() == "-1")
                    {
                        rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoDistanceVision_LeftEye_Pinhole.SelectedValue = dt.DistanceVision_LeftEye_PinHole.ToString();
                    }

                    if (dt.NearVision_LeftEye.ToString() == "-1")
                    {
                        rdoNearVision_LeftEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoNearVision_LeftEye.SelectedValue = dt.NearVision_LeftEye.ToString();
                    }


                    //if (dt.Right_Spherical_Status_AutoRef == "P")
                    //{
                    //    ddlSpherical_RightEye_AutoRef.SelectedIndex = 0;
                    //}
                    //else if (dt.Right_Spherical_Status_AutoRef == "N")
                    //{
                    //    ddlSpherical_RightEye_AutoRef.SelectedIndex = 1;
                    //}
                    //else if (dt.Right_Spherical_Status_AutoRef == "O")
                    //{
                    //    ddlSpherical_RightEye_AutoRef.SelectedIndex = 2;
                    //}
                    //else if (dt.Right_Spherical_Status_AutoRef == "E")
                    //{
                    //    ddlSpherical_RightEye_AutoRef.SelectedIndex = 3;
                    //}

                    //if (decimal.Parse(dt.Right_Spherical_Points_AutoRef.ToString()) < 0)
                    //{
                    //    txtSpherical_RightEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtSpherical_RightEye_AutoRef.Text = dt.Right_Spherical_Points_AutoRef.ToString();
                    //}

                    //if (dt.Right_Cyclinderical_Status_AutoRef == "P")
                    //{
                    //    ddlCyclinderical_RightEye_AutoRef.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlCyclinderical_RightEye_AutoRef.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Right_Cyclinderical_Points_AutoRef.ToString()) < 0)
                    //{
                    //    txtCyclinderical_RightEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtCyclinderical_RightEye_AutoRef.Text = dt.Right_Cyclinderical_Points_AutoRef.ToString();
                    //}

                    //if (int.Parse(dt.Right_Axix_From_AutoRef.ToString()) < 0)
                    //{
                    //    txtAxixA_RightEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtAxixA_RightEye_AutoRef.Text = dt.Right_Axix_From_AutoRef.ToString();
                    //}
                    ////txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    //txtAxixB_RightEye_AutoRef.Text = dt.Right_Axix_To_AutoRef.ToString();

                    //if (dt.Left_Spherical_Status_AutoRef == "P")
                    //{
                    //    ddlSpherical_LeftEye_AutoRef.SelectedIndex = 0;
                    //}
                    //else if (dt.Left_Spherical_Status_AutoRef == "N")
                    //{
                    //    ddlSpherical_LeftEye_AutoRef.SelectedIndex = 1;
                    //}
                    //else if (dt.Left_Spherical_Status_AutoRef == "O")
                    //{
                    //    ddlSpherical_LeftEye_AutoRef.SelectedIndex = 2;
                    //}
                    //else if (dt.Left_Spherical_Status_AutoRef == "E")
                    //{
                    //    ddlSpherical_LeftEye_AutoRef.SelectedIndex = 3;
                    //}
                    //if (decimal.Parse(dt.Left_Spherical_Points_AutoRef.ToString()) < 0)
                    //{
                    //    txtSpherical_LeftEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtSpherical_LeftEye_AutoRef.Text = dt.Left_Spherical_Points_AutoRef.ToString();
                    //}
                    ////txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    //if (dt.Left_Cyclinderical_Status_AutoRef == "P")
                    //{
                    //    ddlCyclinderical_LeftEye_AutoRef.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlCyclinderical_LeftEye_AutoRef.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Left_Cyclinderical_Points_AutoRef.ToString()) < 0)
                    //{
                    //    txtCyclinderical_LeftEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtCyclinderical_LeftEye_AutoRef.Text = dt.Left_Cyclinderical_Points_AutoRef.ToString();
                    //}
                    ////txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    //if (int.Parse(dt.Left_Axix_From_AutoRef.ToString()) < 0)
                    //{
                    //    txtAxixA_LeftEye_AutoRef.Text = "";
                    //}
                    //else
                    //{
                    //    txtAxixA_LeftEye_AutoRef.Text = dt.Left_Axix_From_AutoRef.ToString();
                    //}
                    ////txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    //txtAxixB_LeftEye_AutoRef.Text = dt.Left_Axix_To_AutoRef.ToString();

                    //if (dt.Right_Spherical_Status == "P")
                    //{
                    //    ddlSphericalRightEyeSR.SelectedIndex = 0;
                    //}
                    //else if (dt.Right_Spherical_Status == "N")
                    //{
                    //    ddlSphericalRightEyeSR.SelectedIndex = 1;
                    //}
                    //else if (dt.Right_Spherical_Status == "O")
                    //{
                    //    ddlSphericalRightEyeSR.SelectedIndex = 2;
                    //}
                    //else if (dt.Right_Spherical_Status == "E")
                    //{
                    //    ddlSphericalRightEyeSR.SelectedIndex = 3;
                    //}

                    //if (decimal.Parse(dt.Right_Spherical_Points.ToString()) < 0)
                    //{
                    //    txtSpherical_RightEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtSpherical_RightEyeSR.Text = dt.Right_Spherical_Points.ToString();
                    //}

                    //if (dt.Right_Cyclinderical_Status == "P")
                    //{
                    //    ddlCyclindericalRightEyeSR.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlCyclindericalRightEyeSR.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Right_Cyclinderical_Points.ToString()) < 0)
                    //{
                    //    txtCyclinderical_RightEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtCyclinderical_RightEyeSR.Text = dt.Right_Cyclinderical_Points.ToString();
                    //}

                    //if (int.Parse(dt.Right_Axix_From.ToString()) < 0)
                    //{
                    //    txtAxixA_RightEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtAxixA_RightEyeSR.Text = dt.Right_Axix_From.ToString();
                    //}
                    ////txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    //txtAxixB_RightEyeSR.Text = dt.Right_Axix_To.ToString();

                    //if (dt.Right_Near_Status == "P")
                    //{
                    //    ddlNear_RightEyeSR.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlNear_RightEyeSR.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Right_Near_Points.ToString()) < 0)
                    //{
                    //    txtNear_RightEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtNear_RightEyeSR.Text = dt.Right_Near_Points.ToString();
                    //}

                    //if (dt.Left_Spherical_Status == "P")
                    //{
                    //    ddlSphericalLeftEyeSR.SelectedIndex = 0;
                    //}
                    //else if (dt.Left_Spherical_Status == "N")
                    //{
                    //    ddlSphericalLeftEyeSR.SelectedIndex = 1;
                    //}
                    //else if (dt.Left_Spherical_Status == "O")
                    //{
                    //    ddlSphericalLeftEyeSR.SelectedIndex = 2;
                    //}
                    //else if (dt.Left_Spherical_Status == "E")
                    //{
                    //    ddlSphericalLeftEyeSR.SelectedIndex = 3;
                    //}
                    //if (decimal.Parse(dt.Left_Spherical_Points.ToString()) < 0)
                    //{
                    //    txtSpherical_LeftEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtSpherical_LeftEyeSR.Text = dt.Left_Spherical_Points.ToString();
                    //}
                    ////txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    //if (dt.Left_Cyclinderical_Status == "P")
                    //{
                    //    ddlCyclindericalLeftEyeSR.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlCyclindericalLeftEyeSR.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) < 0)
                    //{
                    //    txtCyclinderical_LeftEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtCyclinderical_LeftEyeSR.Text = dt.Left_Cyclinderical_Points.ToString();
                    //}
                    ////txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    //if (int.Parse(dt.Left_Axix_From.ToString()) < 0)
                    //{
                    //    txtAxixA_LeftEyeSR.Text = "";
                    //}
                    //else
                    //{
                    //    txtAxixA_LeftEyeSR.Text = dt.Left_Axix_From.ToString();
                    //}
                    ////txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    //txtAxixB_LeftEyeSR.Text = dt.Left_Axix_To.ToString();

                    //if (dt.Left_Near_Status == "P")
                    //{
                    //    ddlNear_LeftEye.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    ddlNear_LeftEye.SelectedIndex = 1;
                    //}

                    //if (decimal.Parse(dt.Left_Near_Points.ToString()) < 0)
                    //{
                    //    txtNear_LeftEye.Text = "";
                    //}
                    //else
                    //{
                    //    txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();
                    //}
                    ////txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();

                    if (dt.PostOPTCondition.ToString() == "-1")
                    {
                        rdoPostOPTCondition.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoPostOPTCondition.SelectedValue = dt.PostOPTCondition.ToString();
                    }

                    if (dt.SquintPostOPTCondition.ToString() == "-1")
                    {
                        rdoSquintPostOptCond.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoSquintPostOptCond.SelectedValue = dt.SquintPostOPTCondition.ToString();
                    }

                    txtMedicines.Text = dt.MedicinePrescribed.ToString();
                    txtSurgeonRemarks.Text = dt.Surgeon_Remarks.ToString();
                    if (dt.NextVisit.ToString() == "1")
                    {
                        rdoFollowupAfterSixMonths.Checked = false;
                        rdoNotRequiredFollowup.Checked = true;
                        txtNextVisitDate.Text = DateTime.Parse(dt.NextVisitDate.ToString()).ToString("dd-MMM-yyyy");
                        txtNextVisitDate.Visible = true;
                        //rdoNotRequiredFollowup_CheckedChanged(null, null);
                    }
                    else
                    {
                        rdoFollowupAfterSixMonths.Checked = true;
                        rdoNotRequiredFollowup.Checked = false;
                        txtNextVisitDate.Text = "";
                        txtNextVisitDate.Visible = false;
                        //rdoNotRequiredFollowup_CheckedChanged(null, null);
                    }
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        protected void btnLookupSchool_Click(object sender, EventArgs e)
        {
            try
            {
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Student_School_AfterSurgery(iType)
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_AfterSurgery(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
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

                    //var dtLastData = dx.sp_tblOptometristMasterStudent_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    //try
                    //{
                    //    if (dtLastData != null)
                    //    {
                    //        lblAutoRefCurrentDate.Text = DateTime.Parse(dtLastData.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                    //        //txtTestDate.Enabled = false;

                    //        string Right_Spherical_Points = dtLastData.Right_Spherical_Points.ToString();
                    //        if (dtLastData.Right_Spherical_Status == "Plano")
                    //        {
                    //            Right_Spherical_Points = "";
                    //        }
                    //        else if (dtLastData.Right_Spherical_Status == "Error")
                    //        {
                    //            Right_Spherical_Points = "";
                    //        }
                    //        lblSpherical_RightEye.Text = dtLastData.Right_Spherical_Status + Right_Spherical_Points;
                    //        lblCylinderical_RightEye.Text = dtLastData.Right_Cyclinderical_Status + dtLastData.Right_Cyclinderical_Points.ToString();
                    //        lblAxix_RightEye.Text = dtLastData.Right_Axix_From.ToString(); // + " to " + dtLastData.Right_Axix_To.ToString();

                    //        string Left_Spherical_Points = dtLastData.Left_Spherical_Points.ToString();
                    //        if (dtLastData.Left_Spherical_Status == "Plano")
                    //        {
                    //            Left_Spherical_Points = "";
                    //        }
                    //        else if (dtLastData.Left_Spherical_Status == "Error")
                    //        {
                    //            Left_Spherical_Points = "";
                    //        }
                    //        lblSpherical_LeftEye.Text = dtLastData.Left_Spherical_Status + Left_Spherical_Points;
                    //        lblCylinderical_LeftEye.Text = dtLastData.Left_Cyclinderical_Status + dtLastData.Left_Cyclinderical_Points.ToString();
                    //        lblAxix_LeftEye.Text = dtLastData.Left_Axix_From.ToString(); // + " to " + dtLastData.Left_Axix_To.ToString();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    string str = ex.Message + " - " + ex.Source;
                    //}

                    var dtLastSurgeryData = dx.sp_GetLastSurgeryDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtLastSurgeryData != null)
                        {
                            Label39.Text = dtLastSurgeryData.Hospital.ToString();
                            Label41.Text = dtLastSurgeryData.VisitforSurgeryStudentTransDate.ToString();
                            Label43.Text = dtLastSurgeryData.Surgery_RightEye.ToString() + ", " + dtLastSurgeryData.Surgery_LeftEye.ToString();
                            Label45.Text = dtLastSurgeryData.Surgery_Eye.ToString();
                            Label47.Text = dtLastSurgeryData.Doctor_Surgeon.ToString();
                            Label49.Text = dtLastSurgeryData.Remarks_Surgeon.ToString();
                            Label51.Text = dtLastSurgeryData.Doctor_Ophthalmologist.ToString();
                            Label53.Text = dtLastSurgeryData.Doctor_Orthoptist.ToString();
                            Label55.Text = dtLastSurgeryData.Doctor_Optometrist.ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }


                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        var dtVisitAfterSurgeryPreviousData = dx.sp_tblVisitAfterSurgeryStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtVisitAfterSurgeryPreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtVisitAfterSurgeryPreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitAfterSurgeryStudentId";
                                ddlPreviousTestResult.DataTextField = "VisitAfterSurgeryStudentTransDate";
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

        private void ShowConfirmAddMoreRecord()
        {
            string title = "Confirmation";
            string body = "Record Saved succussfully.<br/> Do you want to add more records?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "ShowPopupAfterSaveConfirmation('" + title + "', '" + body + "');", true);
        }

        protected void btnConfirmYes_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HospitalVisitForAfterSurgery.aspx");
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

            //InitForm();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HospitalVisitForAfterSurgery.aspx");
        }

        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string sStudentName = txtStudentName.Text;

            txtStudentName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sStudentName.ToLower());

            //var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
            //txtStudentCode.Text = studentCode;

            txtTestDate.Focus();
        }

        protected void rdoNotRequiredFollowup_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNotRequiredFollowup.Checked == false)
            {
                txtNextVisitDate.Visible = true;
                txtNextVisitDate.Focus();
            }
            else
            {
                txtNextVisitDate.Visible = false;
            }
        }

        protected void rdoFollowupAfterSixMonths_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoFollowupAfterSixMonths.Checked == true)
            {
                txtNextVisitDate.Visible = true;
                txtNextVisitDate.Focus();
            }
            else
            {
                txtNextVisitDate.Visible = false;
            }
        }
    }
}