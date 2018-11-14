using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saptra.WebApi.Utils
{
    public static class Globals
    {
        //CATALOGO TIPO ESTATUS
        public static string TES_BORRADO_LOGICO = "Borrado Logico";
        public static string TES_PLAN_SEMANAL = "Plan Semanal";
        //CATALOGO ESTATUS
        public static string EST_ACTIVO = "Activo";
        public static string EST_INACTIVO = "Inactivo";
        public static string EST_VALIDADO= "Validado";
        //CATALOGO TIPO_FIGURA
        public static string[] CAT_TIPO_FIGURA = { "Promotor", "Tecnico docente", "Formador" };


        /// <summary>
        /// Convert base64 string to image file, save it and return their path
        /// </summary>
        /// <param name="ImgBase64"></param>
        /// <returns></returns>
        public static String PathImage(string ImgBase64)
        {
            string _path = "";
            string path = ConfigurationManager.AppSettings["Path"];
            string remote_path = ConfigurationManager.AppSettings["RemotePath"];
            string default_image = ConfigurationManager.AppSettings["DefaultImage"];
            string file_name = Guid.NewGuid().ToString() + ".jpeg";
            try
            {
                Byte[] b_img = Convert.FromBase64String(ImgBase64);
                MemoryStream ms = new MemoryStream(b_img, 0, b_img.Length);
                Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(Path.Combine(path, file_name), System.Drawing.Imaging.ImageFormat.Jpeg);

                if(File.Exists(Path.Combine(path, file_name))){
                    _path = Path.Combine(remote_path, file_name);
                }
                else
                {
                    _path = Path.Combine(remote_path, default_image);
                }
            }
            catch(Exception ex)
            {
                _path = Path.Combine(remote_path, default_image).Replace("\\","/");
            }
            return _path;
        }

        /// <summary>
        /// Search image file inside directory, generate and return their base64 string
        /// </summary>
        /// <param name="remotePath"></param>
        /// <returns></returns>
        public static String GetImgBase64(String remotePath)
        {
            string ImgBase64 = "";
            string path = ConfigurationManager.AppSettings["Path"];
            string default_image = ConfigurationManager.AppSettings["DefaultImage"];

            try
            {
                if (remotePath != "")
                {
                    var getFileName = remotePath.Split('/');
                    string file_name = getFileName[getFileName.Length-1].ToString();
                    FileInfo file = new FileInfo(Path.Combine(path, file_name));
                    if (file.Exists)
                    {
                        byte[] b_imge = File.ReadAllBytes(Path.Combine(path, file_name));
                        ImgBase64 = Convert.ToBase64String(b_imge);
                    }
                }
                else
                {
                    FileInfo file = new FileInfo(Path.Combine(path, default_image));
                    if (file.Exists)
                    {
                        byte[] b_imge = File.ReadAllBytes(Path.Combine(path, default_image));
                        ImgBase64 = Convert.ToBase64String(b_imge);
                    }
                }
            }
            catch(Exception ex)
            {
                ImgBase64 = "";
            }
            return ImgBase64;
        }
    }
}
