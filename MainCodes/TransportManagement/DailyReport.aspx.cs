using OfficeOpenXml;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportManagement.Models;


namespace TransportManagement
{
    public partial class DailyReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "DailyReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                txtTestDateFrom.Text = Utilities.GetDate();
                txtTestDateFrom.Focus();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    DateTime dtFrom = DateTime.Parse(txtTestDateFrom.Text);

                    var dtDailyReport = dx.sp_DailyReport_School(dtFrom).SingleOrDefault();

                    lblSchoolName_Student.Text = dtDailyReport.SchoolName_1.ToString();
                    lblSchoolName_Student_2.Text = dtDailyReport.SchoolName_2.ToString();
                    lblSchoolName_Student_3.Text = dtDailyReport.SchoolName_3.ToString();

                    lblSchoolName_Teacher.Text = dtDailyReport.SchoolName_4.ToString();
                    lblSchoolName_Teacher_2.Text = dtDailyReport.SchoolName_5.ToString();
                    lblSchoolName_Teacher_3.Text = dtDailyReport.SchoolName_6.ToString();

                    //string sSchoolName_Student = dtDailyReport.SchoolName_EyeScreening_Student.ToString();
                    //string[] strSchool_Student = sSchoolName_Student.Split(',');

                    //try
                    //{
                    //    lblSchoolName_Student.Text = strSchool_Student[0].ToString();
                    //    lblSchoolName_Student_2.Text = strSchool_Student[1].ToString();
                    //    lblSchoolName_Student_3.Text = strSchool_Student[2].ToString();
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    lblStudentEnrolled.Text = dtDailyReport.StudentsEnrolled.ToString();
                    lblStudentsAutoRef.Text = dtDailyReport.StudentsAutoRef.ToString();
                    lblStudentsOptometrist.Text = dtDailyReport.StudentsOptometrist.ToString();
                    lblStudentswearingglasses.Text = dtDailyReport.StudentsWearningGlasses.ToString();
                    lblStudentssuggestedGlasses.Text = dtDailyReport.StudentssuggestedGlasses.ToString();
                    lblStudentsforCycloplagicRefraction.Text = dtDailyReport.StudentsforCycloplagicRefraction.ToString();
                    lblStudentswithotherissues.Text = dtDailyReport.Studentswithotherissues.ToString();
                    lblStudentforSurgery.Text = dtDailyReport.StudentforSurgery.ToString();

                    //lblSchoolName_Teacher.Text = dtDailyReport.SchoolName_EyeScreening_Teacher.ToString();

                    //string sSchoolName_Teacher = dtDailyReport.SchoolName_EyeScreening_Teacher.ToString();
                    //string[] strSchool_Teacher = sSchoolName_Teacher.Split(',');

                    //try
                    //{
                    //    lblSchoolName_Teacher.Text = strSchool_Teacher[0].ToString();
                    //    lblSchoolName_Teacher_2.Text = strSchool_Teacher[1].ToString();
                    //    lblSchoolName_Teacher_3.Text = strSchool_Teacher[2].ToString();
                    //}
                    //catch (Exception ex)
                    //{

                    //}

                    lblTeacherEnrolled.Text = dtDailyReport.TeachersEnrolled.ToString();
                    lblTeachersAutoRef.Text = dtDailyReport.TeachersAutoRef.ToString();
                    lblTeachersOptometrist.Text = dtDailyReport.TeachersOptometrist.ToString();
                    lblTeacherswearingglasses.Text = dtDailyReport.TeachersWearningGlasses.ToString();
                    lblTeacherssuggestedGlasses.Text = dtDailyReport.TeacherssuggestedGlasses.ToString();
                    lblTeachersforCycloplagicRefraction.Text = dtDailyReport.TeachersforCycloplagicRefraction.ToString();
                    lblTeacherswithotherissues.Text = dtDailyReport.Teacherswithotherissues.ToString();
                    lblTeacherforSurgery.Text = dtDailyReport.TeacherforSurgery.ToString();

                    Label22.Text = dtDailyReport.SchoolName_GlassDispensing_Student.ToString();
                    Label24.Text = dtDailyReport.SchoolName_GlassDispensing_Teacher.ToString();

                    Label26.Text = dtDailyReport.StudentGlassestobedistributed.ToString();
                    Label28.Text = dtDailyReport.TeacherGlassestobedistributed.ToString();
                    Label30.Text = dtDailyReport.StudentGlassesdistributed.ToString();
                    Label32.Text = dtDailyReport.TeacherGlassesdistributed.ToString();
                    Label34.Text = dtDailyReport.StudentNotSatisfied.ToString();
                    Label36.Text = dtDailyReport.TeacherNotSatisfied.ToString();

                    Label40.Text = dtDailyReport.SchoolName_Cycloplegic_Student.ToString();
                    Label44.Text = dtDailyReport.StudentCycloRefractiontobedone.ToString();
                    Label48.Text = dtDailyReport.StudentCycloRefractiondone.ToString();

                    Label52.Text = dtDailyReport.UserId.ToString();

                    pnlCycloplegicRefraction.Visible = true;
                    if (dtDailyReport.StudentCycloRefractiondone.ToString() == "0")
                    {
                        pnlCycloplegicRefraction.Visible = false;
                    }

                    DataTable dtAbnormalities = dx.sp_DailyReport_School_StudentWithAbnormality(0, dtFrom, dtFrom).ToList().ToDataTable();
                    if (dtAbnormalities != null)
                    {
                        if (dtAbnormalities.Rows.Count > 0)
                        {
                            pnlAbnormalities.Visible = true;
                            gvAbnormalities.DataSource = dtAbnormalities;
                            gvAbnormalities.DataBind();
                        }
                        else
                        {
                            pnlAbnormalities.Visible = false;
                        }
                    }
                    else
                    {
                        pnlAbnormalities.Visible = false;
                    }

                    DataTable dtAbnormalities_Teacher = dx.sp_DailyReport_School_TeacherWithAbnormality(0, dtFrom, dtFrom).ToList().ToDataTable();
                    if (dtAbnormalities_Teacher != null)
                    {
                        if (dtAbnormalities_Teacher.Rows.Count > 0)
                        {
                            pnlAbnormaties_Teacher.Visible = true;
                            gvAbnormalities_Teacher.DataSource = dtAbnormalities_Teacher;
                            gvAbnormalities_Teacher.DataBind();
                        }
                        else
                        {
                            pnlAbnormaties_Teacher.Visible = false;
                        }
                    }
                    else
                    {
                        pnlAbnormaties_Teacher.Visible = false;
                    }

                    DataTable dtDailyReport_GlassDispense = dx.sp_DailyReport_School_GlassDispense(dtFrom).ToList().ToDataTable();
                    if (dtDailyReport_GlassDispense != null)
                    {
                        if (dtDailyReport_GlassDispense.Rows.Count > 0)
                        {
                            //pnlGlassDispense.Visible = true;
                            pnlGlassDispensingDetail.Visible = true;
                            gvGlassDispense.DataSource = dtDailyReport_GlassDispense;
                            gvGlassDispense.DataBind();

                            pnlGlassDispensingDetail_Teacher.Visible = true;
                            gvGlassDispense_Teacher.DataSource = dtDailyReport_GlassDispense;
                            gvGlassDispense_Teacher.DataBind();
                        }
                        else
                        {
                            //pnlGlassDispense.Visible = false;
                            pnlGlassDispensingDetail.Visible = false;
                            pnlGlassDispensingDetail_Teacher.Visible = false;
                        }
                    }
                    else
                    {
                        //pnlGlassDispense.Visible = false;
                        pnlGlassDispensingDetail.Visible = false;
                        pnlGlassDispensingDetail_Teacher.Visible = false;
                    }


                    DataTable dtGlassList_Student = dx.sp_DailyReport_School_GlassDispenseDetail_Student(dtFrom).ToList().ToDataTable();
                    if (dtGlassList_Student != null)
                    {
                        if (dtGlassList_Student.Rows.Count > 0)
                        {
                            pnlGlassDispensingList_Student.Visible = true;
                            gvGlassDispensingList_Student.DataSource = dtGlassList_Student;
                            gvGlassDispensingList_Student.DataBind();
                        }
                        else
                        {
                            pnlGlassDispensingList_Student.Visible = false;
                        }
                    }
                    else
                    {
                        pnlGlassDispensingList_Student.Visible = false;
                    }

                    DataTable dtGlassList_Teacher = dx.sp_DailyReport_School_GlassDispenseDetail_Teacher(dtFrom).ToList().ToDataTable();
                    if (dtGlassList_Teacher != null)
                    {
                        if (dtGlassList_Teacher.Rows.Count > 0)
                        {
                            pnlGlassDispensingList_Teacher.Visible = true;
                            gvGlassDispensingList_Teacher.DataSource = dtGlassList_Teacher;
                            gvGlassDispensingList_Teacher.DataBind();
                        }
                        else
                        {
                            pnlGlassDispensingList_Teacher.Visible = false;
                        }
                    }
                    else
                    {
                        pnlGlassDispensingList_Teacher.Visible = false;
                    }

                    DataTable dtAutoRefTestnotperformed = dx.sp_DailyReport_School_RemainingAutoRef(dtFrom).ToList().ToDataTable();
                    if (dtAutoRefTestnotperformed != null)
                    {
                        if (dtAutoRefTestnotperformed.Rows.Count > 0)
                        {
                            pnlAutoRefTestnotperformed.Visible = true;
                            gvAutoRefTestnotperformed.DataSource = dtAutoRefTestnotperformed;
                            gvAutoRefTestnotperformed.DataBind();
                        }
                        else
                        {
                            pnlAutoRefTestnotperformed.Visible = false;
                        }
                    }
                    else
                    {
                        pnlAutoRefTestnotperformed.Visible = false;
                    }

                    DataTable dtOptomertristTestnotperformed = dx.sp_DailyReport_School_RemainingOptometrist(dtFrom).ToList().ToDataTable();
                    if (dtOptomertristTestnotperformed != null)
                    {
                        if (dtOptomertristTestnotperformed.Rows.Count > 0)
                        {
                            pnlOptomertristTestnotperformed.Visible = true;
                            gvOptomertristTestnotperformed.DataSource = dtOptomertristTestnotperformed;
                            gvOptomertristTestnotperformed.DataBind();
                        }
                        else
                        {
                            pnlOptomertristTestnotperformed.Visible = false;
                        }
                    }
                    else
                    {
                        pnlOptomertristTestnotperformed.Visible = false;
                    }


                    DataTable dtDailyStaffPerformance = dx.sp_StaffPerformanceReport_DailyReport(dtFrom).ToList().ToDataTable();
                    if (dtDailyStaffPerformance != null)
                    {
                        if (dtDailyStaffPerformance.Rows.Count > 0)
                        {
                            pnlDailyStaffPerformance.Visible = true;
                            gvDailyStaffPerformance.DataSource = dtDailyStaffPerformance;
                            gvDailyStaffPerformance.DataBind();
                        }
                        else
                        {
                            pnlDailyStaffPerformance.Visible = false;
                        }
                    }
                    else
                    {
                        pnlDailyStaffPerformance.Visible = false;
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
            ClearValidation();
            try
            {
                DateTime dt = DateTime.Parse(txtTestDateFrom.Text);
            }
            catch
            {
                lbl_error.Text = "Invalid 'Date'.";
                return false;
            }
            if (txtTestDateFrom.Text.Trim() == "")
            {
                lbl_error.Text = "'Date' is required.";
                return false;
            }

            return true;
        }

        private void PrintReport(DataTable ReportData, string strForm)
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
                        int row = 1;
                        //Utilities.SetHeader(ws,ref row, 1, 1, ReportData.Columns.Count);
                        //Utilities.SetDataPrinting(ws, 2, 1, ReportData.Rows.Count, ReportData.Columns.Count);
                        //using (ExcelRange HeaderRow = ws.Cells[1, 1, 1, ReportData.Columns.Count])
                        //{
                        //    HeaderRow.Style.Font.Bold = true;
                        //    HeaderRow.Style.Font.Color.SetColor(Color.White);// = Color.White;
                        //    HeaderRow.Style.Fill.BackgroundColor.SetColor(Color.Blue);
                        //    HeaderRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        //}
                        //using (ExcelRange DataRow = ws.Cells[2, 1, ReportData.Rows.Count, ReportData.Columns.Count])
                        //{
                        //    DataRow.Style.Font.Color.SetColor(Color.Black);// = Color.White;
                        //    DataRow.Style.Fill.BackgroundColor.SetColor(Color.White);
                        //    DataRow.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        //}
                    }
                    else
                    {
                        //No data                       

                    }

                    #endregion DataArea

                    Utilities.SetColumnsWidth(ws);

                    #region Download Excel File
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=OpticianReport.xlsx");
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
            txtTestDateFrom.Text = Utilities.GetDate();
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
                    DateTime dtFrom = DateTime.Parse(txtTestDateFrom.Text);

                    if (rdoReportList.SelectedValue == "0")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=DailyReport&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "1")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptRefractedErrorReport_DailyReport&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "2")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptAbnormalitiesReport_DailyReport&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "3")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptGlassDispensingSummary&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "4")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptGlassDispensingDetail&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "5")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptMissingAutoRefTest&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);
                    }
                    else if (rdoReportList.SelectedValue == "6")
                    {
                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=rptMissingOptometristTest&TransactionDate=" + txtTestDateFrom.Text + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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
    }
}