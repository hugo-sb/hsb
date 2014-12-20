using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Utils;
using hsb.EventArguments;

namespace hsb.WPF
{
    #region 【Abstract Class : PropertyItem】
    /// <summary>
    /// フィールド項目ジェネリック抽象クラス
    /// </summary>
    /// <typeparam name="T">フィールドの型</typeparam>
    public abstract class PropertyItem<T> : PropertyItemBase
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">string : フィールド名</param>
        /// <param name="value">T : 初期値</param>
        /// <param name="acceptInvalidValue">bool : 不正な値を受け入れる？</param>
        /// <param name="isReadOnly">bool : 読み込み専用？</param>
        public PropertyItem(string name, T value, bool acceptInvalidValue, bool isReadOnly)
            : base()
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException();

            Name = name;
            _InitValue = _Value = value;
            AcceptInvalidValue = acceptInvalidValue;
            IsReadOnly = isReadOnly;
        }
        #endregion

        #region ■ Members ■

        protected T _Value;             // 値
        protected T _InitValue;         // 初期値

        #endregion

        #region ■ Event / Delegate ■

        /// <summary>
        /// バリデーションチェック用デリゲート
        /// </summary>
        public Func<T, Result> Validator;

        /// <summary>
        /// IOフィルター用デリゲート
        /// </summary>
        public Func<T, bool, T> IOFilter;

        #endregion

        #region ■ Properties ■

        #region **** Property : Value
        /// <summary>
        /// 値
        /// </summary>
        public T Value
        {
            get { return (IOFilter != null) ? IOFilter(_Value, true) : _Value; }
            set
            {
                if (IsReadOnly)
                    throw new ApplicationException("Can't set value");

                if (IOFilter != null)
                    value = IOFilter(value, false);

                if (!Eq(_Value, value))
                {
                    var result = (Validator != null) ? Validator(value) : Result.Success();
                    SetError(result);
                    InvokeValidationChecked(result, value);
                    if (result.Failed)
                        InvokeInvalidated(result, value);
                    if (result.Successful || AcceptInvalidValue)
                    {
                        var e = new ValueChangingEventArgs(value, Value);
                        InvokeValueChanging(e);
                        if (!e.Cancel)
                        {
                            _Value = value;
                            InvokeValueChanged(e);
                            RaisePropertyChanged("Value");
                        }
                    }
                }
            }
        }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Private Method : Eq
        /// <summary>
        /// 値が一致するか？
        ///     Tに対して直接比較演算子を使用できないためIComparableにキャストして比較する
        ///     IComparableでない場合はつねにFalseを返す。
        /// </summary>
        /// <param name="v1">値1 : T</param>
        /// <param name="v2">値2 : T</param>
        /// <returns>結果値 : bool</returns>
        private bool Eq(T v1, T v2)
        {
            if (v1 is IComparable)
                return (v1 as IComparable).CompareTo(v2) == 0;
            else
                return false;
        }
        #endregion

        #region **** Override Method : GetValue
        /// <summary>
        /// 値を取得する
        /// </summary>
        /// <returns>object</returns>
        public override object GetValue()
        {
            return Value;
        }
        #endregion

        #region **** Override Method : SetValue
        /// <summary>
        /// 値を設定する
        /// 　※値は型Tにキャストして代入される。キャスト不可の場合は例外が発生する。
        /// </summary>
        /// <param name="o">object : 値</param>
        public override void SetValue(object o)
        {
            Value = (T)o;
        }
        #endregion

        #region **** Method : Init
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="initialValue">T : 初期値</param>
        public void Init(T initialValue)
        {
            _InitValue = _Value = initialValue;
            RaisePropertyChanged("Value");
            SetError((string)null);
            IsChanged = false;
        }
        #endregion

        #region **** Method : Reset
        /// <summary>
        /// リセット（値を初期値に戻す）
        /// </summary>
        public override void Reset()
        {
            _Value = _InitValue;
            RaisePropertyChanged("Value");
            SetError((string)null);
            IsChanged = false;
        }
        #endregion

        #region **** Override Method : ValidationCheck(1)
        /// <summary>
        /// バリデーションチェックの実行(1)
        /// </summary>
        /// <returns>Result</returns>
        public override Result ValidationCheck()
        {
            var result = (Validator != null) ? Validator(Value) : Result.Success();
            SetError(result);
            return result;
        }
        #endregion

        #region **** Method : ValidationCheck(2)
        /// <summary>
        /// バリデーションチェックの実行(2)
        /// </summary>
        /// <param name="value">T : バリデーションチェックする値</param>
        /// <returns>Result</returns>
        public Result ValidationCheck(T value)
        {
            return (Validator != null) ? Validator(value) : Result.Success();
        }
        #endregion

        #region **** Method : CreateValidator
        /// <summary>
        /// バリデーターを生成する
        /// </summary>
        /// <param name="func">Func : 値の検証関数</param>
        /// <param name="errorMessage">string : エラーメッセージ</param>
        public void CreateValidator(Func<T, bool> func, string errorMessage)
        {
            Validator = (v) => Result.Create(func(v), errorMessage);
        }
        #endregion

        #region **** Method : ToString
        /// <summary>
        /// 文字列化する
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return _Value.ToString();
        }
        #endregion

        #endregion
    }
    #endregion

}
