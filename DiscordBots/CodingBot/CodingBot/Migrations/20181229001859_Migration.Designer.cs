﻿// <auto-generated />
using CodingBot.Resources.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodingBot.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    [Migration("20181229001859_Migration")]
    partial class Migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity("CodingBot.Resources.Database.Spam", b =>
                {
                    b.Property<ulong>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("MessagesSend");

                    b.Property<string>("Name");

                    b.HasKey("UserId");

                    b.ToTable("spam");
                });
#pragma warning restore 612, 618
        }
    }
}
