using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF
{
    #region 【Class : ViewManager】
    /// <summary>
    /// Viewマネージャー
    /// 　ViewとViewModelの紐づけを管理する
    /// </summary>
    public class ViewManager
    {
        #region ■ Constructor ■

        #region **** Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public ViewManager()
        {
            ViewModel2View = new Dictionary<Type, Type>();
        }
        #endregion

        #region **** Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="vm2v">IEnumerable of ViewModel and View Pair : ViewModelとViewのペアの列挙</param>
        public ViewManager(IEnumerable<Tuple<Type, Type>> vm2v)
            : this()
        {
            Add(vm2v);
        }
        #endregion

        #endregion

        #region ■ Members ■

        private Dictionary<Type, Type> ViewModel2View;

        #endregion

        #region ■ Methods ■

        #region **** Method : Add(1)
        /// <summary>
        /// ViewModelとViewの関連を追加する(1)
        /// </summary>
        /// <param name="viewModel">Type : ViewModelの型</param>
        /// <param name="view">Type : Viewの型</param>
        public void Add(Type viewModel, Type view)
        {
            if (ViewModel2View.ContainsKey(viewModel))
                ViewModel2View[viewModel] = view;
            else
                ViewModel2View.Add(viewModel, view);
        }
        #endregion

        #region **** Method : Add(2)
        /// <summary>
        /// ViewModelとViewの関連を追加する(1)
        /// </summary>
        /// <param name="vm2v">IEnumerable of ViewModel and View Pair : ViewModelとViewのペアの列挙</param>
        public void Add(IEnumerable<Tuple<Type, Type>> vm2v)
        {
            foreach (var pair in vm2v)
                Add(pair.Item1, pair.Item2);
        }
        #endregion

        #region **** Method : CreateView
        /// <summary>
        /// 指定したViewModelに対応するViewを生成する
        /// </summary>
        /// <param name="viewModel">ViewModelBase : ViewModel</param>
        /// <returns>View</returns>
        public Window CreateView(ViewModelBase viewModel)
        {
            // ViewModel に対応する Viewが存在する？
            if (ViewModel2View.ContainsKey(viewModel.GetType()))
            {
                // View を生成し、DataContext に ViewModel を設定する
                var viewType = ViewModel2View[viewModel.GetType()];
                var wnd = Activator.CreateInstance(viewType) as Window;
                if (wnd != null)
                    wnd.DataContext = viewModel;
                return wnd;
            }
            else
                return null;
        }
        #endregion

        #region **** Method : GetView
        /// <summary>
        /// 既に存在するWindowのコレクションから、指定したViewModelに対応するWindowを返す。
        /// </summary>
        /// <param name="viewModel">ViewModelBase : ViewModel</param>
        /// <returns>Window</returns>
        public Window GetView(ViewModelBase viewModel)
        {
            if (ViewModel2View.ContainsKey(viewModel.GetType()))
            {
                var viewType = ViewModel2View[viewModel.GetType()];
                foreach (Window wnd in Application.Current.Windows)
                {
                    if (wnd.GetType() == viewType)
                        return wnd;
                }
            }
            return null;
        }
        #endregion

        #region **** Method : ShowModalView
        /// <summary>
        /// ViewModelから対応するViewをモーダルで表示する
        /// </summary>
        /// <param name="viewModel">ViewModelBase : ViewModel</param>
        /// <param name="ownerViewModel">ViewModelBase : 親となるViewModel</param>
        /// <returns>bool</returns>
        public bool ShowModalView(ViewModelBase viewModel, ViewModelBase ownerViewModel)
        {
            var view = CreateView(viewModel);
            if (view != null)
            {
                view.Owner = GetView(ownerViewModel);
                return (view.ShowDialog() == true);
            }
            else
                return false;
        }
        #endregion

        #region **** Method : ShowView
        /// <summary>
        /// ViewModelから対応するViewをモードレスで表示する
        /// 　※ ここで生成されたViewはコレクションに保持されるのでクローズ時にリムーブする必要がある
        /// </summary>
        /// <param name="viewModel">ViewModelbase : ViewModel</param>
        /// <param name="ownerViewModel">ViewModelBase : 親となるViewModel</param>
        public void ShowView(ViewModelBase viewModel, ViewModelBase ownerViewModel)
        {
            var view = CreateView(viewModel);
            if (view != null)
            {
                view.Owner = GetView(ownerViewModel);
                view.Show();
            }
        }
        #endregion

        #endregion
    }
    #endregion

}
