using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using hsb.Utils;
using hsb.Extensions;
using hsb.EventArguments;
using hsb.WPF.EventArguments;

namespace hsb.WPF
{
    #region 【Abstract Class : PropertySet】
    /// <summary>
    /// フィールドセット
    /// </summary>
    public abstract class PropertySet : IEnumerable<PropertyItemBase>, IDataErrorInfo, INotifyPropertyChanged
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertySet()
        {
            Properties = new List<PropertyItemBase>();
        }
        #endregion

        #region ■ Events / Delegate ■

        /// <summary>
        /// INotifyPropertyChanged.PropertyChanged の実装 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// フィールドでバリデーションエラーが発生した
        /// </summary>
        public event EventHandler<PropertyItemInvalidatedEventArgs> PropertyItemInvalidated;

        /// <summary>
        /// フィールドでエラーが発生した
        /// </summary>
        public event EventHandler<PropertyItemEventArgs> ErrorRaised;

        /// <summary>
        /// フィールドのエラーが解消した
        /// </summary>
        public event EventHandler<PropertyItemEventArgs> ErrorCleard;

        /// <summary>
        /// バリデーションチェック実行時用のフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> ValidationCheckFilter;

        /// <summary>
        /// 項目の変更通知をモデルに伝播するさいのフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> IsChangedFilter;

        /// <summary>
        /// 項目のエラー通知をモデルに伝播するさいのフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> HasErrorFilter;

        #endregion

        #region ■ Properties ■

        #region *** Protected Property : Properties
        /// <summary>
        /// プロパティリスト
        /// </summary>
        protected List<PropertyItemBase> Properties { get; set; }
        #endregion

        #region **** Property : Item(1) (Get Only)
        /// <summary>
        /// フィールド(1)
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        /// <returns>PropertyItemBase</returns>
        public PropertyItemBase this[string propertyName]
        {
            get
            {
                var property = Properties.Find(v => v.Name == propertyName);
                if (property == null)
                    throw new ArgumentOutOfRangeException();
                return property;
            }
        }
        #endregion

        #region **** Property : Item(2) (Get Only)
        /// <summary>
        /// フィールド(2)
        /// </summary>        
        /// <param name="idx">int : インデックス</param>
        /// <returns>PropertyItemBase</returns>
        public PropertyItemBase this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < Properties.Count)
                    return Properties[idx];
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region **** Virtual Property : IsChanged (Get Only)
        /// <summary>
        /// 何れかのプロパティが変更されたか？
        /// </summary>
        public virtual bool IsChanged
        {
            get { return Properties.Where(v => (IsChangedFilter == null || IsChangedFilter(v)) && v.IsChanged).Any(); }
        }
        #endregion

        #region **** Virtual Property : HasError (Get Only)
        /// <summary>
        /// エラーが存在する？
        /// </summary>
        public virtual bool HasError
        {
            get
            {
                return Properties.Where(v => (HasErrorFilter == null || HasErrorFilter(v)) && v.HasError).Any();
            }
        }
        #endregion

        #region **** Property : Errors
        /// <summary>
        /// エラーのフィールドを返す
        /// </summary>
        /// <returns>IEnumerable of PropertyItemBase</returns>
        public IEnumerable<PropertyItemBase> Errors()
        {
            return Properties.Where(v => v.HasError);
        }
        #endregion

        #region **** Property : IDataErrorInfo.Error (Get Only)
        /// <summary>
        /// IdataErrorInfo.Error の実装
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { return HasError ? "Has Error" : null; }
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
            get
            {
                var property = Properties.Find(v => v.Name == columnName);
                return (property != null) ? property.ErrorMessage : null;
            }
        }
        #endregion

        #region **** Property : PropertyNames (Get Only)
        /// <summary>
        /// プロパティ名のセットを返す。
        /// </summary>
        public IEnumerable<string> PropertyNames
        {
            get { return Properties.Select(property => property.Name); }
        }
        #endregion

        #region **** Property : Values (Get Onlry)
        /// <summary>
        /// 値のセットを返す。
        /// </summary>
        public IEnumerable<object> Values
        {
            get { return Properties.Select(property => property.GetValue()); }
        }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : GetEnumerator
        /// <summary>
        /// 反復処理用の列挙子を返す (Generic)
        /// 　IEnumerable.GetEnumerator() の実装
        /// </summary>
        /// <returns>IEnumerator of PropertyItemBase</returns>
        IEnumerator<PropertyItemBase> IEnumerable<PropertyItemBase>.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        #endregion

        #region **** Method : GetEnumerator
        /// <summary>
        /// 反復処理用の列挙子を返す
        /// 　IEnumerable.GetEnumerator() の実装
        /// </summary>
        /// <returns>IEnumerator of PropertyItemBase</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        #endregion

        #region **** Method : RaisePropertyChanged(1)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region **** Method : RaisePropertyChanged(2)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">IEnumerable of string : プロパティ名</param>
        public void RaisePropertyChanged(IEnumerable<string> propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region **** Method : RaisePropertyChanged(3)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">array of string : プロパティ名</param>
        public void RaisePropertyChanged(params string[] propertyNames)
        {
            RaisePropertyChanged(propertyNames);
        }
        #endregion

        #region **** Method : AddProperty
        /// <summary>
        /// フィールドを追加する
        /// </summary>
        /// <param name="property">PropertyItemBase</param>
        protected void AddProperty(PropertyItemBase property)
        {
            // PropertyChanged を波及させる
            property.PropertyChanged += OwnedPropertyChanged;
            // バリデーションエラーをトラップする
            property.Invalidated += OwnedPropertyInvalidated;
            Properties.Add(property);
        }
        #endregion

        #region **** Method : ValidationCheck
        /// <summary>
        /// バリデーションチェックの実行
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidationCheck()
        {
            var result = true;
            var filter = ValidationCheckFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in Properties.Where(filter))
            {
                var ret = property.ValidationCheck();
                if (ret.Failed && result)
                    result = false;
            }
            return result;
        }
        #endregion

        #region ***** Method : Clean
        /// <summary>
        /// 状態（変更済・エラー）をクリアーする
        /// </summary>
        public void Clean()
        {
            foreach (var property in Properties)
                property.Clean();
        }
        #endregion

        #region **** Method : SetValues
        /// <summary>
        /// 値を一括設定する
        /// </summary>
        /// <param name="properties">IEnumerable of PropertyItemBase : 設定する値のセット</param>
        /// <param name="propertyFilter">Func : フィルター</param>
        public void SetValues(IEnumerable<PropertyItemBase> properties, Func<PropertyItemBase, bool> propertyFilter = null)
        {
            var filter = propertyFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in properties.Where(filter))
            {
                var f = Properties.Find(v => v.Name == property.Name);
                if (f != null)
                    f.SetValue(property.GetValue());
            }
        }
        #endregion

        #region **** Method : Reset
        /// <summary>
        /// プロパティの値を初期値に戻す
        /// </summary>
        /// <param name="propertyFilter"></param>
        public void Reset(Func<PropertyItemBase, bool> propertyFilter = null)
        {
            var filter = propertyFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in Properties.Where(filter))
                property.Reset();
        }
        #endregion

        #endregion

        #region ■ Event Handlers ■

        #region **** Virtual Event Handler : OwnedPropertyChanged
        /// <summary>
        /// フィールドのプロパティ値が変更された
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        protected virtual void OwnedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var property = sender as PropertyItemBase;
            if (property != null)
            {
                if (e.PropertyName == "Value")
                    RaisePropertyChanged(property.Name);

                // モデルに変更を通知する
                if (e.PropertyName == "IsChanged" && (IsChangedFilter == null || IsChangedFilter(property)))
                    RaisePropertyChanged(e.PropertyName);

                // モデルにエラーを通知する
                if (e.PropertyName == "HasError" && (HasErrorFilter == null || HasErrorFilter(property)))
                {
                    RaisePropertyChanged(e.PropertyName);
                    if (ErrorRaised != null && property.HasError)
                        ErrorRaised(this, new PropertyItemEventArgs(property));
                    if (ErrorCleard != null && !property.HasError)
                        ErrorCleard(this, new PropertyItemEventArgs(property));
                }
            }
        }
        #endregion

        #region **** Virtual Event Handler : OwnedPropertyInvalidated
        /// <summary>
        /// プロパティでバリデーションエラーが発生した
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ValidationCheckedEventArgs</param>
        protected virtual void OwnedPropertyInvalidated(object sender, ValidationCheckedEventArgs e)
        {
            var property = sender as PropertyItemBase;
            if (property != null)
            {
                if (PropertyItemInvalidated != null)
                    PropertyItemInvalidated(this, new PropertyItemInvalidatedEventArgs(property, e.ValidationResult));
            }
        }
        #endregion

        #endregion
    }
    #endregion

}
