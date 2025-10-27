using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Models
{
    [NotMapped]
    public partial class ValidateInputModel : ObservableObject
    {
       
        private string _memberElement;
    
        private string _errorMessage;
        public string ErrorMessage_
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }
        public string MemberElement {  get=>_memberElement; set {

                _memberElement = value;
                OnPropertyChanged(nameof(_memberElement));
            } }
        public ValidateInputModel(string errorMessage, string memberElement)
        {
            ErrorMessage_ = errorMessage;
            MemberElement = memberElement;  
        }
        public ValidateInputModel()
        {
  
        }

    }
}
