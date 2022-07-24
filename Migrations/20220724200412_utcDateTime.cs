using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChealCore.Migrations
{
    public partial class utcDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Identity",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(225)",
                oldMaxLength: 225);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Identity",
                table: "Customer",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(225)",
                oldMaxLength: 225);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                schema: "Identity",
                table: "Customer",
                type: "character varying(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Identity",
                table: "Customer",
                type: "character varying(225)",
                maxLength: 225,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
