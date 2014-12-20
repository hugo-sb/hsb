using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.EventArguments
{
    #region 【Class : ValueChangingEventArgs】
    /// <summary>
    /// 値変更前イベントパラメータ
    /// </summary>
    public class ValueChangingEventArgs : ValueChangedEventArgs
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="newValue">変更後の値 : object</param>
        /// <param name="oldValue">変更前の値 : object</param>
        public ValueChangingEventArgs(object newValue, object oldValue)
            : base(newValue, oldValue)
        {
            Cancel = false;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Cancel
        /// <summary>
        /// キャンセル区分
        ///     イベントハンドラ側で True にセットされると、値の変更処理がキャンセルされる。
        /// </summary>
        public bool Cancel { get; set; }
        #endregion

        #endregion
    }
    #endregion

}
