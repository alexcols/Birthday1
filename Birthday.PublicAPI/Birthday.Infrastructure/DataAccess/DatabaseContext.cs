using Birthday.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Person> Birthdays { get; set; }
        //public DbSet<Image> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Name);
                builder.Property(p => p.SecondName);
                builder.Property(p => p.Date);
                builder.Property(p => p.DateWithoutYear);

                builder.Property(i => i.PhotoGuid);
                builder.Property(i => i.PhotoName)                   
                    .HasMaxLength(100)
                    .IsUnicode();

                builder.Property(i => i.PhotoType)                    
                    .HasMaxLength(10)
                    .IsUnicode();

                builder.Property(i => i.PhotoContent);


                //builder.Property(p => p.PhotoId);
                //builder.HasOne(p => p.Photo)
                //   .WithOne();


            });
            //modelBuilder.Entity<Image>(builder => {
            //    builder.HasKey(i => i.Id);
            //    builder.Property(i => i.FileGuid).IsRequired();
            //    builder.Property(i => i.FileName)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode();

            //    builder.Property(i=>i.FileType)
            //        .IsRequired()
            //        .HasMaxLength(10)
            //        .IsUnicode();

            //    builder.Property(i => i.Content).IsRequired();

            //    //builder.HasOne(i => i.Person)
            //    //    .WithOne()
            //    //   ;

            //});

            base.OnModelCreating(modelBuilder);

        }
    }
}
