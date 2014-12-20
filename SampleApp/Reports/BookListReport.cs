using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.Windows;
using System.Windows.Media;

using hsb.WPF;
using SampleApp.Models;

namespace SampleApp.Reports
{
    #region 【Class : BookListReport】
    /// <summary>
    /// 書籍一覧レポート
    /// </summary>
    class BookListReport : Report
    {
        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="boolList">List of Book</param>
        public BookListReport(IEnumerable<Models.Book> books)
            : base()
        {
            BookList = new List<Models.Book>(books);
            DataRowIndex = 0;

            // 用紙サイズの設定
            PageMediaSize = PageMediaSizeName.ISOA4Rotated;
            PageOrientation = PageOrientation.Portrait;

            // 表の設定
            Table = new TableHelper();
            Table.X = CM2PX(1.0d);
            Table.Y = CM2PX(3.0d);
            Table.RowHeight = CM2PX(0.7d);
            Table.MaxRowCount = 23;
            Table.DefaultPadding = new Thickness(CM2PX(0.2d), CM2PX(0.15d), CM2PX(0.2d), CM2PX(0.05d));
            Table.AddColumn(CM2PX(1.5d));   // No
            Table.AddColumn(CM2PX(7.0d));   // 書名
            Table.AddColumn(CM2PX(5.0d));   // 著者名
            Table.AddColumn(CM2PX(4.0d));   // 出版名
            Table.AddColumn(CM2PX(3.0d));   // カテゴリ
            Table.AddColumn(CM2PX(2.0d));   // 価格
            Table.AddColumn(CM2PX(2.5d));   // 購入日
            Table.AddColumn(CM2PX(2.5d));   // 評価点

        }
        #endregion

        #region ■ Properties ■

        private List<Models.Book> BookList { get; set; }
        private int DataRowIndex { get; set; }
        private TableHelper Table { get; set; }

        #endregion

        #region ■ Methods ■

        #region **** Protected Method : DrawingPage
        /// <summary>
        /// ページを描画する
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="arg"></param>
        protected override void DrawingPage(DrawingContext dc, ReportArgument arg)
        {
            var DCH = GetDrawingContextHelper(dc);
            var titleFont = new Report.FontInfo("Meiryo", true, 32, Brushes.Black);

            // ヘッダの描画
            DCH.DrawText("蔵書リスト", titleFont, CM2PX(1.0d), CM2PX(1.5d));
            DCH.DrawText(string.Format("Page.{0}", arg.PageNo), CM2PX(26.5d), CM2PX(1.4d));

            Table.HeaderColumns[0].DrawText("No", TextAlignment.Right, DCH);
            Table.HeaderColumns[1].DrawText("書名", TextAlignment.Left, DCH);
            Table.HeaderColumns[2].DrawText("著者名", TextAlignment.Left, DCH);
            Table.HeaderColumns[3].DrawText("出版社", TextAlignment.Left, DCH);
            Table.HeaderColumns[4].DrawText("カテゴリ", TextAlignment.Left, DCH);
            Table.HeaderColumns[5].DrawText("価格", TextAlignment.Right, DCH);
            Table.HeaderColumns[6].DrawText("購入日", TextAlignment.Center, DCH);
            Table.HeaderColumns[7].DrawText("評価点", TextAlignment.Left, DCH);

            // 明細の描画
            Table.RowIndex = 0;
            while (DataRowIndex < BookList.Count)
            {
                // 1ページ分明細行を印刷したらブレイク
                if (Table.PageBreak)
                    break;

                if (Table.RowIndex % 2 != 0)
                    Table.DrawRowBorder(Table.RowIndex, null, new Thickness(0.0d), Brushes.WhiteSmoke, DCH);

                var book = BookList[DataRowIndex];
                Table[0].DrawText(book.Id.ToString(), TextAlignment.Right, DCH);
                Table[1].DrawText(book.Title, TextAlignment.Left, DCH);
                Table[2].DrawText(book.Author, TextAlignment.Left, DCH);
                Table[3].DrawText(book.Publisher, TextAlignment.Left, DCH);
                if (book.Category.HasValue)
                    Table[4].DrawText(book.Category.Value.ToDisplayName(), TextAlignment.Left, DCH);
                if (book.Price.HasValue)
                    Table[5].DrawText(book.Price.Value.ToString("#,##0円"), TextAlignment.Right, DCH);
                if (book.PurchaseDate.HasValue)
                    Table[6].DrawText(book.PurchaseDate.Value.ToString("yyyy/MM/dd"), TextAlignment.Center, DCH);
                if (book.ReviewPoint.HasValue)
                    Table[7].DrawText(new String('★', book.ReviewPoint.Value), TextAlignment.Left, DCH);

                DataRowIndex++;
                Table.RowIndex++;
            }

            // 枠線の描画
            Table.DrawBorder(DCH.DefaulBrush, new Thickness(1.0d), null, DCH);
            Table.DrawHeaderBorder(DCH.DefaulBrush, new Thickness(0.0d, 0.0d, 0.0d, 1.0d), null, DCH);

            // 印刷すべきデータがまだあれば印刷を継続する
            arg.PrintTerminate = (DataRowIndex >= BookList.Count);
        }
        #endregion

        #endregion

    }
    #endregion
}
