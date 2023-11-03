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
    public partial class rptRefractedErrorReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "rptRefractedErrorReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                hfSchoolIDPKID.Value = "0";
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

            txtTestDateFrom.Text = "";
            txtTestDateTo.Text = "";
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
                    string strFromDate = string.Empty;
                    string strToDate = string.Empty;

                    if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                    else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                    if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                    else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                    int iReportType = 0;
                    if (rdoNegative.Checked == true)
                    {
                        iReportType = 1;
                    }
                    if (rdoHigherthan.Checked == true)
                    {
                        iReportType = 2;
                    }

                    int iValue = 0;
                    if (rdoHigherthan.Checked == true)
                    {
                        try
                        {
                            iValue = int.Parse(txtHigher.Text);
                        }
                        catch (Exception ex)
                        {
                            iValue = 0;
                        }
                    }

                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=RefractedErrorReportStudent&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&RefractedError=" + iValue + "&ReportType=" + iReportType + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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
                strToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                toDate = DateTime.Parse(strToDate);
            }
            else
            {
                strToDate = txtTestDateTo.Text;
                toDate = DateTime.Parse(strToDate);
            }

            int iReportType = 0;
            if (rdoNegative.Checked == true)
            {
                iReportType = 1;
            }
            if (rdoHigherthan.Checked == true)
            {
                iReportType = 2;
            }

            int iValue = 0;
            if (rdoHigherthan.Checked == true)
            {
                try
                {
                    iValue = int.Parse(txtHigher.Text);
                }
                catch (Exception ex)
                {
                    iValue = 0;
                }
            }

            DataTable dt = dx.sp_ReportforRefractedError_Student(iSchoolAutoId, fromDate, toDate, iReportType, iValue).ToList().ToDataTable();
            PrintReport(dt, "0");
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            try
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

                            workSheet = excel.Workbook.Worksheets.Add("RefractedErrorReport");


                            int iPrint_Record = 0;
                            int iPrint_Column = 1;

                            #region Printing Header

                            Utilities.SetReportHeading(workSheet, "Students report for Refracted Errors");
                            Utilities.SetTimeStamp(workSheet, "Students report for Refracted Errors");

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
                                workSheet.Cells[FirstRow, 7].Value = "Wear Glasses";
                                workSheet.Cells[FirstRow, 8].Value = "Opto Date";
                                workSheet.Cells[FirstRow, 9].Value = "Gender";
                                workSheet.Cells[FirstRow, 10].Value = "Spherical (Right Eye)";
                                workSheet.Cells[FirstRow, 11].Value = "Cyclinderical (Right Eye)";
                                workSheet.Cells[FirstRow, 12].Value = "Axix (Right Eye)";
                                workSheet.Cells[FirstRow, 13].Value = "NearAdd (Right Eye)";
                                workSheet.Cells[FirstRow, 14].Value = "Spherical (Left Eye)";
                                workSheet.Cells[FirstRow, 15].Value = "Cyclinderical (Left Eye)";
                                workSheet.Cells[FirstRow, 16].Value = "Axix (Left Eye)";
                                workSheet.Cells[FirstRow, 17].Value = "NearAdd (Left Eye)";

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
                                        if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5 || FirstColumn == 6 || FirstColumn == 7 || FirstColumn == 9)
                                        {
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                        }
                                        else if (FirstColumn == 12 || FirstColumn == 16)
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
                                        else if (FirstColumn == 8)
                                        {
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = Convert.ToDateTime(dr[dc].ToString());
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                                        }
                                        else
                                        {
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                            workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
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

                            //var sheet = excel.Workbook.Worksheets["RefractedErrorReport"];
                            //// entire worksheet
                            //sheet.Cells.AutoFitColumns(16,24);
                            //Utilities.SetColumnsWidth(workSheet);

                            //var sheet = package.Workbook.Worksheets["MySheet"];
                            //// entire worksheet
                            //sheet.Cells.AutofitColumns();
                            //// a column
                            //sheet.Columns[1].AutoFit();
                            //// a specific range
                            //sheet.Cells["A1:B3"].AutofitColumns();
                            //// a table
                            //tableRange = sheet.Cells[1, 1].LoadFromCollection(tableData2, true, TableStyles.Light1);
                            //tableRange.AutoFitColumns();

                            Response.Clear();
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.AddHeader("content-disposition", "attachment;  filename=RefractedErrorReport.xlsx");
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
            catch (Exception ex)
            {
                //
            }
            //try
            //{
            //    using (ExcelPackage pck = new ExcelPackage())
            //    {
            //        //Create the worksheet

            //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("RefractedErrorReport");


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

            //        Response.Clear();
            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        Response.AddHeader("content-disposition", "attachment; filename=RefractedErrorReport.xlsx");
            //        Response.BinaryWrite(pck.GetAsByteArray());
            //        Response.Flush();
            //        Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef
            //        #endregion
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //
            //}
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