using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace hsb.WPF.Extensions
{
    #region 【Static Class : WindowExtension】
    /// <summary>
    /// Window用の拡張メソッド
    /// </summary>
    public static class WindowExtension
    {
        #region ■ Enums ■

        /// <summary>
        /// 初期設定オプション定義
        /// </summary>
        public enum WindowSetupOptions
        {
            SetShowMessageBoxHandler = 0x1,     // ViewModelからのメッセージボックス表示通知のハンドラーを設定する
            SetCloseViewHandler = 0x2,          // ViewModelからのClose要求通知のハンドラーを設定する
            SetAll = 0x3                        // すべてを設定する
        }

        #endregion

        #region ■ Extension Methods ■

        #region **** Static Method : Setup
        /// <summary>
        /// Windowクラスに対する共通初期設定
        /// 　Windowクラスのコンストラクタ内で、InitializeComponentより前に呼び出す。
        /// </summary>
        /// <param name="window">this : Window</param>
        /// <param name="options">初期設定オプションのセット : int</param>
        public static void Setup(this Window window, WindowSetupOptions options = WindowSetupOptions.SetAll)
        {
            // DataContext変更時のイベントハンドラを設定する
            window.DataContextChanged += (o, e) =>
            {
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                {
                    // ShowMessageBoxイベントのハンドラを設定
                    if ((options & WindowSetupOptions.SetShowMessageBoxHandler) == WindowSetupOptions.SetShowMessageBoxHandler)
                    {
                        vm.ShowMessageBox += (sender, arg) =>
                        {
                            arg.Result = MessageBox.Show(arg.Message, arg.Caption, arg.Buttons, arg.Icon);
                        };
                    }

                    // CloseViewイベントのハンドラを設定
                    if ((options & WindowSetupOptions.SetCloseViewHandler) == WindowSetupOptions.SetCloseViewHandler)
                    {
                        vm.CloseView += (sender, arg) =>
                        {
                            // モーダルダイアログならDialogResultを設定、モードレスなら Closeメソッドを呼ぶ
                            if (ComponentDispatcher.IsThreadModal)
                                window.DialogResult = arg.DialogResult;
                            else
                                window.Close();
                        };
                    }
                }
            };

            // ClosingイベントでViewModelのCanCloseViewe()をコールする
            window.Closing += (o, e) =>
            {
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                    e.Cancel = !vm.CanCloseView(new ViewModelBase.CanCloseViewArgs(ComponentDispatcher.IsThreadModal, window.DialogResult));
            };

            // ClosedイベントでViewModelのClosedView()をコールする
            window.Closed += (o, e) =>
            {
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                {
                    vm.ClosedView(new ViewModelBase.ClosedViewArgs(ComponentDispatcher.IsThreadModal, window.DialogResult));
                }
            };

        }
        #endregion

        #endregion
    }
    #endregion

}
