using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecycleHelperApplication.Service.Helper
{
    public static class Util
    {
        public static string generateRandomString()
        {
            return Guid.NewGuid().ToString();
        }

        public static MvcHtmlString EncodeUrl(string actionName, string controllerName, object routeValues, String tipe)
        {
            return GenerateMvcHtmlEncodedString(null, actionName, controllerName, routeValues, tipe);
        }

        public static MvcHtmlString EncodeUrl(string routeGroup, string actionName, string controllerName, object routeValues, String tipe)
        {
            return GenerateMvcHtmlEncodedString(routeGroup, actionName, controllerName, routeValues, tipe);
        }

        private static MvcHtmlString GenerateMvcHtmlEncodedString(string routeGroup, string actionName, string controllerName, object routeValues, String tipe)
        {
            string queryString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            StringBuilder ancor = new StringBuilder();

            if (!String.IsNullOrEmpty(routeGroup))
            {
                ancor.Append(routeGroup + "/");
            }

            if (controllerName != string.Empty)
            {
                ancor.Append(controllerName);
            }

            if (actionName != "Index")
            {
                ancor.Append("/" + actionName);
            }
            if (queryString != string.Empty)
            {
                if (tipe == "string")
                {
                    ancor.Append("?s=" + Encrypt(queryString));
                }
                else if (tipe == "int")
                {
                    ancor.Append("?q=" + Encrypt(queryString));
                }
            }
            return MvcHtmlString.Create(ancor.ToString());
        }

        public static string Encrypt(string plainText)
        {
            string key = "adsg432387#";
            byte[] EncryptKey = { };
            byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
            EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
            cStream.Write(inputByte, 0, inputByte.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        public static DateTime changeDateFormat(string dateInput)
        {
            string txtTglLahir = dateInput;
            string[] dateTemp = txtTglLahir.Split('/');

            string dd = dateTemp[1];
            string MM = dateTemp[0];
            string yyyy = dateTemp[2];

            string[] yyyyTemp = yyyy.Split(' ');

            if (yyyyTemp.Length > 1)
            {
                yyyy = yyyyTemp[0];
            }

            if (MM == "Jan")
            {
                MM = "01";
            }
            else if (MM == "Feb")
            {
                MM = "02";
            }
            else if (MM == "Mar")
            {
                MM = "03";
            }
            else if (MM == "Apr")
            {
                MM = "04";
            }
            else if (MM == "Mei")
            {
                MM = "05";
            }
            else if (MM == "Jun")
            {
                MM = "06";
            }
            else if (MM == "Jul")
            {
                MM = "07";
            }
            else if (MM == "Agu" || MM == "Agt")
            {
                MM = "08";
            }
            else if (MM == "Sep")
            {
                MM = "09";
            }
            else if (MM == "Okt")
            {
                MM = "10";
            }
            else if (MM == "Nov")
            {
                MM = "11";
            }
            else if (MM == "Des")
            {
                MM = "12";
            }

            string finalDate = dd + "-" + MM + "-" + yyyy;

            // format finalDate bisa diganti sesuai dengan input di parameter DateTime.ParseExact
            // untuk di input ke database
            return DateTime.ParseExact(finalDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }

    }
}
