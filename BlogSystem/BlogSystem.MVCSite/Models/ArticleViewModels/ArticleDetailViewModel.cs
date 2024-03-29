﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSystem.MVCSite.Models.ArticleViewModels
{
    public class ArticleDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
       

        public string Email { get; set; }

        public int GoodCount { get; set; }

        public int BadCount { get; set; }
        public string ImgPath { get; set; }
    
        public string[] CategoryNames { get; set; }
        public Guid[] CategoryIds { get; set; }
    }
}