namespace EmployeeLeave.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmpId = c.Int(nullable: false, identity: true),
                        EmpName = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Mobile = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        PMId = c.Int(nullable: false),
                        IsHR = c.Boolean(nullable: false),
                        IsManager = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpId)
                .ForeignKey("dbo.Projects", t => t.PMId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PMId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        PMId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                    })
                .PrimaryKey(t => t.PMId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveId = c.Int(nullable: false, identity: true),
                        EmpId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        status = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.LeaveId)
                .ForeignKey("dbo.Employees", t => t.EmpId, cascadeDelete: true)
                .Index(t => t.EmpId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leaves", "EmpId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Employees", "PMId", "dbo.Projects");
            DropIndex("dbo.Leaves", new[] { "EmpId" });
            DropIndex("dbo.Employees", new[] { "PMId" });
            DropIndex("dbo.Employees", new[] { "RoleId" });
            DropTable("dbo.Leaves");
            DropTable("dbo.Roles");
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
        }
    }
}
