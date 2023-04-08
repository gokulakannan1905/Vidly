namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameColumnToMembershipTypesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "Name", c => c.String(nullable: false, maxLength: 50));
            Sql("update membershiptypes set name = 'Pay as you go' where id = 1");
            Sql("update membershiptypes set name = 'Monthly' where id = 2");
            Sql("update membershiptypes set name = 'Quarterly' where id = 3");
            Sql("update membershiptypes set name = 'Annual' where id = 4");

        }

        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "Name");
        }
    }
}
