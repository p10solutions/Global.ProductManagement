using Global.ProductsManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global.ProductsManagement.Infraestructure.Data.Maps
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("TB_CATEGORY");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar(200)");
        }
    }
}
