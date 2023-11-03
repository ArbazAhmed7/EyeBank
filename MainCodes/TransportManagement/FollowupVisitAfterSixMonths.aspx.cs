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
    public partial class FollowupVisitAfterSixMonths : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "FollowupVisitAfterSixMonths"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                BindCombos();

                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

                rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

                rdoNotRequiredFollowup.Checked = false;
                rdoFollowupAfterSixMonths.Checked = false;
                rdoNextVisitDate.Checked = false;
                rdoFollowupAfterSixMonths_CheckedChanged(null, null);
                //txtRoutineCheckupDate.Visible = false;

                pnlTestSummary.Visible = false;

                txtTestDate.Text = Utilities.GetDate();
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
                    //int iDistanceVision_RightEye_Unaided = 0;
                    //iDistanceVision_RightEye_Unaided = int.Parse(rdoDistanceVision_RightEye_Unaided.SelectedValue);

                    int iDistanceVision_LeftEye_Unaided = -1;
                    if (rdoDistanceVision_LeftEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_Unaided = 0;
                    //iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);

                    int iDistanceVision_RightEye_WithGlasses = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }
                    //int iDistanceVision_RightEye_WithGlasses = 0;
                    //iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);

                    int iDistanceVision_LeftEye_WithGlasses = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_WithGlasses = 0;
                    //iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);


                    int iDistanceVision_RightEye_PinHole = -1;
                    if (rdoDistanceVision_RightEye_PinHole.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);
                    }
                    //int iDistanceVision_RightEye_PinHole = 0;
                    //iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);

                    int iDistanceVision_LeftEye_Pinhole = -1;
                    if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_Pinhole = 0;
                    //iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);

                    int iNearVision_RightEye = 0;
                    iNearVision_RightEye = int.Parse(rdoNearVision_RightEye.SelectedValue);
                    int iNearVision_LeftEye = 0;
                    iNearVision_LeftEye = int.Parse(rdoNearVision_LeftEye.SelectedValue);


                    string sSpherical_RightEyeType_AutoRef = string.Empty;
                    string sCyclinderical_RightEyeType_AutoRef = string.Empty;
                    string sSpherical_LeftEyeType_AutoRef = string.Empty;
                    string sCyclinderical_LeftEyeType_AutoRef = string.Empty;

                    if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType_AutoRef = "P";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType_AutoRef = "N";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType_AutoRef = "O";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType_AutoRef = "E";
                    }

                    if (ddlCyclinderical_RightEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_RightEyeType_AutoRef = "P"; }
                    else { sCyclinderical_RightEyeType_AutoRef = "N"; }

                    decimal dtxtSpherical_RightEye_AutoRef = 0;
                    if (!(sSpherical_RightEyeType_AutoRef == "O"))
                    {
                        dtxtSpherical_RightEye_AutoRef = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
                    }

                    decimal dCyclinderical_RightEye_AutoRef = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());

                    int dAxixA_RightEye_AutoRef = int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim());
                    int dAxixB_RightEye_AutoRef = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

                    if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType_AutoRef = "P";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType_AutoRef = "N";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType_AutoRef = "O";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType_AutoRef = "E";
                    }

                    if (ddlCyclinderical_LeftEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_LeftEyeType_AutoRef = "P"; }
                    else { sCyclinderical_LeftEyeType_AutoRef = "N"; }

                    decimal dtxtSpherical_LeftEye_AutoRef = 0;
                    if (!(sSpherical_LeftEyeType_AutoRef == "O"))
                    {
                        dtxtSpherical_LeftEye_AutoRef = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
                    }
                    decimal dCyclinderical_LeftEye_AutoRef = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());

                    int dAxixA_LeftEye_AutoRef = int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim());
                    int dAxixB_LeftEye_AutoRef = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                    string sSpherical_RightEyeType = string.Empty;
                    string sCyclinderical_RightEyeType = string.Empty;
                    string sNear_RightEyeType = string.Empty;

                    string sSpherical_LeftEyeType = string.Empty;
                    string sCyclinderical_LeftEyeType = string.Empty;
                    string sNear_LeftEyeType = string.Empty;

                    if (ddlSphericalRightEyeSR.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType = "P";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType = "N";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType = "O";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType = "E";
                    }

                    if (ddlCyclindericalRightEyeSR.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    else { sCyclinderical_RightEyeType = "N"; }

                    if (ddlNear_RightEyeSR.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    else { sNear_RightEyeType = "N"; }

                    decimal dtxtSpherical_RightEye = 0;
                    decimal dCyclinderical_RightEye = 0;
                    decimal dNear_RightEye = 0;
                    try
                    {
                        dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
                        dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
                        dNear_RightEye = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
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
                        dAxixA_RightEye = int.Parse(txtAxixA_RightEyeSR.Text.Trim());
                        dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_RightEye = 0;
                        dAxixB_RightEye = 0;
                    }

                    if (ddlSphericalLeftEyeSR.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType = "P";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType = "N";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType = "O";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType = "E";
                    }

                    if (ddlCyclindericalLeftEyeSR.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    else { sCyclinderical_LeftEyeType = "N"; }

                    if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    else { sNear_LeftEyeType = "N"; }

                    decimal dtxtSpherical_LeftEye = 0;
                    decimal dCyclinderical_LeftEye = 0;
                    decimal dNear_LeftEye = 0;

                    try
                    {
                        dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
                        dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
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
                        dAxixA_LeftEye = int.Parse(txtAxixA_LeftEyeSR.Text.Trim());
                        dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_LeftEye = 0;
                        dAxixB_LeftEye = 0;
                    }

                    int iRetinoScopy_RightEye = -1;
                    if (rdoRetinoScopy_RightEye.SelectedValue != "")
                    {
                        iRetinoScopy_RightEye = int.Parse(rdoRetinoScopy_RightEye.SelectedValue);
                    }

                    string strCycloplegicRefraction_RightEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_RightEye += rdoCycloplegicRefraction_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye.TrimEnd(',');

                    int iRetinoScopy_LeftEye = -1;
                    if (rdoRetinoScopy_LeftEye.SelectedValue != "")
                    {
                        iRetinoScopy_LeftEye = int.Parse(rdoRetinoScopy_LeftEye.SelectedValue);
                    }

                    string strCycloplegicRefraction_LeftEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_LeftEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_LeftEye += rdoCycloplegicRefraction_LeftEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye.TrimEnd(',');


                    //string strDiagnosis_RightEye = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis_RightEye += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis_RightEye = strDiagnosis_RightEye.TrimEnd(',');

                    int iNormal_RightEye = 0;
                    int iNormal_LeftEye = 0;
                    int iRefractiveError_RightEye = 0;
                    int iRefractiveError_LeftEye = 0;
                    int iLowVision_RightEye = 0;
                    int iLowVision_LeftEye = 0;
                    int iNeedsCycloplegicRefraction_RightEye = 0;
                    int iNeedsCycloplegicRefraction_LeftEye = 0;
                    int iSquintStrabismus_RightEye = 0;
                    int iSquintStrabismus_LeftEye = 0;
                    int iLazyEyeAmblyopia_RightEye = 0;
                    int iLazyEyeAmblyopia_LeftEye = 0;
                    int iColorblindnessAchromatopsia_RightEye = 0;
                    int iColorblindnessAchromatopsia_LeftEye = 0;
                    int iCataract_RightEye = 0;
                    int iCataract_LeftEye = 0;
                    int iTraumaticCataract_RightEye = 0;
                    int iTraumaticCataract_LeftEye = 0;
                    int iKeratoconus_RightEye = 0;
                    int iKeratoconus_LeftEye = 0;
                    int iAnisometropia_RightEye = 0;
                    int iAnisometropia_LeftEye = 0;
                    int iPtosis_RightEye = 0;
                    int iPtosis_LeftEye = 0;
                    int iNystagmus_RightEye = 0;
                    int iNystagmus_LeftEye = 0;
                    int iCorneadefects_RightEye = 0;
                    int iCorneadefects_LeftEye = 0;
                    int iPuplidefects_RightEye = 0;
                    int iPuplidefects_LeftEye = 0;
                    int iPterygium_RightEye = 0;
                    int iPterygium_LeftEye = 0;
                    int iOther_RightEye = 0;
                    int iOther_LeftEye = 0;



                    if (chkNormal_RightEye.Checked == true) { iNormal_RightEye = 1; }
                    if (chkNormal_LeftEye.Checked == true) { iNormal_LeftEye = 1; }
                    if (chkRefractiveError_RightEye.Checked == true) { iRefractiveError_RightEye = 1; }
                    if (chkRefractiveError_LeftEye.Checked == true) { iRefractiveError_LeftEye = 1; }
                    if (chkLowVision_RightEye.Checked == true) { iLowVision_RightEye = 1; }
                    if (chkLowVision_LeftEye.Checked == true) { iLowVision_LeftEye = 1; }
                    if (chkNeedsCycloplegicRefraction_RightEye.Checked == true) { iNeedsCycloplegicRefraction_RightEye = 1; }
                    if (chkNeedsCycloplegicRefraction_LeftEye.Checked == true) { iNeedsCycloplegicRefraction_LeftEye = 1; }
                    if (chkSquintStrabismus_RightEye.Checked == true) { iSquintStrabismus_RightEye = 1; }
                    if (chkSquintStrabismus_LeftEye.Checked == true) { iSquintStrabismus_LeftEye = 1; }
                    if (chkLazyEyeAmblyopia_RightEye.Checked == true) { iLazyEyeAmblyopia_RightEye = 1; }
                    if (chkLazyEyeAmblyopia_LeftEye.Checked == true) { iLazyEyeAmblyopia_LeftEye = 1; }
                    if (chkColorblindnessAchromatopsia_RightEye.Checked == true) { iColorblindnessAchromatopsia_RightEye = 1; }
                    if (chkColorblindnessAchromatopsia_LeftEye.Checked == true) { iColorblindnessAchromatopsia_LeftEye = 1; }
                    if (chkCataract_RightEye.Checked == true) { iCataract_RightEye = 1; }
                    if (chkCataract_LeftEye.Checked == true) { iCataract_LeftEye = 1; }
                    if (chkTraumaticCataract_RightEye.Checked == true) { iTraumaticCataract_RightEye = 1; }
                    if (chkTraumaticCataract_LeftEye.Checked == true) { iTraumaticCataract_LeftEye = 1; }
                    if (chkKeratoconus_RightEye.Checked == true) { iKeratoconus_RightEye = 1; }
                    if (chkKeratoconus_LeftEye.Checked == true) { iKeratoconus_LeftEye = 1; }
                    if (chkAnisometropia_RightEye.Checked == true) { iAnisometropia_RightEye = 1; }
                    if (chkAnisometropia_LeftEye.Checked == true) { iAnisometropia_LeftEye = 1; }
                    if (chkPtosis_RightEye.Checked == true) { iPtosis_RightEye = 1; }
                    if (chkPtosis_LeftEye.Checked == true) { iPtosis_LeftEye = 1; }
                    if (chkNystagmus_RightEye.Checked == true) { iNystagmus_RightEye = 1; }
                    if (chkNystagmus_LeftEye.Checked == true) { iNystagmus_LeftEye = 1; }
                    if (chkCorneadefects_RightEye.Checked == true) { iCorneadefects_RightEye = 1; }
                    if (chkCorneadefects_LeftEye.Checked == true) { iCorneadefects_LeftEye = 1; }
                    if (chkPuplidefects_RightEye.Checked == true) { iPuplidefects_RightEye = 1; }
                    if (chkPuplidefects_LeftEye.Checked == true) { iPuplidefects_LeftEye = 1; }
                    if (chkPterygium_RightEye.Checked == true) { iPterygium_RightEye = 1; }
                    if (chkPterygium_LeftEye.Checked == true) { iPterygium_LeftEye = 1; }
                    if (chkOther_RightEye.Checked == true) { iOther_RightEye = 1; }
                    if (chkOther_LeftEye.Checked == true) { iOther_LeftEye = 1; }


                    string strRefractiveErrorOpt = string.Empty;

                    for (int i = 0; i < chkListRefractiveErrorOpt.Items.Count; i++)
                    {
                        if (chkListRefractiveErrorOpt.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strRefractiveErrorOpt += chkListRefractiveErrorOpt.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strRefractiveErrorOpt = strRefractiveErrorOpt.TrimEnd(',');

                    string strRefractiveErrorOpt_LeftEye = string.Empty;

                    for (int i = 0; i < chkListRefractiveErrorOpt_LeftEye.Items.Count; i++)
                    {
                        if (chkListRefractiveErrorOpt_LeftEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strRefractiveErrorOpt_LeftEye += chkListRefractiveErrorOpt_LeftEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strRefractiveErrorOpt_LeftEye = strRefractiveErrorOpt_LeftEye.TrimEnd(',');

                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }
                    //int iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);

                    string strMedicine = string.Empty;

                    for (int i = 0; i < chkMedicine.Items.Count; i++)
                    {
                        if (chkMedicine.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strMedicine += chkMedicine.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strMedicine = strMedicine.TrimEnd(',');


                    int iNextVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iNextVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iNextVisit = 1;
                    }
                    if (rdoNextVisitDate.Checked == true)
                    {
                        iNextVisit = 2;
                    }
                    DateTime dtNextVisitDate = DateTime.Parse("1900-01-01");
                    if (iNextVisit == 2)
                    {
                        if (txtRoutineCheckupDate.Text != "")
                        {
                            dtNextVisitDate = DateTime.Parse(txtRoutineCheckupDate.Text);
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




                    var res = dx.sp_tblVisitSixMonthStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye,
                        sSpherical_RightEyeType_AutoRef, dtxtSpherical_RightEye_AutoRef, sCyclinderical_RightEyeType_AutoRef, dCyclinderical_RightEye_AutoRef, dAxixA_RightEye_AutoRef, dAxixB_RightEye_AutoRef,
                        sSpherical_LeftEyeType_AutoRef, dtxtSpherical_LeftEye_AutoRef, sCyclinderical_LeftEyeType_AutoRef, dCyclinderical_LeftEye_AutoRef, dAxixA_LeftEye_AutoRef, dAxixB_LeftEye_AutoRef,
                        iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,

                        sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,
                        //strDiagnosis_RightEye, txtDiagnosis_RightEye.Text,
                        iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                        strRefractiveErrorOpt, strRefractiveErrorOpt_LeftEye, iTreatment_Glasses, 0, strMedicine, iNextVisit, dtNextVisitDate,
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
                    //int iDistanceVision_RightEye_Unaided = 0;
                    //iDistanceVision_RightEye_Unaided = int.Parse(rdoDistanceVision_RightEye_Unaided.SelectedValue);

                    int iDistanceVision_LeftEye_Unaided = -1;
                    if (rdoDistanceVision_LeftEye_Unaided.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_Unaided = 0;
                    //iDistanceVision_LeftEye_Unaided = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue);

                    int iDistanceVision_RightEye_WithGlasses = -1;
                    if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);
                    }
                    //int iDistanceVision_RightEye_WithGlasses = 0;
                    //iDistanceVision_RightEye_WithGlasses = int.Parse(rdoDistanceVision_RightEye_WithGlasses.SelectedValue);

                    int iDistanceVision_LeftEye_WithGlasses = -1;
                    if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_WithGlasses = 0;
                    //iDistanceVision_LeftEye_WithGlasses = int.Parse(rdoDistanceVision_LeftEye_WithGlasses.SelectedValue);


                    int iDistanceVision_RightEye_PinHole = -1;
                    if (rdoDistanceVision_RightEye_PinHole.SelectedValue != "")
                    {
                        iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);
                    }
                    //int iDistanceVision_RightEye_PinHole = 0;
                    //iDistanceVision_RightEye_PinHole = int.Parse(rdoDistanceVision_RightEye_PinHole.SelectedValue);

                    int iDistanceVision_LeftEye_Pinhole = -1;
                    if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue != "")
                    {
                        iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);
                    }
                    //int iDistanceVision_LeftEye_Pinhole = 0;
                    //iDistanceVision_LeftEye_Pinhole = int.Parse(rdoDistanceVision_LeftEye_Pinhole.SelectedValue);

                    int iNearVision_RightEye = 0;
                    iNearVision_RightEye = int.Parse(rdoNearVision_RightEye.SelectedValue);
                    int iNearVision_LeftEye = 0;
                    iNearVision_LeftEye = int.Parse(rdoNearVision_LeftEye.SelectedValue);


                    string sSpherical_RightEyeType_AutoRef = string.Empty;
                    string sCyclinderical_RightEyeType_AutoRef = string.Empty;
                    string sSpherical_LeftEyeType_AutoRef = string.Empty;
                    string sCyclinderical_LeftEyeType_AutoRef = string.Empty;

                    if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType_AutoRef = "P";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType_AutoRef = "N";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType_AutoRef = "O";
                    }
                    else if (ddlSpherical_RightEye_AutoRef.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType_AutoRef = "E";
                    }

                    if (ddlCyclinderical_RightEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_RightEyeType_AutoRef = "P"; }
                    else { sCyclinderical_RightEyeType_AutoRef = "N"; }

                    decimal dtxtSpherical_RightEye_AutoRef = 0;
                    if (!(sSpherical_RightEyeType_AutoRef == "O"))
                    {
                        dtxtSpherical_RightEye_AutoRef = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
                    }

                    decimal dCyclinderical_RightEye_AutoRef = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());

                    int dAxixA_RightEye_AutoRef = int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim());
                    int dAxixB_RightEye_AutoRef = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());

                    if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType_AutoRef = "P";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType_AutoRef = "N";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType_AutoRef = "O";
                    }
                    else if (ddlSpherical_LeftEye_AutoRef.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType_AutoRef = "E";
                    }

                    if (ddlCyclinderical_LeftEye_AutoRef.SelectedValue == "Positive") { sCyclinderical_LeftEyeType_AutoRef = "P"; }
                    else { sCyclinderical_LeftEyeType_AutoRef = "N"; }

                    decimal dtxtSpherical_LeftEye_AutoRef = 0;
                    if (!(sSpherical_LeftEyeType_AutoRef == "O"))
                    {
                        dtxtSpherical_LeftEye_AutoRef = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
                    }
                    decimal dCyclinderical_LeftEye_AutoRef = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());

                    int dAxixA_LeftEye_AutoRef = int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim());
                    int dAxixB_LeftEye_AutoRef = 0; // int.Parse(txtAxixB_LeftEye.Text.Trim());

                    string sSpherical_RightEyeType = string.Empty;
                    string sCyclinderical_RightEyeType = string.Empty;
                    string sNear_RightEyeType = string.Empty;

                    string sSpherical_LeftEyeType = string.Empty;
                    string sCyclinderical_LeftEyeType = string.Empty;
                    string sNear_LeftEyeType = string.Empty;

                    if (ddlSphericalRightEyeSR.SelectedValue == "Positive")
                    {
                        sSpherical_RightEyeType = "P";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Negative")
                    {
                        sSpherical_RightEyeType = "N";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Plano")
                    {
                        sSpherical_RightEyeType = "O";
                    }
                    else if (ddlSphericalRightEyeSR.SelectedValue == "Error")
                    {
                        sSpherical_RightEyeType = "E";
                    }

                    if (ddlCyclindericalRightEyeSR.SelectedValue == "Positive") { sCyclinderical_RightEyeType = "P"; }
                    else { sCyclinderical_RightEyeType = "N"; }

                    if (ddlNear_RightEyeSR.SelectedValue == "Positive") { sNear_RightEyeType = "P"; }
                    else { sNear_RightEyeType = "N"; }

                    decimal dtxtSpherical_RightEye = 0;
                    decimal dCyclinderical_RightEye = 0;
                    decimal dNear_RightEye = 0;
                    try
                    {
                        dtxtSpherical_RightEye = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
                        dCyclinderical_RightEye = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
                        dNear_RightEye = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
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
                        dAxixA_RightEye = int.Parse(txtAxixA_RightEyeSR.Text.Trim());
                        dAxixB_RightEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_RightEye = 0;
                        dAxixB_RightEye = 0;
                    }

                    if (ddlSphericalLeftEyeSR.SelectedValue == "Positive")
                    {
                        sSpherical_LeftEyeType = "P";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Negative")
                    {
                        sSpherical_LeftEyeType = "N";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Plano")
                    {
                        sSpherical_LeftEyeType = "O";
                    }
                    else if (ddlSphericalLeftEyeSR.SelectedValue == "Error")
                    {
                        sSpherical_LeftEyeType = "E";
                    }

                    if (ddlCyclindericalLeftEyeSR.SelectedValue == "Positive") { sCyclinderical_LeftEyeType = "P"; }
                    else { sCyclinderical_LeftEyeType = "N"; }

                    if (ddlNear_LeftEye.SelectedValue == "Positive") { sNear_LeftEyeType = "P"; }
                    else { sNear_LeftEyeType = "N"; }

                    decimal dtxtSpherical_LeftEye = 0;
                    decimal dCyclinderical_LeftEye = 0;
                    decimal dNear_LeftEye = 0;

                    try
                    {
                        dtxtSpherical_LeftEye = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
                        dCyclinderical_LeftEye = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
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
                        dAxixA_LeftEye = int.Parse(txtAxixA_LeftEyeSR.Text.Trim());
                        dAxixB_LeftEye = 0; // int.Parse(txtAxixB_RightEye.Text.Trim());
                    }
                    catch
                    {
                        dAxixA_LeftEye = 0;
                        dAxixB_LeftEye = 0;
                    }

                    int iRetinoScopy_RightEye = -1;
                    if (rdoRetinoScopy_RightEye.SelectedValue != "")
                    {
                        iRetinoScopy_RightEye = int.Parse(rdoRetinoScopy_RightEye.SelectedValue);
                    }

                    string strCycloplegicRefraction_RightEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_RightEye += rdoCycloplegicRefraction_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye.TrimEnd(',');

                    int iRetinoScopy_LeftEye = -1;
                    if (rdoRetinoScopy_LeftEye.SelectedValue != "")
                    {
                        iRetinoScopy_LeftEye = int.Parse(rdoRetinoScopy_LeftEye.SelectedValue);
                    }

                    string strCycloplegicRefraction_LeftEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_LeftEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_LeftEye += rdoCycloplegicRefraction_LeftEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye.TrimEnd(',');



                    //string strDiagnosis_RightEye = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis_RightEye += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis_RightEye = strDiagnosis_RightEye.TrimEnd(',');

                    int iNormal_RightEye = 0;
                    int iNormal_LeftEye = 0;
                    int iRefractiveError_RightEye = 0;
                    int iRefractiveError_LeftEye = 0;
                    int iLowVision_RightEye = 0;
                    int iLowVision_LeftEye = 0;
                    int iNeedsCycloplegicRefraction_RightEye = 0;
                    int iNeedsCycloplegicRefraction_LeftEye = 0;
                    int iSquintStrabismus_RightEye = 0;
                    int iSquintStrabismus_LeftEye = 0;
                    int iLazyEyeAmblyopia_RightEye = 0;
                    int iLazyEyeAmblyopia_LeftEye = 0;
                    int iColorblindnessAchromatopsia_RightEye = 0;
                    int iColorblindnessAchromatopsia_LeftEye = 0;
                    int iCataract_RightEye = 0;
                    int iCataract_LeftEye = 0;
                    int iTraumaticCataract_RightEye = 0;
                    int iTraumaticCataract_LeftEye = 0;
                    int iKeratoconus_RightEye = 0;
                    int iKeratoconus_LeftEye = 0;
                    int iAnisometropia_RightEye = 0;
                    int iAnisometropia_LeftEye = 0;
                    int iPtosis_RightEye = 0;
                    int iPtosis_LeftEye = 0;
                    int iNystagmus_RightEye = 0;
                    int iNystagmus_LeftEye = 0;
                    int iCorneadefects_RightEye = 0;
                    int iCorneadefects_LeftEye = 0;
                    int iPuplidefects_RightEye = 0;
                    int iPuplidefects_LeftEye = 0;
                    int iPterygium_RightEye = 0;
                    int iPterygium_LeftEye = 0;
                    int iOther_RightEye = 0;
                    int iOther_LeftEye = 0;



                    if (chkNormal_RightEye.Checked == true) { iNormal_RightEye = 1; }
                    if (chkNormal_LeftEye.Checked == true) { iNormal_LeftEye = 1; }
                    if (chkRefractiveError_RightEye.Checked == true) { iRefractiveError_RightEye = 1; }
                    if (chkRefractiveError_LeftEye.Checked == true) { iRefractiveError_LeftEye = 1; }
                    if (chkLowVision_RightEye.Checked == true) { iLowVision_RightEye = 1; }
                    if (chkLowVision_LeftEye.Checked == true) { iLowVision_LeftEye = 1; }
                    if (chkNeedsCycloplegicRefraction_RightEye.Checked == true) { iNeedsCycloplegicRefraction_RightEye = 1; }
                    if (chkNeedsCycloplegicRefraction_LeftEye.Checked == true) { iNeedsCycloplegicRefraction_LeftEye = 1; }
                    if (chkSquintStrabismus_RightEye.Checked == true) { iSquintStrabismus_RightEye = 1; }
                    if (chkSquintStrabismus_LeftEye.Checked == true) { iSquintStrabismus_LeftEye = 1; }
                    if (chkLazyEyeAmblyopia_RightEye.Checked == true) { iLazyEyeAmblyopia_RightEye = 1; }
                    if (chkLazyEyeAmblyopia_LeftEye.Checked == true) { iLazyEyeAmblyopia_LeftEye = 1; }
                    if (chkColorblindnessAchromatopsia_RightEye.Checked == true) { iColorblindnessAchromatopsia_RightEye = 1; }
                    if (chkColorblindnessAchromatopsia_LeftEye.Checked == true) { iColorblindnessAchromatopsia_LeftEye = 1; }
                    if (chkCataract_RightEye.Checked == true) { iCataract_RightEye = 1; }
                    if (chkCataract_LeftEye.Checked == true) { iCataract_LeftEye = 1; }
                    if (chkTraumaticCataract_RightEye.Checked == true) { iTraumaticCataract_RightEye = 1; }
                    if (chkTraumaticCataract_LeftEye.Checked == true) { iTraumaticCataract_LeftEye = 1; }
                    if (chkKeratoconus_RightEye.Checked == true) { iKeratoconus_RightEye = 1; }
                    if (chkKeratoconus_LeftEye.Checked == true) { iKeratoconus_LeftEye = 1; }
                    if (chkAnisometropia_RightEye.Checked == true) { iAnisometropia_RightEye = 1; }
                    if (chkAnisometropia_LeftEye.Checked == true) { iAnisometropia_LeftEye = 1; }
                    if (chkPtosis_RightEye.Checked == true) { iPtosis_RightEye = 1; }
                    if (chkPtosis_LeftEye.Checked == true) { iPtosis_LeftEye = 1; }
                    if (chkNystagmus_RightEye.Checked == true) { iNystagmus_RightEye = 1; }
                    if (chkNystagmus_LeftEye.Checked == true) { iNystagmus_LeftEye = 1; }
                    if (chkCorneadefects_RightEye.Checked == true) { iCorneadefects_RightEye = 1; }
                    if (chkCorneadefects_LeftEye.Checked == true) { iCorneadefects_LeftEye = 1; }
                    if (chkPuplidefects_RightEye.Checked == true) { iPuplidefects_RightEye = 1; }
                    if (chkPuplidefects_LeftEye.Checked == true) { iPuplidefects_LeftEye = 1; }
                    if (chkPterygium_RightEye.Checked == true) { iPterygium_RightEye = 1; }
                    if (chkPterygium_LeftEye.Checked == true) { iPterygium_LeftEye = 1; }
                    if (chkOther_RightEye.Checked == true) { iOther_RightEye = 1; }
                    if (chkOther_LeftEye.Checked == true) { iOther_LeftEye = 1; }

                    string strRefractiveErrorOpt = string.Empty;

                    for (int i = 0; i < chkListRefractiveErrorOpt.Items.Count; i++)
                    {
                        if (chkListRefractiveErrorOpt.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strRefractiveErrorOpt += chkListRefractiveErrorOpt.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strRefractiveErrorOpt = strRefractiveErrorOpt.TrimEnd(',');

                    string strRefractiveErrorOpt_LeftEye = string.Empty;

                    for (int i = 0; i < chkListRefractiveErrorOpt_LeftEye.Items.Count; i++)
                    {
                        if (chkListRefractiveErrorOpt_LeftEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strRefractiveErrorOpt_LeftEye += chkListRefractiveErrorOpt_LeftEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strRefractiveErrorOpt_LeftEye = strRefractiveErrorOpt_LeftEye.TrimEnd(',');

                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }
                    //int iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);

                    string strMedicine = string.Empty;

                    for (int i = 0; i < chkMedicine.Items.Count; i++)
                    {
                        if (chkMedicine.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strMedicine += chkMedicine.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strMedicine = strMedicine.TrimEnd(',');


                    int iNextVisit = -1;
                    if (rdoNotRequiredFollowup.Checked == true)
                    {
                        iNextVisit = 0;
                    }
                    if (rdoFollowupAfterSixMonths.Checked == true)
                    {
                        iNextVisit = 1;
                    }
                    if (rdoNextVisitDate.Checked == true)
                    {
                        iNextVisit = 2;
                    }
                    DateTime dtNextVisitDate = DateTime.Parse("1900-01-01");
                    if (iNextVisit == 2)
                    {
                        if (txtRoutineCheckupDate.Text != "")
                        {
                            dtNextVisitDate = DateTime.Parse(txtRoutineCheckupDate.Text);
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

                    var res = dx.sp_tblVisitSixMonthStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye,
                        sSpherical_RightEyeType_AutoRef, dtxtSpherical_RightEye_AutoRef, sCyclinderical_RightEyeType_AutoRef, dCyclinderical_RightEye_AutoRef, dAxixA_RightEye_AutoRef, dAxixB_RightEye_AutoRef,
                        sSpherical_LeftEyeType_AutoRef, dtxtSpherical_LeftEye_AutoRef, sCyclinderical_LeftEyeType_AutoRef, dCyclinderical_LeftEye_AutoRef, dAxixA_LeftEye_AutoRef, dAxixB_LeftEye_AutoRef,
                        iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,

                        sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye,

                        //strDiagnosis_RightEye, txtDiagnosis_RightEye.Text,
                        iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                        strRefractiveErrorOpt, strRefractiveErrorOpt_LeftEye, iTreatment_Glasses, 0, strMedicine, iNextVisit, dtNextVisitDate,
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

                    var res = dx.sp_tblVisitSixMonthStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "-1" || rdoDistanceVision_RightEye_Unaided.SelectedValue == "")
            {
                lbl_error.Text = "Distance Vision Right Eye (Unaided) is required.";
                rdoDistanceVision_RightEye_Unaided.Focus();
                return false;
            }

            if (rdoDistanceVision_RightEye_WithGlasses.Enabled == true)
            {
                if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue == "-1" || rdoDistanceVision_RightEye_WithGlasses.SelectedValue == "")
                {
                    lbl_error.Text = "Distance Vision Right Eye (With glasses) is required.";
                    rdoDistanceVision_RightEye_WithGlasses.Focus();
                    return false;
                }
            }

            //if (rdoDistanceVision_RightEye_PinHole.SelectedValue == "-1" || rdoDistanceVision_RightEye_PinHole.SelectedValue == "")
            //{
            //    lbl_error.Text = "Distance Vision Right Eye (Pin Hole) is required.";
            //    rdoDistanceVision_RightEye_PinHole.Focus();
            //    return false;
            //}

            if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "-1" || rdoDistanceVision_LeftEye_Unaided.SelectedValue == "")
            {
                lbl_error.Text = "Distance Vision Left Eye (Unaided) is required.";
                rdoDistanceVision_LeftEye_Unaided.Focus();
                return false;
            }

            if (rdoDistanceVision_LeftEye_WithGlasses.Enabled == true)
            {
                if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue == "-1" || rdoDistanceVision_LeftEye_WithGlasses.SelectedValue == "")
                {
                    lbl_error.Text = "Distance Vision Left Eye (With glasses) is required.";
                    rdoDistanceVision_LeftEye_WithGlasses.Focus();
                    return false;
                }
            }

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

            if (txtSpherical_RightEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Spherical (RightEye) is required.";
                txtSpherical_RightEye_AutoRef.Focus();
                return false;
            }

            if (txtCyclinderical_RightEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Cyclinderical (RightEye) is required.";
                txtCyclinderical_RightEye_AutoRef.Focus();
                return false;
            }

            if (txtAxixA_RightEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Axix (RightEye) is required.";
                txtAxixA_RightEye_AutoRef.Focus();
                return false;
            }

            if (txtSpherical_LeftEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Spherical (LeftEye) is required.";
                txtSpherical_LeftEye_AutoRef.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Cyclinderical (LeftEye) is required.";
                txtCyclinderical_LeftEye_AutoRef.Focus();
                return false;
            }

            if (txtAxixA_LeftEye_AutoRef.Text == "")
            {
                lbl_error.Text = "Axix (LeftEye) is required.";
                txtAxixA_LeftEye_AutoRef.Focus();
                return false;
            }

            if (txtSpherical_RightEye_AutoRef.Text.Trim() == "")
            {
                txtSpherical_RightEye_AutoRef.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtSpherical_RightEye_AutoRef.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Right Eye Points.";
                txtSpherical_RightEye_AutoRef.Focus();
                return false;
            }

            if (txtCyclinderical_RightEye_AutoRef.Text.Trim() == "")
            {
                txtCyclinderical_RightEye_AutoRef.Text = "0.00";
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_RightEye_AutoRef.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
                txtCyclinderical_RightEye_AutoRef.Focus();
                return false;
            }

            if (txtAxixA_RightEye_AutoRef.Text.Trim() == "")
            {
                txtAxixA_RightEye_AutoRef.Text = "0";
            }

            //if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_RightEye_AutoRef.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_RightEye_AutoRef.Focus();
                return false;
            }


            if (txtSpherical_LeftEye_AutoRef.Text.Trim() == "")
            {
                txtSpherical_LeftEye_AutoRef.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtSpherical_LeftEye_AutoRef.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Left Eye Points.";
                txtSpherical_LeftEye_AutoRef.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEye_AutoRef.Text.Trim() == "")
            {
                txtCyclinderical_LeftEye_AutoRef.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtCyclinderical_LeftEye_AutoRef.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
                txtCyclinderical_LeftEye_AutoRef.Focus();
                return false;
            }

            if (txtAxixA_LeftEye_AutoRef.Text.Trim() == "")
            {
                txtAxixA_LeftEye_AutoRef.Text = "0";
            }

            //if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_LeftEye_AutoRef.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_LeftEye_AutoRef.Focus();
                return false;
            }


            if (txtSpherical_RightEyeSR.Text == "")
            {
                lbl_error.Text = "Spherical (RightEye) is required.";
                txtSpherical_RightEyeSR.Focus();
                return false;
            }

            if (txtCyclinderical_RightEyeSR.Text == "")
            {
                lbl_error.Text = "Cyclinderical (RightEye) is required.";
                txtCyclinderical_RightEyeSR.Focus();
                return false;
            }

            if (txtAxixA_RightEyeSR.Text == "")
            {
                lbl_error.Text = "Axix (RightEye) is required.";
                txtAxixA_RightEyeSR.Focus();
                return false;
            }

            if (txtNear_RightEyeSR.Text == "")
            {
                lbl_error.Text = "Near Add (RightEye) is required.";
                txtNear_RightEyeSR.Focus();
                return false;
            }

            if (txtSpherical_LeftEyeSR.Text == "")
            {
                lbl_error.Text = "Spherical (LeftEye) is required.";
                txtSpherical_LeftEyeSR.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEyeSR.Text == "")
            {
                lbl_error.Text = "Cyclinderical (LeftEye) is required.";
                txtCyclinderical_LeftEyeSR.Focus();
                return false;
            }

            if (txtAxixA_LeftEyeSR.Text == "")
            {
                lbl_error.Text = "Axix (LeftEye) is required.";
                txtAxixA_LeftEyeSR.Focus();
                return false;
            }

            if (txtNear_LeftEye.Text == "")
            {
                lbl_error.Text = "Near Add (LeftEye) is required.";
                txtNear_LeftEye.Focus();
                return false;
            }

            if (txtSpherical_RightEyeSR.Text.Trim() == "")
            {
                txtSpherical_RightEyeSR.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtSpherical_RightEyeSR.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Right Eye Points.";
                txtSpherical_RightEyeSR.Focus();
                return false;
            }

            if (txtCyclinderical_RightEyeSR.Text.Trim() == "")
            {
                txtCyclinderical_RightEyeSR.Text = "0.00";
            }

            try
            {
                decimal d = decimal.Parse(txtCyclinderical_RightEyeSR.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Right Eye Points.";
                txtCyclinderical_RightEyeSR.Focus();
                return false;
            }

            if (txtAxixA_RightEyeSR.Text.Trim() == "")
            {
                txtAxixA_RightEyeSR.Text = "0";
            }

            //if (int.Parse(txtAxixA_RightEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_RightEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_RightEyeSR.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_RightEyeSR.Focus();
                return false;
            }

            if (txtNear_RightEyeSR.Text.Trim() == "")
            {
                txtNear_RightEyeSR.Text = "0.00";
            }

            try
            {
                decimal d = decimal.Parse(txtNear_RightEyeSR.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Near Right Eye Points.";
                txtNear_RightEyeSR.Focus();
                return false;
            }

            if (txtSpherical_LeftEyeSR.Text.Trim() == "")
            {
                txtSpherical_LeftEyeSR.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtSpherical_LeftEyeSR.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Spherical Left Eye Points.";
                txtSpherical_LeftEyeSR.Focus();
                return false;
            }

            if (txtCyclinderical_LeftEyeSR.Text.Trim() == "")
            {
                txtCyclinderical_LeftEyeSR.Text = "0.00";
            }
            try
            {
                decimal d = decimal.Parse(txtCyclinderical_LeftEyeSR.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Cyclinderical Left Eye Points.";
                txtCyclinderical_LeftEyeSR.Focus();
                return false;
            }

            if (txtAxixA_LeftEyeSR.Text.Trim() == "")
            {
                txtAxixA_LeftEyeSR.Text = "0";
            }

            //if (int.Parse(txtAxixA_LeftEye.Text.Trim()) == 0)
            //{
            //    lbl_error.Text = "Invalid Axix.";
            //    txtAxixA_LeftEye.Focus();
            //    return false;
            //}

            if (int.Parse(txtAxixA_LeftEyeSR.Text.Trim()) > 180)
            {
                lbl_error.Text = "Invalid Axix.";
                txtAxixA_LeftEyeSR.Focus();
                return false;
            }

            if (txtNear_LeftEye.Text.Trim() == "")
            {
                txtNear_LeftEye.Text = "0.00";
            }

            try
            {
                decimal d = decimal.Parse(txtNear_LeftEye.Text.Trim());
            }
            catch (Exception ex)
            {
                lbl_error.Text = "Invalid Near Left Eye Points.";
                txtNear_LeftEye.Focus();
                return false;
            }

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

            chkNormal_RightEye.Checked = false;
            chkNormal_LeftEye.Checked = false;
            chkRefractiveError_RightEye.Checked = false;
            chkRefractiveError_LeftEye.Checked = false;
            chkLowVision_RightEye.Checked = false;
            chkLowVision_LeftEye.Checked = false;
            chkNeedsCycloplegicRefraction_RightEye.Checked = false;
            chkNeedsCycloplegicRefraction_LeftEye.Checked = false;
            chkSquintStrabismus_RightEye.Checked = false;
            chkSquintStrabismus_LeftEye.Checked = false;
            chkLazyEyeAmblyopia_RightEye.Checked = false;
            chkLazyEyeAmblyopia_LeftEye.Checked = false;
            chkColorblindnessAchromatopsia_RightEye.Checked = false;
            chkColorblindnessAchromatopsia_LeftEye.Checked = false;
            chkCataract_RightEye.Checked = false;
            chkCataract_LeftEye.Checked = false;
            chkTraumaticCataract_RightEye.Checked = false;
            chkTraumaticCataract_LeftEye.Checked = false;
            chkKeratoconus_RightEye.Checked = false;
            chkKeratoconus_LeftEye.Checked = false;
            chkAnisometropia_RightEye.Checked = false;
            chkAnisometropia_LeftEye.Checked = false;
            chkPtosis_RightEye.Checked = false;
            chkPtosis_LeftEye.Checked = false;
            chkNystagmus_RightEye.Checked = false;
            chkNystagmus_LeftEye.Checked = false;
            chkCorneadefects_RightEye.Checked = false;
            chkCorneadefects_LeftEye.Checked = false;
            chkPuplidefects_RightEye.Checked = false;
            chkPuplidefects_LeftEye.Checked = false;
            chkPterygium_RightEye.Checked = false;
            chkPterygium_LeftEye.Checked = false;
            chkOther_RightEye.Checked = false;
            chkOther_LeftEye.Checked = false;

            rdoRetinoScopy_RightEye.SelectedValue = "0";
            //rdoCycloplegicRefraction_RightEye.SelectedValue = "0";
            rdoCycloplegicRefraction_RightEye.ClearSelection();
            rdoCycloplegicRefraction_RightEye.Items[0].Selected = true;

            txtCondition_RightEye.Text = "";
            txtMeridian1_RightEye.Text = "";
            txtMeridian2_RightEye.Text = "";
            txtFinalPrescription_RightEye.Text = "";

            rdoRetinoScopy_LeftEye.SelectedValue = "0";
            //rdoCycloplegicRefraction_LeftEye.SelectedValue = "0";
            rdoCycloplegicRefraction_LeftEye.ClearSelection();
            rdoCycloplegicRefraction_LeftEye.Items[0].Selected = true;

            txtCondition_LeftEye.Text = "";
            txtMeridian1_LeftEye.Text = "";
            txtMeridian2_LeftEye.Text = "";
            txtFinalPrescription_LeftEye.Text = "";

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

                var dt = dx.sp_tblVisitSixMonthStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();
                if (dt != null)
                {
                    //hfAutoRefTestTransDate.Value = dt.VisitSixMonthStudentTransDate.ToString(); //   AutoRefStudentTransDate.ToString();

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


                    if (dt.Right_Spherical_Status_AutoRef == "P")
                    {
                        ddlSpherical_RightEye_AutoRef.SelectedIndex = 0;
                    }
                    else if (dt.Right_Spherical_Status_AutoRef == "N")
                    {
                        ddlSpherical_RightEye_AutoRef.SelectedIndex = 1;
                    }
                    else if (dt.Right_Spherical_Status_AutoRef == "O")
                    {
                        ddlSpherical_RightEye_AutoRef.SelectedIndex = 2;
                    }
                    else if (dt.Right_Spherical_Status_AutoRef == "E")
                    {
                        ddlSpherical_RightEye_AutoRef.SelectedIndex = 3;
                    }

                    if (decimal.Parse(dt.Right_Spherical_Points_AutoRef.ToString()) < 0)
                    {
                        txtSpherical_RightEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtSpherical_RightEye_AutoRef.Text = dt.Right_Spherical_Points_AutoRef.ToString();
                    }

                    if (dt.Right_Cyclinderical_Status_AutoRef == "P")
                    {
                        ddlCyclinderical_RightEye_AutoRef.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_RightEye_AutoRef.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Right_Cyclinderical_Points_AutoRef.ToString()) < 0)
                    {
                        txtCyclinderical_RightEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_RightEye_AutoRef.Text = dt.Right_Cyclinderical_Points_AutoRef.ToString();
                    }

                    if (int.Parse(dt.Right_Axix_From_AutoRef.ToString()) < 0)
                    {
                        txtAxixA_RightEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtAxixA_RightEye_AutoRef.Text = dt.Right_Axix_From_AutoRef.ToString();
                    }
                    //txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    txtAxixB_RightEye_AutoRef.Text = dt.Right_Axix_To_AutoRef.ToString();

                    if (dt.Left_Spherical_Status_AutoRef == "P")
                    {
                        ddlSpherical_LeftEye_AutoRef.SelectedIndex = 0;
                    }
                    else if (dt.Left_Spherical_Status_AutoRef == "N")
                    {
                        ddlSpherical_LeftEye_AutoRef.SelectedIndex = 1;
                    }
                    else if (dt.Left_Spherical_Status_AutoRef == "O")
                    {
                        ddlSpherical_LeftEye_AutoRef.SelectedIndex = 2;
                    }
                    else if (dt.Left_Spherical_Status_AutoRef == "E")
                    {
                        ddlSpherical_LeftEye_AutoRef.SelectedIndex = 3;
                    }
                    if (decimal.Parse(dt.Left_Spherical_Points_AutoRef.ToString()) < 0)
                    {
                        txtSpherical_LeftEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtSpherical_LeftEye_AutoRef.Text = dt.Left_Spherical_Points_AutoRef.ToString();
                    }
                    //txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    if (dt.Left_Cyclinderical_Status_AutoRef == "P")
                    {
                        ddlCyclinderical_LeftEye_AutoRef.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclinderical_LeftEye_AutoRef.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Left_Cyclinderical_Points_AutoRef.ToString()) < 0)
                    {
                        txtCyclinderical_LeftEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_LeftEye_AutoRef.Text = dt.Left_Cyclinderical_Points_AutoRef.ToString();
                    }
                    //txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    if (int.Parse(dt.Left_Axix_From_AutoRef.ToString()) < 0)
                    {
                        txtAxixA_LeftEye_AutoRef.Text = "";
                    }
                    else
                    {
                        txtAxixA_LeftEye_AutoRef.Text = dt.Left_Axix_From_AutoRef.ToString();
                    }
                    //txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    txtAxixB_LeftEye_AutoRef.Text = dt.Left_Axix_To_AutoRef.ToString();

                    if (dt.Right_Spherical_Status == "P")
                    {
                        ddlSphericalRightEyeSR.SelectedIndex = 0;
                    }
                    else if (dt.Right_Spherical_Status == "N")
                    {
                        ddlSphericalRightEyeSR.SelectedIndex = 1;
                    }
                    else if (dt.Right_Spherical_Status == "O")
                    {
                        ddlSphericalRightEyeSR.SelectedIndex = 2;
                    }
                    else if (dt.Right_Spherical_Status == "E")
                    {
                        ddlSphericalRightEyeSR.SelectedIndex = 3;
                    }

                    if (decimal.Parse(dt.Right_Spherical_Points.ToString()) < 0)
                    {
                        txtSpherical_RightEyeSR.Text = "";
                    }
                    else
                    {
                        txtSpherical_RightEyeSR.Text = dt.Right_Spherical_Points.ToString();
                    }

                    if (dt.Right_Cyclinderical_Status == "P")
                    {
                        ddlCyclindericalRightEyeSR.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclindericalRightEyeSR.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Right_Cyclinderical_Points.ToString()) < 0)
                    {
                        txtCyclinderical_RightEyeSR.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_RightEyeSR.Text = dt.Right_Cyclinderical_Points.ToString();
                    }

                    if (int.Parse(dt.Right_Axix_From.ToString()) < 0)
                    {
                        txtAxixA_RightEyeSR.Text = "";
                    }
                    else
                    {
                        txtAxixA_RightEyeSR.Text = dt.Right_Axix_From.ToString();
                    }
                    //txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                    txtAxixB_RightEyeSR.Text = dt.Right_Axix_To.ToString();

                    if (dt.Right_Near_Status == "P")
                    {
                        ddlNear_RightEyeSR.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNear_RightEyeSR.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Right_Near_Points.ToString()) < 0)
                    {
                        txtNear_RightEyeSR.Text = "";
                    }
                    else
                    {
                        txtNear_RightEyeSR.Text = dt.Right_Near_Points.ToString();
                    }

                    if (dt.Left_Spherical_Status == "P")
                    {
                        ddlSphericalLeftEyeSR.SelectedIndex = 0;
                    }
                    else if (dt.Left_Spherical_Status == "N")
                    {
                        ddlSphericalLeftEyeSR.SelectedIndex = 1;
                    }
                    else if (dt.Left_Spherical_Status == "O")
                    {
                        ddlSphericalLeftEyeSR.SelectedIndex = 2;
                    }
                    else if (dt.Left_Spherical_Status == "E")
                    {
                        ddlSphericalLeftEyeSR.SelectedIndex = 3;
                    }
                    if (decimal.Parse(dt.Left_Spherical_Points.ToString()) < 0)
                    {
                        txtSpherical_LeftEyeSR.Text = "";
                    }
                    else
                    {
                        txtSpherical_LeftEyeSR.Text = dt.Left_Spherical_Points.ToString();
                    }
                    //txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();

                    if (dt.Left_Cyclinderical_Status == "P")
                    {
                        ddlCyclindericalLeftEyeSR.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlCyclindericalLeftEyeSR.SelectedIndex = 1;
                    }

                    if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) < 0)
                    {
                        txtCyclinderical_LeftEyeSR.Text = "";
                    }
                    else
                    {
                        txtCyclinderical_LeftEyeSR.Text = dt.Left_Cyclinderical_Points.ToString();
                    }
                    //txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    if (int.Parse(dt.Left_Axix_From.ToString()) < 0)
                    {
                        txtAxixA_LeftEyeSR.Text = "";
                    }
                    else
                    {
                        txtAxixA_LeftEyeSR.Text = dt.Left_Axix_From.ToString();
                    }
                    //txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                    txtAxixB_LeftEyeSR.Text = dt.Left_Axix_To.ToString();

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
                    //txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();


                    if (dt.RetinoScopy_RightEye.ToString() == "-1")
                    {
                        rdoRetinoScopy_RightEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoRetinoScopy_RightEye.SelectedValue = dt.RetinoScopy_RightEye.ToString();
                    }

                    string strCycloplegicRefraction_RightEye = dt.CycloplegicRefraction_RightEye.ToString();
                    string sCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye;

                    string[] itemsCycloplegicRefraction_RightEye = sCycloplegicRefraction_RightEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_RightEye.Contains(rdoCycloplegicRefraction_RightEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_RightEye.Items[i].Selected = true;
                        }
                    }


                    //rdoCycloplegicRefraction_RightEye.SelectedValue = dt.CycloplegicRefraction_RightEye.ToString();

                    txtCondition_RightEye.Text = dt.Condition_RightEye.ToString();
                    txtMeridian1_RightEye.Text = dt.Meridian1_RightEye.ToString();
                    txtMeridian2_RightEye.Text = dt.Meridian2_RightEye.ToString();
                    txtFinalPrescription_RightEye.Text = dt.FinalPrescription_RightEye.ToString();

                    if (dt.RetinoScopy_LeftEye.ToString() == "-1")
                    {
                        rdoRetinoScopy_LeftEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoRetinoScopy_LeftEye.SelectedValue = dt.RetinoScopy_LeftEye.ToString();
                    }

                    string strCycloplegicRefraction_LeftEye = dt.CycloplegicRefraction_LeftEye.ToString();
                    string sCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye;

                    string[] itemsCycloplegicRefraction_LeftEye = sCycloplegicRefraction_LeftEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_LeftEye.Contains(rdoCycloplegicRefraction_LeftEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_LeftEye.Items[i].Selected = true;
                        }
                    }

                    //rdoCycloplegicRefraction_LeftEye.SelectedValue = dt.CycloplegicRefraction_LeftEye.ToString();

                    txtCondition_LeftEye.Text = dt.Condition_LeftEye.ToString();
                    txtMeridian1_LeftEye.Text = dt.Meridian1_LeftEye.ToString();
                    txtMeridian2_LeftEye.Text = dt.Meridian2_LeftEye.ToString();
                    txtFinalPrescription_LeftEye.Text = dt.FinalPrescription_LeftEye.ToString();

                    //if (dt.Daignosis.ToString() != "-1")
                    //{
                    //    //rdoDiagnosis_RightEye.SelectedValue = dt.Daignosis.ToString();
                    //    string strDiagnosis = dt.Daignosis.ToString();
                    //    string d = strDiagnosis;

                    //    string[] itemsDiagnosis = d.Split(',');
                    //    for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //    {
                    //        if (itemsDiagnosis.Contains(rdoDiagnosis_RightEye.Items[i].Value))
                    //        {
                    //            rdoDiagnosis_RightEye.Items[i].Selected = true;
                    //        }
                    //    }
                    //    rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);
                    //}
                    //else
                    //{
                    //    rdoDiagnosis_RightEye.SelectedIndex = -1;
                    //    rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);
                    //}

                    ClearCheckboxes();

                    if (dt.Normal_RightEye == 1) chkNormal_RightEye.Checked = true;
                    if (dt.RefractiveError_RightEye == 1) chkRefractiveError_RightEye.Checked = true;
                    if (dt.LowVision_RightEye == 1) chkLowVision_RightEye.Checked = true;
                    if (dt.NeedsCycloplegicRefraction_RightEye == 1) chkNeedsCycloplegicRefraction_RightEye.Checked = true;
                    if (dt.SquintStrabismus_RightEye == 1) chkSquintStrabismus_RightEye.Checked = true;
                    if (dt.LazyEyeAmblyopia_RightEye == 1) chkLazyEyeAmblyopia_RightEye.Checked = true;
                    if (dt.ColorblindnessAchromatopsia_RightEye == 1) chkColorblindnessAchromatopsia_RightEye.Checked = true;
                    if (dt.Cataract_RightEye == 1) chkCataract_RightEye.Checked = true;
                    if (dt.TraumaticCataract_RightEye == 1) chkTraumaticCataract_RightEye.Checked = true;
                    if (dt.Keratoconus_RightEye == 1) chkKeratoconus_RightEye.Checked = true;
                    if (dt.Anisometropia_RightEye == 1) chkAnisometropia_RightEye.Checked = true;
                    if (dt.Ptosis_RightEye == 1) chkPtosis_RightEye.Checked = true;
                    if (dt.Nystagmus_RightEye == 1) chkNystagmus_RightEye.Checked = true;
                    if (dt.Corneadefects_RightEye == 1) chkCorneadefects_RightEye.Checked = true;
                    if (dt.Puplidefects_RightEye == 1) chkPuplidefects_RightEye.Checked = true;
                    if (dt.Pterygium_RightEye == 1) chkPterygium_RightEye.Checked = true;
                    if (dt.Other_RightEye == 1) chkOther_RightEye.Checked = true;

                    if (dt.Normal_LeftEye == 1) chkNormal_LeftEye.Checked = true;
                    if (dt.RefractiveError_LeftEye == 1) chkRefractiveError_LeftEye.Checked = true;
                    if (dt.LowVision_LeftEye == 1) chkLowVision_LeftEye.Checked = true;
                    if (dt.NeedsCycloplegicRefraction_LeftEye == 1) chkNeedsCycloplegicRefraction_LeftEye.Checked = true;
                    if (dt.SquintStrabismus_LeftEye == 1) chkSquintStrabismus_LeftEye.Checked = true;
                    if (dt.LazyEyeAmblyopia_LeftEye == 1) chkLazyEyeAmblyopia_LeftEye.Checked = true;
                    if (dt.ColorblindnessAchromatopsia_LeftEye == 1) chkColorblindnessAchromatopsia_LeftEye.Checked = true;
                    if (dt.Cataract_LeftEye == 1) chkCataract_LeftEye.Checked = true;
                    if (dt.TraumaticCataract_LeftEye == 1) chkTraumaticCataract_LeftEye.Checked = true;
                    if (dt.Keratoconus_LeftEye == 1) chkKeratoconus_LeftEye.Checked = true;
                    if (dt.Anisometropia_LeftEye == 1) chkAnisometropia_LeftEye.Checked = true;
                    if (dt.Ptosis_LeftEye == 1) chkPtosis_LeftEye.Checked = true;
                    if (dt.Nystagmus_LeftEye == 1) chkNystagmus_LeftEye.Checked = true;
                    if (dt.Corneadefects_LeftEye == 1) chkCorneadefects_LeftEye.Checked = true;
                    if (dt.Puplidefects_LeftEye == 1) chkPuplidefects_LeftEye.Checked = true;
                    if (dt.Pterygium_LeftEye == 1) chkPterygium_LeftEye.Checked = true;
                    if (dt.Other_LeftEye == 1) chkOther_LeftEye.Checked = true;

                    enableCheckboxSelections();

                    txtDiagnosis_RightEye.Text = dt.DaignosisRemarks.ToString();
                    txtDiagnosis_LeftEye.Text = dt.DaignosisRemarks_LeftEye.ToString();

                    if (dt.SubDaignosis.ToString() != "-1")
                    {
                        //rdoDiagnosis_RightEye.SelectedValue = dt.Daignosis.ToString();
                        string strDiagnosis = dt.SubDaignosis.ToString();
                        string d = strDiagnosis;

                        string[] itemsDiagnosis = d.Split(',');
                        for (int i = 0; i < chkListRefractiveErrorOpt.Items.Count; i++)
                        {
                            if (itemsDiagnosis.Contains(chkListRefractiveErrorOpt.Items[i].Value))
                            {
                                chkListRefractiveErrorOpt.Items[i].Selected = true;
                            }
                        }
                        //chkListRefractiveErrorOpt_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        chkListRefractiveErrorOpt.SelectedIndex = -1;
                        //rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);
                    }

                    if (dt.SubDaignosis_LeftEye.ToString() != "-1")
                    {
                        //rdoDiagnosis_RightEye.SelectedValue = dt.Daignosis.ToString();
                        string strDiagnosis_LeftEye = dt.SubDaignosis_LeftEye.ToString();
                        string d = strDiagnosis_LeftEye;

                        string[] itemsDiagnosis_LeftEye = d.Split(',');
                        for (int i = 0; i < chkListRefractiveErrorOpt_LeftEye.Items.Count; i++)
                        {
                            if (itemsDiagnosis_LeftEye.Contains(chkListRefractiveErrorOpt_LeftEye.Items[i].Value))
                            {
                                chkListRefractiveErrorOpt_LeftEye.Items[i].Selected = true;
                            }
                        }
                        //chkListRefractiveErrorOpt_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        chkListRefractiveErrorOpt_LeftEye.SelectedIndex = -1;
                        //rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);
                    }

                    if (dt.SubTreatment.ToString() != "-1")
                    {
                        rdoTreatment_Glasses.SelectedValue = dt.SubTreatment.ToString();
                    }
                    else
                    {
                        rdoTreatment_Glasses.SelectedIndex = -1;
                    }
                    //rdoTreatment_Glasses.SelectedValue = dt.SubTreatment.ToString();

                    string strMedicine = dt.MedicineAutoId.ToString();
                    string s = strMedicine;

                    string[] items = s.Split(',');
                    for (int i = 0; i < chkMedicine.Items.Count; i++)
                    {
                        if (items.Contains(chkMedicine.Items[i].Value))
                        {
                            chkMedicine.Items[i].Selected = true;
                        }
                    }

                    if (dt.NextVisit.ToString() == "1")
                    {
                        rdoFollowupAfterSixMonths.Checked = false;
                        rdoNotRequiredFollowup.Checked = true;
                        rdoNextVisitDate.Checked = false;
                        //txtRoutineCheckupDate.Text = DateTime.Parse(dt.NextVisitDate.ToString()).ToString("dd-MMM-yyyy");
                        //txtRoutineCheckupDate.Visible = true;
                        //rdoNotRequiredFollowup_CheckedChanged(null, null);
                    }
                    else if (dt.NextVisit.ToString() == "2")
                    {
                        rdoFollowupAfterSixMonths.Checked = false;
                        rdoNotRequiredFollowup.Checked = false;
                        rdoNextVisitDate.Checked = true;
                        txtRoutineCheckupDate.Text = DateTime.Parse(dt.NextVisitDate.ToString()).ToString("dd-MMM-yyyy") == "01-Jan-1900" ? "" : DateTime.Parse(dt.NextVisitDate.ToString()).ToString("dd-MMM-yyyy");
                        txtRoutineCheckupDate.Visible = true;
                        //rdoNotRequiredFollowup_CheckedChanged(null, null);
                    }
                    else
                    {
                        rdoFollowupAfterSixMonths.Checked = true;
                        rdoNotRequiredFollowup.Checked = false;
                        rdoNextVisitDate.Checked = false;
                        //txtRoutineCheckupDate.Text = "";
                        //txtRoutineCheckupDate.Visible = false;
                        //rdoNotRequiredFollowup_CheckedChanged(null, null);
                    }

                    var dtImage = dx.sp_tblStudentImage_GetDetail(Convert.ToInt32(dt.StudentAutoId.ToString())).SingleOrDefault();
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
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;
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
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Student_School_SixMonthVisit(iType)
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_SixMonthVisit(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
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

                    var dtAutoRefLastData = dx.sp_tblOptometristMasterStudent_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtAutoRefLastData != null)
                        {
                            lblAutoRefPrevVisitDate.Text = DateTime.Parse(dtAutoRefLastData.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                            //txtTestDate.Enabled = false;

                            string Right_Spherical_Points = dtAutoRefLastData.Right_Spherical_Points.ToString();
                            if (dtAutoRefLastData.Right_Spherical_Status == "Plano")
                            {
                                Right_Spherical_Points = "";
                            }
                            else if (dtAutoRefLastData.Right_Spherical_Status == "Error")
                            {
                                Right_Spherical_Points = "";
                            }
                            lblSpherical_RightEye_AutoRef.Text = dtAutoRefLastData.Right_Spherical_Status + Right_Spherical_Points;
                            lblCylinderical_RightEye_AutoRef.Text = dtAutoRefLastData.Right_Cyclinderical_Status + dtAutoRefLastData.Right_Cyclinderical_Points.ToString();
                            lblAxix_RightEye_AutoRef.Text = dtAutoRefLastData.Right_Axix_From.ToString(); // + " to " + dtLastData.Right_Axix_To.ToString();

                            string Left_Spherical_Points = dtAutoRefLastData.Left_Spherical_Points.ToString();
                            if (dtAutoRefLastData.Left_Spherical_Status == "Plano")
                            {
                                Left_Spherical_Points = "";
                            }
                            else if (dtAutoRefLastData.Left_Spherical_Status == "Error")
                            {
                                Left_Spherical_Points = "";
                            }
                            lblSpherical_LeftEye_AutoRef.Text = dtAutoRefLastData.Left_Spherical_Status + Left_Spherical_Points;
                            lblCylinderical_LeftEye_AutoRef.Text = dtAutoRefLastData.Left_Cyclinderical_Status + dtAutoRefLastData.Left_Cyclinderical_Points.ToString();
                            lblAxix_LeftEye_AutoRef.Text = dtAutoRefLastData.Left_Axix_From.ToString(); // + " to " + dtLastData.Left_Axix_To.ToString();
                        }
                        //else
                        //{
                        //    txtTestDate.Text = Utilities.GetDate();
                        //    txtTestDate.Enabled = true;
                        //}
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    int iOptometristStudentId = 0;
                    var dtLastData = dx.sp_tblOptometristMasterStudent_GetTransactionData(Convert.ToInt32(hfStudentIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate.Text)).SingleOrDefault();
                    try
                    {
                        if (dtLastData != null)
                        {
                            iOptometristStudentId = dtLastData.OptometristStudentId;
                            lblSubjectiveRefPrevVisitDate.Text = DateTime.Parse(dtLastData.OptometristStudentTransDate.ToString()).ToString("dd-MMM-yyyy");

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


                    var dtTreatmentData = dx.sp_VisitSummary_Student(Convert.ToInt32(hfStudentIDPKID.Value), iOptometristStudentId).SingleOrDefault();
                    try
                    {
                        if (dtTreatmentData != null)
                        {
                            lblDiagnosis.Text = dtTreatmentData.Daignosis.ToString();
                            lblDiagnosisRemarks.Text = dtTreatmentData.DaignosisRemarks.ToString();
                            lblSubDiagnosis.Text = dtTreatmentData.SubDaignosis.ToString();

                            lblTreatment.Text = dtTreatmentData.SubTreatment.ToString();
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





                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        var dtVisitSixMonthPreviousData = dx.sp_tblVisitSixMonthStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtVisitSixMonthPreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtVisitSixMonthPreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitSixMonthStudentId";
                                ddlPreviousTestResult.DataTextField = "VisitSixMonthStudentTransDate";
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

                    var dtAutoRefTest_Student = dx.sp_AutoRefTest_StudentVisit(int.Parse(hfStudentIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate.Text)).SingleOrDefault();
                    if (dtAutoRefTest_Student != null)
                    {
                        pnlTestSummary.Visible = true;

                        lblAutoRef_RightEye.Text = dtAutoRefTest_Student.Spherical__Right_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Right_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Right_Eye_.ToString();
                        lblAutoRef_LeftEye.Text = dtAutoRefTest_Student.Spherical__Left_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Left_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Left_Eye_.ToString();
                    }

                    var dtOptometrist_Student = dx.sp_Optometrist_StudentVisit(int.Parse(hfStudentIDPKID.Value), DateTime.Parse(lblAutoRefPrevVisitDate.Text)).SingleOrDefault();

                    if (dtOptometrist_Student != null)
                    {
                        pnlTestSummary.Visible = true;

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
            Response.Redirect("~/FollowupVisitAfterSixMonths.aspx");
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
            Response.Redirect("~/FollowupVisitAfterSixMonths.aspx");
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

        private byte[] GetUploadedImage()
        {
            byte[] imageBytes = Convert.FromBase64String(hfImageBytes.Value.Split(',')[1]);

            return imageBytes;
        }

        protected void rdoRetinoScopy_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoRetinoScopy_RightEye.SelectedValue == "0")
            {
                rdoCycloplegicRefraction_RightEye.SelectedValue = "0";
            }
            else
            {
                rdoCycloplegicRefraction_RightEye.SelectedValue = "1";
            }
        }

        protected void rdoRetinoScopy_LeftEye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoRetinoScopy_LeftEye.SelectedValue == "0")
            {
                rdoCycloplegicRefraction_LeftEye.SelectedValue = "0";
            }
            else
            {
                rdoCycloplegicRefraction_LeftEye.SelectedValue = "1";
            }
        }

        private void BindCombos()
        {

            try
            {
                var dtMedicine = (from a in dx.tblMedicines
                                  select a).ToList();

                if (dtMedicine.Count != 0)
                {
                    chkMedicine.DataSource = dtMedicine;
                    chkMedicine.DataValueField = "MedicineAutoId";
                    chkMedicine.DataTextField = "MedicineDescription";
                    chkMedicine.DataBind();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        //protected void rdoDiagnosis_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //rdoSubDiagnosis_RightEye.Visible = false;
        //    //pnlFamilyDetail.Visible = false;

        //    pnlTreatment.Visible = false;
        //    //pnlVisit1.Visible = false;

        //    txtDiagnosis_RightEye.Visible = false;
        //    rdoTreatment_Glasses.SelectedIndex = -1;

        //    if (rdoDiagnosis_RightEye.Items[17].Selected == true)
        //    {
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = true;

        //        pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[15].Selected == true
        //                || rdoDiagnosis_RightEye.Items[14].Selected == true
        //                || rdoDiagnosis_RightEye.Items[13].Selected == true
        //                || rdoDiagnosis_RightEye.Items[12].Selected == true
        //                || rdoDiagnosis_RightEye.Items[11].Selected == true
        //                || rdoDiagnosis_RightEye.Items[10].Selected == true
        //                || rdoDiagnosis_RightEye.Items[9].Selected == true
        //                || rdoDiagnosis_RightEye.Items[8].Selected == true
        //                || rdoDiagnosis_RightEye.Items[7].Selected == true
        //                || rdoDiagnosis_RightEye.Items[6].Selected == true
        //                || rdoDiagnosis_RightEye.Items[5].Selected == true
        //                || rdoDiagnosis_RightEye.Items[4].Selected == true
        //                || rdoDiagnosis_RightEye.Items[3].Selected == true
        //                || rdoDiagnosis_RightEye.Items[16].Selected == true)
        //    {
        //        //if (rdoType.SelectedValue == "0")
        //        //{
        //        //pnlFamilyDetail.Visible = true;
        //        //}
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[1].Selected == true)
        //    {
        //        //rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;

        //        rdoTreatment_Glasses.SelectedValue = "0";
        //        //rdoNextVisit.SelectedValue = "1";
        //    }
        //    if (rdoDiagnosis_RightEye.Items[2].Selected == true)
        //    {
        //        //rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;

        //        rdoTreatment_Glasses.SelectedValue = "0";
        //        //rdoNextVisit.SelectedValue = "1";
        //    }

        //    if (rdoDiagnosis_RightEye.SelectedIndex == -1)
        //    {
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;
        //        pnlTreatment.Visible = false;
        //        //pnlVisit1.Visible = false;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[0].Selected == true)
        //    {
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;
        //        pnlTreatment.Visible = false;
        //        //pnlVisit1.Visible = false;
        //        //rdoSubDiagnosis_RightEye.Visible = false;
        //        rdoDiagnosis_RightEye.Items[17].Selected = false;
        //        rdoDiagnosis_RightEye.Items[16].Selected = false;
        //        rdoDiagnosis_RightEye.Items[15].Selected = false;
        //        rdoDiagnosis_RightEye.Items[14].Selected = false;
        //        rdoDiagnosis_RightEye.Items[13].Selected = false;
        //        rdoDiagnosis_RightEye.Items[12].Selected = false;
        //        rdoDiagnosis_RightEye.Items[11].Selected = false;
        //        rdoDiagnosis_RightEye.Items[10].Selected = false;
        //        rdoDiagnosis_RightEye.Items[9].Selected = false;
        //        rdoDiagnosis_RightEye.Items[8].Selected = false;
        //        rdoDiagnosis_RightEye.Items[7].Selected = false;
        //        rdoDiagnosis_RightEye.Items[6].Selected = false;
        //        rdoDiagnosis_RightEye.Items[5].Selected = false;
        //        rdoDiagnosis_RightEye.Items[4].Selected = false;
        //        rdoDiagnosis_RightEye.Items[3].Selected = false;
        //        rdoDiagnosis_RightEye.Items[2].Selected = false;
        //        rdoDiagnosis_RightEye.Items[1].Selected = false;

        //        rdoDiagnosis_RightEye.Items[17].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[16].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[15].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[14].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[13].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[12].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[11].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[10].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[9].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[8].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[7].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[6].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[5].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[4].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[3].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[2].Enabled = false;
        //        rdoDiagnosis_RightEye.Items[1].Enabled = false;
        //    }
        //    else
        //    {
        //        rdoDiagnosis_RightEye.Items[17].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[16].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[15].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[14].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[13].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[12].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[11].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[10].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[9].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[8].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[7].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[6].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[5].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[4].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[3].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[2].Enabled = true;
        //        rdoDiagnosis_RightEye.Items[1].Enabled = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[17].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;
        //    }
        //}

        protected void rdoNotRequiredFollowup_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNextVisitDate.Checked == true)
            {
                txtRoutineCheckupDate.Visible = true;
                txtRoutineCheckupDate.Focus();
            }
            else
            {
                txtRoutineCheckupDate.Visible = false;
            }
        }

        protected void rdoNextVisitDate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNextVisitDate.Checked == true)
            {
                txtRoutineCheckupDate.Visible = true;
                txtRoutineCheckupDate.Focus();
            }
            else
            {
                txtRoutineCheckupDate.Visible = false;
            }
        }

        protected void rdoFollowupAfterSixMonths_CheckedChanged(object sender, EventArgs e)
        {

            if (rdoNextVisitDate.Checked == true)
            {
                txtRoutineCheckupDate.Visible = true;
                txtRoutineCheckupDate.Focus();
            }
            else
            {
                txtRoutineCheckupDate.Visible = false;
            }
        }

        protected void enableCheckboxSelections()
        {
            pnlTreatment.Visible = false;
            rdoTreatment_Glasses.SelectedIndex = -1;
            txtDiagnosis_RightEye.Visible = false;
            txtDiagnosis_LeftEye.Visible = false;

            rdoTreatment_Glasses.SelectedIndex = -1;

            if (chkOther_RightEye.Checked == true)
            {
                txtDiagnosis_RightEye.Visible = true;

                pnlTreatment.Visible = true;
            }
            if (chkOther_LeftEye.Checked == true)
            {
                txtDiagnosis_LeftEye.Visible = true;

                pnlTreatment.Visible = true;
            }
            if (chkNeedsCycloplegicRefraction_RightEye.Checked == true ||
            chkNeedsCycloplegicRefraction_LeftEye.Checked == true ||
            chkSquintStrabismus_RightEye.Checked == true ||
            chkSquintStrabismus_LeftEye.Checked == true ||
            chkLazyEyeAmblyopia_RightEye.Checked == true ||
            chkLazyEyeAmblyopia_LeftEye.Checked == true ||
            chkColorblindnessAchromatopsia_RightEye.Checked == true ||
            chkColorblindnessAchromatopsia_LeftEye.Checked == true ||
            chkCataract_RightEye.Checked == true ||
            chkCataract_LeftEye.Checked == true ||
            chkTraumaticCataract_RightEye.Checked == true ||
            chkTraumaticCataract_LeftEye.Checked == true ||
            chkKeratoconus_RightEye.Checked == true ||
            chkKeratoconus_LeftEye.Checked == true ||
            chkAnisometropia_RightEye.Checked == true ||
            chkAnisometropia_LeftEye.Checked == true ||
            chkPtosis_RightEye.Checked == true ||
            chkPtosis_LeftEye.Checked == true ||
            chkNystagmus_RightEye.Checked == true ||
            chkNystagmus_LeftEye.Checked == true ||
            chkCorneadefects_RightEye.Checked == true ||
            chkCorneadefects_LeftEye.Checked == true ||
            chkPuplidefects_RightEye.Checked == true ||
            chkPuplidefects_LeftEye.Checked == true ||
            chkPterygium_RightEye.Checked == true ||
            chkPterygium_LeftEye.Checked == true)
            {
                txtDiagnosis_RightEye.Visible = false;
                txtDiagnosis_LeftEye.Visible = false;

                pnlTreatment.Visible = true;
            }

            if (chkRefractiveError_RightEye.Checked == true)
            {
                chkListRefractiveErrorOpt.Visible = true;
                txtDiagnosis_RightEye.Visible = false;
                pnlTreatment.Visible = true;
                rdoTreatment_Glasses.SelectedValue = "0";
            }

            if (chkRefractiveError_LeftEye.Checked == true)
            {
                chkListRefractiveErrorOpt_LeftEye.Visible = true;
                txtDiagnosis_LeftEye.Visible = false;

                pnlTreatment.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
            }

            if (chkLowVision_RightEye.Checked == true)
            {
                chkListRefractiveErrorOpt.Visible = true;
                txtDiagnosis_RightEye.Visible = false;
                pnlTreatment.Visible = true;
                rdoTreatment_Glasses.SelectedValue = "0";
            }

            if (chkLowVision_LeftEye.Checked == true)
            {
                chkListRefractiveErrorOpt_LeftEye.Visible = true;
                txtDiagnosis_LeftEye.Visible = false;
                pnlTreatment.Visible = true;
                rdoTreatment_Glasses.SelectedValue = "0";
            }

            //if (rdoDiagnosis_RightEye.SelectedIndex == -1)
            //{
            //    pnlFamilyDetail.Visible = false;
            //    txtDiagnosis_RightEye.Visible = false;
            //    pnlTreatment.Visible = false;
            //    pnlVisit1.Visible = false;
            //}

            if (chkNormal_RightEye.Checked == true)
            {
                //pnlFamilyDetail.Visible = false;

                //txtMotherName.Text = "";
                //txtMotherCell.Text = "";
                //txtFatherCell.Text = "";
                //txtAddress1.Text = "";
                //txtAddress2.Text = "";

                //txtDistrict.Text = "";
                //txtTown.Text = "";
                //txtCity.Text = "";

                chkMedicine.ClearSelection();
                chkListRefractiveErrorOpt.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;

                txtDiagnosis_RightEye.Visible = false;
                pnlTreatment.Visible = false;
                chkListRefractiveErrorOpt.Visible = false;

                chkRefractiveError_RightEye.Checked = false;
                chkLowVision_RightEye.Checked = false;
                chkNeedsCycloplegicRefraction_RightEye.Checked = false;
                chkSquintStrabismus_RightEye.Checked = false;
                chkLazyEyeAmblyopia_RightEye.Checked = false;
                chkColorblindnessAchromatopsia_RightEye.Checked = false;
                chkCataract_RightEye.Checked = false;
                chkTraumaticCataract_RightEye.Checked = false;
                chkKeratoconus_RightEye.Checked = false;
                chkAnisometropia_RightEye.Checked = false;
                chkPtosis_RightEye.Checked = false;
                chkNystagmus_RightEye.Checked = false;
                chkCorneadefects_RightEye.Checked = false;
                chkPuplidefects_RightEye.Checked = false;
                chkPterygium_RightEye.Checked = false;
                chkOther_RightEye.Checked = false;

                chkRefractiveError_RightEye.Enabled = false;
                chkLowVision_RightEye.Enabled = false;
                chkNeedsCycloplegicRefraction_RightEye.Enabled = false;
                chkSquintStrabismus_RightEye.Enabled = false;
                chkLazyEyeAmblyopia_RightEye.Enabled = false;
                chkColorblindnessAchromatopsia_RightEye.Enabled = false;
                chkCataract_RightEye.Enabled = false;
                chkTraumaticCataract_RightEye.Enabled = false;
                chkKeratoconus_RightEye.Enabled = false;
                chkAnisometropia_RightEye.Enabled = false;
                chkPtosis_RightEye.Enabled = false;
                chkNystagmus_RightEye.Enabled = false;
                chkCorneadefects_RightEye.Enabled = false;
                chkPuplidefects_RightEye.Enabled = false;
                chkPterygium_RightEye.Enabled = false;
                chkOther_RightEye.Enabled = false;
            }
            else
            {

                chkRefractiveError_RightEye.Enabled = true;
                chkLowVision_RightEye.Enabled = true;
                chkNeedsCycloplegicRefraction_RightEye.Enabled = true;
                chkSquintStrabismus_RightEye.Enabled = true;
                chkLazyEyeAmblyopia_RightEye.Enabled = true;
                chkColorblindnessAchromatopsia_RightEye.Enabled = true;
                chkCataract_RightEye.Enabled = true;
                chkTraumaticCataract_RightEye.Enabled = true;
                chkKeratoconus_RightEye.Enabled = true;
                chkAnisometropia_RightEye.Enabled = true;
                chkPtosis_RightEye.Enabled = true;
                chkNystagmus_RightEye.Enabled = true;
                chkCorneadefects_RightEye.Enabled = true;
                chkPuplidefects_RightEye.Enabled = true;
                chkPterygium_RightEye.Enabled = true;
                chkOther_RightEye.Enabled = true;
            }

            if (chkNormal_LeftEye.Checked == true)
            {
                //pnlFamilyDetail.Visible = false;

                //txtMotherName.Text = "";
                //txtMotherCell.Text = "";
                //txtFatherCell.Text = "";
                //txtAddress1.Text = "";
                //txtAddress2.Text = "";

                //txtDistrict.Text = "";
                //txtTown.Text = "";
                //txtCity.Text = "";

                chkMedicine.ClearSelection();
                txtDiagnosis_LeftEye.Visible = false;
                pnlTreatment.Visible = false;
                chkListRefractiveErrorOpt_LeftEye.SelectedIndex = -1;
                chkListRefractiveErrorOpt_LeftEye.Visible = false;

                chkRefractiveError_LeftEye.Checked = false;
                chkLowVision_LeftEye.Checked = false;
                chkNeedsCycloplegicRefraction_LeftEye.Checked = false;
                chkSquintStrabismus_LeftEye.Checked = false;
                chkLazyEyeAmblyopia_LeftEye.Checked = false;
                chkColorblindnessAchromatopsia_LeftEye.Checked = false;
                chkCataract_LeftEye.Checked = false;
                chkTraumaticCataract_LeftEye.Checked = false;
                chkKeratoconus_LeftEye.Checked = false;
                chkAnisometropia_LeftEye.Checked = false;
                chkPtosis_LeftEye.Checked = false;
                chkNystagmus_LeftEye.Checked = false;
                chkCorneadefects_LeftEye.Checked = false;
                chkPuplidefects_LeftEye.Checked = false;
                chkPterygium_LeftEye.Checked = false;
                chkOther_LeftEye.Checked = false;

                chkRefractiveError_LeftEye.Enabled = false;
                chkLowVision_LeftEye.Enabled = false;
                chkNeedsCycloplegicRefraction_LeftEye.Enabled = false;
                chkSquintStrabismus_LeftEye.Enabled = false;
                chkLazyEyeAmblyopia_LeftEye.Enabled = false;
                chkColorblindnessAchromatopsia_LeftEye.Enabled = false;
                chkCataract_LeftEye.Enabled = false;
                chkTraumaticCataract_LeftEye.Enabled = false;
                chkKeratoconus_LeftEye.Enabled = false;
                chkAnisometropia_LeftEye.Enabled = false;
                chkPtosis_LeftEye.Enabled = false;
                chkNystagmus_LeftEye.Enabled = false;
                chkCorneadefects_LeftEye.Enabled = false;
                chkPuplidefects_LeftEye.Enabled = false;
                chkPterygium_LeftEye.Enabled = false;
                chkOther_LeftEye.Enabled = false;
            }
            else
            {

                chkRefractiveError_LeftEye.Enabled = true;
                chkLowVision_LeftEye.Enabled = true;
                chkNeedsCycloplegicRefraction_LeftEye.Enabled = true;
                chkSquintStrabismus_LeftEye.Enabled = true;
                chkLazyEyeAmblyopia_LeftEye.Enabled = true;
                chkColorblindnessAchromatopsia_LeftEye.Enabled = true;
                chkCataract_LeftEye.Enabled = true;
                chkTraumaticCataract_LeftEye.Enabled = true;
                chkKeratoconus_LeftEye.Enabled = true;
                chkAnisometropia_LeftEye.Enabled = true;
                chkPtosis_LeftEye.Enabled = true;
                chkNystagmus_LeftEye.Enabled = true;
                chkCorneadefects_LeftEye.Enabled = true;
                chkPuplidefects_LeftEye.Enabled = true;
                chkPterygium_LeftEye.Enabled = true;
                chkOther_LeftEye.Enabled = true;
            }

            if (chkOther_RightEye.Checked == true)
            {
                txtDiagnosis_RightEye.Visible = true;
            }

            if (chkOther_LeftEye.Checked == true)
            {
                txtDiagnosis_LeftEye.Visible = true;
            }
        }

        private void ClearCheckboxes()
        {
            chkNormal_RightEye.Checked = false;
            chkNormal_LeftEye.Checked = false;
            chkRefractiveError_RightEye.Checked = false;
            chkRefractiveError_LeftEye.Checked = false;
            chkLowVision_RightEye.Checked = false;
            chkLowVision_LeftEye.Checked = false;
            chkNeedsCycloplegicRefraction_RightEye.Checked = false;
            chkNeedsCycloplegicRefraction_LeftEye.Checked = false;
            chkSquintStrabismus_RightEye.Checked = false;
            chkSquintStrabismus_LeftEye.Checked = false;
            chkLazyEyeAmblyopia_RightEye.Checked = false;
            chkLazyEyeAmblyopia_LeftEye.Checked = false;
            chkColorblindnessAchromatopsia_RightEye.Checked = false;
            chkColorblindnessAchromatopsia_LeftEye.Checked = false;
            chkCataract_RightEye.Checked = false;
            chkCataract_LeftEye.Checked = false;
            chkTraumaticCataract_RightEye.Checked = false;
            chkTraumaticCataract_LeftEye.Checked = false;
            chkKeratoconus_RightEye.Checked = false;
            chkKeratoconus_LeftEye.Checked = false;
            chkAnisometropia_RightEye.Checked = false;
            chkAnisometropia_LeftEye.Checked = false;
            chkPtosis_RightEye.Checked = false;
            chkPtosis_LeftEye.Checked = false;
            chkNystagmus_RightEye.Checked = false;
            chkNystagmus_LeftEye.Checked = false;
            chkCorneadefects_RightEye.Checked = false;
            chkCorneadefects_LeftEye.Checked = false;
            chkPuplidefects_RightEye.Checked = false;
            chkPuplidefects_LeftEye.Checked = false;
            chkPterygium_RightEye.Checked = false;
            chkPterygium_LeftEye.Checked = false;
            chkOther_RightEye.Checked = false;
            chkOther_LeftEye.Checked = false;
        }

        protected void chkNormal_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkRefractiveError_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkLowVision_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkNeedsCycloplegicRefraction_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkSquintStrabismus_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkLazyEyeAmblyopia_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkColorblindnessAchromatopsia_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkCataract_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkTraumaticCataract_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkKeratoconus_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkAnisometropia_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPtosis_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkNystagmus_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkCorneadefects_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPuplidefects_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPterygium_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkOther_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkNormal_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkRefractiveError_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkLowVision_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkNeedsCycloplegicRefraction_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkSquintStrabismus_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkLazyEyeAmblyopia_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkColorblindnessAchromatopsia_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkCataract_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkTraumaticCataract_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkKeratoconus_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkAnisometropia_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPtosis_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkNystagmus_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkCorneadefects_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPuplidefects_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkPterygium_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }

        protected void chkOther_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            enableCheckboxSelections();
        }
    }
}