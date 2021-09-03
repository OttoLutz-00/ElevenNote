namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "CreatedUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.Note", "CreateUtc");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Note", "CreateUtc", c => c.DateTimeOffset(nullable: false, precision: 7));
            DropColumn("dbo.Note", "CreatedUtc");
        }
    }
}
