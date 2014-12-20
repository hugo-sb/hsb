using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace hsb.WPF.Extensions
{
    #region 【Static Class : CanvasExtension】
    /// <summary>
    /// Canvas用の拡張メソッド
    /// </summary>
    public static class CanvasExtension
    {
        #region ■ Extension Methods ■

        #region **** Extension Method : toImage
        /// <summary>
        /// Canvasの内容を画像ファイルに出力する
        /// </summary>
        /// <param name="canvas">Canvas : 自身</param>
        /// <param name="path">string : 出力ファイルPATH</param>
        /// <param name="encoder">BitmapEncoder : エンコーダー</param>
        public static void toImage(this Canvas canvas, string path, BitmapEncoder encoder = null)
        {
            // レイアウトを再計算させる
            var size = new Size(canvas.Width, canvas.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // VisualObjectをBitmapに変換する
            var renderBitmap = new RenderTargetBitmap((int)size.Width,       // 画像の幅
                                                      (int)size.Height,      // 画像の高さ
                                                      96.0d,                 // 横96.0DPI
                                                      96.0d,                 // 縦96.0DPI
                                                      PixelFormats.Pbgra32); // 32bit(RGBA各8bit)
            renderBitmap.Render(canvas);

            // 出力用の FileStream を作成する
            using (var os = new FileStream(path, FileMode.Create))
            {
                // 変換したBitmapをエンコードしてFileStreamに保存する。
                // BitmapEncoder が指定されなかった場合は、PNG形式とする。
                encoder = encoder ?? new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(os);
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
