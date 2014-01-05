namespace eftesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SimplifyModelClassesForTesting : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Rooms", "CreatedDate");
            DropColumn("dbo.Rooms", "LastModifiedDate");
            DropColumn("dbo.Streams", "DateCreated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Streams", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "LastModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Rooms", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
