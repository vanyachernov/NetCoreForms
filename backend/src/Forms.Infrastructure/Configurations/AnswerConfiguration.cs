using System.Text.Json;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Entities;
using Forms.Domain.TemplateManagement.ValueObjects;
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
            .WithMany(q => q.Answers)
            .HasForeignKey("question_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(a => a.AnswerValue)
            .HasConversion(
                value => JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
                json => JsonSerializer.Deserialize<AnswerValue>(json, JsonSerializerOptions.Default)!
            )
            .HasColumnType("jsonb")
            .IsRequired();
        
        builder.ComplexProperty(a => a.IsCorrect, ab =>
        {
            ab.Property(abb => abb.Value)
                .IsRequired()
                .HasDefaultValue(false);
        });
    }
}