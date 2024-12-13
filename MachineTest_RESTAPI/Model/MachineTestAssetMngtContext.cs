using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MachineTest_RESTAPI.Model;

public partial class MachineTestAssetMngtContext : DbContext
{
    public MachineTestAssetMngtContext()
    {
    }

    public MachineTestAssetMngtContext(DbContextOptions<MachineTestAssetMngtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssetDefinition> AssetDefinitions { get; set; }

    public virtual DbSet<AssetMaster> AssetMasters { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRegistration> UserRegistrations { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =DESKTOP-8PGNBNF\\SQLEXPRESS; Initial Catalog = MachineTest_AssetMngt; Integrated Security = True; Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetDefinition>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__AssetDef__BBB62059010FD3F5");

            entity.ToTable("AssetDefinition");

            entity.Property(e => e.AdId).HasColumnName("Ad_Id");
            entity.Property(e => e.AdClass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Ad_Class");
            entity.Property(e => e.AdName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Ad_Name");
            entity.Property(e => e.AtId).HasColumnName("At_Id");

            entity.HasOne(d => d.At).WithMany(p => p.AssetDefinitions)
                .HasForeignKey(d => d.AtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetDefi__At_Id__403A8C7D");
        });

        modelBuilder.Entity<AssetMaster>(entity =>
        {
            entity.HasKey(e => e.AmId).HasName("PK__AssetMas__6F9C0DB4151EB308");

            entity.ToTable("AssetMaster");

            entity.HasIndex(e => e.AmSnumber, "UQ__AssetMas__5369C47AE8355D4E").IsUnique();

            entity.Property(e => e.AmId).HasColumnName("Am_Id");
            entity.Property(e => e.AdId).HasColumnName("Ad_Id");
            entity.Property(e => e.AmFrom)
                .HasColumnType("date")
                .HasColumnName("Am_from");
            entity.Property(e => e.AmModel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Am_Model");
            entity.Property(e => e.AmMyyear)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Am_myyear");
            entity.Property(e => e.AmPdate)
                .HasColumnType("date")
                .HasColumnName("Am_Pdate");
            entity.Property(e => e.AmSnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Am_SNumber");
            entity.Property(e => e.AmTo)
                .HasColumnType("date")
                .HasColumnName("Am_to");
            entity.Property(e => e.AmWarranty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("Am_Warranty");
            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.VdId).HasColumnName("Vd_Id");

            entity.HasOne(d => d.Ad).WithMany(p => p.AssetMasters)
                .HasForeignKey(d => d.AdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetMast__Ad_Id__5165187F");

            entity.HasOne(d => d.At).WithMany(p => p.AssetMasters)
                .HasForeignKey(d => d.AtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetMast__At_Id__4F7CD00D");

            entity.HasOne(d => d.Vd).WithMany(p => p.AssetMasters)
                .HasForeignKey(d => d.VdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetMast__Vd_Id__5070F446");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.AtId).HasName("PK__AssetTyp__2CB848DA7B9AD55F");

            entity.ToTable("AssetType");

            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.AtName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PdId).HasName("PK__Purchase__9BB4F80C6D5CA8BF");

            entity.ToTable("PurchaseOrder");

            entity.HasIndex(e => e.PdOrderNo, "UQ__Purchase__D7964DAC2590B215").IsUnique();

            entity.Property(e => e.PdId).HasColumnName("Pd_Id");
            entity.Property(e => e.AdId).HasColumnName("Ad_Id");
            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.PdDate)
                .HasColumnType("date")
                .HasColumnName("Pd_date");
            entity.Property(e => e.PdOrderNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Pd_OrderNo");
            entity.Property(e => e.PdQty).HasColumnName("Pd_Qty");
            entity.Property(e => e.PdStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Pd_status");
            entity.Property(e => e.VdId).HasColumnName("Vd_Id");

            entity.HasOne(d => d.Ad).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.AdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Ad_Id__48CFD27E");

            entity.HasOne(d => d.At).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.AtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__At_Id__49C3F6B7");

            entity.HasOne(d => d.Vd).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Vd_Id__4AB81AF0");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1ABA23F30A");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B616064C7F9E5").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__UserLogi__4DDA2818F946E59C");

            entity.ToTable("UserLogin");

            entity.HasIndex(e => e.Username, "UQ__UserLogi__536C85E478944582").IsUnique();

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLogin_UserId");
        });

        modelBuilder.Entity<UserRegistration>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserRegi__1788CC4C536E2BD4");

            entity.ToTable("UserRegistration");

            entity.HasIndex(e => e.Username, "UQ__UserRegi__536C85E4B76EECFC").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.UserRegistrations)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRegistration_Role");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VdId).HasName("PK__Vendor__A3744E75F06E2CD5");

            entity.ToTable("Vendor");

            entity.Property(e => e.VdId).HasColumnName("Vd_Id");
            entity.Property(e => e.AtId).HasColumnName("At_Id");
            entity.Property(e => e.VdAddr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vd_Addr");
            entity.Property(e => e.VdFrom)
                .HasColumnType("date")
                .HasColumnName("Vd_from");
            entity.Property(e => e.VdName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Vd_Name");
            entity.Property(e => e.VdTo)
                .HasColumnType("date")
                .HasColumnName("Vd_to");
            entity.Property(e => e.VdType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Vd_Type");

            entity.HasOne(d => d.At).WithMany(p => p.Vendors)
                .HasForeignKey(d => d.AtId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vendor__At_Id__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
