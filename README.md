# SOSO部落格
> * _Google搜尋"SOSO部落格"_  
> * _CMS及架站練習_   

* 功能說明
  1. 閱讀部落格文章。
  2. 頁面互動效果(Ex:大事記頁面的時間軸效果)。
  3. 線上撰寫文章，上傳文章圖片。
  4. 文章管理CRUD。
  5. RWD響應式網頁。
 
* 使用技術
  1. ASP.NET MVC
  2. C#
  3. js
  4. css
  5. Ajax
  6. Entity framework
  7. Linq   

* 開發工具
  1. Visual Studio 2017
  2. Sourcetree

* 架站技術
  1. iis
  2. Dns
  3. ssl
  4. Google Search Console Tools
  
* 網站架構
  |        | 說明 |程式 |
  |------- |:-----|:------:|
  | **前端**   |  1. 有兩條`navbar`，透過撰寫`js`程式，當頁面寬度小於`992px`，轉換為手機平板用的。 <br>2. 當頁面寬度小於`768px`，轉換為直式的`banner`。</br> 3. 透過`css`和`js`實現一些頁面動態的效果(Ex:`css`的`transform`屬性、`js`的`事件監聽`)。</br>4. 使用`aos`套件搭配`js事件監聽`實現大事記頁面區塊飛升效果。</br>5. 使用`ckeditor`文章編輯器套件，再`js`驗證圖片上傳格式，和取得文章內容回傳後端。 |  [程式碼](https://github.com/hank444tw/0110Work_online/tree/newmaster/0110Work/Views) |
  | **後端**   |  1. 以`LINQ`語法透過`model`，對資料庫進行存取。</br> 2. 接收前端文章資料，儲存至資料庫。</br> 3. 使用者在文章編輯器上傳的圖片，於文章`submit`後，將圖片移置image資料夾，</br>&emsp;其餘沒用到的則刪除。</br> 4. 捨棄`asp.net mvc`預設的萬用`Route`，直接以文章標題作為URL。 |  [程式碼](https://github.com/hank444tw/0110Work_online/blob/newmaster/0110Work/Controllers/HomeController.cs)</br> [程式碼](https://github.com/hank444tw/0110Work_online/blob/newmaster/0110Work/App_Start/RouteConfig.cs) |
  | **資料庫** | 1. 使用`ASP.NET MVC`的`Entity Framework`進行資料庫設計。</br> 2. 建置`Model`來對資料庫進行存取。| [程式碼](https://github.com/hank444tw/0110Work_online/tree/newmaster/0110Work/Models) |
  | **伺服器** |  1. 使用`Microsoft`的`IIS`服務架站。</br> 2. 使用`no-ip`免費`DNS`服務，以及免費的網域名稱(每月需手動更新一次)。</br> 3. 使用`SSL for free`網站，以檔案驗證方式取得`SSL`憑證。</br>&emsp;之後再以`openSSL`網站服務將憑證原本的`crt`和`key`檔，轉為`IIS`可接受的`pfx`檔。</br> 4. 使用`google search console`服務，透過檔案驗證方式，驗證擁有權，</br>&emsp;並且提交`sitemap`，增加`SEO`分數。 |     

* 網站截圖
<img src="https://github.com/hank444tw/0110Work_online/blob/newmaster/Demo1.jpg" stryle="float:right" />  

<img src="https://github.com/hank444tw/0110Work_online/blob/newmaster/Demo2.jpg" stryle="float:right" />    

<img src="https://github.com/hank444tw/0110Work_online/blob/newmaster/Demo3.jpg" stryle="float:right" />     

<img src="https://github.com/hank444tw/0110Work_online/blob/newmaster/Demo4.jpg" stryle="float:right" />
