using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.Windows;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Media;
using System.IO;
using System.IO.Packaging;

namespace hsb.WPF
{
    #region 【Class : Report】
    /// <summary>
    /// 印刷レポートクラス
    /// </summary>
    public class Report
    {
        #region ■ Enum ■

        #region **** Enum : PrintType
        /// <summary>
        /// 印刷種別
        /// </summary>
        public enum PrintType { Print, XpsDocument, ExportXpsDocumentFile }
        #endregion

        #endregion

        #region ■ Constants ■

        public static readonly string DEFAULT_TYPE_FACE_NAME = "Meiryo";
        public static readonly int DEFAULT_FONT_SIZE = 12;

        #endregion

        #region ■ Inner Class ■

        #region 【Inner Class : PageRange】
        /// <summary>
        /// 印刷ページ範囲
        /// </summary>
        public class PrintRange
        {
            #region ■ Constructor ■
            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="startPage">int? : 印刷開始ページ</param>
            /// <param name="endPage">int? : 印刷終了ページ</param>
            public PrintRange(int? startPage, int? endPage)
            {
                StartPage = startPage;
                EndPage = endPage;
            }
            #endregion

            #region ■ Properties ■

            #region **** Property : StartPage (Set private only)
            /// <summary>
            /// 印刷開始ページ
            /// </summary>
            public int? StartPage { get; private set; }
            #endregion

            #region **** Property : EndPage (Set private only)
            /// <summary>
            /// 　印刷終了ページ
            /// </summary>
            public int? EndPage { get; private set; }
            #endregion

            #endregion
        }
        #endregion

        #region 【Inner Class : ReportArgument】
        /// <summary>
        /// ページ印刷時パラメータ 
        /// </summary>
        public class ReportArgument
        {
            #region ■ Constructor ■
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public ReportArgument(PrintType printType, PrintRange range)
            {
                PrintType = printType;
                Range = range;
                PrintTerminate = true;
                if (range != null)
                    PageNo = range.StartPage ?? 1;
                else
                    PageNo = 1;
            }
            #endregion

            #region ■ Properties ■

            #region **** Property : PrintMode (Set private only)
            /// <summary>
            /// 印刷種別
            /// </summary>
            public PrintType PrintType { get; private set; }
            #endregion

            #region **** Property : Range (Set private only)
            /// <summary>
            /// 印刷範囲
            /// </summary>
            public PrintRange Range { get; private set; }
            #endregion

            #region **** Property : PrintTerminate
            /// <summary>
            /// 印刷終了
            /// </summary>
            public bool PrintTerminate { get; set; }
            #endregion

            #region **** Property : PageNo
            /// <summary>
            /// ページ番号
            /// </summary>
            public int PageNo { get; set; }
            #endregion

            #endregion
        }
        #endregion

        #region 【Protected Inner Class : FontInfo】
        /// <summary>
        /// フォント情報
        /// </summary>
        protected class FontInfo
        {
             #region ■ Constructor ■

             #region **** Constructor(1)
             /// <summary>
             /// コンストラクタ(1)
             /// </summary>
             /// <param name="typeface">Typeface : タイプフェイス</param>
             /// <param name="size">double : フォントサイズ</param>
             /// <param name="brush">Brush : ブラシ</param>
             public FontInfo(Typeface typeface, double size, Brush brush)
             {
                 Typeface = typeface;
                 FontSize = size;
                 Brush = brush;
             }
             #endregion

             #region **** Constructor(2)
             /// <summary>
             /// コンストラクタ(2)
             /// </summary>
             /// <param name="typeface">Typeface : タイプフェイス</param>
             /// <param name="size">double : フォントサイズ</param>
             public FontInfo(Typeface typeface, double size)
                 : this(typeface, size, Brushes.Black)
             {
             }
             #endregion

             #region **** Constructor(3)
             /// <summary>
             /// コンストラクタ(3)
             /// </summary>
             /// <param name="typefaceName">strng : タイプフェイス名</param>
             /// <param name="size">double : フォントサイズ</param>
             /// <param name="brush">Brush : ブラシ</param>
             public FontInfo(string typefaceName, double size, Brush brush)
                 : this(new Typeface(typefaceName), size, brush)
             {
             }
             #endregion

             #region **** Constructor(4)
             /// <summary>
             /// コンストラクタ(4)
             /// </summary>
             /// <param name="typefaceName">strng : タイプフェイス名</param>
             /// <param name="size">double : フォントサイズ</param>
             public FontInfo(string typefaceName, double size)
                 : this(new Typeface(typefaceName), size, Brushes.Black)
             {
             }
             #endregion

             #region **** Constructor(5)
             /// <summary>
             /// コンストラクタ(5)
             /// </summary>
             /// <param name="typefaceName">string : タイプフェイス名</param>
             /// <param name="bold">bool : ボールド？</param>
             /// <param name="size">double : フォントサイズ</param>
             /// <param name="brush">Brush : ブラシ</param>
             public FontInfo(string typefaceName, bool bold, double size, Brush brush)
             {
                 Typeface = new Typeface(new FontFamily(typefaceName), FontStyles.Normal, (bold) ? FontWeights.Bold : FontWeights.Normal, FontStretches.Normal);
                 FontSize = size;
                 Brush = brush;
             }
             #endregion

             #region **** Constructor(6)
             /// <summary>
             /// コンストラクタ(6)
             /// </summary>
             /// <param name="typefaceName">strng : タイプフェイス名</param>
             /// <param name="bold">bool : ボールド？</param>
             /// <param name="size">double : フォントサイズ</param>
             public FontInfo(string typefaceName, bool bold, double size)
                 : this(typefaceName, bold, size, Brushes.Black)
             {
             }
             #endregion

             #endregion

             #region ■ Properties ■

             #region **** Property : Typeface
             /// <summary>
             /// タイプフェイス
             /// </summary>
             public Typeface Typeface { get; set; }
             #endregion

             #region **** Property : FontSize
             /// <summary>
             /// フォントサイズ
             /// </summary>
             public double FontSize { get; set; }
             #endregion

             #region **** Property : Brush
             /// <summary>
             /// ブラシ
             /// </summary>
             public Brush Brush { get; set; }
             #endregion

             #endregion
        }
        #endregion

        #region 【Inner Class : DrawingContextHelper】
        /// <summary>
        /// ドローイングコンテキスト用ヘルパークラス
        /// </summary>
        protected class DrawingContextHelper
        {
            #region ■ Constructor ■
            /// <summary>
            /// コンストラクタ
            /// </summary>
            public DrawingContextHelper(DrawingContext dc, double offsetX, double offsetY)
            {
                DC = dc;
                OffsetX = offsetX;
                OffsetY = offsetY;

                DefaultPen = new Pen(Brushes.Black, 1.0d);
                DefaulBrush = Brushes.Black;
                DefaultFontInfo = new FontInfo(DEFAULT_TYPE_FACE_NAME, false, DEFAULT_FONT_SIZE, DefaulBrush);
            }
            #endregion

            #region ■ Properties ■

            #region **** Private Property : DC
            /// <summary>
            /// ドローイングコンテキスト
            /// </summary>
            private DrawingContext DC { get; set; }
            #endregion

            #region **** Property : OffsetX
            /// <summary>
            /// X座標のオフセット値
            /// </summary>
            public double OffsetX { get; set; }
            #endregion

            #region **** Property : OffsetY
            /// <summary>
            /// Y座標のオフセット値
            /// </summary>
            public double OffsetY { get; set; }
            #endregion

            #region **** Property : DefaultFontInfo
            /// <summary>
            /// デフォルトフォント情報
            /// </summary>
            public FontInfo DefaultFontInfo { get; set; }
            #endregion

            #region **** Property : DefaultPen
            /// <summary>
            /// デフォルトペン
            /// </summary>
            public Pen DefaultPen { get; set; }
            #endregion

            #region **** Property : DefaultBrush
            /// <summary>
            /// デフォルトブラシ
            /// </summary>
            public Brush DefaulBrush { get; set; }
            #endregion

            #endregion

            #region ■ Methods ■

            #region **** Method : CPoint
            /// <summary>
            /// 座標補正済みのPointを返す
            /// </summary>
            /// <param name="x">double : X軸</param>
            /// <param name="y">double : Y軸</param>
            /// <returns>Point</returns>
            public Point CPoint(double x, double y)
            {
                return new Point(x + OffsetX, y + OffsetY);
            }
            #endregion

            #region **** Method : CPoint
            /// <summary>
            /// 座標補正済みのPointを返す
            /// </summary>
            /// <param name="p">Point</param>
            /// <returns>Point</returns>
            public Point CPoint(Point p)
            {
                return new Point(p.X + OffsetX, p.Y + OffsetY);
            }
            #endregion

            #region **** Method : CRect
            /// <summary>
            /// 座標補正済みのRectを返す
            /// </summary>
            /// <param name="x">double : X軸</param>
            /// <param name="y">double : Y軸</param>
            /// <param name="width">double : 幅</param>
            /// <param name="height">double : 高さ</param>
            /// <returns>Rect</returns>
            public Rect CRect(double x, double y, double width, double height)
            {
                return new Rect(x + OffsetX, y + OffsetY, width, height);
            }
            #endregion

            #region **** Method : CRect
            /// <summary>
            /// 座標補正済みのRectを返す
            /// </summary>
            /// <returns>Rect</returns>
            public Rect CRect(Rect r)
            {
                return new Rect(r.X + OffsetX, r.Y + OffsetY, r.Width, r.Height);
            }
            #endregion

            #region ***** Method : FText(1)
            /// <summary>
            /// FormattedText を生成する(1)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="typeface">Typeface : タイプフェイス</param>
            /// <param name="size">double : 文字サイズ</param>
            /// <param name="b">Brush : 描画ブラシ</param>
            /// <returns>FormattedText</returns>
            public FormattedText FText(string s, Typeface typeface, double size, Brush b)
            {
                return new FormattedText(s,
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Windows.FlowDirection.LeftToRight,
                    typeface, size, b);
            }
            #endregion

            #region ***** Method : FText(2)
            /// <summary>
            /// FormattedText を生成する(2)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="fontInfo">SCFontInfo : フォント情報</param>
            /// <returns>FormattedText</returns>
            public FormattedText FText(string s, FontInfo fontInfo)
            {
                return new FormattedText(s,
                    System.Globalization.CultureInfo.CurrentCulture,
                    System.Windows.FlowDirection.LeftToRight,
                    fontInfo.Typeface, fontInfo.FontSize, fontInfo.Brush);
            }
            #endregion

            #region ***** Method : FText(3)
            /// <summary>
            /// FormattedText を生成する(3)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="typeface">Typeface : タイプフェイス</param>
            /// <param name="size">double : 文字サイズ</param>
            /// <param name="b">Brush : 描画ブラシ</param>
            /// <param name="maxWidth">double : 最大幅(lpx)</param>
            /// <param name="alignment">TextAlignment : 文字配置</param>
            /// <returns>FormattedText</returns>
            public FormattedText FText(string s, Typeface typeface, double size, Brush b, double maxWidth, TextAlignment alignment)
            {
                var fmtText = FText(s, typeface, size, b);
                fmtText.MaxTextWidth = maxWidth;
                fmtText.TextAlignment = alignment;
                return fmtText;
            }
            #endregion

            #region ***** Method : FText(4)
            /// <summary>
            /// FormattedText を生成する(4)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="fontInfo">SCFontInfo : フォント情報</param>
            /// <param name="maxWidth">double : 最大幅(lpx)</param>
            /// <param name="alignment">TextAlignment : 文字配置</param>
            /// <returns>FormattedText</returns>
            public FormattedText FText(string s, FontInfo fontInfo, double maxWidth, TextAlignment alignment)
            {
                var fmtText = FText(s, fontInfo);
                fmtText.MaxTextWidth = maxWidth;
                fmtText.TextAlignment = alignment;
                return fmtText;
            }
            #endregion

            #region **** Method : DrawText(1)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(1)
            /// </summary>
            /// <param name="ft">FormattedText</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(FormattedText ft, double x, double y)
            {
                DC.DrawText(ft, CPoint(x, y));
            }
            #endregion

            #region **** Method : DrawText(2)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(2)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="typeface">Typeface : タイプフェイス</param>
            /// <param name="size">double : 文字サイズ</param>
            /// <param name="b">Brush : 描画ブラシ</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, Typeface typeface, double size, Brush b, double x, double y)
            {
                DrawText(FText(s, typeface, size, b), x, y);
            }
            #endregion

            #region ***** Method : DrawText(3)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(3)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="fontInfo">SCFontInfo : フォント情報</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, FontInfo fontInfo, double x, double y)
            {
                DrawText(FText(s, fontInfo), x, y);
            }
            #endregion

            #region ***** Method : DrawText(4)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(4)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="typeface">Typeface : タイプフェイス</param>
            /// <param name="size">double : 文字サイズ</param>
            /// <param name="b">Brush : 描画ブラシ</param>
            /// <param name="maxWidth">double : 最大幅(lpx)</param>
            /// <param name="alignment">TextAlignment : 文字配置</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, Typeface typeface, double size, Brush b, double maxWidth, TextAlignment alignment, double x, double y)
            {
                DrawText(FText(s, typeface, size, b, maxWidth, alignment), x, y);
            }
            #endregion

            #region ***** Method : DrawText(5)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(5)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="fontInfo">SCFontInfo : フォント情報</param>
            /// <param name="maxWidth">double : 最大幅(lpx)</param>
            /// <param name="alignment">TextAlignment : 文字配置</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, FontInfo fontInfo, double maxWidth, TextAlignment alignment, double x, double y)
            {
                DrawText(FText(s, fontInfo, maxWidth, alignment), x, y);
            }
            #endregion

            #region **** Method : DrawText(6)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(6)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, double x, double y)
            {
                DrawText(FText(s, DefaultFontInfo), x, y);
            }
            #endregion

            #region **** Method : DrawText(7)
            /// <summary>
            /// ドローイングコンテキストに文字列を描画する(7)
            /// </summary>
            /// <param name="s">string : 文字列</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            public void DrawText(string s, double maxWidth, TextAlignment alignment, double x, double y)
            {
                DrawText(FText(s, DefaultFontInfo, maxWidth, alignment), x, y);
            }
            #endregion

            #region **** Method : DrawLine(1)
            /// <summary>
            /// 直線を描画する(1)
            /// </summary>
            /// <param name="pen">Pen</param>
            /// <param name="x1">double : 開始点X座標</param>
            /// <param name="y1">double : 開始点Y座標</param>
            /// <param name="x2">double : 終点X座標</param>
            /// <param name="y2">double : 終点Y座標</param>
            public void DrawLine(Pen pen, double x1, double y1, double x2, double y2)
            {
                DC.DrawLine(pen, CPoint(x1, y1), CPoint(x2, y2));
            }
            #endregion

            #region **** Method : DrawLine(2)
            /// <summary>
            /// 直線を描画する(2)
            /// </summary>
            /// <param name="pen">Pen</param>
            public void DrawLine(Pen pen, Point p1, Point p2)
            {
                DC.DrawLine(pen, CPoint(p1), CPoint(p2));
            }
            #endregion

            #region **** Method : DrawLine(3)
            /// <summary>
            /// 直線を描画する(3)
            /// </summary>
            /// <param name="x1">double : 開始点X座標</param>
            /// <param name="y1">double : 開始点Y座標</param>
            /// <param name="x2">double : 終点X座標</param>
            /// <param name="y2">double : 終点Y座標</param>
            public void DrawLine(double x1, double y1, double x2, double y2)
            {
                DC.DrawLine(DefaultPen, CPoint(x1, y1), CPoint(x2, y2));
            }
            #endregion

            #region **** Method : DrawLine(4)
            /// <summary>
            /// 直線を描画する(4)
            /// </summary>
            /// <param name="pen">Pen</param>
            public void DrawLine(Point p1, Point p2)
            {
                DC.DrawLine(DefaultPen, CPoint(p1), CPoint(p2));
            }
            #endregion

            #region **** Method : DrawRect(1)
            /// <summary>
            /// 矩形を描画する(1)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="x1">double : X座標</param>
            /// <param name="y1">double : Y座標</param>
            /// <param name="x2">dounle : 幅</param>
            /// <param name="y2">double : 高さ</param>
            public void DrawRect(Brush b, Pen pen, double x, double y, double width, double height)
            {
                DC.DrawRectangle(b, pen, CRect(x, y, width, height));
            }
            #endregion

            #region **** Method : DrawRect(2)
            /// <summary>
            /// 矩形を描画する(2)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="r">Rect</param>
            public void DrawRect(Brush b, Pen pen, Rect r)
            {
                DC.DrawRectangle(b, pen, CRect(r));
            }
            #endregion

            #region **** Method : DrawRect(3)
            /// <summary>
            /// 矩形を描画する(3)
            /// </summary>
            /// <param name="x1">double : X座標</param>
            /// <param name="y1">double : Y座標</param>
            /// <param name="x2">dounle : 幅</param>
            /// <param name="y2">double : 高さ</param>
            public void DrawRect(double x, double y, double width, double height)
            {
                DC.DrawRectangle(DefaulBrush, DefaultPen, CRect(x, y, width, height));
            }
            #endregion

            #region **** Method : DrawRect(4)
            /// <summary>
            /// 矩形を描画する(2)
            /// </summary>
            /// <param name="r">Rect</param>
            public void DrawRect(Rect r)
            {
                DC.DrawRectangle(DefaulBrush, DefaultPen, CRect(r));
            }
            #endregion

            #region **** Method : DrawRoundRect(1)
            /// <summary>
            /// 角丸矩形を描画する(1)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="width">double : 幅</param>
            /// <param name="height">double : 高さ</param>
            /// <param name="rx">double : 角丸の半径X軸</param>
            /// <param name="ry">double : 角丸の半径Y軸</param>
            public void DrawRoundRect(Brush b, Pen pen, double x, double y, double width, double height, double rx, double ry)
            {
                DC.DrawRoundedRectangle(b, pen, CRect(x, y, width, height), rx, ry);
            }
            #endregion

            #region **** Method : DrawRoundRect(2)
            /// <summary>
            /// 角丸矩形を描画する(2)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="r">Rect : 矩形</param>
            /// <param name="rx">double : 角丸の半径X軸</param>
            /// <param name="ry">double : 角丸の半径Y軸</param>
            public void DrawRoundRect(Brush b, Pen pen, Rect r, double rx, double ry)
            {
                DC.DrawRoundedRectangle(b, pen, CRect(r), rx, ry);
            }
            #endregion

            #region **** Method : DrawRoundRect(3)
            /// <summary>
            /// 角丸矩形を描画する(3)
            /// </summary>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="width">double : 幅</param>
            /// <param name="height">double : 高さ</param>
            /// <param name="rx">double : 角丸の半径X軸</param>
            /// <param name="ry">double : 角丸の半径Y軸</param>
            public void DrawRoundRect(double x, double y, double width, double height, double rx, double ry)
            {
                DC.DrawRoundedRectangle(DefaulBrush, DefaultPen, CRect(x, y, width, height), rx, ry);
            }
            #endregion

            #region **** Method : DrawRoundRect(4)
            /// <summary>
            /// 角丸矩形を描画する(4)
            /// </summary>
            /// <param name="r">Rect : 矩形</param>
            /// <param name="rx">double : 角丸の半径X軸</param>
            /// <param name="ry">double : 角丸の半径Y軸</param>
            public void DrawRoundRect(Rect r, double rx, double ry)
            {
                DC.DrawRoundedRectangle(DefaulBrush, DefaultPen, CRect(r), rx, ry);
            }
            #endregion

            #region **** Method : DrawEllipse(1)
            /// <summary>
            /// 楕円を描画する(1)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="rx">double : 縦半径</param>
            /// <param name="ry">double : 横半径</param>
            public void DrawEllipse(Brush b, Pen pen, double x, double y, double rx, double ry)
            {
                DC.DrawEllipse(b, pen, CPoint(x, y), rx, ry);
            }
            #endregion

            #region **** Method : DrawEllipse(2)
            /// <summary>
            /// 楕円を描画する(2)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="p">Point</param>
            /// <param name="rx">double : 縦半径</param>
            /// <param name="ry">double : 横半径</param>
            public void DrawEllipse(Brush b, Pen pen, Point p, double rx, double ry)
            {
                DC.DrawEllipse(b, pen, CPoint(p), rx, ry);
            }
            #endregion

            #region **** Method : DrawEllipse(3)
            /// <summary>
            /// 楕円を描画する(3)
            /// </summary>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="rx">double : 縦半径</param>
            /// <param name="ry">double : 横半径</param>
            public void DrawEllipse(double x, double y, double rx, double ry)
            {
                DC.DrawEllipse(DefaulBrush, DefaultPen, CPoint(x, y), rx, ry);
            }
            #endregion

            #region **** Method : DrawEllipse(4)
            /// <summary>
            /// 楕円を描画する(4)
            /// </summary>
            /// <param name="p">Point</param>
            /// <param name="rx">double : 縦半径</param>
            /// <param name="ry">double : 横半径</param>
            public void DrawEllipse(Point p, double rx, double ry)
            {
                DC.DrawEllipse(DefaulBrush, DefaultPen, CPoint(p), rx, ry);
            }
            #endregion

            #region **** Method : DrawCircle(1)
            /// <summary>
            /// 円を描画する(1)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="r">double : 半径</param>
            public void DrawCircle(Brush b, Pen pen, double x, double y, double r)
            {
                DrawEllipse(b, pen, x, y, r, r);
            }
            #endregion

            #region **** Method : DrawCircle(2)
            /// <summary>
            /// 円を描画する(2)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="p">Point</param>
            /// <param name="r">double : 半径</param>
            public void DrawCircle(Brush b, Pen pen, Point p, double r)
            {
                DrawEllipse(b, pen, p, r, r);
            }
            #endregion

            #region **** Method : DrawCircle(3)
            /// <summary>
            /// 円を描画する(3)
            /// </summary>
            /// <param name="x">double : X座標</param>
            /// <param name="y">double : Y座標</param>
            /// <param name="r">double : 半径</param>
            public void DrawCircle(double x, double y, double r)
            {
                DrawEllipse(x, y, r, r);
            }
            #endregion

            #region **** Method : DrawCircle(4)
            /// <summary>
            /// 円を描画する(4)
            /// </summary>
            /// <param name="p">Point</param>
            /// <param name="r">double : 半径</param>
            public void DrawCircle(Point p, double r)
            {
                DrawEllipse(p, r, r);
            }
            #endregion

            #region **** Method : DrawGeometry(1)
            /// <summary>
            /// ジオメトリを描画する(1)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="g">Geometry</param>
            public void DrawGeometry(Brush b, Pen pen, Geometry g)
            {
                DC.DrawGeometry(b, pen, g);
            }
            #endregion

            #region **** Method : DrawGeometry(2)
            /// <summary>
            /// ジオメトリを描画する(2)
            /// </summary>
            /// <param name="b">Brush</param>
            /// <param name="pen">Pen</param>
            /// <param name="g">Geometry</param>
            public void DrawGeometry(Geometry g)
            {
                DC.DrawGeometry(DefaulBrush, DefaultPen, g);
            }
            #endregion

            #endregion
        }
        #endregion

        #region 【Inner Class : TableCell】
        /// <summary>
        /// 表描画用ヘルパー・セルクラス
        /// </summary>
        protected class TableCell
        {
            #region ■ Constructor ■

            #region **** Constructor(1)
            /// <summary>
            /// コンストラクタ(1)
            /// </summary>
            /// <param name="table">TableHelper</param>
            /// <param name="x">double : X座標位置</param>
            /// <param name="y">double : Y座標位置</param>
            /// <param name="width">double : セル幅</param>
            /// <param name="height">double : セル高さ</param>
            public TableCell(TableHelper table, double x, double y, double width, double height)
            {
                Table = table;
                OriginX = 0.0d;
                OriginY = 0.0d;
                OffsetX = x;
                OffsetY = y;
                Width = width;
                Height = height;
                Padding = Table.DefaultPadding;
            }
            #endregion

            #region **** Constructor(2)
            /// <summary>
            /// コンストラクタ(2)
            /// </summary>
            /// <param name="table">TableHelper</param>
            /// <param name="width">double : セル幅</param>
            public TableCell(TableHelper table, double width)
            {
                Table = table;
                OriginX = 0.0d;
                OriginY = 0.0d;
                OffsetX = Table.Width;
                OffsetY = 0.0d;
                Width = width;
                Height = Table.RowHeight;
                Padding = Table.DefaultPadding;
            }
            #endregion

            #region **** Constructor(3)
            /// <summary>
            /// コンストラクタ(3)
            /// </summary>
            /// <param name="cell">TableCell</param>
            public TableCell(TableCell cell)
            {
                Table = cell.Table;
                OriginX = cell.OriginX;
                OriginY = cell.OriginY;
                OffsetX = cell.OffsetX;
                OffsetY = cell.OffsetY;
                Width = cell.Width;
                Height = cell.Height;
                Padding = cell.Padding;
            }
            #endregion

            #endregion

            #region ■ Properties ■

            #region **** Private Property : Table
            /// <summary>
            /// 表描画ヘルパー
            /// </summary>
            private TableHelper Table { get; set; }
            #endregion

            #region Property : OriginX
            /// <summary>
            /// 原点（行の左上端）X軸
            /// </summary>
            public double OriginX { get; set; }
            #endregion

            #region Property : OriginY
            /// <summary>
            /// 原点（行の左上端）Y軸
            /// </summary>
            public double OriginY { get; set; }
            #endregion

            #region **** Property : Padding
            /// <summary>
            /// パディング
            /// </summary>
            public Thickness Padding { get; set; }
            #endregion

            #region **** Property : OffsetX (Set private only)
            /// <summary>
            /// セルの位置(X軸)
            /// 　※ 行左上端を原点とする
            /// </summary>
            public double OffsetX { get; private set; }
            #endregion

            #region **** Property : OffsetY (Set private only)
            /// <summary>
            /// セルの位置(Y軸)
            /// 　※ 行左上端を原点とする
            /// </summary>
            public double OffsetY { get; private set; }
            #endregion

            #region **** Property : Width (Set private only)
            /// <summary>
            /// セル幅
            /// </summary>
            public double Width { get; private set; }
            #endregion

            #region **** Property : Height (Set private only)
            /// <summary>
            /// セル高さ
            /// </summary>
            public double Height { get; private set; }
            #endregion

            #region **** Property : Rect (Read only)
            /// <summary>
            /// セルの矩形
            /// </summary>
            public Rect Rect
            {
                get { return new Rect(OffsetX, OffsetY, Width, Height); }
            }
            #endregion

            #region **** Property : Left (Read only)
            /// <summary>
            /// セル左端位置
            /// </summary>
            public double Left { get { return OffsetX; } }
            #endregion

            #region **** Property : Right (Read only)
            /// <summary>
            /// セル右端位置
            /// </summary>
            public double Right { get { return OffsetX + Width; } }
            #endregion

            #region **** Property : Top (Read only)
            /// <summary>
            /// セル上端位置
            /// </summary>
            public double Top { get { return OffsetY; } }
            #endregion

            #region **** Property : Bottom (Read only)
            /// <summary>
            /// セル下端位置
            /// </summary>
            public double Bottom { get { return OffsetY + Height; } }
            #endregion

            #region **** Property : LocationRect (Read only)
            /// <summary>
            /// セルの矩形（絶対位置）
            /// </summary>
            public Rect LocationRect
            {
                get { return new Rect(LocationX, LocationY, Width, Height); }
            }
            #endregion

            #region **** Property : LocationX (Read only)
            /// <summary>
            /// セルのX座標位置(絶対位置）
            /// </summary>
            public double LocationX
            {
                get { return OriginX + OffsetX; }
            }
            #endregion

            #region **** Property : LocationY (Read only)
            /// <summary>
            /// セルのY座標位置(絶対位置)
            /// </summary>
            public double LocationY
            {
                get { return OriginY + OffsetY; }
            }
            #endregion

            #region **** Property : LocationLeft (Read only)
            /// <summary>
            /// セルの左端位置(絶対位置)
            /// </summary>
            public double LocationLeft
            {
                get { return LocationX; }
            }
            #endregion

            #region **** Property : LocationRight (Read only)
            /// <summary>
            /// セルの右端位置(絶対位置)
            /// </summary>
            public double LocationRight
            {
                get { return LocationLeft + Width; }
            }
            #endregion

            #region **** Property : LocationTop (Read only)
            /// <summary>
            /// セルの上端位置(絶対位置)
            /// </summary>
            public double LocationTop
            {
                get { return LocationY; }
            }
            #endregion

            #region **** Property : LocationBottom (Read only)
            /// <summary>
            /// セルの下端位置(絶対指定)
            /// </summary>
            public double LocationBottom
            {
                get { return LocationTop + Height; }
            }
            #endregion

            #endregion

            #region ■ Methods ■

            #region **** Method : Marge
            /// <summary>
            /// セル同志を結合する
            /// </summary>
            /// <param name="cell">TableCell</param>
            public void Marge(TableCell cell)
            {
                var r1 = this.Rect;
                var r2 = cell.Rect;
                this.OffsetX = (r1.Left <= r2.Left) ? r1.Left : r2.Left;
                this.OffsetY = (r1.Top <= r2.Top) ? r1.Top : r2.Top;
                this.Width = (r1.Right <= r2.Right) ? r2.Right - this.OffsetX : r1.Right - this.OffsetX;
                this.Height = (r1.Bottom <= r2.Bottom) ? r2.Bottom - this.OffsetY : r1.Bottom - this.OffsetY;
            }
            #endregion

            #region **** Method: DrawText(1)
            /// <summary>
            /// セルに文字列を印字する(1)
            /// </summary>
            /// <param name="s">string : 印字文字列</param>
            /// <param name="align">TextAlignment : アライアメント</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawText(string s, FontInfo fontInfo, TextAlignment align, DrawingContextHelper dch)
            {
                var ft = dch.FText(s, fontInfo);
                ft.MaxTextWidth = Width - (Padding.Left + Padding.Right);
                ft.MaxTextHeight = Height - (Padding.Top + Padding.Bottom);
                ft.TextAlignment = align;
                dch.DrawText(ft, LocationX + Padding.Left, LocationY + Padding.Top);
            }
            #endregion

            #region **** Method: DrawText(2)
            /// <summary>
            /// セルに文字列を印字する(2)
            /// </summary>
            /// <param name="s">string : 印字文字列</param>
            /// <param name="align">TextAlignment : アライアメント</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawText(string s, TextAlignment align, DrawingContextHelper dch)
            {
                DrawText(s, Table.DefaultFontinfo, align, dch);
            }
            #endregion

            #region **** Method : DrawBorder
            /// <summary>
            /// セルの枠線を描
            /// </summary>
            /// <param name="borderBrush">Brush : ブラシ</param>
            /// <param name="thickness">Thickness : 枠線の太さ</param>
            /// <param name="cell">SCTableHelperCell : セル情報</param>
            /// <param name="fillBrush">Brush : 塗りつぶし用ブラシ</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawBorder(Brush borderBrush, Thickness thickness, Brush fillBrush, DrawingContextHelper dch)
            {
                if (fillBrush != null)
                    dch.DrawRect(fillBrush, null, LocationRect);

                if (thickness.Left != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Left), LocationLeft, LocationTop, LocationLeft, LocationBottom);
                if (thickness.Right != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Right), LocationRight, LocationTop, LocationRight, LocationBottom);
                if (thickness.Top != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Top), LocationLeft, LocationTop, LocationRight, LocationTop);
                if (thickness.Bottom != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Bottom), LocationLeft, LocationBottom, LocationRight, LocationBottom);
            }
            #endregion

            #endregion
        }
        #endregion

        #region 【Inner Class : TableHelper】
        /// <summary>
        /// 表描画用ヘルパークラス
        /// </summary>
        protected class TableHelper
        {
            #region ■ Inner Class ■

            #region 【Inner Class : _HeaderColumns】
            /// <summary>
            /// 見出し列用ヘルパクラス
            /// </summary>
            public class _HeaderColumns
            {
                #region ■ Constructor ■
                /// <summary>
                /// コンストラクタ
                /// </summary>
                /// <param name="table">TableHelper</param>
                public _HeaderColumns(TableHelper table)
                {
                    Table = table;
                }
                #endregion

                #region ■ Properties ■

                #region **** Private Property : Table
                /// <summary>
                /// テーブルヘルパー
                /// </summary>
                private TableHelper Table { get; set; }
                #endregion

                #region **** Property : Indexer
                /// <summary>
                /// インデクサ
                /// </summary>
                /// <param name="col">int : 列</param>
                /// <returns>TableCell</returns>
                public TableCell this[int col]
                {
                    get
                    {
                        if (col >= 0 && col < Table.ColumnDefines.Count)
                        {
                            var cell = new TableCell(Table.ColumnDefines[col]);
                            cell.OriginX = Table.X;
                            cell.OriginY = Table.Y;
                            return cell;
                        }
                        else
                            throw new ArgumentOutOfRangeException("col out of range");
                    }
                }
                #endregion

                #endregion
            }
            #endregion

            #endregion

            #region ■ Constructor ■

            #region **** Constructor(1)
            /// <summary>
            /// コンストラクター(1)
            /// </summary>
            public TableHelper()
            {
                X = 0.0d;
                Y = 0.0d;
                RowHeight = 0.0d;
                MaxRowCount = 0.0d;
                RowIndex = 0;

                ColumnDefines = new List<TableCell>();
                DefaultFontinfo = new FontInfo(DEFAULT_TYPE_FACE_NAME, DEFAULT_FONT_SIZE);
                DefaultPadding = new Thickness(0.0d);

                HeaderColumns = new _HeaderColumns(this);
            }
            #endregion

            #endregion

            #region ■ Properties ■

            #region **** Property : X
            /// <summary>
            /// 表の位置（X座標)
            /// </summary>
            public double X { get; set; }
            #endregion

            #region **** Property : Y
            /// <summary>
            /// 表の位置(Y座標)
            /// </summary>
            public double Y { get; set; }
            #endregion

            #region **** Property : RowHeight
            /// <summary>
            /// 行の高さ
            /// </summary>
            public double RowHeight { get; set; }
            #endregion

            #region **** Property : MaxRowCount
            /// <summary>
            /// 最大行数
            /// </summary>
            public double MaxRowCount { get; set; }
            #endregion

            #region **** Property : RowIndex
            /// <summary>
            /// 行インデックス
            /// </summary>
            public int RowIndex { get; set; }
            #endregion

            #region **** Property : Width (Read only)
            /// <summary>
            /// 表の幅
            /// </summary>
            public double Width
            {
                get 
                {
                    if (ColumnDefines.Count > 0)
                        return ColumnDefines.Max(col => col.Right);
                    else
                        return 0.0d;
                }
            }
            #endregion

            #region **** Property : Height (Read only)
            /// <summary>
            /// 表の高さ
            /// 　※（最大行数＋見出し行）×行高さ
            /// </summary>
            public double Height 
            {
                get { return (MaxRowCount + 1) * RowHeight; }
            }
            #endregion

            #region **** Property : Left (Read only)
            /// <summary>
            /// 表の左端位置
            /// </summary>
            public double Left
            {
                get { return X; }
            }
            #endregion

            #region **** Property : Right (Read only)
            /// <summary>
            /// 表の右端位置
            /// </summary>
            public double Right
            {
                get { return X + Width; }
            }
            #endregion

            #region **** Property : Top (Read only)
            /// <summary>
            /// テーブルの上端位置
            /// </summary>
            public double Top
            {
                get { return Y; }
            }
            #endregion

            #region **** Property : Bottom (Read only)
            /// <summary>
            /// テーブルの下端位置
            /// </summary>
            public double Bottom
            {
                get { return Y + Height; }
            }
            #endregion

            #region **** Property : Rect (Read only)
            /// <summary>
            /// テーブルの矩形
            /// </summary>
            public Rect Rect
            {
                get { return new Rect(X, Y, Width, Height); }
            }
            #endregion

            #region **** Property : HeaderRect (Read Only)
            /// <summary>
            /// 見出し部の矩形
            /// </summary>
            public Rect HeaderRect
            {
                get { return new Rect(X, Y, Width, RowHeight); }
            }
            #endregion

            #region **** Property : ColumnDefines (Set private only)
            /// <summary>
            /// 列定義
            /// </summary>
            public List<TableCell> ColumnDefines { get; private set; }
            #endregion

            #region **** Property : DefaultFontInfo
            /// <summary>
            /// デフォルトフォント情報
            /// </summary>
            public FontInfo DefaultFontinfo { get; set; }
            #endregion

            #region **** Property : DefaultPadding
            /// <summary>
            /// デフォルト・パディング値
            /// </summary>
            public Thickness DefaultPadding { get; set; }
            #endregion

            #region **** Property : Indexer(1) (Read only)
            /// <summary>
            /// セルを取得する
            /// </summary>
            /// <param name="row">int : 行</param>
            /// <param name="col">int : 列</param>
            /// <returns>TableCell</returns>
            public TableCell this[int row, int col]
            {
                get
                {
                    if (col >= 0 && col < ColumnDefines.Count)
                    {
                        var cell = new TableCell(ColumnDefines[col]);
                        cell.OriginX = X;
                        cell.OriginY = RowTop(row);
                        return cell;
                    }
                    else
                        throw new ArgumentOutOfRangeException("col out of range");
                }
            }
            #endregion

            #region **** Property : Indexer(2) (Read only)】
            /// <summary>
            /// 結合したセルを取得する
            /// </summary>
            /// <param name="row">int : 行</param>
            /// <param name="col">int : 列</param>
            /// <param name="col_span">int : 結合セル数</param>
            /// <returns>TableCell</returns>
            public TableCell this[int row, int col, int col_span]
            {
                get
                {
                    if (col >= 0 && col < ColumnDefines.Count && col + col_span - 1 < ColumnDefines.Count)
                    {
                        var cell = this[row, col];
                        for (int col2 = col + 1; col2 < col + col_span; col2++)
                            cell.Marge(this[row, col2]);
                        return cell;
                    }
                    else
                        throw new ArgumentOutOfRangeException("col out of range");
                }
            }
            #endregion

            #region **** Property : Indexer(3) (Read only)
            /// <summary>
            /// セルを取得する
            /// </summary>
            /// <param name="col">int : 列</param>
            /// <returns>TableCell</returns>
            public TableCell this[int col]
            {
                get
                {
                    if (col >= 0 && col < ColumnDefines.Count)
                    {
                        var cell = new TableCell(ColumnDefines[col]);
                        cell.OriginX = X;
                        cell.OriginY = RowTop(RowIndex);
                        return cell;
                    }
                    else
                        throw new ArgumentOutOfRangeException("col out of range");
                }
            }
            #endregion

            #region **** Property : HeaderColumns (Set private only)
            /// <summary>
            /// ヘッダー
            /// </summary>
            public _HeaderColumns HeaderColumns { get; private set; }
            #endregion

            #region **** Property : PageBreak
            /// <summary>
            /// ページブレイク？
            /// </summary>
            public bool PageBreak
            {
                get { return MaxRowCount <= RowIndex; }
            }
            #endregion

            #endregion

            #region ■ Methods ■

            #region **** Method : DrawBorder
            /// <summary>
            /// 枠線を描く
            /// </summary>
            /// <param name="borderBrush">Brush : ブラシ</param>
            /// <param name="thickness">Thickness : 枠線の太さ</param>
            /// <param name="cell">SCTableHelperCell : セル情報</param>
            /// <param name="fillBrush">Brush : 塗りつぶし用ブラシ</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawBorder(Brush borderBrush, Thickness thickness, Brush fillBrush, DrawingContextHelper dch)
            {
                if (fillBrush != null)
                    dch.DrawRect(fillBrush, null, Rect);

                if (thickness.Left != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Left), Left, Top, Left, Bottom);
                if (thickness.Right != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Right), Right, Top, Right, Bottom);
                if (thickness.Top != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Top), Left, Top, Right, Top);
                if (thickness.Bottom != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Bottom), Left, Bottom, Right, Bottom);
            }
            #endregion

            #region **** Method : RowRect
            /// <summary>
            /// 行の矩形を返す
            /// </summary>
            /// <param name="rowIndex">int : 行位置</param>
            /// <returns>Rect</returns>
            public Rect RowRect(int rowIndex)
            {
                if (rowIndex >= 0 && rowIndex < MaxRowCount)
                {
                    return new Rect(X, Y + (rowIndex + 1) * RowHeight, Width, RowHeight);
                }
                else
                    throw new ArgumentOutOfRangeException("rowIndex out of range");
            }
            #endregion

            #region **** Method : RowTop
            /// <summary>
            /// 行の上端位置を返す
            /// </summary>
            /// <param name="rowIndex">int : 行位置</param>
            /// <returns>double</returns>
            public double RowTop(int rowIndex)
            {
                if (rowIndex >= 0 && rowIndex < MaxRowCount)
                {
                    return Y + (rowIndex + 1) * RowHeight;
                }
                else
                    throw new ArgumentOutOfRangeException("rowIndex out of range");
            }
            #endregion

            #region **** Method : RowBottom
            /// <summary>
            /// 行の下端位置を返す
            /// </summary>
            /// <param name="rowIndex"></param>
            /// <returns></returns>
            public double RowBottom(int rowIndex)
            {
                return RowTop(rowIndex) + RowHeight;
            }
            #endregion

            #region **** Method : DrawRowBorder
            /// <summary>
            /// 行の枠線を描
            /// </summary>
            /// <param name="rowIndex">int : 行位置</param>
            /// <param name="borderBrush">Brush : ブラシ</param>
            /// <param name="thickness">Thickness : 枠線の太さ</param>
            /// <param name="cell">SCTableHelperCell : セル情報</param>
            /// <param name="fillBrush">Brush : 塗りつぶし用ブラシ</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawRowBorder(int rowIndex, Brush borderBrush, Thickness thickness, Brush fillBrush, DrawingContextHelper dch)
            {
                var r = RowRect(rowIndex);

                if (fillBrush != null)
                    dch.DrawRect(fillBrush, null, r);

                if (thickness.Left != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Left), r.Left, r.Top, r.Left, r.Bottom);
                if (thickness.Right != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Right), r.Right, r.Top, r.Right, r.Bottom);
                if (thickness.Top != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Top), r.Left, r.Top, r.Right, r.Top);
                if (thickness.Bottom != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Bottom), r.Left, r.Bottom, r.Right, r.Bottom);
            }
            #endregion

            #region **** Method : DrawHeaderBorder
            /// <summary>
            /// 見出しの枠線を描
            /// </summary>
            /// <param name="borderBrush">Brush : ブラシ</param>
            /// <param name="thickness">Thickness : 枠線の太さ</param>
            /// <param name="cell">SCTableHelperCell : セル情報</param>
            /// <param name="fillBrush">Brush : 塗りつぶし用ブラシ</param>
            /// <param name="dch">DrawingContextHelper</param>
            public void DrawHeaderBorder(Brush borderBrush, Thickness thickness, Brush fillBrush, DrawingContextHelper dch)
            {
                var r = HeaderRect;

                if (fillBrush != null)
                    dch.DrawRect(fillBrush, null, r);

                if (thickness.Left != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Left), r.Left, r.Top, r.Left, r.Bottom);
                if (thickness.Right != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Right), r.Right, r.Top, r.Right, r.Bottom);
                if (thickness.Top != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Top), r.Left, r.Top, r.Right, r.Top);
                if (thickness.Bottom != 0)
                    dch.DrawLine(new Pen(borderBrush, thickness.Bottom), r.Left, r.Bottom, r.Right, r.Bottom);
            }
            #endregion

            #region **** Method : AddColumn(1)
            /// <summary>
            /// 列を追加する(1)
            /// </summary>
            /// <param name="width">double : 列幅</param>
            public void AddColumn(double width)
            {
                ColumnDefines.Add(new TableCell(this, width));
            }
            #endregion

            #region **** Method : AddColumn(2)
            /// <summary>
            /// 列を追加する(2)
            /// </summary>
            /// <param name="x">double : X座標位置</param>
            /// <param name="y">double : Y座標位置</param>
            /// <param name="width">double : 幅</param>
            /// <param name="height">duoble : 高さ</param>
            public void AddColun(double x, double y, double width, double height)
            {
                ColumnDefines.Add(new TableCell(this, x, y, width, height));
            }
            #endregion

            #region **** Method : ClearColumns
            /// <summary>
            /// 列情報をクリアする
            /// </summary>
            public void ClearColumns()
            {
                ColumnDefines.Clear();
            }
            #endregion

            #endregion
        }
        #endregion

        #endregion

        #region ■ Constructor ■

        #region **** Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public Report()
        {
            PrintServer = new LocalPrintServer();
            SetDefaultPrintQueue();
        }
        #endregion

        #endregion

        #region ■ Properties ■

        #region **** Protected Property : LocalPrintServer
        /// <summary>
        /// プリントサーバー
        /// </summary>
        protected PrintServer PrintServer { get; set; }
        #endregion

        #region **** Protected Property : Queue
        /// <summary>
        /// 印刷キュー
        /// </summary>
        protected PrintQueue Queue { get; set; }
        #endregion

        #region **** Protected Property : PrintTicket
        /// <summary>
        /// プリントチケット
        /// </summary>
        protected PrintTicket PrintTicket { get; set; }
        #endregion

        #region **** Property : PrintQueueNames (Read Only)
        /// <summary>
        /// 所有するプリントキューの名称リスト
        /// </summary>
        public List<string> PrintQueueNames
        {
            get { return new List<string>(PrintServer.GetPrintQueues().Select(v => v.FullName)); }
        }
        #endregion

        #region **** Propertt : PrintQueueName
        /// <summary>
        /// プリントキュー名
        /// </summary>
        public string PrintQueueName
        {
            get { return Queue.FullName; }
            set
            {
                if (Queue.FullName != value && Queue.Name != value)
                    SetPrintQueue(value);
            }
        }
        #endregion

        #region **** Property : PageMediaSize
        /// <summary>
        /// 用紙サイズ
        /// </summary>
        public PageMediaSizeName PageMediaSize
        {
            get
            {
                return PrintTicket.PageMediaSize.PageMediaSizeName ?? PageMediaSizeName.Unknown;
            }
            set
            {
                if (PrintTicket.PageMediaSize.PageMediaSizeName != value)
                {
                    var printCap = Queue.GetPrintCapabilities();
                    foreach (var mediaSize in printCap.PageMediaSizeCapability.Where(ms => ms.PageMediaSizeName == value))
                    {
                        PrintTicket.PageMediaSize = mediaSize;
                        break;
                    }
                }
            }
        }
        #endregion

        #region **** Property : PageHeight (Read Only)
        /// <summary>
        /// 用紙の高さ
        /// </summary>
        public double PageHeight
        {
            get { return PrintTicket.PageMediaSize.Height ?? 0.0d; }
        }
        #endregion

        #region **** Property : PageWidth (Read Only)
        /// <summary>
        /// 用紙の幅
        /// </summary>
        public double PageWidth
        {
            get { return PrintTicket.PageMediaSize.Width ?? 0.0d; }
        }
        #endregion

        #region **** Property : PageOrientation
        /// <summary>
        /// 用紙の方向
        /// </summary>
        public PageOrientation PageOrientation
        {
            get { return PrintTicket.PageOrientation ?? PageOrientation.Unknown; }
            set
            {
                if (PrintTicket.PageOrientation.Value != value)
                {
                    var printCap = Queue.GetPrintCapabilities();
                    foreach (var po in printCap.PageOrientationCapability.Where(v => v == value))
                    {
                        PrintTicket.PageOrientation = value;
                        break;
                    }
                }
            }
        }
        #endregion

        #region **** Property : OffsetX
        /// <summary>
        /// X座標のオフセット値
        /// </summary>
        public double OffsetX { get; set; }
        #endregion

        #region **** Property : OffsetY
        /// <summary>
        /// Y座標のオフセット値
        /// </summary>
        public double OffsetY { get; set; }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : SetDefaultPrintQueue
        /// <summary>
        /// システムデフォルトのプリントキューを設定する
        /// </summary>
        public void SetDefaultPrintQueue()
        {
            var lps = new LocalPrintServer();
            Queue = lps.DefaultPrintQueue;
            PrintTicket = Queue.UserPrintTicket.Clone();
        }
        #endregion

        #region **** Method : SetPrintQueue
        /// <summary>
        /// プリントキューを設定する
        /// </summary>
        /// <param name="queueName">string : キューの名称</param>
        public void SetPrintQueue(string queueName)
        {
            if (ContainsInQueues(queueName))
            {
                Queue = PrintServer.GetPrintQueue(queueName);
                PrintTicket = Queue.UserPrintTicket.Clone();
            }
            else
                throw new ApplicationException("PrintQueue was not found");
        }
        #endregion

        #region **** Method : ContainsInQueues
        /// <summary>
        /// 指定した名称のプリントキューが存在するか？
        /// </summary>
        /// <param name="queueName">string : キュー名</param>
        /// <returns>bool</returns>
        public bool ContainsInQueues(string queueName)
        {
            return PrintServer.GetPrintQueues().Where(v => (v.Name == queueName || v.FullName == queueName)).Any();
        }
        #endregion

        #region **** Method : SetPageMediaSize(1)
        /// <summary>
        /// 用紙サイズをセットする(1)
        /// </summary>
        /// <param name="mediaSize">PageMediaSize</param>
        public void SetPageMediaSize(PageMediaSize mediaSize)
        {
            PrintTicket.PageMediaSize = mediaSize;
        }
        #endregion

        #region **** Method : SetPageMediaSize(2)
        /// <summary>
        /// 用紙サイズをセットする(2)
        /// </summary>
        /// <param name="mediaSizeName">PageMediaSizeName : 用紙サイズ名</param>
        /// <param name="width">double : 用紙幅</param>
        /// <param name="height">double :用紙高さ</param>
        public void SetPageMediaSize(PageMediaSizeName mediaSizeName, double width, double height)
        {
            PrintTicket.PageMediaSize = new PageMediaSize(mediaSizeName, width, height);
        }
        #endregion

        #region **** Method : PaperSizeSupported
        /// <summary>
        /// 用紙サイズがサポートされているか？
        /// </summary>
        /// <param name="paperSize">PageMediaSizeName : 用紙サイズ名</param>
        /// <returns>bool</returns>
        public bool SetPageMediaSizeSupported(PageMediaSizeName paperSize)
        {
            var printCap = Queue.GetPrintCapabilities();
            return printCap.PageMediaSizeCapability.Where(v => v.PageMediaSizeName == paperSize).Any();
        }
        #endregion

        #region **** Protected Method : GetDrawingContextHelper
        /// <summary>
        /// ドローイングコンテキスト用のヘルパーオブジェクトを取得する
        /// </summary>
        /// <param name="dc">DrawingContext</param>
        /// <returns>DrawingContextHelper</returns>
        protected DrawingContextHelper GetDrawingContextHelper(DrawingContext dc)
        {
            return new DrawingContextHelper(dc, OffsetX, OffsetY);
        }
        #endregion

        #region **** Protected Method : BeforePrinting (Virtual)
        /// <summary>
        /// 印刷前処理
        /// 　※ Falseを返すと印刷処理はキャンセルされる。
        /// </summary>
        /// <param name="printType">PrintType : 印刷種別</param>
        /// <param name="range">PrintRange : 印刷範囲</param>
        /// <returns>bool</returns>
        protected virtual bool BeforePrinting(PrintType printType, PrintRange range)
        {
            // 派生クラスでオーバーライドする
            return true;
        }
        #endregion

        #region **** Protected Method : BeforeDrawingPage (Virtual)
        /// <summary>
        /// ページ印刷前処理
        /// </summary>
        /// <param name="dc">DrawingContext</param>
        /// <param name="arg">ReportArgument</param>
        /// <returns>bool</returns>
        protected virtual bool BeforeDrawingPage(DrawingContext dc, ReportArgument arg)
        {
            // 派生クラスでオーバーライドする
            return true;
        }
        #endregion

        #region **** Protected Method : DrawingPage (Virtual)
        /// <summary>
        /// ページ印刷処理
        /// </summary>
        /// <param name="dc">DrawingContext</param>
        /// <param name="arg">ReportArgument</param>
        protected virtual void DrawingPage(DrawingContext dc, ReportArgument arg)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region **** Protected Method : AfterDrawingPage (Virtual)
        /// <summary>
        /// ページ印刷後処理
        /// </summary>
        /// <param name="dc">DrawingContext</param>
        /// <param name="arg">ReportArgument</param>
        protected virtual void AfterDrawingPage(DrawingContext dc, ReportArgument arg)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region **** Protected Method : Print (Virtual)
        /// <summary>
        /// 印刷処理
        /// </summary>
        /// <param name="writer">XpsDocumentWriter</param>
        /// <param name="printType">PrintType : 印刷種別</param>
        /// <param name="range">PrintRange : 印刷範囲</param>
        protected virtual void PrintProcess(XpsDocumentWriter writer, PrintType printType, PrintRange range)
        {
            var arg = new ReportArgument(printType, range);
            var collator = writer.CreateVisualsCollator();
            collator.BeginBatchWrite();
            try
            {
                do
                {
                    var container = new ContainerVisual();
                    var dv = new DrawingVisual();
                    using (var dc = dv.RenderOpen())
                    {
                        if (BeforeDrawingPage(dc, arg))
                        {
                            DrawingPage(dc, arg);
                            AfterDrawingPage(dc, arg);
                        }
                    }
                    container.Children.Add(dv);
                    collator.Write(container, PrintTicket);
                    arg.PageNo += 1;
                }
                while (!arg.PrintTerminate);
            }
            catch (Exception exp)
            {
                System.Diagnostics.Debug.WriteLine(exp.Message);
            }
            finally
            {
                if (arg.PageNo > 1)
                    collator.EndBatchWrite();
            }
        }
        #endregion

        #region **** Protected Method : AfterPrinting (Virtual)
        /// <summary>
        /// 印刷後処理
        /// </summary>
        /// <param name="printType">PrintType : 印刷種別</param>
        protected virtual void AfterPrinting(PrintType printType)
        {
            // 派生クラスでオーバーライドする
        }
        #endregion

        #region **** Method : Print
        /// <summary>
        /// 印刷処理
        /// </summary>
        /// <param name="range">PrintRange : 印刷範囲</param>
        public void Print(PrintRange range = null)
        {
            if (BeforePrinting(PrintType.Print, range))
            {
                PrintProcess(PrintQueue.CreateXpsDocumentWriter(Queue), PrintType.Print, range);
                AfterPrinting(PrintType.Print);
            }
        }
        #endregion

        #region **** Method : CreateXpsDocumet
        /// <summary>
        /// XpsDocument を生成する
        /// </summary>
        /// <param name="uriString">string : URI文字列</param>
        /// <returns>XpsDocument</returns>
        public XpsDocument CreateXpsDocumet(string uriString, PrintRange range = null)
        {
            XpsDocument xpsDoc = null;

            // 既に PackageStore上に同一URIのパッケージが存在する場合は破棄しておく
            RemoveDocument(uriString);

            if (BeforePrinting(PrintType.Print, range))
            {
                // メモリ上にXpsDocumentを生成する
                var uri = new Uri(uriString);
                var packageStream = new MemoryStream();
                var package = Package.Open(packageStream, FileMode.Create, FileAccess.ReadWrite);
                PackageStore.AddPackage(uri, package); // PackageStore に登録しないとプレビューでエラーとなる
                xpsDoc = new XpsDocument(package, CompressionOption.NotCompressed, uri.AbsoluteUri);
                PrintProcess(XpsDocument.CreateXpsDocumentWriter(xpsDoc), PrintType.Print, range);
                AfterPrinting(PrintType.Print);
            }
            return xpsDoc;
        }
        #endregion

        #region **** Method : ExportXpsFile
        /// <summary>
        /// XPSファイルへの出力処理
        /// </summary>
        /// <param name="path">string : 出力先PATH</param>
        /// <param name="range">PrintRange : 印刷範囲</param>
        public void ExportXpsFile(string path, PrintRange range = null)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException();

            if (BeforePrinting(PrintType.Print, range))
            {
                // 出力先ファイルが既に存在する場合は削除する。
                if (File.Exists(path))
                    File.Delete(path);

                using (var package = Package.Open(path, FileMode.Create))
                using (var xps = new XpsDocument(package))
                {
                    PrintProcess(XpsDocument.CreateXpsDocumentWriter(xps), PrintType.Print, range);
                    AfterPrinting(PrintType.Print);
                }
            }
        }
        #endregion

        #endregion

        #region ■　Static Method ■

        #region **** Static Method : PX2CM
        /// <summary>
        /// 論理ピクセル単位をcm単位に変換する。
        /// </summary>
        /// <param name="px">double : 論理ピクセル</param>
        /// <returns>double : cm</returns>
        public static double PX2CM(double px)
        {
            return px / 96.0d * 2.54d;
        }
        #endregion

        #region **** Static Method : CM2PX
        /// <summary>
        /// cm単位を論理ピクセル単位に変換する。
        /// </summary>
        /// <param name="cm">double : cm</param>
        /// <returns>double : 論理ピクセル</returns>
        public static double CM2PX(double cm)
        {
            return cm / 2.54d * 96.0d;
        }
        #endregion

        #region **** Static Method : RemoveDocument
        /// <summary>
        /// 生成した XpsDocument のパッケージを破棄する
        /// </summary>
        public static void RemoveDocument(string uriString)
        {
            var uri = new Uri(uriString);
            if (PackageStore.GetPackage(uri) != null)
                PackageStore.RemovePackage(uri);
        }
        #endregion

        #endregion
    }
    #endregion
}
