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
    public partial class Treatment : System.Web.UI.Page
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
                BindCombos();

                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

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
                rdoNextVisit_SelectedIndexChanged(null, null);

                txtTestDate.Text = Utilities.GetDate();

                txtCity.Text = "";
                txtTestDate.Enabled = true;
                //rdoDiagnosis_RightEye.Focus();

                txtStudentCode.Enabled = true;
                txtStudentName.Enabled = true;
                btnLookupStudent.Visible = true;

                txtTeacherCode.Enabled = true;
                txtTeacherCode.Enabled = true;
                btnLookupTeacher.Visible = true;

                pnlParentRemarks.Visible = false;

                if (Request.QueryString["redirect"] != null)
                {
                    if (Session["rdoType"] != null)
                    {
                        if (Session["rdoType"].ToString() == "0")
                        {
                            rdoType.SelectedValue = Session["rdoType"].ToString();
                            rdoType_SelectedIndexChanged(null, null);

                            txtTestDate.Text = DateTime.Parse(Session["TestDate"].ToString()).ToString("dd-MMM-yyyy");
                            txtTestDate.Enabled = false;

                            hfStudentIDPKID.Value = Session["Id"].ToString();
                            hfStudentIDPKID_ValueChanged(null, null);

                            int iStudentId = int.Parse(hfStudentIDPKID.Value);
                            DateTime dtTreatment = DateTime.Parse(Session["TestDate"].ToString());

                            var dtTreatmentStudent = (from a in dx.tblTreatmentStudents
                                                      where a.StudentAutoId == iStudentId
                                                          && a.TreatmentStudentTransDate == dtTreatment
                                                      select a).SingleOrDefault();

                            if (dtTreatmentStudent != null)
                            {
                                hfAutoRefTestIDPKID.Value = dtTreatmentStudent.TreatmentStudentId.ToString();

                                rdoOldNewTest.SelectedValue = "1";
                                rdoOldNewTest_SelectedIndexChanged(null, null);

                                ddlPreviousTestResult.SelectedItem.Text = DateTime.Parse(dtTreatmentStudent.TreatmentStudentTransDate.ToString()).ToString("dd/MMM/yyyy");
                                hfAutoRefTestIDPKID_ValueChanged(null, null);

                                //if (Session["TreatmentId"] != null)
                                //{
                                //    hfAutoRefTestIDPKID.Value = Session["TreatmentId"].ToString();
                                //    hfAutoRefTestIDPKID_ValueChanged(null, null);
                                //}
                            }
                            else
                            {

                                var dtOptometristMasterStudent = (from a in dx.tblOptometristMasterStudents
                                                                  where a.StudentAutoId == iStudentId
                                                              && a.OptometristStudentTransDate == dtTreatment
                                                                  select a).SingleOrDefault();
                                if (dtOptometristMasterStudent != null)
                                {
                                    int iDistanceVision_RightEye = int.Parse(dtOptometristMasterStudent.DistanceVision_RightEye_Unaided.ToString());
                                    int iDistanceVision_LeftEye = int.Parse(dtOptometristMasterStudent.DistanceVision_LeftEye_Unaided.ToString());

                                    if (iDistanceVision_RightEye >= 3)
                                    {
                                        chkLowVision_RightEye.Checked = true;
                                    }

                                    if (iDistanceVision_LeftEye >= 3)
                                    {
                                        chkLowVision_LeftEye.Checked = true;
                                    }

                                    string sSRS = dtOptometristMasterStudent.Right_Spherical_Status.ToString();
                                    string sSLS = dtOptometristMasterStudent.Left_Spherical_Status.ToString();

                                    decimal dSRE = decimal.Parse(dtOptometristMasterStudent.Right_Spherical_Points.ToString());
                                    decimal dSLE = decimal.Parse(dtOptometristMasterStudent.Left_Spherical_Points.ToString());

                                    string sCRS = dtOptometristMasterStudent.Right_Cyclinderical_Status.ToString();
                                    string sCLS = dtOptometristMasterStudent.Left_Cyclinderical_Status.ToString();

                                    decimal dCRE = decimal.Parse(dtOptometristMasterStudent.Right_Cyclinderical_Points.ToString());
                                    decimal dCLE = decimal.Parse(dtOptometristMasterStudent.Left_Cyclinderical_Points.ToString());

                                    decimal dNLE = decimal.Parse(dtOptometristMasterStudent.Left_Near_Points.ToString());
                                    decimal dNRE = decimal.Parse(dtOptometristMasterStudent.Right_Near_Points.ToString());


                                    if (dSRE > 0 || dCRE > 0)
                                    {
                                        chkRefractiveError_RightEye.Checked = true;
                                    }
                                    if (dSLE > 0 || dCLE > 0)
                                    {
                                        chkRefractiveError_LeftEye.Checked = true;
                                    }

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

                                    if (dCRE > 0)
                                    {
                                        chkRefractiveError_RightEye.Checked = true;
                                        rdoSubDiagnosis_RightEye.SelectedValue = "3";
                                    }

                                    if (dCLE > 0)
                                    {
                                        chkRefractiveError_LeftEye.Checked = true;
                                        rdoSubDiagnosis_LeftEye.SelectedValue = "3";
                                    }

                                    enableCheckboxSelections();
                                    // rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);


                                }

                            }

                            txtStudentCode.Enabled = false;
                            txtStudentName.Enabled = false;
                            btnLookupStudent.Visible = false;
                        }
                        else
                        {
                            rdoType.SelectedValue = Session["rdoType"].ToString();
                            rdoType_SelectedIndexChanged(null, null);

                            txtTestDate.Text = DateTime.Parse(Session["TestDate"].ToString()).ToString("dd-MMM-yyyy");
                            txtTestDate.Enabled = false;

                            hfTeacherIDPKID.Value = Session["Id"].ToString();
                            hfTeacherIDPKID_ValueChanged(null, null);

                            int iTeacherId = int.Parse(hfTeacherIDPKID.Value);
                            DateTime dtTreatment = DateTime.Parse(Session["TestDate"].ToString());

                            var dtTreatmentTeacher = (from a in dx.tblTreatmentTeachers
                                                      where a.TeacherAutoId == iTeacherId
                                                          && a.TreatmentTeacherTransDate == dtTreatment
                                                      select a).SingleOrDefault();

                            if (dtTreatmentTeacher != null)
                            {
                                hfAutoRefTestIDPKID.Value = dtTreatmentTeacher.TreatmentTeacherId.ToString();

                                rdoOldNewTest.SelectedValue = "1";
                                rdoOldNewTest_SelectedIndexChanged(null, null);

                                ddlPreviousTestResult.SelectedItem.Text = DateTime.Parse(dtTreatmentTeacher.TreatmentTeacherTransDate.ToString()).ToString("dd-MMM-yyyy");

                                hfAutoRefTestIDPKID_ValueChanged(null, null);

                                //if (Session["TreatmentId"] != null)
                                //{
                                //    hfAutoRefTestIDPKID.Value = Session["TreatmentId"].ToString();
                                //    hfAutoRefTestIDPKID_ValueChanged(null, null);
                                //}
                            }
                            else
                            {
                                var dtOptometristMasterTeacher = (from a in dx.tblOptometristMasterTeachers
                                                                  where a.TeacherAutoId == iTeacherId
                                                              && a.OptometristTeacherTransDate == dtTreatment
                                                                  select a).SingleOrDefault();
                                if (dtOptometristMasterTeacher != null)
                                {
                                    int iDistanceVision_RightEye = int.Parse(dtOptometristMasterTeacher.DistanceVision_RightEye_Unaided.ToString());
                                    int iDistanceVision_LeftEye = int.Parse(dtOptometristMasterTeacher.DistanceVision_LeftEye_Unaided.ToString());

                                    if (iDistanceVision_RightEye >= 3)
                                    {
                                        chkLowVision_RightEye.Checked = true;
                                    }

                                    if (iDistanceVision_LeftEye >= 3)
                                    {
                                        chkLowVision_LeftEye.Checked = true;
                                    }

                                    string sSRS = dtOptometristMasterTeacher.Right_Spherical_Status.ToString();
                                    string sSLS = dtOptometristMasterTeacher.Left_Spherical_Status.ToString();

                                    decimal dSRE = decimal.Parse(dtOptometristMasterTeacher.Right_Spherical_Points.ToString());
                                    decimal dSLE = decimal.Parse(dtOptometristMasterTeacher.Left_Spherical_Points.ToString());

                                    string sCRS = dtOptometristMasterTeacher.Right_Cyclinderical_Status.ToString();
                                    string sCLS = dtOptometristMasterTeacher.Left_Cyclinderical_Status.ToString();

                                    decimal dCRE = decimal.Parse(dtOptometristMasterTeacher.Right_Cyclinderical_Points.ToString());
                                    decimal dCLE = decimal.Parse(dtOptometristMasterTeacher.Left_Cyclinderical_Points.ToString());

                                    decimal dNLE = decimal.Parse(dtOptometristMasterTeacher.Left_Near_Points.ToString());
                                    decimal dNRE = decimal.Parse(dtOptometristMasterTeacher.Right_Near_Points.ToString());


                                    if (dSRE > 0 || dCRE > 0)
                                    {
                                        chkRefractiveError_RightEye.Checked = true;
                                    }
                                    if (dSLE > 0 || dCLE > 0)
                                    {
                                        chkRefractiveError_LeftEye.Checked = true;
                                    }

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

                                    if (dCRE > 0)
                                    {
                                        chkRefractiveError_RightEye.Checked = true;
                                        rdoSubDiagnosis_RightEye.SelectedValue = "3";
                                    }

                                    if (dCLE > 0)
                                    {
                                        chkRefractiveError_LeftEye.Checked = true;
                                        rdoSubDiagnosis_LeftEye.SelectedValue = "3";
                                    }

                                    enableCheckboxSelections();
                                    // rdoDiagnosis_RightEye_SelectedIndexChanged(null, null);
                                }
                            }

                            txtTeacherCode.Enabled = false;
                            txtTeacherName.Enabled = false;
                            btnLookupTeacher.Visible = false;
                        }
                    }
                }
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
            }
            else
            {
                pnlStudent.Visible = false;
                pnlStudent_Sub.Visible = false;
                pnlTeacher.Visible = true;
                pnlTeacher_Sub.Visible = true;
            }

            ClearForm();
            ClearValidation();
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
                        //string strDiagnosis = string.Empty;

                        //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                        //{
                        //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        //    {
                        //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        //    }
                        //}
                        //strDiagnosis = strDiagnosis.TrimEnd(',');
                        //if(strDiagnosis.Trim() == "") { strDiagnosis = "0"; }
                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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

                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);

                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }
                        var res = dx.sp_tblTreatmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfStudentIDPKID.Value),
                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                            txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                            iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                            iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
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
                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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

                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);

                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }
                        var res = dx.sp_tblTreatmentTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                             Convert.ToInt32(hfTeacherIDPKID.Value),
                            //strDiagnosis, txtDiagnosis_RightEye.Text,
                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                             iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                             txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                             iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                             iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
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
                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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

                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);

                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }

                        var res = dx.sp_tblTreatmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfStudentIDPKID.Value),
                            //strDiagnosis, txtDiagnosis_RightEye.Text, 
                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                            txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                            iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                            iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
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
                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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
                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);

                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }
                        var res = dx.sp_tblTreatmentTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfTeacherIDPKID.Value),
                            //strDiagnosis, txtDiagnosis_RightEye.Text,
                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                            txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                            iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                            iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
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
                        var res = dx.sp_tblTreatmentStudent_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
                        }
                        else
                        {
                            lbl_error.Text = res.RetMessage;
                        }
                    }
                    else
                    {
                        var res = dx.sp_tblTreatmentTeacher_Delete(Convert.ToInt32(hfAutoRefTestIDPKID.Value)).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            ClearForm();
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
                txtTeacherCode.Focus();
                return false;
            }

            if (hfStudentIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Student Code.";
                txtStudentCode.Focus();
                return false;
            }

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

            if (txtMotherName.Visible == true)
            {
                if (txtMotherName.Text == "")
                {
                    lbl_error.Text = "Mother Name is required.";
                    txtMotherName.Focus();
                    return false;
                }

                if (txtMotherCell.Text == "" && txtFatherCell.Text == "")
                {
                    lbl_error.Text = "Cell No. of Father or Mother is required.";
                    txtMotherCell.Focus();
                    return false;
                }
                if (txtAddress1.Text == "")
                {
                    lbl_error.Text = "Address is required.";
                    txtAddress1.Focus();
                    return false;
                }
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

            return true;
        }

        private bool ValidateInputTeacher()
        {
            ClearValidation();

            if (txtTeacherCode.Text.Trim() == "")
            {
                lbl_error.Text = "Teacher Code is required.";
                txtTeacherCode.Focus();
                return false;
            }

            if (hfTeacherIDPKID.Value == "0")
            {
                lbl_error.Text = "Invalid Teacher Code.";
                txtTeacherCode.Focus();
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

            //if (txtFatherCell.Visible == true)
            //{
            //    if (txtFatherCell.Text == "")
            //    {
            //        lbl_error.Text = "Cell No. is required.";
            //        txtFatherCell.Focus();
            //        return false;
            //    }
            //    if (txtAddress1.Text == "")
            //    {
            //        lbl_error.Text = "Address is required.";
            //        txtAddress1.Focus();
            //        return false;
            //    }
            //}

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
                txtTeacherName.Focus();
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

            txtMotherName.Text = "";
            txtMotherCell.Text = "";
            txtFatherCell.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtDistrict.Text = "";
            txtTown.Text = "";
            txtCity.Text = "";

            chkMedicine.ClearSelection();

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
            //rdoTreatment_Glasses.SelectedValue = "-1";

            rdoNextVisit.SelectedIndex = -1;
            //rdoNextVisit.SelectedValue = "-1";

            rdoSurgery.SelectedIndex = -1;
            //rdoSurgery.SelectedValue = "-1";

            rdoReferal.SelectedIndex = -1;
            //rdoReferal.SelectedValue = "-1";

            rdoYes.Checked = false;
            rdoNo.Checked = false;

            rdoNotAgrees.SelectedIndex = -1;

            txtOtherRemarks.Text = "";

            ClearValidation();

            txtStudentCode.Focus();
        }

        //private void EnableDisableControls(bool enable)
        //{
        //    ddlSpherical_RightEye.Enabled = enable;
        //    txtSpherical_RightEye.Enabled = enable;

        //    ddlCyclinderical_RightEye.Enabled = enable;
        //    txtCyclinderical_RightEye.Enabled = enable;

        //    txtAxixA_RightEye.Enabled = enable;
        //    txtAxixB_RightEye.Enabled = enable;

        //    ddlSpherical_LeftEye.Enabled = enable;
        //    txtSpherical_LeftEye.Enabled = enable;

        //    ddlCyclinderical_LeftEye.Enabled = enable;
        //    txtCyclinderical_LeftEye.Enabled = enable;

        //    txtAxixA_LeftEye.Enabled = enable;
        //    txtAxixB_LeftEye.Enabled = enable;
        //}

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
                if (rdoType.SelectedValue == "0")
                {
                    var dt = dx.sp_tblTreatmentStudent_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransDate.Value = dt.TreatmentStudentTransDate.ToString();

                    hfStudentIDPKID.Value = dt.StudentAutoId.ToString();

                    //if(dt.Daignosis.ToString() != "-1")
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

                    txtMotherName.Text = dt.MotherName.ToString();
                    txtMotherCell.Text = dt.MotherCell.ToString();
                    txtFatherCell.Text = dt.FatherCell.ToString();

                    txtAddress1.Text = dt.Address1.ToString();
                    txtAddress2.Text = dt.Address2.ToString();
                    txtDistrict.Text = dt.District.ToString();

                    txtTown.Text = dt.Town.ToString();
                    txtCity.Text = dt.City.ToString();

                    if (dt.SubTreatment.ToString() != "-1")
                    {
                        rdoTreatment_Glasses.SelectedValue = dt.SubTreatment.ToString();
                    }
                    else
                    {
                        rdoTreatment_Glasses.SelectedIndex = -1;
                    }

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

                    if (dt.NextVisit.ToString() != "-1")
                    {
                        rdoNextVisit.SelectedValue = dt.NextVisit.ToString();
                        rdoNextVisit_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        rdoNextVisit.SelectedIndex = -1;
                    }
                    //rdoNextVisit.SelectedValue = dt.NextVisit.ToString();

                    if (dt.Surgery.ToString() != "-1")
                    {
                        rdoSurgery.SelectedValue = dt.Surgery.ToString();
                    }
                    else
                    {
                        rdoSurgery.SelectedIndex = -1;
                    }
                    //rdoSurgery.SelectedValue = dt.Surgery.ToString();

                    rdoSurgery_SelectedIndexChanged(null, null);
                    if (dt.SurgeryDetail.ToString() != "-1")
                    {
                        rdoSurgery_Detail.SelectedValue = dt.SurgeryDetail.ToString();
                    }
                    else
                    {
                        rdoSurgery_Detail.SelectedIndex = -1;
                    }

                    //rdoSurgery_Detail.SelectedValue = dt.SurgeryDetail.ToString();

                    txtSurgery_Detail.Text = dt.SurgeryDetailRemarks.ToString();

                    if (dt.Referal.ToString() != "-1")
                    {
                        rdoReferal.SelectedValue = dt.Referal.ToString();
                    }
                    else
                    {
                        rdoReferal.SelectedIndex = -1;
                    }

                    int iParentAgree = int.Parse(dt.ParentAgree.ToString());
                    if (iParentAgree == 0)
                    {
                        rdoNo.Checked = true;
                        rdoNo_CheckedChanged(null, null);
                    }
                    if (iParentAgree == 1)
                    {
                        rdoYes.Checked = true;
                        rdoYes_CheckedChanged(null, null);
                    }

                    if (dt.ParentNotAgreeReason.ToString() != "-1")
                    {
                        rdoNotAgrees.SelectedValue = dt.ParentNotAgreeReason.ToString();
                        rdoNotAgrees_SelectedIndexChanged(null, null);
                        if (dt.ParentNotAgreeReason.ToString() == "3")
                        {
                            txtOtherRemarks.Text = dt.ParentNotAgreeRemarks.ToString();
                        }
                        else
                        {
                            txtOtherRemarks.Text = "";
                        }
                    }
                    else
                    {
                        rdoNotAgrees.SelectedIndex = -1;
                    }

                    ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
                }
                else
                {
                    var dt = dx.sp_tblTreatmentTeacher_GetDetail(Convert.ToInt32(autoRefTestIDPKID)).SingleOrDefault();

                    hfAutoRefTestTransDate.Value = dt.TreatmentTeacherTransDate.ToString();

                    hfTeacherIDPKID.Value = dt.TeacherAutoId.ToString();

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

                    //rdoSubDiagnosis_RightEye.SelectedValue = dt.SubDaignosis.ToString();
                    txtDiagnosis_RightEye.Text = dt.DaignosisRemarks.ToString();
                    txtDiagnosis_LeftEye.Text = dt.DaignosisRemarks_LeftEye.ToString();

                    txtMotherName.Text = dt.MotherName.ToString();
                    txtMotherCell.Text = dt.MotherCell.ToString();
                    txtFatherCell.Text = dt.FatherCell.ToString();

                    txtAddress1.Text = dt.Address1.ToString();
                    txtAddress2.Text = dt.Address2.ToString();

                    txtDistrict.Text = dt.District.ToString();
                    txtTown.Text = dt.Town.ToString();
                    txtCity.Text = dt.City.ToString();

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

                    if (dt.NextVisit.ToString() != "-1")
                    {
                        rdoNextVisit.SelectedValue = dt.NextVisit.ToString();
                        rdoNextVisit_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        rdoNextVisit.SelectedIndex = -1;
                    }
                    //rdoNextVisit.SelectedValue = dt.NextVisit.ToString();

                    if (dt.Surgery.ToString() != "-1")
                    {
                        rdoSurgery.SelectedValue = dt.Surgery.ToString();
                    }
                    else
                    {
                        rdoSurgery.SelectedIndex = -1;
                    }
                    //rdoSurgery.SelectedValue = dt.Surgery.ToString();

                    if (dt.SurgeryDetail.ToString() != "-1")
                    {
                        rdoSurgery_Detail.SelectedValue = dt.SurgeryDetail.ToString();
                    }
                    else
                    {
                        rdoSurgery_Detail.SelectedIndex = -1;
                    }
                    //rdoSurgery_Detail.SelectedValue = dt.SurgeryDetail.ToString();

                    txtSurgery_Detail.Text = dt.SurgeryDetailRemarks.ToString();

                    if (dt.Referal.ToString() != "-1")
                    {
                        rdoReferal.SelectedValue = dt.Referal.ToString();
                    }
                    else
                    {
                        rdoReferal.SelectedIndex = -1;
                    }
                    //rdoReferal.SelectedValue = dt.Referal.ToString();

                    int iParentAgree = int.Parse(dt.ParentAgree.ToString());
                    if (iParentAgree == 0)
                    {
                        rdoNo.Checked = true;
                        rdoNo_CheckedChanged(null, null);
                    }
                    if (iParentAgree == 1)
                    {
                        rdoYes.Checked = true;
                        rdoYes_CheckedChanged(null, null);
                    }

                    if (dt.ParentNotAgreeReason.ToString() != "-1")
                    {
                        rdoNotAgrees.SelectedValue = dt.ParentNotAgreeReason.ToString();

                        if (dt.ParentNotAgreeReason.ToString() == "3")
                        {
                            txtOtherRemarks.Visible = true;
                            txtOtherRemarks.Text = dt.ParentNotAgreeRemarks.ToString();
                        }
                        else
                        {
                            txtOtherRemarks.Visible = false;
                            txtOtherRemarks.Text = "";
                        }
                    }
                    else
                    {
                        rdoNotAgrees.SelectedIndex = -1;
                    }

                    ddlHospital.SelectedValue = dt.HospitalAutoId.ToString();
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

                    int iSchoolAutoId = int.Parse(dt.SchoolAutoId.ToString());
                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                    txtDistrict.Text = dtSchool.District.ToString();
                    txtTown.Text = dtSchool.Town.ToString();
                    txtCity.Text = dtSchool.City.ToString();

                    var dtPreviousData = dx.sp_tblTreatmentStudent_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "TreatmentStudentId";
                            ddlPreviousTestResult.DataTextField = "TreatmentStudentTransDate";
                            ddlPreviousTestResult.DataBind();

                            ListItem item = new ListItem();
                            item.Text = "Select";
                            item.Value = "0";
                            ddlPreviousTestResult.Items.Insert(0, item);
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

                    int iSchoolAutoId = int.Parse(dt.SchoolAutoId.ToString());
                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                    txtDistrict.Text = dtSchool.District.ToString();
                    txtTown.Text = dtSchool.Town.ToString();
                    txtCity.Text = dtSchool.City.ToString();

                    var dtPreviousData = dx.sp_tblTreatmentTeacher_GetPreviousTest(Convert.ToInt32(ID)).ToList();
                    try
                    {
                        if (dtPreviousData.Count != 0)
                        {
                            ddlPreviousTestResult.DataSource = dtPreviousData;
                            ddlPreviousTestResult.DataValueField = "TreatmentTeacherId";
                            ddlPreviousTestResult.DataTextField = "TreatmentTeacherTransDate";
                            ddlPreviousTestResult.DataBind();

                            ListItem item = new ListItem();
                            item.Text = "Select";
                            item.Value = "0";
                            ddlPreviousTestResult.Items.Insert(0, item);
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
            if (rdoOldNewTest.SelectedIndex == 0)
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
            Session["rdoType"] = null;
            Session["TestDate"] = null;
            Session["Id"] = null;
            Session["TransactionId"] = null;
            Session["TreatmentId"] = null;

            Response.Redirect("~/Optometrist.aspx");
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

            InitForm();
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
        //    rdoSubDiagnosis_RightEye.Visible = false;
        //    pnlFamilyDetail.Visible = false;

        //    pnlTreatment.Visible = false;
        //    pnlVisit1.Visible = false;

        //    txtDiagnosis_RightEye.Visible = false;
        //    rdoTreatment_Glasses.SelectedIndex = -1;            

        //    if (rdoDiagnosis_RightEye.Items[16].Selected == true)
        //    {
        //        pnlFamilyDetail.Visible = true;
        //        txtDiagnosis_RightEye.Visible = true;

        //        pnlTreatment.Visible = true;
        //        pnlVisit1.Visible = true;
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
        //        if (rdoType.SelectedValue == "0")
        //        {
        //            pnlFamilyDetail.Visible = true;

        //            lblMotherName.Visible = true;
        //            txtMotherName.Visible = true;

        //            lblMotherCell.Visible = true;
        //            txtMotherCell.Visible = true;

        //            lblFatherCell.Text = "Father's Cell";
        //        }
        //        else
        //        {
        //            pnlFamilyDetail.Visible = true;

        //            lblMotherName.Visible = false;
        //            txtMotherName.Visible = false;

        //            lblMotherCell.Visible = false;
        //            txtMotherCell.Visible = false;

        //            lblFatherCell.Text = "Teacher Cell No.";
        //        }
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        pnlVisit1.Visible = true;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[1].Selected == true)
        //    {
        //        rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        pnlVisit1.Visible = true;

        //        rdoTreatment_Glasses.SelectedValue = "0";
        //        rdoNextVisit.SelectedValue = "1";
        //    }

        //    if (rdoDiagnosis_RightEye.Items[2].Selected == true)
        //    {
        //        rdoSubDiagnosis_RightEye.Visible = true;
        //        //pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;

        //        pnlTreatment.Visible = true;
        //        pnlVisit1.Visible = true;

        //        rdoTreatment_Glasses.SelectedValue = "0";
        //        rdoNextVisit.SelectedValue = "1";
        //    }

        //    if (rdoDiagnosis_RightEye.SelectedIndex == -1)
        //    {
        //        pnlFamilyDetail.Visible = false;
        //        txtDiagnosis_RightEye.Visible = false;
        //        pnlTreatment.Visible = false;
        //        pnlVisit1.Visible = false;
        //    }

        //    if (rdoDiagnosis_RightEye.Items[0].Selected == true)
        //    {
        //        pnlFamilyDetail.Visible = false;

        //        txtMotherName.Text = "";
        //        txtMotherCell.Text = "";
        //        txtFatherCell.Text = "";
        //        txtAddress1.Text = "";
        //        txtAddress2.Text = "";
        //        //txtDistrict.Text = "";
        //        //txtTown.Text = "";
        //        //txtCity.Text = "";

        //        chkMedicine.ClearSelection();
        //        rdoSubDiagnosis_RightEye.SelectedIndex = -1;
        //        rdoTreatment_Glasses.SelectedIndex = -1;
        //        rdoNextVisit.SelectedIndex = -1;
        //        rdoSurgery.SelectedIndex = -1;
        //        rdoReferal.SelectedIndex = -1;

        //        txtDiagnosis_RightEye.Visible = false;
        //        pnlTreatment.Visible = false;
        //        pnlVisit1.Visible = false;
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

        //    if (rdoDiagnosis_RightEye.Items[16].Selected == true)
        //    {
        //        txtDiagnosis_RightEye.Visible = true;
        //    }
        //}

        protected void enableCheckboxSelections()
        {
            rdoSubDiagnosis_RightEye.Visible = false;
            rdoSubDiagnosis_LeftEye.Visible = false;
            pnlFamilyDetail.Visible = false;

            pnlTreatment.Visible = false;
            pnlVisit1.Visible = false;

            txtDiagnosis_RightEye.Visible = false;
            txtDiagnosis_LeftEye.Visible = false;

            rdoTreatment_Glasses.SelectedIndex = -1;

            if (chkOther_RightEye.Checked == true)
            {
                pnlFamilyDetail.Visible = true;
                txtDiagnosis_RightEye.Visible = true;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;
            }
            if (chkOther_LeftEye.Checked == true)
            {
                pnlFamilyDetail.Visible = true;
                txtDiagnosis_LeftEye.Visible = true;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;
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
                if (rdoType.SelectedValue == "0")
                {
                    pnlFamilyDetail.Visible = true;

                    lblMotherName.Visible = true;
                    txtMotherName.Visible = true;

                    lblMotherCell.Visible = true;
                    txtMotherCell.Visible = true;

                    lblFatherCell.Text = "Father's Cell";
                }
                else
                {
                    pnlFamilyDetail.Visible = true;

                    lblMotherName.Visible = false;
                    txtMotherName.Visible = false;

                    lblMotherCell.Visible = false;
                    txtMotherCell.Visible = false;

                    lblFatherCell.Text = "Teacher Cell No.";
                }
                txtDiagnosis_RightEye.Visible = false;
                txtDiagnosis_LeftEye.Visible = false;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;
            }

            if (chkRefractiveError_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkRefractiveError_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                txtDiagnosis_LeftEye.Visible = false;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_RightEye.Checked == true)
            {
                rdoSubDiagnosis_RightEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_RightEye.Visible = false;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;

                rdoTreatment_Glasses.SelectedValue = "0";
                rdoNextVisit.SelectedValue = "1";
            }

            if (chkLowVision_LeftEye.Checked == true)
            {
                rdoSubDiagnosis_LeftEye.Visible = true;
                //pnlFamilyDetail.Visible = false;
                txtDiagnosis_LeftEye.Visible = false;

                pnlTreatment.Visible = true;
                pnlVisit1.Visible = true;

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

                chkMedicine.ClearSelection();
                rdoSubDiagnosis_RightEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoNextVisit.SelectedIndex = -1;
                rdoSurgery.SelectedIndex = -1;
                rdoReferal.SelectedIndex = -1;

                txtDiagnosis_RightEye.Visible = false;
                pnlTreatment.Visible = false;
                pnlVisit1.Visible = false;
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

                chkMedicine.ClearSelection();
                rdoSubDiagnosis_LeftEye.SelectedIndex = -1;
                rdoTreatment_Glasses.SelectedIndex = -1;
                rdoNextVisit.SelectedIndex = -1;
                rdoSurgery.SelectedIndex = -1;
                rdoReferal.SelectedIndex = -1;

                txtDiagnosis_LeftEye.Visible = false;
                pnlTreatment.Visible = false;
                pnlVisit1.Visible = false;
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


        //protected void rdoTreatment_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    rdoTreatment_Glasses.Visible = false;
        //    if(rdoTreatment.SelectedValue == "1")
        //    {
        //        rdoTreatment_Glasses.Visible = true;
        //    }
        //}

        protected void rdoNextVisit_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSurgery.Visible = false;
            rdoSurgery_Detail.Visible = false;
            pnlParentRemarks.Visible = false;
            rdoSurgery.SelectedIndex = -1;
            rdoYes.Checked = false;
            rdoNo.Checked = false;
            rdoNotAgrees.SelectedIndex = -1;
            txtOtherRemarks.Text = "";
            if (rdoNextVisit.SelectedValue == "3")
            {
                rdoSurgery.Visible = true;
                pnlParentRemarks.Visible = true;
                //rdoSurgery.SelectedValue = "-1";
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Optometrist.aspx");
        }

        protected void rdoSurgery_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdoSurgery_Detail.Visible = false;

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

        protected void btnMovePrevious_Click(object sender, EventArgs e)
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
                        //string strDiagnosis = string.Empty;

                        //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                        //{
                        //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        //    {
                        //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        //    }
                        //}
                        //strDiagnosis = strDiagnosis.TrimEnd(',');
                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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
                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);
                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }

                        var res = dx.sp_tblTreatmentStudent_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                            Convert.ToInt32(hfStudentIDPKID.Value),
                            //strDiagnosis, txtDiagnosis_RightEye.Text, 
                            iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                            iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                            txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                            iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                            iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
                            strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            int iTreatmentId = res.TreatmentStudentId;

                            hfTeacherIDPKID.Value = "0";
                            Session["rdoType"] = rdoType.SelectedValue;
                            Session["Id"] = hfStudentIDPKID.Value;
                            Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                            Session["TreatmentId"] = iTreatmentId.ToString();

                            //ClearForm();
                            //ShowConfirmAddMoreRecord();

                            //txtTestDate.Text = Utilities.GetDate();                           

                            Response.Redirect("~/Optometrist.aspx?redirect=1");
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
                        //string strDiagnosis = string.Empty;

                        //for (int i = 0; i < rdoDiagnosis_RightEye.Items.Count; i++)
                        //{
                        //    if (rdoDiagnosis_RightEye.Items[i].Selected == true)// getting selected value from CheckBox List  
                        //    {
                        //        strDiagnosis += rdoDiagnosis_RightEye.Items[i].Value + ","; // add selected Item text to the String .  
                        //    }
                        //}
                        //strDiagnosis = strDiagnosis.TrimEnd(',');

                        //int iDiagnosis_RightEye = -1;
                        //if (rdoDiagnosis_RightEye.SelectedValue != "")
                        //{
                        //    iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);
                        //}

                        //int iDiagnosis_RightEye = int.Parse(rdoDiagnosis_RightEye.SelectedValue);

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

                        //int iSubDiagnosis_RightEye = int.Parse(rdoSubDiagnosis_RightEye.SelectedValue);

                        int iTreatment = 0; // int.Parse(rdoTreatment.SelectedValue);

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
                        if (rdoNextVisit.SelectedValue != "")
                        {
                            iNextVisit = int.Parse(rdoNextVisit.SelectedValue);
                        }
                        //int iNextVisit = int.Parse(rdoNextVisit.SelectedValue);

                        int iSurgery = -1;
                        if (rdoSurgery.SelectedValue != "")
                        {
                            iSurgery = int.Parse(rdoSurgery.SelectedValue);
                        }
                        //int iSurgery = int.Parse(rdoSurgery.SelectedValue);

                        int iSurgery_Detail = -1;
                        if (rdoSurgery_Detail.SelectedValue != "")
                        {
                            iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);
                        }
                        //int iSurgery_Detail = int.Parse(rdoSurgery_Detail.SelectedValue);

                        int iReferal = -1;
                        if (rdoReferal.SelectedValue != "")
                        {
                            iReferal = int.Parse(rdoReferal.SelectedValue);
                        }
                        //int iReferal = int.Parse(rdoReferal.SelectedValue);

                        int iParentAgree = -1;
                        if (rdoYes.Checked == true)
                        {
                            iParentAgree = 1;
                        }
                        if (rdoNo.Checked == true)
                        {
                            iParentAgree = 0;
                        }
                        int iParentNotAgreeReason = -1;
                        if (rdoNotAgrees.SelectedValue != "")
                        {
                            iParentNotAgreeReason = int.Parse(rdoNotAgrees.SelectedValue);
                        }
                        var res = dx.sp_tblTreatmentTeacher_InsertUpdate(Convert.ToInt32(hfAutoRefTestIDPKID.Value), DateTime.Parse(txtTestDate.Text),
                             Convert.ToInt32(hfTeacherIDPKID.Value),
                             //strDiagnosis, txtDiagnosis_RightEye.Text, 
                             iNormal_RightEye, iRefractiveError_RightEye, iLowVision_RightEye, iNeedsCycloplegicRefraction_RightEye
                            , iSquintStrabismus_RightEye, iLazyEyeAmblyopia_RightEye, iColorblindnessAchromatopsia_RightEye, iCataract_RightEye, iTraumaticCataract_RightEye
                            , iKeratoconus_RightEye, iAnisometropia_RightEye, iPtosis_RightEye, iNystagmus_RightEye, iCorneadefects_RightEye, iPuplidefects_RightEye
                            , iPterygium_RightEye, iOther_RightEye, txtDiagnosis_RightEye.Text, iNormal_LeftEye, iRefractiveError_LeftEye, iLowVision_LeftEye, iNeedsCycloplegicRefraction_LeftEye
                            , iSquintStrabismus_LeftEye, iLazyEyeAmblyopia_LeftEye, iColorblindnessAchromatopsia_LeftEye, iCataract_LeftEye, iTraumaticCataract_LeftEye, iKeratoconus_LeftEye
                            , iAnisometropia_LeftEye, iPtosis_LeftEye, iNystagmus_LeftEye, iCorneadefects_LeftEye, iPuplidefects_LeftEye, iPterygium_LeftEye
                            , iOther_LeftEye, txtDiagnosis_LeftEye.Text,
                             iSubDiagnosis_RightEye, iSubDiagnosis_LeftEye, txtMotherName.Text, txtMotherCell.Text, txtFatherCell.Text,
                             txtAddress1.Text, txtAddress2.Text, txtDistrict.Text, txtTown.Text, txtCity.Text, iTreatment, iTreatment_Glasses, strMedicine,
                             iNextVisit, iSurgery, iSurgery_Detail, txtSurgery_Detail.Text.Trim(), iReferal,
                             iParentAgree, iParentNotAgreeReason, txtOtherRemarks.Text, Convert.ToInt32(ddlHospital.SelectedValue),
                             strLoginUserID, DateTime.Now, "", strTerminalId, strTerminalIP).FirstOrDefault();

                        if (res.ResponseCode == 1)
                        {
                            lbl_error.Text = res.RetMessage;

                            int iTreatmentId = res.TreatmentTeacherId;

                            hfStudentIDPKID.Value = "0";
                            Session["rdoType"] = rdoType.SelectedValue;
                            Session["Id"] = hfTeacherIDPKID.Value;
                            Session["TestDate"] = DateTime.Parse(txtTestDate.Text);
                            Session["TreatmentId"] = iTreatmentId.ToString();

                            Response.Redirect("~/Optometrist.aspx?redirect=1");

                            //ClearForm();
                            //ShowConfirmAddMoreRecord();

                            //txtTestDate.Text = Utilities.GetDate();
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

        protected void txtMotherName_TextChanged(object sender, EventArgs e)
        {
            string sMotherName = txtMotherName.Text;

            txtMotherName.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sMotherName.ToLower());

            txtMotherCell.Focus();
        }

        protected void txtDistrict_TextChanged(object sender, EventArgs e)
        {
            string sDistrict = txtDistrict.Text;

            txtDistrict.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sDistrict.ToLower());

            txtTown.Focus();
        }

        protected void txtTown_TextChanged(object sender, EventArgs e)
        {
            string sTown = txtTown.Text;

            txtTown.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sTown.ToLower());

            txtCity.Focus();
        }

        protected void txtCity_TextChanged(object sender, EventArgs e)
        {
            string sCity = txtCity.Text;

            txtCity.Text =
                System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.
                ToTitleCase(sCity.ToLower());

            rdoTreatment_Glasses.Focus();
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

        protected void lnkShowMedicine_Click(object sender, EventArgs e)
        {
            if (chkMedicine.Visible == false)
            {
                chkMedicine.Visible = true;
            }
            else
            {
                chkMedicine.Visible = false;
            }
        }

        protected void rdoYes_CheckedChanged(object sender, EventArgs e)
        {
            rdoNotAgrees.Visible = false;
            txtOtherRemarks.Visible = false;
        }

        protected void rdoNo_CheckedChanged(object sender, EventArgs e)
        {
            rdoNotAgrees.Visible = true;
            txtOtherRemarks.Visible = true;
        }

        protected void rdoNotAgrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOtherRemarks.Visible = false;
            lblHospital.Visible = false;
            ddlHospital.Visible = false;

            txtOtherRemarks.Text = "";
            ddlHospital.SelectedValue = "0";

            if (rdoNotAgrees.SelectedValue == "3")
            {
                txtOtherRemarks.Visible = true;
            }
            else if (rdoNotAgrees.SelectedValue == "0")
            {
                lblHospital.Visible = true;
                ddlHospital.Visible = true;
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