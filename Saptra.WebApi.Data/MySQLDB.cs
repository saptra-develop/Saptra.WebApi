using Devart.Data.MySql;
using Renci.SshNet;
using System;
using System.Data;

namespace Saptra.WebApi.Data
{

    public class MySQLDB
    {
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
    }
}
