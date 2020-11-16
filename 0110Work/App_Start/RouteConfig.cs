using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _0110Work
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Homepage",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "ArticleList",
                url: "ArticleList",
                defaults: new { controller = "Home", action = "ArticleList" }
            );

            routes.MapRoute(
                name: "Admin",
                url: "Admin",
                defaults: new { controller = "Home", action = "BackArticleList" }
            );

            routes.MapRoute(
                name: "CreateArticle",
                url: "Admin/CreateArticle",
                defaults: new { controller = "Home", action = "CreateArticle" }
            );

            routes.MapRoute(
                name: "UploadEditorImage",
                url: "Admin/UploadEditorImage",
                defaults: new { controller = "Home", action = "UploadEditorImage" }
            );

            routes.MapRoute(
                name: "EditArticle",
                url: "Admin/EditArticle/{aId}",
                defaults: new { controller = "Home", action = "EditArticle"}
            );

            routes.MapRoute(
                name: "DeleteArticle",
                url: "Admin/DeleteArticle",
                defaults: new { controller = "Home", action = "DeleteArticle"}
            );

            routes.MapRoute(
                name: "CheckTitle",
                url: "Admin/CheckTitle",
                defaults: new { controller = "Home", action = "CheckTitle" }
            );

            routes.MapRoute(
                name: "Article",
                url: "Article/{title}",
                defaults: new { controller = "Home", action = "Article" }
            );

            routes.MapRoute(
                name: "LoginIn",
                url: "LoginIn",
                defaults: new { controller = "Home", action = "LoginIn" }
            );

            routes.MapRoute(
                name: "LoginOut",
                url: "LoginOut",
                defaults: new { controller = "Home", action = "LoginOut" }
            );

            routes.MapRoute(
                name: "GGGGG",
                url: "GGGGG",
                defaults: new { controller = "Home", action = "GGGGG" }
            );

            routes.MapRoute(
                name: "SOSOevents",
                url: "SOSOevents",
                defaults: new { controller = "Home", action = "SOSOevents" }
            );
        }
    }
}
