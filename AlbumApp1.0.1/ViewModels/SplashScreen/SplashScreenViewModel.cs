using AlbumApp1._0._1.Core.Contracts.Services;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Services;
using AlbumApp1._0._1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.ViewModels.SplashScreen
{
    public partial class SplashScreenViewModel : ObservableObject
    {

        

        [ObservableProperty]
        public partial string? Text { get; set; }
        [ObservableProperty]
        public partial int Value { get; set; }
        private readonly IDispatcherQueueService _dispatcher;
        public SplashScreenViewModel(IDispatcherQueueService dispatcher)
        {
            _dispatcher = dispatcher;
          
        }
       
        bool IsUIThread()
        {
            return null != _dispatcher;
        }

        public async Task StartLoadingAsync()
        {
          
                if (IsUIThread()==true){
                for (int i = 0; i <= 100; i ++)
                {
                     await Task.Delay(100);
                    _dispatcher.TryEnqueue(Microsoft.UI.Dispatching.DispatcherQueuePriority.Normal, () =>
                    {
                        Text = $"Загрузка {i}...";
                        Value = i;
                    });
                }
            }
            else
            {
                return;
            }
                Text = "Закончена";
        
        }
       
        //TODO: Do some actual work
    }
    }

