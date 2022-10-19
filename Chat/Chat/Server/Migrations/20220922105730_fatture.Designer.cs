﻿// <auto-generated />
using System;
using Chat.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Chat.Server.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20220922105730_fatture")]
    partial class fatture
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Chat.Shared.Fattura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinatarioUsername")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Iva")
                        .HasColumnType("int");

                    b.Property<string>("MittenteUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("PrezzoFinale")
                        .HasColumnType("real");

                    b.Property<float>("PrezzoNoIva")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("DestinatarioUsername");

                    b.HasIndex("MittenteUsername");

                    b.ToTable("Fatture");
                });

            modelBuilder.Entity("Chat.Shared.Indirizzo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cap")
                        .HasColumnType("int");

                    b.Property<string>("Città")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Via")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Indirizzi");
                });

            modelBuilder.Entity("Chat.Shared.Messaggio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("DestinatarioUsername")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MittenteUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Testo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DestinatarioUsername");

                    b.HasIndex("MittenteUsername");

                    b.ToTable("Messaggi");
                });

            modelBuilder.Entity("Chat.Shared.Utente", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IndirizzoId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.HasIndex("IndirizzoId");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("Chat.Shared.Fattura", b =>
                {
                    b.HasOne("Chat.Shared.Utente", "Destinatario")
                        .WithMany()
                        .HasForeignKey("DestinatarioUsername");

                    b.HasOne("Chat.Shared.Utente", "Mittente")
                        .WithMany()
                        .HasForeignKey("MittenteUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinatario");

                    b.Navigation("Mittente");
                });

            modelBuilder.Entity("Chat.Shared.Messaggio", b =>
                {
                    b.HasOne("Chat.Shared.Utente", "Destinatario")
                        .WithMany()
                        .HasForeignKey("DestinatarioUsername");

                    b.HasOne("Chat.Shared.Utente", "Mittente")
                        .WithMany()
                        .HasForeignKey("MittenteUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destinatario");

                    b.Navigation("Mittente");
                });

            modelBuilder.Entity("Chat.Shared.Utente", b =>
                {
                    b.HasOne("Chat.Shared.Indirizzo", "Indirizzo")
                        .WithMany()
                        .HasForeignKey("IndirizzoId");

                    b.Navigation("Indirizzo");
                });
#pragma warning restore 612, 618
        }
    }
}
