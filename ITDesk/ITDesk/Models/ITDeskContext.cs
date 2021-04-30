using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ITDesk.Models
{
    public partial class ITDeskContext : DbContext
    {
        public ITDeskContext()
        {
        }

        public ITDeskContext(DbContextOptions<ITDeskContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditTrail> AuditTrail { get; set; }
        public virtual DbSet<DeviceCategory> DeviceCategory { get; set; }
        public virtual DbSet<DeviceInfo> DeviceInfo { get; set; }
        public virtual DbSet<EmployeeInfo> EmployeeInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CYG403\\SQLEXPRESS;Database=ITDesk;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditTrail>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UniqueCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeviceCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.DeviceType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeviceInfo>(entity =>
            {
                entity.HasKey(e => e.DeviceId);

                entity.HasIndex(e => e.UniqueCode)
                    .HasName("UQ__DeviceIn__BB96DE6F809A3341")
                    .IsUnique();

                entity.Property(e => e.AssignedBy)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AssignedDate).HasColumnType("date");

                entity.Property(e => e.DeviceName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsAssigned).HasDefaultValueSql("((0))");

                entity.Property(e => e.QrCode).IsUnicode(false);

                entity.Property(e => e.UniqueCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.DeviceInfo)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DeviceInf__Categ__49C3F6B7");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.DeviceInfo)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__DeviceInf__Emplo__4AB81AF0");
            });

            modelBuilder.Entity<EmployeeInfo>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.Designation)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeEmail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasDefaultValueSql("('2c9c31108265d77886569d52e0a1f883')");
            });
        }
    }
}
