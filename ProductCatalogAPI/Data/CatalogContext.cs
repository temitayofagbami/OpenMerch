using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        //constructor
        //options is the depedency injection, specifiy what type of db is want
        //sql, mongo, mysql, etc, so where is this called, startup.cs,
        //it is called in dbconnection 
        public CatalogContext(DbContextOptions options) :
         base(options)

        {

        }

        //properties of catalog context are tables tell dbcontext tables to be created
        public DbSet<CatalogType> CatalogTypes { get; set; }

        public DbSet<CatalogBrand> CatalogBrands { get; set; }

        public DbSet<CatalogCategory> CatalogCategories { get;set }

        public DbSet<CatalogItem> CatalogItems { get; set; }


        //call method to tell builder to build each table
        // with the configurations define for the columns of the table

        protected override void OnModelCreating

           (ModelBuilder builder)

        {

            builder.Entity<CatalogBrand>(ConfigureCatalogBrand);

            builder.Entity<CatalogType>(ConfigureCatalogType);

            builder.Entity<CatalogItem>(ConfigureCatalogItem);

        }

        //build the table ColumnItem with the column configurations provided
        private void ConfigureCatalogItem

            (EntityTypeBuilder<CatalogItem> builder)

        {

            builder.ToTable("Catalog");

            builder.Property(c => c.Id)

                .ForSqlServerUseSequenceHiLo("catalog_hilo")

                .IsRequired();

            builder.Property(c => c.Name)

                .IsRequired()

                .HasMaxLength(50);

            builder.Property(c => c.Price)

                .IsRequired();

            builder.Property(c => c.PictureUrl)

                .IsRequired(false);


            builder.HasOne(c => c.CatalogCategory)

              .WithMany()

              .HasForeignKey(c => c.CatalogCategoryId);


            builder.HasOne(c => c.CatalogBrand)

                .WithMany()

                .HasForeignKey(c => c.CatalogBrandId);



            builder.HasOne(c => c.CatalogType)

                .WithMany()

                .HasForeignKey(c => c.CatalogTypeId);


        }

        //build the table CatalogCategory with the column configurations provided
        private void ConfigureCatalogCategory

            (EntityTypeBuilder<CatalogCategory> builder)

        {

            builder.ToTable("CatalogCategory");

            builder.Property(c => c.Id)

                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")

                .IsRequired();

            builder.Property(c => c.Category)

                .IsRequired()

                .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)

               .IsRequired(false);


        }

        //build the table CatalogType with the column configurations provided
        private void ConfigureCatalogType

            (EntityTypeBuilder<CatalogType> builder)

        {

            builder.ToTable("CatalogType");

            builder.Property(c => c.Id)

                .ForSqlServerUseSequenceHiLo("catalog_type_hilo")

                .IsRequired();

            builder.Property(c => c.Type)

                .IsRequired()

                .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)

              .IsRequired(false);

        }


        //build the table CatalogBrand with the column configurations provided
        private void ConfigureCatalogBrand

            (EntityTypeBuilder<CatalogBrand> builder)

        {

            builder.ToTable("CatalogBrand");

            builder.Property(c => c.Id)

                .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")

                .IsRequired();

            builder.Property(c => c.Brand)

                .IsRequired()

                .HasMaxLength(100);

            builder.Property(c => c.ImageUrl)

              .IsRequired(false);


        }





    }
}
