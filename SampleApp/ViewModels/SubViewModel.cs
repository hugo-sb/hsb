using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using hsb.WPF;

namespace SampleApp.ViewModels
{
    #region 【Class : SubViewModel】
    /// <summary>
    /// SubViewModel
    /// </summary>
    class SubViewModel : ViewModelBase
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="book"></param>
        public SubViewModel(Models.Book book)
        {
            // プロパティ値の初期設定
            BookProperty = CreateDataBindProperty<Models.Book>("Book", book ?? new Models.Book());

            // コマンドの初期設定
            UpdateBookCommand = CreateCommand((book == null) ? "登　録" : "更　新", 
                                              UpdateBook, 
                                              v => Book.IsChanged && !Book.HasError, 
                                              (book == null) ? "書籍情報を登録します。" : "書籍情報を更新します。");
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Book
        /// <summary>
        /// 書籍
        /// </summary>
        public DataBindPropertyItem<Models.Book> BookProperty { get; private set; }
        public Models.Book Book
        {
            get { return BookProperty.Value; }
            set { BookProperty.Value = value; }
        }
        #endregion

        #endregion

        #region ■ Commands ■

        #region **** Property : UpdateCommand (Set private only)
        /// <summary>
        /// 更新コマンド
        /// </summary>
        public DelegateCommand UpdateBookCommand { get; private set; }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : CanCloseView
        /// <summary>
        /// ViewをCloseしても良いか？
        /// </summary>
        /// <param name="arg">CanCloseViewArgs</param>
        /// <returns>bool</returns>
        public override bool CanCloseView(CanCloseViewArgs arg)
        {
            return arg.DialogResult == true ||
                   !Book.IsChanged ||
                   UserConfirm("確認", "変更内容を破棄しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        #endregion

        #region **** Method : ClosedView
        /// <summary>
        /// Viewが閉じられた
        /// </summary>
        /// <param name="arg">ClosedViewArgs</param>
        public override void ClosedView(ClosedViewArgs arg)
        {
            // キャンセルされた場合、編集内容を破棄する
            if (arg.DialogResult != true)
                Book.Reset();
            base.ClosedView(arg);
        }
        #endregion

        #region ***** Private Method : UpdateBook
        /// <summary>
        /// 書籍情報を更新する
        /// </summary>
        /// <param name="v">Object</param>
        private void UpdateBook(object v)
        {
            if (Book.ValidationCheck())
            {
                if (UserConfirm("確認", "書籍情報を更新しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    // 更新処理
                    Book.Update();
                    InvokeCloseView(true);
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
