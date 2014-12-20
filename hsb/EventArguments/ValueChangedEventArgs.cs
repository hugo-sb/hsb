using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.EventArguments
{
    #region 【Class ValueChangedEventArgs】
    /// <summary>
    /// 値変更後イベントパラメータ
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="newValue">変更後の値 : object</param>
        /// <param name="oldValue">変更前の値 : object</param>
        public ValueChangedEventArgs(object newValue, object oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : NewValue (Set protected only)
        /// <summary>
        /// 変更後の値
        /// </summary>
        public object NewValue { get; protected set; }
        #endregion

        #region **** Property : OldValue (Set protected only)
        /// <summary>
        /// 変更前の値 : object
        /// </summary>
        public object OldValue { get; protected set; }
        #endregion

        #endregion
    }
    #endregion

}
