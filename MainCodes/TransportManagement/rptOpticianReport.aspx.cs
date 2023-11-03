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
    public partial class rptOpticianReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "OpticianReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                hfSchoolIDPKID.Value = "0";

                ListItem item = new ListItem();
                item.Text = "All";
                item.Value = "0";
                ddlVisitDate.Items.Insert(0, item);
            }
        }

        private bool ValidateInput()
        {
            ClearValidation();

            return true;
        }

        private void ClearForm()
        {
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";
            hfSchoolIDPKID.Value = "0";

            ddlVisitDate.SelectedValue = "0";
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private void BindCombos()
        {

            try
            {
                ddlVisitDate.Items.Clear();
                //DataTable dt = dx.sp_ReportforOptincian_Student(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                int iSchoolId = int.Parse(hfSchoolIDPKID.Value);
                var dtVisitDate = dx.sp_SchoolWiseVisitDate(iSchoolId).ToList();

                if (dtVisitDate.Count != 0)
                {
                    ddlVisitDate.DataSource = dtVisitDate;
                    ddlVisitDate.DataValueField = "SchoolAutoId";
                    ddlVisitDate.DataTextField = "OptometristStudentTransDate";
                    ddlVisitDate.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "All";
                    item.Value = "0";
                    ddlVisitDate.Items.Insert(0, item);
                }
                else
                {
                    ListItem item = new ListItem();
                    item.Text = "All";
                    item.Value = "0";
                    ddlVisitDate.Items.Insert(0, item);
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strFromDate = string.Empty;
                    string strToDate = string.Empty;

                    if (ddlVisitDate.SelectedValue == "0")
                    {
                        strFromDate = "2022-07-01";
                        strToDate = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        strFromDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
                        strToDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
                    }

                    if (rdoType.SelectedValue == "0")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=OpticianReportStudent&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoType.SelectedValue == "1")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=OpticianReportTeacher&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoType.SelectedValue == "2")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=OpticianReport&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            DateTime fromDate;
            DateTime toDate;

            if (ddlVisitDate.SelectedValue == "0")
            {
                strFromDate = "2022-07-01";
                strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            }
            else
            {
                strFromDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
                strToDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("yyyy-MM-dd");
            }

            fromDate = DateTime.Parse(strFromDate);
            toDate = DateTime.Parse(strToDate);

            if (rdoType.SelectedValue == "0")
            {
                DataTable dt = dx.sp_ReportforOptincian_Student(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "Student");
            }
            else if (rdoType.SelectedValue == "1")
            {
                DataTable dt = dx.sp_ReportforOptincian_Teacher(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "Teacher");
            }
            else if (rdoType.SelectedValue == "2")
            {
                DataTable dt = dx.sp_ReportforOptincian(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "Both");
            }
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            try
            {

                if (strForm == "Teacher")
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

                                workSheet = excel.Workbook.Worksheets.Add("OpticianReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Optician Report");
                                Utilities.SetTimeStamp(workSheet, "Optician Report");

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

                                    workSheet.Cells[FirstRow, 1].Value = "Teacher Code";
                                    workSheet.Cells[FirstRow, 2].Value = "Teacher Name";
                                    workSheet.Cells[FirstRow, 3].Value = "School Name";
                                    workSheet.Cells[FirstRow, 4].Value = "Age";
                                    workSheet.Cells[FirstRow, 5].Value = "Visit";
                                    workSheet.Cells[FirstRow, 6].Value = "Opto Date";
                                    workSheet.Cells[FirstRow, 7].Value = "Spherical (Right Eye)";
                                    workSheet.Cells[FirstRow, 8].Value = "Cyclinderical (Right Eye)";
                                    workSheet.Cells[FirstRow, 9].Value = "Axix (Right Eye)";
                                    workSheet.Cells[FirstRow, 10].Value = "NearAdd (Right Eye)";
                                    workSheet.Cells[FirstRow, 11].Value = "Spherical (Left Eye)";
                                    workSheet.Cells[FirstRow, 12].Value = "Cyclinderical (Left Eye)";
                                    workSheet.Cells[FirstRow, 13].Value = "Axix (Left Eye)";
                                    workSheet.Cells[FirstRow, 14].Value = "NearAdd (Left Eye)";

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
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5 || FirstColumn == 6 || FirstColumn == 7)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else if (FirstColumn == 6)
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
                                            else if (FirstColumn == 9 || FirstColumn == 13)
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

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=OpticianReport.xlsx");
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

                                workSheet = excel.Workbook.Worksheets.Add("OpticianReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Optician Report");
                                Utilities.SetTimeStamp(workSheet, "Optician Report");

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
                                    workSheet.Cells[FirstRow, 8].Value = "Visit";
                                    workSheet.Cells[FirstRow, 9].Value = "Opto Date";
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
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3 || FirstColumn == 4 || FirstColumn == 5 || FirstColumn == 6 || FirstColumn == 7)
                                            {
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                                workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                                            }
                                            else if (FirstColumn == 9)
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

                                //Utilities.SetColumnsWidth(ws);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=OpticianReport.xlsx");
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
        }

        protected void btnLookupSchool_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable data = (from a in dx.sp_GetLookupData_School_OpticianReport()
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

                    BindCombos();
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

                    BindCombos();
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

            if (ddlVisitDate.SelectedValue == "0")
            {
                strFromDate = "01-Jul-2022";
                strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            }
            else
            {
                strFromDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("dd-MMM-yyyy");
                strToDate = DateTime.Parse(ddlVisitDate.SelectedItem.Text.Trim()).ToString("dd-MMM-yyyy");
            }

            ws.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = "Date Range:";
            ws.Cells[FirstRow, FirstColumn + 1, FirstRow, FirstColumn + 1].Value = strFromDate + " to " + strToDate;
            FirstRow++;

            return FirstRow;
        }


    }
}