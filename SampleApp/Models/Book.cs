using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Utils;
using hsb.WPF;

namespace SampleApp.Models
{
    #region 【Enums】

    #region **** Enum : BookCategories
    /// <summary>
    /// 書籍カテゴリー
    /// </summary>
    public enum BookCategories
    {
        Novel,
        Nonfiction,
        Comic
    }
    public static class BookCategoriesExtension
    {
        public static string ToDisplayName(this BookCategories e)
        {
            var names = new string[] { "小説", "ノンフィクション", "漫画" };
            return names[(int)e];
        }
    }    
    #endregion

    #endregion

    #region 【Class : Book】
    /// <summary>
    /// 書籍クラス
    /// </summary>
    class Book : DataBindModelBase
    {
        #region ■ Constructor ■

        #region **** Constructor(1)
        /// <summary>
        /// コンストラクタ(1)
        /// </summary>
        public Book()
        {
            // プロパティ値の初期設定

            // Id
            IdProperty = CreateDataBindProperty<int>("Id", 0);

            // 書籍名
            TitleProperty = CreateDataBindProperty<string>("Title", null, v => 
                { 
                    v.CreateValidator(s => !String.IsNullOrEmpty(s), "書名が未入力です。");
                    v.IOFilter = IOFilters.Trim;
                    v.Description = "書籍名を入力してください。";
                });

            // 著者
            AuthorProperty = CreateDataBindProperty<string>("Author", null, v => 
                {
                    v.CreateValidator(s => !String.IsNullOrEmpty(s), "著者名が未入力です。");
                    v.IOFilter = IOFilters.Trim;
                    v.Description = "著者名を入力してください。";
                });

            // 出版社
            PublisherProperty = CreateDataBindProperty<string>("Publisher", null, v => 
                {
                    v.CreateValidator(s => !String.IsNullOrEmpty(s), "出版社が未入力です。");
                    v.IOFilter = IOFilters.Trim;
                    v.Description = "出版社名を入力してください。";
                });

            // カテゴリ
            CategoryProperty = CreateDataBindProperty<BookCategories?>("Category", null, v => 
                {
                    v.CreateValidator(n => n.HasValue, "カテゴリーが未入力です。");
                    v.Description = "カテゴリーを指定してください。";
                });

            // 価格
            PriceProperty = CreateDataBindProperty<decimal?>("Price", null, v =>
                {
                    v.CreateValidator(n => n >= 0, "価格が不正です。");
                    v.Description = "価格を入力してください。";
                });

            // 購入日
            PurchaseDateProperty = CreateDataBindProperty<DateTime?>("PurchaseDate", null, v =>
                {
                    v.CreateValidator(d => d.HasValue, "購入日が未入力です。");
                    v.Description = "購入日を入力してください。";
                });

            // 評価点
            ReviewPointProperty = CreateDataBindProperty<int?>("ReviewPoint", null, v =>
                {
                    v.CreateValidator(n => (n >= 0 && n <= 5), "評価点が不正です。");
                    v.Description = "評価点を指定してください。";
                });
        }
        #endregion

        #region **** Constructor(2)
        /// <summary>
        /// コンストラクタ(2)
        /// </summary>
        /// <param name="id">int : ID</param>
        /// <param name="title">string : タイトル</param>
        /// <param name="author">string : 著者</param>
        /// <param name="publisher">string : 出版社</param>
        /// <param name="category">BookCategories : カテゴリー</param>
        /// <param name="price">decimal : 価格</param>
        /// <param name="purchaseDate">DateTime? : 購入日</param>
        /// <param name="reviewPoint">int : 評価点</param>
        public Book(int id, string title, string author, string publisher, BookCategories category, decimal price, DateTime? purchaseDate, int reviewPoint)
            : this()
        {
            IdProperty.Init(id);
            TitleProperty.Init(title);
            AuthorProperty.Init(author);
            PublisherProperty.Init(publisher);
            CategoryProperty.Init(category);
            PriceProperty.Init(price);
            PurchaseDateProperty.Init(purchaseDate);
            ReviewPointProperty.Init(reviewPoint);
        }
        #endregion

        #region **** Constructor(3)
        /// <summary>
        /// コンストラクタ(3)
        /// </summary>
        /// <param name="book">Book</param>
        public Book(Book book)
            : this()
        {
            IdProperty.Init(book.Id);
            TitleProperty.Init(string.Format("*{0}",book.Title));
            AuthorProperty.Init(book.Author);
            PublisherProperty.Init(book.Publisher);
            CategoryProperty.Init(book.Category);
            PriceProperty.Init(book.Price);
            PurchaseDateProperty.Init(book.PurchaseDate);
            ReviewPointProperty.Init(book.ReviewPoint);
        }
        #endregion

        #endregion

        #region ■ Properties ■

        #region **** Property : Id (Read Only)
        /// <summary>
        /// ID値
        /// </summary>
        public DataBindPropertyItem<int> IdProperty { get; private set; }
        public int Id
        {
            get { return IdProperty.Value; }
        }
        #endregion

        #region **** Property : Title
        /// <summary>
        /// 書籍名
        /// </summary>
        public DataBindPropertyItem<string> TitleProperty { get; private set; }
        public string Title
        {
            get { return TitleProperty.Value; }
            set { TitleProperty.Value = value; }
        }
        #endregion

        #region **** Property : Author
        /// <summary>
        /// 作者名
        /// </summary>
        public DataBindPropertyItem<string> AuthorProperty { get; private set; }
        public string Author
        {
            get { return AuthorProperty.Value; }
            set { AuthorProperty.Value = value; }
        }
        #endregion

        #region **** Property : Publisher
        /// <summary>
        /// 出版社
        /// </summary>
        public DataBindPropertyItem<string> PublisherProperty { get; private set; }
        public string Publisher
        {
            get { return PublisherProperty.Value; }
            set { PublisherProperty.Value = value; }
        }
        #endregion

        #region **** Property : Category
        /// <summary>
        /// カテゴリー
        /// </summary>
        public DataBindPropertyItem<BookCategories?> CategoryProperty { get; private set; }
        public BookCategories? Category
        {
            get { return CategoryProperty.Value; }
            set { CategoryProperty.Value = value; }
        }
        #endregion

        #region **** Property : Price
        /// <summary>
        /// 価格
        /// </summary>
        public DataBindPropertyItem<decimal?> PriceProperty { get; private set; }
        public decimal? Price
        {
            get { return PriceProperty.Value; }
            set { PriceProperty.Value = value; }
        }
        #endregion

        #region **** Property : PurchaseDate
        /// <summary>
        /// 購入日
        /// </summary>
        public DataBindPropertyItem<DateTime?> PurchaseDateProperty { get; private set; }
        public DateTime? PurchaseDate
        {
            get { return PurchaseDateProperty.Value; }
            set { PurchaseDateProperty.Value = value; }
        }
        #endregion

        #region **** Property : ReviewPoint
        /// <summary>
        /// 評価点
        /// </summary>
        public DataBindPropertyItem<int?> ReviewPointProperty { get; private set; }
        public int? ReviewPoint
        {
            get { return ReviewPointProperty.Value; }
            set { ReviewPointProperty.Value = value; }
        }
        #endregion

        #endregion

        #region ■ Methods ■

        #region **** Method : ToString (Override)
        /// <summary>
        /// 文字列化
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Title, Author);
        }
        #endregion

        #region **** Method : Update
        /// <summary>
        /// 書籍情報を更新する（ダミー）
        /// </summary>
        public void Update()
        {
            if (Id == 0)
            {
                BookCount++;
                IdProperty.Init(BookCount);
            }
            Clean();
        }
        #endregion

        #region **** Method : Delete
        /// <summary>
        /// 書籍情報を削除する（ダミー）
        /// </summary>
        public void Delete()
        {
            IdProperty.Init(0);
        }
        #endregion

        #region **** Method : Clone
        /// <summary>
        /// 自身を複製する
        /// </summary>
        /// <returns>Book</returns>
        public Book Clone()
        {
            return new Book(this);
        }
        #endregion

        #endregion

        #region ■ Static Constructor ■
        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static Book()
        {
            BookCount = 0;
        }
        #endregion

        #region ■ Static Property ■

        #region **** Private Static Property : BookCount
        /// <summary>
        /// 書籍の数
        /// </summary>
        private static int BookCount { get; set; }
        #endregion

        #endregion

        #region ■ Static Methods ■

        #region **** Static Method : GetBookList
        /// <summary>
        /// 蔵書リストを取得する
        /// </summary>
        /// <returns>List of Book</returns>
        public static List<Book> GetBookList()
        {
            var list = new List<Book>
            {
                new Book(1, "ドグラマグラ", "夢野久作", "三一書房", BookCategories.Novel, 1200m, new DateTime(2014,1,10), 5),
                new Book(2, "黒死館殺人事件", "小栗虫太郎", "社会思想社", BookCategories.Novel, 800m, new DateTime(2014,2,12), 3),
                new Book(3, "虚無への供物", "中井英夫", "講談社", BookCategories.Novel, 500m, new DateTime(2014,3,7), 4),
                new Book(4, "孤島の鬼", "江戸川乱歩", "講談社", BookCategories.Novel, 250m, new DateTime(2014,5,20), 3),
                new Book(5, "魔都", "久生十蘭", "三一書房", BookCategories.Novel, 950m, new DateTime(2014,6,10), 4),
                new Book(6, "消えるヒッチハイカー", "J.H.ブルンヴァン", "新宿書房", BookCategories.Nonfiction, 1200m, new DateTime(2014,7,21), 4),
                new Book(7, "メディア・レイプ", "ブラアン・キイ", "リブロポート", BookCategories.Nonfiction, 105m, new DateTime(2014,7,21), 4),
                new Book(8, "魔女", "五十嵐大介", "小学館", BookCategories.Comic, 330m, new DateTime(2014,8,2), 4),
                new Book(9, "百日紅", "杉浦日向子", "筑摩書房", BookCategories.Comic, 550m, new DateTime(2014,9,10), 4),
                new Book(10, "ネガティブ", "イタガキノブオ", "青林堂", BookCategories.Comic, 250m, new DateTime(2014,9,10), 4)
            };
            BookCount += list.Count;
            return list;
        }
        #endregion

        #endregion
    }
    #endregion

}
