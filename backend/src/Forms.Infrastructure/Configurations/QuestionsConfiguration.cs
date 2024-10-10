using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class QuestionsConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions");
        
        builder.HasKey(q => q.Id);
        
        builder
            .Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => QuestionId.Create(value));

        builder.ComplexProperty(t => t.Title, tb =>
        {
            tb.Property(tbb => tbb.Value)
                .HasMaxLength(Constants.MAX_TITLE_TEXT_LENGTH)
                .HasColumnName("title")
                .IsRequired();
        });

        builder.Property(t => t.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.ComplexProperty(t => t.Order, tb =>
        {
            tb.Property(tbb => tbb.Value).IsRequired();
        });

        builder.ComplexProperty(t => t.IsRequired, tb =>
        {
            tb.Property(tbb => tbb.Value).IsRequired();
        });

        builder
            .HasOne(t => t.Template)
            .WithMany(t => t.Questions)
            .HasForeignKey("template_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey("question_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}