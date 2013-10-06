using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleSearchAPI.Query;
//using GoogleSearchAPI.Resources;
using GoogleSearchAPI;

namespace SearchUtils
{
    //this is for avoiding collisions between 'CommonObjects.Language' and 'GoogleSearchAPI.Resources.Languages'
    using gLanguages =  GoogleSearchAPI.Resources.Languages;
    using CountryCode =  GoogleSearchAPI.Resources.CountryCode;
    using NewsEdition =  GoogleSearchAPI.Resources.NewsEdition;
    /// <summary>
    /// Class for handling search logics
    /// </summary>
    public static class SearchLogics
    {
        public static void Search(string query, CommonObjects.Language language)
        {
            //-- Usage example
            WebQuery wQuery = new WebQuery(query);
            wQuery.StartIndex.Value = 2;
            wQuery.HostLangauge.Value = language.EnglishName;// gLanguages.English;
            IGoogleResultSet<GoogleWebResult> resultSet = GoogleService.Instance.Search<GoogleWebResult>(wQuery);
            resultSet.ToString();
        }
    }
}
