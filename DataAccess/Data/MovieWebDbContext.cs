using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class MovieWebDbContext :IdentityDbContext
    {
        public MovieWebDbContext(DbContextOptions<MovieWebDbContext> options) : base(options) { }
        public DbSet<Actor> Actor {  get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Rating> Rating { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Rating>()
                        .HasOne(i => i.Movie)
                        .WithMany(x => x.Ratings)
                        .HasForeignKey(y => y.MovieId)
                        .IsRequired();
            modelBuilder.Entity<Rating>()
                        .HasOne(i => i.User)
                        .WithMany(x => x.Ratings)
                        .HasForeignKey(y => y.UserId)
                        .IsRequired();
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasMany(d => d.Movies).WithMany(p => p.Actors)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieActor",
                        r => r.HasOne<Movie>().WithMany()
                            .HasForeignKey("MovieId")
                            .HasConstraintName("FK_MOVIEACT_MOVIEACT_MOVIE"),
                        l => l.HasOne<Actor>().WithMany()
                            .HasForeignKey("ActorId")
                            .HasConstraintName("FK_MOVIEACT_MOVIEACT_ACTOR"),
                        j =>
                        {
                            j.HasKey("MovieId", "ActorId").HasName("PK_MOVIEACTOR");
                            j.ToTable("MovieActor");
                            j.HasIndex(new[] { "MovieId" }, "MOVIEACTOR2_FK");
                            j.HasIndex(new[] { "ActorId" }, "MOVIEACTOR_FK");
                        });
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.HasMany(d => d.Movies).WithMany(p => p.Genres)
                    .UsingEntity<Dictionary<string, object>>(
                        "MovieGenre",
                        r => r.HasOne<Movie>().WithMany()
                            .HasForeignKey("MovieId")
                            .HasConstraintName("FK_MOVIEGENRE_MOVIEGENRE_MOVIE"),
                        l => l.HasOne<Genre>().WithMany()
                            .HasForeignKey("GenreId")
                            .HasConstraintName("FK_MOVIEGENRE_MOVIEGENRE_GENRE"),
                        j =>
                        {
                            j.HasKey("MovieId", "GenreId").HasName("PK_MOVIEGENRE");
                            j.ToTable("MovieGenre");
                            j.HasIndex(new[] { "MovieId" }, "MOVIEGENRE2_FK");
                            j.HasIndex(new[] { "GenreId" }, "MOVIEGENRE_FK");
                        });
            });
        }


    }
}
