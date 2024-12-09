﻿// <auto-generated />
using System;
using HastaKayitProjesi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HastaKayitProjesi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HastaKayitProjesi.Models.Bolum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BolumAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Bolumler");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Doktor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.ToTable("Doktorlar");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Hasta", b =>
                {
                    b.Property<string>("TcKimlikNo")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("AdSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TcKimlikNo");

                    b.ToTable("Hastalar");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.RandevuKayit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.Property<int>("DoktorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RandevuTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("TcKimlikNo")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.HasIndex("DoktorId");

                    b.HasIndex("TcKimlikNo");

                    b.ToTable("RandevuKayitlar");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Doktor", b =>
                {
                    b.HasOne("HastaKayitProjesi.Models.Bolum", "Bolum")
                        .WithMany("Doktorlar")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.RandevuKayit", b =>
                {
                    b.HasOne("HastaKayitProjesi.Models.Bolum", "Bolum")
                        .WithMany()
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HastaKayitProjesi.Models.Doktor", "Doktor")
                        .WithMany("Randevular")
                        .HasForeignKey("DoktorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HastaKayitProjesi.Models.Hasta", "Hasta")
                        .WithMany("Randevular")
                        .HasForeignKey("TcKimlikNo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Bolum");

                    b.Navigation("Doktor");

                    b.Navigation("Hasta");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Bolum", b =>
                {
                    b.Navigation("Doktorlar");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Doktor", b =>
                {
                    b.Navigation("Randevular");
                });

            modelBuilder.Entity("HastaKayitProjesi.Models.Hasta", b =>
                {
                    b.Navigation("Randevular");
                });
#pragma warning restore 612, 618
        }
    }
}
