﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterView : TabbedPage
    {
      
        public MasterView(Usuario usuario)
        {
            InitializeComponent();
            this.BindingContext = new MasterViewModel(usuario);

        }

        protected override void OnAppearing()
        {
            AssinarMensagens();
        }

        protected override void OnDisappearing()
        {
            CancelarMensagens();
        }

        private void AssinarMensagens()
        {
            MessagingCenter.Subscribe<Usuario>(this, "EditarPerfil", (usuario) =>
            {
                this.CurrentPage = this.Children[1];
            });

            MessagingCenter.Subscribe<Usuario>(this, "SucessoSalvarUsuario", (usuario) =>
            {
                this.CurrentPage = this.Children[0];
            });
        }

        private void CancelarMensagens()
        {
            MessagingCenter.Unsubscribe<Usuario>(this, "EditarPerfil");
            MessagingCenter.Unsubscribe<Usuario>(this, "SucessoSalvarUsuario");
        }
    }
}