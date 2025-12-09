
using FFHRRequestSystem.Entitites.VietN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace FFHRRequestSystem.Repositories.VietN.DBContext;

public partial class FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext : DbContext
{
    public FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext()
    {
    }

    public FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext(DbContextOptions<FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProcessingTypeVietN> ProcessingTypeVietNs { get; set; }

    public virtual DbSet<SystemUserAccount> SystemUserAccounts { get; set; }

    public virtual DbSet<TicketProcessingVietN> TicketProcessingVietNs { get; set; }
    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=VietCute\\SQLEXPRESS;Initial Catalog=FA25_PRN222_3W_PRN222_01_G4_FacilityFeedbackHelpdeskRequestSystem;Persist Security Info=True;User ID=sa;Password=123456;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProcessingTypeVietN>(entity =>
        {
            entity.HasKey(e => e.ProcessingTypeVietNid).HasName("PK__Processi__D46F9CB5B845733F");

            entity.ToTable("ProcessingTypeVietN");

            entity.HasIndex(e => e.TypeCode, "IX_ProcessingTypeVietN_TypeCode").IsUnique();

            entity.Property(e => e.ProcessingTypeVietNid).HasColumnName("ProcessingTypeVietNId");
            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_ProcessingTypeVietN_CreatedDate");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_ProcessingTypeVietN_IsActive");
            entity.Property(e => e.RequireApproval).HasAnnotation("Relational:DefaultConstraintName", "DF_ProcessingTypeVietN_RequireApproval");
            entity.Property(e => e.TypeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(150);
        });

        modelBuilder.Entity<SystemUserAccount>(entity =>
        {
            entity.HasKey(e => e.UserAccountId);

            entity.ToTable("System.UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.EmployeeCode)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<TicketProcessingVietN>(entity =>
        {
            entity.HasKey(e => e.TicketProcessingVietNid).HasName("PK__TicketPr__361F3891085995CE");

            entity.ToTable("TicketProcessingVietN");

            entity.HasIndex(e => e.ProcessingCode, "IX_TicketProcessingVietN_ProcessingCode").IsUnique();

            entity.HasIndex(e => e.TicketReference, "IX_TicketProcessingVietN_TicketReference");

            entity.Property(e => e.TicketProcessingVietNid)
                .ValueGeneratedNever()
                .HasColumnName("TicketProcessingVietNId");
            entity.Property(e => e.ActionDescription).HasMaxLength(1000);
            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_TicketProcessingVietN_CreatedDate");
            entity.Property(e => e.IsAutoProcessed).HasAnnotation("Relational:DefaultConstraintName", "DF_TicketProcessingVietN_IsAutoProcessed");
            entity.Property(e => e.ModifiedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())")
                .HasAnnotation("Relational:DefaultConstraintName", "DF_TicketProcessingVietN_ModifiedDate");
            entity.Property(e => e.OverdueDays).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriorityLevel)
                .HasDefaultValue(2)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_TicketProcessingVietN_Priority");
            entity.Property(e => e.ProcessedBy).HasMaxLength(100);
            entity.Property(e => e.ProcessedDate).HasPrecision(0);
            entity.Property(e => e.ProcessingAction)
                .IsRequired()
                .HasMaxLength(250);
            entity.Property(e => e.ProcessingCode)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.ProcessingTypeVietNid).HasColumnName("ProcessingTypeVietNId");
            entity.Property(e => e.RelatedTicketCode).HasMaxLength(100);
            entity.Property(e => e.ResolvedNote).HasMaxLength(1000);
            entity.Property(e => e.Status)
                .HasDefaultValue(1)
                .HasAnnotation("Relational:DefaultConstraintName", "DF_TicketProcessingVietN_Status");
            entity.Property(e => e.TicketReference)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.ProcessingTypeVietN).WithMany(p => p.TicketProcessingVietNs)
                .HasForeignKey(d => d.ProcessingTypeVietNid)
                .HasConstraintName("FK_TicketProcessingVietN_ProcessingTypeVietN");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}