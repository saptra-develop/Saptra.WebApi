using Devart.Data.MySql;
using Renci.SshNet;
using System;
using System.Configuration;
using System.Data;

namespace Saptra.WebApi.Data
{

    public class MySQLDB
    {
        //SSH conexion parameters
        private static string ssh_ip = ConfigurationManager.AppSettings["ssh_ip"].ToString();
        private static int ssh_port = Int32.Parse(ConfigurationManager.AppSettings["ssh_port"].ToString());
        private static string ssh_user = ConfigurationManager.AppSettings["ssh_user"].ToString();
        private static string ssh_password = ConfigurationManager.AppSettings["ssh_password"].ToString();

        //MYSQL conexion parameters
        private static string mysql_host = ConfigurationManager.AppSettings["mysql_host"].ToString();
        private static uint mysql_port = uint.Parse(ConfigurationManager.AppSettings["mysql_port"].ToString());
        private static string mysql_user = ConfigurationManager.AppSettings["mysql_user"].ToString();
        private static string mysql_password = ConfigurationManager.AppSettings["mysql_password"].ToString();
        private static string mysql_db = ConfigurationManager.AppSettings["mysql_db"].ToString();

        /// <summary>
        /// Post CheckIn hacia MySQL Server
        /// </summary>
        /// <param name="matricula">folio certificado</param>
        /// <param name="rfc">rfc usaurio</param>
        /// <returns></returns>
        public static bool PostCheckIn(string matricula, string rfc)
        {
            bool insertado = false;

            try
            {

                //Open SSH tunnel
                PasswordConnectionInfo connectionInfo = new PasswordConnectionInfo(ssh_ip, ssh_port, ssh_user, ssh_password);
                using (var client = new SshClient(connectionInfo))
                {
                    client.Connect();
                    if (client.IsConnected)
                    {
                        //Agregar puerto de escucha
                        var portForwarded = new ForwardedPortLocal(mysql_host, mysql_port, mysql_host, mysql_port);
                        client.AddForwardedPort(portForwarded);
                        portForwarded.Start();
                        using (var mysql_Con = new MySqlConnection("host=" + mysql_host + ";user=" + mysql_user + ";password=\"" + mysql_password + "\";database=" + mysql_db))
                        using (var mysql_cmd = mysql_Con.CreateCommand())
                        {
                            //Get data info
                            mysql_Con.Open();
                            mysql_cmd.CommandText = "INSERT INTO mProcesoEntrega (mpe_fechareg, mc_matrnu, mpe_rfcfigura) " +
                                                    "VALUES (@FECHA, @MATRICULA, @RFC)";
                            mysql_cmd.Parameters.Add("@FECHA", System.DateTime.Now.ToLongDateString());
                            mysql_cmd.Parameters.Add("@MATRICULA", matricula);
                            mysql_cmd.Parameters.Add("@RFC", rfc);
                            mysql_cmd.CommandType = CommandType.Text;
                            //Reportar inserción
                            insertado = (mysql_cmd.ExecuteNonQuery() > 0) ? true : false;
                            //Cerrar conexiones
                            mysql_Con.Close();
                            portForwarded.Stop();
                            client.Disconnect();
                        }
                    }
                    else
                    {
                        insertado = false;
                    }
                }
            }
            catch
            {
                insertado = false;
            }
            return insertado;
        }
    }
}
