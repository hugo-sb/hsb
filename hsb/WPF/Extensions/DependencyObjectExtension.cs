using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF.Extensions
{
    #region 【Static Class : DependencyObjectExtension】
    /// <summary>
    /// DependencyObjectの拡張メソッド
    /// </summary>
    public static class DependencyObjectExtension
    {
        #region ■ Static Methods ■

        #region **** Private Static Method : Walk
        /// <summary>
        /// WalkInChildrenメソッドの本体
        /// </summary>
        /// <param name="obj">DependencyObject : 依存オブジェクト</param>
        /// <param name="act">Action : デリゲート</param>
        private static void Walk(DependencyObject obj, Action<DependencyObject> act)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(obj))
            {
                if (child is DependencyObject)
                {
                    act(child as DependencyObject);
                    Walk(child as DependencyObject, act);
                }
            }
        }
        #endregion

        #endregion

        #region ■ Extension Methods ■

        #region **** Extension Method : WalkInChildren
        /// <summary>
        /// 子オブジェクトに対してデリゲートを実行する
        /// </summary>
        /// <param name="obj">DependencyObject : this</param>
        /// <param name="act">Action : デリゲート</param>
        public static void WalkInChildren(this DependencyObject obj, Action<DependencyObject> act)
        {
            if (act == null)
                throw new ArgumentNullException();

            Walk(obj, act);
        }
        #endregion

        #endregion
    }
    #endregion

}
