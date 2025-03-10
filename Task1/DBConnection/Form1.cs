using Npgsql;
using System.Data;

namespace DBConnection
{
    public partial class Form1 : Form
    {

        private NpgsqlConnection connection = new NpgsqlConnection();

        private string testConnect = @"Host=localhost;Port=5432;Database=northwind;Username=sa;Password=sa;Persist Security Info=True;";


        public Form1()
        {
            InitializeComponent();
        }

        private void menuConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;

                    connection.Open();
                    MessageBox.Show("Connected successfully");
                }
                else
                {
                    MessageBox.Show("Connection already exists");
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void menuDisconnect_Click(object sender, EventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                MessageBox.Show("Connection closed");
            }
            else
            {
                MessageBox.Show("Connection does not exist");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
