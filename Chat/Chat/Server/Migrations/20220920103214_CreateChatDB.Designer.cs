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
    [Migration("20220920103214_CreateChatDB")]
    partial class CreateChatDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc.1.22426.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.Property<string>("Testo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UtenteUsername")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UtenteUsername");

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

            modelBuilder.Entity("Chat.Shared.Messaggio", b =>
                {
                    b.HasOne("Chat.Shared.Utente", "Utente")
                        .WithMany()
                        .HasForeignKey("UtenteUsername")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utente");
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