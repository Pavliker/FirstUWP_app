using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Dispatching;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlbumApp1._0._1.Services.Photos
{
    public partial class PhotoService:ObservableObject, IPhotoService
    {
        public IGenericRepository<Фотографии> phRep { get; private set; }
        
        private readonly IDispatcherQueueService dispatcher;
        private SynchronizedObservableCollection<Фотографии> photographyCollection;
        public SynchronizedObservableCollection<Фотографии> PhotographyCollection
        {
            get => photographyCollection;
            set
            {
                if (photographyCollection!=value)
                {
                    photographyCollection = value;
                }
            }
        }
        private IUnitOfWork unitOfWork;
        public PhotoService(IGenericRepository<Фотографии> phRep) 
        {
            this.phRep = phRep;

            unitOfWork = App.GetService<IUnitOfWork>();
            photographyCollection = new SynchronizedObservableCollection<Фотографии>();
            dispatcher = App.GetService<IDispatcherQueueService>();

        }
        public async Task AddPhoto(int КодПользователя, int КодОбъекта, int КодСтиля, DateTime ДатаЗагрузки, string НазваниеФотографии, string Описание, string Качество, string Формат, string Разрешение, int Уникальность, long Размер, byte[]Путь)
        {
            await unitOfWork.context.Database.ExecuteSqlRawAsync(
                       "EXEC InsertPhoto @КодПользователя, @КодОбъекта, @КодСтиля, @ДатаЗагрузки, @НазваниеФотографии, @Описание," +
                       "@Качество, @Формат, @Разрешение, @Уникальность, @Размер, @Путь",
                       new SqlParameter("@КодПользователя", КодПользователя),
                       new SqlParameter("@КодОбъекта", КодОбъекта),
                       new SqlParameter("@КодСтиля", КодСтиля),
                       new SqlParameter("@ДатаЗагрузки", ДатаЗагрузки),
                       new SqlParameter("@НазваниеФотографии", НазваниеФотографии),
                       new SqlParameter("@Описание", Описание),
                       new SqlParameter("@Качество", Качество),
                       new SqlParameter("@Формат", Формат),
                       new SqlParameter("@Разрешение", Разрешение),
                       new SqlParameter("@Уникальность", Уникальность),
                       new SqlParameter("@Размер", Размер),
                       new SqlParameter("@Путь", Путь));
        }
         async Task  GetAllPhotos()
        {
           
            var tasks = new List<Task>();
            tasks.Add(Task.Run( () =>
            {
                bool isQueued =dispatcher.GetDispatcherQueue().TryEnqueue(async () =>
                    {
                        int sum = 0;
                        var lst = phRep.GetAll();
                        var stream1Enumerator = lst.GetAsyncEnumerator();

                        var currentGroupId = -1;
                        //await foreach (var i in lst)
                        //{
                            while (await stream1Enumerator.MoveNextAsync())
                            {
                            sum++;
                            photographyCollection.Add(stream1Enumerator.Current);

                            if (photographyCollection.Count != sum)
                                {
                                    //currentGroupId = stream2Enumerator.Current.КодФотографии;

                                    stream1Enumerator.Current.Image = new Microsoft.UI.Xaml.Media.Imaging.BitmapImage();

                                    using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                                    {
                                        await stream.WriteAsync(stream1Enumerator.Current.Путь.AsBuffer());
                                        stream.Seek(0);
                                        await stream1Enumerator.Current.Image.SetSourceAsync(stream);
                                    }
                                    OnPropertyChanged(nameof(PhotographyCollection));
                                    OnPropertyChanged(nameof(stream1Enumerator.Current.Image));
                                }
                            }

                            //j.КодФотографии = i.КодФотографии;
                            //j.КодСтроки = i.КодСтроки;
                            //j.КодПользователя = i.КодПользователя;
                            //j.КодОбъекта = i.КодОбъекта;
                            //j.ДатаЗагрузки = i.ДатаЗагрузки;
                            //j.НазваниеФотографии = i.НазваниеФотографии;
                            //j.Описание = i.Описание;
                            //j.Качество = i.Качество;
                            //j.Формат = i.Формат;
                            //j.Разрешение = i.Разрешение;
                            //j.Уникальность = i.Уникальность;
                            //j.Размер = i.Размер;
                            //j.Путь = i.Путь;
                          
                                                   

                        //}


                    });
            }));
            await Task.WhenAll(tasks.ToArray());
        }
        public async void GetAll()
        {
            await GetAllPhotos();
        }
        public async Task<int> GetIdByPhotoName(string photoname)
        {
            var PhotoID = await unitOfWork.context.Фотографии.Where(o => o.НазваниеФотографии == photoname).Select(o => o.КодФотографии).FirstOrDefaultAsync();
            return PhotoID;
        }
    }
}
