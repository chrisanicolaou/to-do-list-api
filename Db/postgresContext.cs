﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace dotnet_backend
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<ToDoItem> ToDoItems { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                Connection connDetails = new Connection();
                connDetails = Connection.GetSecret();
                optionsBuilder.UseNpgsql($"host={connDetails?.host};port={connDetails?.port};username={connDetails?.username};password={connDetails?.password};database={connDetails?.engine}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Password).HasColumnName("password");
            });
            modelBuilder.Entity<ToDoItem>(entity =>
            {
                entity.HasKey(e => e.ToDoId)
                    .HasName("to_do_items_pkey");

                entity.ToTable("to_do_items");

                entity.Property(e => e.ToDoId).HasColumnName("to_do_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.UserEmail).HasColumnName("user_email");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
