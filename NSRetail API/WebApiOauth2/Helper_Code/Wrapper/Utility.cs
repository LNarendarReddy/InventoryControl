using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace NSRetail.Helper_Code.Wrapper
{
    public static class Utility
    {
        
        //public static string stCategoryImageURL = "http://localhost:8080/oms/resources/categoryimages/";
        //public static string stArticleImageURl = "http://localhost:8080/oms/resources/articleimages/";
        //public static string stCategoryImageURL = "https://3.122.143.49:8443/oms/resources/categoryimages/";
        //public static string stArticleImageURl = "https://3.122.143.49:8443/oms/resources/articleimages/";
        //public static string stArticleImageURL = "http://85.214.68.67/omsapi/images/";
        //public static string stArticleMediaURl = "http://85.214.68.67/omsapi/Media/";
        public static string stCategoryImageURL = "http://81.169.253.148:8080/oms/resources/categoryimages/";
        public static string stArticleImageURl = "http://81.169.253.148:8080/oms/resources/articleimages/";
        //public static string stArticleMediaURl = "http://81.169.253.148/ottoomswebapi/Media/";
        public static string FileExists(string url, string image)
        {
            string stPath = string.Empty;
            stPath = url + image;
            if (File.Exists(stPath))
                return stPath;
            else
                stPath = "null";
            return stPath;
        }
    }
}