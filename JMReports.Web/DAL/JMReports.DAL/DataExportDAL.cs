using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JMReports.Entities;

using System.Net;
using System.IO;
using System.Web.Services.Protocols;
using JMReports.Data.JMWebReference;
using Microsoft.Office.Interop.Excel;


namespace JMReports.Data
{
    public class DataExportDAL : DataAccessComponent
    {

        public int ExportData(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {
            int returnValue = 0;
            try
            {
                //专用表
                OutPutSpecialReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //经营情况一览表
                OutPutOperationReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //酒店整体表
                OutPutHotelWholeReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //客房市场细分
                OutPutRoomMarketReport2Report(mTitle, mHotelId, mYear, mMonth, tablenames);
                //客房销售渠道
                OutPutRoomSalesReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //宴会细分市场
                OutPutBanquetMarketReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //不可分摊成本费用
                OutPutUnallocateReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //各餐厅
                OutPutRestaurantVarReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //主要餐厅汇总
                OutPutRestaurantMainReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //其他运营部门
                OutPutOtherBusinessReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //预测报表
                OutPutForecastReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //餐饮部效率
                OutPutRestaurantEfficiencyReport(mTitle, mHotelId, mYear, mMonth, tablenames);
                //客房部效率
                OutPutRoomEfficiencyReport(mTitle, mHotelId, mYear, mMonth, tablenames);


                MergeExcel();
                returnValue = 1;
            }
            catch (Exception ex)
            {
               returnValue = 0; 
               throw ex ;
            }

            return returnValue;
        }


        /// <summary>
        /// 导出酒店专用表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        public void OutPutSpecialReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "1";
                    break;
                case "2":
                    mReportid = "20001";
                    break;
                case "3":
                    mReportid = "30001";
                    break;
            } 



            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/SpecialReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\酒店专用表.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 导出经营情况一览表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutOperationReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {


            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "2";
                    break;
                case "2":
                    mReportid = "20002";
                    break;
                case "3":
                    mReportid = "30002";
                    break;
            } 


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/OperationReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\经营情况一览.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 导出酒店整体表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutHotelWholeReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "3";
                    break;
                case "2":
                    mReportid = "20003";
                    break;
                case "3":
                    mReportid = "30003";
                    break;
            } 


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/HotelWholeRpt";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\酒店整体表.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        /// <summary>
        /// 客房细分市场
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mHotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRoomMarketReport2Report(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;

            string reportPath = "/JMHotels/RoomMarketReport2";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "hotelid";
            parameters[1].Value = mHotelId;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\客房细分市场.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 客房销售渠道
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mHotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRoomSalesReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;

            string reportPath = "/JMHotels/RoomSales";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "hotelid";
            parameters[1].Value = mHotelId;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\客房销售渠道.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 宴会细分市场
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mHotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutBanquetMarketReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;

            string reportPath = "/JMHotels/BanquetMarket";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "hotelid";
            parameters[1].Value = mHotelId;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\宴会细分市场.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 导出不可分摊成本费用
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutUnallocateReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "5";
                    break;
                case "2":
                    mReportid = "20005";
                    break;
                case "3":
                    mReportid = "30005";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/UnallocateRpt";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\不可分摊成本费用.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 导出各餐厅报表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRestaurantVarReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "11";
                    break;
                case "2":
                    mReportid = "20011";
                    break;
                case "3":
                    mReportid = "30011";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/RestaurantVarReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[5];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            parameters[4] = new ParameterValue();
            parameters[4].Name = "hotel";
            parameters[4].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\各餐厅.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 导出主要餐厅汇总报表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRestaurantMainReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "10";
                    break;
                case "2":
                    mReportid = "20010";
                    break;
                case "3":
                    mReportid = "30010";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/RestaurantMainReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\主要餐厅汇总.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 其他运营部门
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutOtherBusinessReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "14";
                    break;
                case "2":
                    mReportid = "20014";
                    break;
                case "3":
                    mReportid = "30014";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/OtherBusinessReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {
                FileStream stream = File.Create(@"c:\jmreport_download\其他运营部门.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 预测报表
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mHotelId"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutForecastReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;

            string reportPath = "/JMHotels/ForecastReport";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "hotelid";
            parameters[1].Value = mHotelId;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\预测报表.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 导出餐饮部效率
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRestaurantEfficiencyReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "13";
                    break;
                case "2":
                    mReportid = "20013";
                    break;
                case "3":
                    mReportid = "30013";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/RestaurantEfficiency";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\餐饮部效率.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 导出客房部效率
        /// </summary>
        /// <param name="mTitle"></param>
        /// <param name="mReportid"></param>
        /// <param name="mYear"></param>
        /// <param name="mMonth"></param>
        /// <param name="tablenames"></param>
        public void OutPutRoomEfficiencyReport(string mTitle, string mHotelId, string mYear, string mMonth, string tablenames)
        {

            //根据hotelid ，返回report id
            string mReportid = string.Empty;



            switch (mHotelId)
            {
                case "1":
                    mReportid = "9";
                    break;
                case "2":
                    mReportid = "20009";
                    break;
                case "3":
                    mReportid = "30009";
                    break;
            }


            ReportExecutionService rs = new ReportExecutionService();
            //rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;

            rs.PreAuthenticate = true;
            rs.Credentials = new NetworkCredential("jmadmin", "klm9ab43", "JINMAO_GROUP");

            rs.Url = "http://172.16.209.76/reportserver/ReportExecution2005.asmx";

            // Render arguments
            byte[] result = null;
            string reportPath = "/JMHotels/RoomEfficiencyRpt";

            //string format = "MHTML";
            string format = "EXCEL";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            ParameterValue[] parameters = new ParameterValue[4];
            parameters[0] = new ParameterValue();
            parameters[0].Name = "title";
            parameters[0].Value = mTitle;

            parameters[1] = new ParameterValue();
            parameters[1].Name = "reportid";
            parameters[1].Value = mReportid;

            parameters[2] = new ParameterValue();
            parameters[2].Name = "year";
            parameters[2].Value = mYear;

            parameters[3] = new ParameterValue();
            parameters[3].Name = "month";
            parameters[3].Value = mMonth;

            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rs.ExecutionHeaderValue = execHeader;

            execInfo = rs.LoadReport(reportPath, historyID);

            rs.SetExecutionParameters(parameters, "en-us");
            String SessionId = rs.ExecutionHeaderValue.ExecutionID;

            try
            {
                result = rs.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);

                execInfo = rs.GetExecutionInfo();
            }
            catch (SoapException ex)
            {
                throw ex;
            }
            // Write the contents of the report to an excel file.

            try
            {

                FileStream stream = File.Create(@"c:\jmreport_download\客房部效率.xls", result.Length);
                stream.Write(result, 0, result.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        static void MergeExcel()
        {


            string dir = @"C:\jmreport_download";

            //报表合并
            Microsoft.Office.Interop.Excel.Application excel = new Application();

            Microsoft.Office.Interop.Excel.Workbook workbook1 = excel.Workbooks.Open(dir + "//酒店专用表.xls",
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing);
            Microsoft.Office.Interop.Excel.Workbook workbook2 = excel.Workbooks.Open(dir + "//经营情况一览.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook3 = excel.Workbooks.Open(dir + "//酒店整体表.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook4 = excel.Workbooks.Open(dir + "//客房细分市场.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook5 = excel.Workbooks.Open(dir + "//客房销售渠道.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook6 = excel.Workbooks.Open(dir + "//宴会细分市场.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook7 = excel.Workbooks.Open(dir + "//不可分摊成本费用.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook8 = excel.Workbooks.Open(dir + "//各餐厅.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook9 = excel.Workbooks.Open(dir + "//主要餐厅汇总.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook10 = excel.Workbooks.Open(dir + "//其他运营部门.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook11 = excel.Workbooks.Open(dir + "//预测报表.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook12 = excel.Workbooks.Open(dir + "//餐饮部效率.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Workbook workbook13 = excel.Workbooks.Open(dir + "//客房部效率.xls",
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);

            Microsoft.Office.Interop.Excel.Worksheet worksheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook1.Sheets["SpecialReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet2 = (Microsoft.Office.Interop.Excel.Worksheet)workbook2.Sheets["OperationReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet3 = (Microsoft.Office.Interop.Excel.Worksheet)workbook3.Sheets["HotelWholeRpt"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet4 = (Microsoft.Office.Interop.Excel.Worksheet)workbook4.Sheets["RoomMarketReport2"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet5 = (Microsoft.Office.Interop.Excel.Worksheet)workbook5.Sheets["RoomSales"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet6 = (Microsoft.Office.Interop.Excel.Worksheet)workbook6.Sheets["BanquetMarket"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet7 = (Microsoft.Office.Interop.Excel.Worksheet)workbook7.Sheets["UnallocateRpt"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet8 = (Microsoft.Office.Interop.Excel.Worksheet)workbook8.Sheets["RestaurantVarReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet9 = (Microsoft.Office.Interop.Excel.Worksheet)workbook9.Sheets["RestaurantMainReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet10 = (Microsoft.Office.Interop.Excel.Worksheet)workbook10.Sheets["OtherBusinessReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet11 = (Microsoft.Office.Interop.Excel.Worksheet)workbook11.Sheets["ForecastReport"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet12 = (Microsoft.Office.Interop.Excel.Worksheet)workbook12.Sheets["RestaurantEfficiency"];
            Microsoft.Office.Interop.Excel.Worksheet worksheet13 = (Microsoft.Office.Interop.Excel.Worksheet)workbook13.Sheets["RoomEfficiencyRpt"]; 

            //设置报表Sheet Name
            worksheet1.Name = "专用表";
            worksheet2.Name = "经营情况一览";
            worksheet3.Name = "酒店整体表";
            worksheet4.Name = "客房细分市场";
            worksheet5.Name = "客房销售渠道";
            worksheet6.Name = "宴会细分市场";
            worksheet7.Name = "不可分摊成本费用";
            worksheet8.Name = "各餐厅";
            worksheet9.Name = "主要餐厅汇总";
            worksheet10.Name = "其他运营部门";
            worksheet11.Name = "预测报表";
            worksheet12.Name = "餐饮部效率";
            worksheet13.Name = "客房部效率";

            //合并报表
            worksheet2.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet3.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet4.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet5.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet6.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet7.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet8.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet9.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet10.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet11.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet12.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            worksheet13.Copy(Type.Missing, worksheet1);
            workbook1.Save();

            workbook1.Close(false, Type.Missing, Type.Missing);
            workbook2.Close(false, Type.Missing, Type.Missing);
            workbook3.Close(false, Type.Missing, Type.Missing);
            workbook4.Close(false, Type.Missing, Type.Missing);
            workbook5.Close(false, Type.Missing, Type.Missing);
            workbook6.Close(false, Type.Missing, Type.Missing);
            workbook7.Close(false, Type.Missing, Type.Missing);
            workbook8.Close(false, Type.Missing, Type.Missing);
            workbook9.Close(false, Type.Missing, Type.Missing);
            workbook10.Close(false, Type.Missing, Type.Missing);
            workbook11.Close(false, Type.Missing, Type.Missing);
            workbook12.Close(false, Type.Missing, Type.Missing);
            workbook13.Close(false, Type.Missing, Type.Missing);

            //如果报表文件存在，先删除
            if (File.Exists(@"c:\jmreport_download\JINMAO.xls"))
            {
                File.Delete(@"c:\jmreport_download\JINMAO.xls");
            }
            File.Copy(@"c:\jmreport_download\酒店专用表.xls", @"c:\jmreport_download\JINMAO.xls");


            workbook1 = null;
            workbook2 = null;
            workbook3 = null;
            workbook4 = null;
            workbook5 = null;
            workbook6 = null;
            workbook7 = null;
            workbook8 = null;
            workbook9 = null;
            workbook10 = null;
            workbook11 = null;
            workbook12 = null;
            workbook13 = null;

            excel.Quit();
            excel = null;
        }

    }


}
