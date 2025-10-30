using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models;
using AlbumApp1._0._1.Models.Tables;
using AlbumApp1._0._1.Services.Exit;
using AlbumApp1._0._1.Services.Users;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.WindowsAppSDK.Runtime.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace AlbumApp1._0._1.ViewModels
{
    public partial class MailSendViewModel:DialogViewModel
    {
        private readonly IContentDialogExit contentdialog;
        private readonly IUserService userService;
        public MailSendViewModel(IContentDialogExit contentDialog, IUserService userService) 
        {
            contentdialog = contentDialog;
            this.userService = userService;

            //MailModel = new();
            _isEnabled = false;
            if (SendMail().IsCompleted == true)
            {
                _isEnabled = true;
            }
            Random rnd = new Random();
            RandomPass = rnd.Next(10000, 99999);
        }

        public MailModel MailModel { get; set; } = new();
        public string Mail
        {
            get=>MailModel.Email; set { MailModel.Email = value;
            
            OnPropertyChanged(nameof(Mail));
            }
                
        }
        public int Code
        {
            get => MailModel.Code; set
            {
                MailModel.Code = value;

                OnPropertyChanged(nameof(Code));
            }

        }
        public int RandomPass
        {
            get => MailModel.RandomPass;
            set
            {
              
                MailModel.RandomPass = value;
                OnPropertyChanged(nameof(RandomPass));

            }
        }
        public string Pass
        {
            get => MailModel.Pass; set
            {
                MailModel.Pass = value;

                OnPropertyChanged(nameof(Pass));
            }

        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled; set
            {
               
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        protected override async Task OnPrimaryExecuted()
        {

            var user = new Пользователи();
            if (Code.Equals(RandomPass))
            {
                user =  await userService.GetUserByEmail(Mail);
                await userService.UpdateUser(user, Pass);
            }
   
        }

        private string _text;
        public string Text
        {
            get => _text; set
            {
                SetProperty(ref _text, value);
                OnPropertyChanged(nameof(Text));

            }
        }
        [RelayCommand]
        public async Task SendMail()
        {

          
            var fromAddress = new MailAddress("forestnative277@gmail.com", "Восстановление");
            var toAddress = new MailAddress(Mail);
            const string fromPassword = "zhuqlamfyruwlfio"; 

    var smtp = new System.Net.Mail.SmtpClient
    {

        UseDefaultCredentials = false,
        Host = "smtp.gmail.com",
        Port = 587,
        EnableSsl = true,
        DeliveryMethod = SmtpDeliveryMethod.Network,
        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
        Timeout = 20000
    };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Код восстановления",
                Body = $"Ваш код для восстановления пароля: {RandomPass}",
            })
            {
                await smtp.SendMailAsync(message);
            }
            
        }

    }
}
