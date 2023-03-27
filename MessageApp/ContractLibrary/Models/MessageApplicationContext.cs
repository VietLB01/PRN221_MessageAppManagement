using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContractLibrary.Models;

public partial class MessageApplicationContext : DbContext
{
    public MessageApplicationContext()
    {
    }

    public MessageApplicationContext(DbContextOptions<MessageApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageGroup> MessageGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KME6QOJ;database=MessageApplication;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Avatar).HasMaxLength(100);
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasMany(d => d.Groups).WithMany(p => p.Accounts)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupAccount",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Group_Account_Groups"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Group_Account_Account"),
                    j =>
                    {
                        j.HasKey("AccountId", "GroupId");
                        j.ToTable("Group_Account");
                        j.IndexerProperty<int>("AccountId").HasColumnName("AccountID");
                        j.IndexerProperty<int>("GroupId").HasColumnName("GroupID");
                    });
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.GroupName).HasMaxLength(50);
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsFixedLength();
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Message_1");

            entity.ToTable("Message");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AccountIdAcceptNavigation).WithMany(p => p.MessageAccountIdAcceptNavigations)
                .HasForeignKey(d => d.AccountIdAccept)
                .HasConstraintName("FK_Message_Account1");

            entity.HasOne(d => d.AccountIdSendNavigation).WithMany(p => p.MessageAccountIdSendNavigations)
                .HasForeignKey(d => d.AccountIdSend)
                .HasConstraintName("FK_Message_Account");
        });

        modelBuilder.Entity<MessageGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Message_Group_1");

            entity.ToTable("Message_Group");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AccountIdsend).HasColumnName("AccountIDSend");
            entity.Property(e => e.Content).HasColumnType("ntext");
            entity.Property(e => e.GroupIdaccept).HasColumnName("GroupIDAccept");
            entity.Property(e => e.Time)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AccountIdsendNavigation).WithMany(p => p.MessageGroups)
                .HasForeignKey(d => d.AccountIdsend)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Group_Account");

            entity.HasOne(d => d.GroupIdacceptNavigation).WithMany(p => p.MessageGroups)
                .HasForeignKey(d => d.GroupIdaccept)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Message_Group_Groups");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
