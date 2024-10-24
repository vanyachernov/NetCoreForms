using Forms.Domain.Shared;
using Forms.Domain.TemplateManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forms.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(u => u.Email)
            .IsUnique();
        
        builder.ComplexProperty(u => u.FullName, ub =>
        {
            ub.Property(ubb => ubb.FirstName)
                .HasMaxLength(Constants.MAX_FULL_NAME_TEXT_LENGTH)
                .IsRequired();
            
            ub.Property(ubb => ubb.LastName)
                .HasMaxLength(Constants.MAX_FULL_NAME_TEXT_LENGTH)
                .IsRequired();
        });

        builder.OwnsOne(u => u.RefreshToken, ub =>
        {
            ub.Property(ubb => ubb.Value)
                .HasColumnName("RefreshToken")
                .IsRequired(false);
        });

        builder.OwnsOne(u => u.RefreshTokenExpiryTime, ub =>
        {
            ub.Property(ubb => ubb.Value)
                .HasColumnName("RefreshTokenExpiryTime")
                .HasDefaultValueSql("NOW()")
                .IsRequired();
        });
    }
}