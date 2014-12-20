using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using hsb.WPF.EventArguments;
using hsb.WPF.Utils;

namespace hsb.WPF
{
    #region 【Class : ViewModelBase】
    /// <summary>
    /// ViewModlelベースクラス
    /// </summary>
    public class ViewModelBase : DataBindModelBase
    {
        #region ■ Inner Classes ■

        #region 【Inner Class : CanCloseViewArgs】
        /// <summary>
        /// CanCloseView用のパラメーター
        /// </summary>
        public class CanCloseViewArgs
        {
            #region ■ Constructor ■
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="isModel">bool : モーダルウィンドウか？</param>
            /// <param name="dialogResult">bool? : DialogResult</param>
            public CanCloseViewArgs(bool isModel, bool? dialogResult)
            {
                IsModel = isModel;
                DialogResult = dialogResult;
            }
            #endregion

            #region ■ Properties ■

            #region **** Property : IsModel (Set private only)
            /// <summary>
            /// Viewから渡されるパラメーター
            ///     Viewがモーダルであった場合にtrueとなる : bool
            /// </summary>
            public bool IsModel { get; private set; }
            #endregion

            #region **** Property : DialogResult (Set private only)
            /// <summary>
            /// Viewから渡されるパラメーター
            ///     Viewがモーダルであった場合に、戻り値として設定されるDialogResult値 : bool?
            /// </summary>
            public bool? DialogResult { get; private set; }
            #endregion

            #endregion
        }
        #endregion

        #region 【Inner Class : ClosedViewArgs】
        /// <summary>
        /// ClosedViewArgs用のパラメーター
        /// </summary>
        public class ClosedViewArgs
        {
            #region ■ Constructor ■
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="isModel">bool : モーダルウィンドウか？</param>
            /// <param name="dialogResult">bool? : DialogResult</param>
            public ClosedViewArgs(bool isModel, bool? dialogResult)
            {
                IsModel = isModel;
                DialogResult = dialogResult;
            }
            #endregion

            #region ■ Properties ■

            #region **** Property : IsModel (Set private only)
            /// <summary>
            /// Viewから渡されるパラメーター
            ///     Viewがモーダルであった場合にtrueとなる : bool
            /// </summary>
            public bool IsModel { get; private set; }
            #endregion

            #region **** Property : DialogResult (Set private only)
            /// <summary>
            /// Viewから渡されるパラメーター
            ///     Viewがモーダルであった場合に、戻り値として設定されるDialogResult値 : bool?
            /// </summary>
            public bool? DialogResult { get; private set; }
            #endregion

            #endregion
        }
        #endregion

        #endregion

        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewModelBase()
            : base()
        {
            // クロースビューコマンドの生成
            CloseViewCommand = CreateCommand(CloseViewCommandImplement, CanExecuteCloseViewCommand);
        }
        #endregion

        #region ■ Event / Delegate ■

        /// <summary>
        /// Viewに対するクローズ通知
        /// </summary>
        public event EventHandler<CloseViewEventArgs> CloseView;

        /// <summary>
        /// Viewに対するメッセージボックス表示通知
        /// </summary>
        public event EventHandler<ShowMessageBoxEventArgs> ShowMessageBox;

        #endregion

        #region ■ Properties ■

        #region **** Property : CloseViewCommand (Set private only)
        /// <summary>
        /// クローズコマンド
        /// </summary>
        public DelegateCommand CloseViewCommand { get; private set; }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : InvokeCloseView
        /// <summary>
        /// Viewに対してクローズを通知する
        ///     正常にViewを閉じられた場合はTrueを返す。
        /// </summary>
        /// <param name="dialogResult">モーダルウィンドウ時のdialogResult値 : bool</param>
        /// <returns>結果値 : bool</returns>
        public bool InvokeCloseView(bool dialogResult = true)
        {
            if (CloseView != null)
            {
                var e = new CloseViewEventArgs(dialogResult);
                CloseView(this, e);
                return e.Success;
            }
            else
                return false;
        }
        #endregion

        #region **** Method : InvokeShowMessageBox
        /// <summary>
        /// ShowMessageBoxイベントを呼び出す。 
        /// </summary>
        /// <param name="caption">string : メッセージボックスのタイトル</param>
        /// <param name="message">string : メッセージボックスに表示されるテキスト</param>
        /// <param name="buttons">MessageBoxButton : メッセージボックスに表示されるボタン</param>
        /// <param name="icon">MessageBoxImage : メッセージボックスに表示されるアイコン</param>
        /// <returns>MessageBoxResult : メッセージボックスの戻り値</returns>
        public MessageBoxResult InvokeShowMessageBox(string caption, string message,
                                                     MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None)
        {
            if (ShowMessageBox != null)
            {
                var e = new ShowMessageBoxEventArgs(caption, message, buttons, icon);
                ShowMessageBox(this, e);
                return e.Result;
            }
            else
                return MessageBoxResult.None;
        }
        #endregion

        #region **** Method : UserConfirm
        /// <summary>
        /// ユーザーからの確認入力
        ///     メッセージに対して肯定ならTrue、それ以外ならFalseを返す。
        /// </summary>
        /// <param name="caption">string : キャプション</param>
        /// <param name="message">string : メッセージ</param>
        /// <param name="buttons">MessageBoxButton : ダイアログに表示するボタン</param>
        /// <param name="icon">MessageBoxImage : ダイアログに表示されるアイコン</param>
        /// <returns>bool : Yes か OK なら True / それ以外は False</returns>
        public virtual bool UserConfirm(string caption, string message, MessageBoxButton buttons = MessageBoxButton.YesNo,
                                        MessageBoxImage icon = MessageBoxImage.Information)
        {
            return Etc.IsMessageBoxResultPositive(InvokeShowMessageBox(caption, message, buttons, icon));
        }
        #endregion

        #region **** Method : Alert
        /// <summary>
        /// アラートを表示させる
        /// </summary>
        /// <param name="caption">string : キャプション</param>
        /// <param name="message">string : メッセージ</param>
        /// <param name="icon">MessageBoxImage : ダイアログに表示されるアイコン</param>
        public void Alert(string caption, string message, MessageBoxImage icon = MessageBoxImage.Information)
        {
            InvokeShowMessageBox(caption, message, MessageBoxButton.OK, icon);
        }
        #endregion

        #region ***** Protected Virtual Method : CloseViewCommandImplement
        /// <summary>
        /// CloseViewCommandの実体
        /// </summary>
        /// <param name="o">object : コマンドパラメーター</param>
        protected virtual void CloseViewCommandImplement(object o)
        {
            InvokeCloseView((o is bool) ? (bool)o : true);
        }
        #endregion

        #region ***** Protected Virtual Method : CanExecuteCloseViewCommand
        /// <summary>
        /// CloseViewCommandの実行可否
        /// </summary>
        /// <param name="o">object : コマンドパラメーター</param>
        /// <returns>bool</returns>
        protected virtual bool CanExecuteCloseViewCommand(object o)
        {
            return true;
        }
        #endregion

        #region **** Virtual Method : CanCloseView
        /// <summary>
        /// Viewを閉じても良いか？
        ///     ViewでWindowExtension.Setup()が呼ばれた場合、Window.Closing()イベント内からコールされる。
        ///     閉じて良ければtureを返す。
        /// </summary>
        /// <param name="arg">Viewから状態通知パラメーター</param>
        /// <returns>戻り値 : bool</returns>
        public virtual bool CanCloseView(CanCloseViewArgs arg)
        {
            return true;
        }
        #endregion

        #region **** Virtual Method : ClosedView
        /// <summary>
        /// Viewが閉じられた
        ///     ViewでWindowExtension.Setup()が呼ばれた場合、Window.Closed()イベント内からコールされる。
        /// </summary>
        public virtual void ClosedView(ClosedViewArgs arg)
        {
            // イベントハンドラをクリアする
            CloseView = null;
            ShowMessageBox = null;
        }
        #endregion

        #endregion

    }
    #endregion

}
