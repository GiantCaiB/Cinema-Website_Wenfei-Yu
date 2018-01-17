using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieCineplex.Models
{
    public partial class ABCDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ABCData;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cineplex>(entity =>
            {
                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.Location).IsRequired();

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();
            });

            modelBuilder.Entity<CineplexMovie>(entity =>
            {
                entity.HasKey(e => new { e.CineplexId, e.MovieId })
                    .HasName("PK__Cineplex__CB419E6D57651C75");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Cinep__2B3F6F97");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.CineplexMovie)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__CineplexM__Movie__2C3393D0");
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.Property(e => e.EnquiryId).HasColumnName("EnquiryID");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.Message).IsRequired();
            });

            modelBuilder.Entity<EnquiryEvents>(entity =>
            {
                entity.HasKey(e => new { e.EventsId, e.EnquiryId })
                    .HasName("PK__EnquiryE__D153703B506E62AD");

                entity.Property(e => e.EventsId).HasColumnName("EventsID");

                entity.Property(e => e.EnquiryId).HasColumnName("EnquiryID");

                entity.HasOne(d => d.Enquiry)
                    .WithMany(p => p.EnquiryEvents)
                    .HasForeignKey(d => d.EnquiryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__EnquiryEv__Enqui__398D8EEE");

                entity.HasOne(d => d.Events)
                    .WithMany(p => p.EnquiryEvents)
                    .HasForeignKey(d => d.EventsId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__EnquiryEv__Event__38996AB5");
            });

            modelBuilder.Entity<EventsInfo>(entity =>
            {
                entity.HasKey(e => e.EventsId)
                    .HasName("PK__EventsIn__11F369821C35C6D2");

                entity.Property(e => e.EventsId).HasColumnName("EventsID");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<MovieComingSoon>(entity =>
            {
                entity.Property(e => e.MovieComingSoonId).HasColumnName("MovieComingSoonID");

                entity.Property(e => e.LongDescription).IsRequired();

                entity.Property(e => e.ShortDescription).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.EnquiryId).HasColumnName("EnquiryID");

                entity.Property(e => e.SeatId).HasColumnName("SeatID");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.HasOne(d => d.Enquiry)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.EnquiryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Reservati__Enqui__32E0915F");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Reservation)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__Reservati__Sessi__33D4B598");
            });

            modelBuilder.Entity<SessionTime>(entity =>
            {
                entity.HasKey(e => e.SessionId)
                    .HasName("PK__SessionT__C9F4927052BD2243");

                entity.Property(e => e.SessionId).HasColumnName("SessionID");

                entity.Property(e => e.CineplexId).HasColumnName("CineplexID");

                entity.Property(e => e.MovieId).HasColumnName("MovieID");

                entity.Property(e => e.MovieTime).HasColumnType("datetime");

                entity.HasOne(d => d.Cineplex)
                    .WithMany(p => p.SessionTime)
                    .HasForeignKey(d => d.CineplexId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__SessionTi__Cinep__2F10007B");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.SessionTime)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__SessionTi__Movie__300424B4");
            });
        }

        public virtual DbSet<Cineplex> Cineplex { get; set; }
        public virtual DbSet<CineplexMovie> CineplexMovie { get; set; }
        public virtual DbSet<Enquiry> Enquiry { get; set; }
        public virtual DbSet<EnquiryEvents> EnquiryEvents { get; set; }
        public virtual DbSet<EventsInfo> EventsInfo { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieComingSoon> MovieComingSoon { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<SessionTime> SessionTime { get; set; }
    }
}