using Npgsql;
using System.Configuration;

namespace WfAppCore1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // sa 
            string connectionString = "Host=localhost;Port=5432;Database=northwind;Username=sa;Password=sa;Timeout=5;";

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder();

            builder.Host = "localhost";
            builder.Port = 5432;
            builder.Database = "northwind";
            builder.Username = "sa";
            builder.Password = "sa";
            builder.Timeout = 5;
            builder.PersistSecurityInfo = true;

            string connectionString = builder.ConnectionString;

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["WfAppCore1.Properties.Settings.ConnectionString"].ConnectionString;

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            connection.Open();
        }
    }
}
