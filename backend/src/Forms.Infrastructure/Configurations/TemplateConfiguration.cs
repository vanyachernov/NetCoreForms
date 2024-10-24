using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder.ToTable("templates");
        
        builder.HasKey(t => t.Id);
        
        builder
            .Property(t => t.Id)
            .HasConversion(
                id => id.Value,
                value => TemplateId.Create(value));

        builder.HasOne(t => t.Owner)
            .WithMany()
            .HasForeignKey("owner_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(t => t.Title, tb =>
        {
            tb.Property(tbb => tbb.Value)
                .HasMaxLength(Constants.MAX_TITLE_TEXT_LENGTH)
                .HasColumnName("title")
                .IsRequired();
        });

        builder.ComplexProperty(t => t.Description, tb =>
        {
            tb.Property(tbb => tbb.Value)
                .HasColumnName("description")
                .IsRequired();
        });
        
        builder.HasMany(t => t.Roles)
            .WithOne(tr => tr.Template)
            .HasForeignKey(tr => tr.TemplateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}