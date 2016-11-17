using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MongoDB.Bson;

namespace WcfHabrService
{
    public static class HabrArticleController
    {
        public static HabrArticle GetDataById(int articleId)
        {

            var habrArticle = MongoDbDriver.GetHabrArticleByIdFromDb(articleId);

            if(habrArticle == null)
            {
                habrArticle = GetHabrArticleFromInternet(articleId);

                if(habrArticle != null)
                    MongoDbDriver.AddHabrArticleToDb(habrArticle);
            }

            return habrArticle;

        }

        public static HabrArticle FindArticleByKeyword(string keyword)
        {

            var searchPage = Resources.searchString + keyword;

            var web = new HtmlWeb();
            var document = web.Load(searchPage);

            var searchArticle = document.DocumentNode.SelectSingleNode("//div[contains(@class,'post_teaser')]");

            if (searchArticle != null)
            {

                var articleId = Convert.ToInt32(searchArticle.Id.Split('_')[1]);

                return GetDataById(articleId);

            }
            else
            {

                return null;
            }

        }

        public static HabrArticle GetHabrArticleFromInternet(int articleId)
        {

            HtmlDocument document;

            try
            {

                var web = new HtmlWeb();
                document = web.Load(Resources.postString + articleId + "/");

            }
            catch (System.Net.WebException ex)
            {

                return null;

            }

            try
            {

                var tags = document.DocumentNode.SelectNodes("//a[contains(@class,'hub')]");
                var tempTags = new List<string>();                        
                tempTags.AddRange(tags.Select(x => x.InnerText));

             
                var habrArticle = new HabrArticle
                {
                    HabrId = articleId,
                    Theme = document.DocumentNode.SelectSingleNode("//h1[contains(@class,'post__title')]//a").InnerText,
                    Title = document.DocumentNode.SelectNodes("//h1[contains(@class,'post__title')]//span")[1].InnerText,
                    Tags = tempTags,
                    PublicationDate = document.DocumentNode.SelectSingleNode("//span[contains(@class,'post__time_published')]").InnerText

                };

                    return habrArticle;

                }
                catch (Exception ex)
                {
      
                    return null;

                }
        }

        public static string GetOutputDataString(HabrArticle habrArticle)
        {
            if (habrArticle != null)
            {
                var tagsString = "";

                foreach(var tag in habrArticle.Tags)
                    tagsString += "\n" + tag;

                return "\n\nТема: " + habrArticle.Theme + "\n\nНазвание статьи:\n" + habrArticle.Title + "\n\nТеги:\n" + tagsString + "\n\nДата публикации статьи: " + habrArticle.PublicationDate;
            }
            else
            {
                return "Статья не найдена =(";
            }
        }
    }
}
