using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static TestDrive.LoginService;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<LoginException>(this, "FalhaLogin",
                async (exc) => 
                {
                    await DisplayAlert("Login", exc.Message, "OK");
                }
            );
        }

        protected override void OnDisappearing()
        {
            //Cancela a mensagem 
            MessagingCenter.Unsubscribe<LoginException>(this, "FalhaLogin");
        }
    }
}