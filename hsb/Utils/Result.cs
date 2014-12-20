using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Class : Result】
    /// <summary>
    /// 正否とエラー時のメッセージを保持するクラス
    /// </summary>
    public class Result
    {
        #region ■ Private Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="result">bool : 結果値</param>
        /// <param name="description">string : エラーメッセージ</param>
        private Result(bool result, string errorMessage)
        {
            Value = result;
            ErrorMessage = errorMessage;
        }
        #endregion

        #region ■ Properties ■

        #region **** Private Property : Value
        /// <summary>
        /// 正否の値
        /// </summary>
        private bool Value { get; set; }
        #endregion

        #region **** Property : Successful (Get Only)
        /// <summary>
        /// 成功した
        /// </summary>
        public bool Successful { get { return Value; } }
        #endregion

        #region **** Property : Failed (Get Only)
        /// <summary>
        /// 失敗した
        /// </summary>
        public bool Failed { get { return !Value; } }
        #endregion

        #region **** Property : ErrorMessage (Set private only)
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; private set; }
        #endregion

        #endregion

        #region ■ Static Members ■

        #region **** Static Private Member : _Success
        /// <summary>
        /// 成功値
        /// </summary>
        private static Result _Success = new Result(true, null);
        #endregion

        #endregion

        #region ■ Static Methods ■

        #region **** Static Method : Success
        /// <summary>
        /// 成功値を返す
        /// </summary>
        /// <returns>Result</returns>
        public static Result Success()
        {
            return _Success;
        }
        #endregion

        #region **** Static Method : Fail
        /// <summary>
        /// 失敗値を返す
        /// </summary>
        /// <param name="description">string : 説明文</param>
        /// <returns>Result</returns>
        public static Result Fail(string description)
        {
            return new Result(false, description);
        }
        #endregion

        #region **** Static Method : Create
        /// <summary>
        /// Resultクラスを返す
        /// </summary>
        /// <param name="result">bool : 結果値</param>
        /// <param name="errorMessage">string : エラーメッセージ</param>
        /// <returns>Result</returns>
        public static Result Create(bool result, string errorMessage)
        {
            return result ? _Success : Result.Fail(errorMessage);
        }
        #endregion

        #endregion
    }
    #endregion

}
