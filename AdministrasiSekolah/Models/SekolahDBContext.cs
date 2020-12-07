using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdministrasiSekolah.Models
{
    public partial class SekolahDBContext : DbContext
    {
        public SekolahDBContext()
        {
        }

        public SekolahDBContext(DbContextOptions<SekolahDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountUser> AccountUser { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Parent> Parent { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUser>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin);

                entity.Property(e => e.IdAdmin)
                    .HasColumnName("Id_admin")
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.NamaAdmin)
                    .HasColumnName("Nama_admin")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Parent>(entity =>
            {
                entity.HasKey(e => e.IdParent);

                entity.Property(e => e.IdParent).HasColumnName("Id_parent");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.NamaAyah)
                    .HasColumnName("Nama_ayah")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NamaIbu)
                    .HasColumnName("Nama_ibu")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Pekerjaan)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PendapatanAyah).HasColumnName("Pendapatan_ayah");

                entity.Property(e => e.PendapatanIbu).HasColumnName("Pendapatan_ibu");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Parent)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Parent_AccountUser");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Nis);

                entity.Property(e => e.Nis)
                    .HasColumnName("NIS")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Alamat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Angkatan)
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IdParent).HasColumnName("Id_parent");

                entity.Property(e => e.IdUser).HasColumnName("Id_user");

                entity.Property(e => e.Kelas)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NamaStudent)
                    .HasColumnName("Nama_Student")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdParentNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdParent)
                    .HasConstraintName("FK_Student_Parent");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Student)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_Student_AccountUser");
            });
        }
    }
}
