using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF
{
    #region 【Class : DataBindModelBase】
    /// <summary>
    /// DataBindModelベースクラス
    /// </summary>
    public class DataBindModelBase : PropertySet
    {
        #region ■ Methods ■

        #region **** Method : CreateDataBindProperty(1)
        /// <summary>
        /// データバインド項目を生成する(1)
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="name">string : 項目名</param>
        /// <param name="value">T : 値</param>
        /// <param name="acceptInvalidValue">bool : 不正な値を受け入れる？</param>
        /// <param name="isReadOnly">bool : 読み込み専用？</param>
        /// <returns>DataBindPropertyItem</returns>
        protected DataBindPropertyItem<T> CreateDataBindProperty<T>(string name, T value, bool acceptInvalidValue = true, bool isReadOnly = false)
        {
            var property = new DataBindPropertyItem<T>(name, value, acceptInvalidValue, isReadOnly);
            AddProperty(property);
            return property;
        }
        #endregion

        #region **** Method : CreateDataBindProperty(2)
        /// <summary>
        /// データバインド項目を生成する(2)
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="name">string : 項目名</param>
        /// <param name="value">T : 値</param>
        /// <param name="act">Action : 初期設定デリゲート</param>
        /// <returns>DataBindPropertyItem</returns>
        protected DataBindPropertyItem<T> CreateDataBindProperty<T>(string name, T value, Action<DataBindPropertyItem<T>> act)
        {
            var property = new DataBindPropertyItem<T>(name, value, false, false);
            act(property);
            AddProperty(property);
            return property;
        }
        #endregion

        #region **** Method : CreateCommand(1)
        /// <summary>
        /// コマンドを生成する
        /// </summary>
        /// <returns>DelegateCommand</returns>
        protected DelegateCommand CreateCommand(string name, Action<object> command, Func<object, bool> canExecute = null, string desc = null)
        {
            return new DelegateCommand(name, command, canExecute, desc);
        }
        #endregion

        #region **** Method : CreateCommand(2)
        /// <summary>
        /// コマンドを生成する
        /// </summary>
        /// <returns>DelegateCommand</returns>
        protected DelegateCommand CreateCommand(Action<object> command, Func<object, bool> canExecute = null)
        {
            return new DelegateCommand(null, command, canExecute, null);
        }
        #endregion

        #endregion
    }
    #endregion

}
