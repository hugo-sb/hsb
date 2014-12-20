using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace hsb.WPF.Behaviors
{
    #region 【Class : TextBoxBehavior】
    /// <summary>
    /// TextBox用添付ビヘイビア
    ///   SelectAllOnGotFocus プロパティを追加
    /// </summary>
    public class TextBoxBehavior
    {
        #region ■ Register DependencyProperties ■

        #region **** DependencyProperty : SelectAllOnGotFocusProperty
        /// <summary>
        /// SelectAllOnGotFocus 依存プロパティの登録
        ///     bool型 : trueであればフォーカス取得時に全選択状態にする。
        /// </summary>
        public static readonly DependencyProperty SelectAllOnGotFocusProperty =
                DependencyProperty.RegisterAttached(
                        "SelectAllOnGotFocus",
                        typeof(bool),
                        typeof(TextBoxBehavior),
                        new UIPropertyMetadata(false, SelectAllOnGotFocusChanged)
                );
        #endregion

        #endregion

        #region ■ Static Properties ■

        #region **** Dependency Property Getter : GetSelectAllOnGotFocus
        /// <summary>
        /// SelectAllOnGotFocusプロパティへのゲッター
        /// </summary>
        /// <param name="obj">DependencyObject : 依存プロパティ</param>
        /// <returns>bool</returns>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static bool GetSelectAllOnGotFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectAllOnGotFocusProperty);
        }
        #endregion

        #region **** Dependency Property Setter : SetSelectAllOnGotFocus
        /// <summary>
        /// SelectAllOnGotFocusプロパティへのセッター
        /// </summary>
        /// <param name="obj">DependencyObject : 依存プロパティ</param>
        /// <param name="value">bool : 値</param>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static void SetSelectAllOnGotFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectAllOnGotFocusProperty, value);
        }
        #endregion

        #endregion

        #region ■ Static Methods ■

        #region **** Dependency Property Callback : SelectAllOnGotFocusChanged
        /// <summary>
        /// 依存プロパティ変更時のコールバック関数
        /// </summary>
        /// <param name="sender">DependencyObject : 依存プロパティ</param>
        /// <param name="e">DependencyPropertyChangedEventArgs : イベントパラメータ</param>
        private static void SelectAllOnGotFocusChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.GotFocus -= OnTextBoxGotFocus;
                if ((bool)e.NewValue)
                    textBox.GotFocus += OnTextBoxGotFocus;
            }
        }
        #endregion

        #region **** Static Method : OnTextBoxGotFocus
        /// <summary>
        /// TextBoxのGotFocusイベントにセットされるハンドラ
        /// </summary>
        /// <param name="sender">object : レシーバー</param>
        /// <param name="e">RoutedEventArgs : イベントパラメータ</param>
        private static void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
                textBox.Dispatcher.BeginInvoke((Action)(() => textBox.SelectAll()));
        }
        #endregion

        #endregion
    }
    #endregion
}
