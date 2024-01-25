﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolCanteen.DATA.DatabaseConnector;

#nullable disable

namespace SchoolCanteen.DATA.Migrations.Application
{
    [DbContext(typeof(DatabaseApiContext))]
    partial class DatabaseApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FinishedProductProductStorage", b =>
                {
                    b.Property<int>("FinishedProductsFinishedProductId")
                        .HasColumnType("int");

                    b.Property<int>("ProductStoragesProductStorageId")
                        .HasColumnType("int");

                    b.HasKey("FinishedProductsFinishedProductId", "ProductStoragesProductStorageId");

                    b.HasIndex("ProductStoragesProductStorageId");

                    b.ToTable("FinishedProductProductStorage");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Nip")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Phone")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("CompanyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.FinishedProduct", b =>
                {
                    b.Property<int>("FinishedProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<float>("Costs")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Profit")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("FinishedProductId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProductId");

                    b.ToTable("FinishedProducts");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UnitId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.ProductStorage", b =>
                {
                    b.Property<int>("ProductStorageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("ValidityPeriod")
                        .HasColumnType("int");

                    b.HasKey("ProductStorageId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductStorages");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<float>("Quantity")
                        .HasColumnType("float");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UnitId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.RecipeDetail", b =>
                {
                    b.Property<int>("RecipeDetailId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("RecipeDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeDetails");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("UnitId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("FinishedProductProductStorage", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.FinishedProduct", null)
                        .WithMany()
                        .HasForeignKey("FinishedProductsFinishedProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.ProductStorage", null)
                        .WithMany()
                        .HasForeignKey("ProductStoragesProductStorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.ApplicationUser", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.FinishedProduct", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("FinishedProducts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Product", null)
                        .WithMany("FinishedProducts")
                        .HasForeignKey("ProductId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Product", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("Products")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Unit", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.ProductStorage", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("ProductsStorage")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Product", "Product")
                        .WithMany("ProductStorages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Recipe", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("Recipes")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.RecipeDetail", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Product", "Product")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Unit", "Unit")
                        .WithMany("RecipeDetails")
                        .HasForeignKey("RecipeDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolCanteen.DATA.Models.Recipe", "Recipe")
                        .WithMany("Details")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Recipe");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Unit", b =>
                {
                    b.HasOne("SchoolCanteen.DATA.Models.Company", "Company")
                        .WithMany("Units")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Company", b =>
                {
                    b.Navigation("FinishedProducts");

                    b.Navigation("Products");

                    b.Navigation("ProductsStorage");

                    b.Navigation("Recipes");

                    b.Navigation("Units");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Product", b =>
                {
                    b.Navigation("FinishedProducts");

                    b.Navigation("ProductStorages");

                    b.Navigation("RecipeDetails");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Recipe", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("SchoolCanteen.DATA.Models.Unit", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("RecipeDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
