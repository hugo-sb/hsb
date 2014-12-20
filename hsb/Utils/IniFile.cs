using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace hsb.Utils
{
    #region 【Class : IniFile】
    /// <summary>
    /// INI形式ファイル操作クラス
    /// </summary>
    public class IniFile
    {
        #region ■ DLL Imports ■

        #region **** DLL Import : GetPrivateProfileString
        /// <summary>
        /// INIファイルから文字列を取得する
        /// </summary>
        /// <param name="lpApplicationName">string</param>
        /// <param name="lpKeyName">string</param>
        /// <param name="lpDefault">string</param>
        /// <param name="lpReturnedstring">StringBuilder</param>
        /// <param name="nSize">int</param>
        /// <param name="lpFileName">string</param>
        /// <returns>int</returns>
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedstring,
            int nSize,
            string lpFileName);
        #endregion

        #region **** DLL Import : WritePrivateProfileString
        /// <summary>
        /// INIファイルへ文字列を書き込む
        /// </summary>
        /// <param name="lpApplicationName">string</param>
        /// <param name="lpKeyName">string</param>
        /// <param name="lpstring">string</param>
        /// <param name="lpFileName">string</param>
        /// <returns>int</returns>
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(
            string lpApplicationName,
            string lpKeyName,
            string lpstring,
            string lpFileName);
        #endregion

        #endregion

        #region ■ Properties ■

        #region **** Property : Path (Set private only)
        /// <summary>
        /// INIファイルのPATH
        /// </summary>
        public string Path { get; private set; }
        #endregion

        #region Property : Items
        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="section">string : セクション名</param>
        /// <param name="key">string : KEY名</param>
        /// <returns>string</returns>
        public string this[string section, string key]
        {
            get { return ReadString(section, key); }
            set { WriteString(section, key, value); }
        }
        #endregion

        #endregion

        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">string : INIファイルへのPATH</param>
        public IniFile(string path)
        {
            Path = path;
        }
        #endregion

        #region ■ Methods ■

        #region **** Method : ReadString
        /// <summary>
        /// 文字列を取得する
        /// </summary>
        /// <param name="section">string : セクション名</param>
        /// <param name="key">string : KEY名</param>
        /// <returns>string</returns>
        public string ReadString(string section, string key)
        {
            var sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, Path);
            return sb.ToString();
        }
        #endregion

        #region **** Method : WriteString
        /// <summary>
        /// 文字列を書き込む
        /// </summary>
        /// <param name="section">string : セクション名</param>
        /// <param name="key">string : KEY名</param>
        /// <param name="value">string : 値</param>
        public void WriteString(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, Path);
        }
        #endregion

        #endregion
    }
    #endregion

}
