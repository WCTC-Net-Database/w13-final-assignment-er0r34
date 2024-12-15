﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleRpgEntities.Migrations
{
    [DbContext(typeof(GameContext))]
    [Migration("20241215032948_UpdateItemSchema2")]
    partial class UpdateItemSchema2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AbilityPlayer", b =>
                {
                    b.Property<int>("AbilitiesId")
                        .HasColumnType("int");

                    b.Property<int>("PlayersId")
                        .HasColumnType("int");

                    b.HasKey("AbilitiesId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("PlayerAbilities", (string)null);
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Abilities.PlayerAbilities.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AbilityType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<int>("AttackBonus")
                        .HasColumnType("int");

                    b.Property<int>("DefenseBonus")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abilities");

                    b.HasDiscriminator<string>("AbilityType").HasValue("Ability");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Characters.Monsters.Monster", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AggressionLevel")
                        .HasColumnType("int");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("MonsterType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Monsters");

                    b.HasDiscriminator<string>("MonsterType").HasValue("Monster");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Equipments.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ArmorId")
                        .HasColumnType("int");

                    b.Property<int?>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArmorId");

                    b.HasIndex("WeaponId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Equipments.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Rooms.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EastId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NorthId")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("SouthId")
                        .HasColumnType("int");

                    b.Property<int?>("WestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EastId");

                    b.HasIndex("NorthId");

                    b.HasIndex("SouthId");

                    b.HasIndex("WestId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("InventoryId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int?>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<int>("Health")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("RoomId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Abilities.PlayerAbilities.ShoveAbility", b =>
                {
                    b.HasBaseType("ConsoleRpgEntities.Models.Abilities.PlayerAbilities.Ability");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("ShoveAbility");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Characters.Monsters.Goblin", b =>
                {
                    b.HasBaseType("ConsoleRpgEntities.Models.Characters.Monsters.Monster");

                    b.Property<int>("Sneakiness")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Goblin");
                });

            modelBuilder.Entity("AbilityPlayer", b =>
                {
                    b.HasOne("ConsoleRpgEntities.Models.Abilities.PlayerAbilities.Ability", null)
                        .WithMany()
                        .HasForeignKey("AbilitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Characters.Monsters.Monster", b =>
                {
                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "Room")
                        .WithMany("Monsters")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Equipments.Equipment", b =>
                {
                    b.HasOne("Item", "Armor")
                        .WithMany()
                        .HasForeignKey("ArmorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Item", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Armor");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Equipments.Inventory", b =>
                {
                    b.HasOne("Player", "Player")
                        .WithOne("Inventory")
                        .HasForeignKey("ConsoleRpgEntities.Models.Equipments.Inventory", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Rooms.Room", b =>
                {
                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "East")
                        .WithMany()
                        .HasForeignKey("EastId");

                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "North")
                        .WithMany()
                        .HasForeignKey("NorthId");

                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "South")
                        .WithMany()
                        .HasForeignKey("SouthId");

                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "West")
                        .WithMany()
                        .HasForeignKey("WestId");

                    b.Navigation("East");

                    b.Navigation("North");

                    b.Navigation("South");

                    b.Navigation("West");
                });

            modelBuilder.Entity("Item", b =>
                {
                    b.HasOne("ConsoleRpgEntities.Models.Equipments.Inventory", null)
                        .WithMany("Items")
                        .HasForeignKey("InventoryId");

                    b.HasOne("Player", null)
                        .WithMany("InventoryItems")
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.HasOne("ConsoleRpgEntities.Models.Equipments.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId");

                    b.HasOne("ConsoleRpgEntities.Models.Rooms.Room", "Room")
                        .WithMany("Players")
                        .HasForeignKey("RoomId");

                    b.Navigation("Equipment");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Equipments.Inventory", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ConsoleRpgEntities.Models.Rooms.Room", b =>
                {
                    b.Navigation("Monsters");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Navigation("Inventory")
                        .IsRequired();

                    b.Navigation("InventoryItems");
                });
#pragma warning restore 612, 618
        }
    }
}
