using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using hsb.Utils;
using hsb.EventArguments;

namespace hsb.WPF
{
    #region 【Abstract Class : PropertyItemBase】
    /// <summary>
    /// プロパティ項目抽象クラス
    /// </summary>
    public abstract class PropertyItemBase : IDataErrorInfo, INotifyPropertyChanged
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemBase()
        {
            // Got nothing to do...
        }
        #endregion

        #region ■ Events / Delegate ■

        #region **** Event : PropertyChanged
        /// <summary>
        /// INotifyPropertyChanged.PropertyChanged の実装 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region **** Event : ValidationChecked
        /// <summary>
        /// バリデーションチェック後イベント 
        /// </summary>
        public event EventHandler<ValidationCheckedEventArgs> ValidationChecked;
        #endregion

        #region **** Event : Invalidated
        /// <summary>
        /// インバリッドイベント
        /// </summary>
        public event EventHandler<ValidationCheckedEventArgs> Invalidated;
        #endregion

        #region **** Event : ValueChanging
        /// <summary>
        /// 値変更前イベント
        /// </summary>
        public event EventHandler<ValueChangingEventArgs> ValueChanging;
        #endregion

        #region **** Event : ValueChanged
        /// <summary>
        /// 値変更後イベント
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        #endregion

        #endregion

        #region ■ Members ■

        private string _Name = null;
        private bool _AcceptInvalidValue = true;
        private bool _IsChanged = false;
        private string _ErrorMessage = null;
        private bool _IsReadOnly = false;
        private object _Tag = null;
        private string _Description = null;

        #endregion

        #region ■ Properties ■

        #region **** Property : Name (Set protected only)
        /// <summary>
        /// フィールド名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            protected set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        #endregion

        #region **** Property : AcceptInvalidValue
        /// <summary>
        /// 不正な値を受け付ける？
        ///    Trueの場合、ValidationCheckでエラーになった値もValueへの代入を認める
        /// </summary>
        public bool AcceptInvalidValue
        {
            get { return _AcceptInvalidValue; }
            set
            {
                if (_AcceptInvalidValue != value)
                {
                    _AcceptInvalidValue = value;
                    RaisePropertyChanged("AcceptInvalidValue");
                }
            }
        }
        #endregion

        #region **** Property : IsChanged (Set protected only)
        /// <summary>
        /// フィールドが変更された
        /// </summary>
        public bool IsChanged
        {
            get { return _IsChanged; }
            protected set
            {
                if (_IsChanged != value)
                {
                    _IsChanged = value;
                    RaisePropertyChanged("IsChanged");
                }
            }
        }
        #endregion

        #region **** Property : ErrorMessage (Set private only)
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            private set
            {
                if (_ErrorMessage != value)
                {
                    _ErrorMessage = value;
                    RaisePropertyChanged("ErrorMessage");
                    RaisePropertyChanged("HasError");
                }
            }
        }
        #endregion

        #region **** Property : HasError (Get Only)
        /// <summary>
        /// エラーが存在する？
        /// </summary>
        public bool HasError { get { return !string.IsNullOrEmpty(ErrorMessage); } }
        #endregion

        #region **** Property : IsReadOnly
        /// <summary>
        /// 読み取り専用
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    RaisePropertyChanged("IsReadOnly");
                }
            }
        }
        #endregion

        #region **** Property : Tag
        /// <summary>
        /// タグ
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set
            {
                if (_Tag != value)
                {
                    _Tag = value;
                    RaisePropertyChanged("Tag");
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

        #region **** Property : IDataErrorInfo.Error (Get Only)
        /// <summary>
        /// IDataErrorInfo.Error の実装
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { return ErrorMessage; }
        }
        #endregion

        #region **** Property : IDataErrorInfo.Item (Get Only)
        /// <summary>
        /// IDataErrorInfo.Item の実装
        /// </summary>
        /// <param name="columnName">string : フィールド名</param>
        /// <returns></returns>
        string IDataErrorInfo.this[string columnName]
        {
            get { return (columnName == "Value") ? ErrorMessage : null; }
        }
        #endregion

        #endregion

        #region ■ Methods ■

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

        #region **** Abstract Method : GetValue
        /// <summary>
        /// 値を取得する（抽象）
        /// </summary>
        /// <returns>object</returns>
        public abstract object GetValue();
        #endregion

        #region **** Abstract Method : SetValue
        /// <summary>
        /// 値を設定する（抽象）
        /// </summary>
        /// <param name="o"></param>
        public abstract void SetValue(object o);
        #endregion

        #region **** Abstract Method : ValidationCheck
        /// <summary>
        /// バリデーションチェックの実行（抽象）
        /// </summary>
        /// <returns>Result</returns>
        public abstract Result ValidationCheck();
        #endregion

        #region **** Protected Virtual Method : InvokeValidationChecked
        /// <summary>
        /// バリデーションチェック後イベントを発行する
        /// </summary>
        /// <param name="result">Resut : バリデーションチェック結果</param>
        /// <param name="value">object : 値</param>
        protected virtual void InvokeValidationChecked(Result result, object value)
        {
            if (ValidationChecked != null)
                ValidationChecked(this, new ValidationCheckedEventArgs(result, value));
        }
        #endregion

        #region **** Protected Virtual Method : InvokeInvalidated
        /// <summary>
        /// インバリッドイベントを発行する
        /// </summary>
        /// <param name="result">Resut : バリデーションチェック結果</param>
        /// <param name="value">object : 値</param>
        protected virtual void InvokeInvalidated(Result result, object value)
        {
            if (Invalidated != null)
                Invalidated(this, new ValidationCheckedEventArgs(result, value));
        }
        #endregion

        #region **** Protected Virtual Method : InvokeValueChanging
        /// <summary>
        /// 値変更前イベントを発生させる。
        /// </summary>
        /// <param name="e">ValueChangingEventArgs</param>
        protected virtual void InvokeValueChanging(ValueChangingEventArgs e)
        {
            if (ValueChanging != null)
                ValueChanging(this, e);
        }
        #endregion

        #region **** Protected Virtual Method : InvokeValueChanged
        /// <summary>
        /// 値変更後イベントを発生させる。
        /// </summary>
        /// <param name="e">ValueChangedEventArgs</param>
        protected virtual void InvokeValueChanged(ValueChangedEventArgs e)
        {
            if (!IsChanged)
                IsChanged = true;
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
        #endregion

        #region **** Protected Method : SetError(1)
        /// <summary>
        /// エラーメッセージのセット
        /// </summary>
        /// <param name="s"></param>
        protected virtual void SetError(string s)
        {
            ErrorMessage = s;
        }
        #endregion

        #region **** Protected Method : SetError(2)
        /// <summary>
        /// 戻り値よりエラーメッセージのセット(2)
        /// </summary>
        /// <param name="result">Result</param>
        protected void SetError(Result result)
        {
            SetError((result.Failed) ? result.ErrorMessage : null);
        }
        #endregion

        #region **** Method : Clean
        /// <summary>
        /// 状態（変更済・エラー）をクリアーする
        /// </summary>
        public void Clean()
        {
            IsChanged = false;
            SetError((string)null);
        }
        #endregion

        #region **** Method : Reset
        /// <summary>
        /// 値をリセットする（値を初期値に戻す）
        /// </summary>
        public virtual void Reset()
        {
            Clean();
        }
        #endregion

        #region **** Method : ToString
        /// <summary>
        /// 文字列化する
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return GetValue().ToString();
        }
        #endregion

        #endregion
    }
    #endregion


}
