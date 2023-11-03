using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Web.Services;

namespace TransportManagement
{
    public partial class rptSixMonthVisit : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "rptSixMonthVisit")) 
            { 
                Response.Redirect("~/Login.aspx"); 
            }

            if (!IsPostBack)
            {
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

                    string userId = string.Empty;
                    if (chkUserId.Items[0].Selected == true)
                    {
                        userId = "0";
                    }
                    else
                    {
                        for (int i = 0; i < chkUserId.Items.Count; i++)
                        {
                            if (chkUserId.Items[i].Selected == true)// getting selected value from CheckBox List  
                            {
                                userId += chkUserId.Items[i].Value + ","; // add selected Item text to the String .  
                            }
                        }
                        userId = userId.TrimEnd(',');
                    }

                    string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=StaffPerformanceReport&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&UserId=" + userId + "&SchoolAutoId=0&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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

        private void BindCombos()
        {

            try
            {
                var dtUser = dx.sp_tblUser_Report().ToList();

                if (dtUser.Count != 0)
                {
                    chkUserId.DataSource = dtUser;
                    chkUserId.DataValueField = "UserId";
                    chkUserId.DataTextField = "UserName";
                    chkUserId.DataBind();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void btnViewExcel_Click(object sender, EventArgs e)
        {
            int iSchoolAutoId = 0; // int.Parse(hfSchoolIDPKID.Value);

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

            string userId = string.Empty;
            if (chkUserId.Items[0].Selected == true)
            {
                userId = "0";
            }
            else
            {
                for (int i = 0; i < chkUserId.Items.Count; i++)
                {
                    if (chkUserId.Items[i].Selected == true)// getting selected value from CheckBox List  
                    {
                        userId += chkUserId.Items[i].Value + ","; // add selected Item text to the String .  
                    }
                }
                userId = userId.TrimEnd(',');
            }

            DataTable dt = dx.sp_StaffPerformanceReport(iSchoolAutoId, fromDate, toDate, userId).ToList().ToDataTable();
            PrintReport(dt, "0");
        }

        private void PrintReport(DataTable ReportData, string strForm)
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

                        workSheet = excel.Workbook.Worksheets.Add("StaffPerformanceReport");


                        int iPrint_Record = 0;
                        int iPrint_Column = 1;

                        #region Printing Header

                        Utilities.SetReportHeading(workSheet, "Staff Performance Report");
                        Utilities.SetTimeStamp(workSheet, "Staff Performance Report");

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

                            workSheet.Cells[FirstRow, 1].Value = "User Name";
                            workSheet.Cells[FirstRow, 2].Value = "Role";
                            workSheet.Cells[FirstRow, 3].Value = "Test Performed";
                            workSheet.Cells[FirstRow, 4].Value = "Students identified for refractive error";
                            workSheet.Cells[FirstRow, 5].Value = "Students identified with other Abnormailities";




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
                                    if (FirstColumn == 1 || FirstColumn == 2)
                                    {
                                        workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = dr[dc].ToString();
                                        workSheet.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
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
                        Response.AddHeader("content-disposition", "attachment;  filename=StaffPerformanceReport.xlsx");
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private int Filters(ExcelWorksheet ws, int FirstRow, int FirstColumn, string AllorNa)
        {
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