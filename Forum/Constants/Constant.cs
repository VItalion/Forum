using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Constants
{
    public static class Constant
    {
        public const int PageSize = 10;

        public static class View
        {
            public const string SearchResult = "SearchResult";
            public const string Index = "Index";
            public const string Post = "Post";
            public const string GetPost = "GetPost";
            public const string Comments = "Comments";
            public const string ChangeComment = "ChangeComment";
        }
    }
}