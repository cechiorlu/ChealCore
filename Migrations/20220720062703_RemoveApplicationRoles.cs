using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChealCore.Migrations
{
    public partial class RemoveApplicationRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                schema: "Identity",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                schema: "Identity",
                table: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                schema: "Identity",
                table: "Role",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                schema: "Identity",
                table: "Role",
                type: "boolean",
                nullable: true);
        }
    }
}
