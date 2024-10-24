using Forms.Domain.Shared.IDs;
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
        
        builder.HasOne(a => a.Instance)
            .WithMany(i => i.Answers)
            .HasForeignKey("instance_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(a => a.TextAnswer)
            .HasColumnName("text_answer")
            .HasMaxLength(1000)
            .IsRequired(false);
        
        builder.HasOne(a => a.SelectedAnswerOption)
            .WithMany()
            .HasForeignKey("selected_answer_option_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}