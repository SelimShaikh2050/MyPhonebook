using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace phonebook
{
    class Connection
    {
        public SqlConnection Con = new SqlConnection(@"Data Source=.;Initial Catalog=myPhonebookDB;Integrated Security=True");
    }
}
