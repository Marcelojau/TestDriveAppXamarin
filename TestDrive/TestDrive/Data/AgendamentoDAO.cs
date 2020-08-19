using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using TestDrive.Models;

namespace TestDrive.Data
{
    public class AgendamentoDAO
    {
        readonly SQLiteConnection conexao;
        private List<Agendamento> lista;

        public List<Agendamento> Lista
        {
            get 
            {
                return conexao.Table<Agendamento>().ToList();
            }
            private set { lista = value; }
        }

        public AgendamentoDAO(SQLiteConnection conexao)
        {
            this.conexao = conexao;
            this.conexao.CreateTable<Agendamento>();
        }

        public void Salvar(Agendamento agendamento)
        {
            if (conexao.Find<Agendamento>(agendamento.ID) == null)
            {
                conexao.Insert(agendamento);
            }
            else
            {
                conexao.Update(agendamento);
            }
        }
 
    }
}
