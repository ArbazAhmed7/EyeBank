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
    public partial class rptReligionWiseReport : System.Web.UI.Page
    {
        secoffEntities dx = new secoffEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Utilities.CanView(Utilities.GetLoginUserID(), "ReligionWiseReport"))
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                BindCombos();

                rdoReligionType_SelectedIndexChanged(null, null);

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
            chkCity.ClearSelection();
            chkDistrict.ClearSelection();
            chkTown.ClearSelection();
            chkSchoolLevel.ClearSelection();

            hfSchoolIDPKID.Value = "0";
            txtSchoolCode.Text = "";
            txtSchoolName.Text = "";

            txtTestDateFrom.Text = "";
            txtTestDateTo.Text = "";

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

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInput())
                {
                    if (rdoReligionType.SelectedValue == "1")
                    {
                        string sCity = string.Empty;

                        if (chkCity.Items[0].Selected == true)
                        {
                            sCity = "0";
                        }
                        else
                        {
                            for (int i = 0; i < chkCity.Items.Count; i++)
                            {
                                if (chkCity.Items[i].Selected == true)// getting selected value from CheckBox List  
                                {
                                    sCity += chkCity.Items[i].Text + ","; // add selected Item text to the String .  
                                }
                            }
                            sCity = sCity.TrimEnd(',');
                            if (sCity.Trim() == "")
                            {
                                lbl_error.Text = "Please select 'City'.";
                                return;
                            }
                        }

                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;

                        if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                        else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                        if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                        else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReligionReport_City&City=" + sCity + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);

                    }
                    else if (rdoReligionType.SelectedValue == "2")
                    {
                        string sDistrict = string.Empty;

                        if (chkDistrict.Items[0].Selected == true)
                        {
                            sDistrict = "0";
                        }
                        else
                        {
                            for (int i = 0; i < chkDistrict.Items.Count; i++)
                            {
                                if (chkDistrict.Items[i].Selected == true)// getting selected value from CheckBox List  
                                {
                                    sDistrict += chkDistrict.Items[i].Text + ","; // add selected Item text to the String .  
                                }
                            }
                            sDistrict = sDistrict.TrimEnd(',');
                            if (sDistrict.Trim() == "")
                            {
                                lbl_error.Text = "Please select 'District'.";
                                return;
                            }
                        }

                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;

                        if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                        else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                        if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                        else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReligionReport_District&District=" + sDistrict + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);

                    }
                    else if (rdoReligionType.SelectedValue == "3")
                    {
                        string sTown = string.Empty;

                        if (chkTown.Items[0].Selected == true)
                        {
                            sTown = "0";
                        }
                        else
                        {
                            for (int i = 0; i < chkTown.Items.Count; i++)
                            {
                                if (chkTown.Items[i].Selected == true)// getting selected value from CheckBox List  
                                {
                                    sTown += chkTown.Items[i].Text + ","; // add selected Item text to the String .  
                                }
                            }
                            sTown = sTown.TrimEnd(',');
                            if (sTown.Trim() == "")
                            {
                                lbl_error.Text = "Please select 'Town'.";
                                return;
                            }
                        }

                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;

                        if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                        else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                        if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                        else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReligionReport_Town&Town=" + sTown + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);

                    }
                    else if (rdoReligionType.SelectedValue == "4")
                    {
                        string sSchoolLevel = string.Empty;

                        if (chkSchoolLevel.Items[0].Selected == true)
                        {
                            sSchoolLevel = "0";
                        }
                        else if (chkSchoolLevel.Items[1].Selected == true && chkSchoolLevel.Items[2].Selected == true)
                        {
                            sSchoolLevel = "0";
                        }
                        else
                        {
                            for (int i = 0; i < chkSchoolLevel.Items.Count; i++)
                            {
                                if (chkSchoolLevel.Items[i].Selected == true)// getting selected value from CheckBox List  
                                {
                                    sSchoolLevel += chkSchoolLevel.Items[i].Text + ","; // add selected Item text to the String .  
                                }
                            }
                            sSchoolLevel = sSchoolLevel.TrimEnd(',');
                            if (sSchoolLevel.Trim() == "")
                            {
                                lbl_error.Text = "Please select 'SchoolLevel'.";
                                return;
                            }
                        }

                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;

                        if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                        else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                        if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                        else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReligionReport_SchoolLevel&SchoolLevel=" + sSchoolLevel + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "popup", jsReport, false);

                    }
                    else if (rdoReligionType.SelectedValue == "5")
                    {
                        string sSchool = hfSchoolIDPKID.Value;

                        string strFromDate = string.Empty;
                        string strToDate = string.Empty;

                        if (txtTestDateFrom.Text == "") { strFromDate = "2022-07-01"; }
                        else { strFromDate = DateTime.Parse(txtTestDateFrom.Text).ToString("yyyy-MM-dd"); }

                        if (txtTestDateTo.Text == "") { strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); }
                        else { strToDate = DateTime.Parse(txtTestDateTo.Text).ToString("yyyy-MM-dd"); }

                        string jsReport = "<script type='text/javascript'>var newHeight = screen.height - 80;window.open('ReportViewerRDLC.aspx?vReportName=ReligionReport_School&School=" + sSchool + "&FromDate=" + strFromDate + "&ToDate=" + strToDate + "&ReportType=" + rdoReportType.SelectedValue + "','','height='+newHeight+',width=1600,menubar=no,status=yes,location=no,top=1,left=1,scrollbars=yes,resizable=yes')</script>";
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

            if (rdoReligionType.SelectedValue == "1")
            {
                string sCity = string.Empty;

                if (chkCity.Items[0].Selected == true)
                {
                    sCity = "0";
                }
                else
                {
                    for (int i = 0; i < chkCity.Items.Count; i++)
                    {
                        if (chkCity.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            sCity += chkCity.Items[i].Text + ","; // add selected Item text to the String .  
                        }
                    }
                    sCity = sCity.TrimEnd(',');
                    if (sCity.Trim() == "")
                    {
                        lbl_error.Text = "Please select 'City'.";
                        return;
                    }
                }

                DataTable dt = dx.sp_GetCityWiseReligionDetail(sCity, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "1");
            }
            else if (rdoReligionType.SelectedValue == "2")
            {
                string sDistrict = string.Empty;

                if (chkDistrict.Items[0].Selected == true)
                {
                    sDistrict = "0";
                }
                else
                {
                    for (int i = 0; i < chkDistrict.Items.Count; i++)
                    {
                        if (chkDistrict.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            sDistrict += chkDistrict.Items[i].Text + ","; // add selected Item text to the String .  
                        }
                    }
                    sDistrict = sDistrict.TrimEnd(',');
                    if (sDistrict.Trim() == "")
                    {
                        lbl_error.Text = "Please select 'District'.";
                        return;
                    }
                }

                DataTable dt = dx.sp_GetDistrictWiseReligionDetail(sDistrict, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "2");
            }
            else if (rdoReligionType.SelectedValue == "3")
            {
                string sTown = string.Empty;

                if (chkTown.Items[0].Selected == true)
                {
                    sTown = "0";
                }
                else
                {
                    for (int i = 0; i < chkTown.Items.Count; i++)
                    {
                        if (chkTown.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            sTown += chkTown.Items[i].Text + ","; // add selected Item text to the String .  
                        }
                    }
                    sTown = sTown.TrimEnd(',');
                    if (sTown.Trim() == "")
                    {
                        lbl_error.Text = "Please select 'Town'.";
                        return;
                    }
                }

                DataTable dt = dx.sp_GetTownWiseReligionDetail(sTown, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "3");
            }
            else if (rdoReligionType.SelectedValue == "4")
            {
                string sSchoolLevel = string.Empty;

                if (chkSchoolLevel.Items[0].Selected == true)
                {
                    sSchoolLevel = "0";
                }
                else
                {
                    for (int i = 0; i < chkSchoolLevel.Items.Count; i++)
                    {
                        if (chkSchoolLevel.Items[i].Selected == true)// getting selected value from CheckBox List  
                        {
                            sSchoolLevel += chkSchoolLevel.Items[i].Text + ","; // add selected Item text to the String .  
                        }
                    }
                    sSchoolLevel = sSchoolLevel.TrimEnd(',');
                    if (sSchoolLevel.Trim() == "")
                    {
                        lbl_error.Text = "Please select 'School Level'.";
                        return;
                    }
                }

                DataTable dt = dx.sp_GetSchoolLevelWiseReligionDetail(sSchoolLevel, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "4");
            }
            else if (rdoReligionType.SelectedValue == "5")
            {
                int iSchoolAutoId = int.Parse(hfSchoolIDPKID.Value);

                DataTable dt = dx.sp_GetSchoolWiseReligionDetail(iSchoolAutoId, fromDate, toDate).ToList().ToDataTable();
                PrintReport(dt, "5");
            }



        }

        private void PrintReport(DataTable ReportData, string strForm)
        {

            try
            {
                #region DataArea
                if (rdoReligionType.SelectedValue == "1")
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

                                workSheet = excel.Workbook.Worksheets.Add("CityWiseReligionReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Report for Religion (City Wise)");
                                Utilities.SetTimeStamp(workSheet, "Report for Religion (City Wise)");

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
                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "District";
                                    workSheet.Cells[FirstRow, 3].Value = "Town";
                                    workSheet.Cells[FirstRow, 4].Value = "Registered Students";
                                    workSheet.Cells[FirstRow, 5].Value = "Student Screened";
                                    workSheet.Cells[FirstRow, 6].Value = "Muslim Student";
                                    workSheet.Cells[FirstRow, 7].Value = "Non-Muslim Student";
                                    workSheet.Cells[FirstRow, 8].Value = "Registered Teachers";
                                    workSheet.Cells[FirstRow, 9].Value = "Teacher Screened";
                                    workSheet.Cells[FirstRow, 10].Value = "Muslim Teacher";
                                    workSheet.Cells[FirstRow, 11].Value = "Non-Muslim Teacher";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (FirstColumn == 1 || FirstColumn == 2 || FirstColumn == 3)
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
                                }

                                //Utilities.SetColumnsWidth(workSheet);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=CityWiseReligionReport.xlsx");
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
                else if (rdoReligionType.SelectedValue == "2")
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

                                workSheet = excel.Workbook.Worksheets.Add("DistrictWiseReligionReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Report for Religion (District Wise)");
                                Utilities.SetTimeStamp(workSheet, "Report for Religion (District Wise)");

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
                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "District";
                                    workSheet.Cells[FirstRow, 3].Value = "Registered Students";
                                    workSheet.Cells[FirstRow, 4].Value = "Student Screened";
                                    workSheet.Cells[FirstRow, 5].Value = "Muslim Student";
                                    workSheet.Cells[FirstRow, 6].Value = "Non-Muslim Student";
                                    workSheet.Cells[FirstRow, 7].Value = "Registered Teachers";
                                    workSheet.Cells[FirstRow, 8].Value = "Teacher Screened";
                                    workSheet.Cells[FirstRow, 9].Value = "Muslim Teacher";
                                    workSheet.Cells[FirstRow, 10].Value = "Non-Muslim Teacher";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

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
                                }

                                //Utilities.SetColumnsWidth(workSheet);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=DistrictWiseReligionReport.xlsx");
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
                else if (rdoReligionType.SelectedValue == "3")
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

                                workSheet = excel.Workbook.Worksheets.Add("TownWiseReligionReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Report for Religion (Town Wise)");
                                Utilities.SetTimeStamp(workSheet, "Report for Religion (Town Wise)");

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
                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "Town";
                                    workSheet.Cells[FirstRow, 3].Value = "Registered Students";
                                    workSheet.Cells[FirstRow, 4].Value = "Student Screened";
                                    workSheet.Cells[FirstRow, 5].Value = "Muslim Student";
                                    workSheet.Cells[FirstRow, 6].Value = "Non-Muslim Student";
                                    workSheet.Cells[FirstRow, 7].Value = "Registered Teachers";
                                    workSheet.Cells[FirstRow, 8].Value = "Teacher Screened";
                                    workSheet.Cells[FirstRow, 9].Value = "Muslim Teacher";
                                    workSheet.Cells[FirstRow, 10].Value = "Non-Muslim Teacher";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

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
                                }

                                //Utilities.SetColumnsWidth(workSheet);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=TownWiseReligionReport.xlsx");
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
                else if (rdoReligionType.SelectedValue == "4")
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

                                workSheet = excel.Workbook.Worksheets.Add("SchoolLevelWiseReligionReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Report for Religion (School Level Wise)");
                                Utilities.SetTimeStamp(workSheet, "Report for Religion (School Level Wise)");

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
                                    workSheet.Cells[FirstRow, 1].Value = "School Name";
                                    workSheet.Cells[FirstRow, 2].Value = "School Level";
                                    workSheet.Cells[FirstRow, 3].Value = "Registered Students";
                                    workSheet.Cells[FirstRow, 4].Value = "Student Screened";
                                    workSheet.Cells[FirstRow, 5].Value = "Muslim Student";
                                    workSheet.Cells[FirstRow, 6].Value = "Non-Muslim Student";
                                    workSheet.Cells[FirstRow, 7].Value = "Registered Teachers";
                                    workSheet.Cells[FirstRow, 8].Value = "Teacher Screened";
                                    workSheet.Cells[FirstRow, 9].Value = "Muslim Teacher";
                                    workSheet.Cells[FirstRow, 10].Value = "Non-Muslim Teacher";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

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
                                }

                                //Utilities.SetColumnsWidth(workSheet);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=SchoolLevelWiseReligionReport.xlsx");
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
                else if (rdoReligionType.SelectedValue == "5")
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

                                workSheet = excel.Workbook.Worksheets.Add("SchoolWiseReligionReport");


                                int iPrint_Record = 0;
                                int iPrint_Column = 1;

                                #region Printing Header

                                Utilities.SetReportHeading(workSheet, "Report for Religion (School Wise)");
                                Utilities.SetTimeStamp(workSheet, "Report for Religion (School Wise)");

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
                                    workSheet.Cells[FirstRow, 1].Value = "SchoolName";
                                    workSheet.Cells[FirstRow, 2].Value = "StudentCode";
                                    workSheet.Cells[FirstRow, 3].Value = "StudentName";
                                    workSheet.Cells[FirstRow, 4].Value = "FatherName";
                                    workSheet.Cells[FirstRow, 5].Value = "Age";
                                    workSheet.Cells[FirstRow, 6].Value = "Gender";
                                    workSheet.Cells[FirstRow, 7].Value = "Religion";


                                    FirstRow++;
                                    FirstColumn++;
                                    iPrint_Column++;

                                    foreach (DataRow dr in dtMainReport_FilteredTable.Rows)
                                    {

                                        FirstColumn = 1;
                                        LastColumn = dtMainReport_FilteredTable.Rows.Count;

                                        foreach (DataColumn dc in dtMainReport_FilteredTable.Columns)
                                        {
                                            if (!(FirstColumn == 5))
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
                                }

                                //Utilities.SetColumnsWidth(workSheet);

                                Response.Clear();
                                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                Response.AddHeader("content-disposition", "attachment;  filename=SchoolWiseReligionReport.xlsx");
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


                #endregion DataArea
            }
            catch (Exception ex)
            {
                //
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/rptReligionWiseReport.aspx");
        }

        private int Filters(ExcelWorksheet ws, int FirstRow, int FirstColumn, string AllorNa)
        {
            //string strFromDate = string.Empty;
            //string strToDate = string.Empty;

            //if (txtTestDateFrom.Text == "")
            //{
            //    strFromDate = "01-Jul-2022";
            //    strToDate = DateTime.Now.ToString("dd-MMM-yyyy"); ;
            //}
            //else
            //{
            //    strFromDate = DateTime.Parse(txtTestDateFrom.Text.Trim()).ToString("dd-MMM-yyyy");
            //    strToDate = DateTime.Parse(txtTestDateTo.Text.Trim()).ToString("dd-MMM-yyyy");
            //}

            //ws.Cells[FirstRow, FirstColumn, FirstRow, FirstColumn].Value = "Date Range:";
            //ws.Cells[FirstRow, FirstColumn + 1, FirstRow, FirstColumn + 1].Value = strFromDate + " to " + strToDate;
            //FirstRow++;

            return FirstRow;
        }

        private void BindCombos()
        {

            try
            {
                var dtCity = dx.GetCity().ToList();
                if (dtCity.Count != 0)
                {
                    chkCity.DataSource = dtCity;
                    chkCity.DataValueField = "Id";
                    chkCity.DataTextField = "City";
                    chkCity.DataBind();
                }

                var dtDistrict = dx.GetDistrict().ToList();
                if (dtDistrict.Count != 0)
                {
                    chkDistrict.DataSource = dtDistrict;
                    chkDistrict.DataValueField = "Id";
                    chkDistrict.DataTextField = "District";
                    chkDistrict.DataBind();
                }

                var dtTown = dx.GetTown().ToList();
                if (dtTown.Count != 0)
                {
                    chkTown.DataSource = dtTown;
                    chkTown.DataValueField = "Id";
                    chkTown.DataTextField = "Town";
                    chkTown.DataBind();
                }

                var dtSchoolLevel = dx.GetSchoolLevel().ToList();
                if (dtSchoolLevel.Count != 0)
                {
                    chkSchoolLevel.DataSource = dtSchoolLevel;
                    chkSchoolLevel.DataValueField = "Id";
                    chkSchoolLevel.DataTextField = "SchoolLevel";
                    chkSchoolLevel.DataBind();
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message + " - " + ex.Source;
            }
        }

        protected void rdoReligionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCity.Visible = false;
            pnlDistrict.Visible = false;
            pnlTown.Visible = false;
            pnlSchoolLevel.Visible = false;
            pnlSchool.Visible = false;

            ClearForm();

            if (rdoReligionType.SelectedValue == "1")
            {
                pnlCity.Visible = true;
            }
            else if (rdoReligionType.SelectedValue == "2")
            {
                pnlDistrict.Visible = true;
            }
            else if (rdoReligionType.SelectedValue == "3")
            {
                pnlTown.Visible = true;
            }
            else if (rdoReligionType.SelectedValue == "4")
            {
                pnlSchoolLevel.Visible = true;
            }
            else if (rdoReligionType.SelectedValue == "5")
            {
                pnlSchool.Visible = true;
            }
        }
    }
}