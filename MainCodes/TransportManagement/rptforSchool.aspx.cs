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
    public partial class rptforSchool : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "ReportforSchool"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                hfSchoolIDPKID.Value = "0";
            }
        }

        private bool ValidateInput()
        {
            ClearValidation();
            return true;
        }

        private void ClearForm()
        {
            //ddlSchool.SelectedValue = "0";
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";
            hfSchoolIDPKID.Value = "0";

            ClearValidation();
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
                    if (rdoReportList.SelectedValue == "0")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReportforSchool&SchoolAutoId=" + hfSchoolIDPKID.Value + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "1")
                    {
                        string strFromDate = "01-Jul-2022";
                        string strToDate = DateTime.Now.ToString("dd-MMM-yyyy");

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptRefractedErrorReport_SchoolReport&SchoolAutoId=" + hfSchoolIDPKID.Value + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportList.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "2")
                    {
                        string strFromDate = "01-Jul-2022";
                        string strToDate = DateTime.Now.ToString("dd-MMM-yyyy");

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptAbnormalitiesReport_SchoolReport&SchoolAutoId=" + hfSchoolIDPKID.Value + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportList.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
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

        protected void btnViewExcel_Click(object sender, EventArgs e)
        {
            int iSchoolAutoId = int.Parse(hfSchoolIDPKID.Value);

            if (rdoReportList.SelectedValue == "0")
            {
                DataTable dt = dx.sp_ReportforSchool(iSchoolAutoId).ToList().ToDataTable();
                PrintReport(dt, "0");
            }
            else if (rdoReportList.SelectedValue == "1")
            {
                string strFromDate = "01-Jul-2022";
                string strToDate = DateTime.Now.ToString("dd-MMM-yyyy");

                DateTime fromDate = DateTime.Parse(strFromDate);
                DateTime toDate = DateTime.Parse(strToDate);

                DataTable dt = dx.sp_RecractedErrors_StudentReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "1");
            }
            else if (rdoReportList.SelectedValue == "2")
            {
                string strFromDate = "01-Jul-2022";
                string strToDate = DateTime.Now.ToString("dd-MMM-yyyy");

                DateTime fromDate = DateTime.Parse(strFromDate);
                DateTime toDate = DateTime.Parse(strToDate);

                DataTable dt = dx.sp_DailyReport_School_StudentWithAbnormality(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "2");
            }
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            if (strForm == "0")
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

                            workSheet = excel.Workbook.Worksheets.Add("ReportforSchool");


                            int iPrint_Record = 0;
                            int iPrint_Column = 1;

                            #region Printing Header

                            Utilities.SetReportHeading(workSheet, "Report for School");
                            Utilities.SetTimeStamp(workSheet, "Report for School");

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

                                workSheet.Cells[FirstRow, 1].Value = "School Code";
                                workSheet.Cells[FirstRow, 2].Value = "Name of School";
                                workSheet.Cells[FirstRow, 3].Value = "Address of School";
                                workSheet.Cells[FirstRow, 4].Value = "District";
                                workSheet.Cells[FirstRow, 5].Value = "Town";
                                workSheet.Cells[FirstRow, 6].Value = "City";
                                workSheet.Cells[FirstRow, 7].Value = "Screening Date";
                                workSheet.Cells[FirstRow, 8].Value = "Registered Students";
                                workSheet.Cells[FirstRow, 9].Value = "Eye Screened Students";
                                workSheet.Cells[FirstRow, 10].Value = "Registered Teachers";
                                workSheet.Cells[FirstRow, 11].Value = "Eye Screened Teachers";
                                workSheet.Cells[FirstRow, 12].Value = "Students wearing Glasses";
                                workSheet.Cells[FirstRow, 13].Value = "Student Glasses Suggested";
                                workSheet.Cells[FirstRow, 14].Value = "Students provided Glasses";
                                workSheet.Cells[FirstRow, 15].Value = "Teachers wearing Glasses";
                                workSheet.Cells[FirstRow, 16].Value = "Teacher Glasses Suggested";
                                workSheet.Cells[FirstRow, 17].Value = "Teachers provided Glasses";
                                workSheet.Cells[FirstRow, 18].Value = "Refractive Error (Student)";
                                workSheet.Cells[FirstRow, 19].Value = "Refractive Error (Teacher)";
                                workSheet.Cells[FirstRow, 20].Value = "Needs cyclopegic refraction (Student)";
                                workSheet.Cells[FirstRow, 21].Value = "Needs cyclopegic refraction (Teacher)";
                                workSheet.Cells[FirstRow, 22].Value = "Squint Strabismus (Student)";
                                workSheet.Cells[FirstRow, 23].Value = "Squint Strabismus (Teacher)";
                                workSheet.Cells[FirstRow, 24].Value = "Lazy Eye Amblyopia (Student)";
                                workSheet.Cells[FirstRow, 25].Value = "Lazy Eye Amblyopia Techer";
                                workSheet.Cells[FirstRow, 26].Value = "Color blindness Achromatopsia (Student)";
                                workSheet.Cells[FirstRow, 27].Value = "Color blindness Achromatopsia (Teacher)";
                                workSheet.Cells[FirstRow, 28].Value = "Cataract (Student)";
                                workSheet.Cells[FirstRow, 29].Value = "Cataract (Teacher)";
                                workSheet.Cells[FirstRow, 30].Value = "Traumatic Cataract (Student)";
                                workSheet.Cells[FirstRow, 31].Value = "Traumatic Cataract (Teacher)";
                                workSheet.Cells[FirstRow, 32].Value = "Keratoconus (Student)";
                                workSheet.Cells[FirstRow, 33].Value = "Keratoconus (Teacher)";
                                workSheet.Cells[FirstRow, 34].Value = "Anisometropia (Student)";
                                workSheet.Cells[FirstRow, 35].Value = "Anisometropia (Teacher)";
                                workSheet.Cells[FirstRow, 35].Value = "Ptosis (Student)";
                                workSheet.Cells[FirstRow, 36].Value = "Ptosis (Teacher)";
                                workSheet.Cells[FirstRow, 37].Value = "Nystagmus (Student)";
                                workSheet.Cells[FirstRow, 38].Value = "Nystagmus (Teacher)";
                                workSheet.Cells[FirstRow, 39].Value = "Presbyopia (Student)";
                                workSheet.Cells[FirstRow, 40].Value = "Presbyopia (Teacher)";
                                workSheet.Cells[FirstRow, 41].Value = "Cornea defects (Student)";
                                workSheet.Cells[FirstRow, 42].Value = "Cornea defects (Teacher)";
                                workSheet.Cells[FirstRow, 43].Value = "Pupli defects (Student)";
                                workSheet.Cells[FirstRow, 44].Value = "Pupli defects (Teacher)";
                                workSheet.Cells[FirstRow, 45].Value = "Pterygium (Student)";
                                workSheet.Cells[FirstRow, 46].Value = "Pterygium (Teacher)";
                                workSheet.Cells[FirstRow, 47].Value = "Other (Student)";
                                workSheet.Cells[FirstRow, 48].Value = "Other (Teacher)";

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
                                        //workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                        if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5 || FirstColumn == 6)
                                        {
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                        }
                                        else if (FirstColumn == 7)
                                        {
                                            try
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToDateTime(dr[dc].ToString());
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            catch (Exception ex)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0";
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
                            Response.AddHeader("content-disposition", "attachment;  filename=ReportforSchool.xlsx");
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
            else if (strForm == "1")
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

                            workSheet = excel.Workbook.Worksheets.Add("ReportforSchool_RefractedErrors");


                            int iPrint_Record = 0;
                            int iPrint_Column = 1;

                            #region Printing Header

                            Utilities.SetReportHeading(workSheet, "List of Students (with Refractive Error)");
                            Utilities.SetTimeStamp(workSheet, "List of Students (with Refractive Error)");

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

                                workSheet.Cells[FirstRow, 1].Value = "Student Code";
                                workSheet.Cells[FirstRow, 2].Value = "Student Name";
                                workSheet.Cells[FirstRow, 3].Value = "School Name";
                                workSheet.Cells[FirstRow, 4].Value = "Class";
                                workSheet.Cells[FirstRow, 5].Value = "Section";
                                workSheet.Cells[FirstRow, 6].Value = "Age";
                                workSheet.Cells[FirstRow, 7].Value = "FatherName";
                                workSheet.Cells[FirstRow, 8].Value = "Wear Glasses";
                                workSheet.Cells[FirstRow, 9].Value = "Opto Date";
                                workSheet.Cells[FirstRow, 10].Value = "Glass Dispense Date";
                                workSheet.Cells[FirstRow, 11].Value = "Gender";
                                workSheet.Cells[FirstRow, 12].Value = "Glasses not suggested";
                                workSheet.Cells[FirstRow, 13].Value = "Glasses not willing";
                                workSheet.Cells[FirstRow, 14].Value = "Spherical (Right Eye)";
                                workSheet.Cells[FirstRow, 15].Value = "Cyclinderical (Right Eye)";
                                workSheet.Cells[FirstRow, 16].Value = "Axix (Right Eye)";
                                workSheet.Cells[FirstRow, 17].Value = "NearAdd (Right Eye)";
                                workSheet.Cells[FirstRow, 18].Value = "Spherical (Left Eye)";
                                workSheet.Cells[FirstRow, 19].Value = "Cyclinderical (Left Eye)";
                                workSheet.Cells[FirstRow, 20].Value = "Axix (Left Eye)";
                                workSheet.Cells[FirstRow, 21].Value = "NearAdd (Left Eye)";




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
                                        //workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                        if (FirstColumn == 9 || FirstColumn == 10)
                                        {
                                            try
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToDateTime(dr[dc].ToString());
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            catch (Exception ex)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                        }
                                        else if (FirstColumn == 16 || FirstColumn == 20)
                                        {
                                            try
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToInt32(dr[dc].ToString());
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "#,##0";
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                            }
                                            catch (Exception ex)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                        }
                                        else
                                        {
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
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
                            Response.AddHeader("content-disposition", "attachment;  filename=ReportforSchool_RefractedErrors.xlsx");
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
            else
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

                            workSheet = excel.Workbook.Worksheets.Add("ReportforSchool_Abnormalities");


                            int iPrint_Record = 0;
                            int iPrint_Column = 1;

                            #region Printing Header

                            Utilities.SetReportHeading(workSheet, "List of Students (with other Abnormalities)");
                            Utilities.SetTimeStamp(workSheet, "List of Students (with other Abnormalities)");

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

                                workSheet.Cells[FirstRow, 1].Value = "Student Code";
                                workSheet.Cells[FirstRow, 2].Value = "Student Name";
                                workSheet.Cells[FirstRow, 3].Value = "School Name";
                                workSheet.Cells[FirstRow, 4].Value = "Class";
                                workSheet.Cells[FirstRow, 5].Value = "Section";
                                workSheet.Cells[FirstRow, 6].Value = "Age";
                                workSheet.Cells[FirstRow, 7].Value = "Gender";
                                workSheet.Cells[FirstRow, 8].Value = "Wear Glasses";
                                workSheet.Cells[FirstRow, 9].Value = "Daignosis";
                                workSheet.Cells[FirstRow, 10].Value = "Daignosis Remarks";
                                workSheet.Cells[FirstRow, 11].Value = "Sub Daignosis";
                                workSheet.Cells[FirstRow, 12].Value = "Treatment";
                                workSheet.Cells[FirstRow, 13].Value = "Medicine";
                                workSheet.Cells[FirstRow, 14].Value = "Next Visit";
                                workSheet.Cells[FirstRow, 15].Value = "Father Name";
                                workSheet.Cells[FirstRow, 16].Value = "Father Cell";
                                workSheet.Cells[FirstRow, 17].Value = "Mother Name";
                                workSheet.Cells[FirstRow, 18].Value = "Mother Cell";
                                workSheet.Cells[FirstRow, 19].Value = "Address";
                                workSheet.Cells[FirstRow, 20].Value = "Optometrist Name";


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
                                        workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                        workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

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
                            Response.AddHeader("content-disposition", "attachment;  filename=ReportforSchool_Abnormalities.xlsx");
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
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

            return FirstRow;
        }
    }
}