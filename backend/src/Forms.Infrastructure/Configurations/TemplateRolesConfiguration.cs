using Forms.Domain.Shared.IDs;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class TemplateRolesConfiguration : IEntityTypeConfiguration<TemplateRoles>
{
    public void Configure(EntityTypeBuilder<TemplateRoles> builder)
    {
        builder.ToTable("template_roles");

        builder.HasKey(tr => tr.Id);
        
        builder
            .Property(tr => tr.Id)
            .HasConversion(
                id => id.Value,
                value => TemplateRolesId.Create(value));
        
        builder.HasOne(tr => tr.User)
            .WithMany()
            .HasForeignKey("user_id")
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(tr => tr.Role)
            .HasConversion<int>()
            .HasColumnName("role")
            .IsRequired();
    }
}