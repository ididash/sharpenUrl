namespace URLShortener.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shortenerUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserGuid = c.Guid(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserUrl",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        OriginUrl = c.String(maxLength: 2000),
                        CompactUrl = c.String(maxLength: 128),
                        CreateOn = c.DateTime(nullable: false),
                        NumberOfViews = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserUrl", "UserId", "dbo.User");
            DropIndex("dbo.UserUrl", new[] { "UserId" });
            DropTable("dbo.UserUrl");
            DropTable("dbo.User");
        }
    }
}
