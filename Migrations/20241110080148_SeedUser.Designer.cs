﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using make_it_happen.Context;

#nullable disable

namespace make_it_happen.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241110080148_SeedUser")]
    partial class SeedUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("make_it_happen.Models.Campaign", b =>
                {
                    b.Property<int>("CampaignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CampaignId"));

                    b.Property<decimal?>("AmountRaised")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Goal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Mode")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Status")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("TermsConditions")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("VideoUrl")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("CampaignId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("make_it_happen.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("make_it_happen.Models.DonateHistory", b =>
                {
                    b.Property<int>("DonateHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DonateHistoryId"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DonationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<bool?>("ReceiptSent")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Status")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DonateHistoryId");

                    b.HasIndex("CampaignId");

                    b.HasIndex("UserId");

                    b.ToTable("DonateHistories");
                });

            modelBuilder.Entity("make_it_happen.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Bio")
                        .HasColumnType("longtext");

                    b.Property<string>("Contact")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool?>("EmailVerified")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Status")
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("make_it_happen.Models.Campaign", b =>
                {
                    b.HasOne("make_it_happen.Models.Category", "Category")
                        .WithMany("Campaigns")
                        .HasForeignKey("CategoryId");

                    b.HasOne("make_it_happen.Models.User", "User")
                        .WithMany("Campaigns")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("make_it_happen.Models.DonateHistory", b =>
                {
                    b.HasOne("make_it_happen.Models.Campaign", "Campaign")
                        .WithMany("DonationHistory")
                        .HasForeignKey("CampaignId");

                    b.HasOne("make_it_happen.Models.User", "User")
                        .WithMany("DonationHistory")
                        .HasForeignKey("UserId");

                    b.Navigation("Campaign");

                    b.Navigation("User");
                });

            modelBuilder.Entity("make_it_happen.Models.Campaign", b =>
                {
                    b.Navigation("DonationHistory");
                });

            modelBuilder.Entity("make_it_happen.Models.Category", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("make_it_happen.Models.User", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("DonationHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
