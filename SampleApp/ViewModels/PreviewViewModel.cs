using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;

using hsb.WPF;

namespace SampleApp.ViewModels
{
    #region 【Class : PreviewViewModel】
    /// <summary>
    /// PreviewViewModel
    /// </summary>
    class PreviewViewModel : ViewModelBase
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="doc">XpsDocument</param>
        public PreviewViewModel(XpsDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException();

            SourceProperty = CreateDataBindProperty<IDocumentPaginatorSource>("Source", doc.GetFixedDocumentSequence());
        }
        #endregion

        #region ■ Properties ■

        #region **** Property : Soruce
        /// <summary>
        /// プレビューソース
        /// </summary>
        public DataBindPropertyItem<IDocumentPaginatorSource> SourceProperty { get; private set; }
        public IDocumentPaginatorSource Source
        {
            get { return SourceProperty.Value; }
            set { SourceProperty.Value = value; }
        }
        #endregion

        #endregion
    }
    #endregion
}
