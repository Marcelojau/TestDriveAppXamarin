using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        const string url_getVeiculos = "http://aluracar.herokuapp.com/";
        public ObservableCollection<Veiculo> Veiculos { get; set; }

        Veiculo veiculoSelecionado;
        public Veiculo VeiculoSelecionado
        {
            get
            {
                return VeiculoSelecionado;
            }
            set
            {
                veiculoSelecionado = value;
                if (value != null)
                    MessagingCenter.Send(veiculoSelecionado,"VeiculoSelecionado");
            }
        }

        private bool aguarde;

        public bool Aguarde
        {
            get { return aguarde; }
            set 
            { 
                aguarde = value;
                OnPropertyChanged();
            }
        }


        public ListagemViewModel()
        {
            this.Veiculos = new ObservableCollection<Veiculo>();
        }


        public async Task GetVeiculos()
        {
            Aguarde = true;
            HttpClient httpClient = new HttpClient();
            var resultado = await httpClient.GetStringAsync(url_getVeiculos);

            var veiculosJson = JsonConvert.DeserializeObject<List<VeiculoJson>>(resultado);

            foreach(var veiculoJson in veiculosJson)
            {
                this.Veiculos.Add(new Veiculo
                {
                    Nome = veiculoJson.nome,
                    Preco = veiculoJson.preco
                });
            }

            Aguarde = false;
        }
    }
}
