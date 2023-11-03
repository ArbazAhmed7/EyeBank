using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransportManagementCore.Areas.Reporting.Repositories;

namespace TransportManagementCore.Areas.Reporting.Controller
{
    [Area("Reporting")]
    [Route("Reporting/Test")]
    public class ReportingController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        ReportingRepo repo;
        public ReportingController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
              repo = new ReportingRepo();

        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("Print/{Module}/{ReportName}")]
        public async Task<IActionResult> Print(string Module, string ReportName) {
            
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath }\\Reports\\"+ Module+"\\"+ReportName;
            
           List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@EnrollmentDate", null));
            DataTable dt = await repo.DbFunction("Sp_Factory_Report", paraList);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("DataSet1", dt);  
            var result = localReport.Execute(RenderType.Pdf, extension, null, "");
            return File(result.MainStream, "application/pdf");

        }
    }
}
