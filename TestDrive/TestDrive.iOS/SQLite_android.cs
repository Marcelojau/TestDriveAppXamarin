using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using TestDrive.Data;

namespace TestDrive.Droid
{
    public class SQLite_android : ISQLite
    {
        public SQLiteConnection PegarConexao()
        {
            return new SQLite.SQLiteConnection("Agendamento.db3");
        }
    }
}