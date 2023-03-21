﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManagementAPI.Data;

#nullable disable

namespace TaskManagementAPI.Migrations
{
    [DbContext(typeof(TaskManagementDbContext))]
    [Migration("20230321213432_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManagementAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_categories");

                    b.ToTable("categories", (string)null);
                });

            modelBuilder.Entity("TaskManagementAPI.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("MediaURL")
                        .HasColumnType("text")
                        .HasColumnName("media_url");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("task_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_comments");

                    b.HasIndex("TaskId")
                        .HasDatabaseName("ix_comments_task_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_comments_user_id");

                    b.ToTable("comments", (string)null);
                });

            modelBuilder.Entity("TaskManagementAPI.Models.SharedTask", b =>
                {
                    b.Property<int>("TaskId")
                        .HasColumnType("integer")
                        .HasColumnName("task_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean")
                        .HasColumnName("approved");

                    b.HasKey("TaskId", "UserId")
                        .HasName("pk_shared_tasks");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_shared_tasks_user_id");

                    b.ToTable("shared_tasks", (string)null);
                });

            modelBuilder.Entity("TaskManagementAPI.Models.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("due_date");

                    b.Property<int>("PriorityOfTask")
                        .HasColumnType("integer")
                        .HasColumnName("priority_of_task");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("project_id");

                    b.Property<int>("Status")
                        .HasMaxLength(15)
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_tasks");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_tasks_user_id");

                    b.ToTable("tasks", (string)null);
                });

            modelBuilder.Entity("TaskManagementAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)")
                        .HasColumnName("name");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_hash");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("password_salt");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("TaskManagementAPI.Models.Comment", b =>
                {
                    b.HasOne("TaskManagementAPI.Models.TaskEntity", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_tasks_task_id");

                    b.HasOne("TaskManagementAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_comments_users_user_id");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagementAPI.Models.SharedTask", b =>
                {
                    b.HasOne("TaskManagementAPI.Models.TaskEntity", "Task")
                        .WithMany()
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shared_tasks_tasks_task_id");

                    b.HasOne("TaskManagementAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_shared_tasks_users_user_id");

                    b.Navigation("Task");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagementAPI.Models.TaskEntity", b =>
                {
                    b.HasOne("TaskManagementAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tasks_users_user_id");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
