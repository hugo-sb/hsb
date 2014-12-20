using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Static Class : IOFilters】
    /// <summary>
    /// IOフィルタ
    /// </summary>
    public static class IOFilters
    {
        #region ■ Static Methods ■

        #region **** Static Method : ToUpper
        /// <summary>
        /// 大文字変換
        /// </summary>
        /// <param name="s">string : 入出力値</param>
        /// <param name="isOut">bool : 出力区分(True : 出力 / False : 入力)</param>
        /// <returns>string</returns>
        public static string ToUpper(string s, bool isOut)
        {
            return isOut ? s : s.ToUpper();
        }
        #endregion

        #region **** Static Method : ToLower
        /// <summary>
        /// 小文字変換
        /// </summary>
        /// <param name="s">string : 入出力値</param>
        /// <param name="isOut">boo: : 出力区分(True : 出力 / False : 入力)</param>
        /// <returns>string</returns>
        public static string ToLower(string s, bool isOut)
        {
            return isOut ? s : s.ToLower();
        }
        #endregion

        #region **** Static Method : Trim
        /// <summary>
        /// 先頭・末尾の空白文字を除去する
        /// </summary>
        /// <param name="s">string : 入出力値</param>
        /// <param name="isOut">boo: : 出力区分(True : 出力 / False : 入力)</param>
        /// <returns>string</returns>
        public static string Trim(string s, bool isOut)
        {
            return isOut ? s : s.Trim();
        }
        #endregion

        #region **** Static Method : TrimStart
        /// <summary>
        /// 先頭の空白文字を除去する
        /// </summary>
        /// <param name="s">string : 入出力値</param>
        /// <param name="isOut">boo: : 出力区分(True : 出力 / False : 入力)</param>
        /// <returns>string</returns>
        public static string TrimStart(string s, bool isOut)
        {
            return isOut ? s : s.TrimStart();
        }
        #endregion

        #region **** Static Method : TrimEnd
        /// <summary>
        /// 末尾の空白文字を除去する
        /// </summary>
        /// <param name="s">string : 入出力値</param>
        /// <param name="isOut">boo: : 出力区分(True : 出力 / False : 入力)</param>
        /// <returns>string</returns>
        public static string TrimEnd(string s, bool isOut)
        {
            return isOut ? s : s.TrimEnd();
        }
        #endregion

        #endregion
    }
    #endregion
}
