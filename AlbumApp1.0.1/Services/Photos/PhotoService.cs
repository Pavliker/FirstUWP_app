using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Photos
{
    public partial class PhotoService:IPhotoService
    {
        private IUnitOfWork unitOfWork;
        public PhotoService() 
        {
            unitOfWork = App.GetService<IUnitOfWork>();
        }
        public async Task AddPhoto(int КодПользователя, int КодОбъекта, int КодСтиля, DateTime ДатаЗагрузки, string НазваниеФотографии, string Описание, string Формат, string Разрешение, int Уникальность, byte[]Путь)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertPhoto @КодПользователя, @КодОбъекта, @КодСтиля, @ДатаЗагрузки, @НазваниеФотографии, @Описание," +
                       "@Формат, @Разрешение, @Уникальность, @Путь",
                       new SqlParameter("@КодПользователя", КодПользователя),
                       new SqlParameter("@КодОбъекта", КодОбъекта),
                       new SqlParameter("@КодСтиля", КодСтиля),
                       new SqlParameter("@ДатаЗагрузки", ДатаЗагрузки),
                       new SqlParameter("@НазваниеФотографии", НазваниеФотографии),
                       new SqlParameter("@Описание", Описание),
                       new SqlParameter("@Формат", Формат),
                       new SqlParameter("@Разрешение", Разрешение),
                       new SqlParameter("@Уникальность", Уникальность),
                       new SqlParameter("@Путь", Путь));
        }
    }
}
