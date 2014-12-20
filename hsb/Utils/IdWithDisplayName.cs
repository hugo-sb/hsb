using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Utils
{
    #region 【Class : IdWithDisplayName】
    /// <summary>
    /// 表示用の名称を持つID値
    /// </summary>
    /// <typeparam name="T">ID値の型</typeparam>
    public class IdWithDisplayName<T> : IComparable<IdWithDisplayName<T>> where T : IComparable
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="id">T : ID値</param>
        /// <param name="displayName">string : 表示用の名称</param>
        public IdWithDisplayName(T id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Id (Set private only)
        /// <summary>
        /// ID値
        /// </summary>
        public T Id { get; private set; }
        #endregion

        #region **** Property : DisplayName (Set private only)
        /// <summary>
        /// 表示用名称
        /// </summary>
        public string DisplayName { get; private set; }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : Equals(1) (Override)
        /// <summary>
        /// 同値判定(1)
        /// </summary>
        /// <param name="obj">object : 比較対象</param>
        /// <returns>bool</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
            {
                var o = obj as IdWithDisplayName<T>;
                if ((object)o != null)
                    return Id.Equals(o.Id);
                else
                    return false;
            }
        }
        #endregion

        #region **** Method : Equals(2) (Override)
        /// <summary>
        /// 同値判定(2)
        /// </summary>
        /// <param name="id">IdWithDisplayName : 比較対象</param>
        /// <returns></returns>
        public bool Equals(IdWithDisplayName<T> id)
        {
            if ((object)id != null)
                return Id.Equals(id.Id);
            else
                return false;
        }
        #endregion

        #region **** Method : GetHashCode (Override)
        /// <summary>
        /// ハッシュ値を取得する
        /// </summary>
        /// <returns>int</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion

        #region **** Method : CompareTo
        /// <summary>
        /// 大小比較メソッド
        /// 　※ IComparableの実装
        /// </summary>
        /// <param name="id">IdWithDisplayName : 比較対象</param>
        /// <returns>int</returns>
        public int CompareTo(IdWithDisplayName<T> id)
        {
            return Id.CompareTo(id.Id);
        }
        #endregion

        #region **** Method : ToString (Override)
        /// <summary>
        /// 文字列化
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return DisplayName ?? Id.ToString();
        }
        #endregion

        #endregion

        #region ■ Operator Override ■

        #region **** Operator : ==(1)
        /// <summary>
        /// 等号演算子(1)
        /// </summary>
        /// <param name="id1">IdWithDisplayName : 値1</param>
        /// <param name="id2">IdWithDisplayName : 値2</param>
        /// <returns>bool</returns>
        public static bool operator ==(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            if (ReferenceEquals(id1, id2))
                return true;
            if ((object)id1 == null || (object)id2 == null)
                return false;
            return id1.Id.Equals(id2.Id);
        }
        #endregion

        #region **** Operator : ==(2)
        /// <summary>
        /// 等号演算子(2)
        /// </summary>
        /// <param name="id1">IdWithDisplayName : 値1</param>
        /// <param name="rawId">T : 値2</param>
        /// <returns>bool</returns>
        public static bool operator ==(IdWithDisplayName<T> id, T rawId)
        {
            if ((object)id == null)
                return false;
            else
                return id.Id.Equals(rawId);
        }
        #endregion

        #region **** Operator : ==(3)
        /// <summary>
        /// 等号演算子(3)
        /// </summary>
        /// <param name="rawId">T : 値1</param>
        /// <param name="id1">IdWithDisplayName : 値2</param>
        /// <returns>bool</returns>
        public static bool operator ==(T rawId, IdWithDisplayName<T> id)
        {
            return (id == rawId);
        }
        #endregion

        #region **** Operator : !=(1)
        /// <summary>
        /// 不等号演算子(1)
        /// </summary>
        /// <param name="id1">IdWithDisplayName : 値1</param>
        /// <param name="id2">IdWithDisplayName : 値2</param>
        /// <returns>bool</returns>
        public static bool operator !=(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            return !(id1 == id2);
        }
        #endregion

        #region **** Operator : !=(2)
        /// <summary>
        /// 不等号演算子(2)
        /// </summary>
        /// <param name="id">IdWithDisplayName : 値1</param>
        /// <param name="rawId">T : 値2</param>
        /// <returns>bool</returns>
        public static bool operator !=(IdWithDisplayName<T> id, T rawId)
        {
            return !(id == rawId);
        }
        #endregion

        #region **** Operator : !=(3)
        /// <summary>
        /// 不等号演算子(3)
        /// </summary>
        /// <param name="rawId">T : 値1</param>
        /// <param name="id">IdWithDisplayName : 値2</param>
        /// <returns>bool</returns>
        public static bool operator !=(T rawId, IdWithDisplayName<T> id)
        {
            return !(id == rawId);
        }
        #endregion

        #region **** Operater : ()
        /// <summary>
        /// Tへのキャスト演算子
        /// </summary>
        /// <param name="id">IdWithDisplayName : 値</param>
        /// <returns>T</returns>
        public static explicit operator T(IdWithDisplayName<T> id)
        {
            return id.Id;
        }
        #endregion

        #region **** Operater : >(1)
        /// <summary>
        /// ＞演算子
        /// </summary>
        /// <param name="id1">IdWithDisplayName</param>
        /// <param name="id2">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator >(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            if (id1 == null || id2 == null)
                throw new ArgumentNullException();

            return id1.CompareTo(id2) > 0;
        }
        #endregion

        #region **** Operater : >(2)
        /// <summary>
        /// ＞演算子
        /// </summary>
        /// <param name="id1">IdWithDisplayName</param>
        /// <param name="rawId">T</param>
        /// <returns>bool</returns>
        public static bool operator >(IdWithDisplayName<T> id, T rawId)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id.Id.CompareTo(rawId) > 0;
        }
        #endregion

        #region **** Operater : >(3)
        /// <summary>
        /// ＞演算子
        /// </summary>
        /// <param name="rawId">T</param>
        /// <param name="id">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator >(T rawId, IdWithDisplayName<T> id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id.Id.CompareTo(rawId) < 0;
        }
        #endregion

        #region **** Operater : >=(1)
        /// <summary>
        /// ≧演算子(1)
        /// </summary>
        /// <param name="id1">IdWithDisplayName</param>
        /// <param name="id2">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator >=(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            if (id1 == null || id2 == null)
                throw new ArgumentNullException();

            return id1 == id2 || id1.CompareTo(id2) > 0;
        }
        #endregion

        #region **** Operater : >=(2)
        /// <summary>
        /// ≧演算子(2)
        /// </summary>
        /// <param name="id">IdWithDisplayName</param>
        /// <param name="rawId">T</param>
        /// <returns>bool</returns>
        public static bool operator >=(IdWithDisplayName<T> id, T rawId)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id == rawId || id.Id.CompareTo(rawId) > 0;
        }
        #endregion

        #region **** Operater : >=(3)
        /// <summary>
        /// ≧演算子(3)
        /// </summary>
        /// <param name="rawId">T</param>
        /// <param name="id">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator >=(T rawId, IdWithDisplayName<T> id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id == rawId || id.Id.CompareTo(rawId) < 0;
        }
        #endregion

        #region **** Operater : <(1)
        /// <summary>
        /// ＜演算子(1)
        /// </summary>
        /// <param name="id1">IdWithDisplayName</param>
        /// <param name="id2">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator <(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            if (id1 == null || id2 == null)
                throw new ArgumentNullException();

            return id1.CompareTo(id2) < 0;
        }
        #endregion

        #region **** Operater : <(2)
        /// <summary>
        /// ＜演算子(2)
        /// </summary>
        /// <param name="id">IdWithDisplayName</param>
        /// <param name="rawId">T</param>
        /// <returns>bool</returns>
        public static bool operator <(IdWithDisplayName<T> id, T rawId)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id.Id.CompareTo(rawId) < 0;
        }
        #endregion

        #region **** Operater : <(3)
        /// <summary>
        /// ＜演算子(3)
        /// </summary>
        /// <param name="rawId">T</param>
        /// <param name="id">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator <(T rawId, IdWithDisplayName<T> id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id.Id.CompareTo(rawId) > 0;
        }
        #endregion

        #region **** Operater : <=(1)
        /// <summary>
        /// ≦演算子(1)
        /// </summary>
        /// <param name="id1">IdWithDisplayName</param>
        /// <param name="id2">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator <=(IdWithDisplayName<T> id1, IdWithDisplayName<T> id2)
        {
            if (id1 == null || id2 == null)
                throw new ArgumentNullException();

            return id1 == id2 || id1.CompareTo(id2) < 0;
        }
        #endregion

        #region **** Operater : <=(2)
        /// <summary>
        /// ≦演算子(2)
        /// </summary>
        /// <param name="id">IdWithDisplayName</param>
        /// <param name="rawId">T</param>
        /// <returns>bool</returns>
        public static bool operator <=(IdWithDisplayName<T> id, T rawId)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id == rawId || id.Id.CompareTo(rawId) < 0;
        }
        #endregion

        #region **** Operater : <=(3)
        /// <summary>
        /// ≦演算子(3)
        /// </summary>
        /// <param name="rawId">T</param>
        /// <param name="id">IdWithDisplayName</param>
        /// <returns>bool</returns>
        public static bool operator <=(T rawId, IdWithDisplayName<T> id)
        {
            if (id == null)
                throw new ArgumentNullException();

            return id == rawId || id.Id.CompareTo(rawId) > 0;
        }
        #endregion
        #endregion
    }
    #endregion
}
