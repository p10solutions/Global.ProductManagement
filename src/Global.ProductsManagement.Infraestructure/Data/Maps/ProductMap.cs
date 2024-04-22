using Global.ProductsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global.ProductsManagement.Infraestructure.Data.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("TB_PRODUCT");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Price)
                .HasColumnName("PRICE")
                .HasColumnType("numeric(10,2)");

            builder.Property(x => x.CategoryId)
                .HasColumnName("CATEGORY_ID");

            builder.Property(x => x.CreateDate)
                .HasColumnName("DT_CREATE");

            builder.Property(x => x.UpdateDate)
                .HasColumnName("DT_UPDATE");

            builder.Property(x => x.Status)
                .HasColumnName("STATUS");

            builder.Property(x => x.BrandId)
                .HasColumnName("BRAND");

            builder.Property(x => x.Details)
                .HasColumnName("DETAILS")
                .HasColumnType("varchar(max)"); ;

            builder.HasOne(x=>x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .IsRequired();

            builder.HasOne(x => x.Brand)
                .WithMany()
                .HasForeignKey(x => x.BrandId)
                .IsRequired();
        }
    }
}
