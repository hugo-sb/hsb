using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using hsb.WPF.Extensions;

namespace SampleApp.Views
{
    #region 【Class : MainWindow】
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            // 拡張メソッドによる初期化処理を最初に呼び出す
            this.Setup();
            InitializeComponent();

            // 書籍リストをダブルクリックされた時の処理
            BookList.MouseDoubleClick += (o, e) =>
                {
                    var vm = DataContext as ViewModels.MainViewModel;
                    if (vm != null)
                    {
                        vm.EditBookCommand.Execute(null);
                    }
                };
        }
        #endregion
    }
    #endregion
}
