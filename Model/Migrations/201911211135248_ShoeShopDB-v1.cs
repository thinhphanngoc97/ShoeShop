namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoeShopDBv1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ADMIN",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Username = c.String(nullable: false, maxLength: 255, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        Avatar = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CATEGORY",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Metadata = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PRODUCT",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 255),
                        Price = c.Decimal(storeType: "money"),
                        Discount_Amount = c.Int(),
                        Average_Rate = c.Double(),
                        Image_Thumbnail = c.String(unicode: false),
                        Status = c.String(maxLength: 255),
                        Model_Number = c.String(maxLength: 255, unicode: false),
                        Description = c.String(maxLength: 1255),
                        Style = c.String(maxLength: 255),
                        Material = c.String(maxLength: 255),
                        Warranty_Period = c.String(maxLength: 255),
                        String_Material = c.String(maxLength: 255),
                        Created_Time = c.DateTime(storeType: "smalldatetime"),
                        Id_Category = c.Int(),
                        Id_Manufacturer = c.Int(),
                        Metadata = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MANUFACTURER", t => t.Id_Manufacturer)
                .ForeignKey("dbo.CATEGORY", t => t.Id_Category)
                .Index(t => t.Id_Category)
                .Index(t => t.Id_Manufacturer);
            
            CreateTable(
                "dbo.INVOICE_DETAIL",
                c => new
                    {
                        Id_Invoice = c.Int(nullable: false),
                        Id_Product = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id_Invoice, t.Id_Product })
                .ForeignKey("dbo.INVOICE", t => t.Id_Invoice)
                .ForeignKey("dbo.PRODUCT", t => t.Id_Product)
                .Index(t => t.Id_Invoice)
                .Index(t => t.Id_Product);
            
            CreateTable(
                "dbo.INVOICE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Customer_Name = c.String(nullable: false, maxLength: 255),
                        Customer_Email = c.String(maxLength: 255, unicode: false),
                        Customer_Phone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Customer_Address = c.String(nullable: false, maxLength: 1255),
                        Customer_Message = c.String(maxLength: 1255),
                        Total = c.Decimal(storeType: "money"),
                        Payment_Method = c.String(maxLength: 255),
                        Created_Time = c.DateTime(storeType: "smalldatetime"),
                        Id_User = c.Int(),
                        Id_Discount_Code = c.Int(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DISCOUNT_CODE", t => t.Id_Discount_Code)
                .ForeignKey("dbo.USER", t => t.Id_User)
                .Index(t => t.Id_User)
                .Index(t => t.Id_Discount_Code);
            
            CreateTable(
                "dbo.DISCOUNT_CODE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 255, unicode: false),
                        Discount_Amount = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        End_Date = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        Password = c.String(nullable: false, maxLength: 255, unicode: false),
                        Address = c.String(maxLength: 255),
                        Phone = c.String(nullable: false, maxLength: 20, unicode: false),
                        Avatar = c.String(unicode: false),
                        Participation_Time = c.DateTime(storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MANUFACTURER",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Country = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PRODUCT_IMAGE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.String(unicode: false),
                        Id_Product = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PRODUCT", t => t.Id_Product)
                .Index(t => t.Id_Product);
            
            CreateTable(
                "dbo.RATE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number_Of_Stars = c.Double(),
                        Id_Product = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PRODUCT", t => t.Id_Product)
                .Index(t => t.Id_Product);
            
            CreateTable(
                "dbo.MESSAGE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Title = c.String(maxLength: 255),
                        Message_Content = c.String(nullable: false, maxLength: 1000),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SIZE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PRODUCT", "Id_Category", "dbo.CATEGORY");
            DropForeignKey("dbo.RATE", "Id_Product", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCT_IMAGE", "Id_Product", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCT", "Id_Manufacturer", "dbo.MANUFACTURER");
            DropForeignKey("dbo.INVOICE_DETAIL", "Id_Product", "dbo.PRODUCT");
            DropForeignKey("dbo.INVOICE", "Id_User", "dbo.USER");
            DropForeignKey("dbo.INVOICE_DETAIL", "Id_Invoice", "dbo.INVOICE");
            DropForeignKey("dbo.INVOICE", "Id_Discount_Code", "dbo.DISCOUNT_CODE");
            DropIndex("dbo.RATE", new[] { "Id_Product" });
            DropIndex("dbo.PRODUCT_IMAGE", new[] { "Id_Product" });
            DropIndex("dbo.INVOICE", new[] { "Id_Discount_Code" });
            DropIndex("dbo.INVOICE", new[] { "Id_User" });
            DropIndex("dbo.INVOICE_DETAIL", new[] { "Id_Product" });
            DropIndex("dbo.INVOICE_DETAIL", new[] { "Id_Invoice" });
            DropIndex("dbo.PRODUCT", new[] { "Id_Manufacturer" });
            DropIndex("dbo.PRODUCT", new[] { "Id_Category" });
            DropTable("dbo.SIZE");
            DropTable("dbo.MESSAGE");
            DropTable("dbo.RATE");
            DropTable("dbo.PRODUCT_IMAGE");
            DropTable("dbo.MANUFACTURER");
            DropTable("dbo.USER");
            DropTable("dbo.DISCOUNT_CODE");
            DropTable("dbo.INVOICE");
            DropTable("dbo.INVOICE_DETAIL");
            DropTable("dbo.PRODUCT");
            DropTable("dbo.CATEGORY");
            DropTable("dbo.ADMIN");
        }
    }
}
