using System;
using System.Data.Entity.Migrations;

public partial class AddMigration : DbMigration
{
    public override void Up()
    {
        AddColumn("dbo.Food", "OwnerId", c => c.Guid(nullable: false));
    }
    
    public override void Down()
    {
        DropColumn("dbo.Food", "OwnerId");
    }
}
