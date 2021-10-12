namespace EmployeeLeave.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class l : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "EmpName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "EmpName");
        }
    }
}
