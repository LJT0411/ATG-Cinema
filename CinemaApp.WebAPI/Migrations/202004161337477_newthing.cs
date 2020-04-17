namespace CinemaApp.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newthing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Duration", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "Duration");
        }
    }
}
