using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestDrive.Data;
using TestDrive.Models;
using TestDrive.Services;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoViewModel: BaseViewModel
    {
    
        public Agendamento Agendamento { get; set; }

        private string modelo;

        public string Modelo
        {
            get { return this.Agendamento.Modelo; }
            set { this.Agendamento.Modelo = value; }
        }
        public string Nome
        {
            get
            {
                return Agendamento.Nome;
            }

            set
            {
                Agendamento.Nome = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }

        }

        private decimal preco;

        public decimal Preco
        {
            get { return this.Agendamento.Preco; }
            set { this.Agendamento.Preco = value; }
        }

        public string Fone
        {
            get { return Agendamento.Fone; }
            set 
            { 
                Agendamento.Fone = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public string Email
        {
            get { return Agendamento.Email; }
            set 
            { 
                Agendamento.Email = value;
                OnPropertyChanged();
                ((Command)AgendarCommand).ChangeCanExecute();
            }
        }

        public DateTime DataAgendamento
        {
            get { return Agendamento.DataAgendamento; }
            set { Agendamento.DataAgendamento = value; }
        }

        public TimeSpan HoraAgendamento
        {
            get { return Agendamento.HoraAgendamento; }
            set { Agendamento.HoraAgendamento = value; }
        }
        public AgendamentoViewModel(Veiculo veiculo, Usuario usuario)
        {
            this.Agendamento = new Agendamento(usuario.nome, usuario.telefone, usuario.email, veiculo.Nome, veiculo.Preco);

            AgendarCommand = new Command(() =>
            {
                MessagingCenter.Send<Agendamento>(this.Agendamento, "Agendamento");
            }, () =>
            {
                return !string.IsNullOrEmpty(this.Nome) 
                        && !string.IsNullOrEmpty(this.Fone)
                        && !string.IsNullOrEmpty(this.Email);
            });
        }

        public ICommand AgendarCommand{ get; set; }

        public async void SalvarAgendamento()
        {
            AgendamentoService agendamentoSerivce = new AgendamentoService();
            await agendamentoSerivce.EnviarAgendamento(this.Agendamento);
        }
    }
}
