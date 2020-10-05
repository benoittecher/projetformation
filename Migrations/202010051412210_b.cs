namespace WebApplicationFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modules", "Parcours_Id", "dbo.Parcours");
            DropIndex("dbo.Modules", new[] { "Parcours_Id" });
            CreateTable(
                "dbo.ParcoursModules",
                c => new
                    {
                        Parcours_Id = c.Int(nullable: false),
                        Module_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Parcours_Id, t.Module_Id })
                .ForeignKey("dbo.Parcours", t => t.Parcours_Id, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.Module_Id, cascadeDelete: true)
                .Index(t => t.Parcours_Id)
                .Index(t => t.Module_Id);
            
            DropColumn("dbo.Modules", "Parcours_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Modules", "Parcours_Id", c => c.Int());
            DropForeignKey("dbo.ParcoursModules", "Module_Id", "dbo.Modules");
            DropForeignKey("dbo.ParcoursModules", "Parcours_Id", "dbo.Parcours");
            DropIndex("dbo.ParcoursModules", new[] { "Module_Id" });
            DropIndex("dbo.ParcoursModules", new[] { "Parcours_Id" });
            DropTable("dbo.ParcoursModules");
            CreateIndex("dbo.Modules", "Parcours_Id");
            AddForeignKey("dbo.Modules", "Parcours_Id", "dbo.Parcours", "Id");
        }
    }
}
