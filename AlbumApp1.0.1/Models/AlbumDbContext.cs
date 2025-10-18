using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
   public class AlbumDbContext : DbContext
    {
        public AlbumDbContext(DbContextOptions<AlbumDbContext>  options) :base(options)
        {

        }
        //Tables
        public DbSet<Tables.Альбомы> Альбомы { get; set; }
        public DbSet<Tables.Альбомы_Фотографии> Альбомы_Фотографии { get; set; }
        public DbSet<Tables.Вопросы> Вопросы { get; set; }
        public DbSet<Tables.Гости> Гости { get; set; }
        public DbSet<Tables.Места> Места { get; set; }
        public DbSet<Tables.Оборудование> Оборудование { get; set; }
        public DbSet<Tables.Объекты> Объекты { get; set; }
        public DbSet<Tables.Пользователи> Пользователи { get; set; }
        public DbSet<Tables.Роли> Роли { get; set; }
        public DbSet<Tables.Стили> Стили { get; set; }
        public DbSet<Tables.Фотографии> Фотографии { get; set; }
        public DbSet<Tables.Фотографии_Места> Фотографии_Места { get; set; }
        public DbSet<Tables.Фотографии_Оборудование> Фотографии_Оборудование { get; set; }
        public DbSet<Tables.АрхивФотографий> АрхивФотографий {  get; set; }
        //Views
        public DbQuery<Views.Albums_Users> Albums_Users { get; set; }
        public DbQuery<Views.All_Accessories_Photos> All_Accessories_Photos { get; set; }
        public DbQuery<Views.All_Albums_Photos> All_Albums_Photos { get; set; }
        public DbQuery<Views.All_Places_Photos> All_Places_Photos { get; set; }
        public DbQuery<Views.Photos_Detailed> Photos_Detailed { get; set; }
        public DbQuery<Views.Questions> Questions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
        }

    }
}
