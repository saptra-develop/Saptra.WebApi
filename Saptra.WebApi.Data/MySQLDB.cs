
using MySql.Data.MySqlClient;
using Renci.SshNet;
using Renci.SshNet.Common;
using System;
using System.Data;
using System.Reflection;

namespace Saptra.WebApi.Data
{

    public class MySQLDB
    {

        public static String Test()
        {
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("200.33.114.45", 22, "sieron", "Acc3$o$i3r0n_;");
            connectionInfo.Timeout = TimeSpan.FromSeconds(30);
            var client = new SshClient(connectionInfo);
            client.Connect();
            if (client.IsConnected)
                {
                var portForwarded = new ForwardedPortLocal("127.0.0.1", 22, "127.0.0.1", 3306);
                client.AddForwardedPort(portForwarded);
                portForwarded.Start();
                using (MySqlConnection con = new MySqlConnection("SERVER=127.0.0.1;UID=dbsieron;PASSWORD=\"E$t43$tUc0ntra$3n1a;_\";DATABASE=db_context;SslMode=none;"))
                    {
                        using (MySqlCommand com = new MySqlCommand("SELECT NOW()", con))
                        {
                            com.CommandType = CommandType.Text;
                            DataSet ds = new DataSet();
                            MySqlDataAdapter da = new MySqlDataAdapter(com);
                            da.Fill(ds);
                            foreach (DataRow drow in ds.Tables[0].Rows)
                            {
                                Console.WriteLine("From MySql: " + drow[1].ToString());
                            }
                        }
                    }
                    client.Disconnect();
                }
                else
                {
                    Console.WriteLine("Client cannot be reached...");
                }
            
            /*
            SshClient client = null;
            ForwardedPortLocal port = null;
            MySqlConnection _Connection = null;
            //private int localPort;
            using (client = new SshClient("200.33.114.45", "sieron", "Acc3$o$i3r0n_;"))
            {
                try
                {
                    client.Connect();
                    port = new ForwardedPortLocal("127.0.0.1", 22, "127.0.0.1", 22);

                    client.ErrorOccurred += delegate (object sender, ExceptionEventArgs e) {
                        throw e.Exception;
                    };
                    port.Exception += delegate (object sender, ExceptionEventArgs e) {
                        throw e.Exception;
                    };
                    port.RequestReceived += delegate (object sender, PortForwardEventArgs e) {
                        Console.Write(e.OriginatorHost);
                    };
                    client.AddForwardedPort(port);
                    port.Start();

                    //MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();
                    //connBuilder.Server = "127.0.0.1";
                    //connBuilder.Port = 22;
                    //connBuilder.UserID = "dbsieron";
                    //connBuilder.Password = "E$t43$tUc0ntra$3n1a;_";
                    //connBuilder.Database = "db_context";

                    var cs = new MySqlConnectionStringBuilder();
                    cs.Port = port.BoundPort;
                    cs.Server = "127.0.0.1";
                    cs.Database = "db_context";
                    cs.UserID = "dbsieron";
                    cs.Password = "E$t43$tUc0ntra$3n1a;_";

                    _Connection = new MySqlConnection(cs.ConnectionString);
                    _Connection.Open();
                    _Connection.ChangeDatabase(cs.Database);

                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM table"))
                    {
                        cmd.Connection = _Connection;
                        var rdr = cmd.ExecuteReader();
                        DataTable result = new DataTable();
                        result.Load(rdr);
                        rdr.Close();
                        rdr.Dispose();
                        //dataGridView1.DataSource = result;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (_Connection != null && _Connection.State == ConnectionState.Open)
                        _Connection.Close();
                    if (_Connection != null)
                        _Connection.Dispose();
                    _Connection = null;
                    MySqlConnection.ClearAllPools();
                    if (port != null && port.IsStarted)
                        port.Stop();
                    if (port != null)
                        port.Dispose();
                    port = null;
                    if (client != null && client.IsConnected)
                        client.Disconnect();
                }
            }*/
            return "";
        }
        //public static String Test()
        //{
        //    var ci = new ConnectionInfo("200.33.114.45", "sieron", new PasswordAuthenticationMethod("sieron", "Acc3$o$i3r0n_;"));
        //    var cs = new MySqlConnectionStringBuilder();
        //    cs.AllowBatch = true;
        //    cs.Server = "127.0.0.1";
        //    cs.Database = "db_conext";
        //    cs.UserID = "dbsieron";
        //    cs.Password = "E$t43$tUc0ntra$3n1a;_";

        //    using (var tunnel = new SshTunnel(ci, 3306))
        //    {
        //        cs.Port = checked((uint)tunnel.LocalPort);

        //        using (var connection = new MySqlConnection(cs.GetConnectionString(true)))
        //        using (var cmd = connection.CreateCommand())
        //        {
        //            connection.Open();
        //            cmd.CommandText = "SELECT NOW()";

        //            var dt = new DataTable();
        //            var da = new MySqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            //dt.Dump();
        //        }
        //    }
        //    return "";
        //}

    }

    class SshTunnel : IDisposable
    {
        private SshClient client;
        private ForwardedPortLocal port;
        private int localPort;

        public SshTunnel(ConnectionInfo connectionInfo, uint remotePort)
        {
            try
            {
                client = new SshClient(connectionInfo);
                port = new ForwardedPortLocal("127.0.0.1", 0, "127.0.0.1", remotePort);

                client.ErrorOccurred += delegate (object sender, ExceptionEventArgs e) {
                    throw e.Exception;
                };
                port.Exception += delegate (object sender, ExceptionEventArgs e) {
                    throw e.Exception;
                };
                port.RequestReceived += delegate (object sender, PortForwardEventArgs e) {
                    Console.Write(e.OriginatorHost);
                };
                client.Connect();
                client.AddForwardedPort(port);
                port.Start();

                // Hack to allow dynamic local ports, ForwardedPortLocal should expose _listener.LocalEndpoint
                var listener = (System.Net.Sockets.TcpListener)typeof(ForwardedPortLocal).GetField("_listener", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(port);
                localPort = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port;
            }
            catch(Exception ex)
            {
                Dispose();
                throw ex;
            }
        }

        public int LocalPort { get { return localPort; } }

        public void Dispose()
        {
            if (port != null)
                port.Dispose();
            if (client != null)
                client.Dispose();
        }
    }
}
