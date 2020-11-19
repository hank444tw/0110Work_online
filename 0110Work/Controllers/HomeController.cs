using System;
using System.IO; //移動檔案需用到
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _0110Work.Models;


namespace _0110Work.Controllers
{
    public class HomeController : Controller
    {
        DB10861113Entities db = new DB10861113Entities();

        public ActionResult Index()
        {
            int index = db.Article.ToList().Count();
            if(index > 6)
            {
                index = 6;
            }
            var result = db.Article.ToList().OrderByDescending(m => m.postDate).Take(index);
            return View(result);
        }

        public ActionResult ArticleList()
        {
            var result = db.Article.ToList().OrderByDescending(m => m.postDate);
            return View(result);
        }

        public ActionResult BackArticleList()
        {
            if(Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }
            var result = db.Article.ToList().OrderByDescending(m => m.postDate);
            return View(result);
        }

        public ActionResult CreateArticle()
        {
            if (Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }
            ViewData["check"] = "";
            return View();
        }

        //新增文章
        [HttpPost]
        //[ValidateInput(false)] //取消危險驗證，如程式語法，最好不要用
        public ActionResult CreateArticle(HttpPostedFileBase file, Article article,string trueContent)
        {
            if (Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }

            //驗證
            if (file == null || file.ContentLength == 0 || article.title == null || trueContent.IndexOf("\r") != -1
                || trueContent.IndexOf("\n") != -1 || trueContent.IndexOf("amp;") != -1
                || trueContent.IndexOf("/Images/TemporaryImg/") != -1 || trueContent.IndexOf("\\") != -1
                || trueContent.IndexOf("<img alt=") != -1)
            {
                ViewData["check"] = "1";
                return View();
            }

            //檢查標題是否有重複
            var item = db.Article.Where(m => m.title == article.title).FirstOrDefault();
            if (item != null)
            {
                ViewData["check"] = "2";
                return View();
            }

            //檢查內容是否為空白
            if (article.content == null)
            {
                article.content = " ";
            }

            //儲存封面圖片
            string TemporaryImgFolder = Server.MapPath("~/Images/TemporaryImg/"); //暫存資料夾路徑
            string articleImgFolder = Server.MapPath("~/Images/ArticleImg/"); //文章圖片資料夾路徑
            string extensionName = Path.GetExtension(file.FileName); //取得副檔名
            string fileName = $"{GetRandomStringByGuid()}{extensionName}"; //呼叫亂碼方法，組成完整檔名
            file.SaveAs(articleImgFolder + fileName);

            //編輯器文字解碼處理
            string content = article.content;
            content = HttpUtility.HtmlDecode(content); //解碼
            content = content.Replace("\r", "").Replace("\n", "").Replace("\"", "").Replace("amp;", "");

            //呼叫ImgAlt方法，將圖片空白的alt補上
            content = ImgAlt(content);

            //呼叫MoveImg方法，從TemporaryImg移動所需圖片至ArticleImg
            string newImage = "";
            newImage = MoveImg(TemporaryImgFolder, articleImgFolder, content, newImage);
            content = content.Replace("/Images/TemporaryImg/", "/Images/ArticleImg/"); //更改路徑

            article.contentImage = newImage;
            article.content = content;
            article.postDate = DateTime.Now;
            article.imageName = fileName;
            article.aId = GetRandomStringByGuid();

            db.Article.Add(article);
            db.SaveChanges();
            Session["admin"] = true;
            return RedirectToAction("BackArticleList");
        }

        public ActionResult EditArticle(string aId)
        {
            if (Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }
            var result = db.Article.Where(m => m.aId == aId).FirstOrDefault();

            if (aId == "24094681494f49c897c0754188d5fee5")
            {
                return Content("<html><body><script>alert('想對SOSO做甚麼 不允許!!');</script></body></html>");
            }

            ViewData["check"] = "";
            return View(result);
        }

        //編輯文章
        [HttpPost]
        public ActionResult EditArticle(HttpPostedFileBase file, Article article, string trueContent)
        {
            if (Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }

            //驗證
            if (article.title == null || trueContent.IndexOf("\r") != -1 || trueContent.IndexOf("\n") != -1
                || trueContent.IndexOf("amp;") != -1 || trueContent.IndexOf("/Images/TemporaryImg/") != -1
                || trueContent.IndexOf("\\") != -1)
            {
                var result = db.Article.Where(m => m.aId == article.aId).FirstOrDefault();
                ViewData["check"] = "1";
                return View(result);
            }

            //檢查標題是否有重複
            var item = db.Article.Where(m => m.title == article.title).FirstOrDefault();
            if (item != null)
            {
                //檢查標題是否和原本的相同
                item = db.Article.Where(m => m.aId == article.aId).FirstOrDefault();
                if(item.title != article.title)
                {
                    ViewData["check"] = "2";
                    return View(item);
                }
            }

            if (article.content == null)
            {
                article.content = " ";
            }

            string TemporaryImgFolder = Server.MapPath("~/Images/TemporaryImg/"); //暫存資料夾路徑
            string articleImgFolder = Server.MapPath("~/Images/ArticleImg/"); //文章圖片資料夾路徑
            var old = db.Article.Where(m => m.aId == article.aId).FirstOrDefault();

            //判斷是否有再上傳封面圖片
            if (file != null && file.ContentLength != 0)
            {
                //儲存封面圖片
                string extensionName = Path.GetExtension(file.FileName); //取得副檔名
                string fileName = $"{GetRandomStringByGuid()}{extensionName}"; //呼叫亂碼方法，組成完整檔名
                file.SaveAs(articleImgFolder + fileName);

                //刪除舊封面圖片
                System.IO.File.Delete($"{articleImgFolder}{old.imageName}");

                //更新資料庫封面圖片檔名
                old.imageName = fileName;
            }

            //編輯器文字解碼處理
            string content = article.content;
            content = HttpUtility.HtmlDecode(content); //解碼
            content = content.Replace("\r", "").Replace("\n", "").Replace("\"", "").Replace("amp;", "");

            //呼叫ImgAlt方法，將圖片空白的alt補上
            content = ImgAlt(content);

            //檢查ArticleImg資料夾內是否有要刪除的圖片
            string newImage = "";
            string[] imgName = new string[20];
            int x = 0;
            int end = 0;
            for (int index = 0; x < content.Length; index++)
            {
                //判斷有無圖片
                int start = content.IndexOf("/Images/ArticleImg/", x) + 19; //19為"/Images/ArticleImg/"字元數
                if (start == 18)
                {
                    break;
                }

                //逐一從start往後找"."
                end = start;
                while (content[end].ToString() != ".")
                {
                    end++;
                }
                end = end + 4; //再加"."後的副檔名

                //取得完整圖片檔名
                imgName[index] = content.Substring(start, end - start);
                x = end;

                newImage = $"{newImage},{imgName[index]}";
            }
            string[] oldImage = old.contentImage.Split(',');
            for (int b = 1; b < oldImage.Length; b++)
            {
                if (newImage.IndexOf(oldImage[b]) == -1)
                {
                    if (System.IO.File.Exists($"{articleImgFolder}{oldImage[b]}"))
                    {
                        System.IO.File.Delete($"{articleImgFolder}{oldImage[b]}");
                    }
                }
            }

            //呼叫MoveImg方法，從TemporaryImg移動所需圖片至ArticleImg
            newImage = MoveImg(TemporaryImgFolder, articleImgFolder, content, newImage);
            content = content.Replace("/Images/TemporaryImg/", "/Images/ArticleImg/"); //更改路徑

            old.contentImage = newImage;
            old.content = content;
            old.editDate = DateTime.Now;
            old.title = article.title;
            old.littleTitle = article.littleTitle;
            db.SaveChanges();
            Session["admin"] = true;
            return RedirectToAction("BackArticleList");
        }

        //文章
        public ActionResult Article(string title)
        {
            var result = db.Article.Where(m => m.title == title).FirstOrDefault();
            if(result == null)
            {
                return Content("<html><body><script>alert('該筆文章不存在');</script></body></html>");
            }
            return View(result);
        }

        //刪除文章
        [HttpPost]
        public ActionResult DeleteArticle(string aId)
        {
            if (Session["admin"] == null)
            {
                return Content("<html><body><script>alert('請先進行登入');</script></body></html>");
            }

            var item = db.Article.Where(m => m.aId == aId).FirstOrDefault();

            if (aId == "24094681494f49c897c0754188d5fee5")
            {
                return Content("<html><body><script>alert('想對SOSO做甚麼 不允許!!');</script></body></html>");
            }

            string articleImgFolder = Server.MapPath("~/Images/ArticleImg/"); //文章圖片資料夾路徑

            //刪除封面照片
            if (System.IO.File.Exists($"{articleImgFolder}{item.imageName}"))
            {
                System.IO.File.Delete($"{articleImgFolder}{item.imageName}");
            }

            //刪除文章內圖片
            string[] contentImg = item.contentImage.Split(',');
            for(int x = 1;x < contentImg.Length; x++)
            {
                if (System.IO.File.Exists($"{articleImgFolder}{contentImg[x]}"))
                {
                    System.IO.File.Delete($"{articleImgFolder}{contentImg[x]}");
                }
            }

            //從資料庫刪除該筆資料
            db.Article.Remove(item);
            db.SaveChanges();

            return RedirectToAction("BackArticleList");
        }

        //Ajax呼叫此方法，驗證標題是否重複
        [HttpPost]
        public ActionResult CheckTitle(string title,string oldTitle)
        {
            //檢查標題是否為空字串
            if(title.Trim() == "")
            {
                return Content("null");
            }

            var item = db.Article.Where(m => m.title == title.Trim()).FirstOrDefault();

            //標題沒重複
            if(item == null)
            {
                return Content("true");
            }

            //檢查是否為EditArticle頁面
            if(oldTitle != null)
            {
                //目前文章標題
                if (item.title == oldTitle)
                {
                    return Content("3");
                }
            }

            //標題重複
            return Content("false");
        }

        //圖片替代文字
        private static string ImgAlt(string content)
        {
            if (content.IndexOf("<img alt=") != -1)
            {
                int x = 0;
                for (int index = 0; x < content.Length; index++)
                {
                    //判斷有無圖片
                    int start = content.IndexOf("<img alt=", x) + 9; //21為"<img alt="字元數
                    if (start == 8)
                    {
                        break;
                    }

                    if (content[start] == ' ')
                    {
                        content = content.Insert(start, "SOSO");
                        x = start + "SOSO".Length;
                    }
                    else
                    {
                        x = start;
                    }
                }
            }
            return content;
        }

        //從TemporaryImg移動所需圖片至ArticleImg
        private static string MoveImg(string TemporaryImgFolder, string articleImgFolder, string content, string newImage)
        {
            if (content.IndexOf("/Images/TemporaryImg/") != -1)
            {
                string[] imgName = new string[20];
                int x = 0;
                int end = 0;
                for (int index = 0; x < content.Length; index++)
                {
                    //判斷有無圖片
                    int start = content.IndexOf("/Images/TemporaryImg/", x) + 21; //21為"/Images/TemporaryImg/"字元數
                    if (start == 20)
                    {
                        break;
                    }

                    //逐一從start往後找"."
                    end = start;
                    while (content[end].ToString() != ".")
                    {
                        end++;
                    }
                    end = end + 4; //再加"."後的副檔名

                    //取得完整圖片檔名
                    imgName[index] = content.Substring(start, end - start);
                    x = end;

                    //將圖片檔名存入資料庫
                    newImage = $"{newImage},{imgName[index]}";

                    //判斷檔案存在與否，並移動
                    if (System.IO.File.Exists(TemporaryImgFolder + imgName[index]))
                    {
                        System.IO.File.Move(TemporaryImgFolder + imgName[index], articleImgFolder + imgName[index]);
                    };
                }
            }
            return newImage;
        }

        //上傳圖片至伺服器TemporaryImg資料夾
        [HttpPost]
        public ActionResult UploadEditorImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string result = "";
            if (upload != null && upload.ContentLength > 0)
            {
                string TemporaryImgFolder = "~/Images/TemporaryImg/"; //暫存資料夾虛擬路徑
                string extensionName = Path.GetExtension(upload.FileName); //取得副檔名

                //驗證副檔名
                if(extensionName.IndexOf("jpg") == -1 && extensionName.IndexOf("png") == -1)
                {
                    return Content(@"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + "1" + ", \"" + "" + "\", \"" + "圖片格式只能為jpg或png" + "\");</script></body></html>");
                }

                string fileName = $"{GetRandomStringByGuid()}{extensionName}"; //呼叫亂碼方法，組成完整檔名

                //儲存圖片至Server
                upload.SaveAs(Server.MapPath($"{TemporaryImgFolder}{fileName}"));

                var imageUrl = Url.Content($"{TemporaryImgFolder}{fileName}");
                var vMessage = string.Empty;
                result = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + imageUrl + "\", \"" + vMessage + "\");</script></body></html>";
            }
            return Content(result);
        }

        //使用Guid產生亂碼
        public static string GetRandomStringByGuid()
        {
            string str = Guid.NewGuid().ToString().Replace("-", ""); //將"-"字號去掉
            return str;
        }

        //登入
        [HttpPost]
        public ActionResult LoginIn(string password)
        {
            if(password.Trim() != "admin")
            {
                return RedirectToAction("Index");
            }
            Session["admin"] = true;
            return RedirectToAction("BackArticleList");
        }

        //登出
        public ActionResult LoginOut()
        {
            Session["admin"] = null;
            return RedirectToAction("Index");
        }

        //未完成頁面
        public ActionResult GGGGG()
        {
            return PartialView();
        }

        //SOSO大事紀
        public ActionResult SOSOevents()
        {
            return View();
        }
        
        public ActionResult About()
        {
            var title = db.Article.ToList();
            return View(title);
        }

        [HttpPost]
        public ActionResult About(Article article, int type)
        {
            if (type == 0)
            {
                var old = db.Article;
                old.Add(article);
            }

            if (type == 1)
            {
                var old = db.Article.Where(m => m.title == article.title).FirstOrDefault();
                old.littleTitle = article.littleTitle;
                old.title = article.title;
            }

            if (type == 2)
            {
                var old = db.Article.Where(m => m.title == article.title).FirstOrDefault();
                db.Article.Remove(old);
            }

            db.SaveChanges();
            return RedirectToAction("About");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
