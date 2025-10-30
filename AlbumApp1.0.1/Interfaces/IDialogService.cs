using AlbumApp1._0._1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Interfaces
{
    public interface IDialogService
    {
        Task<bool> Show(DialogViewModel viewModel);
    }
}
