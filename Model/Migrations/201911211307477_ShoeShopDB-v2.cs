namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShoeShopDBv2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PRODUCT_SIZE",
                c => new
                    {
                        Id_Product = c.Int(nullable: false),
                        Id_Size = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id_Product, t.Id_Size })
                .ForeignKey("dbo.SIZE", t => t.Id_Size)
                .ForeignKey("dbo.PRODUCT", t => t.Id_Product)
                .Index(t => t.Id_Product)
                .Index(t => t.Id_Size);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PRODUCT_SIZE", "Id_Product", "dbo.PRODUCT");
            DropForeignKey("dbo.PRODUCT_SIZE", "Id_Size", "dbo.SIZE");
            DropIndex("dbo.PRODUCT_SIZE", new[] { "Id_Size" });
            DropIndex("dbo.PRODUCT_SIZE", new[] { "Id_Product" });
            DropTable("dbo.PRODUCT_SIZE");
        }
    }
}
