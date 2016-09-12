namespace Demo.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        Address = c.String(unicode: false),
                        IsDel = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
           
        }
        
        public override void Down()
        {
            DropTable("dbo.Customers");
        }
    }
}
