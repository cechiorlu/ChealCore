using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChealCore.Migrations
{
    public partial class SetupLoanAccounts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanAccount",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerAccountId = table.Column<int>(type: "integer", nullable: false),
                    AccountName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    LoanAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    LoanTerms = table.Column<int>(type: "integer", nullable: false),
                    NumberOfYears = table.Column<double>(type: "double precision", nullable: false),
                    InterestRate = table.Column<double>(type: "double precision", nullable: false),
                    ServicingAccountNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    BranchID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanAccount_Branch_BranchID",
                        column: x => x.BranchID,
                        principalSchema: "Identity",
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanAccount_CustomerAccount_CustomerAccountId",
                        column: x => x.CustomerAccountId,
                        principalSchema: "Identity",
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_BranchID",
                schema: "Identity",
                table: "LoanAccount",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_LoanAccount_CustomerAccountId",
                schema: "Identity",
                table: "LoanAccount",
                column: "CustomerAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanAccount",
                schema: "Identity");
        }
    }
}
