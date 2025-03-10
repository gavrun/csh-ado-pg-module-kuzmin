using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WfApp1
{
    public partial class Form1: Form
    {
        string connectionString = "Host=localhost;Port=5432;Database=Northwind;Username=sa;Password=sa;";

        //NpgsqlConnection connection = new NpgsqlConnection(connectionString);


        public Form1()
        {
            InitializeComponent();
        }
    }
}
