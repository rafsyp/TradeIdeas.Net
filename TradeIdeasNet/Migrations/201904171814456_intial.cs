namespace TradeIdeasNet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Trades", "DateAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trades", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
