using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TourBase_Stage_1_2
{
    public partial class TourBaseContext : DbContext
    {
        public TourBaseContext()
        {
        }

        public TourBaseContext(DbContextOptions<TourBaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Tourist> Tourists { get; set; }
        public virtual DbSet<Transport> Transports { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= DESKTOP-G7C8QEC; Database = TourBase; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.CityName)
                    .HasMaxLength(50)
                    .HasColumnName("City_name");

                entity.Property(e => e.CountryId).HasColumnName("Country_id");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Cities_Countries");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("Country_Id");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(10)
                    .HasColumnName("Country_Name")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.Property(e => e.HotelId).HasColumnName("Hotel_id");

                entity.Property(e => e.CategoryOfTheService)
                    .HasColumnType("ntext")
                    .HasColumnName("Category_of_the_service");

                entity.Property(e => e.CityId).HasColumnName("City_id");

                entity.Property(e => e.HotelName)
                    .HasMaxLength(50)
                    .HasColumnName("Hotel_name");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Hotels)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK_Hotels_Cities");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.Property(e => e.StageId).HasColumnName("Stage_id");

                entity.Property(e => e.HotelId).HasColumnName("Hotel_id");

                entity.Property(e => e.StageNumber).HasColumnName("Stage_number");

                entity.Property(e => e.TourId).HasColumnName("Tour_id");

                entity.Property(e => e.TransportId).HasColumnName("Transport_id");

                entity.HasOne(d => d.Hotel)
                    .WithMany(p => p.Stages)
                    .HasForeignKey(d => d.HotelId)
                    .HasConstraintName("FK_Stages_Hotels");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Stages)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Stages_Tours");

                entity.HasOne(d => d.Transport)
                    .WithMany(p => p.Stages)
                    .HasForeignKey(d => d.TransportId)
                    .HasConstraintName("FK_Stages_Transports");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.Property(e => e.TourId).HasColumnName("Tour_id");

                entity.Property(e => e.DurationInDays).HasColumnName("Duration_in_days");

                entity.Property(e => e.Info).HasColumnType("ntext");

                entity.Property(e => e.TourName)
                    .HasMaxLength(50)
                    .HasColumnName("Tour_name");
            });

            modelBuilder.Entity<Tourist>(entity =>
            {
                entity.Property(e => e.TouristId).HasColumnName("Tourist_id");

                entity.Property(e => e.BirthDate).HasColumnName("Birth_date");

                entity.Property(e => e.EmailAdress)
                    .HasMaxLength(50)
                    .HasColumnName("Email_adress");

                entity.Property(e => e.TouristName)
                    .HasMaxLength(50)
                    .HasColumnName("Tourist_name");
            });

            modelBuilder.Entity<Transport>(entity =>
            {
                entity.Property(e => e.TransportId).HasColumnName("Transport_id");

                entity.Property(e => e.TransportName)
                    .HasMaxLength(50)
                    .HasColumnName("Transport_name");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.Property(e => e.VoucherId).HasColumnName("Voucher_id");

                entity.Property(e => e.TakeOffDate)
                    .HasColumnType("date")
                    .HasColumnName("Take_off_date");

                entity.Property(e => e.TourId).HasColumnName("Tour_id");

                entity.Property(e => e.TouristId).HasColumnName("Tourist_id");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Vouchers_Tours");

                entity.HasOne(d => d.Tourist)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.TouristId)
                    .HasConstraintName("FK_Vouchers_Tourists");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
