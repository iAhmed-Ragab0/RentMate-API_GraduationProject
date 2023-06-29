using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Propertyy> Properties { get; set; }
        public virtual DbSet<PropertyDetails> PropertyDetails { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<WishingList> WishingList { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Governorate> Governorates { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Identity Tables Name
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            //STOP the cycling error
            modelBuilder.Entity<User>()
                        .HasMany(b => b.RentedProperty)
                        .WithOne(b => b.Tenant)
                        .HasForeignKey(b => b.TenantId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Propertyy>()
                        .HasOne(b => b.Owner)
                        .WithMany(b => b.OwenedPoperty)
                        .HasForeignKey(b => b.OwnerId)
                        .OnDelete(DeleteBehavior.Restrict);

            //Appoitments Relationship
            // app with Owner
            modelBuilder.Entity<Appointment>()
                        .HasOne<User>(b => b.Owner)
                        .WithMany(b => b.appointmentOfOwner)
                        .HasForeignKey(b => b.OwnerId)
                        .OnDelete(DeleteBehavior.Restrict);

            // app with Tenant
            modelBuilder.Entity<Appointment>()
                        .HasOne<User>(b => b.Tenant)
                        .WithMany(b => b.appointmentsOfTenant)
                        .HasForeignKey(b => b.TenantId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Propertyy>()
                        .Property(b => b.TenantId)
                        .IsRequired(false);

            //default values for property table
            modelBuilder.Entity<Propertyy>()
                        .Property(b => b.IsRented)
                        .HasDefaultValue(false);

            modelBuilder.Entity<Propertyy>()
                        .HasOne(p => p.Details)
                        .WithOne(pd => pd.Property)
                        .HasForeignKey<PropertyDetails>(pd => pd.PropertyId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Propertyy>()
                        .Property(b => b.NoOfBedsInTheRoom)
                        .HasDefaultValue(1);

            modelBuilder.Entity<Propertyy>()
                        .Property(b => b.NoOfRooms)
                        .HasDefaultValue(1);

            modelBuilder.Entity<Propertyy>()
                        .Property(b => b.NoOfBathroom)
                        .HasDefaultValue(1);


            // proprty photo & Reviews cycle problem
            modelBuilder.Entity<Propertyy>()
                        .HasMany(p => p.Photos)
                        .WithOne()
                        .HasForeignKey(b => b.PropertyId);

            modelBuilder.Entity<Propertyy>()
                        .HasMany(p => p.Reviews)
                        .WithOne()
                        .HasForeignKey(b => b.PropertyId);


            //User
            modelBuilder.Entity<User>()
                        .Property(b => b.IsDeleted)
                        .HasDefaultValue(false);

            //-----------------------------------------------------------------
            ////address

            modelBuilder.Entity<Governorate>()
                        .HasMany(a => a.Cities)
                        .WithOne(c => c.Governorate)
                        .HasForeignKey(a => a.governorate_id)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<City>()
                        .HasMany(c => c.Properties)
                        .WithOne(a => a.City)
                        .HasForeignKey(a => a.CityId)
                        .OnDelete(DeleteBehavior.NoAction);



            // Governrate ID 
            modelBuilder.Entity<Governorate>()
                        .Property(g => g.Id)
                        .ValueGeneratedNever();


            //-------------------------------------

            modelBuilder.Entity<PropertyDetails>()
                        .Property(pd => pd.PropertyId)
                        .IsRequired(false);





            //------------------------------------


            //------------------------------------
            //arabic support
            //modelBuilder.Entity<Governorate>()
            //            .Property(b => b.governorate_name_ar)
            //            .HasColumnType("varchar(max)")
            //            .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8")
            //            .IsUnicode();

            //modelBuilder.Entity<City>()
            //            .Property(b => b.city_name_ar)
            //            .HasColumnType("varchar(max)")
            //            .UseCollation("LATIN1_GENERAL_100_CI_AS_SC_UTF8")
            //            .IsUnicode();


        }
    }
}
