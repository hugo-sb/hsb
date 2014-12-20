using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Static Class : EnumUtil】
    /// <summary>
    /// Enum関連ユーティリティ
    /// </summary>
    /// <typeparam name="T">Enumの型</typeparam>
    public static class EnumUtil<T> where T : IComparable
    {
        #region ■ Static Methods ■

        #region **** Static Method : GetValues
        /// <summary>
        /// 定義済みのEnum値を列挙する
        /// </summary>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> GetValues()
        {
            return Enum.GetValues(typeof(T)) as IEnumerable<T>;
        }
        #endregion

        #region **** Static Method : GetList
        /// <summary>
        /// 定義済みのEnum値を列挙したリストを取得する
        /// </summary>
        /// <returns>List of Enum Value</returns>
        public static List<T> GetList()
        {
            return new List<T>(GetValues());
        }
        #endregion

        #region *** Static Method : GetEnumWithDisplayNameList
        /// <summary>
        /// Enum値をIDとするIdWithDisplayNameのリストを取得する
        /// </summary>
        /// <param name="func">Func : Enum値を文字列に変換するデリゲート</param>
        /// <returns>List of IdWithDisplayName</returns>
        public static List<IdWithDisplayName<T>> GetEnumWithDisplayNameList(Func<T, string> func = null)
        {
            return new List<IdWithDisplayName<T>>(GetList().Select(v => new IdWithDisplayName<T>(v, (func != null) ? func(v) : v.ToString())));
        }
        #endregion

        #region **** Static Method : IsDefined
        /// <summary>
        /// 整数値が enum で定義済みかどうか？ 
        /// </summary>
        /// <param name="n">int : 数値</param>
        /// <returns>bool</returns>
        public static bool IsDefined(int n)
        {
            return Enum.IsDefined(typeof(T), n);
        }
        #endregion

        #endregion
    }
    #endregion
}
