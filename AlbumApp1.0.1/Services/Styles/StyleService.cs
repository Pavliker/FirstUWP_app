using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services.Styles
{
    public class StyleService:IStyleService
    {
        public IGenericRepository<Стили> StyleRepository { get; private set; }
        private readonly IContentDialogExit contentDialogExit;
        public StyleService(IGenericRepository<Стили> StyleRepository)
        {
          contentDialogExit = App.GetService<IContentDialogExit>();
            this.StyleRepository = StyleRepository;
        }

        public async Task<ObservableCollection<Стили>> GetStyles()
        {
            var EmptyCollection = new ObservableCollection<Стили>();
            var lst =  StyleRepository.GetAll();
            await foreach (var obj in lst)
            {
                EmptyCollection.Add(obj);
            }
         
                return EmptyCollection;
            
        
        }
     
    }
}
