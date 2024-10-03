using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.ToTable("answers");
        
        builder.HasKey(a => a.Id);
        
        builder
            .Property(a => a.Id)
            .HasConversion(
                id => id.Value,
                value => AnswerId.Create(value));

        builder.HasOne(a => a.Question)
            .WithMany()
            .HasForeignKey("answers_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.ComplexProperty(a => a.AnswerValue, ab =>
        {
            ab.Property(abb => abb.Value).IsRequired();
        });
    }
}