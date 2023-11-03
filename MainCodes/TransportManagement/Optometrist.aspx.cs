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
    public partial class Optometrist : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "Optometer"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);

                StartButtonImpact(false);
                //pnlTestArea.Visible = false;
                //Div1.Visible = false;
                //pnlRightEye_AutoRef.Visible = false;
                //pnlLeftEye_AutoRef.Visible = false;
                //pnlOptometristTest.Visible = false;
                //pnlTest1_RightEye.Visible = false;
                //pnlTest1_LeftEye.Visible = false;

                rdoDistanceVision_RightEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_RightEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_RightEye_PinHole.SelectedIndex = -1;

                rdoDistanceVision_LeftEye_WithGlasses.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Unaided.SelectedIndex = -1;
                rdoDistanceVision_LeftEye_Pinhole.SelectedIndex = -1;

                //rdoRetinoScopy_RightEye.SelectedIndex = -1;
                //rdoCycloplegicRefraction_RightEye.SelectedIndex = -1;

                //rdoRetinoScopy_LeftEye.SelectedIndex = -1;
                //rdoCycloplegicRefraction_LeftEye.SelectedIndex = -1;

                txtTestDate.Visible = true;
                ddlPreviousTestResult.Visible = false;

                txtNear_RightEye.Text = "00.00";
                txtNear_LeftEye.Text = "00.00";

                txtTestDate.Text = Utilities.GetDate();

                var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                if (dtTestSummary != null)
                {
                    lblResultDate.Text = Utilities.GetDate();
                    lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                    lblTotalTestConducted.Text = dtTestSummary.TotalAutoRefTest.ToString();
                    lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalAutoRefTest);
                }

                DataTable dtTestDetail = dx.sp_Optometrist_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                if (dtTestDetail != null)
                {
                    gvRemainingList.DataSource = dtTestDetail;
                    gvRemainingList.DataBind();
                }

                if (Request.QueryString["redirect"] != null)
                {
                    if (Session["rdoType"] != null)
                    {
                        if (Session["rdoType"].ToString() == "0")
                        {
                            rdoType.SelectedValue = Session["rdoType"].ToString();
                            rdoType_SelectedIndexChanged(null, null);

                            txtTestDate.Text = DateTime.Parse(Session["TestDate"].ToString()).ToString("dd-MMM-yyyy");
                            //txtTestDate.Enabled = false;

                            hfStudentIDPKID.Value = Session["Id"].ToString();
                            hfStudentIDPKID_ValueChanged(null, null);

                            hfAutoRefTestIDPKID.Value = Session["TransactionId"].ToString();
                            hfAutoRefTestIDPKID_ValueChanged(null, null);

                            rdoOptometristTest.SelectedValue = "4";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                        else
                        {
                            rdoType.SelectedValue = Session["rdoType"].ToString();
                            rdoType_SelectedIndexChanged(null, null);

                            txtTestDate.Text = DateTime.Parse(Session["TestDate"].ToString()).ToString("dd-MMM-yyyy");
                            //txtTestDate.Enabled = false;

                            hfTeacherIDPKID.Value = Session["Id"].ToString();
                            hfTeacherIDPKID_ValueChanged(null, null);

                            hfAutoRefTestIDPKID.Value = Session["TransactionId"].ToString();
                            hfAutoRefTestIDPKID_ValueChanged(null, null);

                            rdoOptometristTest.SelectedValue = "4";
                            rdoOptometristTest_SelectedIndexChanged(null, null);
                        }
                    }
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

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);

                ClearForm();
                ClearValidation();

                //rdoOptometristTest.SelectedValue = "1";
                //rdoOptometristTest_SelectedIndexChanged(null, null);

                //rdoOldNewTest.SelectedValue = "0";

                txtStudentCode.Focus();

                StartButtonImpact(false);
                //pnlTestArea.Visible = false;
                //Div1.Visible = false;
                //pnlRightEye_AutoRef.Visible = false;
                //pnlLeftEye_AutoRef.Visible = false;
                //pnlOptometristTest.Visible = false;
                //pnlTest1_RightEye.Visible = false;
                //pnlTest1_LeftEye.Visible = false;
            }
            else
            {
                pnlStudent.Visible = false;
                pnlStudent_Sub.Visible = false;

                pnlTeacher.Visible = true;
                pnlTeacher_Sub.Visible = true;

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);
                ClearForm();
                ClearValidation();

                //rdoOptometristTest.SelectedValue = "1";
                //rdoOptometristTest_SelectedIndexChanged(null, null);

                //rdoOldNewTest.SelectedValue = "0";

                txtTeacherCode.Focus();

                StartButtonImpact(false);
                //pnlTestArea.Visible = false;
                //Div1.Visible = false;
                //pnlRightEye_AutoRef.Visible = false;
                //pnlLeftEye_AutoRef.Visible = false;
                //pnlOptometristTest.Visible = false;
                //pnlTest1_RightEye.Visible = false;
                //pnlTest1_LeftEye.Visible = false;
            }

        }
        protected void lblShowStudentDetail_Click(object sender, EventArgs e)
        {
            if (rdoType.SelectedValue == "0")
            {
                if (txtStudentCode.Text.Trim() == "")
                {
                    lbl_error.Text = "Student Code is required.";
                    txtStudentCode.Focus();
                    return;
                }
            }
            else
            {
                if (txtTeacherCode.Text.Trim() == "")
                {
                    lbl_error.Text = "Teacher Code is required.";
                    txtTeacherCode.Focus();
                    return;
                }
            }

            if (rdoOldNewTest.SelectedValue == "0")
            {
                StartButtonImpact(true);
                //pnlTestArea.Visible = true;
                //Div1.Visible = true;
                //pnlRightEye_AutoRef.Visible = true;
                //pnlLeftEye_AutoRef.Visible = true;
                //pnlOptometristTest.Visible = true;
                //pnlTest1_RightEye.Visible = true;
                //pnlTest1_LeftEye.Visible = true;
            }
            else
            {
                StartButtonImpact(true);
                //pnlTestArea.Visible = true;
                //Div1.Visible = true;
                //pnlRightEye_AutoRef.Visible = true;
                //pnlLeftEye_AutoRef.Visible = true;
                //pnlOptometristTest.Visible = true;
                //pnlTest1_RightEye.Visible = true;
                //pnlTest1_LeftEye.Visible = true;
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
                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        //var autoRefTransId = dx.sp_tblAutoRefTestStudent_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;
                        DateTime dtTest;
                        if (hfAutoRefTestIDPKID.Value == "0")
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }

                        //var res = dx.sp_tblOptometristMasterStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfStudentIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    lbl_error.Text = res.RetMessage;

                        //    ClearForm();
                        //    ShowConfirmAddMoreRecord();

                        //    txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
                    }
                }
                else
                {
                    if (ValidateInputTeacher())
                    {
                        //var autoRefTransId = dx.sp_tblAutoRefTestTeacher_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;

                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        DateTime dtTest;
                        if (hfAutoRefTestIDPKID.Value == "0")
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }

                        //var res = dx.sp_tblOptometristMasterTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfTeacherIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    lbl_error.Text = res.RetMessage;

                        //    ClearForm();
                        //    ShowConfirmAddMoreRecord();

                        //    txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
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

                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInputStudent())
                    {
                        //var autoRefTransId = dx.sp_tblAutoRefTestStudent_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;

                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        DateTime dtTest;
                        if (hfAutoRefTestIDPKID.Value == "0")
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }

                        //var res = dx.sp_tblOptometristMasterStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfStudentIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    lbl_error.Text = res.RetMessage;

                        //    ClearForm();
                        //    ShowConfirmAddMoreRecord();

                        //    txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
                    }
                }
                else
                {
                    if (ValidateInputTeacher())
                    {
                        //var autoRefTransId = dx.sp_tblAutoRefTestTeacher_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;

                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        DateTime dtTest;
                        if (hfAutoRefTestIDPKID.Value == "0")
                        {
                            dtTest = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dtTest = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text);
                        }

                        //var res = dx.sp_tblOptometristMasterTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfTeacherIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    lbl_error.Text = res.RetMessage;

                        //    ClearForm();
                        //    ShowConfirmAddMoreRecord();

                        //    txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
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
                        var res = dx.sp_tblOptometristMasterStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            var dtLastData = dx.sp_tblOptometristMasterStudent_GetLastTest(Convert.ToInt32(hfStudentIDPKID.Value)).SingleOrDefault();
            if (dtLastData == null)
            {
                lbl_error.Text = "'Auto Refraction Test' not conducted for this Student. Please perform 'Auto Refraction Test'.";
                return false;
            }

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

            lblSpherical_RightEye.Text = "";
            lblCylinderical_RightEye.Text = "";
            lblAxix_RightEye.Text = "";

            lblSpherical_LeftEye.Text = "";
            lblCylinderical_LeftEye.Text = "";
            lblAxix_LeftEye.Text = "";

            chkOccularHistory.Checked = false;
            txtOccularHistory.Text = "";

            chkMedicalHistory.Checked = false;
            txtMedicalHistory.Text = "";

            chkChiefComplain.Checked = false;
            txtChiefComplain.Text = "";

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

            ClearValidation();

            txtStudentCode.Focus();
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        private void StartButtonImpact(bool bValue)
        {

            pnlTestArea.Visible = bValue;
            Div1.Visible = bValue;
            pnlRightEye_AutoRef.Visible = bValue;
            pnlLeftEye_AutoRef.Visible = bValue;
            pnlOptometristTest.Visible = bValue;
            pnlTest1_RightEye.Visible = bValue;
            pnlTest1_LeftEye.Visible = bValue;
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
                if (rdoType.SelectedValue == "0")
                {
                    var dt = dx.sp_tblOptometristMasterStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    //hfAutoRefTestTransID.Value = dt.AutoRefStudentTransId.ToString();
                    hfAutoRefTestTransDate.Value = dt.OptometristStudentTransDate.ToString(); //   AutoRefStudentTransDate.ToString();

                    txtTestDate.Text = DateTime.Parse(hfAutoRefTestTransDate.Value).ToString("dd-MMM-yyyy");

                    int iHasChiefComplain = Convert.ToInt32(dt.HasChiefComplain.ToString());
                    if (iHasChiefComplain == 1) { chkChiefComplain.Checked = true; }
                    else { chkChiefComplain.Checked = false; }
                    txtChiefComplain.Text = dt.ChiefComplainRemarks.ToString();

                    int iHasOccularHistory = Convert.ToInt32(dt.HasOccularHistory.ToString());
                    if (iHasOccularHistory == 1) { chkOccularHistory.Checked = true; }
                    else { chkOccularHistory.Checked = false; }
                    txtOccularHistory.Text = dt.OccularHistoryRemarks.ToString();

                    int iHasMedicalHistory = Convert.ToInt32(dt.HasMedicalHistory.ToString());
                    if (iHasMedicalHistory == 1) { chkMedicalHistory.Checked = true; }
                    else { chkMedicalHistory.Checked = false; }
                    txtMedicalHistory.Text = dt.MedicalHistoryRemarks.ToString();
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
                    //rdoDistanceVision_LeftEye_Unaided.SelectedValue = dt.DistanceVision_LeftEye_Unaided.ToString();
                    //rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = dt.DistanceVision_LeftEye_WithGlasses.ToString();
                    //rdoDistanceVision_LeftEye_Pinhole.SelectedValue = dt.DistanceVision_LeftEye_PinHole.ToString();
                    //rdoNearVision_LeftEye.SelectedValue = dt.NearVision_LeftEye.ToString();

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
                        txtSpherical_RightEye.Text = "0.00";
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
                        txtCyclinderical_RightEye.Text = "0.00";
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

                    if (decimal.Parse(dt.Right_Near_Points.ToString()) == 0)
                    {
                        txtNear_RightEye.Text = "0.00";
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
                        txtSpherical_LeftEye.Text = "0.00";
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

                    if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) == 0)
                    {
                        txtCyclinderical_LeftEye.Text = "0.00";
                    }
                    else
                    {
                        txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();
                    }
                    //txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    if (int.Parse(dt.Left_Axix_From.ToString()) == 0)
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

                    if (decimal.Parse(dt.Left_Near_Points.ToString()) == 0)
                    {
                        txtNear_LeftEye.Text = "0.00";
                    }
                    else
                    {
                        txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();
                    }
                    //txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();

                    string strAchromatopsia = dt.Achromatopsia.ToString();
                    //string strList[] = strAchromatopsia.Split(',');

                    string s = strAchromatopsia;

                    string[] items = s.Split(',');
                    for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                    {
                        if (items.Contains(chkAchromatopsia.Items[i].Value))
                        {
                            chkAchromatopsia.Items[i].Selected = true;
                        }
                    }

                    //rdoAchromatopsia.SelectedValue = dt.Achromatopsia.ToString();
                    rdoDouchrome.SelectedValue = dt.Douchrome.ToString();

                    if (dt.RetinoScopy_RightEye.ToString() == "-1")
                    {
                        rdoRetinoScopy_RightEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoRetinoScopy_RightEye.SelectedValue = dt.RetinoScopy_RightEye.ToString();
                    }
                    //rdoRetinoScopy_RightEye.SelectedValue = dt.RetinoScopy_RightEye.ToString();
                    //if (dt.CycloplegicRefraction_RightEye.ToString() == "-1")
                    //{
                    //    rdoCycloplegicRefraction_RightEye.SelectedIndex = -1;
                    //}
                    //else
                    //{
                    //    rdoCycloplegicRefraction_RightEye.SelectedValue = dt.CycloplegicRefraction_RightEye.ToString();
                    //}

                    string strCycloplegicRefraction_RightEye = dt.CycloplegicRefraction_RightEye.ToString();
                    string sCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye;

                    string[] itemsCycloplegicRefraction_RightEye = sCycloplegicRefraction_RightEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_RightEye.Contains(rdoCycloplegicRefraction_RightEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_RightEye.Items[i].Selected = true;
                        }
                        else
                        {
                            rdoCycloplegicRefraction_RightEye.Items[i].Selected = false;
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
                    //rdoRetinoScopy_LeftEye.SelectedValue = dt.RetinoScopy_LeftEye.ToString();

                    //if (dt.CycloplegicRefraction_LeftEye.ToString() == "-1")
                    //{
                    //    rdoCycloplegicRefraction_LeftEye.SelectedIndex = -1;
                    //}
                    //else
                    //{
                    //    rdoCycloplegicRefraction_LeftEye.SelectedValue = dt.CycloplegicRefraction_LeftEye.ToString();
                    //}

                    string strCycloplegicRefraction_LeftEye = dt.CycloplegicRefraction_LeftEye.ToString();
                    string sCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye;

                    string[] itemsCycloplegicRefraction_LeftEye = sCycloplegicRefraction_LeftEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_LeftEye.Contains(rdoCycloplegicRefraction_LeftEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_LeftEye.Items[i].Selected = true;
                        }
                        else
                        {
                            rdoCycloplegicRefraction_LeftEye.Items[i].Selected = false;
                        }
                    }

                    //rdoCycloplegicRefraction_LeftEye.SelectedValue = dt.CycloplegicRefraction_LeftEye.ToString();

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

                    lblOptometrist_Student.Text = dt.UserId.ToString();

                }
                else
                {
                    var dt = dx.sp_tblOptometristMasterTeacher_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    //hfAutoRefTestTransID.Value = dt.AutoRefTeacherTransId.ToString();
                    hfAutoRefTestTransDate.Value = dt.OptometristTeacherTransDate.ToString();

                    txtTestDate.Text = DateTime.Parse(hfAutoRefTestTransDate.Value).ToString("dd-MMM-yyyy");

                    int iHasChiefComplain = Convert.ToInt32(dt.HasChiefComplain.ToString());
                    if (iHasChiefComplain == 1) { chkChiefComplain.Checked = true; }
                    else { chkChiefComplain.Checked = false; }
                    txtChiefComplain.Text = dt.ChiefComplainRemarks.ToString();

                    int iHasOccularHistory = Convert.ToInt32(dt.HasOccularHistory.ToString());
                    if (iHasOccularHistory == 1) { chkOccularHistory.Checked = true; }
                    else { chkOccularHistory.Checked = false; }
                    txtOccularHistory.Text = dt.OccularHistoryRemarks.ToString();

                    int iHasMedicalHistory = Convert.ToInt32(dt.HasMedicalHistory.ToString());
                    if (iHasMedicalHistory == 1) { chkMedicalHistory.Checked = true; }
                    else { chkMedicalHistory.Checked = false; }
                    txtMedicalHistory.Text = dt.MedicalHistoryRemarks.ToString();
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

                    int iNeedCycloRefraction_RightEye = Convert.ToInt32(dt.NeedCycloRefraction_RightEye.ToString());
                    if (iNeedCycloRefraction_RightEye == 1) { chkNeedsCycloRefraction_RightEye.Checked = true; }
                    else { chkNeedsCycloRefraction_RightEye.Checked = false; }
                    txtNeedsCycloRefraction_RightEye.Text = dt.NeedCycloRefractionRemarks_RightEye;


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
                    //rdoDistanceVision_LeftEye_Unaided.SelectedValue = dt.DistanceVision_LeftEye_Unaided.ToString();
                    //rdoDistanceVision_LeftEye_WithGlasses.SelectedValue = dt.DistanceVision_LeftEye_WithGlasses.ToString();
                    //rdoDistanceVision_LeftEye_Pinhole.SelectedValue = dt.DistanceVision_LeftEye_PinHole.ToString();
                    //rdoNearVision_LeftEye.SelectedValue = dt.NearVision_LeftEye.ToString();

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
                        txtSpherical_RightEye.Text = "0.00";
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
                        txtCyclinderical_RightEye.Text = "0.00";
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

                    if (decimal.Parse(dt.Right_Near_Points.ToString()) == 0)
                    {
                        txtNear_RightEye.Text = "0.00";
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
                        txtSpherical_LeftEye.Text = "0.00";
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

                    if (decimal.Parse(dt.Left_Cyclinderical_Points.ToString()) == 0)
                    {
                        txtCyclinderical_LeftEye.Text = "0.00";
                    }
                    else
                    {
                        txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();
                    }
                    //txtCyclinderical_LeftEye.Text = dt.Left_Cyclinderical_Points.ToString();

                    if (int.Parse(dt.Left_Axix_From.ToString()) == 0)
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

                    if (decimal.Parse(dt.Left_Near_Points.ToString()) == 0)
                    {
                        txtNear_LeftEye.Text = "0.00";
                    }
                    else
                    {
                        txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();
                    }
                    //txtNear_LeftEye.Text = dt.Left_Near_Points.ToString();

                    string strAchromatopsia = dt.Achromatopsia.ToString();
                    //string strList[] = strAchromatopsia.Split(',');

                    string s = strAchromatopsia;

                    string[] items = s.Split(',');
                    for (int i = 0; i < chkAchromatopsia.Items.Count; i++)
                    {
                        if (items.Contains(chkAchromatopsia.Items[i].Value))
                        {
                            chkAchromatopsia.Items[i].Selected = true;
                        }
                    }

                    //rdoAchromatopsia.SelectedValue = dt.Achromatopsia.ToString();
                    rdoDouchrome.SelectedValue = dt.Douchrome.ToString();

                    if (dt.RetinoScopy_RightEye.ToString() == "-1")
                    {
                        rdoRetinoScopy_RightEye.SelectedIndex = -1;
                    }
                    else
                    {
                        rdoRetinoScopy_RightEye.SelectedValue = dt.RetinoScopy_RightEye.ToString();
                    }
                    //rdoRetinoScopy_RightEye.SelectedValue = dt.RetinoScopy_RightEye.ToString();
                    //if (dt.CycloplegicRefraction_RightEye.ToString() == "-1")
                    //{
                    //    rdoCycloplegicRefraction_RightEye.SelectedIndex = -1;
                    //}
                    //else
                    //{
                    //    rdoCycloplegicRefraction_RightEye.SelectedValue = dt.CycloplegicRefraction_RightEye.ToString();
                    //}

                    string strCycloplegicRefraction_RightEye = dt.CycloplegicRefraction_RightEye.ToString();
                    string sCycloplegicRefraction_RightEye = strCycloplegicRefraction_RightEye;

                    string[] itemsCycloplegicRefraction_RightEye = sCycloplegicRefraction_RightEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_RightEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_RightEye.Contains(rdoCycloplegicRefraction_RightEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_RightEye.Items[i].Selected = true;
                        }
                        else
                        {
                            rdoCycloplegicRefraction_RightEye.Items[i].Selected = false;
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
                    //rdoRetinoScopy_LeftEye.SelectedValue = dt.RetinoScopy_LeftEye.ToString();

                    //if (dt.CycloplegicRefraction_LeftEye.ToString() == "-1")
                    //{
                    //    rdoCycloplegicRefraction_LeftEye.SelectedIndex = -1;
                    //}
                    //else
                    //{
                    //    rdoCycloplegicRefraction_LeftEye.SelectedValue = dt.CycloplegicRefraction_LeftEye.ToString();
                    //}

                    string strCycloplegicRefraction_LeftEye = dt.CycloplegicRefraction_LeftEye.ToString();
                    string sCycloplegicRefraction_LeftEye = strCycloplegicRefraction_LeftEye;

                    string[] itemsCycloplegicRefraction_LeftEye = sCycloplegicRefraction_LeftEye.Split(',');
                    for (int i = 0; i < rdoCycloplegicRefraction_LeftEye.Items.Count; i++)
                    {
                        if (itemsCycloplegicRefraction_LeftEye.Contains(rdoCycloplegicRefraction_LeftEye.Items[i].Value))
                        {
                            rdoCycloplegicRefraction_LeftEye.Items[i].Selected = true;
                        }
                        else
                        {
                            rdoCycloplegicRefraction_LeftEye.Items[i].Selected = false;
                        }
                    }

                    //rdoCycloplegicRefraction_LeftEye.SelectedValue = dt.CycloplegicRefraction_LeftEye.ToString();

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

                    lblOptometrist_Teacher.Text = dt.UserId.ToString();
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

                    var dtLastData_AutoRef = dx.sp_tblOptometristMasterStudent_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtLastData_AutoRef != null)
                        {
                            txtTestDate.Text = DateTime.Parse(dtLastData_AutoRef.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtLastData = dx.sp_tblAutoRefTestStudent_GetCurrentDateTest(Convert.ToInt32(ID), DateTime.Parse(txtTestDate.Text)).SingleOrDefault();
                    try
                    {
                        if (dtLastData != null)
                        {
                            txtTestDate.Text = DateTime.Parse(dtLastData.AutoRefStudentTransDate.ToString()).ToString("dd-MMM-yyyy");
                            //txtTestDate.Enabled = false;

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
                        }
                        else
                        {
                            txtTestDate.Text = Utilities.GetDate();
                            txtTestDate.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                    if (dtTestSummary != null)
                    {
                        lblResultDate.Text = Utilities.GetDate();
                        lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                        lblTotalTestConducted.Text = dtTestSummary.TotalOptometristTest.ToString();
                        lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalOptometristTest);
                    }

                    DataTable dtTestDetail = dx.sp_Optometrist_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                    if (dtTestDetail != null)
                    {
                        gvRemainingList.DataSource = dtTestDetail;
                        gvRemainingList.DataBind();
                    }

                    var dtPreviousData = dx.sp_tblOptometristMasterStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "OptometristStudentId";
                            ddlPreviousTestResult.DataTextField = "OptometristStudentTransDate";
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

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);

                if (rdoOldNewTest.SelectedValue == "0")
                {
                    txtTestDate.Text = Utilities.GetDate();
                    StartButtonImpact(false);
                    //pnlTestArea.Visible = false;
                    //Div1.Visible = false;
                    //pnlRightEye_AutoRef.Visible = false;
                    //pnlLeftEye_AutoRef.Visible = false;
                    //pnlOptometristTest.Visible = false;
                    //pnlTest1_RightEye.Visible = false;
                    //pnlTest1_LeftEye.Visible = false;
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

                    var dtLastData_AutoRef = dx.sp_tblOptometristMasterTeacher_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtLastData_AutoRef != null)
                        {
                            txtTestDate.Text = DateTime.Parse(dtLastData_AutoRef.AutoRefTeacherTransDate.ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtLastData = dx.sp_tblOptometristMasterTeacher_GetLastTest(Convert.ToInt32(ID)).SingleOrDefault();
                    try
                    {
                        if (dtLastData != null)
                        {
                            txtTestDate.Text = DateTime.Parse(dtLastData.AutoRefTeacherTransDate.ToString()).ToString("dd-MMM-yyyy");

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
                        }
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message + " - " + ex.Source;
                    }

                    var dtTestSummary = dx.sp_AutoRefTest_TestSummary(DateTime.Parse(Utilities.GetDate())).SingleOrDefault();
                    if (dtTestSummary != null)
                    {
                        lblResultDate.Text = Utilities.GetDate();
                        lblTotalEnrollments.Text = dtTestSummary.TotalEnrollment.ToString();
                        lblTotalTestConducted.Text = dtTestSummary.TotalOptometristTest.ToString();
                        lblRemainingTest.Text = Convert.ToString(dtTestSummary.TotalEnrollment - dtTestSummary.TotalOptometristTest);
                    }

                    DataTable dtTestDetail = dx.sp_Optometrist_TestDetail(DateTime.Parse(Utilities.GetDate())).ToList().ToDataTable();
                    if (dtTestDetail != null)
                    {
                        gvRemainingList.DataSource = dtTestDetail;
                        gvRemainingList.DataBind();
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

                rdoOptometristTest.SelectedValue = "1";
                rdoOptometristTest_SelectedIndexChanged(null, null);

                if (rdoOldNewTest.SelectedValue == "0")
                {
                    txtTestDate.Text = Utilities.GetDate(); // DateTime.Now.ToString("dd-MMM-yyyy");

                    StartButtonImpact(false);
                    //pnlTestArea.Visible = false;
                    //Div1.Visible = false;
                    //pnlRightEye_AutoRef.Visible = false;
                    //pnlLeftEye_AutoRef.Visible = false;
                    //pnlOptometristTest.Visible = false;
                    //pnlTest1_RightEye.Visible = false;
                    //pnlTest1_LeftEye.Visible = false;
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

                Div1.Visible = true;
                gvRemainingList.Visible = true;
            }
            else
            {
                txtTestDate.Visible = false;
                ddlPreviousTestResult.Visible = true;

                rdoType.Enabled = false;

                Div1.Visible = false;
                gvRemainingList.Visible = false;
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

            pnlRightEye_AutoRef.Visible = false;
            pnlLeftEye_AutoRef.Visible = false;

            pnlTestArea.Visible = false;
            pnlTest1_RightEye.Visible = false;
            pnlTest1_LeftEye.Visible = false;

            pnlTest2_RightEye.Visible = false;
            pnlTest2_LeftEye.Visible = false;

            pnlTest3_RightEye.Visible = false;
            pnlTest3_LeftEye.Visible = false;

            //pnl4thTest.Visible = false;

            pnlOphthalmoscope_RightEye.Visible = false;
            pnlOphthalmoscope_LeftEye.Visible = false;

            //pnlAmplophobia.Visible = false;

            pnlTest2b.Visible = false;
            pnlTest4a.Visible = false;
            pnlTest4e_RightEye.Visible = false;
            pnlTest4e_LeftEye.Visible = false;
            pnlTestSummary.Visible = false;

            if (rdoOptometristTest.SelectedValue == "1")
            {
                pnlRightEye_AutoRef.Visible = true;
                pnlLeftEye_AutoRef.Visible = true;

                pnlStudent_Sub.Visible = true;
                pnlTeacher_Sub.Visible = true;

                pnlTestArea.Visible = true;
                pnlTest1_RightEye.Visible = true;
                pnlTest1_LeftEye.Visible = true;

                chkChiefComplain.Visible = true;
                if (chkChiefComplain.Checked == true)
                {
                    txtChiefComplain.Visible = true;
                }
                chkOccularHistory.Visible = true;
                if (chkOccularHistory.Checked == true)
                {
                    txtOccularHistory.Visible = true;
                }
                chkMedicalHistory.Visible = true;
                if (chkMedicalHistory.Checked == true)
                {
                    txtMedicalHistory.Visible = true;
                }

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoDistanceVision_RightEye_Unaided.Focus();
                if (rdoOldNewTest.SelectedValue == "1")
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        txtStudentCode.Enabled = true;
                        txtStudentName.Enabled = true;
                        btnLookupStudent.Visible = true;
                    }
                    else
                    {
                        txtTeacherCode.Enabled = true;
                        txtTeacherName.Enabled = true;
                        btnLookupTeacher.Visible = true;
                    }

                    Div1.Visible = false;
                    gvRemainingList.Visible = false;
                }
            }
            else if (rdoOptometristTest.SelectedValue == "2")
            {
                pnlTest2_RightEye.Visible = true;
                pnlTest2_LeftEye.Visible = true;
                pnlTest2b.Visible = true;

                chkChiefComplain.Visible = false;
                txtChiefComplain.Visible = false;
                chkOccularHistory.Visible = false;
                txtOccularHistory.Visible = false;
                chkMedicalHistory.Visible = false;
                txtMedicalHistory.Visible = false;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                ddlSpherical_RightEye.Focus();
                if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "0")
                {
                    txtSpherical_RightEye.Text = "00.00";
                    txtCyclinderical_RightEye.Text = "00.00";
                    txtAxixA_RightEye.Text = "0";

                    txtSpherical_RightEye.Enabled = false;
                    txtCyclinderical_RightEye.Enabled = false;
                    txtAxixA_RightEye.Enabled = false;
                }
                else
                {
                    txtSpherical_RightEye.Enabled = true;
                    txtCyclinderical_RightEye.Enabled = true;
                    txtAxixA_RightEye.Enabled = true;
                }

                if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "0")
                {
                    txtSpherical_LeftEye.Text = "00.00";
                    txtCyclinderical_LeftEye.Text = "00.00";
                    txtAxixA_LeftEye.Text = "0";

                    txtSpherical_LeftEye.Enabled = false;
                    txtCyclinderical_LeftEye.Enabled = false;
                    txtAxixA_LeftEye.Enabled = false;
                }
                else
                {
                    txtSpherical_LeftEye.Enabled = true;
                    txtCyclinderical_LeftEye.Enabled = true;
                    txtAxixA_LeftEye.Enabled = true;
                }

                if (rdoOldNewTest.SelectedValue == "1")
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        txtStudentCode.Enabled = false;
                        txtStudentName.Enabled = false;
                        btnLookupStudent.Visible = false;
                    }
                    else
                    {
                        txtTeacherCode.Enabled = false;
                        txtTeacherName.Enabled = false;
                        btnLookupTeacher.Visible = false;
                    }
                }
            }
            else if (rdoOptometristTest.SelectedValue == "3")
            {
                pnlTest3_RightEye.Visible = true;
                pnlTest3_LeftEye.Visible = true;

                chkChiefComplain.Visible = false;
                txtChiefComplain.Visible = false;
                chkOccularHistory.Visible = false;
                txtOccularHistory.Visible = false;
                chkMedicalHistory.Visible = false;
                txtMedicalHistory.Visible = false;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoRetinoScopy_RightEye.Focus();

                if (rdoOldNewTest.SelectedValue == "1")
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        txtStudentCode.Enabled = false;
                        txtStudentName.Enabled = false;
                        btnLookupStudent.Visible = false;
                    }
                    else
                    {
                        txtTeacherCode.Enabled = false;
                        txtTeacherName.Enabled = false;
                        btnLookupTeacher.Visible = false;
                    }
                }
            }
            else if (rdoOptometristTest.SelectedValue == "4")
            {
                pnlOphthalmoscope_RightEye.Visible = true;
                pnlOphthalmoscope_LeftEye.Visible = true;
                pnlTest4a.Visible = true;
                pnlTest4e_RightEye.Visible = true;
                pnlTest4e_LeftEye.Visible = true;

                chkChiefComplain.Visible = false;
                txtChiefComplain.Visible = false;
                chkOccularHistory.Visible = false;
                txtOccularHistory.Visible = false;
                chkMedicalHistory.Visible = false;
                txtMedicalHistory.Visible = false;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                rdoHirchberg_Distance.Focus();

                rdoHirchberg_Distance_SelectedIndexChanged(null, null);

                if (rdoOldNewTest.SelectedValue == "1")
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        txtStudentCode.Enabled = false;
                        txtStudentName.Enabled = false;
                        btnLookupStudent.Visible = false;
                    }
                    else
                    {
                        txtTeacherCode.Enabled = false;
                        txtTeacherName.Enabled = false;
                        btnLookupTeacher.Visible = false;
                    }
                }
            }
            else if (rdoOptometristTest.SelectedValue == "5")
            {
                //pnlOphthalmoscope_RightEye.Visible = true;
                //pnlOphthalmoscope_LeftEye.Visible = true;
                //pnlTest4a.Visible = true;
                //pnlTest4e_RightEye.Visible = true;
                //pnlTest4e_LeftEye.Visible = true;

                //chkChiefComplain.Visible = false;
                //txtChiefComplain.Visible = false;
                //chkOccularHistory.Visible = false;
                //txtOccularHistory.Visible = false;
                //chkMedicalHistory.Visible = false;
                //txtMedicalHistory.Visible = false;

                pnlTestSummary.Visible = true;

                lblTestName.Text = rdoOptometristTest.SelectedItem.Text.Trim();
                //rdoHirchberg_Distance.Focus();
                if (rdoType.SelectedValue == "0")
                {
                    DateTime dt;
                    try
                    {
                        if (rdoOldNewTest.SelectedValue == "0")
                        {
                            dt = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dt = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        dt = DateTime.Parse(txtTestDate.Text);
                    }

                    var dtAutoRefTest_Student = dx.sp_AutoRefTest_StudentVisit(int.Parse(hfStudentIDPKID.Value), dt).SingleOrDefault();
                    if (dtAutoRefTest_Student != null)
                    {
                        lblAutoRef_RightEye.Text = dtAutoRefTest_Student.Spherical__Right_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Right_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Right_Eye_.ToString();
                        lblAutoRef_LeftEye.Text = dtAutoRefTest_Student.Spherical__Left_Eye_.ToString() + " / " + dtAutoRefTest_Student.Cyclinderical__Left_Eye_.ToString() + " x " + dtAutoRefTest_Student.Axix__Left_Eye_.ToString();
                    }

                    var dtOptometrist_Student = dx.sp_Optometrist_StudentVisit(int.Parse(hfStudentIDPKID.Value), dt).SingleOrDefault();

                    if (dtOptometrist_Student != null)
                    {
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
                        lblNearAdd_RightEye.Text = dtOptometrist_Student.Near_Add__Right_Eye_.ToString();

                        lblDistance_LeftEye.Text = dtOptometrist_Student.Spherical__Left_Eye_.ToString() + " / " + dtOptometrist_Student.Cyclinderical__Left_Eye_.ToString() + " x " + dtOptometrist_Student.Axix__Left_Eye_.ToString();
                        lblNearAdd_LeftEye.Text = dtOptometrist_Student.Near_Add__Left_Eye_.ToString();

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
                else
                {

                    DateTime dt;
                    try
                    {
                        if (rdoOldNewTest.SelectedValue == "0")
                        {
                            dt = DateTime.Parse(txtTestDate.Text);
                        }
                        else
                        {
                            dt = DateTime.Parse(ddlPreviousTestResult.SelectedItem.Text.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        dt = DateTime.Parse(txtTestDate.Text);
                    }
                    var dtAutoRefTest_Teacher = dx.sp_AutoRefTest_TeacherVisit(int.Parse(hfTeacherIDPKID.Value), dt).SingleOrDefault();
                    if (dtAutoRefTest_Teacher != null)
                    {
                        lblAutoRef_RightEye.Text = dtAutoRefTest_Teacher.Spherical__Right_Eye_.ToString() + " / " + dtAutoRefTest_Teacher.Cyclinderical__Right_Eye_.ToString() + " x " + dtAutoRefTest_Teacher.Axix__Right_Eye_.ToString();
                        lblAutoRef_LeftEye.Text = dtAutoRefTest_Teacher.Spherical__Left_Eye_.ToString() + " / " + dtAutoRefTest_Teacher.Cyclinderical__Left_Eye_.ToString() + " x " + dtAutoRefTest_Teacher.Axix__Left_Eye_.ToString();
                    }
                    var dtOptometrist_Teacher = dx.sp_Optometrist_TeacherVisit(int.Parse(hfTeacherIDPKID.Value), dt).SingleOrDefault();

                    if (dtOptometrist_Teacher != null)
                    {
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
                        lblNearAdd_RightEye.Text = dtOptometrist_Teacher.Near_Add__Right_Eye_.ToString();

                        lblDistance_LeftEye.Text = dtOptometrist_Teacher.Spherical__Left_Eye_.ToString() + " / " + dtOptometrist_Teacher.Cyclinderical__Left_Eye_.ToString() + " x " + dtOptometrist_Teacher.Axix__Left_Eye_.ToString();
                        lblNearAdd_LeftEye.Text = dtOptometrist_Teacher.Near_Add__Left_Eye_.ToString();

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
                }

                if (rdoOldNewTest.SelectedValue == "1")
                {
                    if (rdoType.SelectedValue == "0")
                    {
                        txtStudentCode.Enabled = false;
                        txtStudentName.Enabled = false;
                        btnLookupStudent.Visible = false;
                    }
                    else
                    {
                        txtTeacherCode.Enabled = false;
                        txtTeacherName.Enabled = false;
                        btnLookupTeacher.Visible = false;
                    }
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

                //if (rdoOldNewTest.SelectedValue == "0")
                //{
                if (rdoType.SelectedValue == "0")
                {
                    if (ValidateInputStudent())
                    {
                        //var autoRefTransId = dx.sp_tblAutoRefTestStudent_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;

                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        DateTime dtTest;
                        dtTest = DateTime.Parse(txtTestDate.Text);

                        //var res = dx.sp_tblOptometristMasterStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfStudentIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    hfAutoRefTestIDPKID.Value = res.OptometristStudentId.ToString();
                        //    //lbl_error.Text = res.RetMessage;
                        //    if (rdoOptometristTest.SelectedValue == "1" && chkNeedsCycloRefraction_RightEye.Checked == true)
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "1")
                        //    {
                        //        //error here...
                        //        rdoOptometristTest.SelectedValue = "2";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //        if (rdoOldNewTest.SelectedValue == "0")
                        //        {
                        //            if (txtSpherical_RightEye.Text == "0.00" && txtCyclinderical_RightEye.Text == "0.00"
                        //                    && txtAxixA_RightEye.Text == "0" && txtNear_RightEye.Text == "0.00"
                        //                    && txtSpherical_LeftEye.Text == "0.00" && txtCyclinderical_LeftEye.Text == "0.00"
                        //                    && txtAxixA_LeftEye.Text == "0" && txtNear_LeftEye.Text == "0.00")
                        //            {
                        //                txtSpherical_RightEye.Text = "";
                        //                txtCyclinderical_RightEye.Text = "";
                        //                txtAxixA_RightEye.Text = "";
                        //                txtNear_RightEye.Text = "";
                        //                txtSpherical_LeftEye.Text = "";
                        //                txtCyclinderical_LeftEye.Text = "";
                        //                txtAxixA_LeftEye.Text = "";
                        //                txtNear_LeftEye.Text = "";
                        //            }

                        //            if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "0")
                        //            {
                        //                txtSpherical_RightEye.Text = "0.00";
                        //            }

                        //            if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "0")
                        //            {
                        //                txtSpherical_LeftEye.Text = "0.00";
                        //            }
                        //        }

                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "2" && chkObjectiveRefraction.Checked == false)
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "2")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "3";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "3")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "4")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "5";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "5")
                        //    {
                        //        hfTeacherIDPKID.Value = "0";

                        //        Session["rdoType"] = rdoType.SelectedValue;
                        //        Session["Id"] = hfStudentIDPKID.Value;
                        //        Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                        //        Session["TransactionId"] = hfAutoRefTestIDPKID.Value;

                        //        Response.Redirect("~/Treatment.aspx?redirect=1");

                        //        //rdoOptometristTest.SelectedValue = "5";
                        //        //rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }

                        //    //ClearForm();
                        //    //ShowConfirmAddMoreRecord();

                        //    //txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
                    }
                }
                else
                {
                    if (ValidateInputTeacher())
                    {
                        //var autoRefTransId = dx.sp_tblAutoRefTestTeacher_GetMaxCode().SingleOrDefault();
                        //hfAutoRefTestTransID.Value = autoRefTransId;

                        int iGlassType_RightEye = 0;
                        iGlassType_RightEye = 0;
                        int iGlassType_LeftEye = 0;
                        iGlassType_LeftEye = 0;

                        int iOccularHistory = 0;
                        if (chkOccularHistory.Checked == true) { iOccularHistory = 1; }

                        int iMedicalHistory = 0;
                        if (chkMedicalHistory.Checked == true) { iMedicalHistory = 1; }

                        int iChiefComplain = 0;
                        if (chkChiefComplain.Checked == true) { iChiefComplain = 1; }

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

                        int iPupillaryReactions_LeftEye = 0;
                        try { iPupillaryReactions_LeftEye = int.Parse(rdoPupillaryReactions_LeftEye.SelectedValue); }
                        catch { iPupillaryReactions_LeftEye = 0; }

                        int iCoverUncovertTest_LeftEye = 0;
                        try { iCoverUncovertTest_LeftEye = int.Parse(rdoCoverUncovertTest_LeftEye.SelectedValue); }
                        catch { iCoverUncovertTest_LeftEye = 0; }

                        DateTime dtTest;
                        dtTest = DateTime.Parse(txtTestDate.Text);

                        //var res = dx.sp_tblOptometristMasterTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), dtTest,
                        //    Convert.ToInt32(hfTeacherIDPKID.Value), iChiefComplain, txtChiefComplain.Text, iOccularHistory, txtOccularHistory.Text, iMedicalHistory, txtMedicalHistory.Text,
                        //    iDistanceVision_RightEye_Unaided, iDistanceVision_RightEye_WithGlasses, iDistanceVision_RightEye_PinHole, iNearVision_RightEye, iNeedsCycloRefraction_RightEye, txtNeedsCycloRefraction_RightEye.Text,
                        //    iDistanceVision_LeftEye_Unaided, iDistanceVision_LeftEye_WithGlasses, iDistanceVision_LeftEye_Pinhole, iNearVision_LeftEye, iNeedsCycloRefraction_LeftEye, txtNeedsCycloRefraction_LeftEye.Text,
                        //    sSpherical_RightEyeType, dtxtSpherical_RightEye, sCyclinderical_RightEyeType, dCyclinderical_RightEye, dAxixA_RightEye, dAxixB_RightEye, sNear_RightEyeType, dNear_RightEye,
                        //    sSpherical_LeftEyeType, dtxtSpherical_LeftEye, sCyclinderical_LeftEyeType, dCyclinderical_LeftEye, dAxixA_LeftEye, dAxixB_LeftEye, sNear_LeftEyeType, dNear_LeftEye, iDouchrome, strAchromatopsia,
                        //    iRetinoScopy_RightEye, strCycloplegicRefraction_RightEye, txtCondition_RightEye.Text, txtMeridian1_RightEye.Text, txtMeridian2_RightEye.Text, txtFinalPrescription_RightEye.Text,
                        //    iRetinoScopy_LeftEye, strCycloplegicRefraction_LeftEye, txtCondition_LeftEye.Text, txtMeridian1_LeftEye.Text, txtMeridian2_LeftEye.Text, txtFinalPrescription_LeftEye.Text,
                        //    iHirchberg_Distance, iHirchberg_Near, iOphthalmoscope_RightEye, iPupillaryReactions_RightEye, iCoverUncovertTest_RightEye, txtCoverUncovertTestRemarks_RightEye.Text, txtExtraOccularMuscleRemarks_RightEye.Text,
                        //    iOphthalmoscope_LeftEye, iPupillaryReactions_LeftEye, iCoverUncovertTest_LeftEye, txtCoverUncovertTestRemarks_LeftEye.Text, txtExtraOccularMuscleRemarks_LeftEye.Text,
                        //    strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        //if (res.ResponseCode == 1)
                        //{
                        //    hfAutoRefTestIDPKID.Value = res.OptometristTeacherId.ToString();
                        //    //lbl_error.Text = res.RetMessage;

                        //    if (rdoOptometristTest.SelectedValue == "1" && chkNeedsCycloRefraction_RightEye.Checked == true)
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "1")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "2";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);

                        //        if (txtSpherical_RightEye.Text == "0.00" && txtCyclinderical_RightEye.Text == "0.00"
                        //                && txtAxixA_RightEye.Text == "0" && txtNear_RightEye.Text == "0.00"
                        //                && txtSpherical_LeftEye.Text == "0.00" && txtCyclinderical_LeftEye.Text == "0.00"
                        //                && txtAxixA_LeftEye.Text == "0" && txtNear_LeftEye.Text == "0.00")
                        //        {
                        //            txtSpherical_RightEye.Text = "";
                        //            txtCyclinderical_RightEye.Text = "";
                        //            txtAxixA_RightEye.Text = "";
                        //            txtNear_RightEye.Text = "";
                        //            txtSpherical_LeftEye.Text = "";
                        //            txtCyclinderical_LeftEye.Text = "";
                        //            txtAxixA_LeftEye.Text = "";
                        //            txtNear_LeftEye.Text = "";
                        //        }

                        //        if (rdoDistanceVision_RightEye_Unaided.SelectedValue == "0")
                        //        {
                        //            txtSpherical_RightEye.Text = "0.00";
                        //        }

                        //        if (rdoDistanceVision_LeftEye_Unaided.SelectedValue == "0")
                        //        {
                        //            txtSpherical_LeftEye.Text = "0.00";
                        //        }

                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "2" && chkObjectiveRefraction.Checked == false)
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "2")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "3";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "3")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "4";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "4")
                        //    {
                        //        rdoOptometristTest.SelectedValue = "5";
                        //        rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }
                        //    else if (rdoOptometristTest.SelectedValue == "5")
                        //    {
                        //        hfStudentIDPKID.Value = "0";
                        //        Session["rdoType"] = rdoType.SelectedValue;
                        //        Session["Id"] = hfTeacherIDPKID.Value;
                        //        Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                        //        Session["TransactionId"] = hfAutoRefTestIDPKID.Value;

                        //        Response.Redirect("~/Treatment.aspx?redirect=1");

                        //        //rdoOptometristTest.SelectedValue = "5";
                        //        //rdoOptometristTest_SelectedIndexChanged(null, null);
                        //    }


                        //    //ClearForm();
                        //    //ShowConfirmAddMoreRecord();

                        //    //txtTestDate.Text = Utilities.GetDate();
                        //}
                        //else
                        //{
                        //    lbl_error.Text = res.RetMessage;
                        //}
                    }
                }
                //}
                //else
                //{
                //    if (rdoType.SelectedValue == "0")
                //    {
                //        if (rdoOptometristTest.SelectedValue == "1" && chkNeedsCycloRefraction_RightEye.Checked == true)
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "1")
                //        {
                //            //error here...
                //            rdoOptometristTest.SelectedValue = "2";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "2" && chkObjectiveRefraction.Checked == false)
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "2")
                //        {
                //            rdoOptometristTest.SelectedValue = "3";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "3")
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "4")
                //        {
                //            rdoOptometristTest.SelectedValue = "5";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "5")
                //        {
                //            hfTeacherIDPKID.Value = "0";

                //            Session["rdoType"] = rdoType.SelectedValue;
                //            Session["Id"] = hfStudentIDPKID.Value;
                //            Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                //            Session["TransactionId"] = hfAutoRefTestIDPKID.Value;

                //            Response.Redirect("~/Treatment.aspx?redirect=1");
                //        }
                //    }
                //    else
                //    {
                //        if (rdoOptometristTest.SelectedValue == "1" && chkNeedsCycloRefraction_RightEye.Checked == true)
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "1")
                //        {
                //            rdoOptometristTest.SelectedValue = "2";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "2" && chkObjectiveRefraction.Checked == false)
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "2")
                //        {
                //            rdoOptometristTest.SelectedValue = "3";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "3")
                //        {
                //            rdoOptometristTest.SelectedValue = "4";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "4")
                //        {
                //            rdoOptometristTest.SelectedValue = "5";
                //            rdoOptometristTest_SelectedIndexChanged(null, null);
                //        }
                //        else if (rdoOptometristTest.SelectedValue == "5")
                //        {
                //            hfStudentIDPKID.Value = "0";
                //            Session["rdoType"] = rdoType.SelectedValue;
                //            Session["Id"] = hfTeacherIDPKID.Value;
                //            Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                //            Session["TransactionId"] = hfAutoRefTestIDPKID.Value;

                //            Response.Redirect("~/Treatment.aspx?redirect=1");
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Optometrist.aspx");
        }

        protected void chkOccularHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOccularHistory.Checked == true)
            {
                txtOccularHistory.Visible = true;
                txtOccularHistory.Focus();
            }
            else
            {
                txtOccularHistory.Visible = false;
            }
        }

        protected void chkMedicalHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMedicalHistory.Checked == true)
            {
                txtMedicalHistory.Visible = true;
                txtMedicalHistory.Focus();
            }
            else
            {
                txtMedicalHistory.Visible = false;
            }
        }

        protected void chkChiefComplain_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChiefComplain.Checked == true)
            {
                txtChiefComplain.Visible = true;
                txtChiefComplain.Focus();
            }
            else
            {
                txtChiefComplain.Visible = false;
            }
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

        protected void rdoHirchberg_Distance_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoHirchberg_Distance.SelectedValue == "0")
            {
                txtExtraOccularMuscleRemarks_RightEye.Text = "Full";
                txtExtraOccularMuscleRemarks_LeftEye.Text = "Full";
            }
            else
            {
                txtExtraOccularMuscleRemarks_RightEye.Text = "";
                txtExtraOccularMuscleRemarks_LeftEye.Text = "";
            }
        }

        //protected void txtSpherical_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
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

        //protected void txtAxixA_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (decimal.Parse(txtAxixA_RightEye.Text) == 0 || decimal.Parse(txtAxixA_RightEye.Text) > 180)
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

        //protected void txtSpherical_LeftEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
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
        //    ddlNear_RightEye.Focus();
        //}

        //protected void txtNear_RightEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!(txtNear_RightEye.Text == "" || txtNear_RightEye.Text == "0.00"))
        //        {
        //            txtNear_RightEye.Text = decimal.Parse(txtNear_RightEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtNear_RightEye.Text = "0.0";
        //    }
        //    ddlNear_LeftEye.Focus();
        //}

        //protected void txtNear_LeftEye_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!(txtNear_LeftEye.Text == "" || txtNear_LeftEye.Text == "0.00"))
        //        {
        //            txtNear_LeftEye.Text = decimal.Parse(txtNear_LeftEye.Text).ToString("00.00");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        txtNear_LeftEye.Text = "0.0";
        //    }
        //    rdoDouchrome.Focus();
        //}

        //protected void ddlSpherical_RightEye_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlSpherical_RightEye.SelectedItem.Text == "Plano")
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