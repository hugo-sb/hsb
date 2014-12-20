using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Extensions
{
    #region 【Static Class : IEnumerableExtension】
    /// <summary>
    /// IEnumerableインターフェイスの拡張メソッド
    /// </summary>
    public static class IEnumerableExtension
    {
        #region ■ Extension Methods ■

        #region **** Extension Method : JoinString
        /// <summary>
        /// IEnumerable の要素を連結した一つの文字列にする
        /// </summary>
        /// <typeparam name="T">IEnumerableの型</typeparam>
        /// <param name="values">IEnumerable : this</param>
        /// <param name="glue">string : 各要素間を接続する文字列</param>
        /// <param name="converter">Func : 各要素を文字列に変換するデリゲート</param>
        /// <returns>string</returns>
        public static string JoinString<T>(this IEnumerable<T> values, string glue, Func<T, string> converter = null)
        {
            if (converter != null)
                return string.Join(glue, values.Select(converter));
            else
                return string.Join(glue, values);
        }
        #endregion

        #endregion
    }
    #endregion

}
