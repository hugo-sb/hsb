using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Extensions
{
    #region 【Static Class : ListExtension】
    /// <summary>
    /// Listクラスの拡張メソッド
    /// </summary>
    public static class ListExtension
    {
        #region ■ Extension Methods ■

        #region **** Extension Method : AddRange
        /// <summary>
        /// Listに不定数の要素を挿入する
        /// </summary>
        /// <typeparam name="T">Listの型</typeparam>
        /// <param name="list">リスト : Generic List</param>
        /// <param name="items">リストに追加する要素のparams配列</param>
        public static void AddRange<T>(this List<T> list, params T[] items)
        {
            list.AddRange(items);
        }
        #endregion

        #endregion
    }
    #endregion

}
