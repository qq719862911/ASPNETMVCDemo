using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

using System.IO;
using System.Security.Cryptography;

namespace MVC2015.Web.Site.Common
{
    public static class CommonHelper
    {
        public static string ValidationCode_KEY = "ValidationCode";
        public static readonly string EncryptKey = "sd54nfg1";

        public static string Encrypt(string originString)
        {
            return Encrypt(originString, EncryptKey);
        }
        public static string Decrypt(string cryptString)
        {
            return Decrypt(cryptString, EncryptKey);
        }
        public static string Decrypt(string pToDecrypt, string sKey)
        {

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }


            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }
        public static string Encrypt(string pToEncrypt, string sKey)
        {

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }
        //Return object from Cookie after serializing json string
        public static T ReadObjectInCookie<T>(string key)
        {
            var objCookie = System.Web.HttpContext.Current.Request.Cookies[key];
            if (objCookie == null || string.IsNullOrEmpty(objCookie.Value))
                return default(T);


            string val = HttpUtility.UrlDecode(objCookie.Value, System.Text.UTF8Encoding.UTF8);

            return JsonConvert.DeserializeObject<T>(val);

        }

        // Serialize object to json string
        public static string ToJson(this object obj)
        {
            if (obj == null)
                return string.Empty;

            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //return jss.Serialize(obj);
            string json = JsonConvert.SerializeObject(obj, Formatting.None);
            return json;
        }

        //Save object in Cookie
        public static void SaveObjectInCookie(string key, object obj, DateTime? expr = null)
        {
            string cookieVal = ToJson(obj);

            cookieVal = HttpUtility.UrlEncode(cookieVal, System.Text.UTF8Encoding.UTF8); //HttpContext.Current.Server.UrlEncode(cookieVal);

            //UTF8Encoding utf8 = new UTF8Encoding();
            //byte[] encodeByte = utf8.GetBytes(cookieVal);
            //string test = utf8.GetString(encodeByte);

            //string encodeCookieVal = System.Text.Encoding.ASCII.GetString(encodeByte);

            if (string.IsNullOrEmpty(cookieVal))
            {
                return;
            }
            DateTime newExpr;
            if (expr.HasValue)
            {
                newExpr = expr.Value;
            }
            else
            {
                newExpr = DateTime.Now.AddMinutes(20);
            }
            //SetCookie(key, cookieVal, expr);

            HttpCookie ck;

            if (System.Web.HttpContext.Current.Request.Cookies[key] != null)
            {
                ck = System.Web.HttpContext.Current.Request.Cookies[key];
            }
            else
            {
                ck = new HttpCookie(key);
            }
            ck.Value = cookieVal;
            ck.Expires = newExpr;
            ck.HttpOnly = true;
            System.Web.HttpContext.Current.Request.Cookies.Set(ck);
            System.Web.HttpContext.Current.Response.Cookies.Add(ck);
        }

        public static byte[] CreateCheckCodeImage(string checkCode)
        {
            if (string.IsNullOrWhiteSpace(checkCode))
            {
                throw new ArgumentException("Check code is null!");
            }
          
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((double)(checkCode.Length * 25))), 40);
            Graphics g = Graphics.FromImage(image);

            try
            {
                Random random = new Random();

                g.Clear(Color.White);

                for (int i = 0; i <= 12; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    int colorImage = random.Next(2);
                    Color penColor = default(Color);
                    if (colorImage == 1)
                    {
                        penColor = Color.DarkOliveGreen;
                    }
                    else
                    {
                        penColor = Color.FromArgb(171, 192, 13);
                    }

                    g.DrawLine(new Pen(penColor), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 18, (System.Drawing.FontStyle.Strikeout));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    new Rectangle(0, 0, image.Width, image.Height), Color.FromArgb(0, 112, 192), Color.FromArgb(0, 188, 242), 1.2f, true);
                g.DrawString(checkCode, font, brush, 17, 8);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                //context.Response.ClearContent();
                //context.Response.ContentType = "image/Gif";
                //context.Response.BinaryWrite(ms.ToArray());
                return ms.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
        public static string GenerateCheckCode()
        {
            int number = 0;
            char code = '\0';
            string checkCode = String.Empty;
            char[] availableCode = 
                    {
                        '2','3','4','6','7','9','A','C','D','E','G','H','J',
			            'K','M','N','P','Q','R','T','U','V','W','X','Y','Z'
		            };

            System.Random random = new Random();

            for (int i = 0; i <= 5; i++)
            {
                number = random.Next();

                int indexChar = number % availableCode.Length;
                code = availableCode[indexChar];

                checkCode += code.ToString();
            }
            return checkCode;
        }

        public static void SetCookie(string key, string value, DateTime? exper = null, bool httpOnly = true)
        {
            //request
            HttpCookie ck;
            value = HttpUtility.UrlEncode(value, System.Text.UTF8Encoding.UTF8);
            if (System.Web.HttpContext.Current.Request.Cookies[key] != null)
            {
                ck = System.Web.HttpContext.Current.Request.Cookies[key];
                ck.Value = value;
            }
            else
            {
                ck = new HttpCookie(key);
                ck.Value = value;
            }
            if (exper != null)
            {
                ck.Expires = exper.Value;
                ck.HttpOnly = httpOnly;
            }

            //response
            HttpCookie ckRes;
            if (System.Web.HttpContext.Current.Response.Cookies[key] != null)
            {
                ckRes = System.Web.HttpContext.Current.Response.Cookies[key];
                ckRes.Value = value;
            }
            else
            {
                ckRes = new HttpCookie(key);
                ckRes.Value = value;
            }
            if (exper != null)
            {
                ckRes.Expires = exper.Value;
            }
            ckRes.HttpOnly = httpOnly;
        }

    }
}