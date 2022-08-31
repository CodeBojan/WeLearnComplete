using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeLearn.Data.Models;
using WeLearn.Data.Models.Content;
using WeLearn.Data.Models.Content.Notices;
using WeLearn.Data.Models.Identity;
using WeLearn.Data.Models.Notifications;
using WeLearn.Data.Models.Roles;

namespace WeLearn.Data.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public DbSet<DbPersistedGrant> Grants { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ExternalSystem> ExternalSystems { get; set; }
    public DbSet<Credentials> Credentials { get; set; }
    public DbSet<StudyYear> StudyYears { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Role> ApiRoles { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<StudyYearAdminRole> StudyYearAdminRoles { get; set; }
    public DbSet<CourseAdminRole> CourseAdminRoles { get; set; }
    public DbSet<FollowedCourse> FollowedCourses { get; set; }
    public DbSet<FollowedStudyYear> FollowedStudyYears { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<StudyMaterial> StudyMaterials { get; set; }
    public DbSet<CourseMaterialUploadRequest> CourseMaterialUploadRequests { get; set; }
    public DbSet<Notice> Notices { get; set; }
    public DbSet<GeneralNotice> GeneralNotices { get; set; }
    public DbSet<StudyYearNotice> StudyYearNotices { get; set; }
    public DbSet<CourseNotice> CourseNotices { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected ApplicationDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<DatedEntity>();

        // TODO indices

        builder.Entity<DbPersistedGrant>(dpg =>
        {
            dpg.HasKey(dpg => dpg.Key);

            dpg.Property(dpg => dpg.SubjectId)
            .HasMaxLength(36);

            //pg.Property(pg => pg.Key)
            //   .HasMaxLength(44);

            //pg.Property(pg => pg.SessionId)
            //.HasMaxLength(32);

            dpg.HasIndex(dpg => dpg.SessionId);
            dpg.HasIndex(dpg => dpg.SubjectId);
            dpg.HasIndex(dpg => dpg.ClientId);
        });

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

            c.HasOne(c => c.Creator)
            .WithMany(a => a.Credentials)
            .HasForeignKey(c => c.CreatorId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CourseCredentials>(cc =>
        {
            cc.HasKey(cc => new { cc.CourseId, cc.CredentialsId });

            cc.HasOne(cc => cc.Course)
            .WithMany(c => c.CourseCredentials)
            .HasForeignKey(cc => cc.CourseId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

            cc.HasOne(cc => cc.Credentials)
            .WithMany(c => c.CourseCredentials)
            .HasForeignKey(cc => cc.CredentialsId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FollowedCourse>(fc =>
        {
            fc.HasKey(fc => new { fc.AccountId, fc.CourseId });

            fc.HasOne(fc => fc.Account)
            .WithMany(a => a.FollowedCourses)
            .HasForeignKey(fc => fc.AccountId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

            fc.HasOne(fc => fc.Course)
            .WithMany(c => c.FollowingUsers)
            .HasForeignKey(fc => fc.CourseId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<FollowedStudyYear>(fsy =>
        {
            fsy.HasKey(fsy => new { fsy.AccountId, fsy.StudyYearId });

            fsy.HasOne(fsy => fsy.Account)
            .WithMany(a => a.FollowedStudyYears)
            .HasForeignKey(fsy => fsy.AccountId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.Cascade);

            fsy.HasOne(fsy => fsy.StudyYear)
            .WithMany(sy => sy.FollowedStudyYears)
            .HasForeignKey(fsy => fsy.StudyYearId)
            .HasPrincipalKey(sy => sy.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Comment>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasOne(c => c.Author)
            .WithMany(a => a.Comments)
            .HasForeignKey(c => c.AuthorId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

            c.HasOne(c => c.Content)
            .WithMany(c => c.Comments)
            .HasForeignKey(c => c.ContentId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StudyYear>(sy =>
        {
            sy.HasKey(sy => sy.Id);

            sy.Ignore(sy => sy.FollowingCount);
            sy.Ignore(sy => sy.IsFollowing);

            sy.HasIndex(sy => sy.ShortName)
            .IsUnique();
            sy.HasIndex(sy => sy.FullName)
            .IsUnique();
        });

        builder.Entity<Course>(c =>
        {
            c.HasKey(c => c.Id);

            c.HasIndex(c => c.Code)
            .IsUnique();

            c.Ignore(c => c.IsFollowing)
            .Ignore(c => c.FollowingCount);

            c.Ignore(c => c.Notices)
            .Ignore(c => c.Posts)
            .Ignore(c => c.Documents)
            .Ignore(c => c.StudyMaterials);
        });

        builder.Entity<Content>(c =>
        {
            c.HasKey(c => c.Id);

            c.Ignore(c => c.CommentCount);

            c.HasDiscriminator(c => c.Type)
            .HasValue<Post>(ContentType.Post.Value())
            .HasValue<Notice>(ContentType.Notice.Value())
            .HasValue<Document>(ContentType.Document.Value())
            .HasValue<StudyMaterial>(ContentType.StudyMaterial.Value())
            .HasValue<GeneralNotice>(ContentType.NoticeGeneral.Value())
            .HasValue<StudyYearNotice>(ContentType.NoticeStudyYear.Value())
            .HasValue<CourseNotice>(ContentType.NoticeCourse.Value());

            c.HasOne(c => c.Course)
            .WithMany(c => c.Contents)
            .HasForeignKey(c => c.CourseId)
            .HasPrincipalKey(c => c.Id)
            .IsRequired(false) // currently only because of notices that don't relate to a course
            .OnDelete(DeleteBehavior.Cascade);

            c.HasOne(c => c.Creator)
            .WithMany(a => a.CreatedContent)
            .HasForeignKey(c => c.CreatorId)
            .HasPrincipalKey(a => a.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

            c.HasOne(c => c.ExternalSystem)
            .WithMany(es => es.Contents)
            .HasForeignKey(c => c.ExternalSystemId)
            .HasPrincipalKey(es => es.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<DocumentContainer>(dc =>
        {
            dc.HasMany(dc => dc.Documents)
            .WithOne(d => d.DocumentContainer)
            .HasForeignKey(dc => dc.DocumentContainerId)
            .HasPrincipalKey(d => d.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StudyYearNotice>(syn =>
        {
            syn.HasOne(syn => syn.StudyYear)
            .WithMany(sy => sy.StudyYearNotices)
            .HasForeignKey(syn => syn.StudyYearId)
            .HasPrincipalKey(sy => sy.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<StudyMaterial>(sm =>
        {
        });

        builder.Entity<Document>(d =>
        {
            d.HasOne(d => d.CourseMaterialUploadRequest)
            .WithMany(cmur => cmur.Documents)
            .HasForeignKey(d => d.CourseMaterialUploadRequestId)
            .HasPrincipalKey(cmur => cmur.Id)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CourseMaterialUploadRequest>(cmur =>
        {
            cmur.HasKey(cmur => cmur.Id);

            cmur.HasOne(cmur => cmur.Course)
            .WithMany(c => c.CourseMaterialUploadRequests)
            .HasForeignKey(cmur => cmur.CourseId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

            cmur.HasOne(cmur => cmur.Creator)
            .WithMany(a => a.CourseMaterialUploadRequests)
            .HasForeignKey(cmur => cmur.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Notification>(n =>
        {
            n.HasKey(n => n.Id);

            n.HasDiscriminator(n => n.Type)
            .HasValue<Notification>(NotificationType.General.Value())
            .HasValue<CommentNotification>(NotificationType.Comment.Value())
            .HasValue<ContentNotification>(NotificationType.Content.Value());

            n.HasOne(n => n.Receiver)
            .WithMany(r => r.Notifications)
            .HasForeignKey(n => n.ReceiverId)
            .HasPrincipalKey(r => r.Id)
            .OnDelete(DeleteBehavior.Cascade);

        });

        builder.Entity<CommentNotification>(cn =>
        {
            cn.HasOne(cn => cn.Comment)
            .WithMany(c => c.CommentNotifications)
            .HasForeignKey(cn => cn.CommentId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ContentNotification>(cn =>
        {
            cn.HasOne(cn => cn.Content)
            .WithMany(c => c.ContentNotifications)
            .HasForeignKey(cn => cn.ContentId)
            .HasPrincipalKey(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);
        });
    }

    public override int SaveChanges()
    {
        AdjustEntities();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        AdjustEntities();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void AdjustEntities()
    {
        var entries = ChangeTracker
                      .Entries()
                      .Where(e => e.Entity is DatedEntity && (
                          e.State == EntityState.Added
                          || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((DatedEntity)entityEntry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((DatedEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }
    }
}
