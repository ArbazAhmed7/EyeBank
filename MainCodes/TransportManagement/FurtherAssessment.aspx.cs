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
    public partial class FurtherAssessment : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "HospitalVisitForFurtherAssessment"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                BindCombos();

                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);

                rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

                rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                //rdoDiagnosis_RightEye.SelectedIndex = -1;
                //rdoDiagnosis_RightEye.SelectedValue = "-1";

                ClearCheckboxes();

                rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                rdoSubDiagnosis_RightEye.SelectedValue = "-1";

                rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
                rdoSubDiagnosis_LeftEye.SelectedValue = "-1";

                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedValue = "-1";

                rdoNextVisit.SelectedIndex = -1;
                rdoNextVisit.SelectedValue = "-1";

                rdoSurgery.SelectedIndex = -1;
                rdoSurgery.SelectedValue = "-1";

                rdoSurgery_Detail.SelectedIndex = -1;
                rdoSurgery_Detail.SelectedValue = "-1";

                rdoReferal.SelectedIndex = -1;
                rdoReferal.SelectedValue = "-1";

                //rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);

                enableCheckboxSelections();

                rdoNextVisit_SelectedIndexChanged(null, null);

                txtNear_RightEye.Text = "00.00";
                txtNear_LeftEye.Text = "00.00";

                txtTestDate.Text = Utilities.GetDate();

                pnlTestSummary.Visible = false;

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
            pnlStudent.Visible = true;
            pnlStudent_Sub.Visible = true;

            pnlTeacher.Visible = false;
            pnlTeacher_Sub.Visible = false;

            rdoOptometristTest.SelectedValue = "1";
            rdoOptometristTest_SelectedIndexChanged(null, null);

            ClearForm();
            ClearValidation();

            txtStudentCode.Focus();
        }
        protected void lblShowStudentDetail_Click(object sender, EventArgs e)
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                if (ValidateInputStudent())
                {
                    int iGlassType_RightEye = 0;
                    iGlassType_RightEye = 0;
                    int iGlassType_LeftEye = 0;
                    iGlassType_LeftEye = 0;

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

                    int iNeedsCycloRefraction_RightEye = 0;
                    if (chkNeedsCycloRefraction_RightEye.Checked == true)
                    {
                        iNeedsCycloRefraction_RightEye = 1;
                    }

                    int iNeedsCycloRefraction_LeftEye = 0;
                    if (chkNeedsCycloRefraction_LeftEye.Checked == true)
                    {
                        iNeedsCycloRefraction_LeftEye = 1;
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

                    string strAchromatopsia = string.Empty;

                    for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                    {
                        if (chkAchromatopsia.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strAchromatopsia += chkAchromatopsia.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strAchromatopsia = strAchromatopsia.TrimEnd(',');

                    int iDouchrome = 0;
                    try { iDouchrome = int.Parse(rdoDouchrome.SelectedValue); }
                    catch { iDouchrome = 0; }

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

                    int iHirchberg_Distance = 0;
                    try { iHirchberg_Distance = int.Parse(rdoHirchberg_Distance.SelectedValue); }
                    catch { iHirchberg_Distance = 0; }

                    int iHirchberg_Near = 0;
                    try { iHirchberg_Near = int.Parse(rdoHirchberg_Near.SelectedValue); }
                    catch { iHirchberg_Near = 0; }

                    int iOphthalmoscope_RightEye = 0;
                    try { iOphthalmoscope_RightEye = int.Parse(rdoOphthalmoscope_RightEye.SelectedValue); }
                    catch { iOphthalmoscope_RightEye = 0; }

                    int iPupillaryReactions_RightEye = 0;
                    try { iPupillaryReactions_RightEye = int.Parse(rdoPupillaryReactions_RightEye.SelectedValue); }
                    catch { iPupillaryReactions_RightEye = 0; }

                    int iCoverUncovertTest_RightEye = 0;
                    try { iCoverUncovertTest_RightEye = int.Parse(rdoCoverUncovertTest_RightEye.SelectedValue); }
                    catch { iCoverUncovertTest_RightEye = 0; }

                    int iOphthalmoscope_LeftEye = 0;
                    try { iOphthalmoscope_LeftEye = int.Parse(rdoOphthalmoscope_LeftEye.SelectedValue); }
                    catch { iOphthalmoscope_LeftEye = 0; }

                    int iPupillaryReactions_LeftEye = -1;
                    //try { iPupillaryReactions_LeftEye = int.Parse(rdoPupillaryReactions_LeftEye.SelectedValue); }
                    //catch { iPupillaryReactions_LeftEye = 0; }

                    int iCoverUncovertTest_LeftEye = 0;
                    try { iCoverUncovertTest_LeftEye = int.Parse(rdoCoverUncovertTest_LeftEye.SelectedValue); }
                    catch { iCoverUncovertTest_LeftEye = 0; }

                    // Diagnosis - Treatment
                    //string strDiagnosis = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis = strDiagnosis.TrimEnd(',');
                    //if (strDiagnosis.Trim() == "") { strDiagnosis = "0"; }

                    //int iSubDiagnosis_RightEye = -1;
                    //if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    //{
                    //    iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    //}

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

                    int iSubDiagnosis_RightEye = -1;
                    if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    {
                        iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    }

                    int iSubDiagnosis_LeftEye = -1;
                    if (rdoSubDiagnosis_LeftEye.SelectedValue != "")
                    {
                        iSubDiagnosis_LeftEye = int.Parse(rdoSubDiagnosis_LeftEye.SelectedValue);
                    }

                    int iTreatment = 0;
                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }

                    //string strMedicine = string.Empty;

                    //for (int i = 0; i < chkMedicine.Items.Count; i++)
                    //{
                    //    if (chkMedicine.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strMedicine += chkMedicine.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strMedicine = strMedicine.TrimEnd(',');

                    int iFrequency = 0;
                    if (txtFrequency.Text != "")
                    {
                        iFrequency = int.Parse(txtFrequency.Text);
                    }

                    int iNextVisit = -1;
                    if (rdoNextVisit.SelectedValue != "")
                    {
                        iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                    }

                    int iSurgery = -1;
                    if (rdoSurgery.SelectedValue != "")
                    {
                        iSurgery = int.Parse(rdoSurgery.SelectedValue);
                    }

                    int iSurgery_Detail = -1;
                    if (rdoSurgery_Detail.SelectedValue != "")
                    {
                        iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                    }

                    int iReferal = -1;
                    if (rdoReferal.SelectedValue != "")
                    {
                        iReferal = int.Parse(rdoReferal.SelectedValue);
                    }

                    DateTime dtTest;
                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        dtTest = DateTime.Parse(txtTestDate.Text);
                    }
                    else
                    {
                        try
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }
                        catch (Exception ex)
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitForFurtherAssessmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlDoctor.SelectedValue),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //strDiagnosis, txtDiagnosis_RightEye.Text,iSubDiagnosis_RightEye, 
                        iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text, iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye,
                        iTreatment, iTreatment_Glasses, txtMedicine.Text, iFrequency, iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                        strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        if (!(txtMedicine.Text.Trim() == "" && txtFrequency.Text.Trim() == ""))
                        {
                            var resDetail = dx.sp_tblMedicineFurtherAssessmentStudent_InsertUpdate(0, Convert.ToInt32(res.VisitForFurtherAssessmentStudentId),
                                                                                             Convert.ToInt32(hfStudentIDPKID.Value), txtMedicine.Text,
                                                                                             int.Parse(txtFrequency.Text)).FirstOrDefault();
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
                    int iGlassType_RightEye = 0;
                    iGlassType_RightEye = 0;
                    int iGlassType_LeftEye = 0;
                    iGlassType_LeftEye = 0;

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

                    int iNeedsCycloRefraction_RightEye = 0;
                    if (chkNeedsCycloRefraction_RightEye.Checked == true)
                    {
                        iNeedsCycloRefraction_RightEye = 1;
                    }

                    int iNeedsCycloRefraction_LeftEye = 0;
                    if (chkNeedsCycloRefraction_LeftEye.Checked == true)
                    {
                        iNeedsCycloRefraction_LeftEye = 1;
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

                    string strAchromatopsia = string.Empty;

                    for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                    {
                        if (chkAchromatopsia.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strAchromatopsia += chkAchromatopsia.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strAchromatopsia = strAchromatopsia.TrimEnd(',');

                    int iDouchrome = 0;
                    try { iDouchrome = int.Parse(rdoDouchrome.SelectedValue); }
                    catch { iDouchrome = 0; }

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

                    int iHirchberg_Distance = 0;
                    try { iHirchberg_Distance = int.Parse(rdoHirchberg_Distance.SelectedValue); }
                    catch { iHirchberg_Distance = 0; }

                    int iHirchberg_Near = 0;
                    try { iHirchberg_Near = int.Parse(rdoHirchberg_Near.SelectedValue); }
                    catch { iHirchberg_Near = 0; }

                    int iOphthalmoscope_RightEye = 0;
                    try { iOphthalmoscope_RightEye = int.Parse(rdoOphthalmoscope_RightEye.SelectedValue); }
                    catch { iOphthalmoscope_RightEye = 0; }

                    int iPupillaryReactions_RightEye = 0;
                    try { iPupillaryReactions_RightEye = int.Parse(rdoPupillaryReactions_RightEye.SelectedValue); }
                    catch { iPupillaryReactions_RightEye = 0; }

                    int iCoverUncovertTest_RightEye = 0;
                    try { iCoverUncovertTest_RightEye = int.Parse(rdoCoverUncovertTest_RightEye.SelectedValue); }
                    catch { iCoverUncovertTest_RightEye = 0; }

                    int iOphthalmoscope_LeftEye = 0;
                    try { iOphthalmoscope_LeftEye = int.Parse(rdoOphthalmoscope_LeftEye.SelectedValue); }
                    catch { iOphthalmoscope_LeftEye = 0; }

                    int iPupillaryReactions_LeftEye = -1;
                    //try { iPupillaryReactions_LeftEye = int.Parse(rdoPupillaryReactions_LeftEye.SelectedValue); }
                    //catch { iPupillaryReactions_LeftEye = 0; }

                    int iCoverUncovertTest_LeftEye = 0;
                    try { iCoverUncovertTest_LeftEye = int.Parse(rdoCoverUncovertTest_LeftEye.SelectedValue); }
                    catch { iCoverUncovertTest_LeftEye = 0; }

                    // Diagnosis - Treatment
                    //string strDiagnosis = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis = strDiagnosis.TrimEnd(',');
                    //if (strDiagnosis.Trim() == "") { strDiagnosis = "0"; }

                    //int iSubDiagnosis_RightEye = -1;
                    //if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    //{
                    //    iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    //}

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

                    int iSubDiagnosis_RightEye = -1;
                    if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    {
                        iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    }

                    int iSubDiagnosis_LeftEye = -1;
                    if (rdoSubDiagnosis_LeftEye.SelectedValue != "")
                    {
                        iSubDiagnosis_LeftEye = int.Parse(rdoSubDiagnosis_LeftEye.SelectedValue);
                    }

                    int iTreatment = 0;

                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }

                    //string strMedicine = string.Empty;

                    //for (int i = 0; i < chkMedicine.Items.Count; i++)
                    //{
                    //    if (chkMedicine.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strMedicine += chkMedicine.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strMedicine = strMedicine.TrimEnd(',');

                    int iFrequency = 0;
                    if (txtFrequency.Text != "")
                    {
                        iFrequency = int.Parse(txtFrequency.Text);
                    }

                    int iNextVisit = -1;
                    if (rdoNextVisit.SelectedValue != "")
                    {
                        iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                    }

                    int iSurgery = -1;
                    if (rdoSurgery.SelectedValue != "")
                    {
                        iSurgery = int.Parse(rdoSurgery.SelectedValue);
                    }

                    int iSurgery_Detail = -1;
                    if (rdoSurgery_Detail.SelectedValue != "")
                    {
                        iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                    }

                    int iReferal = -1;
                    if (rdoReferal.SelectedValue != "")
                    {
                        iReferal = int.Parse(rdoReferal.SelectedValue);
                    }

                    DateTime dtTest;
                    if (hfAutoRefTestIDPKID.Value == "0")
                    {
                        dtTest = DateTime.Parse(txtTestDate.Text);
                    }
                    else
                    {
                        try
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }
                        catch (Exception ex)
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitForFurtherAssessmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlDoctor.SelectedValue),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //strDiagnosis, txtDiagnosis_RightEye.Text, iSubDiagnosis_RightEye, 
                        iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text, iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye,
                        iTreatment, iTreatment_Glasses, txtMedicine.Text, iFrequency, iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                        strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        lbl_error.Text = res.RetMessage;

                        if (!(txtMedicine.Text.Trim() == "" && txtFrequency.Text.Trim() == ""))
                        {
                            var resDetail = dx.sp_tblMedicineFurtherAssessmentStudent_InsertUpdate(0, Convert.ToInt32(res.VisitForFurtherAssessmentStudentId),
                                                                                             Convert.ToInt32(hfStudentIDPKID.Value), txtMedicine.Text,
                                                                                             int.Parse(txtFrequency.Text)).FirstOrDefault();
                        }

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
                    if (rdoType.SelectedValue == "0")
                    {
                        var res = dx.sp_tblVisitForFurtherAssessmentStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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
                        var res = dx.sp_tblOptometristMasterTeacher_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            if (txtStudentCode.Text.Trim() == "")
            {
                lbl_error.Text = "Student Code is required.";
                txtStudentCode.Focus();
                return false;
            }
            // Invalid Code - Database Validation for Student / Teacher
            //if (txtTeacherCode.Text.Trim() == "")
            //{
            //    lbl_error.Text = "Teacher Code is required.";
            //    txtTeacherCode.Focus();
            //    return false;
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

            if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "")
            {
                lbl_error.Text = "Distance Vision Right Eye (Unaided) is required.";
                rdoDistanceVision_RightEye_Unaided.Focus();
                return false;
            }

            if (rdoDistanceVision_RightEye_WithGlasses.Enabled == true)
            {
                if (rdoDistanceVision_RightEye_WithGlasses.SelectedValue == "")
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

            if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "")
            {
                lbl_error.Text = "Distance Vision Left Eye (Unaided) is required.";
                rdoDistanceVision_LeftEye_Unaided.Focus();
                return false;
            }

            if (rdoDistanceVision_LeftEye_WithGlasses.Enabled == true)
            {
                if (rdoDistanceVision_LeftEye_WithGlasses.SelectedValue == "")
                {
                    lbl_error.Text = "Distance Vision Left Eye (With glasses) is required.";
                    rdoDistanceVision_LeftEye_WithGlasses.Focus();
                    return false;
                }
            }

            //if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue == "-1" || rdoDistanceVision_LeftEye_Pinhole.SelectedValue == "")
            //{
            //    lbl_error.Text = "Distance Vision Left Eye (Pin Hole) is required.";
            //    rdoDistanceVision_LeftEye_Pinhole.Focus();
            //    return false;
            //}

            if (rdoOptometristTest.SelectedValue == "2")
            {
                if (txtSpherical_RightEye.Text == "")
                {
                    lbl_error.Text = "Spherical (RightEye) is required.";
                    txtSpherical_RightEye.Focus();
                    return false;
                }

                if (txtCyclinderical_RightEye.Text == "")
                {
                    lbl_error.Text = "Cyclinderical (RightEye) is required.";
                    txtCyclinderical_RightEye.Focus();
                    return false;
                }

                if (txtAxixA_RightEye.Text == "")
                {
                    lbl_error.Text = "Axix (RightEye) is required.";
                    txtAxixA_RightEye.Focus();
                    return false;
                }

                if (txtNear_RightEye.Text == "")
                {
                    lbl_error.Text = "Near Add (RightEye) is required.";
                    txtNear_RightEye.Focus();
                    return false;
                }

                if (txtSpherical_LeftEye.Text == "")
                {
                    lbl_error.Text = "Spherical (LeftEye) is required.";
                    txtSpherical_LeftEye.Focus();
                    return false;
                }

                if (txtCyclinderical_LeftEye.Text == "")
                {
                    lbl_error.Text = "Cyclinderical (LeftEye) is required.";
                    txtCyclinderical_LeftEye.Focus();
                    return false;
                }

                if (txtAxixA_LeftEye.Text == "")
                {
                    lbl_error.Text = "Axix (LeftEye) is required.";
                    txtAxixA_LeftEye.Focus();
                    return false;
                }

                if (txtNear_LeftEye.Text == "")
                {
                    lbl_error.Text = "Near Add (LeftEye) is required.";
                    txtNear_LeftEye.Focus();
                    return false;
                }

                if (txtSpherical_RightEye.Text.Trim() == "")
                {
                    txtSpherical_RightEye.Text = "0.00";
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
                    txtCyclinderical_RightEye.Text = "0.00";
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
                    txtAxixA_RightEye.Text = "0";
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

                if (txtNear_RightEye.Text.Trim() == "")
                {
                    txtNear_RightEye.Text = "0.00";
                }

                try
                {
                    decimal d = decimal.Parse(txtNear_RightEye.Text.Trim());
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Invalid Near Right Eye Points.";
                    txtNear_RightEye.Focus();
                    return false;
                }

                if (txtSpherical_LeftEye.Text.Trim() == "")
                {
                    txtSpherical_LeftEye.Text = "0.00";
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
                    txtCyclinderical_LeftEye.Text = "0.00";
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

                if (txtAxixA_LeftEye.Text.Trim() == "")
                {
                    txtAxixA_LeftEye.Text = "0";
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
            }

            if (rdoOptometristTest.SelectedValue == "4")
            {
                if (txtExtraOccularMuscleRemarks_RightEye.Text == "")
                {
                    lbl_error.Text = "Extra Occular Muscle Remarks is required.";
                    txtExtraOccularMuscleRemarks_RightEye.Focus();
                    return false;
                }

                if (txtExtraOccularMuscleRemarks_LeftEye.Text == "")
                {
                    lbl_error.Text = "Extra Occular Muscle Remarks is required.";
                    txtExtraOccularMuscleRemarks_LeftEye.Focus();
                    return false;
                }
            }

            if (rdoOptometristTest.SelectedValue == "5")
            {
                if (chkNormal_RightEye.Checked == false &&
                chkNormal_LeftEye.Checked == false &&
                chkRefractiveError_RightEye.Checked == false &&
                chkRefractiveError_LeftEye.Checked == false &&
                chkLowVision_RightEye.Checked == false &&
                chkLowVision_LeftEye.Checked == false &&
                chkNeedsCycloplegicRefraction_RightEye.Checked == false &&
                chkNeedsCycloplegicRefraction_LeftEye.Checked == false &&
                chkSquintStrabismus_RightEye.Checked == false &&
                chkSquintStrabismus_LeftEye.Checked == false &&
                chkLazyEyeAmblyopia_RightEye.Checked == false &&
                chkLazyEyeAmblyopia_LeftEye.Checked == false &&
                chkColorblindnessAchromatopsia_RightEye.Checked == false &&
                chkColorblindnessAchromatopsia_LeftEye.Checked == false &&
                chkCataract_RightEye.Checked == false &&
                chkCataract_LeftEye.Checked == false &&
                chkTraumaticCataract_RightEye.Checked == false &&
                chkTraumaticCataract_LeftEye.Checked == false &&
                chkKeratoconus_RightEye.Checked == false &&
                chkKeratoconus_LeftEye.Checked == false &&
                chkAnisometropia_RightEye.Checked == false &&
                chkAnisometropia_LeftEye.Checked == false &&
                chkPtosis_RightEye.Checked == false &&
                chkPtosis_LeftEye.Checked == false &&
                chkNystagmus_RightEye.Checked == false &&
                chkNystagmus_LeftEye.Checked == false &&
                chkCorneadefects_RightEye.Checked == false &&
                chkCorneadefects_LeftEye.Checked == false &&
                chkPuplidefects_RightEye.Checked == false &&
                chkPuplidefects_LeftEye.Checked == false &&
                chkPterygium_RightEye.Checked == false &&
                chkPterygium_LeftEye.Checked == false &&
                chkOther_RightEye.Checked == false &&
                chkOther_LeftEye.Checked == false)
                {
                    lbl_error.Text = "'Diagnosis' is required.";
                    txtStudentName.Focus();
                    return false;
                }

                //string strDiagnosis = string.Empty;

                //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                //{
                //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                //    {
                //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                //    }
                //}
                //strDiagnosis = strDiagnosis.TrimEnd(',');
                //if (strDiagnosis.Trim() == "")
                //{
                //    lbl_error.Text = "'Diagnosis' is required.";
                //    txtStudentName.Focus();
                //    return false;
                //}
            }

            return true;
        }

        private bool ValidateInputTeacher()
        {
            ClearValidation();
            //if (txtStudentCode.Text.Trim() == "")
            //{
            //    lbl_error.Text = "Student Code is required.";
            //    txtTeacherCode.Focus();
            //    return false;
            //}
            // Invalid Code - Database Validation for Student / Teacher
            var dtLastData = dx.sp_tblOptometristMasterTeacher_GetLastTest(Convert.ToInt32(hfTeacherIDPKID.Value)).SingleOrDefault();
            if (dtLastData == null)
            {
                lbl_error.Text = "'Auto Refraction Test' not conducted for this Teacher. Please perform 'Auto Refraction Test'.";
                return false;
            }

            if (txtTeacherCode.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Code is required.";
                txtTeacherCode.Focus();
                return false;
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

            //if (rdoDistanceVision_LeftEye_Pinhole.SelectedValue == "-1" || rdoDistanceVision_LeftEye_Pinhole.SelectedValue == "")
            //{
            //    lbl_error.Text = "Distance Vision Left Eye (Pin Hole) is required.";
            //    rdoDistanceVision_LeftEye_Pinhole.Focus();
            //    return false;
            //}

            if (rdoOptometristTest.SelectedValue == "2")
            {
                if (txtSpherical_RightEye.Text == "")
                {
                    lbl_error.Text = "Spherical (RightEye) is required.";
                    txtSpherical_RightEye.Focus();
                    return false;
                }

                if (txtCyclinderical_RightEye.Text == "")
                {
                    lbl_error.Text = "Cyclinderical (RightEye) is required.";
                    txtCyclinderical_RightEye.Focus();
                    return false;
                }

                if (txtAxixA_RightEye.Text == "")
                {
                    lbl_error.Text = "Axix (RightEye) is required.";
                    txtAxixA_RightEye.Focus();
                    return false;
                }

                if (txtNear_RightEye.Text == "")
                {
                    lbl_error.Text = "Near Add (RightEye) is required.";
                    txtNear_RightEye.Focus();
                    return false;
                }

                if (txtSpherical_LeftEye.Text == "")
                {
                    lbl_error.Text = "Spherical (LeftEye) is required.";
                    txtSpherical_LeftEye.Focus();
                    return false;
                }

                if (txtCyclinderical_LeftEye.Text == "")
                {
                    lbl_error.Text = "Cyclinderical (LeftEye) is required.";
                    txtCyclinderical_LeftEye.Focus();
                    return false;
                }

                if (txtAxixA_LeftEye.Text == "")
                {
                    lbl_error.Text = "Axix (LeftEye) is required.";
                    txtAxixA_LeftEye.Focus();
                    return false;
                }

                if (txtNear_LeftEye.Text == "")
                {
                    lbl_error.Text = "Near Add (LeftEye) is required.";
                    txtNear_LeftEye.Focus();
                    return false;
                }

                if (txtSpherical_RightEye.Text.Trim() == "")
                {
                    txtSpherical_RightEye.Text = "0.00";
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
                    txtCyclinderical_RightEye.Text = "0.00";
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
                    txtAxixA_RightEye.Text = "0";
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

                if (txtNear_RightEye.Text.Trim() == "")
                {
                    txtNear_RightEye.Text = "0.00";
                }

                try
                {
                    decimal d = decimal.Parse(txtNear_RightEye.Text.Trim());
                }
                catch (Exception ex)
                {
                    lbl_error.Text = "Invalid Near Right Eye Points.";
                    txtNear_RightEye.Focus();
                    return false;
                }

                if (txtSpherical_LeftEye.Text.Trim() == "")
                {
                    txtSpherical_LeftEye.Text = "0.00";
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
                    txtCyclinderical_LeftEye.Text = "0.00";
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

                if (txtAxixA_LeftEye.Text.Trim() == "")
                {
                    txtAxixA_LeftEye.Text = "0";
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
            }

            if (rdoOptometristTest.SelectedValue == "4")
            {
                if (txtExtraOccularMuscleRemarks_RightEye.Text == "")
                {
                    lbl_error.Text = "Extra Occular Muscle Remarks is required.";
                    txtExtraOccularMuscleRemarks_RightEye.Focus();
                    return false;
                }

                if (txtExtraOccularMuscleRemarks_LeftEye.Text == "")
                {
                    lbl_error.Text = "Extra Occular Muscle Remarks is required.";
                    txtExtraOccularMuscleRemarks_LeftEye.Focus();
                    return false;
                }
            }



            return true;
        }
        private void ClearForm()
        {
            InitForm();

            rdoOptometristTest.SelectedValue = "1";
            rdoOptometristTest_SelectedIndexChanged(null, null);

            hfAutoRefTestIDPKID.Value = "0";
            hfStudentIDPKID.Value = "0";
            hfTeacherIDPKID.Value = "0";

            txtTestDate.Text = Utilities.GetDate();

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
            lblGender_Teacher.Text = "";
            lblSchoolName_Teacher.Text = "";

            rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
            rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
            rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

            rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
            rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
            rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

            rdoNearVision_RightEye.SelectedValue = "0";
            rdoNearVision_LeftEye.SelectedValue = "0";

            chkNeedsCycloRefraction_LeftEye.Checked = false;
            txtNeedsCycloRefraction_LeftEye.Text = "";

            chkNeedsCycloRefraction_RightEye.Checked = false;
            txtNeedsCycloRefraction_RightEye.Text = "";

            ddlSpherical_RightEye.SelectedIndex = 0;
            txtSpherical_RightEye.Text = "";

            ddlCyclinderical_RightEye.SelectedIndex = 0;
            txtCyclinderical_RightEye.Text = "";

            txtAxixA_RightEye.Text = "";
            txtAxixB_RightEye.Text = "";

            ddlNear_RightEye.SelectedIndex = 0;
            txtNear_RightEye.Text = "00.00";

            ddlSpherical_LeftEye.SelectedIndex = 0;
            txtSpherical_LeftEye.Text = "";

            ddlCyclinderical_LeftEye.SelectedIndex = 0;
            txtCyclinderical_LeftEye.Text = "";

            txtAxixA_LeftEye.Text = "";
            txtAxixB_LeftEye.Text = "";

            ddlNear_LeftEye.SelectedIndex = 0;
            txtNear_LeftEye.Text = "00.00";

            chkAchromatopsia.ClearSelection();
            chkAchromatopsia.Items[0].Selected = true;
            //rdoAchromatopsia.SelectedValue = "0";
            rdoDouchrome.SelectedValue = "2";

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

            rdoHirchberg_Distance.SelectedValue = "0";
            rdoHirchberg_Near.SelectedValue = "0";

            rdoOphthalmoscope_RightEye.SelectedValue = "0";
            rdoPupillaryReactions_RightEye.SelectedValue = "0";
            rdoCoverUncovertTest_RightEye.SelectedValue = "0";
            txtCoverUncovertTestRemarks_RightEye.Text = "";

            rdoOphthalmoscope_LeftEye.SelectedValue = "0";
            rdoPupillaryReactions_LeftEye.SelectedValue = "0";
            rdoCoverUncovertTest_LeftEye.SelectedValue = "0";
            txtCoverUncovertTestRemarks_LeftEye.Text = "";

            txtExtraOccularMuscleRemarks_RightEye.Text = "";
            txtExtraOccularMuscleRemarks_LeftEye.Text = "";

            txtMedicine.Text = "";
            txtFrequency.Text = "";
            //rdoDiagnosis_RightEye.SelectedIndex = -1;
            //rdoDiagnosis_RightEye.SelectedValue = "-1";

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

            txtDiagnosis_RightEye.Text = "";
            txtDiagnosis_LeftEye.Text = "";

            rdoSubDiagnosis_RightEye.SelectedIndex = -1;
            rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
            //rdoSubDiagnosis_RightEye.SelectedValue = "-1";
            rdoTreatment_Glasses.SelectedIndex = -1;
            rdoNextVisit.SelectedIndex = -1;
            rdoSurgery.SelectedIndex = -1;
            rdoReferal.SelectedIndex = -1;

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

                var dt = dx.sp_tblVisitForFurtherAssessmentStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                hfAutoRefTestTransDate.Value = dt.VisitForFurtherAssessmentStudentTransDate.ToString(); //   AutoRefStudentTransDate.ToString();

                txtTestDate.Text = DateTime.Parse(hfAutoRefTestTransDate.Value).ToString("dd-MMM-yyyy");

                ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                ddlDoctor.SelectedValue = dt.DoctorAutoId.ToString();

                if (dt.DistanceVision_RightEye_Unaided.ToString() == "-1")
                {
                    rdoDistanceVision_RightEye_Unaided.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_RightEye_Unaided.SelectedValue = dt.DistanceVision_RightEye_Unaided.ToString();
                }

                if (dt.DistanceVision_RightEye_WithGlasses.ToString() == "-1")
                {
                    rdoDistanceVision_RightEye_WithGlasses.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_RightEye_WithGlasses.SelectedValue = dt.DistanceVision_RightEye_WithGlasses.ToString();
                }

                if (dt.DistanceVision_RightEye_PinHole.ToString() == "-1")
                {
                    rdoDistanceVision_RightEye_PinHole.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_RightEye_PinHole.SelectedValue = dt.DistanceVision_RightEye_PinHole.ToString();
                }

                if (dt.NearVision_RightEye.ToString() == "-1")
                {
                    rdoNearVision_RightEye.SelectedValue = "-1";
                }
                else
                {
                    rdoNearVision_RightEye.SelectedValue = dt.NearVision_RightEye.ToString();
                }

                int iNeedCycloRefraction_RightEye = Convert.ToInt32(dt.NeedCycloRefraction_RightEye.ToString());
                if (iNeedCycloRefraction_RightEye == 1) { chkNeedsCycloRefraction_RightEye.Checked = true; }
                else { chkNeedsCycloRefraction_RightEye.Checked = false; }
                txtNeedsCycloRefraction_RightEye.Text = dt.NeedCycloRefractionRemarks_RightEye;


                if (dt.DistanceVision_LeftEye_Unaided.ToString() == "-1")
                {
                    rdoDistanceVision_LeftEye_Unaided.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_LeftEye_Unaided.SelectedValue = dt.DistanceVision_LeftEye_Unaided.ToString();
                }

                if (dt.DistanceVision_LeftEye_WithGlasses.ToString() == "-1")
                {
                    rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = dt.DistanceVision_LeftEye_WithGlasses.ToString();
                }

                if (dt.DistanceVision_LeftEye_PinHole.ToString() == "-1")
                {
                    rdoDistanceVision_LeftEye_Pinhole.SelectedValue = "-1";
                }
                else
                {
                    rdoDistanceVision_LeftEye_Pinhole.SelectedValue = dt.DistanceVision_LeftEye_PinHole.ToString();
                }

                if (dt.NearVision_LeftEye.ToString() == "-1")
                {
                    rdoNearVision_LeftEye.SelectedValue = "-1";
                }
                else
                {
                    rdoNearVision_LeftEye.SelectedValue = dt.NearVision_LeftEye.ToString();
                }

                int iNeedCycloRefraction_LeftEye = Convert.ToInt32(dt.NeedCycloRefraction_LeftEye.ToString());
                if (iNeedCycloRefraction_LeftEye == 1) { chkNeedsCycloRefraction_LeftEye.Checked = true; }
                else { chkNeedsCycloRefraction_LeftEye.Checked = false; }
                txtNeedsCycloRefraction_LeftEye.Text = dt.NeedCycloRefractionRemarks_LeftEye;

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

                if (decimal.Parse(dt.Right_Spherical_Points.ToString()) == 0)
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

                if (decimal.Parse(dt.Right_Cyclinderical_Points.ToString()) == 0)
                {
                    txtCyclinderical_RightEye.Text = "";
                }
                else
                {
                    txtCyclinderical_RightEye.Text = dt.Right_Cyclinderical_Points.ToString();
                }

                if (int.Parse(dt.Right_Axix_From.ToString()) == 0)
                {
                    txtAxixA_RightEye.Text = "";
                }
                else
                {
                    txtAxixA_RightEye.Text = dt.Right_Axix_From.ToString();
                }
                txtAxixB_RightEye.Text = dt.Right_Axix_To.ToString();

                if (dt.Right_Near_Status == "P")
                {
                    ddlNear_RightEye.SelectedIndex = 0;
                }
                else
                {
                    ddlNear_RightEye.SelectedIndex = 1;
                }

                if (decimal.Parse(dt.Right_Near_Points.ToString()) == 0)
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
                if (decimal.Parse(dt.Left_Spherical_Points.ToString()) == 0)
                {
                    txtSpherical_LeftEye.Text = "";
                }
                else
                {
                    txtSpherical_LeftEye.Text = dt.Left_Spherical_Points.ToString();
                }

                if (dt.Left_Cyclinderical_Status == "P")
                {
                    ddlCyclinderical_LeftEye.SelectedIndex = 0;
                }
                else
                {
                    ddlCyclinderical_LeftEye.SelectedIndex = 1;
                }

                if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) == 0)
                {
                    txtCyclinderical_LeftEye.Text = "";
                }
                else
                {
                    txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();
                }

                if (int.Parse(dt.Left_Axix_From.ToString()) == 0)
                {
                    txtAxixA_LeftEye.Text = "";
                }
                else
                {
                    txtAxixA_LeftEye.Text = dt.Left_Axix_From.ToString();
                }
                txtAxixB_LeftEye.Text = dt.Left_Axix_To.ToString();

                if (dt.Left_Near_Status == "P")
                {
                    ddlNear_LeftEye.SelectedIndex = 0;
                }
                else
                {
                    ddlNear_LeftEye.SelectedIndex = 1;
                }

                if (decimal.Parse(dt.Left_Near_Points.ToString()) == 0)
                {
                    txtNear_LeftEye.Text = "";
                }
                else
                {
                    txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();
                }

                string strAchromatopsia = dt.Achromatopsia.ToString();
                string s = strAchromatopsia;

                string[] items = s.Split(',');
                for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                {
                    if (items.Contains(chkAchromatopsia.Items[i].Value))
                    {
                        chkAchromatopsia.Items[i].Selected = true;
                    }
                }

                rdoDouchrome.SelectedValue = dt.Douchrome.ToString();
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

                txtCondition_LeftEye.Text = dt.Condition_LeftEye.ToString();
                txtMeridian1_LeftEye.Text = dt.Meridian1_LeftEye.ToString();
                txtMeridian2_LeftEye.Text = dt.Meridian2_LeftEye.ToString();
                txtFinalPrescription_LeftEye.Text = dt.FinalPrescription_LeftEye.ToString();

                rdoHirchberg_Distance.SelectedValue = dt.Hirchberg_Distance.ToString();
                rdoHirchberg_Near.SelectedValue = dt.Hirchberg_Near.ToString();

                rdoOphthalmoscope_RightEye.SelectedValue = dt.Ophthalmoscope_RightEye.ToString();
                rdoPupillaryReactions_RightEye.SelectedValue = dt.PupillaryReactions_RightEye.ToString();

                rdoCoverUncovertTest_RightEye.SelectedValue = dt.CoverUncovertTest_RightEye.ToString();
                txtCoverUncovertTestRemarks_RightEye.Text = dt.CoverUncovertTestRemarks_RightEye.ToString();

                txtExtraOccularMuscleRemarks_RightEye.Text = dt.ExtraOccularMuscleRemarks_RightEye.ToString();
                txtExtraOccularMuscleRemarks_LeftEye.Text = dt.ExtraOccularMuscleRemarks_LeftEye.ToString();

                rdoOphthalmoscope_LeftEye.SelectedValue = dt.Ophthalmoscope_LeftEye.ToString();
                //rdoPupillaryReactions_LeftEye.SelectedValue = dt.PupillaryReactions_LeftEye.ToString();

                rdoCoverUncovertTest_LeftEye.SelectedValue = dt.CoverUncovertTest_LeftEye.ToString();
                txtCoverUncovertTestRemarks_LeftEye.Text = dt.CoverUncovertTestRemarks_LeftEye.ToString();

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


                //if (dt.SubDaignosis.ToString() != "-1")
                //{
                //    rdoSubDiagnosis_RightEye.SelectedValue = dt.SubDaignosis.ToString();
                //}
                //else
                //{
                //    rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                //}
                //txtDiagnosis_RightEye.Text = dt.DaignosisRemarks.ToString();

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

                if (dt.SubDaignosis.ToString() != "-1")
                {
                    rdoSubDiagnosis_RightEye.SelectedValue = dt.SubDaignosis.ToString();
                }
                else
                {
                    rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                }

                if (dt.SubDaignosis_LeftEye.ToString() != "-1")
                {
                    rdoSubDiagnosis_LeftEye.SelectedValue = dt.SubDaignosis_LeftEye.ToString();
                }
                else
                {
                    rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
                }

                txtDiagnosis_RightEye.Text = dt.DaignosisRemarks.ToString();
                txtDiagnosis_LeftEye.Text = dt.DaignosisRemarks_LeftEye.ToString();

                if (dt.SubTreatment.ToString() != "-1")
                {
                    rdoTreatment_Glasses.SelectedValue = dt.SubTreatment.ToString();
                }
                else
                {
                    rdoTreatment_Glasses.SelectedIndex = -1;
                }

                //txtMedicine.Text = dt.Medicine.ToString();
                //txtFrequency.Text = dt.Frequency.ToString();

                BindGrid();

                if (dt.NextVisit.ToString() != "-1")
                {
                    rdoNextVisit.SelectedValue = dt.NextVisit.ToString();
                    rdoNextVisit_SelectedIndexChanged(null, null);
                }
                else
                {
                    rdoNextVisit.SelectedIndex = -1;
                }

                if (dt.Surgery.ToString() != "-1")
                {
                    rdoSurgery.SelectedValue = dt.Surgery.ToString();
                }
                else
                {
                    rdoSurgery.SelectedIndex = -1;
                }

                rdoSurgery_SelectedIndexChanged(null, null);
                if (dt.SurgeryDetail.ToString() != "-1")
                {
                    rdoSurgery_Detail.SelectedValue = dt.SurgeryDetail.ToString();
                }
                else
                {
                    rdoSurgery_Detail.SelectedIndex = -1;
                }

                txtSurgery_Detail.Text = dt.SurgeryDetailRemarks.ToString();

                if (dt.Referal.ToString() != "-1")
                {
                    rdoReferal.SelectedValue = dt.Referal.ToString();
                }
                else
                {
                    rdoReferal.SelectedIndex = -1;
                }

                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        protected void btnLookupStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int iType = int.Parse(rdoOldNewTest.SelectedValue.ToString());
                DataTable data = (from a in dx.sp_GetLookupData_Student_FurtherAssessment(0, 0, iType)
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

                    if (dt.WearGlasses == 0)
                    {
                        rdoDistanceVision_RightEye_WithGlasses.Enabled = false;
                        rdoDistanceVision_LeftEye_WithGlasses.Enabled = false;
                    }
                    else
                    {
                        rdoDistanceVision_RightEye_WithGlasses.Enabled = true;
                        rdoDistanceVision_LeftEye_WithGlasses.Enabled = true;
                    }

                    var dtPreviousData = dx.sp_tblVisitForFurtherAssessmentStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "VisitForFurtherAssessmentStudentId";
                            ddlPreviousTestResult.DataTextField = "VisitForFurtherAssessmentStudentTransDate";
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
                    //btnEdit.Visible = true;
                    //btnDelete.Visible = true;
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
                DataTable data = (from a in dx.sp_GetLookupData_Teacher(0)
                                  select a).ToList().ToDataTable();

                hfLookupResultTeacher.Value = "0";
                Session["lookupData"] = data;

                Session["Code"] = "Teacher Code";
                Session["Name"] = "Teacher Name";
                Session["Description"] = "School Name";

                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControlMultiColumn.aspx?winTitle=Select User&hfName=" + hfLookupResultTeacher.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
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

                    if (dt.WearGlasses == 0)
                    {
                        rdoDistanceVision_RightEye_WithGlasses.Enabled = false;
                        rdoDistanceVision_LeftEye_WithGlasses.Enabled = false;
                    }
                    else
                    {
                        rdoDistanceVision_RightEye_WithGlasses.Enabled = true;
                        rdoDistanceVision_LeftEye_WithGlasses.Enabled = true;
                    }

                    var dtPreviousData = dx.sp_tblOptometristMasterTeacher_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "OptometristTeacherId";
                            ddlPreviousTestResult.DataTextField = "OptometristTeacherTransDate";
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
            if (rdoOldNewTest.SelectedValue == "0")
            {
                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                rdoType.Enabled = true;
            }
            else
            {
                txtTestDate.Visible = false;
                ddlPreviousTestResult.Visible = true;

                rdoType.Enabled = false;
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

        protected void rdoOptometristTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlStudent_Sub.Visible = false;
            pnlTeacher_Sub.Visible = false;

            pnlTestArea.Visible = false;
            pnlTest1_RightEye.Visible = false;
            pnlTest1_LeftEye.Visible = false;

            pnlTest2_RightEye.Visible = false;
            pnlTest2_LeftEye.Visible = false;

            pnlTest3_RightEye.Visible = false;
            pnlTest3_LeftEye.Visible = false;

            pnlOphthalmoscope_RightEye.Visible = false;
            pnlOphthalmoscope_LeftEye.Visible = false;

            pnlTest2b.Visible = false;
            pnlTest4a.Visible = false;
            pnlTest4e_RightEye.Visible = false;
            pnlTest4e_LeftEye.Visible = false;

            pnlDiagnosis.Visible = false;
            pnlTreatment.Visible = false;


            if (rdoOptometristTest.SelectedValue == "1")
            {
                pnlStudent_Sub.Visible = true;
                pnlTeacher_Sub.Visible = true;

                pnlTestArea.Visible = true;
                pnlTest1_RightEye.Visible = true;
                pnlTest1_LeftEye.Visible = true;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoDistanceVision_RightEye_Unaided.Focus();
            }
            else if (rdoOptometristTest.SelectedValue == "2")
            {
                pnlTest2_RightEye.Visible = true;
                pnlTest2_LeftEye.Visible = true;
                pnlTest2b.Visible = true;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                ddlSpherical_RightEye.Focus();
            }
            else if (rdoOptometristTest.SelectedValue == "3")
            {
                pnlTest3_RightEye.Visible = true;
                pnlTest3_LeftEye.Visible = true;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoRetinoScopy_RightEye.Focus();
            }
            else if (rdoOptometristTest.SelectedValue == "4")
            {
                pnlOphthalmoscope_RightEye.Visible = true;
                pnlOphthalmoscope_LeftEye.Visible = true;
                pnlTest4a.Visible = true;
                pnlTest4e_RightEye.Visible = true;
                pnlTest4e_LeftEye.Visible = true;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoHirchberg_Distance.Focus();
            }
            else if (rdoOptometristTest.SelectedValue == "5")
            {

                pnlDiagnosis.Visible = true;
                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();

                if (rdoOldNewTest.SelectedValue == "0")
                {
                    int iDistanceVision_RightEye = int.Parse(rdoDistanceVision_RightEye_Unaided.SelectedValue.ToString());
                    int iDistanceVision_LeftEye = int.Parse(rdoDistanceVision_LeftEye_Unaided.SelectedValue.ToString());

                    if (iDistanceVision_RightEye >= 3)
                    {
                        chkLowVision_RightEye.Checked = true;
                    }

                    if (iDistanceVision_LeftEye >= 3)
                    {
                        chkLowVision_LeftEye.Checked = true;
                    }

                    decimal dSRE = decimal.Parse(txtSpherical_RightEye.Text);
                    decimal dSLE = decimal.Parse(txtSpherical_LeftEye.Text);

                    decimal dCRE = decimal.Parse(txtCyclinderical_RightEye.Text);
                    decimal dCLE = decimal.Parse(txtCyclinderical_RightEye.Text);

                    if (dSRE > 0 || dCRE > 0)
                    {
                        //rdoDiagnosis_RightEye.Items[1].Selected = true;
                        chkRefractiveError_RightEye.Checked = true;
                    }

                    if (dSLE > 0 || dCLE > 0)
                    {
                        //rdoDiagnosis_RightEye.Items[1].Selected = true;
                        chkRefractiveError_LeftEye.Checked = true;
                    }
                }

            }
            else if (rdoOptometristTest.SelectedValue == "6")
            {
                pnlTreatment.Visible = true;
                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();

                if (rdoOldNewTest.SelectedValue == "0")
                {
                    string sSRS = ddlSpherical_RightEye.SelectedValue.ToString();
                    string sSLS = ddlSpherical_LeftEye.SelectedValue.ToString();

                    decimal dSRE = decimal.Parse(txtSpherical_RightEye.Text);
                    decimal dSLE = decimal.Parse(txtSpherical_LeftEye.Text);

                    string sCRS = ddlCyclinderical_RightEye.SelectedValue.ToString();
                    string sCLS = ddlCyclinderical_LeftEye.SelectedValue.ToString();

                    decimal dCRE = decimal.Parse(txtCyclinderical_RightEye.Text);
                    decimal dCLE = decimal.Parse(txtCyclinderical_RightEye.Text);

                    decimal dNLE = decimal.Parse(txtNear_RightEye.Text);
                    decimal dNRE = decimal.Parse(txtNear_LeftEye.Text);

                    if (dNRE > 0)
                    {
                        chkRefractiveError_RightEye.Checked = true;
                        rdoSubDiagnosis_RightEye.SelectedValue = "0";
                    }

                    if (dNLE > 0)
                    {
                        chkRefractiveError_LeftEye.Checked = true;
                        rdoSubDiagnosis_LeftEye.SelectedValue = "0";
                    }

                    if ((sSRS == "N") && (dSRE > 0))
                    {
                        chkRefractiveError_RightEye.Checked = true;
                        rdoSubDiagnosis_RightEye.SelectedValue = "1";
                    }

                    if ((sSLS == "N") && (dSLE > 0))
                    {
                        chkRefractiveError_LeftEye.Checked = true;
                        rdoSubDiagnosis_LeftEye.SelectedValue = "1";
                    }

                    if ((sSRS == "P") && (dSRE > 0))
                    {
                        chkRefractiveError_RightEye.Checked = true;
                        rdoSubDiagnosis_RightEye.SelectedValue = "2";
                    }

                    if ((sSLS == "P") && (dSLE > 0))
                    {
                        chkRefractiveError_LeftEye.Checked = true;
                        rdoSubDiagnosis_LeftEye.SelectedValue = "2";
                    }

                    if (dCRE > 0 || dCLE > 0)
                    {
                        chkRefractiveError_RightEye.Checked = true;
                        rdoSubDiagnosis_RightEye.SelectedValue = "3";
                    }

                    if (dCRE > 0 || dCLE > 0)
                    {
                        chkRefractiveError_LeftEye.Checked = true;
                        rdoSubDiagnosis_LeftEye.SelectedValue = "3";
                    }

                    enableCheckboxSelections();
                    //rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);

                }

            }
        }

        protected void ddlPreviousTestResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPreviousTestResult = ddlPreviousTestResult.SelectedValue;
            hfAutoRefTestIDPKID.Value = strPreviousTestResult;
            hfAutoRefTestIDPKID_ValueChanged(null, null);

            //InitForm();
        }

        protected void btnMoveNext_Click(object sender, EventArgs e)
        {
            try
            {
                string strLoginUserID = Utilities.GetLoginUserID();
                string strTerminalId = Utilities.getTerminalId();
                string strTerminalIP = Utilities.getTerminalIP();

                if (ValidateInputStudent())
                {
                    //var autoRefTransId = dx.sp_tblAutoRefTestStudent_GetMaxCode().SingleOrDefault();
                    //hfAutoRefTestTransID.Value = autoRefTransId;

                    int iGlassType_RightEye = 0;
                    iGlassType_RightEye = 0;
                    int iGlassType_LeftEye = 0;
                    iGlassType_LeftEye = 0;

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

                    int iNeedsCycloRefraction_RightEye = 0;
                    if (chkNeedsCycloRefraction_RightEye.Checked == true)
                    {
                        iNeedsCycloRefraction_RightEye = 1;
                    }

                    int iNeedsCycloRefraction_LeftEye = 0;
                    if (chkNeedsCycloRefraction_LeftEye.Checked == true)
                    {
                        iNeedsCycloRefraction_LeftEye = 1;
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

                    string strAchromatopsia = string.Empty;

                    for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                    {
                        if (chkAchromatopsia.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strAchromatopsia += chkAchromatopsia.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strAchromatopsia = strAchromatopsia.TrimEnd(',');

                    //int iAchromatopsia = 0;
                    //try { iAchromatopsia = int.Parse(rdoAchromatopsia.SelectedValue); }
                    //catch { iAchromatopsia = 0; }

                    int iDouchrome = 0;
                    try { iDouchrome = int.Parse(rdoDouchrome.SelectedValue); }
                    catch { iDouchrome = 0; }

                    int iRetinoScopy_RightEye = -1;
                    if (rdoRetinoScopy_RightEye.SelectedValue != "")
                    {
                        iRetinoScopy_RightEye = int.Parse(rdoRetinoScopy_RightEye.SelectedValue);
                    }
                    //int iRetinoScopy_RightEye = 0;
                    //try { iRetinoScopy_RightEye = int.Parse(rdoRetinoScopy_RightEye.SelectedValue); }
                    //catch { iRetinoScopy_RightEye = 0; }

                    //int iCycloplegicRefraction_RightEye = -1;
                    //if (rdoCycloplegicRefraction_RightEye.SelectedValue != "")
                    //{
                    //    iCycloplegicRefraction_RightEye = int.Parse(rdoCycloplegicRefraction_RightEye.SelectedValue);
                    //}

                    string strCycloplegicRefraction_RightEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_RightEye += rdoCycloplegicRefraction_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye.TrimEnd(',');

                    //int iCycloplegicRefraction_RightEye = 0;
                    //try { iCycloplegicRefraction_RightEye = int.Parse(rdoCycloplegicRefraction_RightEye.SelectedValue); }
                    //catch { iCycloplegicRefraction_RightEye = 0; }

                    int iRetinoScopy_LeftEye = -1;
                    if (rdoRetinoScopy_LeftEye.SelectedValue != "")
                    {
                        iRetinoScopy_LeftEye = int.Parse(rdoRetinoScopy_LeftEye.SelectedValue);
                    }
                    //int iRetinoScopy_LeftEye = 0;
                    //try { iRetinoScopy_LeftEye = int.Parse(rdoRetinoScopy_LeftEye.SelectedValue); }
                    //catch { iRetinoScopy_LeftEye = 0; }

                    //int iCycloplegicRefraction_LeftEye = -1;
                    //if (rdoCycloplegicRefraction_LeftEye.SelectedValue != "")
                    //{
                    //    iCycloplegicRefraction_LeftEye = int.Parse(rdoCycloplegicRefraction_LeftEye.SelectedValue);
                    //}

                    string strCycloplegicRefraction_LeftEye = string.Empty;

                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (rdoCycloplegicRefraction_LeftEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strCycloplegicRefraction_LeftEye += rdoCycloplegicRefraction_LeftEye.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye.TrimEnd(',');

                    //int iCycloplegicRefraction_LeftEye = 0;
                    //try { iCycloplegicRefraction_LeftEye = int.Parse(rdoCycloplegicRefraction_LeftEye.SelectedValue); }
                    //catch { iCycloplegicRefraction_LeftEye = 0; }

                    int iHirchberg_Distance = 0;
                    try { iHirchberg_Distance = int.Parse(rdoHirchberg_Distance.SelectedValue); }
                    catch { iHirchberg_Distance = 0; }

                    int iHirchberg_Near = 0;
                    try { iHirchberg_Near = int.Parse(rdoHirchberg_Near.SelectedValue); }
                    catch { iHirchberg_Near = 0; }

                    int iOphthalmoscope_RightEye = 0;
                    try { iOphthalmoscope_RightEye = int.Parse(rdoOphthalmoscope_RightEye.SelectedValue); }
                    catch { iOphthalmoscope_RightEye = 0; }

                    int iPupillaryReactions_RightEye = 0;
                    try { iPupillaryReactions_RightEye = int.Parse(rdoPupillaryReactions_RightEye.SelectedValue); }
                    catch { iPupillaryReactions_RightEye = 0; }

                    int iCoverUncovertTest_RightEye = 0;
                    try { iCoverUncovertTest_RightEye = int.Parse(rdoCoverUncovertTest_RightEye.SelectedValue); }
                    catch { iCoverUncovertTest_RightEye = 0; }

                    int iOphthalmoscope_LeftEye = 0;
                    try { iOphthalmoscope_LeftEye = int.Parse(rdoOphthalmoscope_LeftEye.SelectedValue); }
                    catch { iOphthalmoscope_LeftEye = 0; }

                    int iPupillaryReactions_LeftEye = -1;
                    //try { iPupillaryReactions_LeftEye = int.Parse(rdoPupillaryReactions_LeftEye.SelectedValue); }
                    //catch { iPupillaryReactions_LeftEye = 0; }

                    int iCoverUncovertTest_LeftEye = 0;
                    try { iCoverUncovertTest_LeftEye = int.Parse(rdoCoverUncovertTest_LeftEye.SelectedValue); }
                    catch { iCoverUncovertTest_LeftEye = 0; }

                    // Diagnosis - Treatment
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

                    int iSubDiagnosis_RightEye = -1;
                    if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    {
                        iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    }

                    int iSubDiagnosis_LeftEye = -1;
                    if (rdoSubDiagnosis_LeftEye.SelectedValue != "")
                    {
                        iSubDiagnosis_LeftEye = int.Parse(rdoSubDiagnosis_LeftEye.SelectedValue);
                    }
                    //string strDiagnosis = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis = strDiagnosis.TrimEnd(',');
                    //if (strDiagnosis.Trim() == "") { strDiagnosis = "0"; }

                    //int iSubDiagnosis_RightEye = -1;
                    //if (rdoSubDiagnosis_RightEye.SelectedValue != "")
                    //{
                    //    iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);
                    //}

                    int iTreatment = 0;

                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }

                    //string strMedicine = string.Empty;

                    //for (int i = 0; i < chkMedicine.Items.Count; i++)
                    //{
                    //    if (chkMedicine.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strMedicine += chkMedicine.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strMedicine = strMedicine.TrimEnd(',');
                    int iFrequency = 0;
                    if (txtFrequency.Text != "")
                    {
                        iFrequency = int.Parse(txtFrequency.Text);
                    }

                    int iNextVisit = -1;
                    if (rdoNextVisit.SelectedValue != "")
                    {
                        iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                    }

                    int iSurgery = -1;
                    if (rdoSurgery.SelectedValue != "")
                    {
                        iSurgery = int.Parse(rdoSurgery.SelectedValue);
                    }

                    int iSurgery_Detail = -1;
                    if (rdoSurgery_Detail.SelectedValue != "")
                    {
                        iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                    }

                    int iReferal = -1;
                    if (rdoReferal.SelectedValue != "")
                    {
                        iReferal = int.Parse(rdoReferal.SelectedValue);
                    }

                    DateTime dtTest;
                    dtTest = DateTime.Parse(txtTestDate.Text);

                    var res = dx.sp_tblVisitForFurtherAssessmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlDoctor.SelectedValue),
                        iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //strDiagnosis, txtDiagnosis_RightEye.Text, iSubDiagnosis_RightEye, 
                        iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye,
                        iTreatment, iTreatment_Glasses, txtMedicine.Text, iFrequency, iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                        strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                    if (res.ResponseCode == 1)
                    {
                        hfAutoRefTestIDPKID.Value = res.VisitForFurtherAssessmentStudentId.ToString();
                        //lbl_error.Text = res.RetMessage;
                        if (rdoOptometristTest.SelectedValue == "1" && chkNeedsCycloRefraction_RightEye.Checked == true)
                        {
                            rdoOptometristTest.SelectedValue = "4";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "1")
                        {
                            //error here...
                            rdoOptometristTest.SelectedValue = "2";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                            if (rdoOldNewTest.SelectedValue == "0")
                            {
                                if (txtSpherical_RightEye.Text == "0.00" && txtCyclinderical_RightEye.Text == "0.00"
                                        && txtAxixA_RightEye.Text == "0" && txtNear_RightEye.Text == "0.00"
                                        && txtSpherical_LeftEye.Text == "0.00" && txtCyclinderical_LeftEye.Text == "0.00"
                                        && txtAxixA_LeftEye.Text == "0" && txtNear_LeftEye.Text == "0.00")
                                {
                                    txtSpherical_RightEye.Text = "";
                                    txtCyclinderical_RightEye.Text = "";
                                    txtAxixA_RightEye.Text = "";
                                    txtNear_RightEye.Text = "";
                                    txtSpherical_LeftEye.Text = "";
                                    txtCyclinderical_LeftEye.Text = "";
                                    txtAxixA_LeftEye.Text = "";
                                    txtNear_LeftEye.Text = "";
                                }

                                if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "0")
                                {
                                    txtSpherical_RightEye.Text = "0.00";
                                }

                                if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "0")
                                {
                                    txtSpherical_LeftEye.Text = "0.00";
                                }
                            }

                        }
                        else if (rdoOptometristTest.SelectedValue == "2" && chkObjectiveRefraction.Checked == false)
                        {
                            rdoOptometristTest.SelectedValue = "4";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "2")
                        {
                            rdoOptometristTest.SelectedValue = "3";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "3")
                        {
                            rdoOptometristTest.SelectedValue = "4";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "4")
                        {
                            rdoOptometristTest.SelectedValue = "5";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "5")
                        {
                            rdoOptometristTest.SelectedValue = "6";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else if (rdoOptometristTest.SelectedValue == "6")
                        {
                            ClearForm();
                            ShowConfirmAddMoreRecord();

                            txtTestDate.Text = Utilities.GetDate();
                        }
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FurtherAssessment.aspx");
        }

        protected void chkNeedsCycloRefraction_RightEye_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNeedsCycloRefraction_RightEye.Checked == true)
            {
                txtNeedsCycloRefraction_RightEye.Visible = true;
                txtNeedsCycloRefraction_RightEye.Focus();
            }
            else
            {
                txtNeedsCycloRefraction_RightEye.Visible = false;
            }
        }

        protected void chkNeedsCycloRefraction_LeftEye_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNeedsCycloRefraction_LeftEye.Checked == true)
            {
                txtNeedsCycloRefraction_LeftEye.Visible = true;
                txtNeedsCycloRefraction_LeftEye.Focus();
            }
            else
            {
                txtNeedsCycloRefraction_LeftEye.Visible = false;
            }
        }

        protected void rdoCoverUncovertTest_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoCoverUncovertTest_RightEye.SelectedValue == "7")
            {
                txtCoverUncovertTestRemarks_RightEye.Visible = true;
                txtCoverUncovertTestRemarks_RightEye.Focus();
            }
            else
            {
                txtCoverUncovertTestRemarks_RightEye.Visible = false;
            }
        }

        protected void rdoCoverUncovertTest_LeftEye_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoCoverUncovertTest_LeftEye.SelectedValue == "7")
            {
                txtCoverUncovertTestRemarks_LeftEye.Visible = true;
                txtCoverUncovertTestRemarks_LeftEye.Focus();
            }
            else
            {
                txtCoverUncovertTestRemarks_LeftEye.Visible = false;
            }
        }

        protected void btnMovePrevious_Click(object sender, EventArgs e)
        {
            if (rdoOptometristTest.SelectedValue == "2")
            {
                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);
            }
            else if (rdoOptometristTest.SelectedValue == "3")
            {
                rdoOptometristTest.SelectedValue = "2";
                rdoOptometristTest_SelectedIndexChanged(null, null);
            }
            else if (rdoOptometristTest.SelectedValue == "4")
            {
                rdoOptometristTest.SelectedValue = "3";
                rdoOptometristTest_SelectedIndexChanged(null, null);
            }
            else if (rdoOptometristTest.SelectedValue == "5")
            {
                rdoOptometristTest.SelectedValue = "4";
                rdoOptometristTest_SelectedIndexChanged(null, null);
            }
            else if (rdoOptometristTest.SelectedValue == "6")
            {
                rdoOptometristTest.SelectedValue = "5";
                rdoOptometristTest_SelectedIndexChanged(null, null);
            }

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

        protected void chkObjectiveRefraction_CheckedChanged(object sender, EventArgs e)
        {
            btnMoveNext_Click(null, null);
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
                var dtDoctor = dx.Database.SqlQuery<tblDoctor>(q).ToList();
                if (dtDoctor.Count != 0)
                {
                    ddlDoctor.DataSource = dtDoctor;
                    ddlDoctor.DataValueField = "DoctorAutoId";
                    ddlDoctor.DataTextField = "DoctorDescription";
                    ddlDoctor.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Select";
                    item.Value = "0";
                    ddlDoctor.Items.Insert(0, item);
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
            var dtDoctor = dx.Database.SqlQuery<tblDoctor>(q).ToList();

            if (dtDoctor.Count != 0)
            {
                ddlDoctor.DataSource = dtDoctor;
                ddlDoctor.DataValueField = "DoctorAutoId";
                ddlDoctor.DataTextField = "DoctorDescription";
                ddlDoctor.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlDoctor.Items.Insert(0, item);
            }
            else
            {
                ddlDoctor.Items.Clear();
                ddlDoctor.DataSource = null;
                ddlDoctor.DataBind();

                ListItem item = new ListItem();
                item.Text = "Select";
                item.Value = "0";
                ddlDoctor.Items.Insert(0, item);
            }
        }

        //protected void rdoDiagnosis_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    rdoSubDiagnosis_RightEye.Visible = false;

        //    //pnlTreatment.Visible = false;
        //    //pnlVisit1.Visible = false;

        //    txtDiagnosis_RightEye.Visible = false;
        //    //rdoTreatment_Glasses.SelectedIndex = -1;

        //    if (rdoDiagnosis_RightEye.Items[16].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;

        //        //pnlTreatment.Visible = true;
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
        //                || rdoDiagnosis_RightEye.Items[3].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = false;

        //        //pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[1].Selected == true)
        //    {
        //        rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        //pnlTreatment.Visible = true;
        //        //pnlVisit1.Visible = true;

        //        //rdoTreatment_Glasses.SelectedValue = "0";
        //        //rdoNextVisit.SelectedValue = "1";
        //    }

        //    if (rdoDiagnosis_RightEye.SelectedIndex == -1)
        //    {
        //        txtDiagnosis_RightEye.Visible = false;
        //        //pnlTreatment.Visible = false;
        //        //pnlVisit1.Visible = false;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[0].Selected == true)
        //    {

        //        //chkMedicine.ClearSelection();
        //        rdoSubDiagnosis_RightEye.SelectedIndex = -1;

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

        //    if (rdoDiagnosis_RightEye.Items[16].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;
        //    }
        //}

        protected void rdoNextVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSurgery.Visible = false;
            rdoSurgery_Detail.Visible = false;
            rdoSurgery.SelectedIndex = -1;
            if (rdoNextVisit.SelectedValue == "3")
            {
                rdoSurgery.Visible = true;
                //rdoSurgery.SelectedValue = "-1";
            }
        }

        protected void rdoSurgery_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSurgery_Detail.Visible = false;
            rdoSurgery_Detail.SelectedIndex = -1;
            if (rdoSurgery.SelectedValue == "3")
            {
                rdoSurgery_Detail.Visible = true;
            }
        }

        protected void rdoSurgery_Detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSurgery_Detail.Visible = false;
            if (rdoSurgery_Detail.SelectedValue == "8")
            {
                txtSurgery_Detail.Visible = true;//c
            }
        }

        protected void rdoTreatment_Glasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoTreatment_Glasses.SelectedValue == "0")
            {
                rdoNextVisit.SelectedValue = "1";
            }
            else
            {
                rdoNextVisit.SelectedIndex = -1;
            }
        }

        protected void btnAddMedicine_Click(object sender, EventArgs e)
        {
            ClearValidation();

            if (txtMedicine.Text.Trim() == "")
            {
                lbl_error.Text = "'Medicine Description' is required.";
                txtMedicine.Focus();
                return;
            }

            if (txtFrequency.Text.Trim() == "")
            {
                lbl_error.Text = "'Medicine Duration (Days)' is required.";
                txtFrequency.Focus();
                return;
            }

            var res = dx.sp_tblMedicineFurtherAssessmentStudent_InsertUpdate(0, Convert.ToInt32(hfAutoRefTestIDPKID.Value),
                                                                             Convert.ToInt32(hfStudentIDPKID.Value), txtMedicine.Text,
                                                                             int.Parse(txtFrequency.Text)).FirstOrDefault();

            if (res.ResponseCode == 1)
            {
                txtMedicine.Text = "";
                txtFrequency.Text = "";

                BindGrid();
            }
            else
            {
                lbl_error.Text = res.RetMessage;
            }


        }

        protected void gvMedicine_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ClearValidation();
            string MedicineAutoId = this.gvMedicine.DataKeys[e.RowIndex].Value.ToString();

            try
            {
                if (Convert.ToInt32(MedicineAutoId) > 0)
                {
                    using (var ctx = new secoffEntities())
                    {
                        var res = ctx.sp_tblMedicineFurtherAssessmentStudent_Delete(Convert.ToInt32(MedicineAutoId)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;
                            //ClearForm();
                            BindGrid();
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

            }
        }

        public void BindGrid()
        {
            try
            {
                using (secoffEntities ctx = new secoffEntities())
                {
                    var detail = ctx.sp_tblMedicineFurtherAssessmentStudent_GetDetail(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).ToList();
                    if (detail.Count > 0)
                    {
                        gvMedicine.DataSource = detail;
                        gvMedicine.DataBind();

                    }
                    else
                    {
                        gvMedicine.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void enableCheckboxSelections()
        {
            rdoSubDiagnosis_RightEye.Visible = false;
            rdoSubDiagnosis_LeftEye.Visible = false;

            txtDiagnosis_RightEye.Visible = false;
            txtDiagnosis_LeftEye.Visible = false;

            rdoTreatment_Glasses.SelectedIndex = -1;

            if (chkOther_RightEye.Checked == true)
            {
                txtDiagnosis_RightEye.Visible = true;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;
            }
            if (chkOther_LeftEye.Checked == true)
            {
                txtDiagnosis_LeftEye.Visible = true;
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
            }

            if (chkRefractiveError_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkRefractiveError_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                txtDiagnosis_LeftEye.Visible = false;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_LeftEye.Visible = false;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
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

                rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoNextVisit.SelectedIndex = -1;
                rdoSurgery.SelectedIndex = -1;
                rdoReferal.SelectedIndex = -1;

                txtDiagnosis_RightEye.Visible = false;
                rdoSubDiagnosis_RightEye.Visible = false;

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
                rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoNextVisit.SelectedIndex = -1;
                rdoSurgery.SelectedIndex = -1;
                rdoReferal.SelectedIndex = -1;

                txtDiagnosis_LeftEye.Visible = false;
                rdoSubDiagnosis_LeftEye.Visible = false;

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