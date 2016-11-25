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
       
        public HabrArticle GetHabrArticleById(int habrArticleId)
        {

            return HabrArticleController.GetDataById(habrArticleId);

        }

        public HabrArticle GetHabrArticleByKeyword(string keyword)
        {
            return HabrArticleController.FindArticleByKeyword(keyword);

        }

    }
}
