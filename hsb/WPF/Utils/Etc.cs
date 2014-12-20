using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF.Utils
{
    #region 【Static Class : Etc】
    /// <summary>
    /// 汎用メソッド定義
    /// </summary>
    public static class Etc
    {
        #region ■ Static Methdos ■

        #region **** Static Method : IsMessageBoxResultPositive
        /// <summary>
        /// MessageBoxの戻り値が肯定かどうか？ 
        ///     OK or Yes なら True、以外は False を返す。
        /// </summary>
        /// <param name="result">MessageBoxResul : メッセージボックスからの戻り値t</param>
        /// <returns>bool</returns>
        public static bool IsMessageBoxResultPositive(MessageBoxResult result)
        {
            return (result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
        }
        #endregion

        #endregion
    }
    #endregion
}
