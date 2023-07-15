using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ZooAplicationApi.Data.Models;

namespace ZooAplicationApi.Data;

public partial class ZoologicoApiwebDataContext : DbContext
{
    public ZoologicoApiwebDataContext()
    {
    }

    public ZoologicoApiwebDataContext(DbContextOptions<ZoologicoApiwebDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registro> Registros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ZoologicoApiweb.Data");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registro>(entity =>
        {
            entity.ToTable("Registro");

            entity.Property(e => e.Discriminator).HasDefaultValueSql("(N'')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
