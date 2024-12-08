﻿using Microsoft.EntityFrameworkCore;
using AfishaApi.Models; // Убедитесь, что пространство имен правильно указано

namespace AfishaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet для пользователей
        public DbSet<UserEntityDb> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<UserEntityDb>()
               // .HasKey(u => u.Id);
            // Здесь вы можете также добавить другие настройки для вашей модели
        }

    }
}