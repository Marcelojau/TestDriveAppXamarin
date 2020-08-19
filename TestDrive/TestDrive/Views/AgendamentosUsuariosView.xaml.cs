using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using TestDrive.Services;
using TestDrive.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestDrive.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgendamentosUsuariosView : ContentPage
    {
        readonly AgendamentoUsuarioViewModel _viewModel;
        public AgendamentosUsuariosView()
        {
            InitializeComponent();
            this._viewModel = new AgendamentoUsuarioViewModel();
            this.BindingContext = this._viewModel;
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Agendamento>(this, "AgendamentoSelecionado",
                async (agendamento) =>
                {
                    if (!agendamento.Confirmado)
                    {
                        var reenviar = await DisplayAlert("Reenviar Agendamento", "Deseja reenviar o agendamento?", "Sim", "Não");

                        if (reenviar)
                        {
                            AgendamentoService agendamentoService = new AgendamentoService();
                            await agendamentoService.EnviarAgendamento(agendamento);
                            this._viewModel.AtualizarLista();
                        }
                    }
                });

            MessagingCenter.Subscribe<Agendamento>(this, "SucessoAgendamento", 
                async (agendamento) => 
                {
                    await DisplayAlert("Reenviar", "Reenvio feito com sucesso!", "OK");
                });

            MessagingCenter.Subscribe<Agendamento>(this, "FalhaAgendamento",
                async (agendamento) =>
                {
                    await DisplayAlert("Reenviar", "Erro ao reenviar agendamento, por favor tente mais tarde!", "OK");
                });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Agendamento>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Agendamento>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Agendamento>(this, "FalhaAgendamento");
        }
    }
}