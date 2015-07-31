using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using JMReports.Entities;

namespace JMReports.Data
{
    public class DataImportDAL : DataAccessComponent
    {
        public IList<ErrorMessage> Errors { get; private set; }

        #region Precise cache
        private static Dictionary<string, int> DicPrecise = null;
        private static void InitDicPrecise()
        {
            DicPrecise = new Dictionary<string, int>();
            string strsql = string.Format(@"select ID,Precise from ReportItem");
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = db.ExecuteDataSet(CommandType.Text, strsql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DicPrecise[dr["ID"].ToString()] = Convert.ToInt32(dr["Precise"].ToString());
            }
            return;
        }
        public static int GetPreciseByItemId(string itemid)
        {
            if (DicPrecise == null)
            {
                InitDicPrecise();
            }
            if (DicPrecise.Keys.Contains(itemid))
                return DicPrecise[itemid];
            return 1;
        }
        #endregion

        #region AccountItem cache
        private static Dictionary<string, AccountItemComparision> DicAccountItemComparision = null;
        private static void InitAccountItemComparision()
        {
            DicAccountItemComparision = new Dictionary<string, AccountItemComparision>();
            string strsql = string.Format(@"select * from AccountItemComparision");
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            DataSet ds = db.ExecuteDataSet(CommandType.Text, strsql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                AccountItemComparision entity = new AccountItemComparision();
                entity.ItemID = Convert.ToInt32(dr["ItemID"].ToString());
                entity.Divisor = Convert.ToInt32(dr["Divisor"].ToString());
                entity.Dividend = Convert.ToInt32(dr["Dividend"].ToString());
                entity.Comment = dr["Comment"].ToString();
                entity.YearCode = Convert.ToInt32(dr["YearCode"].ToString());
                DicAccountItemComparision[entity.YearCode + "-" + entity.ItemID] = entity;
            }
            return;
        }
        public static List<int> GetItemIDsByYearCode(string yearCode)
        {
            if (DicAccountItemComparision == null)
            {
                InitAccountItemComparision();
            }
            List<int> list = new List<int>();
            foreach (string key in DicAccountItemComparision.Keys)
            {
                if (key.StartsWith(yearCode))
                    list.Add(DicAccountItemComparision[key].ItemID);
            }
            return list;
        }
        #endregion

        private DataSet InitialBudgetDS(Database db, int yearcode, int monthcode, int hotelid)
        {
            //Load budgetmonthly2_ex by year, month, and hotel
            string strsql = string.Format("SELECT yearCode, MonthCode, ItemId, convert(nvarchar(50), ItemId)  ItemIdStr, Budget, MonthDate, HotelId, TrackCode FROM [Repository].[dbo].[BudgetMonthly2_ex] where yearCode = {0} and MonthCode = {1} and HotelId = {2}"
                , yearcode
                , monthcode
                , hotelid);
            var ds = db.ExecuteDataSet(CommandType.Text, strsql);

            return ds;
        }

        private bool CompareDS(string tableName, DataTable leftDT, DataTable rightDT, IList<string> exclusiveItemId)
        {
           return true;   //Cesc 今年没有预算，临时改动

            var d = from a in leftDT.AsEnumerable()
                    where !a.IsNull(0) //&& a.Field<double>("ItemId") != 168
                    join b in rightDT.AsEnumerable() on a.Field<double>("ItemId").ToString() equals b.Field<string>("ItemIdStr") into ps
                    from c in ps.DefaultIfEmpty()
                        //where c == null || (a.IsNull("本月预算") && !c.IsNull("Budget")) || Math.Abs(a.Field<double>("本月预算") - Convert.ToDouble(c.Field<decimal>("Budget"))) >= 1  //a.Field<double>("本月预算") != Convert.ToDouble(c == null ? decimal.MinValue : c.Field<decimal>("Budget"))     
                    where c == null || Math.Abs((a.IsNull("本月预算") ? 0.0 : a.Field<double>("本月预算")) - Convert.ToDouble(c.IsNull("Budget") ? 0.0m : c.Field<decimal>("Budget"))) >= 1
                    //select a;
                    select new { ItemId = Convert.ToString(a.Field<double>("ItemId")), RightItemId = c == null ? "N/A" : c.Field<string>("ItemIdStr"), LeftBudget = a.Field<double?>("本月预算"), RightBudget = c == null ? double.MinValue : Convert.ToDouble(c.Field<decimal>("Budget")) };
            //select new { ItemId = a.Field<double>("ItemId"), RightItemId = c.Field<string>("ItemIdStr"), RightBudget = c.Field<string>("BudgetStr") };

            bool retBool = false;
            Errors = new List<ErrorMessage>();
            if (exclusiveItemId != null)
            {
                var dd = from x in d
                         join y in exclusiveItemId
                         on x.ItemId equals y into yy
                         from y2 in yy.DefaultIfEmpty()
                         where y2 == null
                         select x;

                foreach (var item in dd)
                {
                    var d0 = item.ItemId;
                    Errors.Add(new ErrorMessage { TableName = tableName, ItemId = item.ItemId, LeftBudget = item.LeftBudget, RightBudget = item.RightBudget, ErrorType = (item.RightItemId == "N/A" ? ErrorType.MissingBugdet : ErrorType.BugdetMismatch), Description = item.RightItemId == "N/A" ? "未预先导入预算" : "和预先导入预算不匹配" });

                }
                retBool = !dd.Any();
            }
            else
            {
                foreach (var item in d)
                {
                    //var left = item.Field<double>("ItemId");
                    var a0 = item.ItemId;
                    var a1 = item.RightItemId;
                    var a2 = item.LeftBudget;
                    var a3 = item.RightBudget;
                    Errors.Add(new ErrorMessage { TableName = tableName, ItemId = item.ItemId, LeftBudget = item.LeftBudget, RightBudget = item.RightBudget, ErrorType = (item.RightItemId == "N/A" ? ErrorType.MissingBugdet : ErrorType.BugdetMismatch), Description = item.RightItemId == "N/A" ? "未预先导入预算" : "和预先导入预算不匹配" });

                }
                retBool = !d.Any();
            }

            return retBool;
        }

        //Leo: 2014.11.10, Write down status into DataImportStatus
        public bool LogDataImportStatus(int datatype, int yearcode, int hotelid, string message)
        {
            bool retBool = false;

            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction dbTran = conn.BeginTransaction();
                try
                {
                    string sqlstmt = "insert into DataImportstatus(YearCode, HotelId, Message, DataType, Status)	values(@YearCode, @HotelId, @Message, @DataType, 0);";

                    var dbcmd = db.GetSqlStringCommand(sqlstmt);

                    db.AddInParameter(dbcmd, "@YearCode", DbType.Int32, yearcode);
                    db.AddInParameter(dbcmd, "@HotelId", DbType.Int32, hotelid);
                    db.AddInParameter(dbcmd, "@Message", DbType.String, message);
                    db.AddInParameter(dbcmd, "@DataType", DbType.Int32, datatype);
                    db.ExecuteNonQuery(dbcmd, dbTran);
                    dbTran.Commit();
                    retBool = true;

                }
                catch
                {
                    dbTran.Rollback();
                    retBool = false;
                }
            }

            return retBool;
        }

        public int DataImport(DataTable dt, string tablename, int yearcode, int monthcode, int hotelid)
        {
            string strsql = string.Empty;
            int mReturnCount = 0;
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            string mMonth = yearcode.ToString() + "-" + monthcode + "-1";

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction dbTran = conn.BeginTransaction();

                try
                {

                    #region 专用表

                    if (tablename == "专用表")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string itemid = dr["itemid"].ToString();
                            string itemname = dr["项目"].ToString();
                            string monthact = dr["本月实际"].ToString();

                            if (monthact != "")
                            {
                                //删除已经存在的数据
                                strsql = "delete from ActMonthly2_ex where yearcode=" + yearcode.ToString() + " and MonthCode=" + monthcode.ToString() +
                                            " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                                //存入数据库
                                strsql = string.Format(@"insert into ActMonthly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,Hotelid,TrackCode)" +
                                         " values({0},{1},{2},{3},'{4}',{5},{6})",
                                         yearcode.ToString(),
                                         monthcode.ToString(),
                                         itemid.ToString(),
                                         monthact.ToString(),
                                         mMonth.ToString(),
                                         hotelid.ToString(), 3);

                                int vCount = db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                //处理年ActYearly2的数据
                                strsql = string.Format(@"delete ActYearly2_ex where HotelId={0} and ItemId={1} and yearCode={2}",
                                    hotelid.ToString(), itemid.ToString(), yearcode.ToString());
                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();

                                for (int m = 1; m <= 12; m++)
                                {

                                    string mMonth2 = yearcode.ToString() + "-" + m.ToString() + "-1";

                                    //获取小于本月的年合计数值
                                    strsql = string.Format(@"select SUM(act) act from ActMonthly2_ex where  HotelId={0} and  ItemId={1} and yearCode={2} and MonthCode<={3}",
                                                     hotelid.ToString(), itemid.ToString(), yearcode.ToString(), m.ToString());

                                    string vAmount = string.Empty;
                                    vAmount = db.ExecuteScalar(dbTran, CommandType.Text, strsql).ToString();

                                    if (vAmount == "")
                                    {
                                        vAmount = "0";
                                    }

                                    strsql = string.Format(@"insert into ActYearly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,HotelId,TrackCode)
                                                    values({0},{1},{2},{3},'{4}',{5},{6})",
                                        yearcode.ToString(), m.ToString(), itemid.ToString(), vAmount, mMonth2, hotelid, 3);
                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();
                                }

                                mReturnCount = vCount + mReturnCount;

                            }
                        }
                    }
                    #endregion

                    #region 专用表_预测


                    if (tablename == "专用表_预测")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string itemid = dr["ItemId"].ToString();
                            if (itemid != string.Empty)
                            {
                                string m1 = dr["第一个月"].ToString();
                                string m2 = dr["第二个月"].ToString();
                                string m3 = dr["第三个月"].ToString();
                                string m4 = dr["第四个月"].ToString();
                                string m5 = dr["第五个月"].ToString();
                                string m6 = dr["第六个月"].ToString();
                                string[] mlist = { m1, m2, m3, m4, m5, m6 };
                                var mResult = mlist.ToList().Where(x => VerifyActData(x)).Count() > 0;
                                if (mResult)
                                {

                                    int a = 12 - monthcode >= 6 ? 0 : 6 - (12 - monthcode);
                                    DateTime baseDt = new DateTime(year: yearcode, month: monthcode, day: 1);
                                    //Delete, and insert
                                    int vCount = 0;

                                    for (int i = 0; i < mlist.Length; i++)
                                    {
                                        //if (!VerifyActData(mlist[i]))
                                        //    continue;
                                        var currentDt = baseDt.AddMonths(i + 1);
                                        //删除已经存在的数据
                                        strsql = "delete from ActMonthly2_special_forcast where yearcode=" + currentDt.Year.ToString() + " and MonthCode=" + currentDt.Month.ToString() +
                                                    " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);
                                        //存入数据库
                                        strsql = string.Format(@"insert into ActMonthly2_special_forcast(yearCode,MonthCode,ItemId,Act,MonthDate,Hotelid,TrackCode)" +
                                                 @" values({0},{1},{2},{3},'{4}',{5},{6})",
                                                 currentDt.Year.ToString(),
                                                 currentDt.Month.ToString(),
                                                 itemid.ToString(),
                                                 String.IsNullOrEmpty(mlist[i]) ? "NULL" : mlist[i],
                                                 currentDt.ToString(),
                                                 hotelid.ToString(), 3);

                                        vCount = db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);



                                        //int vCount = db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);
                                    }

                                    mReturnCount = vCount + mReturnCount;
                                }
                            }
                        }

                    }


                    #endregion

                    #region 餐饮部效率
                    if (tablename == "餐饮部效率")
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            string itemid = dr["itemid"].ToString();
                            string itemname = dr["项目"].ToString();
                            string monthact = dr["本月实际"].ToString();

                            if (monthact != "")
                            {
                                //删除已经存在的数据
                                strsql = "delete from ActMonthly2_ex where yearcode=" + yearcode.ToString() + " and MonthCode=" + monthcode.ToString() +
                                            " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                                //存入数据库
                                strsql = string.Format(@"insert into ActMonthly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,Hotelid,TrackCode)" +
                                         " values({0},{1},{2},{3},'{4}',{5},{6})",
                                         yearcode.ToString(),
                                         monthcode.ToString(),
                                         itemid.ToString(),
                                         monthact.ToString(),
                                         mMonth.ToString(),
                                         hotelid.ToString(), 3);

                                int vCount = db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                //处理年ActYearly2的数据
                                strsql = string.Format(@"delete ActYearly2_ex where HotelId={0} and ItemId={1} and yearCode={2}",
                                    hotelid.ToString(), itemid.ToString(), yearcode.ToString());
                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();

                                for (int m = 1; m <= 12; m++)
                                {

                                    string mMonth2 = yearcode.ToString() + "-" + m.ToString() + "-1";

                                    //获取小于本月的年合计数值
                                    strsql = string.Format(@"select SUM(act) act from ActMonthly2_ex where  HotelId={0} and  ItemId={1} and yearCode={2} and MonthCode<={3}",
                                                     hotelid.ToString(), itemid.ToString(), yearcode.ToString(), m.ToString());

                                    string vAmount = string.Empty;
                                    vAmount = db.ExecuteScalar(dbTran, CommandType.Text, strsql).ToString();

                                    if (vAmount == "")
                                    {
                                        vAmount = "0";
                                    }


                                    strsql = string.Format(@"insert into ActYearly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,HotelId,TrackCode)
                                                    values({0},{1},{2},{3},'{4}',{5},{6})",
                                        yearcode.ToString(), m.ToString(), itemid.ToString(), vAmount, mMonth2, hotelid, 3);
                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();
                                }

                                mReturnCount = vCount + mReturnCount;

                            }
                        }
                    }
                    #endregion

                    #region 客房部效率
                    if (tablename == "客房部效率")
                    {

                        foreach (DataRow dr in dt.Rows)
                        {
                            string itemid = dr["itemid"].ToString();
                            string itemname = dr["项目"].ToString();
                            string monthact = dr["本月实际"].ToString();

                            if (monthact != "")
                            {
                                //删除已经存在的数据
                                strsql = "delete from ActMonthly2_ex where yearcode=" + yearcode.ToString() + " and MonthCode=" + monthcode.ToString() +
                                            " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                                //存入数据库
                                strsql = string.Format(@"insert into ActMonthly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,Hotelid,TrackCode)" +
                                         " values({0},{1},{2},{3},'{4}',{5},{6})",
                                         yearcode.ToString(),
                                         monthcode.ToString(),
                                         itemid.ToString(),
                                         monthact.ToString(),
                                         mMonth.ToString(),
                                         hotelid.ToString(), 3);

                                int vCount = db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                //处理年ActYearly2的数据
                                strsql = string.Format(@"delete ActYearly2_ex where HotelId={0} and ItemId={1} and yearCode={2}",
                                    hotelid.ToString(), itemid.ToString(), yearcode.ToString());
                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();

                                for (int m = 1; m <= 12; m++)
                                {

                                    string mMonth2 = yearcode.ToString() + "-" + m.ToString() + "-1";

                                    //获取小于本月的年合计数值
                                    strsql = string.Format(@"select SUM(act) act from ActMonthly2_ex where  HotelId={0} and  ItemId={1} and yearCode={2} and MonthCode<={3}",
                                                     hotelid.ToString(), itemid.ToString(), yearcode.ToString(), m.ToString());

                                    string vAmount = string.Empty;
                                    vAmount = db.ExecuteScalar(dbTran, CommandType.Text, strsql).ToString();

                                    if (vAmount == "")
                                    {
                                        vAmount = "0";
                                    }

                                    strsql = string.Format(@"insert into ActYearly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,HotelId,TrackCode)
                                                    values({0},{1},{2},{3},'{4}',{5},{6})",
                                        yearcode.ToString(), m.ToString(), itemid.ToString(), vAmount, mMonth2, hotelid, 3);
                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();
                                }

                                mReturnCount = vCount + mReturnCount;

                            }
                        }
                    }
                    #endregion

                    #region 客房市场细分
                    if (tablename == "客房市场细分")
                    {
                        //Build the exclusive item id list
                        var itemIds = new List<string>();
                        if (hotelid == 1 || hotelid == 2)
                        {
                            itemIds.AddRange(new string[] { "468", "469", "470", "471", "472", "473", "474", "475", "476" });
                        }
                        else if (hotelid == 3)
                        {

                            //itemIds.AddRange(new string[] { "30727", "30728", "30729", "30730", "30731", "30732", "30733", "30734", "30735", "30736", "30737", "30738", "30739", "30740", "30741" });
                        }

                        var a = InitialBudgetDS(db, yearcode, monthcode, hotelid);

                        var b = CompareDS(tablename, dt, a.Tables[0], itemIds);

                        if (!b)
                        {
                            //预算未导入，或新导入数据和预算不匹配
                        }
                        else
                        {
                            string itemid = string.Empty;
                            string ItemEName = string.Empty;
                            string ItemCName = string.Empty;
                            string KPI = string.Empty;
                            string MonthActual = string.Empty;
                            string MonthBudget = string.Empty;

                            string ProcessPercent = string.Empty;
                            string LastMonthActual = string.Empty;
                            string QoQ = string.Empty;
                            string YearActual = string.Empty;
                            string YearBudgetActual = string.Empty;
                            string YearPercent = string.Empty;
                            string QoQYear = string.Empty;
                            string YoYYear = string.Empty;

                            foreach (DataRow dr in dt.Rows)
                            {
                                itemid = dr["ItemId"].ToString();
                                ItemEName = dr["英文代码"].ToString();
                                ItemCName = dr["细分市场"].ToString();
                                KPI = dr["KPI"].ToString();

                                MonthActual = dr["本月实际"].ToString();
                                if (MonthActual.Trim() == string.Empty) MonthActual = "0";

                                MonthBudget = dr["本月预算"].ToString();
                                if (MonthBudget.Trim() == string.Empty) MonthBudget = "0";

                                ProcessPercent = dr["完成比例"].ToString();
                                if (ProcessPercent.Trim() == string.Empty) ProcessPercent = "0";

                                LastMonthActual = dr["上年同期"].ToString();
                                if (LastMonthActual.Trim() == string.Empty) LastMonthActual = "0";

                                QoQ = dr["同比增减"].ToString();
                                if (QoQ.Trim() == string.Empty) QoQ = "0";

                                YearActual = dr["本年实际"].ToString();
                                if (YearActual.Trim() == string.Empty) YearActual = "0";

                                YearBudgetActual = dr["累计预算"].ToString();
                                if (YearBudgetActual.Trim() == string.Empty) YearBudgetActual = "0";

                                YearPercent = dr["完成比例1"].ToString();
                                if (YearPercent.Trim() == string.Empty) YearPercent = "0";

                                QoQYear = dr["上年同期1"].ToString();
                                if (QoQYear.Trim() == string.Empty) QoQYear = "0";

                                YoYYear = dr["同比增减1"].ToString();
                                if (YoYYear.Trim() == string.Empty) YoYYear = "0";

                                if (itemid != "")
                                {
                                    //Part 1: Import data from 1st segment
                                    if (int.Parse(itemid) <= 196 || (int.Parse(itemid) >= 30700 && int.Parse(itemid) <= 30726))
                                    {
                                        strsql = "delete from RoomMarket where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                        " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                        strsql = string.Format(@"insert into RoomMarket(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,MonthBudget,ProcessPercent,LastMonthActual,QoQ,YearActual,YearBudgetActual,YearPercent,QoQYear,YoYYear,OnYear,OnMonth)" +
                                             " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})",
                                             hotelid.ToString(),
                                             itemid.ToString(),
                                             ItemEName.ToString(),
                                             ItemCName.ToString(),
                                             KPI.ToString(),
                                             MonthActual.ToString(),
                                             MonthBudget.ToString(),
                                             ProcessPercent.ToString(),
                                             LastMonthActual.ToString(),
                                             QoQ.ToString(),
                                             YearActual.ToString(),
                                             YearBudgetActual.ToString(),
                                             YearPercent.ToString(),
                                             QoQYear.ToString(),
                                             YoYYear.ToString(),
                                             yearcode.ToString(),
                                             monthcode.ToString()
                                             );

                                        int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());
                                        mReturnCount = vCount + mReturnCount;
                                    }

                                    //Part 2: for Shenzhen JW
                                    if (int.Parse(itemid) >= 30727)
                                    {
                                        strsql = "delete from RoomMarket_GroupDetail where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                        " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                        strsql = string.Format(@"insert into RoomMarket_GroupDetail(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,MonthBudget,ProcessPercent,LastMonthActual,QoQ,YearActual,YearBudgetActual,YearPercent,QoQYear,YoYYear,OnYear,OnMonth)" +
                                             " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})",
                                             hotelid.ToString(),
                                             itemid.ToString(),
                                             ItemEName.ToString(),
                                             ItemCName.ToString(),
                                             KPI.ToString(),
                                             MonthActual.ToString(),
                                             MonthBudget.ToString(),
                                             ProcessPercent.ToString(),
                                             LastMonthActual.ToString(),
                                             QoQ.ToString(),
                                             YearActual.ToString(),
                                             YearBudgetActual.ToString(),
                                             YearPercent.ToString(),
                                             QoQYear.ToString(),
                                             YoYYear.ToString(),
                                             yearcode.ToString(),
                                             monthcode.ToString()
                                             );

                                        int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());
                                        mReturnCount = vCount + mReturnCount;
                                    }


                                    if (int.Parse(itemid) >= 468 && int.Parse(itemid) <= 476)
                                    {
                                        strsql = "delete from RoomMarket_CompanyPrice where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                            " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                                        MonthActual = dr["本月实际"].ToString();
                                        if (MonthActual.Trim() == string.Empty) MonthActual = "0";

                                        LastMonthActual = dr["本月预算"].ToString();
                                        if (LastMonthActual.Trim() == string.Empty) LastMonthActual = "0";

                                        QoQ = dr["完成比例"].ToString();
                                        if (QoQ.Trim() == string.Empty) QoQ = "0";

                                        YearActual = dr["上年同期"].ToString();
                                        if (YearActual.Trim() == string.Empty) YearActual = "0";

                                        QoQYear = dr["同比增减"].ToString();
                                        if (QoQYear.Trim() == string.Empty) QoQYear = "0";

                                        YoYYear = dr["本年实际"].ToString();
                                        if (YoYYear.Trim() == string.Empty) YoYYear = "0";


                                        strsql = string.Format(@"insert into RoomMarket_CompanyPrice(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,LastMonthActual,QoQ,YearActual,QoQYear,YoYYear,OnYear,OnMonth)" +
                                             " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12})",
                                             hotelid.ToString(),
                                             itemid.ToString(),
                                             ItemEName.ToString(),
                                             ItemCName.ToString(),
                                             KPI.ToString(),
                                             MonthActual.ToString(),
                                             LastMonthActual.ToString(),
                                             QoQ.ToString(),
                                             YearActual.ToString(),
                                             QoQYear.ToString(),
                                             YoYYear.ToString(),
                                             yearcode.ToString(),
                                             monthcode.ToString()
                                             );

                                        int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());

                                        mReturnCount = vCount + mReturnCount;
                                    }


                                }
                            }
                        }


                    }

                    #endregion

                    #region 客房销售渠道
                    if (tablename == "客房销售渠道")
                    {
                        var a = InitialBudgetDS(db, yearcode, monthcode, hotelid);

                        var b = CompareDS(tablename, dt, a.Tables[0], null);

                        if (!b)
                        {
                            //预算未导入，或新导入数据和预算不匹配
                        }
                        else
                        {
                            string itemid = string.Empty;
                            string ItemEName = string.Empty;
                            string ItemCName = string.Empty;
                            string KPI = string.Empty;
                            string MonthActual = string.Empty;
                            string MonthBudget = string.Empty;

                            string ProcessPercent = string.Empty;
                            string LastMonthActual = string.Empty;
                            string QoQ = string.Empty;
                            string YearActual = string.Empty;
                            string YearBudgetActual = string.Empty;
                            string YearPercent = string.Empty;
                            string QoQYear = string.Empty;
                            string YoYYear = string.Empty;
                            string TopYoYYear = string.Empty;

                            foreach (DataRow dr in dt.Rows)
                            {
                                itemid = dr["itemid"].ToString();
                                ItemEName = dr["ItemName"].ToString();
                                ItemCName = dr["ItemName"].ToString();
                                KPI = dr["KPI"].ToString();

                                MonthActual = dr["本月实际"].ToString();
                                if (MonthActual.Trim() == string.Empty) MonthActual = "0";

                                //MonthBudget = dr["本月预算"].ToString();
                                //if (MonthBudget.Trim() == string.Empty) MonthBudget = "0";

                                //ProcessPercent = dr["完成比例"].ToString();
                                //if (ProcessPercent.Trim() == string.Empty) ProcessPercent = "0";

                                LastMonthActual = dr["上年同期"].ToString();
                                if (LastMonthActual.Trim() == string.Empty) LastMonthActual = "0";

                                QoQ = dr["同比增减"].ToString();
                                if (QoQ.Trim() == string.Empty) QoQ = "0";

                                YearActual = dr["本年实际"].ToString();
                                if (YearActual.Trim() == string.Empty) YearActual = "0";

                                //YearBudgetActual = dr["累计预算"].ToString();
                                //if (YearBudgetActual.Trim() == string.Empty) YearBudgetActual = "0";

                                //YearPercent = dr["完成比例"].ToString();
                                //if (YearPercent.Trim() == string.Empty) YearPercent = "0";

                                QoQYear = dr["上年同期1"].ToString();
                                if (QoQYear.Trim() == string.Empty) QoQYear = "0";

                                YoYYear = dr["同比增减1"].ToString();
                                if (YoYYear.Trim() == string.Empty) YoYYear = "0";

                                TopYoYYear = dr["F10"].ToString();
                                if (TopYoYYear.Trim() == string.Empty) TopYoYYear = "0";

                                if (itemid != "")
                                {
                                    strsql = "delete from RoomSales where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                                    " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                    //strsql = string.Format(@"insert into RoomSales(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,MonthBudget,ProcessPercent,LastMonthActual,QoQ,YearActual,YearBudgetActual,YearPercent,QoQYear,YoYYear,OnYear,OnMonth)" +
                                    //     " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})",
                                    //     hotelid.ToString(),
                                    //     itemid.ToString(),
                                    //     ItemEName.ToString(),
                                    //     ItemCName.ToString(),
                                    //     KPI.ToString(),
                                    //     MonthActual.ToString(),
                                    //     MonthBudget.ToString(),
                                    //     ProcessPercent.ToString(),
                                    //     LastMonthActual.ToString(),
                                    //     QoQ.ToString(),
                                    //     YearActual.ToString(),
                                    //     YearBudgetActual.ToString(),
                                    //     YearPercent.ToString(),
                                    //     QoQYear.ToString(),
                                    //     YoYYear.ToString(),
                                    //     yearcode.ToString(),
                                    //     monthcode.ToString()
                                    //     );
                                    strsql = string.Format(@"insert into RoomSales(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,LastMonthActual,QoQ,YearActual,QoQYear,YoYYear,TopYoYYear,OnYear,OnMonth)" +
                                         " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13})",
                                         hotelid.ToString(),
                                         itemid.ToString(),
                                         ItemEName.ToString(),
                                         ItemCName.ToString(),
                                         KPI.ToString(),
                                         MonthActual.ToString(),
                                         //MonthBudget.ToString(),
                                         //ProcessPercent.ToString(),
                                         LastMonthActual.ToString(),
                                         QoQ.ToString(),
                                         YearActual.ToString(),
                                         //YearBudgetActual.ToString(),
                                         //YearPercent.ToString(),
                                         QoQYear.ToString(),
                                         YoYYear.ToString(),
                                         TopYoYYear.ToString(),
                                         yearcode.ToString(),
                                         monthcode.ToString()
                                         );

                                    int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());

                                    mReturnCount = vCount + mReturnCount;
                                }


                            }

                        }
                    }
                    #endregion

                    #region 宴会细分市场


                    if (tablename == "宴会细分市场")
                    {
                        var a = InitialBudgetDS(db, yearcode, monthcode, hotelid);

                        var b = CompareDS(tablename, dt, a.Tables[0], null);

                        if (!b)
                        {
                            //预算未导入，或新导入数据和预算不匹配
                        }
                        else
                        {
                            string itemid = string.Empty;
                            string ItemEName = string.Empty;
                            string ItemCName = string.Empty;
                            string KPI = string.Empty;
                            string MonthActual = string.Empty;
                            string MonthBudget = string.Empty;

                            string ProcessPercent = string.Empty;
                            string LastMonthActual = string.Empty;
                            string QoQ = string.Empty;
                            string YearActual = string.Empty;
                            string YearBudgetActual = string.Empty;
                            string YearPercent = string.Empty;
                            string QoQYear = string.Empty;
                            string YoYYear = string.Empty;

                            foreach (DataRow dr in dt.Rows)
                            {
                                itemid = dr["itemid"].ToString();
                                ItemEName = dr["收入"].ToString();
                                ItemCName = dr["收入"].ToString();
                                //KPI = dr["KPI"].ToString();

                                MonthActual = dr["本月实际"].ToString();
                                if (MonthActual.Trim() == string.Empty) MonthActual = "0";

                                MonthBudget = dr["本月预算"].ToString();
                                if (MonthBudget.Trim() == string.Empty) MonthBudget = "0";

                                ProcessPercent = dr["完成比例"].ToString();
                                if (ProcessPercent.Trim() == string.Empty) ProcessPercent = "0";

                                LastMonthActual = dr["上年同期"].ToString();
                                if (LastMonthActual.Trim() == string.Empty) LastMonthActual = "0";

                                QoQ = dr["同比增减"].ToString();
                                if (QoQ.Trim() == string.Empty) QoQ = "0";

                                YearActual = dr["本年实际"].ToString();
                                if (YearActual.Trim() == string.Empty) YearActual = "0";

                                YearBudgetActual = dr["累计预算"].ToString();
                                if (YearBudgetActual.Trim() == string.Empty) YearBudgetActual = "0";

                                YearPercent = dr["完成比例1"].ToString();
                                if (YearPercent.Trim() == string.Empty) YearPercent = "0";

                                QoQYear = dr["上年同期1"].ToString();
                                if (QoQYear.Trim() == string.Empty) QoQYear = "0";

                                YoYYear = dr["同比增减1"].ToString();
                                if (YoYYear.Trim() == string.Empty) YoYYear = "0";

                                if (itemid != "")
                                {
                                    strsql = "delete from BanquetMarket where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                                    " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();

                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                    strsql = string.Format(@"insert into BanquetMarket(HotelId,ItemId,ItemEName,ItemCName,KPI,MonthActual,MonthBudget,ProcessPercent,LastMonthActual,QoQ,YearActual,YearBudgetActual,YearPercent,QoQYear,YoYYear,OnYear,OnMonth)" +
                                         " values({0},{1},'{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16})",
                                         hotelid.ToString(),
                                         itemid.ToString(),
                                         ItemEName.ToString(),
                                         ItemCName.ToString(),
                                         KPI.ToString(),
                                         MonthActual.ToString(),
                                         MonthBudget.ToString(),
                                         ProcessPercent.ToString(),
                                         LastMonthActual.ToString(),
                                         QoQ.ToString(),
                                         YearActual.ToString(),
                                         YearBudgetActual.ToString(),
                                         YearPercent.ToString(),
                                         QoQYear.ToString(),
                                         YoYYear.ToString(),
                                         yearcode.ToString(),
                                         monthcode.ToString()
                                         );

                                    int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());

                                    mReturnCount = vCount + mReturnCount;
                                }


                            }
                        }

                    }

                    #endregion

                    #region 客房竞争组合

                    if (tablename == "客房竞争组合")
                    {
                        //Here, to serialize the data table which come from the Excel 

                        string mItemId = string.Empty;
                        string mCombination = string.Empty;
                        string mSTR = string.Empty;
                        string mOccupancy_MyProp = string.Empty;
                        string mOccupancy_CompSet = string.Empty;
                        string mOccupancy_Index = string.Empty;
                        string mOccupancy_MPI = string.Empty;

                        string mADR_MyProp = string.Empty;
                        string mADR_CompSet = string.Empty;
                        string mADR_Index = string.Empty;
                        string mADR_MPI = string.Empty;

                        string mRevPAR_MyProp = string.Empty;
                        string RevPAR_CompSet = string.Empty;
                        string mRevPAR_Index = string.Empty;
                        string mRevPAR_MPI = string.Empty;

                        string mOnYear = string.Empty;
                        string mOnMonth = string.Empty;
                        string mDecorate = string.Empty;

                        strsql = "delete from RoomCompete2 where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                               " and hotelId=" + hotelid.ToString();
                        strsql += ";" + "delete from RoomCompete where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                                " and hotelId=" + hotelid.ToString();

                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                        string prefix = "", combination = "", subgroup = "";

                        foreach (DataRow dr in dt.Rows)
                        {
                            mItemId = dr[0].ToString();

                            if (mItemId != "")
                            {

                                //--------------------

                                if (int.Parse(mItemId) == 0)
                                {
                                    prefix = dr[1].ToString();
                                    combination = dr[2].ToString();
                                }

                                if (string.IsNullOrWhiteSpace(combination) || string.IsNullOrEmpty(combination))
                                    continue;

                                if (int.Parse(mItemId) == -1)
                                {
                                    subgroup = dr[1].ToString();
                                }

                                else if (int.Parse(mItemId) <= 12 && int.Parse(mItemId) > 0)
                                {

                                    strsql = string.Format(@"insert into RoomCompete2(HotelId, ItemId, OnYear, OnMonth, B, C, D, E, F, G, H, I, J, K, L, M, N, Combination, SubGroup) 
values('{0}','{1}','{2}','{3}',{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},@Combination, @SubGroup)",
                                        hotelid, mItemId, yearcode.ToString(), monthcode.ToString(), ReplaceNull2(dr[1].ToString()), ReplaceNull2(dr[2].ToString()), ReplaceNull2(dr[3].ToString()),
                                        ReplaceNull2(dr[4].ToString()), ReplaceNull2(dr[5].ToString()), ReplaceNull2(dr[6].ToString()), ReplaceNull2(dr[7].ToString()),
                                        ReplaceNull2(dr[8].ToString()), ReplaceNull2(dr[9].ToString()), ReplaceNull2(dr[10].ToString()), ReplaceNull2(dr[11].ToString()),
                                        ReplaceNull2(dr[12].ToString()), ReplaceNull2(dr[13].ToString()));
                                    //, prefix + combination, subgroup
                                    var dbcmd = db.GetSqlStringCommand(strsql);

                                    db.AddInParameter(dbcmd, "@Combination", DbType.String, prefix + combination);
                                    db.AddInParameter(dbcmd, "@SubGroup", DbType.String, subgroup);
                                    int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbcmd, dbTran).ToString());
                                    mReturnCount = vCount + mReturnCount;
                                }
                                else if (int.Parse(mItemId) > 12 && int.Parse(mItemId) <= 14)
                                {

                                    int offset = 1;
                                    mSTR = dr[1].ToString();
                                    mOccupancy_MyProp = dr[2 + offset].ToString();
                                    mOccupancy_CompSet = dr[3 + offset].ToString();
                                    mOccupancy_Index = dr[4 + offset].ToString();
                                    mOccupancy_MPI = dr[5 + offset].ToString();

                                    mADR_MyProp = dr[6 + offset].ToString();
                                    mADR_CompSet = dr[7 + offset].ToString();
                                    mADR_Index = dr[8 + offset].ToString();
                                    mADR_MPI = dr[9 + offset].ToString();

                                    mRevPAR_MyProp = dr[10 + offset].ToString();
                                    RevPAR_CompSet = dr[11 + offset].ToString();
                                    mRevPAR_Index = dr[12 + offset].ToString();
                                    mRevPAR_MPI = dr[13 + offset].ToString();

                                    mCombination = prefix + combination; //dr[14].ToString();



                                    mDecorate = "1";


                                    strsql = string.Format(@"insert into RoomCompete(ItemId,HotelId,Combination,STR1,Occupancy_MyProp,Occupancy_CompSet,Occupancy_Index,Occupancy_MPI,ADR_MyProp,ADR_CompSet,ADR_Index,ADR_MPI,RevPAR_MyProp,RevPAR_CompSet,RevPAR_Index,RevPAR_MPI,OnYear,OnMonth,Decorate)" +
                                             " values({0},{1},@Combination, '{2}',{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})",
                                             mItemId.ToString(),
                                             hotelid.ToString(),
                                             //mCombination.ToString(),
                                             ReplaceNull(mSTR.ToString()),
                                             ReplaceNull(mOccupancy_MyProp.ToString()),
                                             ReplaceNull(mOccupancy_CompSet.ToString()),
                                             ReplaceNull(mOccupancy_Index.ToString()),
                                             ReplaceNull(mOccupancy_MPI.ToString()),
                                             ReplaceNull(mADR_MyProp.ToString()),
                                             ReplaceNull(mADR_CompSet.ToString()),
                                             ReplaceNull(mADR_Index.ToString()),
                                             ReplaceNull(mADR_MPI.ToString()),
                                             ReplaceNull(mRevPAR_MyProp.ToString()),
                                             ReplaceNull(RevPAR_CompSet.ToString()),
                                             ReplaceNull(mRevPAR_Index.ToString()),
                                             ReplaceNull(mRevPAR_MPI.ToString()),
                                             yearcode.ToString(),
                                             monthcode.ToString(),
                                             mDecorate.ToString()
                                             );

                                    var dbcmd = db.GetSqlStringCommand(strsql);

                                    db.AddInParameter(dbcmd, "@Combination", DbType.String, prefix + combination);
                                    int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbcmd, dbTran).ToString());

                                    mReturnCount = vCount + mReturnCount;
                                }


                            }
                        }

                    }

                    #endregion

                    #region 预测

                    if (tablename == "预测")
                    {
                        string mItemId = string.Empty;


                        string mOnTheBook = string.Empty;
                        string mRoomCount = string.Empty;
                        string mAvgRoomPrice = string.Empty;
                        string mRevenue = string.Empty;

                        string mHotelId = string.Empty;
                        string mOnYear = string.Empty;
                        string mOnMonth = string.Empty;
                        string mDecorate = string.Empty;

                        foreach (DataRow dr in dt.Rows)
                        {
                            mItemId = dr["ItemId"].ToString();

                            if (mItemId != "")
                            {

                                mOnTheBook = dr["On The Book"].ToString();
                                mRoomCount = dr["房晚数"].ToString();
                                mAvgRoomPrice = dr["平均房价"].ToString();
                                mRevenue = dr["收入（万元）"].ToString();


                                strsql = "delete from Forecast where onYear=" + yearcode.ToString() + " and onMonth=" + monthcode.ToString() +
                                            " and itemid=" + mItemId + " and hotelid=" + hotelid.ToString();

                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);


                                strsql = string.Format(@"insert into Forecast(ItemId,OnTheBook,RoomCount,AvgRoomPrice,Revenue,HotelId,OnYear,OnMonth)" +
                                         " values({0},'{1}',{2},{3},{4},{5},{6},{7})",
                                         mItemId.ToString(),
                                         mOnTheBook.ToString(),
                                         mRoomCount.ToString(),
                                         mAvgRoomPrice.ToString(),
                                         mRevenue.ToString(),
                                         hotelid.ToString(),
                                         yearcode.ToString(),
                                         monthcode.ToString()
                                         );

                                int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());

                                mReturnCount = vCount + mReturnCount;


                            }
                        }

                    }

                    #endregion

                    #region 其他运营部门

                    if (tablename == "其他运营部门")
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            string itemid = dr["itemid"].ToString();
                            string itemname = dr["项目"].ToString();
                            string monthact = dr["本月实际"].ToString();
                            //string monthbudget = dr["本月预算"].ToString();

                            if (itemid != "")
                            {
                                //删除已经存在的数据
                                strsql = "delete from ActMonthly2_ex where yearcode=" + yearcode.ToString() + " and MonthCode=" + monthcode.ToString() +
                                            " and itemid=" + itemid + " and hotelid=" + hotelid.ToString();


                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                                //存入数据库
                                strsql = string.Format(@"insert into ActMonthly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,Hotelid,TrackCode)" +
                                         " values({0},{1},{2},{3},'{4}',{5},{6})",
                                         yearcode.ToString(),
                                         monthcode.ToString(),
                                         itemid.ToString(),
                                         monthact.ToString(),
                                         mMonth.ToString(),
                                         hotelid.ToString(), 3);

                                int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());
                                mReturnCount = vCount + mReturnCount;

                                //处理年ActYearly2的数据
                                strsql = string.Format(@"delete ActYearly2_ex where HotelId={0} and ItemId={1} and yearCode={2}",
                                    hotelid.ToString(), itemid.ToString(), yearcode.ToString());
                                db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();

                                for (int m = 1; m <= 12; m++)
                                {

                                    string mMonth2 = yearcode.ToString() + "-" + m.ToString() + "-1";

                                    //获取小于本月的年合计数值
                                    strsql = string.Format(@"select SUM(act) act from ActMonthly2_ex where  HotelId={0} and  ItemId={1} and yearCode={2} and MonthCode<={3}",
                                                     hotelid.ToString(), itemid.ToString(), yearcode.ToString(), m.ToString());

                                    string vAmount = string.Empty;
                                    vAmount = db.ExecuteScalar(dbTran, CommandType.Text, strsql).ToString();

                                    if (vAmount == "")
                                    {
                                        vAmount = "0";
                                    }

                                    strsql = string.Format(@"insert into ActYearly2_ex(yearCode,MonthCode,ItemId,Act,MonthDate,HotelId,TrackCode)
                                                    values({0},{1},{2},{3},'{4}',{5},{6})",
                                        yearcode.ToString(), m.ToString(), itemid.ToString(), vAmount, mMonth2, hotelid, 3);
                                    db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString();
                                }
                            }
                        }

                    }

                    #endregion

                    dbTran.Commit();

                }
                catch
                {
                    dbTran.Rollback();
                    mReturnCount = 0;
                }
            }
            return mReturnCount;
        }

        private string ReplaceNull(string datavalue)
        {
            return (string.IsNullOrEmpty(datavalue) || string.IsNullOrWhiteSpace(datavalue)) ? "NULL" : datavalue;
        }

        private string ReplaceNull2(string datavalue)
        {
            return (string.IsNullOrEmpty(datavalue) || string.IsNullOrWhiteSpace(datavalue)) ? "NULL" : "'" + datavalue + "'";
        }


        public int getPrecise(Database db, int itemid)
        {

            string strsql = string.Format(@"select Precise from ReportItem where ID ={0} ", itemid.ToString());


            int vReturnValue = 0;

            vReturnValue = int.Parse(db.ExecuteScalar(CommandType.Text, strsql).ToString());

            return vReturnValue;
        }

        public int DataImportBudget(DataTable dt, string tablename, int yearcode, int hotelid)
        {
            string strsql = string.Empty;
            int mReturnCount = 0;
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);

            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction dbTran = conn.BeginTransaction();
                try
                {
                    List<int> listFilter = GetItemIDsByYearCode(yearcode.ToString());
                    Dictionary<string, Dictionary<int, decimal>> dicResult = new Dictionary<string, Dictionary<int, decimal>>();
                    dicResult = GetBudgetDicResult(dt, listFilter);

                    foreach (string itemid in dicResult.Keys)
                    {
                        strsql = "delete from BudgetMonthly2_ex where yearcode=" + yearcode.ToString() + "and ItemId=" + itemid + " and hotelid=" + hotelid.ToString();
                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                        strsql = "delete from BudgetYearly2_ex where yearcode=" + yearcode.ToString() + "and ItemId=" + itemid + " and hotelid=" + hotelid.ToString();
                        db.ExecuteNonQuery(dbTran, CommandType.Text, strsql);

                        decimal yearBudget = 0;
                        foreach (int monthCode in dicResult[itemid].Keys)
                        {
                            decimal monthbudget = dicResult[itemid][monthCode];
                            string padMonthCode = monthCode.ToString().PadLeft(2, '0');
                            strsql = string.Format(@"insert into BudgetMonthly2_ex(yearCode,MonthCode,ItemId,Budget,MonthDate,Hotelid,TrackCode)" +
                                            " values({0},{1},{2},{3},'{4}',{5},{6})",
                                            yearcode.ToString(),
                                            Convert.ToInt32(monthCode),
                                            itemid.ToString(),
                                            monthbudget.ToString(),
                                            yearcode + "-" + padMonthCode + "-01",
                                            hotelid.ToString(), 3);

                            int vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());
                            mReturnCount = vCount + mReturnCount;
                            yearBudget += monthbudget;


                            //处理BudgetYearly2_ex
                            //计算本年的合计，然后insert into 到
                            strsql = string.Format(@"insert into BudgetYearly2_ex(yearCode,MonthCode,ItemId,Budget,MonthDate,Hotelid,TrackCode)" +
                                        " values({0},{1},{2},{3},'{4}',{5},{6})",
                                        yearcode.ToString(),
                                        Convert.ToInt32(monthCode),
                                        itemid.ToString(),
                                        yearBudget.ToString(),
                                        yearcode + "-" + padMonthCode + "-01",
                                        hotelid.ToString(), 3);

                            vCount = Convert.ToInt32(db.ExecuteNonQuery(dbTran, CommandType.Text, strsql).ToString());
                            mReturnCount = vCount + mReturnCount;
                        }

                    }
                    dbTran.Commit();

                }
                catch
                {
                    dbTran.Rollback();
                    mReturnCount = 0;
                }
            }
            return mReturnCount;
        }

        private Dictionary<string, Dictionary<int, decimal>> GetBudgetDicResult(DataTable dt, List<int> listFilter)
        {
            Dictionary<string, Dictionary<int, decimal>> dicResult = new Dictionary<string, Dictionary<int, decimal>>();

            foreach (DataRow dr in dt.Rows)
            {

                string itemid = dr["itemid"].ToString();
                string itemname = dr["项目"].ToString();
                if (itemid != "" && !listFilter.Contains(Convert.ToInt32(itemid)))
                {
                    Dictionary<int, decimal> dicRow = new Dictionary<int, decimal>();
                    for (int i = 1; i <= 12; i++)
                    {
                        string monthColName = i.ToString() + "月份";
                        string monthbudget = dr[monthColName].ToString();
                        decimal v_monthbudget = 0.00m;
                        if (monthbudget != "")
                        {
                            v_monthbudget = decimal.Parse(monthbudget) * GetPreciseByItemId(itemid);
                        }
                        dicRow[i] = v_monthbudget;
                    }
                    dicResult[itemid] = dicRow;
                }
            }
            return dicResult;

        }

        private bool VerifyActData(string a)
        {
            decimal b;
            return decimal.TryParse(a, out b);
        }
    }
}
