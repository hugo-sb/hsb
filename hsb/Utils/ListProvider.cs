using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Class : ListProvider】
    /// <summary>
    /// リスト提供クラス
    /// </summary>
    /// <typeparam name="T">リストの型</typeparam>
    public class ListProvider<T>
    {
        #region ■ Properties ■

        #region **** Property : Items (Set protected only)
        /// <summary>
        /// リスト
        /// </summary>
        public List<T> Items { get; protected set; }
        #endregion

        #endregion

        #region ■ Constructor ■

        #region **** Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public ListProvider()
        {
            Items = null;
        }
        #endregion

        #region **** Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="items">初期化リスト</param>
        public ListProvider(IEnumerable<T> items)
        {
            Items = new List<T>(items);
        }
        #endregion

        #endregion
    }
    #endregion

}
