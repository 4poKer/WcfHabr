using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfHabrService
{
   
    [ServiceContract]
    public interface IHabrService
    {

        [OperationContract]
        string GetHabrArticleById(int habrArticleId);

        [OperationContract]
        string GetHabrArticleByKeyword(string keyword);
    }


   
}
