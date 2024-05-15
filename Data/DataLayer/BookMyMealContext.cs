using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookMyMeal.Data.DataLayer
{
    public partial class BookMyMealContext : DbContext
    {
        public BookMyMealContext()
        {
        }

        public BookMyMealContext(DbContextOptions<BookMyMealContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bookmymeal> Bookmymeals { get; set; } = null!;
        public virtual DbSet<Coupon> Coupons { get; set; } = null!;
        public virtual DbSet<Employeelogin> Employeelogins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=MealBookingContext");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bookmymeal>(entity =>
            {
                entity.HasKey(e => e.MealBookingId)
                    .HasName("PK__bookmyme__456CF4E5AE23D76B");

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsBooked).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.BookmymealCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bookmymeal_employeelogin1");

                entity.HasOne(d => d.EmployeeLogin)
                    .WithMany(p => p.BookmymealEmployeeLogins)
                    .HasForeignKey(d => d.EmployeeLoginId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_bookmymeal_employeelogin");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponId).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.UpdateOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.MealBooking)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.MealBookingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_coupon_bookmymeal");
            });

            modelBuilder.Entity<Employeelogin>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__employee__1299A861DFFECE57");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
