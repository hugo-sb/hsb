using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using hsb.WPF;

namespace SampleApp
{
    #region 【Class : App】
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public App()
        {
            // ViewModelとViewを関係づける
            Views = new ViewManager();
            Views.Add(typeof(ViewModels.MainViewModel), typeof(Views.MainWindow));
            Views.Add(typeof(ViewModels.SubViewModel), typeof(Views.SubWindow));
            Views.Add(typeof(ViewModels.PreviewViewModel), typeof(Views.PreviewWindow));
        }
        #endregion

        #region ■ Priperties ■

        #region **** Property : Views (Set private only)
        /// <summary>
        /// ViewManager
        /// </summary>
        public ViewManager Views { get; private set; }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Protected Override Method : OnStartup
        /// <summary>
        /// アプリケーションが実行開始した
        /// </summary>
        /// <param name="e">StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // MainViewModel と対応する View の生成
            var vm = new ViewModels.MainViewModel();
            var wnd = Views.CreateView(vm);

            // View の表示
            wnd.Show();
        }
        #endregion

        #endregion

        #region ■ Static Properties ■

        #region **** Static Property : ViewManager
        /// <summary>
        /// ViewManagerを返す
        /// </summary>
        /// <returns>ViewManager</returns>
        public static ViewManager ViewManager
        {
            get { return (Current as App).Views; }
        }
        #endregion

        #endregion
    }
    #endregion
}
