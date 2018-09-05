using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ProductCatalogAPI.Migrations
{
    public partial class addCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "CatalogType",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "CatalogBrand",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatalogCategoryId",
                table: "Catalog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CatalogCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Catalog_CatalogCategoryId",
                table: "Catalog",
                column: "CatalogCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Catalog_CatalogCategories_CatalogCategoryId",
                table: "Catalog",
                column: "CatalogCategoryId",
                principalTable: "CatalogCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Catalog_CatalogCategories_CatalogCategoryId",
                table: "Catalog");

            migrationBuilder.DropTable(
                name: "CatalogCategories");

            migrationBuilder.DropIndex(
                name: "IX_Catalog_CatalogCategoryId",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "CatalogType");

            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "CatalogBrand");

            migrationBuilder.DropColumn(
                name: "CatalogCategoryId",
                table: "Catalog");
        }
    }
}
