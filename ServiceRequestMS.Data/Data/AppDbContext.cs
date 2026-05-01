using Microsoft.AspNetCore.Http; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServiceRequestMS.core.Models;
using ServiceRequestMS.Data.Data;
using ServiceRequestMS.Core.Models;
using System.Security.Claims;
using ServiceRequestMS.core.Models.Enums;
namespace ServiceRequestMS.data.Data;
public class AppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string _connectionString;
    public AppDbContext(DbContextOptions<AppDbContext> dbOptions, IHttpContextAccessor httpContextAccessor, IOptions<DatabaseOptions> dbSettings) : base(dbOptions)
    {
        _httpContextAccessor = httpContextAccessor;
        _connectionString = dbSettings.Value.DefaultConnection;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.Claims
          .FirstOrDefault(c => (c.Type == ClaimTypes.NameIdentifier || c.Type.EndsWith("nameidentifier"))
                               && Guid.TryParse(c.Value, out _))?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
        }
        Guid? currentUserId = null;

        if (Guid.TryParse(userIdClaim, out Guid parsedGuid))
        {
            currentUserId = parsedGuid;
        }

        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            var entity = (BaseEntity)entityEntry.Entity;

            if (entityEntry.State == EntityState.Added)
            {
                entity.CreatedDate = DateTime.Now;
                entity.CreatedBy = currentUserId ?? Guid.Empty;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entity.LastUpdatedDate = DateTime.Now;
                entity.LastUpdatedBy = currentUserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }

    public DbSet<Attachment> Attachments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Request>(entity =>
        {
           
            entity.HasOne(r => r.Requester)
                  .WithMany(u => u.CreatedRequests)
                  .HasForeignKey(r => r.CreatedBy)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.AssignedStaff)
                  .WithMany(u => u.AssignedRequests)
                  .HasForeignKey(r => r.AssignedStaffId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Ignore("UserId");
        });
        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<Request>().Property(r => r.Status).HasConversion<string>();

        var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var managerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var empId = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var staffId = Guid.Parse("44444444-4444-4444-4444-444444444444");

        var catHardwareId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var catSoftwareId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

        var itemMouseId = Guid.Parse("d1111111-1111-1111-1111-111111111111");
        var itemMonitorId = Guid.Parse("d2222222-2222-2222-2222-222222222222");
        var itemWindowsId = Guid.Parse("d3333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<User>().HasData(
           new User { Id = adminId, FullName = "System Admin", UserName = "admin", PasswordHash = "123", Role = UserRoles.Admin, CreatedDate = DateTime.Now },
           new User { Id = managerId, FullName = "Project Manager", UserName = "manager", PasswordHash = "123", Role = UserRoles.Manager, CreatedDate = DateTime.Now },
           new User { Id = empId, FullName = "Ahmad Employee", UserName = "ahmad", PasswordHash = "123", Role = UserRoles.Employee, CreatedDate = DateTime.Now },
           new User { Id = staffId, FullName = "Tech Staff", UserName = "staff", PasswordHash = "123", Role = UserRoles.Staff, CreatedDate = DateTime.Now }
        );

        modelBuilder.Entity<Category>().HasData(
           new Category { Id = catHardwareId, Name = "Hardware", Description = "Physical devices and equipment", CreatedDate = DateTime.Now },
           new Category { Id = catSoftwareId, Name = "Software", Description = "Applications and licenses", CreatedDate = DateTime.Now }
        );

        modelBuilder.Entity<Item>().HasData(
           new Item { Id = itemMouseId, Name = "Mouse", Description = "Pointing devices", CategoryId = catHardwareId, CreatedDate = DateTime.Now },
           new Item { Id = itemMonitorId, Name = "Monitor", Description = "Display screens", CategoryId = catHardwareId, CreatedDate = DateTime.Now },
           new Item { Id = itemWindowsId, Name = "Windows License", Description = "OS Activation", CategoryId = catSoftwareId, CreatedDate = DateTime.Now }
        );

        modelBuilder.Entity<Request>().HasData(
           new Request
           {
               Id = Guid.NewGuid(),
               Title = "Broken Mouse",
               Description = "Left click is not responding",
               Status = RequestStatus.New,
               CategoryItemId = itemMouseId,
               CreatedBy = empId,
               CreatedDate = DateTime.Now  
           },
           new Request
           {
               Id = Guid.NewGuid(),
               Title = "Screen Flickering",
               Description = "Monitor screen keeps turning off",
               Status = RequestStatus.Assigned,
               CategoryItemId = itemMonitorId,
               CreatedBy = empId,
               AssignedStaffId = staffId,
               CreatedDate = DateTime.Now.AddHours(-2)
           },
           new Request
           {
               Id = Guid.NewGuid(),
               Title = "Windows Activation",
               Description = "Need to activate Windows 11",
               Status = RequestStatus.InProgress,
               CategoryItemId = itemWindowsId,
               CreatedBy = empId,
               AssignedStaffId = staffId,
               CreatedDate = DateTime.Now.AddDays(-1)
           }
        );
    }
}


