using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Extensions
{
    #region 【Static Class : DateTimeExtension】
    /// <summary>
    /// DateTimeクラスの拡張メソッド
    /// </summary>
    public static class DateTimeExtension
    {
        #region 【Public Inner Class : DateRange】
        /// <summary>
        /// 日付範囲クラス
        /// </summary>
        public class DateRange : IEnumerable<DateTime>
        {
            #region 【Properties】

            #region **** Property : IsEmpty (Set private only)
            /// <summary>
            /// 範囲が空？
            /// </summary>
            public bool IsEmpty { get; private set; }
            #endregion

            #region **** Property : RangeFrom (Set private only)
            /// <summary>
            /// 範囲開始日
            /// </summary>
            public DateTime? RangeFrom { get; private set; }
            #endregion

            #region **** Property : RangeTo (Set private only)
            /// <summary>
            /// 範囲終了日
            /// </summary>
            public DateTime? RangeTo { get; private set; }
            #endregion

            #region **** Property : Span
            /// <summary>
            /// 範囲の TimeSpan を返す
            /// </summary>
            public TimeSpan Span
            {
                get
                {
                    return (RangeTo ?? DateTime.MaxValue) - (RangeFrom ?? DateTime.MinValue);
                }
            }
            #endregion

            #region **** Property : Days
            /// <summary>
            /// 範囲の日数を返す
            /// </summary>
            public int Days
            {
                get
                {
                    var f = (RangeFrom ?? DateTime.MinValue).StripTime();
                    var t = (RangeTo ?? DateTime.MaxValue).AddDays(1).StripTime();
                    return (t - f).Days;
                }
            }
            #endregion

            #region **** Property : DisplayFormat
            /// <summary>
            /// ToString() 時の書式設定
            /// </summary>
            public string DisplayFormat { get; set; }
            #endregion

            #region **** Private Property : _RangeFrom
            /// <summary>
            /// 内部利用範囲開始値
            /// </summary>
            private DateTime _RangeFrom
            {
                get { return RangeFrom ?? DateTime.MinValue; }

            }
            #endregion

            #region **** Private Property : _RangeTo
            /// <summary>
            /// 内部利用範囲終了値
            /// </summary>
            private DateTime _RangeTo
            {
                get { return RangeTo ?? DateTime.MaxValue; }
            }
            #endregion

            #endregion

            #region 【Constructor】

            #region **** Constructor(1)
            /// <summary>
            /// コンストラクタ(1)
            /// </summary>
            public DateRange()
            {
                RangeFrom = null;
                RangeTo = null;
                DisplayFormat = @"{0}～{1}";
                IsEmpty = true;
            }
            #endregion

            #region **** Constructor(2)
            /// <summary>
            /// コンストラクタ(2)
            /// </summary>
            /// <param name="from">開始日</param>
            /// <param name="to">終了日</param>
            public DateRange(DateTime? from, DateTime? to)
            {
                RangeFrom = from;
                RangeTo = to;
                DisplayFormat = @"{0}～{1}";
                IsEmpty = _RangeTo < _RangeFrom;
            }
            #endregion

            #endregion

            #region 【Methods】

            #region **** Method : GetEnumerator
            /// <summary>
            /// IEnumerable.GetEnumerator の実装(Generics版)
            /// </summary>
            /// <returns>IEnumerator of DateTime</returns>
            IEnumerator<DateTime> IEnumerable<DateTime>.GetEnumerator()
            {
                if (!IsEmpty)
                {
                    for (var dt = _RangeFrom; dt <= _RangeTo; dt = dt.AddDays(1))
                        yield return dt;
                }
            }
            #endregion

            #region **** Method : GetEnumerator
            /// <summary>
            /// IEnumerable.GetEnumerator の実装
            /// </summary>
            /// <returns>IEnumerator of DateTime</returns>
            /// 
            IEnumerator IEnumerable.GetEnumerator()
            {
                if (!IsEmpty)
                {
                    for (var dt = _RangeFrom; dt <= _RangeTo; dt = dt.AddDays(1))
                        yield return dt;
                }
            }
            #endregion

            #region **** Method : InRange
            /// <summary>
            /// 指定した日が範囲内
            /// </summary>
            /// <param name="dt">検査日</param>
            /// <returns>bool</returns>
            public bool InRange(DateTime dt)
            {
                if (!IsEmpty)
                    return _RangeFrom <= dt && dt <= _RangeTo;
                else
                    return false;
            }
            #endregion

            #region **** Method : OutOfRange
            /// <summary>
            /// 指定した日が範囲外
            /// </summary>
            /// <param name="dt">DateTime : 検査日</param>
            /// <returns>bool</returns>
            public bool OutOfRange(DateTime dt)
            {
                return !InRange(dt);
            }
            #endregion

            #region **** Method : ToString(1)
            /// <summary>
            /// 文字列化(1)
            /// </summary>
            /// <param name="displayFormat">表示書式</param>
            /// <returns>string</returns>
            public string ToString(string displayFormat)
            {
                return string.Format(displayFormat, _RangeFrom, _RangeTo);
            }
            #endregion

            #region **** Method : ToString(2)
            /// <summary>
            /// 文字列化(2)
            /// </summary>
            /// <returns>string</returns>
            public override string ToString()
            {
                return ToString(DisplayFormat);
            }
            #endregion

            #endregion
        }
        #endregion

        #region ■ Extension Methods ■

        #region **** Extenson Method : SetYear
        /// <summary>
        /// 対象日の年を変更した値を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="value">int</param>
        /// <returns>DateTime</returns>
        public static DateTime SetYear(this DateTime dt, int value)
        {
            return new DateTime(value, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : SetMonth
        /// <summary>
        /// 対象日の月を変更した値を返す
        /// </summary>
        /// <param name="dt">thid</param>
        /// <param name="value">int</param>
        /// <returns>DateTime</returns>
        public static DateTime SetMonth(this DateTime dt, int value)
        {
            return new DateTime(dt.Year, value, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : SetDay
        /// <summary>
        /// 対象日の日を変更した値を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="value">int</param>
        /// <returns>DateTime</returns>
        public static DateTime SetDay(this DateTime dt, int value)
        {
            return new DateTime(dt.Year, dt.Month, value, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : IsLeapYear
        /// <summary>
        /// 対象日を含む年が閏年かどうかを返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>bool</returns>
        public static bool IsLeapYear(this DateTime dt)
        {
            return DateTime.IsLeapYear(dt.Year);
        }
        #endregion

        #region **** Extension Method : BeginOfYear
        /// <summary>
        /// 対象日を含む年の年初日を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime BeginOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : EndOfYear
        /// <summary>
        /// 対象日を含む年の大みそかを返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfYear(this DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : BeginOfMonth
        /// <summary>
        /// 対象日を含む年月の月初日を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime BeginOfMonth(this DateTime dt)
        {
            return dt.AddDays((dt.Day - 1) * -1);
        }
        #endregion

        #region **** Extension Method : EndOfMonth
        /// <summary>
        /// 対象日を含む年月の月末尾を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month), dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
        #endregion

        #region **** Extension Method : SundayOfWeek
        /// <summary>
        /// 対象日を含む週の日曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime SundayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Sunday)
                return dt.AddDays((int)dt.DayOfWeek * -1);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : MondayOfWeek
        /// <summary>
        /// 対象日を含む週の月曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime MondayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Monday)
                return dt.SundayOfWeek().AddDays(1);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : TuesdayOfWeek
        /// <summary>
        /// 対象日を含む週の火曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime TuesdayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Tuesday)
                return dt.SundayOfWeek().AddDays(2);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : WednesdayOfWeek
        /// <summary>
        /// 対象日を含む週の水曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime WednesdayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Wednesday)
                return dt.SundayOfWeek().AddDays(3);
            else
                return dt;
        }
        #endregion

        #region **** Exxtension Method : ThursdayOfWeek
        /// <summary>
        /// 対象日を含む週の木曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime ThursdayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Thursday)
                return dt.SundayOfWeek().AddDays(4);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : FridayOfWeek
        /// <summary>
        /// 対象日を含む週の金曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime FridayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Friday)
                return dt.SundayOfWeek().AddDays(5);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : SaturdayOfWeek
        /// <summary>
        /// 対象日を含む週の土曜日を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime SaturdayOfWeek(this DateTime dt)
        {
            if (dt.DayOfWeek != DayOfWeek.Saturday)
                return dt.SundayOfWeek().AddDays(6);
            else
                return dt;
        }
        #endregion

        #region **** Extension Method : StripTime
        /// <summary>
        /// 時刻を落とす
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateTime</returns>
        public static DateTime StripTime(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
        #endregion

        #region **** Extension Method : StripDate
        /// <summary>
        /// 日付を落としす
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="baseDate">DateTime? : 基準日</param>
        /// <returns>DateTime</returns>
        public static DateTime StripDate(this DateTime dt, DateTime? baseDate = null)
        {
            baseDate = baseDate ?? DateTime.MinValue;
            return new DateTime(baseDate.Value.Year, baseDate.Value.Month, baseDate.Value.Day, dt.Hour, dt.Minute, dt.Second);
        }
        #endregion

        #region **** Extension Method : Days
        /// <summary>
        /// 対象日から指定日数分の範囲を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="value">int</param>
        /// <returns>DateRange</returns>
        public static DateRange Days(this DateTime dt, int value)
        {
            if (value > 0)
                return new DateRange(dt, dt.AddDays(value - 1));
            else
                return new DateRange();
        }
        #endregion

        #region **** Extension Method : Months
        /// <summary>
        /// 対象日から指定月数分の範囲を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="value">int</param>
        /// <returns>DateRange</returns>
        public static DateRange Months(this DateTime dt, int value)
        {
            if (value > 0)
                return new DateRange(dt, dt.AddMonths(value).AddDays(-1));
            else
                return new DateRange();
        }
        #endregion

        #region **** Extension Method : Years
        /// <summary>
        /// 対象日から指定年数分の範囲を返す。
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="value">int</param>
        /// <returns>DateRange</returns>
        public static DateRange Years(this DateTime dt, int value)
        {
            if (value > 0)
                return new DateRange(dt, dt.AddYears(value).AddDays(-1));
            else
                return new DateRange();
        }
        #endregion

        #region **** Extension Method : DaysInYear
        /// <summary>
        /// 対象日を含む年の範囲を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateRange</returns>
        public static DateRange DaysInYear(this DateTime dt)
        {
            return new DateRange(dt.BeginOfYear(), dt.EndOfYear());
        }
        #endregion

        #region **** Extension Method : DaysInMonth
        /// <summary>
        /// 対象日を含む年月の範囲を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <returns>DateRange</returns>
        public static DateRange DaysInMonth(this DateTime dt)
        {
            return new DateRange(dt.BeginOfMonth(), dt.EndOfMonth());
        }
        #endregion

        #region **** Extension Method : FiscalMonth
        /// <summary>
        /// 対象日を含む会計年月を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="closingDay">int : 締日</param>
        /// <returns>DateTime</returns>
        public static DateTime FiscalMonth(this DateTime dt, int closingDay)
        {
            if (dt.Day <= closingDay)
                return dt.BeginOfMonth();
            else
                return dt.BeginOfMonth().AddMonths(1);
        }
        #endregion

        #region **** Extension Method : BeginOfFiscalMonth
        /// <summary>
        /// 対象日を含む会計年月の開始日を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="closingDay">int : 締日</param>
        /// <returns>DateTime</returns>
        public static DateTime BeginOfFiscalMonth(this DateTime dt, int closingDay)
        {
            if (closingDay == 31 || dt.BeginOfMonth().AddMonths(-1).EndOfMonth().Day <= closingDay)
                return dt.BeginOfMonth();
            else
                return dt.BeginOfMonth().AddMonths(-1).SetDay(closingDay + 1);
        }
        #endregion

        #region **** Extension Method : EndOfFiscalMonth
        /// <summary>
        /// 対象日を含む会計年月の終了日を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="closingDay">int</param>
        /// <returns>DateTime</returns>
        public static DateTime EndOfFiscalMonth(this DateTime dt, int closingDay)
        {
            if (closingDay == 31)
                return dt.EndOfMonth();
            else
                return dt.SetDay(closingDay);
        }
        #endregion

        #region **** Extension Method : DaysInFiscalMonth
        /// <summary>
        /// 対象日を含む会計年月の範囲を返す
        /// </summary>
        /// <param name="dt">this</param>
        /// <param name="closingDay">int : 締日</param>
        /// <returns>DateRange</returns>
        public static DateRange DaysInFiscalMonth(this DateTime dt, int closingDay)
        {
            return new DateRange(dt.BeginOfFiscalMonth(closingDay), dt.EndOfFiscalMonth(closingDay));
        }
        #endregion

        #region **** Extension Method : FiscalYear
        /// <summary>
        /// 対象日を含む会計年度を返す
        /// </summary>
        /// <param name="dt">DateTime : this</param>
        /// <param name="startingMonth">int : 年度の開始月</param>
        /// <returns>int</returns>
        public static int FiscalYear(this DateTime dt, int startingMonth)
        {
            return (dt.Month >= startingMonth) ? dt.Year : dt.Year - 1;
        }
        #endregion

        #endregion
    }
    #endregion
}
