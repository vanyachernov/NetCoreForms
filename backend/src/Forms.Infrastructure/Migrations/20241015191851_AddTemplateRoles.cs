using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "owner_id",
                table: "templates",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "finished_at",
                table: "instances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "template_roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    template_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_template_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_template_roles_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_template_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_templates_owner_id",
                table: "templates",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_roles_template_id",
                table: "template_roles",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_roles_user_id",
                table: "template_roles",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_templates_users_owner_id",
                table: "templates",
                column: "owner_id",
                principalTable: "AspNetUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_templates_users_owner_id",
                table: "templates");

            migrationBuilder.DropTable(
                name: "template_roles");

            migrationBuilder.DropIndex(
                name: "ix_templates_owner_id",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "owner_id",
                table: "templates");

            migrationBuilder.DropColumn(
                name: "finished_at",
                table: "instances");
        }
    }
}
