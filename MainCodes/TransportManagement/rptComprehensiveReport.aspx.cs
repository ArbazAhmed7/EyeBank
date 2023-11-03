using OfficeOpenXml;
using OfficeOpenXml.Style;
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
    public partial class rptComprehensiveReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "rptComprehensiveReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                rdoType.SelectedValue = "0";
                rdoType_SelectedIndexChanged(null, null);

                txtTestDateFrom.Text = "";
                txtTestDateTo.Text = "";
            }
        }

        protected void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedIndex == 0)
            {
                rdoReportStudent.Visible = true;
                rdoReportTeacher.Visible = false;
            }
            else
            {
                rdoReportStudent.Visible = false;
                rdoReportTeacher.Visible = true;
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

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";
            hfSchoolIDPKID.Value = "0";

            txtTestDateFrom.Text = "";
            txtTestDateTo.Text = "";

            rdoType.SelectedValue = "0";
            rdoReportStudent.SelectedValue = "0";
            rdoReportTeacher.SelectedValue = "0";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strFromDate = string.Empty;
                    string strToDate = string.Empty;

                    if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                    else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                    if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ; }
                    else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                    if (rdoType.SelectedValue == "0")
                    {
                        //if (rdoReportStudent.SelectedValue == "0")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportStudent.SelectedValue == "1")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportStudent.SelectedValue == "2")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report_School_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportStudent.SelectedValue == "3")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report_School_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportStudent.SelectedValue == "4")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report_Class_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportStudent.SelectedValue == "5")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report_Class_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else 
                        if (rdoReportStudent.SelectedValue == "6")
                        {
                            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report_Section_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        }
                        else if (rdoReportStudent.SelectedValue == "7")
                        {
                            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report_Section_wise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        }
                    }
                    else
                    {
                        if (rdoReportTeacher.SelectedValue == "0")
                        {
                            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report_TeacherWise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        }
                        //else if (rdoReportTeacher.SelectedValue == "1")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report_TeacherWise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        //else if (rdoReportTeacher.SelectedValue == "2")
                        //{
                        //    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummarized_Comprehensive_Report_School_wise_TeacherWise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        //}
                        else if (rdoReportTeacher.SelectedValue == "3")
                        {
                            string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptDetailed_Comprehensive_Report_School_wise_TeacherWise&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                        }
                    }
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

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        protected void btnViewExcel_Click(object sender, EventArgs e)
        {
            int iSchoolAutoId = int.Parse(hfSchoolIDPKID.Value);

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            DateTime fromDate;
            DateTime toDate;

            if (txtTestDateFrom.Text == "")
            {
                strFromDate = "01-Jul-2022";
                fromDate = DateTime.Parse(strFromDate);
                strFromDate = fromDate.ToString("yyyy-MM-dd");
            }
            else
            {
                strFromDate = txtTestDateFrom.Text;
                fromDate = DateTime.Parse(strFromDate);
                strFromDate = fromDate.ToString("yyyy-MM-dd");
            }

            if (txtTestDateTo.Text == "")
            {
                strToDate = "31-Dec-2099";
                toDate = DateTime.Parse(strToDate);
                strToDate = toDate.ToString("yyyy-MM-dd");
            }
            else
            {
                strToDate = txtTestDateTo.Text;
                toDate = DateTime.Parse(strToDate);
                strToDate = toDate.ToString("yyyy-MM-dd");
            }

            if (rdoType.SelectedValue == "0")
            {
                //if (rdoReportStudent.SelectedValue == "0")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                //else if (rdoReportStudent.SelectedValue == "1")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                //else if (rdoReportStudent.SelectedValue == "2")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                //else if (rdoReportStudent.SelectedValue == "3")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                //else if (rdoReportStudent.SelectedValue == "4")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_ClassWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                //else if (rdoReportStudent.SelectedValue == "5")
                //{
                //    DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "0");
                //}
                if (rdoReportStudent.SelectedValue == "6")
                {
                    DataTable dt = dx.sp_tblTreatmentStudentReport_SectionWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    PrintReport(dt, "1");
                }
                else if (rdoReportStudent.SelectedValue == "7")
                {
                    DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    PrintReport(dt, "2");
                }

            }
            else
            {
                if (rdoReportTeacher.SelectedValue == "0")
                {
                    DataTable dt = dx.sp_tblTreatmentTeacherReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    PrintReport(dt, "3");
                }
                //else if (rdoReportStudent.SelectedValue == "1")
                //{
                //    DataTable dt = dx.sp_tblTreatmentTeacherReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "1");
                //}
                //else if (rdoReportStudent.SelectedValue == "2")
                //{
                //    DataTable dt = dx.sp_tblTreatmentTeacherReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                //    PrintReport(dt, "1");
                //}
                else if (rdoReportTeacher.SelectedValue == "3")
                {
                    DataTable dt = dx.sp_tblTreatmentTeacherReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    PrintReport(dt, "4");
                }
            }
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            try
            {

                if (strForm == "1")
                {
                    DataTable dtSchool = ReportData;
                    if (dtSchool != null)
                    {
                        if (dtSchool.Rows.Count > 0)
                        {
                            string AllorNaInDropDown = "All";

                            int FirstRow = 2;
                            int LastRow = dtSchool.Rows.Count;

                            int FirstColumn = 1;
                            int LastColumn = dtSchool.Rows.Count;

                            int HeadingHeaderColumn = dtSchool.Columns.Count;

                            using (ExcelPackage excel = new ExcelPackage())
                            {
                                ExcelWorksheet workSheet;

                                workSheet = excel.Workbook.Worksheets.Add("SummarizedReportforAbrormalities_Student");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Summarized Report for Abrormalities (School, Class, Section)");
                                Utilities.SetTimeStamp(workSheet, "Comprehensive Report");

                                FirstRow++;

                                FirstRow = Filters(workSheet, FirstRow, FirstColumn, AllorNaInDropDown);

                                Utilities.SetReportSelectionLabel(workSheet, 2, 1, FirstRow, 1);
                                Utilities.SetReportSelectionValue(workSheet, 2, 2, FirstRow, 2);

                                #endregion

                                Utilities.SetHeaderPanelBackground(workSheet, 1, 1, FirstRow, 7);
                                //Utilities.SetHeader(ws, ref FirstRow, 1, FirstRow, dtMainReport.Columns.Count);


                                iPrint_Record++;
                                FirstRow++;

                                FirstColumn = 1;

                                DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                if (dtMainReport_FilteredTable.Rows.Count > 0)
                                {
                                    FirstRow++;

                                    // Creating an instance
                                    // of ExcelPackage
                                    //ExcelPackage excel = new ExcelPackage();

                                    // name of the sheet
                                    //var workSheet = excel.Workbook.Worksheets.Add("SchoolList");

                                    // setting the properties
                                    // of the work sheet 
                                    workSheet.TabColor = System.Drawing.Color.Black;
                                    workSheet.DefaultRowHeight = 12;

                                    // Setting the properties
                                    // of the first row
                                    workSheet.Row(FirstRow).Height = 20;
                                    workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Row(FirstRow).Style.Font.Bold = true;

                                    // Header of the Excel sheet

                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "Class Code";
                                    workSheet.Cells[FirstRow, 3].Value = "Class Section";
                                    workSheet.Cells[FirstRow, 4].Value = "Normal";
                                    workSheet.Cells[FirstRow, 5].Value = "Wearing Glasses";
                                    workSheet.Cells[FirstRow, 6].Value = "Refractive Error";
                                    workSheet.Cells[FirstRow, 7].Value = "Low Vision";
                                    workSheet.Cells[FirstRow, 8].Value = "Student Glasses Suggested";
                                    workSheet.Cells[FirstRow, 9].Value = "Needs cyclopegic refration";
                                    workSheet.Cells[FirstRow, 10].Value = "Squint Strabismus";
                                    workSheet.Cells[FirstRow, 11].Value = "LazyEye Amblyopia";
                                    workSheet.Cells[FirstRow, 12].Value = "Colorblindness Achromatopsia";
                                    workSheet.Cells[FirstRow, 13].Value = "Cataract";
                                    workSheet.Cells[FirstRow, 14].Value = "Traumatic Cataract";
                                    workSheet.Cells[FirstRow, 15].Value = "Keratoconus";
                                    workSheet.Cells[FirstRow, 16].Value = "Anisometropia";
                                    workSheet.Cells[FirstRow, 17].Value = "Ptosis";
                                    workSheet.Cells[FirstRow, 18].Value = "Nystagmus";
                                    workSheet.Cells[FirstRow, 19].Value = "Cornea defects";
                                    workSheet.Cells[FirstRow, 20].Value = "Pupli defects";
                                    workSheet.Cells[FirstRow, 21].Value = "Pterygium";
                                    workSheet.Cells[FirstRow, 22].Value = "Other";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    // Inserting the article data into excel
                                    // sheet by using the for each loop
                                    // As we have values to the first row 
                                    // we will start with second row
                                    //int recordIndex = 2;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0;-#,##0;-"; // "#,##0";
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                                }
                                                catch (Exception ex)
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                                }
                                            }
                                            //else
                                            //{
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            //}

                                            FirstColumn++;
                                        }
                                        FirstRow++;
                                    }

                                    // By default, the column width is not 
                                    // set to auto fit for the content
                                    // of the range, so we are using
                                    // AutoFit() method here. 

                                    //workSheet.Cells.AutoFitColumns();

                                    //workSheet.Column(1).AutoFit(0);

                                }

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=SummarizedReportforAbrormalities_Student.xlsx");
                                Response.BinaryWrite(excel.GetAsByteArray());
                                Response.Flush();
                                Response.End();
                            }
                        }
                        else
                        {
                            // No Data
                        }
                    }
                    else
                    {
                        // No Data
                    }
                }
                else if (strForm == "2")
                {
                    DataTable dtSchool = ReportData;
                    if (dtSchool != null)
                    {
                        if (dtSchool.Rows.Count > 0)
                        {
                            string AllorNaInDropDown = "All";

                            int FirstRow = 2;
                            int LastRow = dtSchool.Rows.Count;

                            int FirstColumn = 1;
                            int LastColumn = dtSchool.Rows.Count;

                            int HeadingHeaderColumn = dtSchool.Columns.Count;

                            using (ExcelPackage excel = new ExcelPackage())
                            {
                                ExcelWorksheet workSheet;

                                workSheet = excel.Workbook.Worksheets.Add("DetailedReportforAbrormalities_Student");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Detailed Report for Abrormalities (School, Class, Section)");
                                Utilities.SetTimeStamp(workSheet, "Comprehensive Report");

                                FirstRow++;

                                FirstRow = Filters(workSheet, FirstRow, FirstColumn, AllorNaInDropDown);

                                Utilities.SetReportSelectionLabel(workSheet, 2, 1, FirstRow, 1);
                                Utilities.SetReportSelectionValue(workSheet, 2, 2, FirstRow, 2);

                                #endregion

                                Utilities.SetHeaderPanelBackground(workSheet, 1, 1, FirstRow, 7);
                                //Utilities.SetHeader(ws, ref FirstRow, 1, FirstRow, dtMainReport.Columns.Count);


                                iPrint_Record++;
                                FirstRow++;

                                FirstColumn = 1;

                                DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                if (dtMainReport_FilteredTable.Rows.Count > 0)
                                {
                                    FirstRow++;

                                    // Creating an instance
                                    // of ExcelPackage
                                    //ExcelPackage excel = new ExcelPackage();

                                    // name of the sheet
                                    //var workSheet = excel.Workbook.Worksheets.Add("SchoolList");

                                    // setting the properties
                                    // of the work sheet 
                                    workSheet.TabColor = System.Drawing.Color.Black;
                                    workSheet.DefaultRowHeight = 12;

                                    // Setting the properties
                                    // of the first row
                                    workSheet.Row(FirstRow).Height = 20;
                                    workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Row(FirstRow).Style.Font.Bold = true;

                                    // Header of the Excel sheet                                    

                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "Class";
                                    workSheet.Cells[FirstRow, 3].Value = "Section";
                                    workSheet.Cells[FirstRow, 4].Value = "Student Code";
                                    workSheet.Cells[FirstRow, 5].Value = "Student Name";
                                    workSheet.Cells[FirstRow, 6].Value = "Gender";
                                    workSheet.Cells[FirstRow, 7].Value = "Age";
                                    workSheet.Cells[FirstRow, 8].Value = "Normal";
                                    workSheet.Cells[FirstRow, 9].Value = "Wearing Glasses";
                                    workSheet.Cells[FirstRow, 10].Value = "Refractive Error";
                                    workSheet.Cells[FirstRow, 11].Value = "Presbyopia";
                                    workSheet.Cells[FirstRow, 12].Value = "Student Glasses Suggested";
                                    workSheet.Cells[FirstRow, 13].Value = "Needs cyclopegic refration";
                                    workSheet.Cells[FirstRow, 14].Value = "Squint Strabismus";
                                    workSheet.Cells[FirstRow, 15].Value = "LazyEye Amblyopia";
                                    workSheet.Cells[FirstRow, 16].Value = "Colorblindness Achromatopsia";
                                    workSheet.Cells[FirstRow, 17].Value = "Cataract";
                                    workSheet.Cells[FirstRow, 18].Value = "Traumatic Cataract";
                                    workSheet.Cells[FirstRow, 19].Value = "Keratoconus";
                                    workSheet.Cells[FirstRow, 20].Value = "Anisometropia";
                                    workSheet.Cells[FirstRow, 21].Value = "Ptosis";
                                    workSheet.Cells[FirstRow, 22].Value = "Nystagmus";

                                    workSheet.Cells[FirstRow, 23].Value = "Cornea defects";
                                    workSheet.Cells[FirstRow, 24].Value = "Pupli defects";
                                    workSheet.Cells[FirstRow, 25].Value = "Pterygium";
                                    workSheet.Cells[FirstRow, 26].Value = "Other";



                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    // Inserting the article data into excel
                                    // sheet by using the for each loop
                                    // As we have values to the first row 
                                    // we will start with second row
                                    //int recordIndex = 2;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5 || FirstColumn == 6 || FirstColumn == 7)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0;-#,##0;-"; // "#,##0";
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                                }
                                                catch (Exception ex)
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                                }
                                            }
                                            //else
                                            //{
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            //}

                                            FirstColumn++;
                                        }
                                        FirstRow++;
                                    }

                                    // By default, the column width is not 
                                    // set to auto fit for the content
                                    // of the range, so we are using
                                    // AutoFit() method here. 

                                    //workSheet.Cells.AutoFitColumns();

                                    //workSheet.Column(1).AutoFit(0);

                                }

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=DetailedReportforAbrormalities_Student.xlsx");
                                Response.BinaryWrite(excel.GetAsByteArray());
                                Response.Flush();
                                Response.End();
                            }
                        }
                        else
                        {
                            // No Data
                        }
                    }
                    else
                    {
                        // No Data
                    }
                }
                else if (strForm == "3")
                {
                    DataTable dtSchool = ReportData;
                    if (dtSchool != null)
                    {
                        if (dtSchool.Rows.Count > 0)
                        {
                            string AllorNaInDropDown = "All";

                            int FirstRow = 2;
                            int LastRow = dtSchool.Rows.Count;

                            int FirstColumn = 1;
                            int LastColumn = dtSchool.Rows.Count;

                            int HeadingHeaderColumn = dtSchool.Columns.Count;

                            using (ExcelPackage excel = new ExcelPackage())
                            {
                                ExcelWorksheet workSheet;

                                workSheet = excel.Workbook.Worksheets.Add("SummarizedReportforAbrormalities_Teacher");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Summarized Report for Abrormalities (School)");
                                Utilities.SetTimeStamp(workSheet, "Comprehensive Report");

                                FirstRow++;

                                FirstRow = Filters(workSheet, FirstRow, FirstColumn, AllorNaInDropDown);

                                Utilities.SetReportSelectionLabel(workSheet, 2, 1, FirstRow, 1);
                                Utilities.SetReportSelectionValue(workSheet, 2, 2, FirstRow, 2);

                                #endregion

                                Utilities.SetHeaderPanelBackground(workSheet, 1, 1, FirstRow, 7);
                                //Utilities.SetHeader(ws, ref FirstRow, 1, FirstRow, dtMainReport.Columns.Count);


                                iPrint_Record++;
                                FirstRow++;

                                FirstColumn = 1;

                                DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                if (dtMainReport_FilteredTable.Rows.Count > 0)
                                {
                                    FirstRow++;

                                    // Creating an instance
                                    // of ExcelPackage
                                    //ExcelPackage excel = new ExcelPackage();

                                    // name of the sheet
                                    //var workSheet = excel.Workbook.Worksheets.Add("SchoolList");

                                    // setting the properties
                                    // of the work sheet 
                                    workSheet.TabColor = System.Drawing.Color.Black;
                                    workSheet.DefaultRowHeight = 12;

                                    // Setting the properties
                                    // of the first row
                                    workSheet.Row(FirstRow).Height = 20;
                                    workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Row(FirstRow).Style.Font.Bold = true;

                                    // Header of the Excel sheet

                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "Normal";
                                    workSheet.Cells[FirstRow, 3].Value = "Wearing Glasses";
                                    workSheet.Cells[FirstRow, 4].Value = "Refractive Error";
                                    workSheet.Cells[FirstRow, 5].Value = "Presbyopia";
                                    workSheet.Cells[FirstRow, 6].Value = "Student Glasses Suggested";
                                    workSheet.Cells[FirstRow, 7].Value = "Needs cyclopegic refration";
                                    workSheet.Cells[FirstRow, 8].Value = "Squint Strabismus";
                                    workSheet.Cells[FirstRow, 9].Value = "LazyEye Amblyopia";
                                    workSheet.Cells[FirstRow, 10].Value = "Colorblindness Achromatopsia";
                                    workSheet.Cells[FirstRow, 11].Value = "Cataract";
                                    workSheet.Cells[FirstRow, 12].Value = "Traumatic Cataract";
                                    workSheet.Cells[FirstRow, 13].Value = "Keratoconus";
                                    workSheet.Cells[FirstRow, 14].Value = "Anisometropia";
                                    workSheet.Cells[FirstRow, 15].Value = "Ptosis";
                                    workSheet.Cells[FirstRow, 16].Value = "Nystagmus";
                                    workSheet.Cells[FirstRow, 17].Value = "Cornea defects";
                                    workSheet.Cells[FirstRow, 18].Value = "Pupli defects";
                                    workSheet.Cells[FirstRow, 19].Value = "Pterygium";
                                    workSheet.Cells[FirstRow, 20].Value = "Other";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    // Inserting the article data into excel
                                    // sheet by using the for each loop
                                    // As we have values to the first row 
                                    // we will start with second row
                                    //int recordIndex = 2;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (FirstColumn == 1)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0;-#,##0;-"; // "#,##0";
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                                }
                                                catch (Exception ex)
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                                }
                                            }

                                            FirstColumn++;
                                        }
                                        FirstRow++;
                                    }

                                    // By default, the column width is not 
                                    // set to auto fit for the content
                                    // of the range, so we are using
                                    // AutoFit() method here. 

                                    //workSheet.Cells.AutoFitColumns();

                                    //workSheet.Column(1).AutoFit(0);

                                }

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=SummarizedReportforAbrormalities_Teacher.xlsx");
                                Response.BinaryWrite(excel.GetAsByteArray());
                                Response.Flush();
                                Response.End();
                            }
                        }
                        else
                        {
                            // No Data
                        }
                    }
                    else
                    {
                        // No Data
                    }
                }
                else if (strForm == "4")
                {
                    DataTable dtSchool = ReportData;
                    if (dtSchool != null)
                    {
                        if (dtSchool.Rows.Count > 0)
                        {
                            string AllorNaInDropDown = "All";

                            int FirstRow = 2;
                            int LastRow = dtSchool.Rows.Count;

                            int FirstColumn = 1;
                            int LastColumn = dtSchool.Rows.Count;

                            int HeadingHeaderColumn = dtSchool.Columns.Count;

                            using (ExcelPackage excel = new ExcelPackage())
                            {
                                ExcelWorksheet workSheet;

                                workSheet = excel.Workbook.Worksheets.Add("DetailedReportforAbrormalities_Teacher");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Detailed Report for Abrormalities (School)");
                                Utilities.SetTimeStamp(workSheet, "Comprehensive Report");

                                FirstRow++;

                                FirstRow = Filters(workSheet, FirstRow, FirstColumn, AllorNaInDropDown);

                                Utilities.SetReportSelectionLabel(workSheet, 2, 1, FirstRow, 1);
                                Utilities.SetReportSelectionValue(workSheet, 2, 2, FirstRow, 2);

                                #endregion

                                Utilities.SetHeaderPanelBackground(workSheet, 1, 1, FirstRow, 7);
                                //Utilities.SetHeader(ws, ref FirstRow, 1, FirstRow, dtMainReport.Columns.Count);


                                iPrint_Record++;
                                FirstRow++;

                                FirstColumn = 1;

                                DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                if (dtMainReport_FilteredTable.Rows.Count > 0)
                                {
                                    FirstRow++;

                                    // Creating an instance
                                    // of ExcelPackage
                                    //ExcelPackage excel = new ExcelPackage();

                                    // name of the sheet
                                    //var workSheet = excel.Workbook.Worksheets.Add("SchoolList");

                                    // setting the properties
                                    // of the work sheet 
                                    workSheet.TabColor = System.Drawing.Color.Black;
                                    workSheet.DefaultRowHeight = 12;

                                    // Setting the properties
                                    // of the first row
                                    workSheet.Row(FirstRow).Height = 20;
                                    workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                    workSheet.Row(FirstRow).Style.Font.Bold = true;

                                    // Header of the Excel sheet                                    

                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "Teacher Code";
                                    workSheet.Cells[FirstRow, 3].Value = "Teacher Name";
                                    workSheet.Cells[FirstRow, 4].Value = "Gender";
                                    workSheet.Cells[FirstRow, 5].Value = "Age";
                                    workSheet.Cells[FirstRow, 6].Value = "Normal";
                                    workSheet.Cells[FirstRow, 7].Value = "Wearing Glasses";
                                    workSheet.Cells[FirstRow, 8].Value = "Refractive Error";
                                    workSheet.Cells[FirstRow, 9].Value = "Presbyopia";
                                    workSheet.Cells[FirstRow, 10].Value = "Student Glasses Suggested";
                                    workSheet.Cells[FirstRow, 11].Value = "Needs cyclopegic refration";
                                    workSheet.Cells[FirstRow, 12].Value = "Squint Strabismus";
                                    workSheet.Cells[FirstRow, 13].Value = "LazyEye Amblyopia";
                                    workSheet.Cells[FirstRow, 14].Value = "Colorblindness Achromatopsia";
                                    workSheet.Cells[FirstRow, 15].Value = "Cataract";
                                    workSheet.Cells[FirstRow, 16].Value = "Traumatic Cataract";
                                    workSheet.Cells[FirstRow, 17].Value = "Keratoconus";
                                    workSheet.Cells[FirstRow, 18].Value = "Anisometropia";
                                    workSheet.Cells[FirstRow, 19].Value = "Ptosis";
                                    workSheet.Cells[FirstRow, 20].Value = "Nystagmus";
                                    workSheet.Cells[FirstRow, 21].Value = "Cornea defects";
                                    workSheet.Cells[FirstRow, 22].Value = "Pupli defects";
                                    workSheet.Cells[FirstRow, 23].Value = "Pterygium";
                                    workSheet.Cells[FirstRow, 24].Value = "Other";

                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    // Inserting the article data into excel
                                    // sheet by using the for each loop
                                    // As we have values to the first row 
                                    // we will start with second row
                                    //int recordIndex = 2;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0;-#,##0;-"; // "#,##0";
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                                }
                                                catch (Exception ex)
                                                {
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                                }
                                            }
                                            //else
                                            //{
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            //    workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                            //}

                                            FirstColumn++;
                                        }
                                        FirstRow++;
                                    }

                                    // By default, the column width is not 
                                    // set to auto fit for the content
                                    // of the range, so we are using
                                    // AutoFit() method here. 

                                    //workSheet.Cells.AutoFitColumns();

                                    //workSheet.Column(1).AutoFit(0);

                                }

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=DetailedReportforAbrormalities_Teacher.xlsx");
                                Response.BinaryWrite(excel.GetAsByteArray());
                                Response.Flush();
                                Response.End();
                            }
                        }
                        else
                        {
                            // No Data
                        }
                    }
                    else
                    {
                        // No Data
                    }
                }


            }
            catch (Exception ex)
            {
                //
            }

            //try
            //{
            //    using (ExcelPackage pck = new ExcelPackage())
            //    {
            //        //Create the worksheet

            //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("ComprehensiveReport");


            //        #region DataArea


            //        if (ReportData.Rows.Count > 0)
            //        {
            //            ws.Cells[1, 1].LoadFromDataTable(ReportData, true);
            //            int row = 1;
            //        }
            //        else
            //        {
            //            //No data                       

            //        }

            //        #endregion DataArea

            //        Utilities.SetColumnsWidth(ws);

            //        #region Download Excel File
            //        if (strForm == "0")
            //        {
            //            Response.Clear();
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("content-disposition", "attachment; filename=ComprehensiveReport_Student.xlsx");
            //            Response.BinaryWrite(pck.GetAsByteArray());
            //            Response.Flush();
            //            Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef
            //        }
            //        else
            //        {
            //            Response.Clear();
            //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            Response.AddHeader("content-disposition", "attachment; filename=ComprehensiveReport_Teacher.xlsx");
            //            Response.BinaryWrite(pck.GetAsByteArray());
            //            Response.Flush();
            //            Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef

            //        }
            //        #endregion
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //
            //}
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

        private int Filters(ExcelWorksheet ws, int FirstRow, int FirstColumn, string AllorNa)
        {
            string strSchoolName = string.Empty;
            if (txtSchoolName.Text == "")
            {
                strSchoolName = "All";
            }
            else
            {
                strSchoolName = txtSchoolName.Text;
            }

            ws.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = "School:";
            ws.Cells[FirstRow, FirstColumn + 1, FirstRow, FirstColumn + 1].Value = strSchoolName;
            FirstRow++;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            if (txtTestDateFrom.Text == "")
            {
                strFromDate = "01-Jul-2022";
                strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            }
            else
            {
                strFromDate = DateTime.Parse(txtTestDateFrom.Text.Trim()).ToString("dd-MMM-yyyy");
                strToDate = DateTime.Parse(txtTestDateTo.Text.Trim()).ToString("dd-MMM-yyyy");
            }

            ws.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = "Date Range:";
            ws.Cells[FirstRow, FirstColumn + 1, FirstRow, FirstColumn + 1].Value = strFromDate + " to " + strToDate;
            FirstRow++;

            return FirstRow;
        }


    }
}