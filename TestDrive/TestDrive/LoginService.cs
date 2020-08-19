using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestDrive.Models;
using Xamarin.Forms;
using static TestDrive.Models.Usuario;

namespace TestDrive
{
	public class LoginService
	{
		public async Task FazerLogin(Login login)
		{
			
			using (var client = new HttpClient())
			{
				var camposFormulario = new FormUrlEncodedContent(new[]
				{
					new KeyValuePair<string,string>("email", login.email),
					new KeyValuePair<string,string>("senha", login.senha)
				});

				client.BaseAddress = new Uri("https://aluracar.herokuapp.com");
				HttpResponseMessage resultado = new HttpResponseMessage();
				try
				{
					resultado = await client.PostAsync("/login", camposFormulario);

				}
				catch
				{
					MessagingCenter.Send<LoginException>(new LoginException(@"Ocorreu um erro de comunicação com o servidor.
					Por favor verifique sua conexão e tente novamente mais tarde!!"), "FalhaLogin");

				}

				if (resultado.IsSuccessStatusCode) 
				{
					var conteudoResultado = await resultado.Content.ReadAsStringAsync();
					var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudoResultado);
					MessagingCenter.Send<Usuario>(resultadoLogin.usuario, "SucessoLogin");
				}
				else 
				{ 
					MessagingCenter.Send<LoginException>(new LoginException("Usuário ou senha incorretos!"), "FalhaLogin");
				}

			}
			
			
		}

		public class LoginException : Exception
		{
			public LoginException(string message) : base(message)
			{

			}
		}
	}
}
