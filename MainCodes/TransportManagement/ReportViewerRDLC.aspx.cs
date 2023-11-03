using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class ReportViewerRDLC : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string flag = Request.QueryString["flag"].ToString().Trim();

                //string msg = PrintReport(flag);
                string reportName = Request.QueryString["vReportName"].ToString().Trim();

                string msg = PrintReport(reportName);
                if (msg != string.Empty)
                {
                    //ShowMessage(msg);
                    ReportViewer1.Visible = false;
                }
                //this.CrystalReportViewer1.DisplayGroupTree = false;
                //ReportViewer1.LocalReport.ReportPath = "ReportsRDLC\\ReportTest.rdlc";
            }
        }

        private string PrintReport(string reportName)
        {
            try
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;

                switch (reportName)
                {
                    case "EyeGlassPrescription_Student":
                        {
                            string studentAutoId = Request.QueryString["StudentAutoId"].ToString();
                            string reportType = Request.QueryString["ReportType"].ToString();

                            if (reportType == "0")
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptEyeGlassPrescription_Student.rdlc");
                            }
                            else
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptEyeGlassPrescription_Student_preprint.rdlc");
                            }
                            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptEyeGlassPrescription_Student.rdlc");

                            DataTable dt = dx.sp_EyeGlassPrescription_Student(studentAutoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "EyeGlassPrescription_Teacher":
                        {

                            string teacherAutoId = Request.QueryString["TeacherAutoId"].ToString();
                            string reportType = Request.QueryString["ReportType"].ToString();
                            if (reportType == "0")
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptEyeGlassPrescription_Teacher.rdlc");
                            }
                            else
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptEyeGlassPrescription_Teacher_preprint.rdlc");
                            }
                            DataTable dt = dx.sp_EyeGlassPrescription_Teacher(teacherAutoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "DailyReport":
                        {

                            DateTime transDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptDailyReport.rdlc");

                            DataTable dt = dx.sp_DailyReport_School(transDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                DataTable dt2 = dx.sp_StaffPerformanceReport_DailyReport(transDate).ToList().ToDataTable();
                                DataTable dt3 = dx.sp_DailyReport_School_GlassDispense(transDate).ToList().ToDataTable();
                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dt3));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("TransactionDate", transDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptRefractedErrorReport_DailyReport":
                        {
                            int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorReport.rdlc");

                            DataTable dt = dx.sp_RecractedErrors_StudentReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                //SetReportParameters(fromDate, toDate);

                                //int n = 2;
                                //ReportParameter[] parms = new ReportParameter[];
                                //parms[0] = new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy"));
                                //parms[1] = new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy"));
                                //ReportViewer1.LocalReport.SetParameters(parms);

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", "All"));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptAbnormalitiesReport_DailyReport":
                        {
                            int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptAbnormalitiesReport_DailyReport.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_StudentWithAbnormality(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptGlassDispensingSummary":
                        {
                            //int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptGlassDispensingSummary.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_GlassDispense(fromDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptGlassDispensingDetail":
                        {
                            int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptGlassDispensingDetail.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_GlassDispenseDetail(fromDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptMissingAutoRefTest":
                        {
                            int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptMissingAutoRefTest.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_RemainingAutoRef(fromDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptMissingOptometristTest":
                        {
                            int iSchoolAutoId = 0; // int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptMissingOptometristTest.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_RemainingOptometrist(fromDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "OpticianReportStudent":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptOpticianReport_Student.rdlc");

                            DataTable dt = dx.sp_ReportforOptincian_Student(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "OpticianReportTeacher":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptOpticianReport_Teacher.rdlc");

                            DataTable dt = dx.sp_ReportforOptincian_Teacher(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "OpticianReport":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptOpticianReport.rdlc");

                            DataTable dt = dx.sp_ReportforOptincian(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReportforSchool":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptforSchool.rdlc");

                            DataTable dt = dx.sp_ReportforSchool(iSchoolAutoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                DateTime dtFrom = DateTime.Parse("2022-07-01");
                                DateTime dtTo = DateTime.Now;

                                DataTable dt2 = dx.sp_DailyReport_School_StudentWithAbnormality(iSchoolAutoId, dtFrom, dtTo).ToList().ToDataTable();

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt2));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }
                            break;
                        }
                    case "rptRefractedErrorReport_SchoolReport":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorReport.rdlc");

                            DataTable dt = dx.sp_RecractedErrors_StudentReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptAbnormalitiesReport_SchoolReport":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptAbnormalitiesReport_DailyReport.rdlc");

                            DataTable dt = dx.sp_DailyReport_School_StudentWithAbnormality(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "SchoolList":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptSchoolList.rdlc");

                            DataTable dt = dx.sp_SchoolList(iSchoolAutoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }
                            break;
                        }
                    case "AttendanceReport":
                        {

                            DateTime transDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            string strReportType = Request.QueryString["ReportType"].ToString();
                            string strUserId = Request.QueryString["UserId"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptAttendanceReport.rdlc");

                            DataTable dt = dx.sp_StaffAttendanceReport(transDate, strUserId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "StaffPerformanceReport":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            string strUserId = Request.QueryString["UserId"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptStaffPerformanceReport.rdlc");

                            DataTable dt = dx.sp_StaffPerformanceReport(iSchoolAutoId, fromDate, toDate, strUserId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "VisitSummary_Student":
                        {
                            int studentAutoId = int.Parse(Request.QueryString["StudentAutoId"].ToString());
                            int OptoId = int.Parse(Request.QueryString["OptoId"].ToString());

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptVisitSummary_Student.rdlc");

                            DataTable dt = dx.sp_VisitSummary_Student(studentAutoId, OptoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "VisitSummary_Teacher":
                        {
                            int TeacherAutoId = int.Parse(Request.QueryString["TeacherAutoId"].ToString());
                            int OptoId = int.Parse(Request.QueryString["OptoId"].ToString());

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptVisitSummary_Teacher.rdlc");

                            DataTable dt = dx.sp_VisitSummary_Teacher(TeacherAutoId, OptoId).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptSummaryGlassesProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptSummaryGlassesProvided.rdlc");

                            DataTable dt = dx.sp_GlassesProvided_SummaryReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptDetailGlassesProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptDetailGlassesProvided.rdlc");

                            DataTable dt = dx.sp_GlassesProvided_DetailReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptDetailGlassesProvidedNotProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptDetailGlassesProvidedNotProvided.rdlc");

                            DataTable dt = dx.sp_GlassesProvidedNotProvided_DetailReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptRefractedErrorReport":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorReport.rdlc");

                            DataTable dt = dx.sp_RecractedErrors_StudentReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptRefractedErrorGlassesProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorGlassesProvided.rdlc");

                            DataTable dt = dx.sp_RefractedErrors_GlassesprovidedReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptRefractedErrorGlassesnotProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorGlassesnotProvided.rdlc");

                            DataTable dt = dx.sp_RefractedErrors_GlassesnotprovidedReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptRefractedErrorGlassesnotProvidedDetail":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorGlassesnotProvidedDetail.rdlc");

                            DataTable dt = dx.sp_RefractedErrors_GlassesnotprovidedDetailReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptCycloRefractionGlassesnotProvided":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptCycloRefractionGlassesnotProvided.rdlc");

                            DataTable dt = dx.sp_CycloRefractionReport(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                string sSchoolName = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchoolName = "All";
                                }
                                else
                                {
                                    var dtSchool = dx.sp_tblSchool_GetDetail(iSchoolAutoId).SingleOrDefault();
                                    sSchoolName = dtSchool.SchoolName.ToString();
                                }

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchoolName));
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    #region Commented Comprehensive Reports
                    //case "rptSummarized_Comprehensive_Report":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.ProcessingMode = ProcessingMode.Local;

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_AllSchoolWise.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptDetailed_Comprehensive_Report":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_AllSchoolWise_Detail.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptSummarized_Comprehensive_Report_School_wise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_SchoolWise.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptDetailed_Comprehensive_Report_School_wise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_SchoolWise_Detail.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptSummarized_Comprehensive_Report_Class_wise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_ClassWise.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_ClassWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptDetailed_Comprehensive_Report_Class_wise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_ClassWise_Detail.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    #endregion Commented Comprehensive Report
                    case "rptSummarized_Comprehensive_Report_Section_wise":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_ClassSectionWise.rdlc");

                            DataTable dt = dx.sp_tblTreatmentStudentReport_SectionWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptDetailed_Comprehensive_Report_Section_wise":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveStudentReport_ClassSectionWise_Detail.rdlc");

                            DataTable dt = dx.sp_tblTreatmentStudentReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "rptSummarized_Comprehensive_Report_TeacherWise":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveTeacherReport_AllSchoolWise.rdlc");

                            DataTable dt = dx.sp_tblTreatmentTeacherReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    #region Commented Comprehensive Report
                    //case "rptDetailed_Comprehensive_Report_TeacherWise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveTeacherReport_AllSchoolWise_Detail.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentTeacherReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    //case "rptSummarized_Comprehensive_Report_School_wise_TeacherWise":
                    //    {
                    //        int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                    //        DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                    //        DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                    //        string strReportType = Request.QueryString["ReportType"].ToString();

                    //        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveTeacherReport_SchoolWise.rdlc");

                    //        DataTable dt = dx.sp_tblTreatmentTeacherReport_SchoolWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                    //        if (dt != null)
                    //        {
                    //            //DataSet ds = new DataSet();
                    //            //ds.Tables.Add(dt);

                    //            // Supply a DataTable corresponding to each report dataset                                    
                    //            ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                    //            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                    //            ReportParameterCollection reportparameter = new ReportParameterCollection();
                    //            reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                    //            reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                    //            ReportViewer1.LocalReport.SetParameters(reportparameter);

                    //            ReportViewer1.DataBind();
                    //            ReportViewer1.LocalReport.Refresh();
                    //        }

                    //        break;
                    //    }
                    #endregion Commented Comprehensive Report
                    case "rptDetailed_Comprehensive_Report_School_wise_TeacherWise":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptComprehensiveTeacherReport_SchoolWise_Detail.rdlc");

                            DataTable dt = dx.sp_tblTreatmentTeacherReport_StudentWise(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "RefractedErrorReportStudent":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            int iReportType = int.Parse(Request.QueryString["ReportType"].ToString());
                            int iRefractedError = int.Parse(Request.QueryString["RefractedError"].ToString());

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptRefractedErrorReport_Student.rdlc");

                            DataTable dt = dx.sp_ReportforRefractedError_Student(iSchoolAutoId, fromDate, toDate, iReportType, iRefractedError).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReportforAbnormalities":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());
                            int iClassAutoId = int.Parse(Request.QueryString["ClassAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            int iRefractiveError = int.Parse(Request.QueryString["RefractiveError"].ToString());
                            int iNeedscyclopegicrefration = int.Parse(Request.QueryString["Needscyclopegicrefration"].ToString());
                            int iSquintStrabismus = int.Parse(Request.QueryString["SquintStrabismus"].ToString());
                            int iLazyEyeAmblyopia = int.Parse(Request.QueryString["LazyEyeAmblyopia"].ToString());
                            int iColorblindnessAchromatopsia = int.Parse(Request.QueryString["ColorblindnessAchromatopsia"].ToString());
                            int iCataract = int.Parse(Request.QueryString["Cataract"].ToString());
                            int iTraumaticCataract = int.Parse(Request.QueryString["TraumaticCataract"].ToString());
                            int iKeratoconus = int.Parse(Request.QueryString["Keratoconus"].ToString());
                            int iAnisometropia = int.Parse(Request.QueryString["Anisometropia"].ToString());
                            int iPtosis = int.Parse(Request.QueryString["Ptosis"].ToString());
                            int iNystagmus = int.Parse(Request.QueryString["Nystagmus"].ToString());
                            int iLowVision = int.Parse(Request.QueryString["LowVision"].ToString());
                            int iCorneadefects = int.Parse(Request.QueryString["Corneadefects"].ToString());
                            int iPuplidefects = int.Parse(Request.QueryString["Puplidefects"].ToString());
                            int iPterygium = int.Parse(Request.QueryString["Pterygium"].ToString());
                            int iOther = int.Parse(Request.QueryString["Other"].ToString());
                            int iPresbyopia = int.Parse(Request.QueryString["Presbyopia"].ToString());
                            int iMyopia = int.Parse(Request.QueryString["Myopia"].ToString());
                            int iHypermetropia = int.Parse(Request.QueryString["Hypermetropia"].ToString());
                            int iAstigmatism = int.Parse(Request.QueryString["Astigmatism"].ToString());

                            if (iSchoolAutoId == 0 && iClassAutoId == 0)
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptforAbnormalities_School.rdlc");

                                DataTable dt = dx.sp_ReportforAbnormality_SchoolWise(iSchoolAutoId, iClassAutoId, fromDate, toDate,
                                                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).ToList().ToDataTable();
                                if (dt != null)
                                {
                                    //DataSet ds = new DataSet();
                                    //ds.Tables.Add(dt);

                                    // Supply a DataTable corresponding to each report dataset                                    
                                    ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                    string sClass = string.Empty;
                                    if (iClassAutoId == 0)
                                    {
                                        sClass = "All";
                                    }
                                    else
                                    {
                                        var dtClass = dx.sp_tblClass_GetDetail(iClassAutoId).SingleOrDefault();
                                        sClass = dtClass.ClassNo.ToString();
                                    }

                                    ReportParameterCollection reportparameter = new ReportParameterCollection();
                                    reportparameter.Add(new ReportParameter("Class", sClass));
                                    reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                    reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                    ReportViewer1.LocalReport.SetParameters(reportparameter);

                                    ReportViewer1.DataBind();
                                    ReportViewer1.LocalReport.Refresh();
                                }
                            }
                            else if (iSchoolAutoId == 0 && iClassAutoId != 0)
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptforAbnormalities_School.rdlc");

                                DataTable dt = dx.sp_ReportforAbnormality_SchoolWise(iSchoolAutoId, iClassAutoId, fromDate, toDate,
                                                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).ToList().ToDataTable();
                                if (dt != null)
                                {
                                    //DataSet ds = new DataSet();
                                    //ds.Tables.Add(dt);

                                    // Supply a DataTable corresponding to each report dataset                                    
                                    ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                    string sClass = string.Empty;
                                    if (iClassAutoId == 0)
                                    {
                                        sClass = "All";
                                    }
                                    else
                                    {
                                        var dtClass = dx.sp_tblClass_GetDetail(iClassAutoId).SingleOrDefault();
                                        sClass = dtClass.ClassNo.ToString();
                                    }

                                    ReportParameterCollection reportparameter = new ReportParameterCollection();
                                    reportparameter.Add(new ReportParameter("Class", sClass));
                                    reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                    reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                    ReportViewer1.LocalReport.SetParameters(reportparameter);

                                    ReportViewer1.DataBind();
                                    ReportViewer1.LocalReport.Refresh();
                                }
                            }
                            else if (iSchoolAutoId != 0 && iClassAutoId == 0)
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptforAbnormalities.rdlc");

                                DataTable dt = dx.sp_ReportforAbnormality(iSchoolAutoId, fromDate, toDate,
                                                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).ToList().ToDataTable();
                                if (dt != null)
                                {
                                    //DataSet ds = new DataSet();
                                    //ds.Tables.Add(dt);

                                    // Supply a DataTable corresponding to each report dataset                                    
                                    ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                    ReportParameterCollection reportparameter = new ReportParameterCollection();
                                    reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                    reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                    ReportViewer1.LocalReport.SetParameters(reportparameter);

                                    ReportViewer1.DataBind();
                                    ReportViewer1.LocalReport.Refresh();
                                }
                            }
                            else if (iSchoolAutoId != 0 && iClassAutoId != 0)
                            {
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptforAbnormalities_Class.rdlc");

                                DataTable dt = dx.sp_ReportforAbnormality_ClassWise(iSchoolAutoId, iClassAutoId, fromDate, toDate,
                                                                        iRefractiveError, iNeedscyclopegicrefration, iSquintStrabismus, iLazyEyeAmblyopia, iColorblindnessAchromatopsia, iCataract,
                                                                        iTraumaticCataract, iKeratoconus, iAnisometropia, iPtosis, iNystagmus, iLowVision, iCorneadefects, iPuplidefects,
                                                                        iPterygium, iOther, iPresbyopia, iMyopia, iHypermetropia, iAstigmatism).ToList().ToDataTable();
                                if (dt != null)
                                {
                                    //DataSet ds = new DataSet();
                                    //ds.Tables.Add(dt);

                                    // Supply a DataTable corresponding to each report dataset                                    
                                    ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                    ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                    ReportParameterCollection reportparameter = new ReportParameterCollection();
                                    reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                    reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                    ReportViewer1.LocalReport.SetParameters(reportparameter);

                                    ReportViewer1.DataBind();
                                    ReportViewer1.LocalReport.Refresh();
                                }
                            }
                            break;
                        }
                    case "ReligionReport_City":
                        {
                            string sCity = Request.QueryString["City"].ToString();

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptReligionReport_City.rdlc");

                            DataTable dt = dx.sp_GetCityWiseReligionDetail(sCity, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                if (sCity == "0")
                                {
                                    sCity = "All";
                                }
                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("City", sCity));
                                //reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                //reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReligionReport_District":
                        {
                            string sDistrict = Request.QueryString["District"].ToString();

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptReligionReport_District.rdlc");

                            DataTable dt = dx.sp_GetDistrictWiseReligionDetail(sDistrict, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                if (sDistrict == "0")
                                {
                                    sDistrict = "All";
                                }
                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("District", sDistrict));
                                //reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                //reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReligionReport_Town":
                        {
                            string sTown = Request.QueryString["Town"].ToString();

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptReligionReport_Town.rdlc");

                            DataTable dt = dx.sp_GetTownWiseReligionDetail(sTown, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                if (sTown == "0")
                                {
                                    sTown = "All";
                                }
                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("Town", sTown));
                                //reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                //reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReligionReport_SchoolLevel":
                        {
                            string sSchoolLevel = Request.QueryString["SchoolLevel"].ToString();

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptReligionReport_SchoolLevel.rdlc");

                            DataTable dt = dx.sp_GetSchoolLevelWiseReligionDetail(sSchoolLevel, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));

                                if (sSchoolLevel == "0")
                                {
                                    sSchoolLevel = "All";
                                }
                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolLevel", sSchoolLevel));
                                //reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                //reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }
                    case "ReligionReport_School":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["School"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~\\ReportsRDLC\\rptReligionReport_School.rdlc");

                            DataTable dt = dx.sp_GetSchoolWiseReligionDetail(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                            if (dt != null)
                            {
                                //DataSet ds = new DataSet();
                                //ds.Tables.Add(dt);

                                // Supply a DataTable corresponding to each report dataset                                    
                                ReportViewer1.LocalReport.DataSources.Clear();   // For Refreshing Report
                                ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                                string sSchool = string.Empty;
                                if (iSchoolAutoId == 0)
                                {
                                    sSchool = "All";
                                }
                                else
                                {
                                    string q = "SELECT * FROM tblSchool WHERE SchoolAutoId = " + iSchoolAutoId + "";
                                    var dtSchool = dx.Database.SqlQuery<tblSchool>(q).SingleOrDefault();
                                    if (dtSchool != null)
                                    {
                                        sSchool = dtSchool.SchoolName.ToString().Trim();
                                    }
                                }
                                ReportParameterCollection reportparameter = new ReportParameterCollection();
                                reportparameter.Add(new ReportParameter("SchoolName", sSchool));
                                //reportparameter.Add(new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy")));
                                //reportparameter.Add(new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy")));
                                ReportViewer1.LocalReport.SetParameters(reportparameter);

                                ReportViewer1.DataBind();
                                ReportViewer1.LocalReport.Refresh();
                            }

                            break;
                        }

                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }

        private void SetReportParameters(DateTime fromDate, DateTime toDate)
        {
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("DateFrom", fromDate.ToString("dd-MMM-yyyy"));
            parameters[1] = new ReportParameter("DateTo", toDate.ToString("dd-MMM-yyyy"));
            this.ReportViewer1.LocalReport.SetParameters(parameters);
        }
    }
}