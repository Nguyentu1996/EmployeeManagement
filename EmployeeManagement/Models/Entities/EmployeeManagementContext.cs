using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EmployeeManagement.Models.HumanResources.Response;

namespace EmployeeManagement.Models.Entities
{
    public partial class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext()
        {
        }

        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnnualLeave> AnnualLeave { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<LeaveApplication> LeaveApplication { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Statistics> Statistics { get; set; }
        public virtual DbSet<TimeSheet> TimeSheet { get; set; }
        public virtual DbSet<TimeSheetsLog> TimeSheetsLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=BAO-NGUYEN;Database=EmployeeManagement;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AnnualLeave>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AnnualLeave)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnnualLeave_Employee");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Department")
                    .IsUnique();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.DepartmentId);

                entity.HasIndex(e => e.Email)
                    .HasName("IX_Employee")
                    .IsUnique();

                entity.HasIndex(e => e.PositionId);

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("date");

                entity.Property(e => e.EditDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IdNumber).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.TaxId).HasMaxLength(50);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Position");
            });

            modelBuilder.Entity<LeaveApplication>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.ManagerId);

                entity.Property(e => e.Comment).HasMaxLength(450);

                entity.Property(e => e.CommentDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Feedback).HasMaxLength(450);

                entity.Property(e => e.FeedbackDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.LeaveApplicationEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveApplication_Employee");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.LeaveApplicationManager)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LeaveApplication_Employee1");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("IX_Position")
                    .IsUnique();

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Statistics>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.Property(e => e.PaidLeave).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.Unauthorized).HasColumnType("decimal(4, 1)");

                entity.Property(e => e.UnpaidLeave).HasColumnType("decimal(4, 1)");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Statistics)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Statistics_Employee");
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId);

                entity.HasIndex(e => e.ManagerId);

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EditDate).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeSheetEmployee)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeSheet_Employee");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.TimeSheetManager)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeSheet_Employee1");
            });

            modelBuilder.Entity<TimeSheetsLog>(entity =>
            {
                entity.HasIndex(e => e.TimeSheetsId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EditDate).HasColumnType("date");

                entity.HasOne(d => d.TimeSheets)
                    .WithMany(p => p.TimeSheetsLog)
                    .HasForeignKey(d => d.TimeSheetsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeSheetsLog_TimeSheet");
            });

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, PositionId = 3, DepartmentId = 1, FullName = "Lê Nguyễn Phước Thành", Sex = true, Dob = new DateTime(1995, 02, 03), IdNumber = "2514254253", PhoneNumber = "0325123657", Email = "lnpttor@gmail.com", Address = "Huế", TaxId = "231251254", Image = null, CreateDate = new DateTime(2019, 10, 10), EditDate = new DateTime(2019, 10, 10) },
                new Employee() { Id = 2, PositionId = 2, DepartmentId = 1, FullName = "Tú", Sex = true, Dob = new DateTime(2000, 10, 01), IdNumber = "2021252125", PhoneNumber = "0325122422", Email = "tu@gmail.com", Address = "Huế", TaxId = "325125425", Image = null, CreateDate = new DateTime(2019, 10, 10), EditDate = new DateTime(2019, 10, 10) },
                new Employee() { Id = 3, PositionId = 1, DepartmentId = 2, FullName = "Trí", Sex = true, Dob = new DateTime(2000, 10, 09), IdNumber = "2521425125", PhoneNumber = "0785425425", Email = "tri@gmail.com", Address = "Huế", TaxId = "021323652", Image = null, CreateDate = new DateTime(2019, 10, 10), EditDate = new DateTime(2019, 10, 10) },
                new Employee() { Id = 4, PositionId = 1, DepartmentId = 2, FullName = "Nhân", Sex = true, Dob = new DateTime(2000, 11, 22), IdNumber = "2122225254", PhoneNumber = "0765854752", Email = "nhan@gmail.com", Address = "Huế", TaxId = "232114225", Image = null, CreateDate = new DateTime(2019, 10, 10), EditDate = new DateTime(2019, 10, 10) },
                new Employee() { Id = 5, PositionId = 1, DepartmentId = 1, FullName = "Báo", Sex = true, Dob = new DateTime(1990, 01, 02), IdNumber = "2121252142", PhoneNumber = "0325124256", Email = "bao@gmail.com", Address = "Huế", TaxId = "322521242", Image = null, CreateDate = new DateTime(2019, 10, 10), EditDate = new DateTime(2019, 10, 10) }
            );

            modelBuilder.Entity<Department>().HasData(
                new Department() { Id = 1, Name = "Kinh doanh" },
                new Department() { Id = 2, Name = "Nhân sự" },
                new Department() { Id = 3, Name = "Lập trình" }
            );

            modelBuilder.Entity<Position>().HasData(
               new Position() { Id = 1, Name = "Nhân viên" },
               new Position() { Id = 2, Name = "Phó phòng" },
               new Position() { Id = 3, Name = "Trưởng phòng" }
            );
        }

        public DbSet<EmployeeManagement.Models.HumanResources.Response.DepartmentView> DepartmentView { get; set; }
    }
}
