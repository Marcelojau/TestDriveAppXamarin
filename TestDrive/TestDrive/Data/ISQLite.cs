using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDrive.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}
