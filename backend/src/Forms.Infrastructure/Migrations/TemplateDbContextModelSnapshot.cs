﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Forms.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Forms.Infrastructure.Migrations
{
    [DbContext(typeof(TemplateDbContext))]
    partial class TemplateDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Aggregate.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.ComplexProperty<Dictionary<string, object>>("Description", "Forms.Domain.TemplateManagement.Aggregate.Template.Description#Description", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("description");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Forms.Domain.TemplateManagement.Aggregate.Template.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("character varying(120)")
                                .HasColumnName("title");
                        });

                    b.HasKey("Id")
                        .HasName("pk_templates");

                    b.ToTable("templates", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<Guid?>("templates_id")
                        .HasColumnType("uuid")
                        .HasColumnName("templates_id");

                    b.ComplexProperty<Dictionary<string, object>>("Title", "Forms.Domain.TemplateManagement.Entities.Question.Title#Title", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(120)
                                .HasColumnType("character varying(120)")
                                .HasColumnName("title");
                        });

                    b.HasKey("Id")
                        .HasName("pk_questions");

                    b.HasIndex("templates_id")
                        .HasDatabaseName("ix_questions_templates_id");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Question", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Aggregate.Template", null)
                        .WithMany("Questions")
                        .HasForeignKey("templates_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_questions_templates_templates_id");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Aggregate.Template", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
