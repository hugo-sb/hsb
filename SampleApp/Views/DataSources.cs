using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Utils;
using SampleApp.Models;

namespace SampleApp.Views
{
    #region 【Class : BookCategoriesSource】
    /// <summary>
    /// BookCategories ID with Name List Provider
    /// </summary>
    class BookCategoriesSource : ListProvider<IdWithDisplayName<BookCategories>>
    {
        public BookCategoriesSource()
        {
            Items = EnumUtil<BookCategories>.GetEnumWithDisplayNameList(v => v.ToDisplayName());
        }
    }
    #endregion

    #region 【Class : ReviewPointsSource】
    /// <summary>
    /// ReviewPoints with Name List Provider
    /// </summary>
    class ReviewPointsSource : ListProvider<string>
    {
        public ReviewPointsSource()
        {
            Items = new List<string> { "", "★", "★★", "★★★", "★★★★", "★★★★★" };
        }
    }
    #endregion
}
