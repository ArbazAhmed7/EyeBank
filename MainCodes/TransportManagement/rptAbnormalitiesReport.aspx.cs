using OfficeOpenXml;
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
    public partial class rptAbnormalitiesReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "rptAbnormalitiesReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                hfSchoolIDPKID.Value = "0";
                hfClassIDPKID.Value = "0";

                txtTestDateFrom.Text = "";
                txtTestDateTo.Text = "";
            }
        }

        private bool ValidateInput()
        {
            ClearValidation();

            if (txtTestDateFrom.Text.Trim() != "")
            {
                try
                {
                    DateTime dt = DateTime.Parse(txtTestDateFrom.Text);
                }
                catch
                {
                    lbl_error.Text = "Invalid 'Date'.";
                    return false;
                }
            }

            if (txtTestDateTo.Text.Trim() != "")
            {
                try
                {
                    DateTime dt = DateTime.Parse(txtTestDateTo.Text);
                }
                catch
                {
                    lbl_error.Text = "Invalid 'Date'.";
                    return false;
                }
            }



            return true;
        }

        private void ClearForm()
        {
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";
            hfSchoolIDPKID.Value = "0";

            txtClassNo.Text = "";
            hfClassIDPKID.Value = "0";

            txtTestDateFrom.Text = "";
            txtTestDateTo.Text = "";

            chkRefractiveError.Checked = false;
            chkLowVision.Checked = false;
            chkNeedscyclopegicrefration.Checked = false;
            chkSquintStrabismus.Checked = false;
            chkLazyEyeAmblyopia.Checked = false;
            chkColorblindnessAchromatopsia.Checked = false;
            chkCataract.Checked = false;
            chkTraumaticCataract.Checked = false;
            chkKeratoconus.Checked = false;
            chkAnisometropia.Checked = false;
            chkPtosis.Checked = false;
            chkNystagmus.Checked = false;
            chkCorneadefects.Checked = false;
            chkPuplidefects.Checked = false;
            chkPterygium.Checked = false;
            chkOther.Checked = false;
            chkPresbyopia.Checked = false;
            chkMyopia.Checked = false;
            chkHypermetropia.Checked = false;
            chkAstigmatism.Checked = false;
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    int iSchoolAutoId = int.Parse(hfSchoolIDPKID.Value);
                    int iClassAutoId = int.Parse(hfClassIDPKID.Value);

                    string strFromDate = string.Empty;
                    string strToDate = string.Empty;

                    if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                    else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                    if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ; }
                    else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                    int iRefractiveError = 0;
                    if (chkRefractiveError.Checked == true) { iRefractiveError = 1; }

                    int iNeedscyclopegicrefration = 0;
                    if (chkNeedscyclopegicrefration.Checked == true) { iNeedscyclopegicrefration = 1; }

                    int iSquintStrabismus = 0;
                    if (chkSquintStrabismus.Checked == true) { iSquintStrabismus = 1; }

                    int iLazyEyeAmblyopia = 0;
                    if (chkLazyEyeAmblyopia.Checked == true) { iLazyEyeAmblyopia = 1; }

                    int iColorblindnessAchromatopsia = 0;
                    if (chkColorblindnessAchromatopsia.Checked == true) { iColorblindnessAchromatopsia = 1; }

                    int iCataract = 0;
                    if (chkCataract.Checked == true) { iCataract = 1; }

                    int iTraumaticCataract = 0;
                    if (chkTraumaticCataract.Checked == true) { iTraumaticCataract = 1; }

                    int iKeratoconus = 0;
                    if (chkKeratoconus.Checked == true) { iKeratoconus = 1; }

                    int iAnisometropia = 0;
                    if (chkAnisometropia.Checked == true) { iAnisometropia = 1; }

                    int iPtosis = 0;
                    if (chkPtosis.Checked == true) { iPtosis = 1; }

                    int iNystagmus = 0;
                    if (chkNystagmus.Checked == true) { iNystagmus = 1; }

                    int iLowVision = 0;
                    if (chkLowVision.Checked == true) { iLowVision = 1; }

                    int iCorneadefects = 0;
                    if (chkCorneadefects.Checked == true) { iCorneadefects = 1; }

                    int iPuplidefects = 0;
                    if (chkPuplidefects.Checked == true) { iPuplidefects = 1; }

                    int iPterygium = 0;
                    if (chkPterygium.Checked == true) { iPterygium = 1; }

                    int iOther = 0;
                    if (chkOther.Checked == true) { iOther = 1; }

                    int iPresbyopia = 0;
                    if (chkPresbyopia.Checked == true) { iPresbyopia = 1; }

                    int iMyopia = 0;
                    if (chkMyopia.Checked == true) { iMyopia = 1; }

                    int iHypermetropia = 0;
                    if (chkHypermetropia.Checked == true) { iHypermetropia = 1; }

                    int iAstigmatism = 0;
                    if (chkAstigmatism.Checked == true) { iAstigmatism = 1; }

                    DateTime fromDate = DateTime.Parse(strFromDate);
                    DateTime toDate = DateTime.Parse(strToDate);

                    var dtAbnormalities = dx.sp_ReportforAbnormality_CheckData(iSchoolAutoId, iClassAutoId, fromDate, toDate,
                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).SingleOrDefault();
                    if (dtAbnormalities != null)
                    {
                        if (dtAbnormalities == 0)
                        {
                            lbl_error.Text = "No record exists against selected abnomalities.";
                            txtTestDateFrom.Focus();
                            return;
                        }
                    }

                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReportforAbnormalities&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ClassAutoId=" + hfClassIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "&RefractiveError=" + iRefractiveError + "&Needscyclopegicrefration=" + iNeedscyclopegicrefration + "&SquintStrabismus=" + iSquintStrabismus + "&LazyEyeAmblyopia=" + iLazyEyeAmblyopia + "&ColorblindnessAchromatopsia=" + iColorblindnessAchromatopsia + "&Cataract=" + iCataract + "&TraumaticCataract=" + iTraumaticCataract + "&Keratoconus=" + iKeratoconus + "&Anisometropia=" + iAnisometropia + "&Ptosis=" + iPtosis + "&Nystagmus=" + iNystagmus + "&LowVision=" + iLowVision + "&Corneadefects=" + iCorneadefects + "&Puplidefects=" + iPuplidefects + "&Pterygium=" + iPterygium + "&Other=" + iOther + "&Presbyopia=" + iPresbyopia + "&Myopia=" + iMyopia + "&Hypermetropia=" + iHypermetropia + "&Astigmatism=" + iAstigmatism + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                }
                else
                {
                    //lbl_error.Text = res.RetMessage;
                }
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message;
            }

        }

        protected void btnViewExcel_Click(object sender, EventArgs e)
        {
            int iSchoolAutoId = int.Parse(hfSchoolIDPKID.Value);
            int iClassAutoId = int.Parse(hfClassIDPKID.Value);

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            DateTime fromDate;
            DateTime toDate;

            if (txtTestDateFrom.Text == "")
            {
                strFromDate = "01-Jul-2022";
                fromDate = DateTime.Parse(strFromDate);
            }
            else
            {
                strFromDate = txtTestDateFrom.Text;
                fromDate = DateTime.Parse(strFromDate);
            }

            if (txtTestDateTo.Text == "")
            {
                strToDate = "31-Dec-2099";
                toDate = DateTime.Parse(strToDate);
            }
            else
            {
                strToDate = txtTestDateTo.Text;
                toDate = DateTime.Parse(strToDate);
            }

            int iRefractiveError = 0;
            if (chkRefractiveError.Checked == true) { iRefractiveError = 1; }

            int iNeedscyclopegicrefration = 0;
            if (chkNeedscyclopegicrefration.Checked == true) { iNeedscyclopegicrefration = 1; }

            int iSquintStrabismus = 0;
            if (chkSquintStrabismus.Checked == true) { iSquintStrabismus = 1; }

            int iLazyEyeAmblyopia = 0;
            if (chkLazyEyeAmblyopia.Checked == true) { iLazyEyeAmblyopia = 1; }

            int iColorblindnessAchromatopsia = 0;
            if (chkColorblindnessAchromatopsia.Checked == true) { iColorblindnessAchromatopsia = 1; }

            int iCataract = 0;
            if (chkCataract.Checked == true) { iCataract = 1; }

            int iTraumaticCataract = 0;
            if (chkTraumaticCataract.Checked == true) { iTraumaticCataract = 1; }

            int iKeratoconus = 0;
            if (chkKeratoconus.Checked == true) { iKeratoconus = 1; }

            int iAnisometropia = 0;
            if (chkAnisometropia.Checked == true) { iAnisometropia = 1; }

            int iPtosis = 0;
            if (chkPtosis.Checked == true) { iPtosis = 1; }

            int iNystagmus = 0;
            if (chkNystagmus.Checked == true) { iNystagmus = 1; }

            int iLowVision = 0;
            if (chkLowVision.Checked == true) { iLowVision = 1; }

            int iCorneadefects = 0;
            if (chkCorneadefects.Checked == true) { iCorneadefects = 1; }

            int iPuplidefects = 0;
            if (chkPuplidefects.Checked == true) { iPuplidefects = 1; }

            int iPterygium = 0;
            if (chkPterygium.Checked == true) { iPterygium = 1; }

            int iOther = 0;
            if (chkOther.Checked == true) { iOther = 1; }

            int iPresbyopia = 0;
            if (chkPresbyopia.Checked == true) { iPresbyopia = 1; }

            int iMyopia = 0;
            if (chkMyopia.Checked == true) { iMyopia = 1; }

            int iHypermetropia = 0;
            if (chkHypermetropia.Checked == true) { iHypermetropia = 1; }

            int iAstigmatism = 0;
            if (chkAstigmatism.Checked == true) { iAstigmatism = 1; }

            DataTable dt = dx.sp_ReportforAbnormality(iSchoolAutoId, fromDate, toDate,
                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).ToList().ToDataTable();
            PrintReport(dt, "0");
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Create the worksheet

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("OpticianReport");


                    #region DataArea


                    if (ReportData.Rows.Count > 0)
                    {
                        ws.Cells[1, 1].LoadFromDataTable(ReportData, true);
                        int row = 1;
                    }
                    else
                    {
                        //No data                       

                    }

                    #endregion DataArea

                    Utilities.SetColumnsWidth(ws);

                    #region Download Excel File
                    if (strForm == "0")
                    {
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=OpticianReport_Student.xlsx");
                        Response.BinaryWrite(pck.GetAsByteArray());
                        Response.Flush();
                        Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef
                    }
                    else
                    {
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=OpticianReport_Teacher.xlsx");
                        Response.BinaryWrite(pck.GetAsByteArray());
                        Response.Flush();
                        Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef

                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //
            }
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
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnLookupClass_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData("Class")
                                  select a).ToList().ToDataTable();

                hfLookupResultClass.Value = "0";
                Session["lookupData"] = data;
                Session["Code"] = "Class No.";
                Session["Name"] = "";
                string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('LookupControl/LookupControl.aspx?winTitle=Select User&hfName=" + hfLookupResultClass.ID + "','.','height=600,width=500,menubar=no,status=yes,location=no,top=100,left=400,scrollbars=yes,resizable=yes')</script>";
                ScriptManager.RegisterStartupScript(btnLookupClass, this.GetType(), "popup", jsReport, false);
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void hfLookupResultClass_ValueChanged(object sender, EventArgs e)
        {
            string selectedPKID = string.Empty;
            selectedPKID = hfLookupResultClass.Value;
            hfClassIDPKID.Value = selectedPKID; //to allow update mode

            LoadClassDetail(selectedPKID);
            lbl_error.Text = "";
        }

        protected void hfClassIDPKID_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string ClassIDPKID = string.Empty;
                ClassIDPKID = hfClassIDPKID.Value;

                if (Convert.ToUInt32(ClassIDPKID) > 0)
                {
                    LoadClassDetail(ClassIDPKID);
                }

                hfLookupResultClass.Value = "0";
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        private void LoadClassDetail(string ID)
        {
            try
            {
                if (Convert.ToUInt32(ID) > 0)
                {
                    var dt = dx.sp_tblClass_GetDetail(Convert.ToInt32(ID)).SingleOrDefault();
                    //txtClassSection.Text = dt.SectionAutoId;
                    txtClassNo.Text = dt.ClassNo;
                }

            }
            catch (Exception ex)
            {
                lbl_error.Text = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("rptAbnormalitiesReport.aspx");
            //ClearForm();
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
    }
}