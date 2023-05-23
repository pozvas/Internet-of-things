using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public partial class KripnoteContext : DbContext
{
    public KripnoteContext()
    {
    }

    public KripnoteContext(DbContextOptions<KripnoteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Datum> Data { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=kripnote;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Datum>(entity =>
        {
            entity.HasKey(e => e.IdD).HasName("data_pkey");

            entity.ToTable("data");

            entity.Property(e => e.IdD)
                .ValueGeneratedNever()
                .HasColumnName("id_d");
            entity.Property(e => e.MsgD).HasColumnName("msg_d");
            entity.Property(e => e.TempD).HasColumnName("temp_d");
            entity.Property(e => e.TimeD)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time_d");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
