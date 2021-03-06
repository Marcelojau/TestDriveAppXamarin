﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.ViewModels;
using Xamarin.Forms;

namespace TestDrive.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    public partial class ListagemView : ContentPage
    {
    
        public ListagemViewModel ViewModel { get; set; }
        readonly Usuario usuario;
        public ListagemView(Usuario usuario)
        {

            InitializeComponent();
            this.ViewModel = new ListagemViewModel();
            this.usuario = usuario;
            this.BindingContext = this.ViewModel;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            AssinarMensagens();

            await this.ViewModel.GetVeiculos();

        }

      
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            CancelarAssinatura();
        }

        private void AssinarMensagens()
        {

            MessagingCenter.Subscribe<Veiculo>(this, "VeiculoSelecionado",
                (veiculo) =>
                {
                    Navigation.PushAsync(new DetalheView(veiculo, this.usuario));
                });
            MessagingCenter.Subscribe<Exception>(this, "FalhaListagem",
             (msg) =>
             {
                 DisplayAlert("Erro", "ocorreu um erro ao obter a listagem de veículos, por favor verifique sua conexão e tente mais tarde", "OK");
             });
        }

        private void CancelarAssinatura()
        {
            MessagingCenter.Unsubscribe<Veiculo>(this, "VeiculoSelecionado");
            MessagingCenter.Unsubscribe<Exception>(this, "FalhaListagem");
        }

    }
}
