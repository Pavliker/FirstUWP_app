using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
  
    public partial class InputValidator : ObservableValidator
    {
        [NotMapped]
        public bool IsGuest { get; set; }

        public ObservableCollection<ValidateInputModel> _validate { get; }
       = new ObservableCollection<ValidateInputModel>();
        //private ValidateInputModel _changemodel = new();
        //public ValidateInputModel ChangeModel
        //{
        //    get => _changemodel;
        //    set
        //    {
        //        _changemodel = value;
        //        OnPropertyChanged(nameof(ChangeModel));
        //    }

        //}
        //private string _errorMessage;
        //public string ErrorMessage
        //{
        //    get
        //    {
        //        return _errorMessage;
        //    }
        //    set
        //    {
        //        if (_errorMessage != value)
        //        {
        //            _errorMessage = value;
        //            OnPropertyChanged(nameof(ErrorMessage));
        //        }
        //    }
        //}
        public void ValidateClear(string propertyName)
        {
            var lst = _validate
                .Where(o => o.MemberElement == propertyName)
                .ToList();

            if (_validate.Count() > 0)
            {
                foreach (var i in lst)
                {
                    if (_validate.Any(x => x.MemberElement == i.MemberElement
                                         && x.ErrorMessage_ == i.ErrorMessage_))
                    {
                        _validate.Remove(i);
                    }
                }
            }



            if (!HasErrors)
            {
              
                // remove all error entries for this property
                var itemsToRemove = _validate
                    .Where(x => x.MemberElement == propertyName)
                    .ToList();

                foreach (var item in itemsToRemove)
                    _validate.Remove(item);

                //_errorMessage = string.Empty;
            }
        }
        public void Validate(string value, string propertyName) 
        {
            OnPropertyChanged(propertyName);
            ValidateClear(propertyName);
            
            
            ValidateAllProperties();
            OnPropertyChanged(nameof(HasErrors));


            if (HasErrors==true)
            {
                foreach (var error in GetErrors(propertyName).OfType<ValidationResult>())
                {

                    _validate.Add(
                    new(error.ErrorMessage, string.Join(", ", error.MemberNames))
                    );

                }
                if (_validate.Any(o => o.MemberElement == "ХешированныйПароль"))
                {
                  IsGuest = false; 
                }
                else
                {
                    IsGuest = true;
                }
                    int lastIndex = _validate.Count - 1;

                for (int i = _validate.Count - 1; i >= 0; i--)
                {
                    if (i != lastIndex) // keep only last item
                        _validate.RemoveAt(i);
                }
                //_errorMessage = string.Join(Environment.NewLine,
                //    validateInputModels
                //        .Where(x => x.MemberElement == propertyName)
                //        .Select(x => x.ErrorMessage_)
                //);

            }


            
            //ValidateClear(propertyName);
        }

    }
}
