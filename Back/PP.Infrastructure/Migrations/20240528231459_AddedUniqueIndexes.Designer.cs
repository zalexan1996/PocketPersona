﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PP.Infrastructure;

#nullable disable

namespace PP.Infrastructure.Migrations
{
    [DbContext(typeof(PersonaDbContext))]
    [Migration("20240528231459_AddedUniqueIndexes")]
    partial class AddedUniqueIndexes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArcanaGame", b =>
                {
                    b.Property<int>("AppearsInId")
                        .HasColumnType("int");

                    b.Property<int>("ArcanaId")
                        .HasColumnType("int");

                    b.HasKey("AppearsInId", "ArcanaId");

                    b.HasIndex("ArcanaId");

                    b.ToTable("ArcanaGame");
                });

            modelBuilder.Entity("PP.Domain.Entities.Arcana", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Arcana");
                });

            modelBuilder.Entity("PP.Domain.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppearsInId")
                        .HasColumnType("int");

                    b.Property<string>("Gifts")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AppearsInId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Character");
                });

            modelBuilder.Entity("PP.Domain.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Game");
                });

            modelBuilder.Entity("PP.Domain.Entities.SocialLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArcanaId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("UnlockConditions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArcanaId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("SocialLink");
                });

            modelBuilder.Entity("PP.Domain.Entities.SocialLinkDialogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("SocialLinkId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("SocialLinkId");

                    b.ToTable("SocialLinkDialogue");
                });

            modelBuilder.Entity("ArcanaGame", b =>
                {
                    b.HasOne("PP.Domain.Entities.Game", null)
                        .WithMany()
                        .HasForeignKey("AppearsInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PP.Domain.Entities.Arcana", null)
                        .WithMany()
                        .HasForeignKey("ArcanaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PP.Domain.Entities.Character", b =>
                {
                    b.HasOne("PP.Domain.Entities.Game", "AppearsIn")
                        .WithMany()
                        .HasForeignKey("AppearsInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppearsIn");
                });

            modelBuilder.Entity("PP.Domain.Entities.SocialLink", b =>
                {
                    b.HasOne("PP.Domain.Entities.Arcana", "Arcana")
                        .WithMany()
                        .HasForeignKey("ArcanaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PP.Domain.Entities.Character", "Character")
                        .WithOne()
                        .HasForeignKey("PP.Domain.Entities.SocialLink", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Arcana");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("PP.Domain.Entities.SocialLinkDialogue", b =>
                {
                    b.HasOne("PP.Domain.Entities.SocialLink", "SocialLink")
                        .WithMany("Dialogues")
                        .HasForeignKey("SocialLinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SocialLink");
                });

            modelBuilder.Entity("PP.Domain.Entities.SocialLink", b =>
                {
                    b.Navigation("Dialogues");
                });
#pragma warning restore 612, 618
        }
    }
}
