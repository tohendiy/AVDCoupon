﻿// <auto-generated />
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ADVCoupon.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180425091130_NewChanges")]
    partial class NewChanges
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("ADVCoupon.Models.Geoposition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Accuracy");

                    b.Property<string>("Building");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Geopositions");
                });

            modelBuilder.Entity("ADVCoupon.Models.Network", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<byte[]>("LogoImage");

                    b.Property<Guid?>("NetworkBarcodeId");

                    b.Property<Guid?>("ProductCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("NetworkBarcodeId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkBarcode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarcodeValue");

                    b.Property<Guid?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("NetworkBarcodes");
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkCoupon", b =>
                {
                    b.Property<Guid>("NetworkId");

                    b.Property<Guid>("CouponId");

                    b.HasKey("NetworkId", "CouponId");

                    b.HasIndex("CouponId");

                    b.ToTable("NetworkCoupon");
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("GeopositionId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("NetworkId");

                    b.HasKey("Id");

                    b.HasIndex("GeopositionId");

                    b.HasIndex("NetworkId");

                    b.ToTable("NetworkPoints");
                });

            modelBuilder.Entity("ADVCoupon.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Image");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ProviderId");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ADVCoupon.Models.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("ADVCoupon.Models.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("LogoImage");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("ADVCoupon.Models.UserCoupon", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<Guid>("CouponId");

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("GeopositionId");

                    b.Property<Guid>("NetworkId");

                    b.HasKey("UserId", "CouponId");

                    b.HasIndex("CouponId");

                    b.HasIndex("GeopositionId");

                    b.ToTable("UserCoupon");
                });

            modelBuilder.Entity("AVDCoupon.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<DateTime>("Birthday");

                    b.Property<int>("ChildNumber");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<Guid?>("NetworkId");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<Guid?>("ProviderId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NetworkId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("ProviderId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AVDCoupon.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<int>("CurrentCapacity");

                    b.Property<double?>("DiscountAbsolute");

                    b.Property<double?>("DiscountPercentage");

                    b.Property<DateTime>("EndDate");

                    b.Property<bool>("IsApproved");

                    b.Property<Guid?>("ProductId");

                    b.Property<Guid?>("ProviderId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TotalCapacity");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ADVCoupon.Models.Network", b =>
                {
                    b.HasOne("ADVCoupon.Models.NetworkBarcode")
                        .WithMany("Networks")
                        .HasForeignKey("NetworkBarcodeId");

                    b.HasOne("ADVCoupon.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkBarcode", b =>
                {
                    b.HasOne("ADVCoupon.Models.Product")
                        .WithMany("NetworkBarcodes")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkCoupon", b =>
                {
                    b.HasOne("AVDCoupon.Models.Coupon", "Coupon")
                        .WithMany("NetworkCoupons")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ADVCoupon.Models.Network", "Network")
                        .WithMany("NetworkCoupons")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ADVCoupon.Models.NetworkPoint", b =>
                {
                    b.HasOne("ADVCoupon.Models.Geoposition", "Geoposition")
                        .WithMany()
                        .HasForeignKey("GeopositionId");

                    b.HasOne("ADVCoupon.Models.Network")
                        .WithMany("NetworkPoints")
                        .HasForeignKey("NetworkId");
                });

            modelBuilder.Entity("ADVCoupon.Models.Product", b =>
                {
                    b.HasOne("ADVCoupon.Models.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("ADVCoupon.Models.UserCoupon", b =>
                {
                    b.HasOne("AVDCoupon.Models.Coupon", "Coupon")
                        .WithMany("UserCoupons")
                        .HasForeignKey("CouponId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ADVCoupon.Models.Geoposition", "Geoposition")
                        .WithMany()
                        .HasForeignKey("GeopositionId");

                    b.HasOne("AVDCoupon.Models.ApplicationUser", "ClientUser")
                        .WithMany("UserCoupons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AVDCoupon.Models.ApplicationUser", b =>
                {
                    b.HasOne("ADVCoupon.Models.Network", "Network")
                        .WithMany("MerchantUsers")
                        .HasForeignKey("NetworkId");

                    b.HasOne("ADVCoupon.Models.Provider", "Provider")
                        .WithMany("RetailUsers")
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("AVDCoupon.Models.Coupon", b =>
                {
                    b.HasOne("ADVCoupon.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.HasOne("ADVCoupon.Models.Provider")
                        .WithMany("Coupons")
                        .HasForeignKey("ProviderId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AVDCoupon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AVDCoupon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AVDCoupon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AVDCoupon.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
