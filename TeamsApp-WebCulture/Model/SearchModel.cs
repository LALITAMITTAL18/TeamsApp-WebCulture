using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamsApp_WebCulture
{
    public class SearchResult
    {
        public SearchModels d { get; set; }
    }
    public class SearchModels
    {
        public SearchModel[] results { get; set; }
    }

    public class SearchModel
    {
       
        public string ID { get; set; }
        public string EntityType { get; set; }
       
        public string DivisionDescription { get; set; }
       
        public string Description { get; set; }
       
    }
}