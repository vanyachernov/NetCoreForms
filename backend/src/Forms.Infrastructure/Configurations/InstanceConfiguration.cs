using Forms.Domain.Shared;
using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Aggregate;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class InstanceConfiguration : IEntityTypeConfiguration<Instance>
{
    public void Configure(EntityTypeBuilder<Instance> builder)
    {
        builder.ToTable("instances");
        
        builder.HasKey(q => q.Id);
        
        builder
            .Property(q => q.Id)
            .HasConversion(
                id => id.Value,
                value => InstanceId.Create(value));

        builder.HasOne(i => i.Respondent)
            .WithMany()
            .HasForeignKey("respondent_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Template)
            .WithMany()
            .HasForeignKey("template_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(i => i.Answers)
            .WithOne(a => a.Instance)
            .HasForeignKey("instance_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}