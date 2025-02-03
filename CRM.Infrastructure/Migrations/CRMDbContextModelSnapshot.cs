﻿// <auto-generated />
using System;
using CRM.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRM.Infrastructure.Migrations
{
    [DbContext(typeof(CRMDbContext))]
    partial class CRMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CRM.Domain.Auth.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("CRM.Domain.Auth.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CRM.Domain.Auth.RolePermission", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("CRM.Domain.Auth.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRM.Domain.Auth.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Address", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BranchCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("BranchId")
                        .HasColumnType("bigint");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Country")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FAXNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LocationCode1")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LocationCode2")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LocationCode3")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Branch", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("AllowedCODCurencies")
                        .HasColumnType("integer");

                    b.Property<string>("BillingExternalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BranchCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("BranchName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConsolidationQuery")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("CurrencyCode")
                        .HasColumnType("integer");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<int>("DimensionUnit")
                        .HasColumnType("integer");

                    b.Property<string>("EORI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExternalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GPI")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IOSS")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LicenseRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductService")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProductTypeCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShipmentService")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<string>("SupplierCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.Property<string>("VATNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WeightUnit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Customer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("BillingExternalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BusinessType")
                        .HasColumnType("integer");

                    b.Property<string>("CompanyCommercialName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CompanyLegalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExternalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LogoBase64")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<short>("StatusId")
                        .HasColumnType("smallint");

                    b.Property<string>("StoreBarcodePrefix")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("CRM.Domain.Auth.RolePermission", b =>
                {
                    b.HasOne("CRM.Domain.Auth.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Auth.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CRM.Domain.Auth.UserRole", b =>
                {
                    b.HasOne("CRM.Domain.Auth.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Auth.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Address", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Branch", "Branch")
                        .WithMany("Addresses")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Branch", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Customer", "Customer")
                        .WithMany("Branches")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("CRM.Domain.Auth.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("CRM.Domain.Auth.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CRM.Domain.Auth.User", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Branch", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Branches");
                });
#pragma warning restore 612, 618
        }
    }
}
