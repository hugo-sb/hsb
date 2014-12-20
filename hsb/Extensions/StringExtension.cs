using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Globalization;

namespace hsb.Extensions
{
    #region 【Static Class : StringExtension】
    /// <summary>
    /// Stringクラスの拡張メソッド
    /// </summary>
    public static class StringExtension
    {
        #region ■ DLL Import ■

        #region **** DLL Import function : LCMapStringW
        /// <summary>
        /// LCMapStringW
        /// </summary>
        /// <param name="Locale">int</param>
        /// <param name="dwMapFlags">uint</param>
        /// <param name="lpSrcStr">string</param>
        /// <param name="cchSrc">int</param>
        /// <param name="lpDestStr">string</param>
        /// <param name="cchDest">int</param>
        /// <returns>int</returns>
        [DllImport("kernel32.dll")]
        static extern int LCMapStringW(int Locale, uint dwMapFlags,
            [MarshalAs(UnmanagedType.LPWStr)]string lpSrcStr, int cchSrc,
            [MarshalAs(UnmanagedType.LPWStr)] string lpDestStr, int cchDest);
        #endregion

        #endregion

        #region ■ Enums ■

        #region **** Enum : MapFlags
        /// <summary>
        /// LCMapStringWに使用するフラグ定数
        /// </summary>
        public enum MapFlags : uint
        {
            NORM_IGNORECASE = 0x00000001,           //大文字と小文字を区別しません。
            NORM_IGNORENONSPACE = 0x00000002,       //送りなし文字を無視します。このフラグをセットすると、日本語アクセント文字も削除されます。
            NORM_IGNORESYMBOLS = 0x00000004,        //記号を無視します。
            LCMAP_LOWERCASE = 0x00000100,           //小文字を使います。
            LCMAP_UPPERCASE = 0x00000200,           //大文字を使います。
            LCMAP_SORTKEY = 0x00000400,             //正規化されたワイド文字並び替えキーを作成します。
            LCMAP_BYTEREV = 0x00000800,             //Windows NT のみ : バイト順序を反転します。たとえば 0x3450 と 0x4822 を渡すと、結果は 0x5034 と 0x2248 になります。
            SORT_STRINGSORT = 0x00001000,           //区切り記号を記号と同じものとして扱います。
            NORM_IGNOREKANATYPE = 0x00010000,       //ひらがなとカタカナを区別しません。ひらがなとカタカナを同じと見なします。
            NORM_IGNOREWIDTH = 0x00020000,          //シングルバイト文字と、ダブルバイトの同じ文字とを区別しません。
            LCMAP_HIRAGANA = 0x00100000,            //ひらがなにします。
            LCMAP_KATAKANA = 0x00200000,            //カタカナにします。
            LCMAP_HALFWIDTH = 0x00400000,           //半角文字にします（適用される場合）。
            LCMAP_FULLWIDTH = 0x00800000,           //全角文字にします（適用される場合）。
            LCMAP_LINGUISTIC_CASING = 0x01000000,   //大文字と小文字の区別に、ファイルシステムの規則（既定値）ではなく、言語上の規則を使います。LCMAP_LOWERCASE、または LCMAP_UPPERCASE とのみ組み合わせて使えます。
            LCMAP_SIMPLIFIED_CHINESE = 0x02000000,  //中国語の簡体字を繁体字にマップします。
            LCMAP_TRADITIONAL_CHINESE = 0x04000000, //中国語の繁体字を簡体字にマップします。
        }
        #endregion

        #endregion

        #region ■ Extension Methods ■

        #region **** Extension Method : Omission
        /// <summary>
        /// 文字列を指定桁数で省略する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <param name="length">int : 桁数</param>
        /// <returns>string</returns>
        public static string Omission(this string s, int length)
        {
            if (s.Length > length)
                return string.Format("{0}…", s.Substring(0, length - 1));
            else
                return s;
        }
        #endregion

        #region **** Extension Method : Chomp
        /// <summary>
        /// 文字列末尾の改行コードを削除する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <returns>string</returns>
        public static string Chomp(this string s)
        {
            return Regex.Replace(s, "[\r\n]+$", "");
        }
        #endregion

        #region **** Extension Method : StringConvert
        /// <summary>
        ///  文字列を変換する（LCMapStringWのラッパ)
        /// </summary>
        /// <param name="s">string : 変換元の文字列</param>
        /// <param name="flags">MapFlags : 変換条件</param>
        /// <returns>string</returns>
        public static string StringConvert(this string s, MapFlags flags)
        {
            var ci = CultureInfo.CurrentCulture;
            var result = new string(' ', s.Length);
            LCMapStringW(ci.LCID, (uint)flags, s, s.Length, result, result.Length);
            return result;
        }
        #endregion

        #region **** Extension Method : Han2Zen
        /// <summary>
        /// 半角文字列を全角にする
        /// </summary>
        /// <param name="s">strng : 変換元の文字列</param>
        /// <returns>string</returns>
        public static string Han2Zen(this string s)
        {
            return StringConvert(s, MapFlags.LCMAP_FULLWIDTH | MapFlags.LCMAP_KATAKANA);
        }
        #endregion

        #region **** Extension Method : Zen2Han
        /// <summary>
        /// 全角文字列を半角にする
        /// </summary>
        /// <param name="s">string : 変換元の文字列</param>
        /// <returns>string</returns>
        public static string Zen2Han(this string s)
        {
            return StringConvert(s, MapFlags.LCMAP_HALFWIDTH);
        }
        #endregion

        #region **** Extension Method : toInt
        /// <summary>
        /// 例外を発生させずに文字列を整数に変換する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <returns>int?</returns>
        public static int? ToInt(this string s)
        {
            int n = 0;
            if (int.TryParse(s, out n))
                return n;
            else
                return null;
        }
        #endregion

        #region **** Extension Method : ToDouble
        /// <summary>
        /// 例外を発生させずに文字列を実数に変換する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <returns>double?</returns>
        public static double? ToDouble(this string s)
        {
            double n = 0.0d;
            if (double.TryParse(s, out n))
                return n;
            else
                return null;
        }
        #endregion

        #region **** Extension Method : ToDecimal
        /// <summary>
        /// 例外を発生させずに文字列をデシマルに変換する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <returns>decimal?</returns>
        public static decimal? ToDecimal(this string s)
        {
            decimal n = 0m;
            if (decimal.TryParse(s, out n))
                return n;
            else
                return null;
        }
        #endregion

        #region **** Extension Method : ToDateTime
        /// <summary>
        /// 例外を発生させずに文字列を日時型に変換する
        /// </summary>
        /// <param name="s">string : 文字列</param>
        /// <returns>DateTime?</returns>
        public static DateTime? ToDateTime(this string s)
        {
            DateTime dt;
            if (DateTime.TryParse(s, out dt))
                return dt;
            else
                return null;
        }
        #endregion

        #endregion
    }
    #endregion
}
