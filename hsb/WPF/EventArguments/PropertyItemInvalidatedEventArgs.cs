using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Utils;

namespace hsb.WPF.EventArguments
{
    #region 【Class : PropertyItemInvalidatedEventArgs】
    /// <summary>
    /// バリデーションエラー時イベントパラメータ
    /// </summary>
    public class PropertyItemInvalidatedEventArgs : PropertyItemEventArgs
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemInvalidatedEventArgs(PropertyItemBase property, Result result)
            : base(property)
        {
            ValidationResult = result;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : ValidationResult (Set private only)
        /// <summary>
        /// バリデーションチェックの結果
        /// </summary>
        public Result ValidationResult { get; private set; }
        #endregion

        #endregion
    }
    #endregion

}
