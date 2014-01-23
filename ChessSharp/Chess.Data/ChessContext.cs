using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Chess.Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chess.Data
{
    public class ChessContext : DbContext, IUnitOfWork
    {
        public ChessContext()
            : base("ChessContext")
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Square> Squares { get; set; }
        public DbSet<Challenge> Challenges { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<ChessPiece> ChessPieces { get; set; }
        public DbSet<ChessUser> ChessUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Entity<Game>().HasMany(x => x.Squares);
            modelBuilder.Entity<Game>().HasMany(x => x.Moves);
            modelBuilder.Entity<Game>().HasRequired(x => x.Challenge);
            modelBuilder.Entity<Game>().HasOptional(x => x.WinnerPlayer);

            modelBuilder.Entity<Move>().HasRequired(x => x.Game);

            modelBuilder.Entity<Square>().HasRequired(x => x.Game);
            modelBuilder.Entity<Square>().HasOptional(x => x.ChessPiece);

            modelBuilder.Entity<ChessPiece>().HasRequired(x => x.Game);

            modelBuilder.Entity<Player>().HasRequired(x => x.ChessUser);

            modelBuilder.Entity<Player>().HasMany(x => x.Challenges)
                .WithRequired(x => x.ChallengingPlayer)
                .HasForeignKey(x => x.ChallengingPlayerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasMany(x => x.Challenges)
                .WithRequired(x => x.LightPlayer)
                .HasForeignKey(x => x.LightPlayerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasMany(x => x.Challenges)
                .WithRequired(x => x.DarkPlayer)
                .HasForeignKey(x => x.DarkPlayerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>().HasMany(x => x.Games)
                .WithRequired(x => x.LightPlayer)
                .HasForeignKey(x => x.LightPlayerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasMany(x => x.Games)
                .WithRequired(x => x.DarkPlayer)
                .HasForeignKey(x => x.DarkPlayerId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasMany(x => x.Games)
                .WithRequired(x => x.WinnerPlayer)
                .HasForeignKey(x => x.WinnerPlayerId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public T Find<T>(params object[] keyValues) where T : class, IEntity
        {
            return Set<T>().Find(keyValues);
        }

        public T Find<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> All<T>() where T : class, IEntity
        {
            return Set<T>();
        }

        public IEnumerable<T> All<T>(Func<T, bool> predicate) where T : class, IEntity
        {
            return Set<T>().Where(predicate);
        }

        public void Remove<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Remove(entity);
        }

        public void Remove<T>(IEnumerable<T> entities) where T : class, IModifiable
        {
            foreach (var entity in entities)
                Set<T>().Remove(entity);
        }

        public void Add<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Add(entity);
        }

        public void Attach<T>(T entity) where T : class, IModifiable
        {
            Set<T>().Attach(entity);
        }

        public bool Exists<T>(T entity) where T : class, IModifiable
        {
            return Set<T>().Local.Any(e => e == entity);
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
