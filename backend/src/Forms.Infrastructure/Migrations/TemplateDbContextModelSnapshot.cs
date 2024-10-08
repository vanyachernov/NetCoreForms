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

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Answer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AnswerValue")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("answer_value");

                    b.Property<Guid>("InstanceId")
                        .HasColumnType("uuid")
                        .HasColumnName("instance_id");

                    b.Property<Guid?>("answer_id")
                        .HasColumnType("uuid")
                        .HasColumnName("answer_id");

                    b.Property<Guid>("question_id")
                        .HasColumnType("uuid")
                        .HasColumnName("question_id");

                    b.ComplexProperty<Dictionary<string, object>>("IsCorrect", "Forms.Domain.TemplateManagement.Entities.Answer.IsCorrect#IsCorrect", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("boolean")
                                .HasDefaultValue(false)
                                .HasColumnName("is_correct_value");
                        });

                    b.HasKey("Id")
                        .HasName("pk_answers");

                    b.HasIndex("InstanceId")
                        .HasDatabaseName("ix_answers_instance_id");

                    b.HasIndex("answer_id")
                        .HasDatabaseName("ix_answers_answer_id");

                    b.HasIndex("question_id")
                        .HasDatabaseName("ix_answers_question_id");

                    b.ToTable("answers", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Instance", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("respondent_id")
                        .HasColumnType("text")
                        .HasColumnName("respondent_id");

                    b.Property<Guid>("template_id")
                        .HasColumnType("uuid")
                        .HasColumnName("template_id");

                    b.HasKey("Id")
                        .HasName("pk_instances");

                    b.HasIndex("respondent_id")
                        .HasDatabaseName("ix_instances_respondent_id");

                    b.HasIndex("template_id")
                        .HasDatabaseName("ix_instances_template_id");

                    b.ToTable("instances", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<Guid>("template_id")
                        .HasColumnType("uuid")
                        .HasColumnName("template_id");

                    b.Property<Guid?>("templates_id")
                        .HasColumnType("uuid")
                        .HasColumnName("templates_id");

                    b.ComplexProperty<Dictionary<string, object>>("IsRequired", "Forms.Domain.TemplateManagement.Entities.Question.IsRequired#IsRequired", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<bool>("Value")
                                .HasColumnType("boolean")
                                .HasColumnName("is_required_value");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Order", "Forms.Domain.TemplateManagement.Entities.Question.Order#Order", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("order_value");
                        });

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

                    b.HasIndex("template_id")
                        .HasDatabaseName("ix_questions_template_id");

                    b.HasIndex("templates_id")
                        .HasDatabaseName("ix_questions_templates_id");

                    b.ToTable("questions", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.ComplexProperty<Dictionary<string, object>>("FullName", "Forms.Domain.TemplateManagement.Entities.User.FullName#FullName", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("character varying(60)")
                                .HasColumnName("full_name_first_name");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("character varying(60)")
                                .HasColumnName("full_name_last_name");
                        });

                    b.HasKey("Id")
                        .HasName("pk_asp_net_users");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_roles");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_role_claims");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_role_claims_role_id");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_asp_net_user_claims");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_claims_user_id");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_asp_net_user_logins");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_asp_net_user_logins_user_id");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("RoleId")
                        .HasColumnType("text")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_asp_net_user_roles");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_asp_net_user_roles_role_id");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_asp_net_user_tokens");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Answer", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Entities.Instance", "Instance")
                        .WithMany()
                        .HasForeignKey("InstanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_instances_instance_id");

                    b.HasOne("Forms.Domain.TemplateManagement.Entities.Instance", null)
                        .WithMany("Answers")
                        .HasForeignKey("answer_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_answers_instances_answer_id");

                    b.HasOne("Forms.Domain.TemplateManagement.Entities.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("question_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_answers_questions_question_id");

                    b.Navigation("Instance");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Instance", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Entities.User", "Respondent")
                        .WithMany()
                        .HasForeignKey("respondent_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_instances_users_respondent_id");

                    b.HasOne("Forms.Domain.TemplateManagement.Aggregate.Template", "Template")
                        .WithMany()
                        .HasForeignKey("template_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_instances_templates_template_id");

                    b.Navigation("Respondent");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Question", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Aggregate.Template", "Template")
                        .WithMany()
                        .HasForeignKey("template_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_questions_templates_template_id");

                    b.HasOne("Forms.Domain.TemplateManagement.Aggregate.Template", null)
                        .WithMany("Questions")
                        .HasForeignKey("templates_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_questions_templates_templates_id");

                    b.Navigation("Template");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_role_claims_asp_net_roles_role_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_claims_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_logins_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_roles_role_id");

                    b.HasOne("Forms.Domain.TemplateManagement.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_roles_asp_net_users_user_id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Forms.Domain.TemplateManagement.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_asp_net_user_tokens_asp_net_users_user_id");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Aggregate.Template", b =>
                {
                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Instance", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("Forms.Domain.TemplateManagement.Entities.Question", b =>
                {
                    b.Navigation("Answers");
                });
#pragma warning restore 612, 618
        }
    }
}
