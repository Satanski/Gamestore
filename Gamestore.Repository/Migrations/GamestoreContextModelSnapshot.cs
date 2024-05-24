﻿// <auto-generated />
using System;
using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gamestore.DAL.Migrations
{
    [DbContext(typeof(GamestoreContext))]
    partial class GamestoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gamestore.Repository.Entities.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c6c84790-2f43-4f7c-b6df-94c70491343d"),
                            Description = "Desc",
                            Key = "Key",
                            Name = "Gra testowa nazwa"
                        });
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.GameGenre", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("GameGenres");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.GamePlatform", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlatformId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "PlatformId");

                    b.HasIndex("PlatformId");

                    b.ToTable("GamePlatforms");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("ParentGenreId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8"),
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = new Guid("6cc174bc-1725-4f54-bda8-b57389288b2a"),
                            Name = "RTS",
                            ParentGenreId = new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8")
                        },
                        new
                        {
                            Id = new Guid("658e2c8b-d5f5-4e23-a4ff-deea222748dc"),
                            Name = "TBS",
                            ParentGenreId = new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8")
                        },
                        new
                        {
                            Id = new Guid("ac35a93d-e380-4cfc-bf6a-683180718c4f"),
                            Name = "RPG"
                        },
                        new
                        {
                            Id = new Guid("934c60a7-2755-4c12-8420-6b1e017c0144"),
                            Name = "Sports"
                        },
                        new
                        {
                            Id = new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6"),
                            Name = "Races"
                        },
                        new
                        {
                            Id = new Guid("824ead0b-4e95-4d7b-b9b8-f47b009442b3"),
                            Name = "Rally",
                            ParentGenreId = new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6")
                        },
                        new
                        {
                            Id = new Guid("e5c77a2a-40f1-4dfa-b847-7b5b2c15e52d"),
                            Name = "Arcade",
                            ParentGenreId = new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6")
                        },
                        new
                        {
                            Id = new Guid("ba252c3a-ff3d-42a1-ba75-2b21e8690452"),
                            Name = "Formula",
                            ParentGenreId = new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6")
                        },
                        new
                        {
                            Id = new Guid("dbf1e07c-93f8-4a9e-9238-3e027f13b8b7"),
                            Name = "Off-road",
                            ParentGenreId = new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6")
                        },
                        new
                        {
                            Id = new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354"),
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("88de2753-24ea-475f-a59f-0608359d2b8b"),
                            Name = "FPS",
                            ParentGenreId = new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354")
                        },
                        new
                        {
                            Id = new Guid("200c416a-538c-498e-a2a0-cdf4c74e142b"),
                            Name = "TPS",
                            ParentGenreId = new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354")
                        },
                        new
                        {
                            Id = new Guid("a864da3e-2e9e-4c03-8eaa-ca6c5322b1c9"),
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("8b447f60-7cca-460a-bc6a-36e02015f46a"),
                            Name = "Puzzle & Skill"
                        });
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.Platform", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Type")
                        .IsUnique();

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7289405-cd3c-48e5-b858-9ae4faa889d4"),
                            Type = "Mobile"
                        },
                        new
                        {
                            Id = new Guid("f29416dc-e41a-4131-8e66-93ec11bc8e45"),
                            Type = "Browser"
                        },
                        new
                        {
                            Id = new Guid("d1099105-d805-49e4-b2c7-c1fcf4040a81"),
                            Type = "Desktop"
                        },
                        new
                        {
                            Id = new Guid("644a99d9-dce9-4057-b94e-ee704c620b61"),
                            Type = "Console"
                        });
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.GameGenre", b =>
                {
                    b.HasOne("Gamestore.Repository.Entities.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gamestore.Repository.Entities.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.GamePlatform", b =>
                {
                    b.HasOne("Gamestore.Repository.Entities.Game", "Game")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gamestore.Repository.Entities.Platform", "Platform")
                        .WithMany("GamePlatforms")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.Game", b =>
                {
                    b.Navigation("GameGenres");

                    b.Navigation("GamePlatforms");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.Genre", b =>
                {
                    b.Navigation("GameGenres");
                });

            modelBuilder.Entity("Gamestore.Repository.Entities.Platform", b =>
                {
                    b.Navigation("GamePlatforms");
                });
#pragma warning restore 612, 618
        }
    }
}
