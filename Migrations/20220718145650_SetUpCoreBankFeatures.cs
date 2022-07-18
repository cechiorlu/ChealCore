using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChealCore.Migrations
{
    public partial class SetUpCoreBankFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BranchName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    SortCode = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                schema: "Identity",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    Address = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "GLCategory",
                schema: "Identity",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CategoryDescription = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CodeNumber = table.Column<long>(type: "bigint", nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    mainAccountCategory = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccountName = table.Column<string>(type: "text", nullable: false),
                    SubCategory = table.Column<string>(type: "text", nullable: false),
                    mainAccountCategory = table.Column<int>(type: "integer", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccount",
                schema: "Identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerID = table.Column<int>(type: "integer", nullable: false),
                    AccountName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    AccountNumber = table.Column<long>(type: "bigint", nullable: true),
                    AccountBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    Accounttype = table.Column<int>(type: "integer", nullable: false),
                    DateOpened = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerAccount_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalSchema: "Identity",
                        principalTable: "Customer",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GLAccount",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountName = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CodeNumber = table.Column<long>(type: "bigint", nullable: true),
                    AccountBalance = table.Column<float>(type: "real", nullable: false),
                    GLCategoryID = table.Column<int>(type: "integer", nullable: false),
                    BranchID = table.Column<int>(type: "integer", nullable: false),
                    IsActivated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GLAccount_Branch_BranchID",
                        column: x => x.BranchID,
                        principalSchema: "Identity",
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GLAccount_GLCategory_GLCategoryID",
                        column: x => x.GLCategoryID,
                        principalSchema: "Identity",
                        principalTable: "GLCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountConfiguration",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsBusinessOpen = table.Column<bool>(type: "boolean", nullable: false),
                    FinancialDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SavingsCreditInterestRate = table.Column<double>(type: "double precision", nullable: false),
                    SavingsMinimumBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    SavingsInterestExpenseGlID = table.Column<int>(type: "integer", nullable: true),
                    SavingsInterestPayableGlID = table.Column<int>(type: "integer", nullable: true),
                    CurrentCreditInterestRate = table.Column<double>(type: "double precision", nullable: false),
                    CurrentMinimumBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentCot = table.Column<decimal>(type: "numeric", nullable: false),
                    CurrentInterestExpenseGlID = table.Column<int>(type: "integer", nullable: true),
                    CurrentCotIncomeGlID = table.Column<int>(type: "integer", nullable: true),
                    CurrentInterestPayableGlID = table.Column<int>(type: "integer", nullable: true),
                    LoanDebitInterestRate = table.Column<double>(type: "double precision", nullable: false),
                    LoanInterestIncomeGlID = table.Column<int>(type: "integer", nullable: true),
                    LoanInterestExpenseGLID = table.Column<int>(type: "integer", nullable: true),
                    LoanInterestReceivableGlID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountConfiguration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentCotIncomeGlID",
                        column: x => x.CurrentCotIncomeGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentInterestExpenseGlID",
                        column: x => x.CurrentInterestExpenseGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_CurrentInterestPayableGlID",
                        column: x => x.CurrentInterestPayableGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestExpenseGLID",
                        column: x => x.LoanInterestExpenseGLID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestIncomeGlID",
                        column: x => x.LoanInterestIncomeGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_LoanInterestReceivableGlID",
                        column: x => x.LoanInterestReceivableGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_SavingsInterestExpenseGlID",
                        column: x => x.SavingsInterestExpenseGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AccountConfiguration_GLAccount_SavingsInterestPayableGlID",
                        column: x => x.SavingsInterestPayableGlID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "GLPosting",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreditAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Narration = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DrGlAccountID = table.Column<int>(type: "integer", nullable: true),
                    CrGlAccountID = table.Column<int>(type: "integer", nullable: true),
                    PostInitiatorId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLPosting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GLPosting_GLAccount_CrGlAccountID",
                        column: x => x.CrGlAccountID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_GLPosting_GLAccount_DrGlAccountID",
                        column: x => x.DrGlAccountID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TellerPosting",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Narration = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PostingType = table.Column<int>(type: "integer", nullable: false),
                    CustomerAccountID = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GLAccountID = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TellerPosting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TellerPosting_CustomerAccount_CustomerAccountID",
                        column: x => x.CustomerAccountID,
                        principalSchema: "Identity",
                        principalTable: "CustomerAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TellerPosting_GLAccount_GLAccountID",
                        column: x => x.GLAccountID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TellerPosting_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTill",
                schema: "Identity",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    GlAccountID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserTill_GLAccount_GlAccountID",
                        column: x => x.GlAccountID,
                        principalSchema: "Identity",
                        principalTable: "GLAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTill_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Identity",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentCotIncomeGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "CurrentCotIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentInterestExpenseGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "CurrentInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_CurrentInterestPayableGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "CurrentInterestPayableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestExpenseGLID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "LoanInterestExpenseGLID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestIncomeGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "LoanInterestIncomeGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_LoanInterestReceivableGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "LoanInterestReceivableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_SavingsInterestExpenseGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "SavingsInterestExpenseGlID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountConfiguration_SavingsInterestPayableGlID",
                schema: "Identity",
                table: "AccountConfiguration",
                column: "SavingsInterestPayableGlID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerAccount_CustomerID",
                schema: "Identity",
                table: "CustomerAccount",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_BranchID",
                schema: "Identity",
                table: "GLAccount",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_GLAccount_GLCategoryID",
                schema: "Identity",
                table: "GLAccount",
                column: "GLCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_GLPosting_CrGlAccountID",
                schema: "Identity",
                table: "GLPosting",
                column: "CrGlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_GLPosting_DrGlAccountID",
                schema: "Identity",
                table: "GLPosting",
                column: "DrGlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_CustomerAccountID",
                schema: "Identity",
                table: "TellerPosting",
                column: "CustomerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_GLAccountID",
                schema: "Identity",
                table: "TellerPosting",
                column: "GLAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TellerPosting_UserId",
                schema: "Identity",
                table: "TellerPosting",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTill_GlAccountID",
                schema: "Identity",
                table: "UserTill",
                column: "GlAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_UserTill_UserId",
                schema: "Identity",
                table: "UserTill",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountConfiguration",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "GLPosting",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "TellerPosting",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Transaction",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "UserTill",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "CustomerAccount",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "GLAccount",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Customer",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "GLCategory",
                schema: "Identity");
        }
    }
}
