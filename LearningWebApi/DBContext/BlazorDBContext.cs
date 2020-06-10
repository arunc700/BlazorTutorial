using LearningModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningWebApi.DBContext
{
    public class BlazorDBContext : DbContext
    {
        public BlazorDBContext(DbContextOptions<BlazorDBContext> options) : base(options)
        {

        }

     

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>().HasData(new Department() { DepartmentId = 1, DepartmentName = "IT" });
            modelBuilder.Entity<Department>().HasData(new Department() { DepartmentId = 2, DepartmentName = "HR" });
            modelBuilder.Entity<Department>().HasData(new Department() { DepartmentId = 3, DepartmentName = "Admin" });
            modelBuilder.Entity<Department>().HasData(new Department() { DepartmentId = 4, DepartmentName = "Payroll" });

            //Send employee tables
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId = 1,
                FirstName = "Arun",
                LastName = "Singh",
                DateOfBirth = new DateTime(1990, 12, 12),
                //Department = new Department() { DepartmentId = 1, DepartmentName = "Computer" },
                DepartmentId = 1,
                Email = "arun@gmail.com",
                Gender = Gender.Male,
                PhotoPath = "images/img1.png"

            });
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId = 2,
                FirstName = "Ram",
                LastName = "Pandey",
                DateOfBirth = new DateTime(1990, 12, 12),
                //Department = new Department() { DepartmentId = 1, DepartmentName = "IT" },
                DepartmentId = 3,
                Email = "ram@gmail.com",
                Gender = Gender.Male,
                PhotoPath = "images/img2.png"

            });
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId = 3,
                FirstName = "Pankaj",
                LastName = "Pandey",
                DateOfBirth = new DateTime(1990, 12, 12),
                //Department = new Department() { DepartmentId = 1, DepartmentName = "IT" },
                DepartmentId = 2,
                Email = "pankaj@gmail.com",
                Gender = Gender.Male,
                PhotoPath = "images/img3.png"

            });
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId = 4,
                FirstName = "Nisha",
                LastName = "Singh",
                DateOfBirth = new DateTime(1990, 12, 12),
                //Department = new Department() { DepartmentId = 1, DepartmentName = "IT" },
                DepartmentId = 4,
                Email = "nisha@gmail.com",
                Gender = Gender.Female,
                PhotoPath = "images/img3.png"

            });


            modelBuilder.Entity<UserRefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId);

                entity.ToTable("UserRefreshTokens");

                entity.Property(e => e.TokenId).HasColumnName("TokenId");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("ExpiryDate")
                    .HasColumnType("Datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("Token")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__user___60FC61CA");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");

                entity.Property(e => e.RoleId).HasColumnName("RoleId");

                entity.Property(e => e.RoleDesc)
                    .IsRequired()
                    .HasColumnName("RoleDesc")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('New Position - title not formalized yet')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_user_id_2")
                    .IsClustered(false);

                entity.ToTable("Users");

                entity.Property(e => e.UserId).HasColumnName("UserId");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasMaxLength(100)
                    .IsUnicode(false);



                entity.Property(e => e.CreatedOn)
                    .HasColumnName("CreatedOn")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");


                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("Password")
                    .HasMaxLength(100)
                    .IsUnicode(false);


                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleId")
                    .HasDefaultValueSql("((1))");


                //entity.HasOne(d => d.Role)
                //    .WithMany(p => p.Users)
                //    .HasForeignKey(d => d.RoleId)
                //    .HasConstraintName("FK__User__role_id__6E565CE8");
            });

        }

        
    }
}
