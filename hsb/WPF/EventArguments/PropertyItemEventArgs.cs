using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF.EventArguments
{
    #region 【Class : PropertyItemEventArgs】
    /// <summary>
    /// データバインド項目イベントパラメータ
    /// </summary>
    public class PropertyItemEventArgs : EventArgs
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemEventArgs(PropertyItemBase property)
        {
            Property = Property;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Property (Set private only)
        /// <summary>
        /// フィールド
        /// </summary>
        public PropertyItemBase Property { get; private set; }
        #endregion

        #endregion
    }
    #endregion

}
