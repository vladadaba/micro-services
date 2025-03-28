﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using JobService.Models;

namespace JobService.Migrations
{
    [DbContext(typeof(JobContext))]
    [Migration("20201115010908_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("JobService.Models.JobItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnName("status")
                        .HasColumnType("integer");

                    b.HasKey("Id")
                        .HasName("pk_job_items");

                    b.ToTable("job_items");
                });

            modelBuilder.Entity("JobService.Models.OutboxItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uuid");

                    b.Property<string>("AggregateId")
                        .IsRequired()
                        .HasColumnName("aggregate_id")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("AggregateType")
                        .IsRequired()
                        .HasColumnName("aggregate_type")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("CorrelationId")
                        .IsRequired()
                        .HasColumnName("correlation_id")
                        .HasColumnType("text");

                    b.Property<string>("Payload")
                        .HasColumnName("payload")
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("timestamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnName("type")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("pk_outbox_items");

                    b.ToTable("outbox_items");
                });
#pragma warning restore 612, 618
        }
    }
}
