﻿// <auto-generated />
using System;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppleUsed.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppleUsed.DAL.Entities.Ad", b =>
                {
                    b.Property<int>("AdId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdViewsId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("CharacteristicsId");

                    b.Property<int?>("CityId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<decimal>("Price");

                    b.Property<int?>("PurchasedId");

                    b.Property<string>("Title");

                    b.HasKey("AdId");

                    b.HasIndex("AdViewsId")
                        .IsUnique()
                        .HasFilter("[AdViewsId] IS NOT NULL");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("CharacteristicsId")
                        .IsUnique()
                        .HasFilter("[CharacteristicsId] IS NOT NULL");

                    b.HasIndex("CityId");

                    b.HasIndex("PurchasedId");

                    b.ToTable("Ads");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.AdPhotos", b =>
                {
                    b.Property<int>("AdPhotosId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdId");

                    b.Property<string>("AdPhotoName");

                    b.Property<string>("PhotoHashAvg");

                    b.Property<string>("PhotoHashBig");

                    b.Property<string>("PhotoHashSmall");

                    b.HasKey("AdPhotosId");

                    b.HasIndex("AdId");

                    b.ToTable("AdPhotos");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.AdViews", b =>
                {
                    b.Property<int>("AdViewsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdId");

                    b.Property<int>("SumViews");

                    b.HasKey("AdViewsId");

                    b.HasIndex("AdId");

                    b.ToTable("AdViews");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Characteristics", b =>
                {
                    b.Property<int>("CharacteristicsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdId");

                    b.Property<int>("ProductColorsId");

                    b.Property<int>("ProductMemoriesId");

                    b.Property<int>("ProductModelsId");

                    b.Property<int>("ProductStatesId");

                    b.Property<int>("ProductTypesId");

                    b.HasKey("CharacteristicsId");

                    b.HasIndex("AdId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CityAreaId");

                    b.Property<string>("Name");

                    b.HasKey("CityId");

                    b.HasIndex("CityAreaId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.CityArea", b =>
                {
                    b.Property<int>("CityAreaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CityAreaId");

                    b.ToTable("CityAreas");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Conversation", b =>
                {
                    b.Property<int>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdId");

                    b.Property<string>("BuyerId");

                    b.Property<string>("BuyerName");

                    b.Property<string>("SellerId");

                    b.Property<string>("SellerName");

                    b.HasKey("ConversationId");

                    b.ToTable("Conversations");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ConversationMessage", b =>
                {
                    b.Property<int>("ConversationMessageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdId");

                    b.Property<int>("ConversationId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Message");

                    b.Property<string>("ReceiverId");

                    b.Property<string>("SenderId");

                    b.Property<int>("Status");

                    b.HasKey("ConversationMessageId");

                    b.HasIndex("ConversationId");

                    b.ToTable("ConversationMessages");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductColors", b =>
                {
                    b.Property<int>("ProductColorsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ProductColorsId");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductMemories", b =>
                {
                    b.Property<int>("ProductMemoriesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ProductMemoriesId");

                    b.ToTable("ProductMemories");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductModels", b =>
                {
                    b.Property<int>("ProductModelsId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("ProductTypesId");

                    b.HasKey("ProductModelsId");

                    b.HasIndex("ProductTypesId");

                    b.ToTable("ProductModels");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductStates", b =>
                {
                    b.Property<int>("ProductStatesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ProductStatesId");

                    b.ToTable("ProductStates");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductTypes", b =>
                {
                    b.Property<int>("ProductTypesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ProductTypesId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AdId");

                    b.Property<DateTime>("DateOfPayment");

                    b.Property<DateTime>("EndDateService");

                    b.Property<bool>("IsPayed");

                    b.Property<int?>("ServicesId");

                    b.Property<DateTime>("StartDateService");

                    b.Property<decimal>("TotalCost");

                    b.HasKey("PurchaseId");

                    b.HasIndex("AdId");

                    b.HasIndex("ServicesId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Services", b =>
                {
                    b.Property<int>("ServicesId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost");

                    b.Property<int>("DaysOfActiveService");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("ServicesId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("AppleUsed.DAL.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
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
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("AppleUsed.DAL.Entities.Ad", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.AdViews", "AdViews")
                        .WithOne()
                        .HasForeignKey("AppleUsed.DAL.Entities.Ad", "AdViewsId");

                    b.HasOne("AppleUsed.DAL.Identity.ApplicationUser", "ApplicationUser")
                        .WithMany("Ads")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("AppleUsed.DAL.Entities.Characteristics", "Characteristics")
                        .WithOne()
                        .HasForeignKey("AppleUsed.DAL.Entities.Ad", "CharacteristicsId");

                    b.HasOne("AppleUsed.DAL.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("AppleUsed.DAL.Entities.Purchase", "Purchased")
                        .WithMany()
                        .HasForeignKey("PurchasedId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.AdPhotos", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.Ad", "Ad")
                        .WithMany("Photos")
                        .HasForeignKey("AdId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.AdViews", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.Ad", "Ad")
                        .WithMany()
                        .HasForeignKey("AdId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Characteristics", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.Ad", "Ad")
                        .WithMany()
                        .HasForeignKey("AdId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.City", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.CityArea", "CityArea")
                        .WithMany("Cities")
                        .HasForeignKey("CityAreaId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ConversationMessage", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.ProductModels", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.ProductTypes", "ProductTypes")
                        .WithMany("ProductModels")
                        .HasForeignKey("ProductTypesId");
                });

            modelBuilder.Entity("AppleUsed.DAL.Entities.Purchase", b =>
                {
                    b.HasOne("AppleUsed.DAL.Entities.Ad", "Ad")
                        .WithMany()
                        .HasForeignKey("AdId");

                    b.HasOne("AppleUsed.DAL.Entities.Services", "Services")
                        .WithMany()
                        .HasForeignKey("ServicesId");
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
                    b.HasOne("AppleUsed.DAL.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AppleUsed.DAL.Identity.ApplicationUser")
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

                    b.HasOne("AppleUsed.DAL.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AppleUsed.DAL.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
