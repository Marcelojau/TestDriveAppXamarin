using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestDrive.Data;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class AgendamentoUsuarioViewModel: BaseViewModel
    {
        ObservableCollection<Agendamento> lista = new ObservableCollection<Agendamento>();
        public ObservableCollection<Agendamento> Lista 
        {
            get 
            {
                return lista;       
            }
            private set 
            {
                lista = value;
                OnPropertyChanged();
            }
        }

        private Agendamento agendamentoSelecionado;
        public Agendamento AgendamentoSelecionado
        {
            get { return agendamentoSelecionado; }
            set
            {
                if (value != null)
                {
                    agendamentoSelecionado = value;
                    MessagingCenter.Send<Agendamento>(agendamentoSelecionado, "AgendamentoSelecionado");
                }
            }
        }
        public AgendamentoUsuarioViewModel()
        {
            AtualizarLista();
        }

        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {

                AgendamentoDAO dao = new AgendamentoDAO(conexao);
                var listaDb = dao.Lista;



                this.Lista.Clear();
                foreach (var itemDB in listaDb)
                {
                    this.Lista.Add(itemDB);
                }
            }
        }
    }
}
