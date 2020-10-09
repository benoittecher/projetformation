namespace WebApplicationFormation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class placesdispo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "NbPlacesTotal", c => c.Int(nullable: false));
            DropColumn("dbo.Sessions", "NbInscrits");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "NbInscrits", c => c.Int(nullable: false));
            DropColumn("dbo.Sessions", "NbPlacesTotal");
        }
    }
}
