﻿// <auto-generated />
using System;
using MagniseCryptocurrenciesApp.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagniseCryptocurrenciesApp.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MagniseCryptocurrenciesApp.DataAccess.EntitesModel.Asset", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("PriceUSD")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("TypeIsCrypto")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("MagniseCryptocurrenciesApp.DataAccess.EntitesModel.AssetRate", b =>
                {
                    b.Property<string>("AssetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetIdQuote")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AssetId", "AssetIdQuote");

                    b.ToTable("AssetRates");
                });

            modelBuilder.Entity("MagniseCryptocurrenciesApp.DataAccess.EntitesModel.AssetRate", b =>
                {
                    b.HasOne("MagniseCryptocurrenciesApp.DataAccess.EntitesModel.Asset", "Asset")
                        .WithMany("AssetRates")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
