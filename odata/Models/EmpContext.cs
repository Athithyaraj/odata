using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace odata.Models;

public partial class EmpContext : DbContext
{
    public EmpContext()
    {
    }

    public EmpContext(DbContextOptions<EmpContext> options)
        : base(options)
    {
    }

    public DbSet<Department> Departments { get; set; }

    public DbSet<DeptManager> DeptManagers { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Salary> Salaries { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Title> Titles { get; set; }

    public DbSet<WorksIn> WorksIns { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Database=emp;Username=postgres;Password=welcome1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptNo).HasName("departments_pkey");

            entity.ToTable("departments");

            entity.HasIndex(e => e.DeptName, "departments_dept_name_key").IsUnique();

            entity.Property(e => e.DeptNo)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("dept_no");
            entity.Property(e => e.DeptName)
                .HasMaxLength(40)
                .HasColumnName("dept_name");
        });

        modelBuilder.Entity<DeptManager>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.FromDate, e.DeptNo }).HasName("dept_manager_pkey");

            entity.ToTable("dept_manager");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.FromDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("from_date");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("dept_no");
            entity.Property(e => e.ToDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("to_date");

            entity.HasOne(d => d.DeptNoNavigation).WithMany(p => p.DeptManagers)
                .HasForeignKey(d => d.DeptNo)
                .HasConstraintName("dept_manager_dept_no_fkey");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.DeptManagers)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("dept_manager_emp_no_fkey");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpNo).HasName("employees_pkey");

            entity.ToTable("employees");

            entity.Property(e => e.EmpNo)
                .ValueGeneratedNever()
                .HasColumnName("emp_no");
            entity.Property(e => e.BirthDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("birth_date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(14)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hire_date");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .HasColumnName("last_name");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.FromDate }).HasName("salaries_pkey");

            entity.ToTable("salaries");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.FromDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("from_date");
            entity.Property(e => e.Salary1).HasColumnName("salary");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.Salaries)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("salaries_emp_no_fkey");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.Title1, e.FromDate }).HasName("titles_pkey");

            entity.ToTable("titles");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.Title1)
                .HasMaxLength(50)
                .HasColumnName("title");
            entity.Property(e => e.FromDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("from_date");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.Titles)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("titles_emp_no_fkey");
        });

        modelBuilder.Entity<WorksIn>(entity =>
        {
            entity.HasKey(e => new { e.EmpNo, e.FromDate, e.DeptNo }).HasName("works_in_pkey");

            entity.ToTable("works_in");

            entity.Property(e => e.EmpNo).HasColumnName("emp_no");
            entity.Property(e => e.FromDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("from_date");
            entity.Property(e => e.DeptNo)
                .HasMaxLength(4)
                .IsFixedLength()
                .HasColumnName("dept_no");
            entity.Property(e => e.ToDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("to_date");

            entity.HasOne(d => d.DeptNoNavigation).WithMany(p => p.WorksIns)
                .HasForeignKey(d => d.DeptNo)
                .HasConstraintName("works_in_dept_no_fkey");

            entity.HasOne(d => d.EmpNoNavigation).WithMany(p => p.WorksIns)
                .HasForeignKey(d => d.EmpNo)
                .HasConstraintName("works_in_emp_no_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
