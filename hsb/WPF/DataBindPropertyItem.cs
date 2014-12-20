using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF
{
    #region 【Class : DataBindPropertyItem】
    /// <summary>
    /// ビューモデル用データバインド項目
    /// </summary>
    /// <typeparam name="T">値の型</typeparam>
    public class DataBindPropertyItem<T> : PropertyItem<T>
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">string : フィールド名</param>
        /// <param name="value">T : 初期値</param>
        /// <param name="acceptInvalidValue">bool : 不正な値を受け入れる？</param>
        /// <param name="isReadOnly">bool : 読み込み専用？</param>
        public DataBindPropertyItem(string name, T value, bool acceptInvalidValue, bool isReadOnly)
            : base(name, value, acceptInvalidValue, isReadOnly)
        {
            // Got nothing to do...
        }
        #endregion
    }
    #endregion


}
