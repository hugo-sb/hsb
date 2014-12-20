using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF.DataConverters
{
    #region 【Class : Bool2Visibility】
    /// <summary>
    /// bool <=> Visibility
    /// </summary>
    public class Bool2Visibility : DataConverterBase<bool, Visibility>
    {
        public Bool2Visibility()
        {
            Convert = (v, o) => v ? Visibility.Visible : Visibility.Hidden;
            ConvertBack = (v, o) => (v == Visibility.Visible);
        }
    }
    #endregion
}
