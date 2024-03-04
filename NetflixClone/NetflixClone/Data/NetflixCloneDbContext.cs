﻿using Microsoft.EntityFrameworkCore;
using NetflixClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetflixClone.Data
{
    public class NetflixCloneDbContext : DbContext
    {
        public DbSet<Utente> Utenti {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utente>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.NomeUtente)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DataCreazione)
                    .IsRequired();

                entity.Property(e => e.Iban)
                    .HasMaxLength(17);
            });

            modelBuilder.Entity<Profilo>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(15);

                entity.HasOne(e => e.Utente)
                .WithMany(u => u.Profili)
                .HasForeignKey(e => e.UtenteId)
                .IsRequired();

            });
            modelBuilder.Entity<Film>().ToTable("Film");
            modelBuilder.Entity<SerieTv>().ToTable("SerieTv");
        }

        public DbSet<Profilo> Profili { get; set; }
        public DbSet<Video> Video { get; set; }

        /*Not sure if needed -----------------------*/

        public DbSet<Film> Films { get; set; }
        public DbSet<SerieTv> SerieTv { get; set; }
        public DbSet<Stagione> Stagioni { get; set; }

        public DbSet<Episodio> Episodi { get; set; }
        /*------------------------------------------*/
        public string ConnectionString { get; }

        public NetflixCloneDbContext()
        {
            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=NetflixCloneDb;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public NetflixCloneDbContext(DbContextOptions<NetflixCloneDbContext> options) : base(options)
        {
            ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=NetflixCloneDb;Trusted_Connection=True;MultipleActiveResultSets=true";

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
