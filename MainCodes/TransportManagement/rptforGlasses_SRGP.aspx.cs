﻿using OfficeOpenXml;
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
    public partial class rptforGlasses_SRGP : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "rptforGlasses_SRGP"))
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

                //txtTestDateFrom.Text = "";
                //txtTestDateTo.Text = "";

                txtTestDateFrom.Visible = false;
                txtTestDateTo.Visible = false;
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

            rdo0.Checked = true;
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

                    if (rdo0.Checked == true)
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptSummaryGlassesProvided&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&SchoolAutoId=" + hfSchoolIDPKID.Value + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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

            if (rdo0.Checked == true)
            {
                DataTable dt = dx.sp_GlassesProvided_SummaryReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "0");
            }
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {

            string strReportName = string.Empty;

            if (rdo0.Checked == true)
            {
                strReportName = rdo0.Text;

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

                            workSheet = excel.Workbook.Worksheets.Add("SchoolwiseReportforGlasses");


                            int iPrint_Record = 0;
                            int iPrint_Column = 1;

                            #region Printing Header

                            Utilities.SetReportHeading(workSheet, strReportName);
                            Utilities.SetTimeStamp(workSheet, "School wise Report for Glasses");

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

                                workSheet.Cells[FirstRow, 1].Value = "Name of School";
                                workSheet.Cells[FirstRow, 2].Value = "Address of School";
                                workSheet.Cells[FirstRow, 3].Value = "District";
                                workSheet.Cells[FirstRow, 4].Value = "Town";
                                workSheet.Cells[FirstRow, 5].Value = "City";
                                workSheet.Cells[FirstRow, 6].Value = "Glasses Suggested (Student)";
                                workSheet.Cells[FirstRow, 7].Value = "Glasses distributed (Student)";
                                workSheet.Cells[FirstRow, 8].Value = "Glasses Suggested (Teacher)";
                                workSheet.Cells[FirstRow, 9].Value = "Glasses distributed (Teacher)";

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
                            Response.AddHeader("content-disposition", "attachment;  filename=SchoolwiseReportforGlasses.xlsx");
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


    }
}