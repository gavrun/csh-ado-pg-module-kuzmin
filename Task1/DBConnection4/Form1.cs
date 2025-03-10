using Npgsql;
using System.Configuration;
using System.Data;

namespace DBConnection
{
    public partial class Form1 : Form
    {

        private NpgsqlConnection connection = new NpgsqlConnection();

        // ok
        //private string testConnect = @"Host=localhost;Port=5432;Database=northwind;Username=sa;Password=sa;Persist Security Info=True;";

        // error database
        //private string testConnect = @"Host=localhost;Port=5432;Database=Northwind;Username=sa;Password=sa;Persist Security Info=True;";

        // error user
        //private string testConnect = @"Host=localhost;Port=5432;Database=northwind;Username=sa;Password=xx;Persist Security Info=True;";

        // error server stop service

        public Form1()
        {
            InitializeComponent();

            menuDisconnect.Enabled = false;
            this.connection.StateChange += new StateChangeEventHandler(this.connection_StateChange);
        }

        private string testConnect = GetConnectionStringByName("DBConnect.NorthwindConnectionString");

        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            menuConnect.Enabled = (e.CurrentState == ConnectionState.Closed);
            menuDisconnect.Enabled = (e.CurrentState == ConnectionState.Open);
        }

        private static string GetConnectionStringByName(string name)
        {
            string returnValue = null;

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            if (settings != null)
            {
                returnValue = settings.ConnectionString;
            }

            return returnValue;
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
            catch (PostgresException XcpSQL)
            {
                MessageBox.Show(XcpSQL.MessageText, "PostgreSQL Error code: " + XcpSQL.SqlState, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (NpgsqlException XcpNpgsql)
            {
                MessageBox.Show("Error connecting to server: " + XcpNpgsql.ErrorCode, "as NpgsqlException", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception Xcp)
            {
                MessageBox.Show("Unexpected Error: " + Xcp.Message, "as Unexpected Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuDisconnect_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception Xcp)
            {
                MessageBox.Show("Error while closing connection: " + Xcp.Message, "as Unexpected Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void menuListConnections_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    MessageBox.Show($"name = {cs.Name}\nproviderName = {cs.ProviderName}\nconnectionString = {cs.ConnectionString}\n\nTotal connections {ConfigurationManager.ConnectionStrings.Count}", "Connections", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
