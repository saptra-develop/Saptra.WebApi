using Devart.Data.MySql;
using Renci.SshNet;
using System;
using System.Data;

namespace Saptra.WebApi.Data
{

    public class MySQLDB
    {
        /*public static string Test()
        {
            // Password Authentication
            MySqlConnection myConn = new MySqlConnection("host=localhost;protocol=SSH;user=dbsieron;password=\"E$t43$tUc0ntra$3n1a;_\";database=db_conext");
            myConn.SshOptions.AuthenticationType = SshAuthenticationType.Password;
            myConn.SshOptions.User = "sieron";
            myConn.SshOptions.Host = "200.33.114.45";
            myConn.SshOptions.Port = 22;
            myConn.SshOptions.Password = "Acc3$o$i3r0n_;";
            MySqlCommand myCommand = new MySqlCommand("select NOW()", myConn);
            myConn.Open();
            Int64 count = Convert.ToInt64(myCommand.ExecuteScalar());
            Console.WriteLine(count);
            myConn.Close();
            return "";
        }*/

        public static String Test()
        {
            //Open SSH tunnel
            PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo("200.33.114.45", 22, "sieron", "Acc3$o$i3r0n_;");
            using (var client = new SshClient(connectionInfo))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    //Agregar puerto de escucha
                    var portForwarded = new ForwardedPortLocal("127.0.0.1", 3306, "localhost", 3306);
                    client.AddForwardedPort(portForwarded);
                    portForwarded.Start();
                    using (var mysql_Con = new MySqlConnection("host=localhost;user=dbsieron;password=\"E$t43$tUc0ntra$3n1a;_\";database=db_conext"))
                    using (var mysql_cmd = mysql_Con.CreateCommand())
                    {
                        //Get data info
                        mysql_Con.Open();
                        mysql_cmd.CommandText = "select * from v_certificados limit 10";
                        mysql_cmd.CommandType = CommandType.Text;
                        var da = new MySqlDataAdapter(mysql_cmd);
                        var ds = new DataSet();
                        da.Fill(ds);
                        //Close conexions
                        mysql_Con.Close();
                        portForwarded.Stop();
                        client.Disconnect();
                    }
                }
                else
                {
                    Console.WriteLine("Client cannot be reached...");
                }
            }
            return "";
        }

            //    /*
            //    SshClient client = null;
            //    ForwardedPortLocal port = null;
            //    MySqlConnection _Connection = null;
            //    //private int localPort;
            //    using (client = new SshClient("200.33.114.45", "sieron", "Acc3$o$i3r0n_;"))
            //    {
            //        try
            //        {
            //            client.Connect();
            //            port = new ForwardedPortLocal("127.0.0.1", 22, "127.0.0.1", 22);

            //            client.ErrorOccurred += delegate (object sender, ExceptionEventArgs e) {
            //                throw e.Exception;
            //            };
            //            port.Exception += delegate (object sender, ExceptionEventArgs e) {
            //                throw e.Exception;
            //            };
            //            port.RequestReceived += delegate (object sender, PortForwardEventArgs e) {
            //                Console.Write(e.OriginatorHost);
            //            };
            //            client.AddForwardedPort(port);
            //            port.Start();

            //            //MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();
            //            //connBuilder.Server = "127.0.0.1";
            //            //connBuilder.Port = 22;
            //            //connBuilder.UserID = "dbsieron";
            //            //connBuilder.Password = "E$t43$tUc0ntra$3n1a;_";
            //            //connBuilder.Database = "db_context";

            //            var cs = new MySqlConnectionStringBuilder();
            //            cs.Port = port.BoundPort;
            //            cs.Server = "127.0.0.1";
            //            cs.Database = "db_context";
            //            cs.UserID = "dbsieron";
            //            cs.Password = "E$t43$tUc0ntra$3n1a;_";

            //            _Connection = new MySqlConnection(cs.ConnectionString);
            //            _Connection.Open();
            //            _Connection.ChangeDatabase(cs.Database);

            //            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM table"))
            //            {
            //                cmd.Connection = _Connection;
            //                var rdr = cmd.ExecuteReader();
            //                DataTable result = new DataTable();
            //                result.Load(rdr);
            //                rdr.Close();
            //                rdr.Dispose();
            //                //dataGridView1.DataSource = result;
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            throw ex;
            //        }
            //        finally
            //        {
            //            if (_Connection != null && _Connection.State == ConnectionState.Open)
            //                _Connection.Close();
            //            if (_Connection != null)
            //                _Connection.Dispose();
            //            _Connection = null;
            //            MySqlConnection.ClearAllPools();
            //            if (port != null && port.IsStarted)
            //                port.Stop();
            //            if (port != null)
            //                port.Dispose();
            //            port = null;
            //            if (client != null && client.IsConnected)
            //                client.Disconnect();
            //        }
            //    }*/
            //    return "";
            //}
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
}
