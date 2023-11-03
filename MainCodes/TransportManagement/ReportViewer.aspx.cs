using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportManagement.Models;

namespace TransportManagement
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        SecoffEntities dx = new SecoffEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //string flag = Request.QueryString["flag"].ToString().Trim();

                //string msg = PrintReport(flag);
                string reportName = Request.QueryString["vReportName"].ToString().Trim();

                string msg = PrintReport(reportName);
                if (msg != string.Empty)
                {
                    //ShowMessage(msg);
                    this.CrystalReportViewer1.Visible = false;
                }
                //this.CrystalReportViewer1.DisplayGroupTree = false;
            }
            catch (Exception ex)
            { }
        }

        private string PrintReport(string reportName)
        {
            try
            {
                switch (reportName)
                {
                    case "EyeGlassPrescription_Student":
                        {
                            int studentAutoId = int.Parse(Request.QueryString["StudentAutoId"].ToString());

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptEyeGlassPrescription_Student.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_EyeGlassPrescription_Student(studentAutoId).ToList().ToDataTable();

                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getEyeGlassPrescription_Student(ds));

                            this.CrystalReportViewer1.ReportSource = RptDoc;
                            this.CrystalReportViewer1.RefreshReport();
                            this.CrystalReportViewer1.DataBind();
                            CrystalReportViewer1.Visible = true;

                            break;
                        }
                    case "EyeGlassPrescription_Teacher":
                        {

                            int teacherAutoId = int.Parse(Request.QueryString["TeacherAutoId"].ToString());

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptEyeGlassPrescription_Teacher.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_EyeGlassPrescription_Teacher(teacherAutoId).ToList().ToDataTable();

                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getEyeGlassPrescription_Teacher(ds));

                            this.CrystalReportViewer1.ReportSource = RptDoc;
                            this.CrystalReportViewer1.RefreshReport();
                            this.CrystalReportViewer1.DataBind();
                            CrystalReportViewer1.Visible = true;

                            break;
                        }
                    case "DailyReport":
                        {

                            DateTime transDate = DateTime.Parse(Request.QueryString["TransactionDate"].ToString());
                            string strReportType = Request.QueryString["ReportType"].ToString();

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptDailyReport.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_DailyReport_School(transDate).ToList().ToDataTable();
                            
                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getDailyReport_School(ds));

                            if (strReportType == "0")
                            {
                                this.CrystalReportViewer1.ReportSource = RptDoc;
                                this.CrystalReportViewer1.RefreshReport();
                                this.CrystalReportViewer1.DataBind();
                                CrystalReportViewer1.Visible = true;
                            }
                            else
                            {
                                RptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, RptDoc.FileName);
                            }

                            break;
                        }
                    case "OpticianReportStudent":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptOpticianReport_Student.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_ReportforOptincian_Student(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();

                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getReportforOptician_Student(ds));

                            if (strReportType == "0")
                            {
                                this.CrystalReportViewer1.ReportSource = RptDoc;
                                this.CrystalReportViewer1.RefreshReport();
                                this.CrystalReportViewer1.DataBind();
                                CrystalReportViewer1.Visible = true;
                            }
                            else
                            {
                                RptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, RptDoc.FileName);
                            }
                            break;
                        }
                    case "OpticianReportTeacher":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptOpticianReport_Teacher.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_ReportforOptincian_Teacher(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();

                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getReportforOptician_Teacher(ds));

                            if (strReportType == "0")
                            {
                                this.CrystalReportViewer1.ReportSource = RptDoc;
                                this.CrystalReportViewer1.RefreshReport();
                                this.CrystalReportViewer1.DataBind();
                                CrystalReportViewer1.Visible = true;
                            }
                            else
                            {
                                RptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, RptDoc.FileName);
                            }

                            break;
                        }
                    case "ComprehensiveSummaryReport_Student":
                        {
                            int iSchoolAutoId = int.Parse(Request.QueryString["SchoolAutoId"].ToString());

                            DateTime fromDate = DateTime.Parse(Request.QueryString["FromDate"].ToString());
                            DateTime toDate = DateTime.Parse(Request.QueryString["ToDate"].ToString());

                            string strReportType = Request.QueryString["ReportType"].ToString();

                            CrystalDecisions.CrystalReports.Engine.ReportDocument RptDoc;
                            CrystalReportSource1.Report.FileName = @"~\Reports\rptOpticianReport_Teacher.rpt";

                            RptDoc = CrystalReportSource1.ReportDocument;

                            DataTable dt = dx.sp_ReportforOptincian_Teacher(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();

                            DataSet ds = new DataSet();
                            ds.Tables.Add(dt);

                            RptDoc.SetDataSource(getReportforOptician_Teacher(ds));

                            if (strReportType == "0")
                            {
                                this.CrystalReportViewer1.ReportSource = RptDoc;
                                this.CrystalReportViewer1.RefreshReport();
                                this.CrystalReportViewer1.DataBind();
                                CrystalReportViewer1.Visible = true;
                            }
                            else
                            {
                                RptDoc.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, HttpContext.Current.Response, false, RptDoc.FileName);
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

        #region Eye Glass Prescription
        //include Eye Glass Prescription
        public EyeGlassPrescriptionProperties.ProcedureParametersCollection getEyeGlassPrescription_Student(DataSet ds)
        {
            EyeGlassPrescriptionProperties.ProcedureParametersCollection apps = new EyeGlassPrescriptionProperties.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EyeGlassPrescriptionProperties.ProcedureParameters app = new EyeGlassPrescriptionProperties.ProcedureParameters();
                    
                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.ClassAutoId = int.Parse(dr["ClassAutoId"].ToString());
                    app.SectionAutoId = int.Parse(dr["SectionAutoId"].ToString());
                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.StudentCode = dr["StudentCode"].ToString();
                    app.StudentName = dr["StudentName"].ToString();
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.ClassCode = dr["ClassCode"].ToString();
                    app.ClassSection = dr["ClassSection"].ToString();
                    app.Age = int.Parse(dr["Age"].ToString());
                    app.OptometristStudentTransDate = dr["OptometristStudentTransDate"].ToString();
                    app.OptometristName = dr["OptometristName"].ToString();
                    app.Spherical_Right_Eye = dr["Spherical_Right_Eye"].ToString();
                    app.Cyclinderical_Right_Eye = dr["Cyclinderical_Right_Eye"].ToString();
                    app.Axix_Right_Eye = dr["Axix_Right_Eye"].ToString();
                    app.NearAdd_Right_Eye = dr["NearAdd_Right_Eye"].ToString();
                    app.Spherical_Left_Eye = dr["Spherical_Left_Eye"].ToString();
                    app.Cyclinderical_Left_Eye = dr["Cyclinderical_Left_Eye"].ToString();
                    app.Axix_Left_Eye = dr["Axix_Left_Eye"].ToString();
                    app.NearAdd_Left_Eye = dr["NearAdd_Left_Eye"].ToString();                    

                    apps.Add(app);
                }
                
            }
            else
            {
                EyeGlassPrescriptionProperties.ProcedureParameters app = new EyeGlassPrescriptionProperties.ProcedureParameters();

                app.StudentAutoId = 0;
                app.SchoolAutoId = 0;
                app.ClassAutoId = 0;
                app.SectionAutoId = 0;
                app.StudentAutoId = 0;
                app.StudentCode = "";
                app.StudentName = "";
                app.SchoolName = "";
                app.ClassCode = "";
                app.ClassSection = "";
                app.Age = 0;
                app.OptometristStudentTransDate = "";
                app.OptometristName = "";
                app.Spherical_Right_Eye = "";
                app.Cyclinderical_Right_Eye = "";
                app.Axix_Right_Eye = "";
                app.NearAdd_Right_Eye = "";
                app.Spherical_Left_Eye = "";
                app.Cyclinderical_Left_Eye = "";
                app.Axix_Left_Eye = "";
                app.NearAdd_Left_Eye = "";
            }
            return (EyeGlassPrescriptionProperties.ProcedureParametersCollection)apps;
        }

        public EyeGlassPrescriptionProperties_Teacher.ProcedureParametersCollection getEyeGlassPrescription_Teacher(DataSet ds)
        {
            EyeGlassPrescriptionProperties_Teacher.ProcedureParametersCollection apps = new EyeGlassPrescriptionProperties_Teacher.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EyeGlassPrescriptionProperties_Teacher.ProcedureParameters app = new EyeGlassPrescriptionProperties_Teacher.ProcedureParameters();

                    app.TeacherAutoId = int.Parse(dr["TeacherAutoId"].ToString());
                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.TeacherCode = dr["TeacherCode"].ToString();
                    app.TeacherName = dr["TeacherName"].ToString();
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.Age = int.Parse(dr["Age"].ToString());
                    app.OptometristTeacherTransDate = dr["OptometristTeacherTransDate"].ToString();
                    app.OptometristName = dr["OptometristName"].ToString();
                    app.Spherical_Right_Eye = dr["Spherical_Right_Eye"].ToString();
                    app.Cyclinderical_Right_Eye = dr["Cyclinderical_Right_Eye"].ToString();
                    app.Axix_Right_Eye = dr["Axix_Right_Eye"].ToString();
                    app.NearAdd_Right_Eye = dr["NearAdd_Right_Eye"].ToString();
                    app.Spherical_Left_Eye = dr["Spherical_Left_Eye"].ToString();
                    app.Cyclinderical_Left_Eye = dr["Cyclinderical_Left_Eye"].ToString();
                    app.Axix_Left_Eye = dr["Axix_Left_Eye"].ToString();
                    app.NearAdd_Left_Eye = dr["NearAdd_Left_Eye"].ToString();

                    apps.Add(app);
                }

            }
            else
            {
                EyeGlassPrescriptionProperties_Teacher.ProcedureParameters app = new EyeGlassPrescriptionProperties_Teacher.ProcedureParameters();

                app.TeacherAutoId = 0;
                app.SchoolAutoId = 0;
                app.TeacherCode = "";
                app.TeacherName = "";
                app.SchoolName = "";
                app.Age = 0;
                app.OptometristTeacherTransDate = "";
                app.OptometristName = "";
                app.Spherical_Right_Eye = "";
                app.Cyclinderical_Right_Eye = "";
                app.Axix_Right_Eye = "";
                app.NearAdd_Right_Eye = "";
                app.Spherical_Left_Eye = "";
                app.Cyclinderical_Left_Eye = "";
                app.Axix_Left_Eye = "";
                app.NearAdd_Left_Eye = "";
            }
            return (EyeGlassPrescriptionProperties_Teacher.ProcedureParametersCollection)apps;
        }

        #endregion Eye Glass Prescription

        #region Daily Report (School)

        public DailyReportProperties.ProcedureParametersCollection getDailyReport_School(DataSet ds)
        {
            DailyReportProperties.ProcedureParametersCollection apps = new DailyReportProperties.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DailyReportProperties.ProcedureParameters app = new DailyReportProperties.ProcedureParameters();

                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.TransactionDate = DateTime.Parse(dr["TransactionDate"].ToString());

                    app.Enrolled_Student = int.Parse(dr["StudentsEnrolled"].ToString());
                    app.AutoRef_Student = int.Parse(dr["StudentsAutoRef"].ToString());
                    app.Optometrist_Student = int.Parse(dr["StudentsOptometrist"].ToString());
                    app.suggestedGlasses_Student = int.Parse(dr["StudentssuggestedGlasses"].ToString());
                    app.forCycloplagicRefraction_Student = int.Parse(dr["StudentsforCycloplagicRefraction"].ToString());
                    app.withotherissues_Student = int.Parse(dr["Studentswithotherissues"].ToString());
                    app.Surgery_Student = int.Parse(dr["StudentforSurgery"].ToString());

                    app.Enrolled_Teacher = int.Parse(dr["TeachersEnrolled"].ToString());
                    app.AutoRef_Teacher = int.Parse(dr["TeachersAutoRef"].ToString());
                    app.Optometrist_Teacher = int.Parse(dr["TeachersOptometrist"].ToString());
                    app.suggestedGlasses_Teacher = int.Parse(dr["TeacherssuggestedGlasses"].ToString());
                    app.forCycloplagicRefraction_Teacher = int.Parse(dr["TeachersforCycloplagicRefraction"].ToString());
                    app.withotherissues_Teacher = int.Parse(dr["Teacherswithotherissues"].ToString());
                    app.Surgery_Teacher = int.Parse(dr["TeacherforSurgery"].ToString());

                    apps.Add(app);
                }

            }
            else
            {
                DailyReportProperties.ProcedureParameters app = new DailyReportProperties.ProcedureParameters();

                app.SchoolAutoId = 0;
                app.SchoolName = "";

                app.Enrolled_Student = 0;
                app.AutoRef_Student = 0;
                app.Optometrist_Student = 0;
                app.suggestedGlasses_Student = 0;
                app.forCycloplagicRefraction_Student = 0;
                app.withotherissues_Student = 0;
                app.Surgery_Student = 0;

                app.Enrolled_Teacher = 0;
                app.AutoRef_Teacher = 0;
                app.Optometrist_Teacher = 0;
                app.suggestedGlasses_Teacher = 0;
                app.forCycloplagicRefraction_Teacher = 0;
                app.withotherissues_Teacher = 0;
                app.Surgery_Teacher = 0;
            }
            return (DailyReportProperties.ProcedureParametersCollection)apps;
        }

        #endregion Daily Report (School)

        #region Report for Optician
        //include Report for Optician
        public ReportforOptician_StudentProperties.ProcedureParametersCollection getReportforOptician_Student(DataSet ds)
        {
            ReportforOptician_StudentProperties.ProcedureParametersCollection apps = new ReportforOptician_StudentProperties.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportforOptician_StudentProperties.ProcedureParameters app = new ReportforOptician_StudentProperties.ProcedureParameters();

                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.ClassAutoId = int.Parse(dr["ClassAutoId"].ToString());
                    app.SectionAutoId = int.Parse(dr["SectionAutoId"].ToString());
                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.StudentCode = dr["StudentCode"].ToString();
                    app.StudentName = dr["StudentName"].ToString();
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.ClassCode = dr["ClassCode"].ToString();
                    app.ClassSection = dr["ClassSection"].ToString();
                    app.Age = int.Parse(dr["Age"].ToString());
                    app.OptometristStudentTransDate = dr["OptometristStudentTransDate"].ToString();
                    app.OptometristName = ""; // dr["OptometristName"].ToString();
                    app.Spherical_Right_Eye = dr["Spherical_Right_Eye"].ToString();
                    app.Cyclinderical_Right_Eye = dr["Cyclinderical_Right_Eye"].ToString();
                    app.Axix_Right_Eye = dr["Axix_Right_Eye"].ToString();
                    app.NearAdd_Right_Eye = dr["NearAdd_Right_Eye"].ToString();
                    app.Spherical_Left_Eye = dr["Spherical_Left_Eye"].ToString();
                    app.Cyclinderical_Left_Eye = dr["Cyclinderical_Left_Eye"].ToString();
                    app.Axix_Left_Eye = dr["Axix_Left_Eye"].ToString();
                    app.NearAdd_Left_Eye = dr["NearAdd_Left_Eye"].ToString();

                    apps.Add(app);
                }

            }
            else
            {
                ReportforOptician_StudentProperties.ProcedureParameters app = new ReportforOptician_StudentProperties.ProcedureParameters();

                app.StudentAutoId = 0;
                app.SchoolAutoId = 0;
                app.ClassAutoId = 0;
                app.SectionAutoId = 0;
                app.StudentAutoId = 0;
                app.StudentCode = "";
                app.StudentName = "";
                app.SchoolName = "";
                app.ClassCode = "";
                app.ClassSection = "";
                app.Age = 0;
                app.OptometristStudentTransDate = "";
                app.OptometristName = "";
                app.Spherical_Right_Eye = "";
                app.Cyclinderical_Right_Eye = "";
                app.Axix_Right_Eye = "";
                app.NearAdd_Right_Eye = "";
                app.Spherical_Left_Eye = "";
                app.Cyclinderical_Left_Eye = "";
                app.Axix_Left_Eye = "";
                app.NearAdd_Left_Eye = "";
            }
            return (ReportforOptician_StudentProperties.ProcedureParametersCollection)apps;
        }

        public ReportforOptician_TeacherProperties.ProcedureParametersCollection getReportforOptician_Teacher(DataSet ds)
        {
            ReportforOptician_TeacherProperties.ProcedureParametersCollection apps = new ReportforOptician_TeacherProperties.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportforOptician_TeacherProperties.ProcedureParameters app = new ReportforOptician_TeacherProperties.ProcedureParameters();

                    app.TeacherAutoId = int.Parse(dr["TeacherAutoId"].ToString());
                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.TeacherCode = dr["TeacherCode"].ToString();
                    app.TeacherName = dr["TeacherName"].ToString();
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.Age = int.Parse(dr["Age"].ToString());
                    app.OptometristTeacherTransDate = dr["OptometristTeacherTransDate"].ToString();
                    app.OptometristName = ""; // dr["OptometristName"].ToString();
                    app.Spherical_Right_Eye = dr["Spherical_Right_Eye"].ToString();
                    app.Cyclinderical_Right_Eye = dr["Cyclinderical_Right_Eye"].ToString();
                    app.Axix_Right_Eye = dr["Axix_Right_Eye"].ToString();
                    app.NearAdd_Right_Eye = dr["NearAdd_Right_Eye"].ToString();
                    app.Spherical_Left_Eye = dr["Spherical_Left_Eye"].ToString();
                    app.Cyclinderical_Left_Eye = dr["Cyclinderical_Left_Eye"].ToString();
                    app.Axix_Left_Eye = dr["Axix_Left_Eye"].ToString();
                    app.NearAdd_Left_Eye = dr["NearAdd_Left_Eye"].ToString();

                    apps.Add(app);
                }

            }
            else
            {
                ReportforOptician_TeacherProperties.ProcedureParameters app = new ReportforOptician_TeacherProperties.ProcedureParameters();

                app.TeacherAutoId = 0;
                app.SchoolAutoId = 0;
                app.TeacherCode = "";
                app.TeacherName = "";
                app.SchoolName = "";
                app.Age = 0;
                app.OptometristTeacherTransDate = "";
                app.OptometristName = "";
                app.Spherical_Right_Eye = "";
                app.Cyclinderical_Right_Eye = "";
                app.Axix_Right_Eye = "";
                app.NearAdd_Right_Eye = "";
                app.Spherical_Left_Eye = "";
                app.Cyclinderical_Left_Eye = "";
                app.Axix_Left_Eye = "";
                app.NearAdd_Left_Eye = "";
            }
            return (ReportforOptician_TeacherProperties.ProcedureParametersCollection)apps;
        }

        #endregion Report for Optician

        #region Comprehensive Summary Report
        //include Comprehensive Summary Report
        public ComprehensiveSummaryReport_StudentProperties.ProcedureParametersCollection getReportComprehensiveSummaryReport_Student(DataSet ds)
        {
            ComprehensiveSummaryReport_StudentProperties.ProcedureParametersCollection apps = new ComprehensiveSummaryReport_StudentProperties.ProcedureParametersCollection();

            if (ds != null)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ComprehensiveSummaryReport_StudentProperties.ProcedureParameters app = new ComprehensiveSummaryReport_StudentProperties.ProcedureParameters();

                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.SchoolAutoId = int.Parse(dr["SchoolAutoId"].ToString());
                    app.ClassAutoId = int.Parse(dr["ClassAutoId"].ToString());
                    app.SectionAutoId = int.Parse(dr["SectionAutoId"].ToString());
                    app.StudentAutoId = int.Parse(dr["StudentAutoId"].ToString());
                    app.StudentCode = dr["StudentCode"].ToString();
                    app.StudentName = dr["StudentName"].ToString();
                    app.SchoolName = dr["SchoolName"].ToString();
                    app.ClassCode = dr["ClassCode"].ToString();
                    app.ClassSection = dr["ClassSection"].ToString();
                    app.Age = int.Parse(dr["Age"].ToString());
                    app.Normal = int.Parse(dr["Normal"].ToString());
                    app.WearingGlasses = int.Parse(dr["WearingGlasses"].ToString());
                    app.Refractive_Error = int.Parse(dr["Refractive_Error"].ToString());
                    app.Needs_cyclopegic_refration = int.Parse(dr["Needs_cyclopegic_refration"].ToString());
                    app.Squint_Strabismus = int.Parse(dr["Squint_Strabismus"].ToString());
                    app.LazyEye_Amblyopia = int.Parse(dr["LazyEye_Amblyopia"].ToString());
                    app.Colorblindness_Achromatopsia = int.Parse(dr["Colorblindness_Achromatopsia"].ToString());
                    app.Cataract = int.Parse(dr["Cataract"].ToString());
                    app.Traumatic_Cataract = int.Parse(dr["Traumatic_Cataract"].ToString());
                    app.Keratoconus = int.Parse(dr["Keratoconus"].ToString());
                    app.Anisometropia = int.Parse(dr["Anisometropia"].ToString());
                    app.Ptosis = int.Parse(dr["Ptosis"].ToString());
                    app.Nystagmus = int.Parse(dr["Nystagmus"].ToString());
                    app.Presbyopia = int.Parse(dr["Presbyopia"].ToString());
                    app.Other = int.Parse(dr["Other"].ToString());
                    
                    apps.Add(app);
                }

            }
            else
            {
                ComprehensiveSummaryReport_StudentProperties.ProcedureParameters app = new ComprehensiveSummaryReport_StudentProperties.ProcedureParameters();

                app.StudentAutoId = 0;
                app.SchoolAutoId = 0;
                app.ClassAutoId = 0;
                app.SectionAutoId = 0;
                app.StudentAutoId = 0;
                app.StudentCode = "";
                app.StudentName = "";
                app.SchoolName = "";
                app.ClassCode = "";
                app.ClassSection = "";
                app.Age = 0;
                app.Normal = 0;
                app.WearingGlasses = 0;
                app.Refractive_Error = 0;
                app.Needs_cyclopegic_refration = 0;
                app.Squint_Strabismus = 0;
                app.LazyEye_Amblyopia = 0;
                app.Colorblindness_Achromatopsia = 0;
                app.Cataract = 0;
                app.Traumatic_Cataract = 0;
                app.Keratoconus = 0;
                app.Anisometropia = 0;
                app.Ptosis = 0;
                app.Nystagmus = 0;
                app.Presbyopia = 0;
                app.Other = 0;

            }
            return (ComprehensiveSummaryReport_StudentProperties.ProcedureParametersCollection)apps;
        }

        #endregion Comprehensive Summary Report


    }
}