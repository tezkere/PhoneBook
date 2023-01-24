﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReportApi.Helpers;

#nullable disable

namespace ReportApi.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    partial class ReportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ReportApi.Entities.Report", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("uuid");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("UUID");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("ReportApi.Entities.ReportDetail", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("uuid");

                    b.Property<int>("ContactCount")
                        .HasColumnType("integer");

                    b.Property<string>("LocationName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhoneCount")
                        .HasColumnType("integer");

                    b.Property<Guid>("ReportId")
                        .HasColumnType("uuid");

                    b.HasKey("UUID");

                    b.HasIndex("ReportId")
                        .IsUnique();

                    b.ToTable("ReportDetail");
                });

            modelBuilder.Entity("ReportApi.Entities.ReportDetail", b =>
                {
                    b.HasOne("ReportApi.Entities.Report", "Report")
                        .WithOne("ReportDetail")
                        .HasForeignKey("ReportApi.Entities.ReportDetail", "ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Report");
                });

            modelBuilder.Entity("ReportApi.Entities.Report", b =>
                {
                    b.Navigation("ReportDetail")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
