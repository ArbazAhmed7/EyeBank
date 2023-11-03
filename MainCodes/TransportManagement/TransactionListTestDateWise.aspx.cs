using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;


namespace TransportManagement
{
    public partial class TransactionListTestDateWise : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Utilities.CanView(Utilities.GetLoginUserID(), "AuditTrail_TestDate"))
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
            if (!Page.IsPostBack)
            {
                InitForm();

                txtTestDateFrom.Text = Utilities.GetDate();
                txtTestDateTo.Text = Utilities.GetDate();
                txtTestDateFrom.Focus();
            }
        }

        private void InitForm()
        {
            BindFormsDDL();
        }

        private void BindFormsDDL()
        {
            try
            {

                var listForms = (from a in dx.sp_GetSpecificFormsData()
                                 select a).ToList();

                if (listForms.Count > 0)
                {
                    ddlForm.DataSource = listForms;
                    ddlForm.DataTextField = "FormDescription";
                    ddlForm.DataValueField = "FormAutoId";
                    ddlForm.DataBind();

                    ListItem item = new ListItem();
                    item.Text = "Diagnosis / Treatment / Next Visit";
                    item.Value = "0";
                    ddlForm.Items.Insert(0, item);
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    gv_TransactionList.DataSource = null;
                    gv_TransactionList.DataBind();

                    string strForm = ddlForm.SelectedItem.Text;
                    DateTime dtFrom = DateTime.Parse(txtTestDateFrom.Text);
                    DateTime dtTo = DateTime.Parse(txtTestDateTo.Text);

                    if (strForm == "School Enrollment")
                    {
                        var dtSchoolEnrollmentView = dx.sp_TransactionList_SchoolEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtSchoolEnrollmentView;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Student Enrollment")
                    {
                        var dtStudentEnrollmentView = dx.sp_TransactionList_StudentEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtStudentEnrollmentView;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Teacher Enrollment")
                    {
                        var dtTeacherEnrollmentView = dx.sp_TransactionList_TeacherEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtTeacherEnrollmentView;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Auto Refraction Inspection")
                    {
                        var dtAutoRefTest = dx.sp_TransactionList_AutoRefTest_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtAutoRefTest;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Optometrist Inspection")
                    {
                        var dtOptometer = dx.sp_TransactionList_Otpometrist_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtOptometer;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Diagnosis / Treatment / Next Visit")
                    {
                        var dtTreatment = dx.sp_TransactionList_Treatment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtTreatment;
                        gv_TransactionList.DataBind();
                    }
                    else if (strForm == "Followup visit dispensing Glasses")
                    {
                        var dtGlassDespense = dx.sp_TransactionList_GlassDespenseStudent_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList();

                        gv_TransactionList.DataSource = dtGlassDespense;
                        gv_TransactionList.DataBind();
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

        protected void btnAbort_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/dashboard.aspx");
        }

        private bool ValidateInput()
        {
            CleatValidation();
            try
            {
                DateTime dt = DateTime.Parse(txtTestDateFrom.Text);
            }
            catch
            {
                lbl_error.Text = "Invalid 'Entry (From Date)'.";
                return false;
            }
            if (txtTestDateFrom.Text.Trim() == "")
            {
                lbl_error.Text = "'Entry (From Date)' is required.";
                return false;
            }

            try
            {
                DateTime dt = DateTime.Parse(txtTestDateTo.Text);
            }
            catch
            {
                lbl_error.Text = "Invalid 'Entry (To Date)'.";
                return false;
            }
            if (txtTestDateTo.Text.Trim() == "")
            {
                lbl_error.Text = "'Entry (To Date)' is required.";
                return false;
            }
            return true;
        }

        private void PrintReport(DataTable ReportData, string strForm)
        {
            try
            {
                #region DataArea


                if (ReportData.Rows.Count > 0)
                {
                    if (strForm == "SchoolEnrollment")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;

                                        workSheet.Cells[FirstRow, 1].Value = "Enrollment Date";
                                        workSheet.Cells[FirstRow, 2].Value = "School Code";
                                        workSheet.Cells[FirstRow, 3].Value = "School Name";
                                        workSheet.Cells[FirstRow, 4].Value = "Address 1";
                                        workSheet.Cells[FirstRow, 5].Value = "Address 2";
                                        workSheet.Cells[FirstRow, 6].Value = "District";
                                        workSheet.Cells[FirstRow, 7].Value = "Town";
                                        workSheet.Cells[FirstRow, 8].Value = "City";
                                        workSheet.Cells[FirstRow, 9].Value = "Telephone No";
                                        workSheet.Cells[FirstRow, 10].Value = "Cell No";
                                        workSheet.Cells[FirstRow, 11].Value = "Primary";
                                        workSheet.Cells[FirstRow, 12].Value = "Secondary";
                                        workSheet.Cells[FirstRow, 13].Value = "For Gender";
                                        workSheet.Cells[FirstRow, 14].Value = "Registered Students";
                                        workSheet.Cells[FirstRow, 15].Value = "Registered Teachers";
                                        workSheet.Cells[FirstRow, 16].Value = "Title";
                                        workSheet.Cells[FirstRow, 17].Value = "Principal Name";
                                        workSheet.Cells[FirstRow, 18].Value = "Principal Cell No";
                                        workSheet.Cells[FirstRow, 19].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 20].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 21].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 22].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 23].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 1 || FirstColumn == 20)
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
                                                else if (!(FirstColumn == 14 || FirstColumn == 15))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();

                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "StudentEnrollment")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;


                                        workSheet.Cells[FirstRow, 1].Value = "School Name";
                                        workSheet.Cells[FirstRow, 2].Value = "Class";
                                        workSheet.Cells[FirstRow, 3].Value = "Section";
                                        workSheet.Cells[FirstRow, 4].Value = "Student Code";
                                        workSheet.Cells[FirstRow, 5].Value = "Student Reg No";
                                        workSheet.Cells[FirstRow, 6].Value = "Student Name";
                                        workSheet.Cells[FirstRow, 7].Value = "Father Name";
                                        workSheet.Cells[FirstRow, 8].Value = "Age";
                                        workSheet.Cells[FirstRow, 9].Value = "Student Enrollment Date";
                                        workSheet.Cells[FirstRow, 10].Value = "Gender";
                                        workSheet.Cells[FirstRow, 11].Value = "Decreased Vision";
                                        workSheet.Cells[FirstRow, 12].Value = "Wear Glasses";
                                        workSheet.Cells[FirstRow, 13].Value = "Picture Available";
                                        workSheet.Cells[FirstRow, 14].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 15].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 16].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 17].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 18].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 9 || FirstColumn == 15)
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
                                                else if (!(FirstColumn == 8))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();

                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "TeacherEnrollment")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;

                                        workSheet.Cells[FirstRow, 1].Value = "School Name";
                                        workSheet.Cells[FirstRow, 2].Value = "Teacher Code";
                                        workSheet.Cells[FirstRow, 3].Value = "Teacher Name";
                                        workSheet.Cells[FirstRow, 4].Value = "Relation";
                                        workSheet.Cells[FirstRow, 5].Value = "Father / Husband Name";
                                        workSheet.Cells[FirstRow, 6].Value = "Age";
                                        workSheet.Cells[FirstRow, 7].Value = "Teacher Enrollment Date";
                                        workSheet.Cells[FirstRow, 8].Value = "Gender";
                                        workSheet.Cells[FirstRow, 9].Value = "Decreased Vision";
                                        workSheet.Cells[FirstRow, 10].Value = "Wear Glasses";
                                        workSheet.Cells[FirstRow, 11].Value = "Picture Available";
                                        workSheet.Cells[FirstRow, 12].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 13].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 14].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 15].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 16].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 7 || FirstColumn == 13)
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
                                                else if (!(FirstColumn == 6))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();

                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "AutoRefractionInspection")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;


                                        workSheet.Cells[FirstRow, 1].Value = "Test Type";
                                        workSheet.Cells[FirstRow, 2].Value = "Test Date";
                                        workSheet.Cells[FirstRow, 3].Value = "Code";
                                        workSheet.Cells[FirstRow, 4].Value = "Name";
                                        workSheet.Cells[FirstRow, 5].Value = "Father Name";
                                        workSheet.Cells[FirstRow, 6].Value = "Age";
                                        workSheet.Cells[FirstRow, 7].Value = "School Name";
                                        workSheet.Cells[FirstRow, 8].Value = "Class";
                                        workSheet.Cells[FirstRow, 9].Value = "Section";
                                        workSheet.Cells[FirstRow, 10].Value = "Spherical  Right Eye";
                                        workSheet.Cells[FirstRow, 11].Value = "Cyclinderical Right Eye";
                                        workSheet.Cells[FirstRow, 12].Value = "Axix  Right Eye";
                                        workSheet.Cells[FirstRow, 13].Value = "Spherical Left Eye";
                                        workSheet.Cells[FirstRow, 14].Value = "Cyclinderical  Left Eye";
                                        workSheet.Cells[FirstRow, 15].Value = "Axix Left Eye";
                                        workSheet.Cells[FirstRow, 16].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 17].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 18].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 19].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 20].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 2 || FirstColumn == 17)
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
                                                else if (!(FirstColumn == 6 || FirstColumn == 12 || FirstColumn == 15))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();
                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "OptometristInspection")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;

                                        workSheet.Cells[FirstRow, 1].Value = "Test Type";
                                        workSheet.Cells[FirstRow, 2].Value = "Code";
                                        workSheet.Cells[FirstRow, 3].Value = "Name";
                                        workSheet.Cells[FirstRow, 4].Value = "Father Name";
                                        workSheet.Cells[FirstRow, 5].Value = "Age";
                                        workSheet.Cells[FirstRow, 6].Value = "School Name";
                                        workSheet.Cells[FirstRow, 7].Value = "Class";
                                        workSheet.Cells[FirstRow, 8].Value = "Section";
                                        workSheet.Cells[FirstRow, 9].Value = "Test Date";
                                        workSheet.Cells[FirstRow, 10].Value = "Has Chief Complain";
                                        workSheet.Cells[FirstRow, 11].Value = "Chief Complain Remarks";
                                        workSheet.Cells[FirstRow, 12].Value = "Has Occular History";
                                        workSheet.Cells[FirstRow, 13].Value = "Occular History Remarks";
                                        workSheet.Cells[FirstRow, 14].Value = "Has Medical History";
                                        workSheet.Cells[FirstRow, 15].Value = "Medical History Remarks";
                                        workSheet.Cells[FirstRow, 16].Value = "Distance Vision Right Eye Unaided";
                                        workSheet.Cells[FirstRow, 17].Value = "Distance Vision Right Eye With Glasses";
                                        workSheet.Cells[FirstRow, 18].Value = "Distance Vision Right Eye Pin Hole";
                                        workSheet.Cells[FirstRow, 19].Value = "NearVision RightEye";
                                        workSheet.Cells[FirstRow, 20].Value = "Need Cyclo Refraction Right Eye";
                                        workSheet.Cells[FirstRow, 21].Value = "NeedCycloRefractionRemarks RightEye";
                                        workSheet.Cells[FirstRow, 22].Value = "DistanceVision LeftEye Unaided";
                                        workSheet.Cells[FirstRow, 23].Value = "DistanceVision LeftEye WithGlasses";
                                        workSheet.Cells[FirstRow, 24].Value = "DistanceVision LeftEye PinHole";
                                        workSheet.Cells[FirstRow, 25].Value = "NearVision LeftEye";
                                        workSheet.Cells[FirstRow, 26].Value = "Need Cyclo Refraction Left Eye";
                                        workSheet.Cells[FirstRow, 27].Value = "NeedCycloRefractionRemarks LeftEye";
                                        workSheet.Cells[FirstRow, 28].Value = "Spherical  Right Eye";
                                        workSheet.Cells[FirstRow, 29].Value = "Cyclinderical Right Eye";
                                        workSheet.Cells[FirstRow, 30].Value = "Axix  Right Eye";
                                        workSheet.Cells[FirstRow, 31].Value = "Near Add  Right Eye";
                                        workSheet.Cells[FirstRow, 32].Value = "Spherical Left Eye";
                                        workSheet.Cells[FirstRow, 33].Value = "Cyclinderical  Left Eye";
                                        workSheet.Cells[FirstRow, 34].Value = "Axix Left Eye";
                                        workSheet.Cells[FirstRow, 35].Value = "Near Add Left Eye";
                                        workSheet.Cells[FirstRow, 36].Value = "Douchrome Test";
                                        workSheet.Cells[FirstRow, 37].Value = "Color Blindness Test";
                                        workSheet.Cells[FirstRow, 38].Value = "Retinoscopy Right Eye";
                                        workSheet.Cells[FirstRow, 39].Value = "Tests after Cycloplegic Refraction Right Eye";
                                        workSheet.Cells[FirstRow, 40].Value = "Condition  Right Eye";
                                        workSheet.Cells[FirstRow, 41].Value = "Meridian 1  Right Eye";
                                        workSheet.Cells[FirstRow, 42].Value = "Meridian 2  Right Eye";
                                        workSheet.Cells[FirstRow, 43].Value = "Final Prescription  Right Eye";
                                        workSheet.Cells[FirstRow, 44].Value = "Retinoscopy Left Eye";
                                        workSheet.Cells[FirstRow, 45].Value = "Tests after Cycloplegic Refraction Left Eye";
                                        workSheet.Cells[FirstRow, 46].Value = "Condition  Left Eye";
                                        workSheet.Cells[FirstRow, 47].Value = "Meridian 1  Left Eye";
                                        workSheet.Cells[FirstRow, 48].Value = "Meridian 2  Left Eye";
                                        workSheet.Cells[FirstRow, 49].Value = "Final Prescription  Left Eye";
                                        workSheet.Cells[FirstRow, 50].Value = "Hirchberg Distance";
                                        workSheet.Cells[FirstRow, 51].Value = "Hirchberg Near";
                                        workSheet.Cells[FirstRow, 52].Value = "Ophthalmoscope Right Eye";
                                        workSheet.Cells[FirstRow, 53].Value = "Pupillary Reactions Right Eye";
                                        workSheet.Cells[FirstRow, 54].Value = "Cover Uncovert Test  Right Eye";
                                        workSheet.Cells[FirstRow, 55].Value = "Extra Occular Muscle Test  Right Eye";
                                        workSheet.Cells[FirstRow, 56].Value = "Ophthalmoscope Left Eye";
                                        workSheet.Cells[FirstRow, 57].Value = "Pupillary Reactions Left Eye";
                                        workSheet.Cells[FirstRow, 58].Value = "Cover Uncovert Test  Left Eye";
                                        workSheet.Cells[FirstRow, 59].Value = "Extra Occular Muscle Test  Left Eye";
                                        workSheet.Cells[FirstRow, 60].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 61].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 62].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 63].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 64].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 9 || FirstColumn == 61)
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
                                                else if (!(FirstColumn == 30 || FirstColumn == 34))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();
                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "Treatment")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;

                                        workSheet.Cells[FirstRow, 1].Value = "Test Type";
                                        workSheet.Cells[FirstRow, 2].Value = "Code";
                                        workSheet.Cells[FirstRow, 3].Value = "Name";
                                        workSheet.Cells[FirstRow, 4].Value = "Father Name";
                                        workSheet.Cells[FirstRow, 5].Value = "Age";
                                        workSheet.Cells[FirstRow, 6].Value = "School Name";
                                        workSheet.Cells[FirstRow, 7].Value = "Class";
                                        workSheet.Cells[FirstRow, 8].Value = "Section";
                                        workSheet.Cells[FirstRow, 9].Value = "Test Date";
                                        workSheet.Cells[FirstRow, 10].Value = "Diagnosis";
                                        workSheet.Cells[FirstRow, 11].Value = "Diagnosis Remarks";
                                        workSheet.Cells[FirstRow, 12].Value = "Sub Diagnosis";
                                        workSheet.Cells[FirstRow, 13].Value = "Treatment";
                                        workSheet.Cells[FirstRow, 14].Value = "Sub Treatment";
                                        workSheet.Cells[FirstRow, 15].Value = "Medicine Prescribed";
                                        workSheet.Cells[FirstRow, 16].Value = "Next Visit";
                                        workSheet.Cells[FirstRow, 17].Value = "Surgery";
                                        workSheet.Cells[FirstRow, 18].Value = "Surgery Detail";
                                        workSheet.Cells[FirstRow, 19].Value = "Surgery Detail Remarks";
                                        workSheet.Cells[FirstRow, 20].Value = "Referal";
                                        workSheet.Cells[FirstRow, 21].Value = "Mother Name";
                                        workSheet.Cells[FirstRow, 22].Value = "Mother Cell";
                                        workSheet.Cells[FirstRow, 23].Value = "Father Cell";
                                        workSheet.Cells[FirstRow, 24].Value = "Address 1";
                                        workSheet.Cells[FirstRow, 25].Value = "Address 2";
                                        workSheet.Cells[FirstRow, 26].Value = "District";
                                        workSheet.Cells[FirstRow, 27].Value = "Town";
                                        workSheet.Cells[FirstRow, 28].Value = "City";
                                        workSheet.Cells[FirstRow, 29].Value = "Parents Agree (Yes / No)";
                                        workSheet.Cells[FirstRow, 30].Value = "Not Agree Reason";
                                        workSheet.Cells[FirstRow, 31].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 32].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 33].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 34].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 35].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 9 || FirstColumn == 30)
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
                                                else if (!(FirstColumn == 5))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();
                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }
                    else if (strForm == "GlassDespense")
                    {
                        DataTable dtSchool = ReportData;
                        if (dtSchool != null)
                        {
                            if (dtSchool.Rows.Count > 0)
                            {
                                string AllorNaInDropDown = "All";

                                int FirstRow = 1;
                                int LastRow = dtSchool.Rows.Count;

                                int FirstColumn = 1;
                                int LastColumn = dtSchool.Rows.Count;

                                int HeadingHeaderColumn = dtSchool.Columns.Count;

                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    ExcelWorksheet workSheet;

                                    workSheet = excel.Workbook.Worksheets.Add(strForm);

                                    int iPrint_Record = 0;
                                    int iPrint_Column = 1;

                                    //iPrint_Record++;
                                    //FirstRow++;

                                    FirstColumn = 1;

                                    DataView dvMainReport = (DataView)dtSchool.DefaultView;
                                    DataTable dtMainReport_FilteredTable = dvMainReport.ToTable();

                                    if (dtMainReport_FilteredTable.Rows.Count > 0)
                                    {
                                        //FirstRow++;

                                        // setting the properties
                                        // of the work sheet 
                                        workSheet.TabColor = System.Drawing.Color.Black;
                                        workSheet.DefaultRowHeight = 12;

                                        // Setting the properties
                                        // of the first row
                                        workSheet.Row(FirstRow).Height = 20;
                                        workSheet.Row(FirstRow).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        workSheet.Row(FirstRow).Style.Font.Bold = true;

                                        workSheet.Cells[FirstRow, 1].Value = "Test Type";
                                        workSheet.Cells[FirstRow, 2].Value = "Code";
                                        workSheet.Cells[FirstRow, 3].Value = "Name";
                                        workSheet.Cells[FirstRow, 4].Value = "Father Name";
                                        workSheet.Cells[FirstRow, 5].Value = "Age";
                                        workSheet.Cells[FirstRow, 6].Value = "School Name";
                                        workSheet.Cells[FirstRow, 7].Value = "Class";
                                        workSheet.Cells[FirstRow, 8].Value = "Section";
                                        workSheet.Cells[FirstRow, 9].Value = "Test Date";
                                        workSheet.Cells[FirstRow, 10].Value = "Vision with Glasses  Right Eye";
                                        workSheet.Cells[FirstRow, 11].Value = "Vision with Glasses Left Eye";
                                        workSheet.Cells[FirstRow, 12].Value = "Student Satisfication";
                                        workSheet.Cells[FirstRow, 13].Value = "Unsatisfied";
                                        workSheet.Cells[FirstRow, 14].Value = "Unsatisfied Reason";
                                        workSheet.Cells[FirstRow, 15].Value = "Spherical Right Eye";
                                        workSheet.Cells[FirstRow, 16].Value = "Cyclinderical Right Eye";
                                        workSheet.Cells[FirstRow, 17].Value = "Axix Right Eye";
                                        workSheet.Cells[FirstRow, 18].Value = "Near Add Right Eye";
                                        workSheet.Cells[FirstRow, 19].Value = "Spherical  Left Eye";
                                        workSheet.Cells[FirstRow, 20].Value = "Cyclinderical Left Eye";
                                        workSheet.Cells[FirstRow, 21].Value = "Axix Left Eye";
                                        workSheet.Cells[FirstRow, 22].Value = "Near Add Left Eye";
                                        workSheet.Cells[FirstRow, 23].Value = "Followup Required";
                                        workSheet.Cells[FirstRow, 24].Value = "Picture Available";
                                        workSheet.Cells[FirstRow, 25].Value = "Login User Id";
                                        workSheet.Cells[FirstRow, 26].Value = "Entry Date";
                                        workSheet.Cells[FirstRow, 27].Value = "Entry Operation";
                                        workSheet.Cells[FirstRow, 28].Value = "Entry Terminal Name";
                                        workSheet.Cells[FirstRow, 29].Value = "Entry Terminal IP Address";

                                        FirstRow++;
                                        FirstColumn++;
                                        iPrint_Column++;

                                        foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                        {

                                            FirstColumn = 1;
                                            LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                            foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                            {
                                                if (FirstColumn == 9 || FirstColumn == 26)
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
                                                else if (!(FirstColumn == 17 || FirstColumn == 21))
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

                                        Utilities.SetColumnsWidth(workSheet);

                                        Response.Clear();
                                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                        Response.AddHeader("content-disposition", "attachment;  filename=" + strForm + ".xlsx");
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();
                                    }
                                    else
                                    {
                                        // No Data
                                    }
                                }
                            }
                            else
                            {
                                // No Data
                            }
                        }
                    }

                }
                else
                {
                    //No data                       

                }

                #endregion DataArea

            }
            catch (Exception ex)
            {
                //
            }
        }

        private void PrintRR(DataTable ReportData)
        {

            try
            {
                using (ExcelPackage pck = new ExcelPackage())
                {
                    //Create the worksheet

                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Sheet1");


                    #region DataArea


                    if (ReportData.Rows.Count > 0)
                    {
                        ws.Cells[1, 1].LoadFromDataTable(ReportData, true);

                        //using (ExcelRange HeaderRow = ws.Cells[1, 1, 1, ReportData.Columns.Count])
                        //{
                        //    HeaderRow.Style.Font.Color = ExcelColor.;
                        //    HeaderRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        //}
                    }
                    else
                    {
                        //No data                       

                    }

                    #endregion DataArea

                    #region Download Excel File
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=Report.xlsx");
                    Response.BinaryWrite(pck.GetAsByteArray());
                    Response.Flush();
                    Response.End(); //this cause updatepanel thread to abort and cause exception to solve, add view button in triggers. Zeeshanef
                    #endregion
                }
            }
            catch (Exception ex)
            {
                //
            }


        }

        private void ClearForm()
        {
            InitForm();
            //gvRole.DataSource = null;
            gv_TransactionList.DataBind();
        }

        private void CleatValidation()
        {
            lbl_error.Text = "";
        }

        private void ClearValidation()
        {
            lbl_error.Text = "";
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    string strForm = ddlForm.SelectedItem.Text;
                    DateTime dtFrom = DateTime.Parse(txtTestDateFrom.Text);
                    DateTime dtTo = DateTime.Parse(txtTestDateTo.Text);

                    if (strForm == "School Enrollment")
                    {
                        DataTable dtSchoolEnrollmentView = dx.sp_TransactionList_SchoolEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtSchoolEnrollmentView, "SchoolEnrollment");
                    }
                    else if (strForm == "Student Enrollment")
                    {
                        DataTable dtStudentEnrollmentView = dx.sp_TransactionList_StudentEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtStudentEnrollmentView, "StudentEnrollment");
                        //PrintRR(dtStudentEnrollmentView);
                    }
                    else if (strForm == "Teacher Enrollment")
                    {
                        DataTable dtTeacherEnrollmentView = dx.sp_TransactionList_TeacherEnrollment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtTeacherEnrollmentView, "TeacherEnrollment");
                    }
                    else if (strForm == "Auto Refraction Inspection")
                    {
                        DataTable dtAutoRefTest = dx.sp_TransactionList_AutoRefTest_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtAutoRefTest, "AutoRefractionInspection");
                    }
                    else if (strForm == "Optometrist Inspection")
                    {
                        DataTable dtOptometer = dx.sp_TransactionList_Otpometrist_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtOptometer, "OptometristInspection");
                    }
                    else if (strForm == "Diagnosis / Treatment / Next Visit")
                    {
                        DataTable dtTreatment = dx.sp_TransactionList_Treatment_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtTreatment, "Treatment");
                    }
                    else if (strForm == "Followup visit dispensing Glasses")
                    {
                        var dtGlassDespense = dx.sp_TransactionList_GlassDespenseStudent_TestDate(strForm, dtFrom.ToString("yyyy-MM-dd"), dtTo.ToString("yyyy-MM-dd")).ToList().ToDataTable();

                        PrintReport(dtGlassDespense, "GlassDespense");
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
    }
}