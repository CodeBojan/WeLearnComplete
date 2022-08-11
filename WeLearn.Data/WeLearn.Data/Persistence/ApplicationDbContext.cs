﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Roles;

namespace WeLearn.Data.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ExternalSystem> ExternalSystems { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<StudyYear> StudyYears { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Notice> Notices { get; set; }
    public DbSet<Role> ApiRoles { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<StudyYearAdminRole> StudyYearAdminRoles { get; set; }
    public DbSet<CourseAdminRole> CourseAdminRoles { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<BaseEntity>();
        builder.Ignore<ContentBase>();

        // TODO indices

        builder.Entity<Account>(a =>
        {
            a.HasKey(n => n.Id);

            a.HasOne(a => a.User)
            .WithOne(u => u.Account)
            .HasForeignKey<Account>(a => a.Id)
            .HasPrincipalKey<ApplicationUser>(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Role>(r =>
        {
            r.HasKey(r => r.Id);

            r.HasMany(r => r.AccountRoles)
            .WithOne(ar => ar.Role)
            .HasForeignKey(ar => ar.RoleId)
            .HasPrincipalKey(r => r.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<AccountRole>(ar =>
        {
            ar.HasKey(ar => ar.Id);

            ar.HasDiscriminator(ar => ar.Type)
            .HasValue<StudyYearAdminRole>(RoleType.StudyYearAdmin.ToString())
            .HasValue<CourseAdminRole>(RoleType.CourseAdmin.ToString());

            ar.HasOne(r => r.Account)
            .WithMany(a => a.Roles)
            .HasForeignKey(r => r.AccountId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StudyYearAdminRole>(syar =>
        {
            syar.HasOne(syar => syar.StudyYear)
            .WithMany(sy => sy.AdminRoles)
            .HasForeignKey(syar => syar.StudyYearId)
            .HasPrincipalKey(sy => sy.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CourseAdminRole>(car =>
        {
            car.HasOne(car => car.Course)
            .WithMany(c => c.AdminRoles)
            .HasForeignKey(car => car.CourseId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ExternalSystem>(es =>
        {
            es.HasKey(es => es.Id);

            es.HasMany(es => es.Credentials)
            .WithOne(c => c.ExternalSystem)
            .HasForeignKey(c => c.ExternalSystemId)
            .HasPrincipalKey(es => es.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Credentials>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasOne(c => c.Course)
            .WithMany(c => c.Credentials)
            .HasForeignKey(c => c.CourseId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

            c.HasOne(c => c.Creator)
            .WithMany(a => a.Credentials)
            .HasForeignKey(c => c.AccountId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Notice>(n =>
        {
            n.HasKey(n => n.Id);


        });
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseEntity && (
                  e.State == EntityState.Added
                  || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
