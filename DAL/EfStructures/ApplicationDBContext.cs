using System;
using System.Collections.Generic;
using DAL;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using DAL.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.EfStructures;

public partial class ApplicationDBContext : DbContext
{


    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
        base.SavingChanges +=(sender, args) =>
        {
            Console.WriteLine($"Saving changes for {((ApplicationDBContext)sender)!.Database!.GetConnectionString()}");
        };
        base.SavedChanges += (sender, args) =>
        {
            Console.WriteLine($"Saved {args!.EntitiesSavedCount} changes for {((ApplicationDBContext)sender)!.Database!.GetConnectionString()}");
        };
        base.SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occured! {args.Exception.Message} entities");
        };

        ChangeTracker.Tracked += ChangeTracker_Tracked;
        ChangeTracker.StateChanged += ChangeTracker_StateChanged;
    }

    private void ChangeTracker_StateChanged(object? sender, EntityStateChangedEventArgs e)
    {
        if (e.Entry.Entity is not User u)
        {
            return;
        }
        var action = string.Empty;
        Console.WriteLine($"User {u.Surname} {u.Name} was {e.OldState} before the state chaned to {e.NewState}");
        switch (e.NewState)
        {
            case EntityState.Unchanged:
                action = e.OldState switch
                {
                    EntityState.Added => "Added",
                    EntityState.Modified => "Edited",
                    _ => action
                };
                Console.WriteLine($"The object was {action}");
                break;
        }
    }

    private void ChangeTracker_Tracked(object? sender, EntityTrackedEventArgs e)
    {
        var source = (e.FromQuery) ? "Database" : "Code";
        if(e.Entry.Entity is User u)
        {
            Console.WriteLine($"User entry {u.Surname} {u.Name} was added from {source}");
        }
    }

    public DbSet<Registration>? Registrations { get; set; }

    public DbSet<Request>? Requests { get; set; }

    public DbSet<Room>? Rooms { get; set; }

    public DbSet<User>? Users { get; set; }
    public DbSet<General>? General { get; set; }
    public DbSet<TimeSlot>? TimeSlots { get; set; }

    public DbSet<SeriLogEntry>? LogEntries {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=DESKTOP-MSIISMQ\\SQLEXPRESS;Database=VKR_1;integrated security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasOne(d => d.Administrator).WithMany(p => p.Registrations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_registrations_users");

            entity.HasOne(d => d.Request).WithMany(p => p.Registrations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_registrations_requests");

            entity.HasOne(d => d.Room).WithMany(p => p.Registrations)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_registrations_rooms");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_requests_users");

            entity.HasOne(d => d.TimeSlot).WithOne(p => p.Request)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_requests_time_slots");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Admin).IsFixedLength();
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasOne(d => d.Administrator).WithMany(p => p.TimeSlots)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_time_slots_users");

            entity.Property(e => e.Free).IsFixedLength();

        });

        modelBuilder.Entity<SeriLogEntry>(entity =>
        {
            entity.Property(e => e.Properties).HasColumnType("Xml");
            entity.Property(e => e.TimeStamp).HasDefaultValueSql("GetDate()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Ошибка параллелищма.
            // Необходимо зарегистрировать в журнал и обработать.
            throw new CustomConcurrencyException("A concurrency error happened", ex);
        }
        catch (RetryLimitExceededException ex)
        {
            // Превышен лимит на количество повторных попыток DbResiliency
            // Необходимо зарегистрировать в журнал и обработать.
            throw new CustomRetryLimitExceededException("There is a problem with SQL Server.", ex);
        }
        catch(DbUpdateException ex)
        {
            // Ошибка при обновлении базы данных
            // Необходимо зарегистрировать в журнал и обработать.
            throw new CustomDbUpdateException("An error occurred updating the database.", ex);
        }
        catch(Exception ex)
        {
            // Ошибка при обновлении базы данных
            // Необходимо зарегистрировать в журнал и обработать.
            throw new CustomException("An error occured updating the database.", ex);
        }
    }
}
