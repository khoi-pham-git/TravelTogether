using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace TravelTogether2.Models
{
    public partial class TourGuide_v2Context : DbContext
    {
        public TourGuide_v2Context()
        {
        }

        public TourGuide_v2Context(DbContextOptions<TourGuide_v2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Follow> Follows { get; set; }
        public virtual DbSet<HasLanguage> HasLanguages { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<TourActivity> TourActivities { get; set; }
        public virtual DbSet<TourGuide> TourGuides { get; set; }
        public virtual DbSet<TravelAgency> TravelAgencies { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripActivity> TripActivities { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=MINHLUAN\\MINHLUAN;Initial Catalog=TourGuide_v2;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Email);

                entity.ToTable("Account");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("Area");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Latitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longtitude)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TravelAgencyId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TravelAgency)
                    .WithMany(p => p.Areas)
                    .HasForeignKey(d => d.TravelAgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_TravelAgency");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("Follow");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Follow_Customer");

                entity.HasOne(d => d.TourGuide)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.TourGuideId)
                    .HasConstraintName("FK_Follow_TourGuide");
            });

            modelBuilder.Entity<HasLanguage>(entity =>
            {
                entity.ToTable("HasLanguage");

                entity.HasOne(d => d.Language)
                    .WithMany(p => p.HasLanguages)
                    .HasForeignKey(d => d.LanguageId)
                    .HasConstraintName("FK_HasLanguage_Language");

                entity.HasOne(d => d.TourGuide)
                    .WithMany(p => p.HasLanguages)
                    .HasForeignKey(d => d.TourGuideId)
                    .HasConstraintName("FK_HasLanguage_TourGuide");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.ToTable("Language");

                entity.Property(e => e.Language1)
                    .HasMaxLength(100)
                    .HasColumnName("Language");

                entity.Property(e => e.Level).HasMaxLength(50);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PaymentCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_Payment_Trip");
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.ToTable("Place");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Latitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Longtitude)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.AreaId)
                    .HasConstraintName("FK_Place_Area");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Places)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Place_Category");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.TourGuide)
                    .WithMany(p => p.Tours)
                    .HasForeignKey(d => d.TourGuideId)
                    .HasConstraintName("FK_Tour_TourGuide");
            });

            modelBuilder.Entity<TourActivity>(entity =>
            {
                entity.ToTable("TourActivity");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.TourActivities)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_TourActivity_Place");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.TourActivities)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_TourActivity_Tour");
            });

            modelBuilder.Entity<TourGuide>(entity =>
            {
                entity.ToTable("TourGuide");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Certification)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SocialNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TravelAgencyId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.TravelAgency)
                    .WithMany(p => p.TourGuides)
                    .HasForeignKey(d => d.TravelAgencyId)
                    .HasConstraintName("FK_TourGuide_TravelAgency");
            });

            modelBuilder.Entity<TravelAgency>(entity =>
            {
                entity.ToTable("TravelAgency");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.BookingDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Feedback).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Trip_Customer");

                entity.HasOne(d => d.Tour)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("FK_Trip_Tour");
            });

            modelBuilder.Entity<TripActivity>(entity =>
            {
                entity.ToTable("TripActivity");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.TourActivity)
                    .WithMany(p => p.TripActivities)
                    .HasForeignKey(d => d.TourActivityId)
                    .HasConstraintName("FK_TripActivity_TourActivity");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripActivities)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_TripActivity_Trip");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
