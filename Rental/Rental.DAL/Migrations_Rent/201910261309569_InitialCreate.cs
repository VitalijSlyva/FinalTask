namespace Rental.DAL.Migrations_Rent
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        BrandId = c.Int(),
                        Number = c.String(),
                        Price = c.Int(nullable: false),
                        Doors = c.Int(nullable: false),
                        Кoominess = c.Int(nullable: false),
                        Fuel = c.String(),
                        Carrying = c.Int(nullable: false),
                        EngineVolume = c.Double(nullable: false),
                        Hoursepower = c.Double(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        TransmissionId = c.Int(),
                        CarcassId = c.Int(),
                        QualityId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Carcasses", t => t.CarcassId)
                .ForeignKey("dbo.Qualities", t => t.QualityId)
                .ForeignKey("dbo.Transmissions", t => t.TransmissionId)
                .Index(t => t.BrandId)
                .Index(t => t.TransmissionId)
                .Index(t => t.CarcassId)
                .Index(t => t.QualityId);
            
            CreateTable(
                "dbo.Carcasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Photo = c.Binary(),
                        CarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(),
                        CarId = c.Int(),
                        WithDriver = c.Boolean(nullable: false),
                        DateStart = c.DateTime(nullable: false),
                        DateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Confirms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerId = c.String(),
                        OrderId = c.Int(),
                        IsConfirmed = c.Boolean(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionId = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                        OrderId = c.Int(),
                        CrashId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Crashes", t => t.CrashId)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId)
                .Index(t => t.CrashId);
            
            CreateTable(
                "dbo.Crashes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReturnId = c.Int(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Returns", t => t.ReturnId)
                .Index(t => t.ReturnId);
            
            CreateTable(
                "dbo.Returns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManagerId = c.String(),
                        IsReturned = c.Boolean(nullable: false),
                        OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        CarId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Qualities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "TransmissionId", "dbo.Transmissions");
            DropForeignKey("dbo.Cars", "QualityId", "dbo.Qualities");
            DropForeignKey("dbo.Properties", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Payments", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Returns", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Crashes", "ReturnId", "dbo.Returns");
            DropForeignKey("dbo.Payments", "CrashId", "dbo.Crashes");
            DropForeignKey("dbo.Confirms", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Images", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "CarcassId", "dbo.Carcasses");
            DropForeignKey("dbo.Cars", "BrandId", "dbo.Brands");
            DropIndex("dbo.Properties", new[] { "CarId" });
            DropIndex("dbo.Returns", new[] { "OrderId" });
            DropIndex("dbo.Crashes", new[] { "ReturnId" });
            DropIndex("dbo.Payments", new[] { "CrashId" });
            DropIndex("dbo.Payments", new[] { "OrderId" });
            DropIndex("dbo.Confirms", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CarId" });
            DropIndex("dbo.Images", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "QualityId" });
            DropIndex("dbo.Cars", new[] { "CarcassId" });
            DropIndex("dbo.Cars", new[] { "TransmissionId" });
            DropIndex("dbo.Cars", new[] { "BrandId" });
            DropTable("dbo.Transmissions");
            DropTable("dbo.Qualities");
            DropTable("dbo.Properties");
            DropTable("dbo.Returns");
            DropTable("dbo.Crashes");
            DropTable("dbo.Payments");
            DropTable("dbo.Confirms");
            DropTable("dbo.Orders");
            DropTable("dbo.Images");
            DropTable("dbo.Carcasses");
            DropTable("dbo.Cars");
            DropTable("dbo.Brands");
        }
    }
}
