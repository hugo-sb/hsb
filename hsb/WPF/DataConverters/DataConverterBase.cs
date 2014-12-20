using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace hsb.WPF.DataConverters
{
    #region 【Class : DataConverterBase】
    /// <summary>
    /// データーコンバーター用の基底クラス
    /// </summary>
    /// <typeparam name="T1">入力値の型</typeparam>
    /// <typeparam name="T2">出力値の型</typeparam>
    public class DataConverterBase<T1, T2> : IValueConverter
    {
        #region ■ Delegates ■

        protected Func<T1, object, T2> Convert;
        protected Func<T2, object, T1> ConvertBack;

        #endregion

        #region ■ Methods ■

        #region **** Method : IValueConverter.Convert
        /// <summary>
        /// IValueConverter.Convertの実装
        /// </summary>
        /// <param name="value">object : 値</param>
        /// <param name="targetType">Type : 型</param>
        /// <param name="parameter">object : 引数</param>
        /// <param name="culture">CultureInfo : カルチャ情報</param>
        /// <returns>object</returns>
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (Convert != null) ? Convert((T1)value, parameter) : value;
            }
            catch
            {
                return value;
            }
        }
        #endregion

        #region **** Method : ConvertBack
        /// <summary>
        /// IValueConverter.ConvertBackの実装
        /// </summary>
        /// <param name="value">object : 値</param>
        /// <param name="targetType">Type : 型</param>
        /// <param name="parameter">object : 引数</param>
        /// <param name="culture">CultureInfo : カルチャ情報</param>
        /// <returns>object</returns>
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return (ConvertBack != null) ? ConvertBack((T2)value, parameter) : value;
            }
            catch
            {
                return value;
            }
        }
        #endregion

        #endregion
    }
    #endregion

}
