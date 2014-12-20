using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF.EventArguments
{
    #region 【Class : ShowMessageBoxEventArgs】
    /// <summary>
    /// Viewに対するメッセージボックス表示通知のイベントパラメーター
    /// </summary>
    public class ShowMessageBoxEventArgs : EventArgs
    {
        #region ■　Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="caption">メッセージボックスのタイトル : string</param>
        /// <param name="message">メッセージボックスに表示されるテキスト : string</param>
        public ShowMessageBoxEventArgs(string caption, string message, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None)
        {
            Caption = caption;
            Message = message;
            Buttons = buttons;
            Icon = icon;
            Result = MessageBoxResult.OK;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Caption (Set private only)
        /// <summary>
        /// メッセージボックスのタイトル
        /// </summary>
        public string Caption { get; private set; }
        #endregion

        #region **** Property : Message (Set private only)
        /// <summary>
        /// メッセージボックスに表示するテキスト
        /// </summary>
        public string Message { get; private set; }
        #endregion

        #region **** Property : Buttons (Set private only)
        /// <summary>
        ///  メッセージボックスに表示されるボタン
        /// </summary>
        public MessageBoxButton Buttons { get; private set; }
        #endregion

        #region **** Property : Icon (Set private only)
        /// <summary>
        /// メッセージボックスに表示されるアイコン
        /// </summary>
        public MessageBoxImage Icon { get; private set; }
        #endregion

        #region **** Property : Result
        /// <summary>
        /// メッセージボックスからの戻り値
        ///     View側のイベントハンドラで値を設定する
        /// </summary>
        public MessageBoxResult Result { get; set; }
        #endregion

        #endregion
    }
    #endregion
}
