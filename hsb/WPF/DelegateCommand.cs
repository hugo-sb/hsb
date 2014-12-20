using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace hsb.WPF
{
    #region 【Class : DelegateCommand】
    /// <summary>
    /// ICommand の実装
    /// 　実際の処理はデリゲートに移譲
    /// </summary>
    public class DelegateCommand : ICommand, INotifyPropertyChanged
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="command">Action : ICommand.Executeのデリゲート</param>
        /// <param name="canExecute">Func : ICommand.CanExecuteのデリゲート</param>
        public DelegateCommand(string name, Action<object> command, Func<object, bool> canExecute, string desc = null)
        {
            if (command == null)
                throw new ArgumentNullException();

            Name = name;
            Command = command;
            CanExecute = canExecute;
            Description = desc;
            IsEnabled = true;
        }
        #endregion

        #region ■ Event / Delegate ■

        #region **** Delegate : Command
        /// <summary>
        /// ICommand.Executeのデリゲート
        /// </summary>
        private Action<object> Command;
        #endregion

        #region **** Delegate : CanExecute
        /// <summary>
        /// ICommand.CanExecuteのデリゲート
        ///     実行可能な場合、Trueを返す
        /// </summary>
        private Func<object, bool> CanExecute;
        #endregion

        #region **** Event : CanExecuteChanged
        /// <summary>
        /// ICommand.CanExecuteChanged の実装 
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region **** Event : PropertyChanged
        /// <summary>
        /// INotifyPropertyChanged.PropertyChanged の実装 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #endregion

        #region ■ Members ■

        private string _Name = "";
        private string _Description = "";
        private bool _IsEnabled = true;

        #endregion

        #region ■ Properties ■

        #region **** Property : Name
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        #endregion

        #region **** Property : Description
        /// <summary>
        /// 説明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
        #endregion

        #region **** Property : IsEnabled
        /// <summary>
        /// コマンドは有効？
        /// </summary>
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled != value)
                {
                    _IsEnabled = value;
                    RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : Execute
        /// <summary>
        /// コマンドの実行
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        public void Execute(object parameter)
        {
            Command(parameter);
        }
        #endregion

        #region **** Method : ICommand.Execute
        /// <summary>
        /// ICommand.Executeの実装 
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        void ICommand.Execute(object parameter)
        {
            Command(parameter);
        }
        #endregion

        #region **** Method : ICommand.CanExecute
        /// <summary>
        /// ICommand.Executeの実装 
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        /// <returns>bool : 実行可否</returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (IsEnabled)
                return (CanExecute != null) ? CanExecute(parameter) : true;
            else
                return false;
        }
        #endregion

        #region **** Protected Method : RaisePropertyChanged
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion

        #region ■　Static Methods ■

        #region **** Static Method : RaiseCanExecuteChanged
        /// <summary>
        /// CanExecuteの状態が変更されたことを通知する
        /// </summary>
        public static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion


        #endregion
    }
    #endregion

}
