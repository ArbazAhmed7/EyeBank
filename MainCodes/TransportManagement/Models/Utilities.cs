using Microsoft.VisualBasic;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TransportManagement.Models
{
    public static class Utilities
    {
        //Arbaz Work Start
        public static string GetEncrypt(string EncryptString)
        {
            string Str = EncryptString;
            string Key = "";
            object c = null;
            string cipher = "";
            Key = "5d7er4ty75es4rt4ybv478x6x-w7@*^ds*#dc$0%";
            if (Key.Length >= Str.Length)
            {
                Key = Strings.Mid(Key, 1, Strings.Len(Str));
                for (int i = 1; i <= Key.Length; i++)
                {
                    c = Convert.ToInt32(Strings.Asc(Strings.Mid(Str, i, 1)) ^ Strings.Asc(Strings.Mid(Key, i, 1)));
                    if (Convert.ToInt32(c) < 10)
                    {
                        c = "00" + Convert.ToString(c);
                    }
                    if (Convert.ToInt32(c) > 9 && Convert.ToInt32(c) < 100)
                    {
                        c = "0" + Convert.ToString(c);
                    }
                    cipher = cipher + Convert.ToString(c);
                }
            }
            return cipher;
        }
        //END
        public static string getTerminalId()
        {
            string returnVal = string.Empty;

            try
            {
                System.Net.IPHostEntry obj = new System.Net.IPHostEntry();
                obj = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
                returnVal = obj.HostName.ToString();
            }
            catch (System.Net.Sockets.SocketException se)
            {
                returnVal = System.Web.HttpContext.Current.Request.UserHostName;//Dns.GetHostName().ToString();//HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString();
            }

            return returnVal;

        }
        public static string getTerminalIP()
        {
            string returnVal = string.Empty;

            try
            {
                returnVal = System.Web.HttpContext.Current.Request.UserHostAddress;//obj.AddressList[0].ToString();
            }
            catch (System.Net.Sockets.SocketException se)
            {
                returnVal = System.Web.HttpContext.Current.Request.UserHostAddress;//HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]; //HttpContext.Current.Request.ServerVariables["REMOTE_HOST"].ToString();
            }

            return returnVal;
        }
        public static string GetLoginUserID()
        {
            string userid = (HttpContext.Current.Session["LoginUserId_TM"] == null ? "" : Convert.ToString(HttpContext.Current.Session["LoginUserId_TM"]));

            return userid;
        }

        public static int GetLoginUserRight()
        {
            int right = (HttpContext.Current.Session["LoginUserRight"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["LoginUserRight"]));

            return right;
        }

        public static string GetDate()
        {
            string dtCurrent = DateTime.Now.AddHours(4).ToString("dd-MMM-yyyy");

            return dtCurrent;
        }

        public static bool CanView(string UserID, string FormID)
        {
            bool res = false;
            List<sp_GetMenuNew_Result> UserMenuData = (List<sp_GetMenuNew_Result>)HttpContext.Current.Session["UserMenuData"];

            if (UserMenuData.Count(o => o.FormId == FormID) > 0)
            {
                res = true;
            }

            return res;
        }


        #region Excel Sheet - Dynamic Functions
        public static string GetFontForExcel()
        {
            return "Arial Narrow";
        }
        public static void GetExcelReportStyle()
        {
            ExcelReportStyle.HeaderPanel.BackgroundColor = Color.FromArgb(211, 211, 211);

            ExcelReportStyle.ReportName.FontColor = Color.Black;
            ExcelReportStyle.ReportName.FontSize = 14;
            ExcelReportStyle.ReportName.FontFamily = GetFontForExcel();
            ExcelReportStyle.ReportName.IsBold = true;
            ExcelReportStyle.ReportName.IsWrap = true;
            ExcelReportStyle.ReportName.VerticalAlignment = ExcelVerticalAlignment.Top;
            ExcelReportStyle.ReportName.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.TimeStampLabel.FontColor = Color.Black;
            ExcelReportStyle.TimeStampLabel.FontSize = 8;
            ExcelReportStyle.TimeStampLabel.FontFamily = GetFontForExcel();
            ExcelReportStyle.TimeStampLabel.IsBold = false;
            ExcelReportStyle.TimeStampLabel.IsWrap = true;
            ExcelReportStyle.TimeStampLabel.VerticalAlignment = ExcelVerticalAlignment.Top;
            ExcelReportStyle.TimeStampLabel.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.TimeStampValue.FontColor = Color.Black;
            ExcelReportStyle.TimeStampValue.FontSize = 8;
            ExcelReportStyle.TimeStampValue.FontFamily = GetFontForExcel();
            ExcelReportStyle.TimeStampValue.IsBold = true;
            ExcelReportStyle.TimeStampValue.IsWrap = true;
            ExcelReportStyle.TimeStampValue.VerticalAlignment = ExcelVerticalAlignment.Top;
            ExcelReportStyle.TimeStampValue.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.ReportSelectionLabel.FontColor = Color.Black;
            ExcelReportStyle.ReportSelectionLabel.FontSize = 8;
            ExcelReportStyle.ReportSelectionLabel.FontFamily = GetFontForExcel();
            ExcelReportStyle.ReportSelectionLabel.IsBold = false;
            ExcelReportStyle.ReportSelectionLabel.IsWrap = true;
            ExcelReportStyle.ReportSelectionLabel.VerticalAlignment = ExcelVerticalAlignment.Top;
            ExcelReportStyle.ReportSelectionLabel.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.ReportSelectionValue.FontColor = Color.Black;
            ExcelReportStyle.ReportSelectionValue.FontSize = 8;
            ExcelReportStyle.ReportSelectionValue.FontFamily = GetFontForExcel();
            ExcelReportStyle.ReportSelectionValue.IsBold = true;
            ExcelReportStyle.ReportSelectionValue.IsWrap = true;
            ExcelReportStyle.ReportSelectionValue.VerticalAlignment = ExcelVerticalAlignment.Top;
            ExcelReportStyle.ReportSelectionValue.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.Header.BackgroundColor = Color.FromArgb(15, 36, 62);
            ExcelReportStyle.Header.FontColor = Color.White;
            ExcelReportStyle.Header.FontSize = 8;
            ExcelReportStyle.Header.FontFamily = GetFontForExcel();
            ExcelReportStyle.Header.IsBold = true;
            ExcelReportStyle.Header.IsWrap = true;
            ExcelReportStyle.Header.IsAutoFilter = true;
            ExcelReportStyle.Header.VerticalAlignment = ExcelVerticalAlignment.Top;

            ExcelReportStyle.SubReport.BackgroundColor = Color.FromArgb(15, 36, 62);
            ExcelReportStyle.SubReport.FontColor = Color.Cyan;
            ExcelReportStyle.SubReport.FontSize = 8;
            ExcelReportStyle.SubReport.FontFamily = GetFontForExcel();
            ExcelReportStyle.SubReport.IsBold = true;
            ExcelReportStyle.SubReport.IsWrap = true;
            ExcelReportStyle.SubReport.IsAutoFilter = true;
            ExcelReportStyle.SubReport.VerticalAlignment = ExcelVerticalAlignment.Top;

            ExcelReportStyle.DataPrinting.BackgroundColor = Color.White;
            ExcelReportStyle.DataPrinting.FontColor = Color.Black;
            ExcelReportStyle.DataPrinting.FontSize = 8;
            ExcelReportStyle.DataPrinting.FontFamily = GetFontForExcel();
            ExcelReportStyle.DataPrinting.IsBold = false;
            ExcelReportStyle.DataPrinting.IsWrap = true;
            ExcelReportStyle.DataPrinting.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.DataPrinting.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.DataPrinting.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.DataPrinting.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.DataPrinting.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.SubReportDataPrinting.FontColor = Color.Black;
            ExcelReportStyle.SubReportDataPrinting.FontSize = 8;
            ExcelReportStyle.SubReportDataPrinting.FontFamily = GetFontForExcel();
            ExcelReportStyle.SubReportDataPrinting.IsBold = false;
            ExcelReportStyle.SubReportDataPrinting.IsWrap = true;
            ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.SubReportDataPrinting.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.Highlighter.BackgroundColor = Color.Yellow;
            ExcelReportStyle.Highlighter.FontColor = Color.Black;
            ExcelReportStyle.Highlighter.FontSize = 8;
            ExcelReportStyle.Highlighter.FontFamily = GetFontForExcel();
            ExcelReportStyle.Highlighter.IsBold = false;
            ExcelReportStyle.Highlighter.IsWrap = true;
            ExcelReportStyle.Highlighter.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.Highlighter.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.Highlighter.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.Highlighter.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.Highlighter.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.Group1.BackgroundColor = Color.FromArgb(166, 166, 166);
            ExcelReportStyle.Group1.BackgroundColorHide = Color.White;
            ExcelReportStyle.Group1.FontColor = Color.Black;
            ExcelReportStyle.Group1.FontColorHide = Color.Transparent;
            ExcelReportStyle.Group1.FontSize = 8;
            ExcelReportStyle.Group1.FontFamily = GetFontForExcel();
            ExcelReportStyle.Group1.IsBold = true;
            ExcelReportStyle.Group1.IsWrap = true;
            ExcelReportStyle.Group1.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.Group1Total.BackgroundColor = Color.FromArgb(166, 166, 166);
            ExcelReportStyle.Group1Total.FontColor = Color.Black;
            ExcelReportStyle.Group1Total.FontSize = 8;
            ExcelReportStyle.Group1Total.FontFamily = GetFontForExcel();
            ExcelReportStyle.Group1Total.IsBold = true;
            ExcelReportStyle.Group1Total.IsWrap = true;
            ExcelReportStyle.Group1Total.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1Total.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1Total.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1Total.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group1Total.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.Group2.BackgroundColor = Color.FromArgb(211, 211, 211);
            ExcelReportStyle.Group2.BackgroundColorHide = Color.White;
            ExcelReportStyle.Group2.FontColor = Color.Black;
            ExcelReportStyle.Group2.FontColorHide = Color.Transparent;
            ExcelReportStyle.Group2.FontSize = 8;
            ExcelReportStyle.Group2.FontFamily = GetFontForExcel();
            ExcelReportStyle.Group2.IsBold = true;
            ExcelReportStyle.Group2.IsWrap = true;
            ExcelReportStyle.Group2.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.Group2Total.BackgroundColor = Color.FromArgb(211, 211, 211);
            ExcelReportStyle.Group2Total.FontColor = Color.Black;
            ExcelReportStyle.Group2Total.FontSize = 8;
            ExcelReportStyle.Group2Total.FontFamily = GetFontForExcel();
            ExcelReportStyle.Group2Total.IsBold = true;
            ExcelReportStyle.Group2Total.IsWrap = true;
            ExcelReportStyle.Group2Total.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2Total.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2Total.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2Total.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.Group2Total.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.GrandTotal.BackgroundColor = Color.FromArgb(15, 36, 62);
            ExcelReportStyle.GrandTotal.FontColor = Color.White;
            ExcelReportStyle.GrandTotal.FontSize = 8;
            ExcelReportStyle.GrandTotal.FontFamily = GetFontForExcel();
            ExcelReportStyle.GrandTotal.IsBold = true;
            ExcelReportStyle.GrandTotal.IsWrap = true;
            ExcelReportStyle.GrandTotal.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.GrandTotal.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.GrandTotal.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.GrandTotal.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.GrandTotal.VerticalAlignment = ExcelVerticalAlignment.Center;

            ExcelReportStyle.NoData.BackgroundColor = Color.Red;
            ExcelReportStyle.NoData.FontColor = Color.Black;
            ExcelReportStyle.NoData.FontSize = 8;
            ExcelReportStyle.NoData.FontFamily = GetFontForExcel();
            ExcelReportStyle.NoData.IsBold = true;
            ExcelReportStyle.NoData.IsWrap = true;
            ExcelReportStyle.NoData.ExcelBorderStyleBottom = ExcelBorderStyle.Thin;
            ExcelReportStyle.NoData.ExcelBorderStyleTop = ExcelBorderStyle.Thin;
            ExcelReportStyle.NoData.ExcelBorderStyleLeft = ExcelBorderStyle.Thin;
            ExcelReportStyle.NoData.ExcelBorderStyleRight = ExcelBorderStyle.Thin;
            ExcelReportStyle.NoData.VerticalAlignment = ExcelVerticalAlignment.Center;
            ExcelReportStyle.NoData.HorizontalAlignment = ExcelHorizontalAlignment.Left;

            ExcelReportStyle.SubReportHeader.EmployeeInfoBackgroundColor = Color.FromArgb(15, 36, 62);
            ExcelReportStyle.SubReportHeader.EmployeeInfoFontColor = Color.White;
            ExcelReportStyle.SubReportHeader.EmployeeInfoFontSize = 8;
            ExcelReportStyle.SubReportHeader.BackgroundColor = Color.FromArgb(20, 40, 110);
            ExcelReportStyle.SubReportHeader.FontColor = Color.White;
            ExcelReportStyle.SubReportHeader.FontSize = 8;
            ExcelReportStyle.SubReportHeader.FontFamily = GetFontForExcel();
            ExcelReportStyle.SubReportHeader.IsBold = true;
            ExcelReportStyle.SubReportHeader.IsWrap = true;
            ExcelReportStyle.SubReportHeader.IsAutoFilter = true;
            ExcelReportStyle.SubReportHeader.VerticalAlignment = ExcelVerticalAlignment.Top;
        }
        public static void SetReportHeading(ExcelWorksheet ws, string FormId)
        {
            /// This function will set heading of report which takes ExcelWorkSheet object and FormId
            /// Why FormId ? Because FormId fetchs its form name which is going to be displayed on Report Heading. 
            /// If culture is Arabic then it will print Arabic Name otherwise English Name will be printed by function.
            /// Other Styles are also incorporated in this function which are Excel Standards.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            ws.Cells[1, 1].Value = FormId; // GetFormDescriptionByFormId(FormId, false);

            using (ExcelRange rng = ws.Cells[1, 1, 1, 5])
            {
                rng.Merge = true;
                rng.Style.Font.Bold = ExcelReportStyle.ReportName.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.ReportName.FontSize;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.ReportName.FontColor);
                //rng.Style.Font.Name = ExcelReportStyle.ReportName.FontFamily;
                rng.Style.HorizontalAlignment = ExcelReportStyle.ReportName.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.ReportName.VerticalAlignment;
            }
        }
        public static void SetSubReportHeading(ExcelWorksheet ws, string Heading)
        {
            /// This function will set heading of report which takes ExcelWorkSheet object and FormId
            /// Why FormId ? Because FormId fetchs its form name which is going to be displayed on Report Heading. 
            /// If culture is Arabic then it will print Arabic Name otherwise English Name will be printed by function.
            /// Other Styles are also incorporated in this function which are Excel Standards.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            ws.Cells[1, 1].Value = Heading;

            using (ExcelRange rng = ws.Cells[1, 1, 1, 5])
            {
                rng.Merge = true;
                rng.Style.Font.Bold = ExcelReportStyle.ReportName.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.ReportName.FontSize;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.ReportName.FontColor);
                //rng.Style.Font.Name = ExcelReportStyle.ReportName.FontFamily;
                rng.Style.HorizontalAlignment = ExcelReportStyle.ReportName.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.ReportName.VerticalAlignment;
            }
        }
        public static void SetReportSelectionLabel(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will set Report Section Label styling as per standard defined. Kindly pass its respective parameters which results in fruitfull output. 
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.ReportSelectionLabel.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.ReportSelectionLabel.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.ReportSelectionLabel.FontFamily;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.ReportSelectionLabel.FontColor);
                rng.Style.HorizontalAlignment = ExcelReportStyle.ReportSelectionLabel.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.ReportSelectionLabel.VerticalAlignment;
            }
        }
        public static void SetTimeStamp(ExcelWorksheet ws, string FormId)
        {
            /// This function will set Time Stamp of report which takes ExcelWorkSheet object and FormId as parameters.
            /// Why FormId ? Because FormId fetchs it's form Name (Report ID) which is going to be displayed on Report Heading. It will print all Time Stamps which are in standard.
            /// Note: English Description will be printed as Report ID.  
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            ws.Cells[1, 6].Value = "Report Id"; // Utilities.NullHandleObject(GetGlobalResObjText("Global", "lblReportId").ToString());
            ws.Cells[1, 7].Value = FormId; // GetFormDescriptionByFormId(FormId, true);

            ws.Cells[2, 6].Value = "Login User Id";//Utilities.NullHandleObject(GetGlobalResObjText("Global", "lblUserId").ToString());
            ws.Cells[2, 7].Value = Utilities.GetLoginUserID();

            ws.Cells[3, 6].Value = "Terminal Id"; // Utilities.NullHandleObject(GetGlobalResObjText("Global", "lblTerminal").ToString());
            ws.Cells[3, 7].Value = Utilities.getTerminalId().Trim();

            ws.Cells[4, 6].Value = "Print Date"; // Utilities.NullHandleObject(GetGlobalResObjText("Global", "lblPrintDate").ToString());
            ws.Cells[4, 7].Value = DateTime.Now.ToString("dd/MMM/yyyy") + " " + DateTime.Now.ToString("HH:mm");

            using (ExcelRange rng = ws.Cells[1, 6, 4, 6])
            {
                rng.Style.Font.Bold = ExcelReportStyle.TimeStampLabel.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.TimeStampLabel.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.TimeStampLabel.FontFamily;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.TimeStampLabel.FontColor);
                rng.Style.HorizontalAlignment = ExcelReportStyle.TimeStampLabel.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.TimeStampLabel.VerticalAlignment;
            }

            using (ExcelRange rng = ws.Cells[1, 7, 4, 7])
            {
                rng.Style.Font.Bold = ExcelReportStyle.TimeStampValue.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.TimeStampValue.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.TimeStampValue.FontFamily;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.TimeStampValue.FontColor);
                rng.Style.HorizontalAlignment = ExcelReportStyle.TimeStampValue.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.TimeStampValue.VerticalAlignment;
            }
        }
        public static void SetReportSelectionValue(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will set Report Section Value styling as per standard defined. Kindly pass its respective parameters which results in fruitfull output. 
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.ReportSelectionValue.IsBold;
                rng.Style.Font.Size = ExcelReportStyle.ReportSelectionValue.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.ReportSelectionValue.FontFamily;
                rng.Style.Font.Color.SetColor(ExcelReportStyle.ReportSelectionValue.FontColor);
                rng.Style.HorizontalAlignment = ExcelReportStyle.ReportSelectionValue.HorizontalAlignment;
                rng.Style.VerticalAlignment = ExcelReportStyle.ReportSelectionValue.VerticalAlignment;
            }
        }
        public static void SetHeaderPanelBackground(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will set Report Header Background Color.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(Color.LightGray);//(ExcelReportStyle.HeaderPanel.BackgroundColor);
            }
        }
        public static void SetHeader(ExcelWorksheet ws, ref int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will set header as per our standard defined. It takes ExcelWorkSheet object, FromRow, FromColumn, ToRow and ToColumn as parameters.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            FromRow += 2;
            ToRow += 2;

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.Header.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Header.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.Header.FontColor);
                rng.AutoFilter = ExcelReportStyle.Header.IsAutoFilter;
                rng.Style.VerticalAlignment = ExcelReportStyle.Header.VerticalAlignment;
                rng.Style.WrapText = ExcelReportStyle.Header.IsWrap;
                rng.Style.Font.Size = ExcelReportStyle.Header.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Header.FontFamily;
            }
        }
        public static void SetSubReportHeader(ExcelWorksheet ws, ref int EmpInfoFromRow, int EmpInfoFromColumn, int EmpInfoToRow, int EmpInfoToColumn, int SubReportFromRow, int SubReportFromColumn, int SubReportToRow, int SubReportToColumn)
        {
            EmpInfoFromRow += 2;
            EmpInfoToRow += 2;
            SubReportFromRow += 2;
            SubReportToRow += 2;

            using (ExcelRange rng = ws.Cells[EmpInfoFromRow, EmpInfoFromColumn, EmpInfoToRow, EmpInfoToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.Header.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Header.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.Header.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.Header.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Header.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Header.FontFamily;
            }

            using (ExcelRange rng = ws.Cells[SubReportFromRow, SubReportFromColumn, SubReportToRow, SubReportToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.SubReportHeader.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.SubReportHeader.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.SubReportHeader.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.SubReportHeader.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.SubReportHeader.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.SubReportHeader.FontFamily;
            }
            ws.Cells[EmpInfoFromRow, 1, EmpInfoFromRow, SubReportToColumn].AutoFilter = true;
        }
        public static void SetSubReportHeaderHeading(ExcelWorksheet ws, int Row, int Column)
        {
            using (ExcelRange rng = ws.Cells[Row, Column])
            {
                rng.Style.Font.Bold = ExcelReportStyle.SubReport.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.SubReport.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.SubReport.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.SubReport.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.SubReport.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.SubReport.FontFamily;
            }
        }
        public static void SetDataPrinting(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style while data printing.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.DataPrinting.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.DataPrinting.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.DataPrinting.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.DataPrinting.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.DataPrinting.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.DataPrinting.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleRight;
            }
        }

        public static void SetDataPrinting_Earning(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style while data printing.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.DataPrinting.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.DataPrinting.BackgroundColor);
                rng.Style.Font.Color.SetColor(Color.Green);
                rng.Style.VerticalAlignment = ExcelReportStyle.DataPrinting.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.DataPrinting.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.DataPrinting.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleRight;

                rng.Style.Numberformat.Format = "#,##0.00";
            }
        }

        public static void SetDataPrinting_Deduction(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style while data printing.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.DataPrinting.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.DataPrinting.BackgroundColor);
                rng.Style.Font.Color.SetColor(Color.Red);
                rng.Style.VerticalAlignment = ExcelReportStyle.DataPrinting.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.DataPrinting.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.DataPrinting.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.DataPrinting.ExcelBorderStyleRight;

                rng.Style.Numberformat.Format = "#,##0.00";
            }
        }

        public static void SetSubReportDataPrinting(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn, bool IsEven)
        {
            Color BackgroundColor;
            if (IsEven)
            {
                BackgroundColor = Color.WhiteSmoke;
            }
            else
            {
                BackgroundColor = Color.LightGray;
            }

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.SubReportDataPrinting.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.SubReportDataPrinting.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.SubReportDataPrinting.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.SubReportDataPrinting.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.SubReportDataPrinting.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.SubReportDataPrinting.ExcelBorderStyleRight;
            }
        }
        public static void SetHighlighter(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn, Color HighlighterColor)
        {
            /// This function will apply style while data printing for highlighters.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.Highlighter.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(HighlighterColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.Highlighter.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.Highlighter.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Highlighter.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Highlighter.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.Highlighter.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.Highlighter.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.Highlighter.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.Highlighter.ExcelBorderStyleRight;
            }
        }
        public static void SetGroupOnePrinting(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn, bool IsHighlighted)
        {
            /// This function will apply style for Group One. It takes 6 Parameters which are, ExcelWorkSheet object, FromRow, FromColumn, ToRow, ToColumn and IsHighlighted.
            /// Why IsHighlighted ? Because Data Printing and Group Printing both are different functions. Data Printing will apply styles except Group Columns.
            /// So we've got its separate functions for Group One and Group Two.
            /// In Grouping, repetition of the same words is restricted. So that, we apply Group One on only Single Name and rest of the rows belonging to that group are blank. 
            /// In conclusion, we also have to apply styling on blank rows. Kindly put If else condition in your code, If Group One Exists then send IsHighlighted as True else False.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                if (IsHighlighted)
                {
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group1.BackgroundColor);
                    rng.Style.Font.Color.SetColor(ExcelReportStyle.Group1.FontColor);
                }
                else
                {
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group1.BackgroundColorHide);
                    rng.Style.Font.Color.SetColor(ExcelReportStyle.Group1.FontColorHide);
                }
                rng.Style.Font.Bold = ExcelReportStyle.Group1.IsBold;
                rng.Style.VerticalAlignment = ExcelReportStyle.Group1.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Group1.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Group1.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.Group1.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.Group1.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.Group1.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.Group1.ExcelBorderStyleRight;
            }
        }
        public static void SetGroupTwoPrinting(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn, bool IsHighlighted)
        {
            /// This function will apply style for Group Two. It takes 6 Parameters which are, ExcelWorkSheet object, FromRow, FromColumn, ToRow, ToColumn and IsHighlighted.
            /// Why IsHighlighted ? Because Data Printing and Group Printing both are different functions. Data Printing will apply styles except Group Columns.
            /// So we've got its separate functions for Group One and Group Two.
            /// In Grouping, repetition of the same words is restricted. So that, we apply Group Two on only Single Name and rest of the rows belonging to that group are blank. 
            /// In conclusion, we also have to apply styling on blank rows. Kindly put If else condition in your code, If Group Two Exists then send IsHighlighted as True else False.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                if (IsHighlighted)
                {
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group2.BackgroundColor);
                    rng.Style.Font.Color.SetColor(ExcelReportStyle.Group2.FontColor);
                }
                else
                {
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group2.BackgroundColorHide);
                    rng.Style.Font.Color.SetColor(ExcelReportStyle.Group2.FontColorHide);
                }
                rng.Style.Font.Bold = ExcelReportStyle.Group2.IsBold;
                rng.Style.VerticalAlignment = ExcelReportStyle.Group2.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Group2.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Group2.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.Group2.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.Group2.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.Group2.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.Group2.ExcelBorderStyleRight;
            }
        }
        public static void SetGroupOneTotal(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style for Group One Total. Kindly do your logics by yourself, this function is meant to apply styling only.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.Group1Total.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group1Total.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.Group1Total.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.Group1Total.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Group1Total.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Group1Total.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.Group1Total.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.Group1Total.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.Group1Total.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.Group1Total.ExcelBorderStyleRight;
            }
        }
        public static void SetGroupTwoTotal(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style for Group Tow Total. Kindly do your logics by yourself, this function is meant to apply styling only.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.Group2Total.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.Group2Total.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.Group2Total.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.Group2Total.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.Group2Total.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.Group2Total.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.Group2Total.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.Group2Total.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.Group2Total.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.Group2Total.ExcelBorderStyleRight;
            }
        }
        public static void SetGrandTotal(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply style for Grand Total. Kindly do your logics by yourself, this function is meant to apply styling only.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Style.Font.Bold = ExcelReportStyle.GrandTotal.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.GrandTotal.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.GrandTotal.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.GrandTotal.VerticalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.GrandTotal.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.GrandTotal.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.GrandTotal.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.GrandTotal.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.GrandTotal.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.GrandTotal.ExcelBorderStyleRight;
            }
        }
        public static void SetNoData(ExcelWorksheet ws, int FromRow, int FromColumn, int ToRow, int ToColumn)
        {
            /// This function will apply standard when no data is found.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            using (ExcelRange rng = ws.Cells[FromRow, FromColumn, ToRow, ToColumn])
            {
                rng.Merge = true;
                rng.Style.Font.Bold = ExcelReportStyle.NoData.IsBold;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(ExcelReportStyle.NoData.BackgroundColor);
                rng.Style.Font.Color.SetColor(ExcelReportStyle.NoData.FontColor);
                rng.Style.VerticalAlignment = ExcelReportStyle.NoData.VerticalAlignment;
                rng.Style.HorizontalAlignment = ExcelReportStyle.NoData.HorizontalAlignment;
                rng.Style.Font.Size = ExcelReportStyle.NoData.FontSize;
                //rng.Style.Font.Name = ExcelReportStyle.NoData.FontFamily;
                rng.Style.Border.Bottom.Style = ExcelReportStyle.NoData.ExcelBorderStyleBottom;
                rng.Style.Border.Top.Style = ExcelReportStyle.NoData.ExcelBorderStyleTop;
                rng.Style.Border.Left.Style = ExcelReportStyle.NoData.ExcelBorderStyleLeft;
                rng.Style.Border.Right.Style = ExcelReportStyle.NoData.ExcelBorderStyleRight;
                rng.Value = "No Data";
            }
        }
        public static void SetFreezeRows(ExcelWorksheet ws, int Row)
        {
            ///This function will freeze header rows as per standard. if its criteria doesn't meeet with standard if will not freeze rows.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            if (Row <= 8)
            {
                ws.View.FreezePanes(Row, 1);
            }
        }
        public static void SetColumnsWidth(ExcelWorksheet ws)
        {
            /// This function will apply width as per standard. You are requested to call this function at the end of your ExcelSheet code.
            /// Note: Kindly do not apply any style by yourself if you are using this function.

            ws.Cells[ws.Dimension.Address].AutoFitColumns(16, 24);
            ws.Cells[ws.Dimension.Address].Style.WrapText = true;
        }
        #endregion




        public static class ExcelReportStyle
        {
            public class HeaderPanel
            {
                public static Color BackgroundColor { get; set; }
            }
            public class ReportName
            {
                public static int FontSize { get; set; }
                public static string FontFamily { get; set; }
                public static Color FontColor { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }

            }
            public class TimeStampLabel
            {
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }

            }
            public class TimeStampValue
            {
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }

            }
            public class ReportSelectionLabel
            {
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }

            }
            public class ReportSelectionValue
            {
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }

            }
            public class Header
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static bool IsAutoFilter { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class SubReport
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static bool IsAutoFilter { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class DataPrinting
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class SubReportDataPrinting
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class Highlighter
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class Group1
            {
                public static Color BackgroundColor { get; set; }
                public static Color BackgroundColorHide { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static Color FontColorHide { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class Group1Total
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class Group2
            {
                public static Color BackgroundColor { get; set; }
                public static Color BackgroundColorHide { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static Color FontColorHide { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class Group2Total
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class GrandTotal
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
            public class NoData
            {
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleBottom { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleTop { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleLeft { get; set; }
                public static ExcelBorderStyle ExcelBorderStyleRight { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
                public static ExcelHorizontalAlignment HorizontalAlignment { get; set; }
            }
            public class SubReportHeader
            {
                public static Color EmployeeInfoBackgroundColor { get; set; }
                public static int EmployeeInfoFontSize { get; set; }
                public static Color EmployeeInfoFontColor { get; set; }
                public static Color BackgroundColor { get; set; }
                public static int FontSize { get; set; }
                public static Color FontColor { get; set; }
                public static string FontFamily { get; set; }
                public static bool IsBold { get; set; }
                public static bool IsWrap { get; set; }
                public static bool IsAutoFilter { get; set; }
                public static ExcelVerticalAlignment VerticalAlignment { get; set; }
            }
        }
    }
}