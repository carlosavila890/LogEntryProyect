using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using bd.log.datos;

namespace bd.log.datos.Migrations
{
    [DbContext(typeof(LogDbContext))]
    [Migration("20170728200120_creacion_entidades")]
    partial class creacion_entidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("bd.log.entidades.LogCategory", b =>
                {
                    b.Property<int>("LogCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("ParameterValue")
                        .IsRequired()
                        .HasMaxLength(32);

                    b.HasKey("LogCategoryId");

                    b.ToTable("LogCategories");
                });

            modelBuilder.Entity("bd.log.entidades.LogEntry", b =>
                {
                    b.Property<int>("LogEntryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("ExceptionTrace")
                        .HasMaxLength(4096);

                    b.Property<int>("LogCategoryId");

                    b.Property<DateTime>("LogDate");

                    b.Property<int>("LogLevelId");

                    b.Property<string>("MachineIP")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("MachineName")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("ObjEntityId")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.HasKey("LogEntryId");

                    b.HasIndex("LogCategoryId");

                    b.HasIndex("LogLevelId");

                    b.ToTable("LogEntries");
                });

            modelBuilder.Entity("bd.log.entidades.LogLevel", b =>
                {
                    b.Property<int>("LogLevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(5);

                    b.HasKey("LogLevelId");

                    b.ToTable("LogLevels");
                });

            modelBuilder.Entity("bd.log.entidades.LogEntry", b =>
                {
                    b.HasOne("bd.log.entidades.LogCategory", "LogCategory")
                        .WithMany("LogEntries")
                        .HasForeignKey("LogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("bd.log.entidades.LogLevel", "LogLevel")
                        .WithMany("LogEntries")
                        .HasForeignKey("LogLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
