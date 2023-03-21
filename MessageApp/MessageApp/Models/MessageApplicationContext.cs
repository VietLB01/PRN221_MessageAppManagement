using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MessageApp.Models;

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

    public virtual DbSet<Message> Messages { get; set; }

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
