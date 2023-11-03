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
    public partial class HospitalVisitForFundoscopy : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "HospitalVisitForFundoscopy"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                InitForm();

                BindCombos();

                chkListFundoscopyfindingsRight.SelectedIndex = -1;
                chkListFundoscopyfindingsRight.SelectedValue = "-1";

                chkListFundoscopyfindingsLeft.SelectedIndex = -1;
                chkListFundoscopyfindingsLeft.SelectedValue = "-1";

                chkListFundoscopyfindingsRight_Posterior.SelectedIndex = -1;
                chkListFundoscopyfindingsRight_Posterior.SelectedValue = "-1";

                chkListFundoscopyfindingsLeft_Posterior.SelectedIndex = -1;
                chkListFundoscopyfindingsLeft_Posterior.SelectedValue = "-1";

                //rdoDiagnosis_RightEye.SelectedIndex = -1;
                //rdoDiagnosis_RightEye.SelectedValue = "-1";

                enableCheckboxSelections();

                rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                rdoSubDiagnosis_RightEye.SelectedValue = "-1";

                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedValue = "-1";

                chkListDiagRight.SelectedIndex = -1;
                chkListDiagRight.SelectedValue = "-1";

                chkListDiagLeft.SelectedIndex = -1;
                chkListDiagLeft.SelectedValue = "-1";

                chkListDiag2Right.SelectedIndex = -1;
                chkListDiag2Right.SelectedValue = "-1";

                chkListDiag2Left.SelectedIndex = -1;
                chkListDiag2Left.SelectedValue = "-1";

                rdoOldNewTest_SelectedIndexChanged(null, null);

                rdoFundoscopyfindingsType_SelectedIndexChanged(null, null);

                //rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);

                chkListDiag2Right.Visible = false;
                chkListDiag2Left.Visible = false;

                //chkSurgerySuggested.Visible = false;

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
                if (ValidateInput())
                {
                    string strLoginUserID = Utilities.GetLoginUserID();
                    string strTerminalId = Utilities.getTerminalId();
                    string strTerminalIP = Utilities.getTerminalIP();

                    int iFundoscopyfindingsType = -1;
                    if (rdoFundoscopyfindingsType.SelectedValue != "")
                    {
                        iFundoscopyfindingsType = int.Parse(rdoFundoscopyfindingsType.SelectedValue);
                    }

                    string strFundoscopyfindingsRight = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsRight.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsRight += chkListFundoscopyfindingsRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsRight = strFundoscopyfindingsRight.TrimEnd(',');


                    string strFundoscopyfindingsLeft = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsLeft.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsLeft += chkListFundoscopyfindingsLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsLeft = strFundoscopyfindingsLeft.TrimEnd(',');

                    string strFundoscopyfindingsRight_Posterior = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsRight_Posterior.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsRight_Posterior.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsRight_Posterior += chkListFundoscopyfindingsRight_Posterior.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsRight_Posterior = strFundoscopyfindingsRight_Posterior.TrimEnd(',');


                    string strFundoscopyfindingsLeft_Posterior = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsLeft_Posterior.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsLeft_Posterior.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsLeft_Posterior += chkListFundoscopyfindingsLeft_Posterior.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsLeft_Posterior = strFundoscopyfindingsLeft_Posterior.TrimEnd(',');



                    //string strDiagnosis = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis = strDiagnosis.TrimEnd(',');

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


                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }


                    string strDiagRight = string.Empty;
                    for (int i = 0; i < chkListDiagRight.Items.Count; i++)
                    {
                        if (chkListDiagRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiagRight += chkListDiagRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiagRight = strDiagRight.TrimEnd(',');

                    string strDiagLeft = string.Empty;
                    for (int i = 0; i < chkListDiagLeft.Items.Count; i++)
                    {
                        if (chkListDiagLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiagLeft += chkListDiagLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiagLeft = strDiagLeft.TrimEnd(',');


                    int iSurgerySuggested = 0;
                    if (chkSurgerySuggested.Checked == true)
                    {
                        iSurgerySuggested = 1;
                    }

                    string strDiag2Right = string.Empty;
                    for (int i = 0; i < chkListDiag2Right.Items.Count; i++)
                    {
                        if (chkListDiag2Right.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiag2Right += chkListDiag2Right.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiag2Right = strDiag2Right.TrimEnd(',');

                    string strDiag2Left = string.Empty;
                    for (int i = 0; i < chkListDiag2Left.Items.Count; i++)
                    {
                        if (chkListDiag2Left.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiag2Left += chkListDiag2Left.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiag2Left = strDiag2Left.TrimEnd(',');

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

                    DateTime dtSurgery = DateTime.Parse("1900-01-01");
                    int iSurgery = 0;
                    if (chkSurgery.Checked == true)
                    {
                        iSurgery = 1;
                        if (txtSurgeryDate.Text != "")
                        {
                            dtSurgery = DateTime.Parse(txtSurgeryDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitFundoscopyStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                            Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlDoctor.SelectedValue),
                                            iFundoscopyfindingsType, strFundoscopyfindingsRight, strFundoscopyfindingsLeft, txtRemarks.Text,
                                            strFundoscopyfindingsRight_Posterior, strFundoscopyfindingsLeft_Posterior,
                                            //strDiagnosis, txtDiagnosis_RightEye.Text, iSubDiagnosis_RightEye.ToString(), 
                                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye,
                                            iTreatment_Glasses,
                                            strDiagRight, strDiagLeft, txtMedicinesPrescribed.Text,
                                            txtOphthalmologistremarks.Text, iSurgerySuggested, strDiag2Right, strDiag2Left, iRoutineCheckup, dtRoutineCheck,
                                            iFurtherAssessment, dtFurtherAssessment, iSurgery, dtSurgery,
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

                    int iFundoscopyfindingsType = -1;
                    if (rdoFundoscopyfindingsType.SelectedValue != "")
                    {
                        iFundoscopyfindingsType = int.Parse(rdoFundoscopyfindingsType.SelectedValue);
                    }

                    string strFundoscopyfindingsRight = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsRight.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsRight += chkListFundoscopyfindingsRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsRight = strFundoscopyfindingsRight.TrimEnd(',');


                    string strFundoscopyfindingsLeft = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsLeft.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsLeft += chkListFundoscopyfindingsLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsLeft = strFundoscopyfindingsLeft.TrimEnd(',');

                    string strFundoscopyfindingsRight_Posterior = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsRight_Posterior.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsRight_Posterior.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsRight_Posterior += chkListFundoscopyfindingsRight_Posterior.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsRight_Posterior = strFundoscopyfindingsRight_Posterior.TrimEnd(',');


                    string strFundoscopyfindingsLeft_Posterior = string.Empty;
                    for (int i = 0; i < chkListFundoscopyfindingsLeft_Posterior.Items.Count; i++)
                    {
                        if (chkListFundoscopyfindingsLeft_Posterior.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strFundoscopyfindingsLeft_Posterior += chkListFundoscopyfindingsLeft_Posterior.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strFundoscopyfindingsLeft_Posterior = strFundoscopyfindingsLeft_Posterior.TrimEnd(',');



                    //string strDiagnosis = string.Empty;

                    //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                    //{
                    //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                    //    {
                    //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                    //    }
                    //}
                    //strDiagnosis = strDiagnosis.TrimEnd(',');

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

                    int iTreatment_Glasses = -1;
                    if (rdoTreatment_Glasses.SelectedValue != "")
                    {
                        iTreatment_Glasses = int.Parse(rdoTreatment_Glasses.SelectedValue);
                    }


                    string strDiagRight = string.Empty;
                    for (int i = 0; i < chkListDiagRight.Items.Count; i++)
                    {
                        if (chkListDiagRight.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiagRight += chkListDiagRight.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiagRight = strDiagRight.TrimEnd(',');

                    string strDiagLeft = string.Empty;
                    for (int i = 0; i < chkListDiagLeft.Items.Count; i++)
                    {
                        if (chkListDiagLeft.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiagLeft += chkListDiagLeft.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiagLeft = strDiagLeft.TrimEnd(',');


                    int iSurgerySuggested = 0;
                    if (chkSurgerySuggested.Checked == true)
                    {
                        iSurgerySuggested = 1;
                    }

                    string strDiag2Right = string.Empty;
                    for (int i = 0; i < chkListDiag2Right.Items.Count; i++)
                    {
                        if (chkListDiag2Right.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiag2Right += chkListDiag2Right.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiag2Right = strDiag2Right.TrimEnd(',');

                    string strDiag2Left = string.Empty;
                    for (int i = 0; i < chkListDiag2Left.Items.Count; i++)
                    {
                        if (chkListDiag2Left.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            strDiag2Left += chkListDiag2Left.Items[i].Value + ","; // add selected Item text to the String .  
                        }
                    }
                    strDiag2Left = strDiag2Left.TrimEnd(',');

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

                    DateTime dtSurgery = DateTime.Parse("1900-01-01");
                    int iSurgery = 0;
                    if (chkSurgery.Checked == true)
                    {
                        iSurgery = 1;
                        if (txtSurgeryDate.Text != "")
                        {
                            dtSurgery = DateTime.Parse(txtSurgeryDate.Text);
                        }
                    }

                    var res = dx.sp_tblVisitFundoscopyStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                                            Convert.ToInt32(hfStudentIDPKID.Value), Convert.ToInt32(ddlHospital.SelectedValue), Convert.ToInt32(ddlDoctor.SelectedValue),
                                            iFundoscopyfindingsType, strFundoscopyfindingsRight, strFundoscopyfindingsLeft, txtRemarks.Text,
                                            strFundoscopyfindingsRight_Posterior, strFundoscopyfindingsLeft_Posterior,
                                            //strDiagnosis, txtDiagnosis_RightEye.Text, iSubDiagnosis_RightEye.ToString(), 
                                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye,
                                            iTreatment_Glasses,
                                            strDiagRight, strDiagLeft, txtMedicinesPrescribed.Text,
                                            txtOphthalmologistremarks.Text, iSurgerySuggested, strDiag2Right, strDiag2Left, iRoutineCheckup, dtRoutineCheck,
                                            iFurtherAssessment, dtFurtherAssessment, iSurgery, dtSurgery,
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
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(hfStudentIDPKID.Value) > 0)
                {
                    var res = dx.sp_tblVisitFundoscopyStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

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

            if (ddlDoctor.SelectedValue == "0")
            {
                lbl_error.Text = "Doctor is required.";
                ddlDoctor.Focus();
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

            hfLookupResultSchool.Value = "0";
            hfLookupResultStudent.Value = "0";

            ddlHospital.SelectedValue = "0";
            ddlDoctor.SelectedValue = "0";

            //chkListFundoscopyfindingsRight.Items.Clear();
            //chkListFundoscopyfindingsLeft.Items.Clear();

            //chkListFundoscopyfindingsRight_Posterior.Items.Clear();
            //chkListFundoscopyfindingsLeft_Posterior.Items.Clear();

            chkListFundoscopyfindingsRight.ClearSelection();
            chkListFundoscopyfindingsLeft.ClearSelection();
            chkListFundoscopyfindingsRight_Posterior.ClearSelection();
            chkListFundoscopyfindingsLeft_Posterior.ClearSelection();

            //rdoDiagnosis_RightEye.SelectedIndex = -1;
            //txtDiagnosis_RightEye.Text = "";
            //rdoSubDiagnosis_RightEye.SelectedIndex = -1;

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

            rdoTreatment_Glasses.SelectedIndex = -1;

            txtRemarks.Text = "";

            //chkListDiagRight.Items.Clear();
            //chkListDiagLeft.Items.Clear();

            chkListDiagRight.ClearSelection();
            chkListDiagLeft.ClearSelection();

            txtMedicinesPrescribed.Text = "";
            txtOphthalmologistremarks.Text = "";

            chkSurgerySuggested.Checked = false;

            //chkListDiag2Right.Items.Clear();
            //chkListDiag2Left.Items.Clear();

            chkListDiag2Right.ClearSelection();
            chkListDiag2Left.ClearSelection();

            chkRoutineCheckup.Checked = false;
            txtRoutineCheckupDate.Text = "";

            chkFurtherAssessment.Checked = false;
            txtFurtherAssessmentDate.Text = "";

            chkSurgery.Checked = false;
            txtSurgeryDate.Text = "";

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
                DataTable data = (from a in dx.sp_GetLookupData_Student_School_FundoScopy(iType)
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
                DataTable data = (from a in dx.sp_GetLookupData_Student_FundoScopy(Convert.ToInt32(hfSchoolIDPKID.Value), 0, iType)
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
                        var dtVisitFundoscopyPreviousData = dx.sp_tblVisitFundoscopyStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                        try
                        {
                            if (dtVisitFundoscopyPreviousData.Count != 0)
                            {
                                ddlPreviousTestResult.DataSource = dtVisitFundoscopyPreviousData;
                                ddlPreviousTestResult.DataValueField = "VisitFundoscopyStudentId";
                                ddlPreviousTestResult.DataTextField = "VisitFundoscopyStudentTransDate";
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
            Response.Redirect("~/HospitalVisitForFundoscopy.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnConfirmNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ModalHide", "HideBootstrapModal();", true);
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HospitalVisitForFundoscopy.aspx");
        }

        protected void txtStudentName_TextChanged(object sender, EventArgs e)
        {
            string sStudentName = txtStudentName.Text;

            txtStudentName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sStudentName.ToLower());

            //var studentCode = dx.sp_tblStudent_GetMaxCode(txtSchoolCode.Text).SingleOrDefault();
            //txtStudentCode.Text = studentCode;

            ddlHospital.Focus();
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
                var dt = dx.sp_tblVisitFundoscopyStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                hfAutoRefTestTransID.Value = dt.VisitFundoscopyStudentId.ToString();
                //hfAutoRefTestTransDate.Value = dt.VisitFundoscopyStudentTransDate.ToString();

                ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                ddlDoctor.SelectedValue = dt.DoctorAutoId.ToString();

                rdoFundoscopyfindingsType.SelectedValue = dt.FundoscopyType.ToString();
                rdoFundoscopyfindingsType_SelectedIndexChanged(null, null);

                string strFundoscopyfindings_RightEye = dt.Fundoscopyfindings_RightEye.ToString();
                string[] items_Right = strFundoscopyfindings_RightEye.Split(',');
                for (int i = 0; i < chkListFundoscopyfindingsRight.Items.Count; i++)
                {
                    if (items_Right.Contains(chkListFundoscopyfindingsRight.Items[i].Value))
                    {
                        chkListFundoscopyfindingsRight.Items[i].Selected = true;
                    }
                }

                string strFundoscopyfindings_LeftEye = dt.Fundoscopyfindings_LeftEye.ToString();
                string[] items_Left = strFundoscopyfindings_LeftEye.Split(',');
                for (int i = 0; i < chkListFundoscopyfindingsLeft.Items.Count; i++)
                {
                    if (items_Left.Contains(chkListFundoscopyfindingsLeft.Items[i].Value))
                    {
                        chkListFundoscopyfindingsLeft.Items[i].Selected = true;
                    }
                }

                string strFundoscopyfindings_RightEye_Posterior = dt.Fundoscopyfindings_RightEye_Posterior.ToString();
                string[] items_Right_Posterior = strFundoscopyfindings_RightEye_Posterior.Split(',');
                for (int i = 0; i < chkListFundoscopyfindingsRight_Posterior.Items.Count; i++)
                {
                    if (items_Right_Posterior.Contains(chkListFundoscopyfindingsRight_Posterior.Items[i].Value))
                    {
                        chkListFundoscopyfindingsRight_Posterior.Items[i].Selected = true;
                    }
                }

                string strFundoscopyfindings_LeftEye_Posterior = dt.Fundoscopyfindings_LeftEye_Posterior.ToString();
                string[] items_Left_Posterior = strFundoscopyfindings_LeftEye_Posterior.Split(',');
                for (int i = 0; i < chkListFundoscopyfindingsLeft_Posterior.Items.Count; i++)
                {
                    if (items_Left_Posterior.Contains(chkListFundoscopyfindingsLeft_Posterior.Items[i].Value))
                    {
                        chkListFundoscopyfindingsLeft_Posterior.Items[i].Selected = true;
                    }
                }

                txtRemarks.Text = dt.Remarks_Fundoscopyfindings.ToString();

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

                string strDiagnosis_RightEye = dt.Diagnosis_RightEye.ToString();
                string[] itemsDiagnosis_Right = strDiagnosis_RightEye.Split(',');
                for (int i = 0; i < chkListDiagRight.Items.Count; i++)
                {
                    if (itemsDiagnosis_Right.Contains(chkListDiagRight.Items[i].Value))
                    {
                        chkListDiagRight.Items[i].Selected = true;
                    }
                }

                string strDiagnosis_LeftEye = dt.Diagnosis_LeftEye.ToString();
                string[] itemsDiagnosis_Left = strDiagnosis_LeftEye.Split(',');
                for (int i = 0; i < chkListDiagLeft.Items.Count; i++)
                {
                    if (itemsDiagnosis_Left.Contains(chkListDiagLeft.Items[i].Value))
                    {
                        chkListDiagLeft.Items[i].Selected = true;
                    }
                }

                txtMedicinesPrescribed.Text = dt.MedicinePrescribed.ToString();
                txtOphthalmologistremarks.Text = dt.OphthalmologistRemarks.ToString();

                if (Convert.ToInt32(dt.SurgerySuggested.ToString()) == 0)
                {
                    chkSurgerySuggested.Checked = false;
                }
                else { chkSurgerySuggested.Checked = true; }

                chkSurgerySuggested_CheckedChanged(null, null);

                string strSubDiagnosis_RightEye = dt.SubDiagnosis_RightEye.ToString();
                string[] itemsSubDiagnosis_Right = strSubDiagnosis_RightEye.Split(',');
                for (int i = 0; i < chkListDiag2Right.Items.Count; i++)
                {
                    if (itemsSubDiagnosis_Right.Contains(chkListDiag2Right.Items[i].Value))
                    {
                        chkListDiag2Right.Items[i].Selected = true;
                    }
                }

                string strSubDiagnosis_LeftEye = dt.SubDiagnosis_LeftEye.ToString();
                string[] itemsSubDiagnosis_Left = strSubDiagnosis_LeftEye.Split(',');
                for (int i = 0; i < chkListDiag2Left.Items.Count; i++)
                {
                    if (itemsSubDiagnosis_Left.Contains(chkListDiag2Left.Items[i].Value))
                    {
                        chkListDiag2Left.Items[i].Selected = true;
                    }
                }

                if (Convert.ToInt32(dt.RoutineCheckup.ToString()) == 0)
                {
                    chkRoutineCheckup.Checked = false;
                    txtRoutineCheckupDate.Text = "";
                }
                else
                {
                    chkRoutineCheckup.Checked = true;
                    txtRoutineCheckupDate.Text = dt.RoutineCheckupDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.RoutineCheckupDate.ToString()).ToString("dd-MMM-yyyy");
                }



                if (Convert.ToInt32(dt.FurtherAssessment.ToString()) == 0)
                {
                    chkFurtherAssessment.Checked = false;
                    txtFurtherAssessmentDate.Text = "";
                }
                else
                {
                    chkFurtherAssessment.Checked = true;
                    txtFurtherAssessmentDate.Text = dt.RoutineCheckupDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.FurtherAssessmentDate.ToString()).ToString("dd-MMM-yyyy");
                }



                if (Convert.ToInt32(dt.SurgeryFollowup.ToString()) == 0)
                {
                    chkSurgery.Checked = false;
                    txtSurgeryDate.Text = "";
                }
                else
                {
                    chkSurgery.Checked = true;
                    txtSurgeryDate.Text = dt.SurgeryDate.ToString() == "1900-01-01" ? "" : DateTime.Parse(dt.SurgeryDate.ToString()).ToString("dd-MMM-yyyy");
                }

                txtSchoolName.Focus();

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

        //    txtDiagnosis_RightEye.Visible = false;
        //    rdoTreatment_Glasses.SelectedIndex = -1;

        //    if (rdoDiagnosis_RightEye.Items[17].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;

        //        //pnlTreatment.Visible = true;
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
        //        txtDiagnosis_RightEye.Visible = false;

        //        //pnlTreatment.Visible = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[1].Selected == true)
        //    {
        //        rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        //pnlTreatment.Visible = true;

        //        rdoTreatment_Glasses.SelectedValue = "0";
        //    }

        //    if (rdoDiagnosis_RightEye.SelectedIndex == -1)
        //    {
        //        txtDiagnosis_RightEye.Visible = false;
        //        //pnlTreatment.Visible = false;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[0].Selected == true)
        //    {
        //        rdoSubDiagnosis_RightEye.SelectedIndex = -1;
        //        rdoTreatment_Glasses.SelectedIndex = -1;
        //        txtDiagnosis_RightEye.Visible = false;
        //        //pnlTreatment.Visible = false;
        //        rdoSubDiagnosis_RightEye.Visible = false;

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

        //    if (rdoDiagnosis_RightEye.Items[17].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;
        //    }
        //}

        protected void rdoFundoscopyfindingsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkListFundoscopyfindingsRight.Visible = true;
            chkListFundoscopyfindingsLeft.Visible = true;

            chkListFundoscopyfindingsRight_Posterior.Visible = true;
            chkListFundoscopyfindingsLeft_Posterior.Visible = true;

            //if (rdoFundoscopyfindingsType.SelectedValue == "0")
            //{
            //    chkListFundoscopyfindingsRight.Visible = true;
            //    chkListFundoscopyfindingsLeft.Visible = true;
            //}
            //else if (rdoFundoscopyfindingsType.SelectedValue == "1")
            //{
            //    chkListFundoscopyfindingsRight_Posterior.Visible = true;
            //    chkListFundoscopyfindingsLeft_Posterior.Visible = true;
            //}
        }

        protected void chkSurgerySuggested_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSurgerySuggested.Checked == true)
            {
                chkListDiag2Right.Visible = true;
                chkListDiag2Left.Visible = true;
                //chkSurgerySuggested.Visible = true;
                chkFurtherAssessment.Visible = false;
            }
            else
            {
                chkListDiag2Right.Visible = false;
                chkListDiag2Left.Visible = false;
                //chkSurgerySuggested.Visible = false;
                chkFurtherAssessment.Visible = true;
            }
        }

        protected void enableCheckboxSelections()
        {
            rdoSubDiagnosis_RightEye.Visible = false;
            rdoSubDiagnosis_LeftEye.Visible = false;
            //pnlFamilyDetail.Visible = false;

            //rdoTreatment_Glasses.Visible = false;
            //pnlVisit1.Visible = false;

            txtDiagnosis_RightEye.Visible = false;
            txtDiagnosis_LeftEye.Visible = false;

            rdoTreatment_Glasses.SelectedIndex = -1;

            if (chkOther_RightEye.Checked == true)
            {
                //pnlFamilyDetail.Visible = true;
                txtDiagnosis_RightEye.Visible = true;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;
            }
            if (chkOther_LeftEye.Checked == true)
            {
                //pnlFamilyDetail.Visible = true;
                txtDiagnosis_LeftEye.Visible = true;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;
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

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;
            }

            if (chkRefractiveError_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                //rdoNextVisit.SelectedValue = "1";
            }

            if (chkRefractiveError_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                txtDiagnosis_LeftEye.Visible = false;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                //rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                //rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_LeftEye.Visible = false;

                //pnlTreatment.Visible = true;
                //pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                //rdoNextVisit.SelectedValue = "1";
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

                //chkMedicine.ClearSelection();
                rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                //rdoNextVisit.SelectedIndex = -1;
                //rdoSurgery.SelectedIndex = -1;
                //rdoReferal.SelectedIndex = -1;

                txtDiagnosis_RightEye.Visible = false;
                //pnlTreatment.Visible = false;
                //pnlVisit1.Visible = false;
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
                //pnlFamilyDetail.Visible = false;

                //txtMotherName.Text = "";
                //txtMotherCell.Text = "";
                //txtFatherCell.Text = "";
                //txtAddress1.Text = "";
                //txtAddress2.Text = "";

                //txtDistrict.Text = "";
                //txtTown.Text = "";
                //txtCity.Text = "";

                //chkMedicine.ClearSelection();
                rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                //rdoNextVisit.SelectedIndex = -1;
                //rdoSurgery.SelectedIndex = -1;
                //rdoReferal.SelectedIndex = -1;

                txtDiagnosis_LeftEye.Visible = false;
                //pnlTreatment.Visible = false;
                //pnlVisit1.Visible = false;
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