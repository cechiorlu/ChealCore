﻿// <auto-generated />
using System;
using ChealCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChealCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220720062703_RemoveApplicationRoles")]
    partial class RemoveApplicationRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ChealCore.Models.AccountConfiguration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<decimal>("CurrentCot")
                        .HasColumnType("numeric");

                    b.Property<int?>("CurrentCotIncomeGlID")
                        .HasColumnType("integer");

                    b.Property<double>("CurrentCreditInterestRate")
                        .HasColumnType("double precision");

                    b.Property<int?>("CurrentInterestExpenseGlID")
                        .HasColumnType("integer");

                    b.Property<int?>("CurrentInterestPayableGlID")
                        .HasColumnType("integer");

                    b.Property<decimal>("CurrentMinimumBalance")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("FinancialDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsBusinessOpen")
                        .HasColumnType("boolean");

                    b.Property<double>("LoanDebitInterestRate")
                        .HasColumnType("double precision");

                    b.Property<int?>("LoanInterestExpenseGLID")
                        .HasColumnType("integer");

                    b.Property<int?>("LoanInterestIncomeGlID")
                        .HasColumnType("integer");

                    b.Property<int?>("LoanInterestReceivableGlID")
                        .HasColumnType("integer");

                    b.Property<double>("SavingsCreditInterestRate")
                        .HasColumnType("double precision");

                    b.Property<int?>("SavingsInterestExpenseGlID")
                        .HasColumnType("integer");

                    b.Property<int?>("SavingsInterestPayableGlID")
                        .HasColumnType("integer");

                    b.Property<decimal>("SavingsMinimumBalance")
                        .HasColumnType("numeric");

                    b.HasKey("ID");

                    b.HasIndex("CurrentCotIncomeGlID");

                    b.HasIndex("CurrentInterestExpenseGlID");

                    b.HasIndex("CurrentInterestPayableGlID");

                    b.HasIndex("LoanInterestExpenseGLID");

                    b.HasIndex("LoanInterestIncomeGlID");

                    b.HasIndex("LoanInterestReceivableGlID");

                    b.HasIndex("SavingsInterestExpenseGlID");

                    b.HasIndex("SavingsInterestPayableGlID");

                    b.ToTable("AccountConfiguration", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<byte[]>("ProfilePicture")
                        .HasColumnType("bytea");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("User", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SortCode")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Branch", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("character varying(225)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("character varying(225)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(225)
                        .HasColumnType("character varying(225)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.CustomerAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("numeric");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<long?>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("Accounttype")
                        .HasColumnType("integer");

                    b.Property<int>("CustomerID")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOpened")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("CustomerID");

                    b.ToTable("CustomerAccount", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.GLAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<float>("AccountBalance")
                        .HasColumnType("real");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("BranchID")
                        .HasColumnType("integer");

                    b.Property<long?>("CodeNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("GLCategoryID")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("boolean");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("GLCategoryID");

                    b.ToTable("GLAccount", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.GLCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<long?>("CodeNumber")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("mainAccountCategory")
                        .HasColumnType("integer");

                    b.HasKey("CategoryId");

                    b.ToTable("GLCategory", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.GLPosting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int?>("CrGlAccountID")
                        .HasColumnType("integer");

                    b.Property<decimal>("CreditAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DebitAmount")
                        .HasColumnType("numeric");

                    b.Property<int?>("DrGlAccountID")
                        .HasColumnType("integer");

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostInitiatorId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.HasIndex("CrGlAccountID");

                    b.HasIndex("DrGlAccountID");

                    b.ToTable("GLPosting", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.TellerPosting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CustomerAccountID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("GLAccountID")
                        .HasColumnType("integer");

                    b.Property<string>("Narration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostingType")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CustomerAccountID");

                    b.HasIndex("GLAccountID");

                    b.HasIndex("UserId");

                    b.ToTable("TellerPosting", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SubCategory")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.Property<int>("mainAccountCategory")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Transaction", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.UserTill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<int>("GlAccountID")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("GlAccountID");

                    b.HasIndex("UserId");

                    b.ToTable("UserTill", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Role", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Identity");
                });

            modelBuilder.Entity("ChealCore.Models.AccountConfiguration", b =>
                {
                    b.HasOne("ChealCore.Models.GLAccount", "CurrentCotIncomeGl")
                        .WithMany()
                        .HasForeignKey("CurrentCotIncomeGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "CurrentInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("CurrentInterestExpenseGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "CurrentInterestPayableGl")
                        .WithMany()
                        .HasForeignKey("CurrentInterestPayableGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "LoanInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestExpenseGLID");

                    b.HasOne("ChealCore.Models.GLAccount", "LoanInterestIncomeGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestIncomeGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "LoanInterestReceivableGl")
                        .WithMany()
                        .HasForeignKey("LoanInterestReceivableGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "SavingsInterestExpenseGl")
                        .WithMany()
                        .HasForeignKey("SavingsInterestExpenseGlID");

                    b.HasOne("ChealCore.Models.GLAccount", "SavingsInterestPayableGl")
                        .WithMany()
                        .HasForeignKey("SavingsInterestPayableGlID");

                    b.Navigation("CurrentCotIncomeGl");

                    b.Navigation("CurrentInterestExpenseGl");

                    b.Navigation("CurrentInterestPayableGl");

                    b.Navigation("LoanInterestExpenseGl");

                    b.Navigation("LoanInterestIncomeGl");

                    b.Navigation("LoanInterestReceivableGl");

                    b.Navigation("SavingsInterestExpenseGl");

                    b.Navigation("SavingsInterestPayableGl");
                });

            modelBuilder.Entity("ChealCore.Models.CustomerAccount", b =>
                {
                    b.HasOne("ChealCore.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ChealCore.Models.GLAccount", b =>
                {
                    b.HasOne("ChealCore.Models.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChealCore.Models.GLCategory", "GLCategory")
                        .WithMany()
                        .HasForeignKey("GLCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("GLCategory");
                });

            modelBuilder.Entity("ChealCore.Models.GLPosting", b =>
                {
                    b.HasOne("ChealCore.Models.GLAccount", "CrGlAccount")
                        .WithMany()
                        .HasForeignKey("CrGlAccountID");

                    b.HasOne("ChealCore.Models.GLAccount", "DrGlAccount")
                        .WithMany()
                        .HasForeignKey("DrGlAccountID");

                    b.Navigation("CrGlAccount");

                    b.Navigation("DrGlAccount");
                });

            modelBuilder.Entity("ChealCore.Models.TellerPosting", b =>
                {
                    b.HasOne("ChealCore.Models.CustomerAccount", "CustomerAccount")
                        .WithMany()
                        .HasForeignKey("CustomerAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChealCore.Models.GLAccount", "TillAccount")
                        .WithMany()
                        .HasForeignKey("GLAccountID");

                    b.HasOne("ChealCore.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CustomerAccount");

                    b.Navigation("TillAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ChealCore.Models.UserTill", b =>
                {
                    b.HasOne("ChealCore.Models.GLAccount", "GLAccount")
                        .WithMany()
                        .HasForeignKey("GlAccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChealCore.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GLAccount");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ChealCore.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ChealCore.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChealCore.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ChealCore.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
