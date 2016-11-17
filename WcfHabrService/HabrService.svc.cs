using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfHabrService
{
    public class Service1 : IHabrService
    {
       
        public string GetHabrArticleById(int habrArticleId)
        {

            var habrArticle = HabrArticleController.GetDataById(habrArticleId);

            return HabrArticleController.GetOutputDataString(habrArticle);
        }

        public string GetHabrArticleByKeyword(string keyword)
        {
            var habrArticle = HabrArticleController.FindArticleByKeyword(keyword);

            return HabrArticleController.GetOutputDataString(habrArticle);
        }

    }
}
