namespace BlogSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Contents = c.String(nullable: false, storeType: "ntext"),
                        UserId = c.Guid(nullable: false),
                        GoodCount = c.Int(nullable: false),
                        BadCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false, maxLength: 40, unicode: false),
                        Password = c.String(nullable: false, maxLength: 30, unicode: false),
                        ImgPath = c.String(nullable: false, maxLength: 300, unicode: false),
                        FansCount = c.Int(nullable: false),
                        FoucsCount = c.Int(nullable: false),
                        SiteName = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArticleToCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BlogCategoryId = c.Guid(nullable: false),
                        BlogArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.BlogArticleId)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId)
                .Index(t => t.BlogCategoryId)
                .Index(t => t.BlogArticleId);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(),
                        UserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentContent = c.String(nullable: false, maxLength: 800),
                        UserId = c.Guid(nullable: false),
                        ArticleId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Articles", t => t.ArticleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.Fans",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        FoucsUserId = c.Guid(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.FoucsUserId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.FoucsUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fans", "UserId", "dbo.Users");
            DropForeignKey("dbo.Fans", "FoucsUserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.ArticleToCategories", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogCategories", "UserId", "dbo.Users");
            DropForeignKey("dbo.ArticleToCategories", "BlogArticleId", "dbo.Articles");
            DropForeignKey("dbo.Articles", "UserId", "dbo.Users");
            DropIndex("dbo.Fans", new[] { "FoucsUserId" });
            DropIndex("dbo.Fans", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.BlogCategories", new[] { "UserId" });
            DropIndex("dbo.ArticleToCategories", new[] { "BlogArticleId" });
            DropIndex("dbo.ArticleToCategories", new[] { "BlogCategoryId" });
            DropIndex("dbo.Articles", new[] { "UserId" });
            DropTable("dbo.Fans");
            DropTable("dbo.Comments");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.ArticleToCategories");
            DropTable("dbo.Users");
            DropTable("dbo.Articles");
        }
    }
}
