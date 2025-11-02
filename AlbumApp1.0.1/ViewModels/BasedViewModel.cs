using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.UI.Xaml;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AlbumApp1._0._1.ViewModels
{
    public abstract class BasedViewModelContext :ObservableRecipient, IDisposable
    {
        private bool _disposed; 
        public static readonly DependencyProperty TypeProperty =
           DependencyProperty.RegisterAttached(
             "BasedViewModelContext",
             typeof(string), //we need string here 
             typeof(BasedViewModelContext),
             new PropertyMetadata(null)
           );
        public static void SetType(DependencyObject element, string value)
        {
            element.SetValue(TypeProperty, value);
        }
        public static string GetType(DependencyObject element)
        {
            return (string)element.GetValue(TypeProperty);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;
                if (disposing)
                {

                }
            }
        }
        


    }
}
    
