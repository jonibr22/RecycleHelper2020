using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecycleHelperApplication.Service.Helper
{
    public static class MyExtensions
    {
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, String tipe)
        {
            return CreateEncodedActionLinkMvcHtmlString(linkText, null, actionName, controllerName, routeValues, htmlAttributes, tipe);
        }

        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string routeGroup, string actionName, string controllerName, object routeValues, object htmlAttributes, String tipe)
        {
            return CreateEncodedActionLinkMvcHtmlString(linkText, routeGroup, actionName, controllerName, routeValues, htmlAttributes, tipe);
        }

        private static MvcHtmlString CreateEncodedActionLinkMvcHtmlString(string linkText, string routeGroup, string actionName, string controllerName, object routeValues, object htmlAttributes, String tipe)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
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
            ancor.Append("<a ");
            if (htmlAttributes != null)
            {
                ancor.Append(htmlAttributes);
            }
            ancor.Append(" href='");

            if (!String.IsNullOrEmpty(routeGroup))
            {
                ancor.Append($"/{routeGroup}");
            }
            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
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
            ancor.Append("'");
            ancor.Append(">");
            ancor.Append(linkText);
            ancor.Append("</a>");
            return MvcHtmlString.Create(ancor.ToString());
        }

        public static MvcHtmlString EncodedURLActionLink(this HtmlHelper htmlHelper, string routeGroup, string actionName, string controllerName, object routeValues, String tipe)
        {
            return CreateEncodedUrlActionLinkMvcHtmlString(routeGroup, actionName, controllerName, routeValues, tipe);
        }

        public static MvcHtmlString EncodedURLActionLink(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, String tipe)
        {
            return CreateEncodedUrlActionLinkMvcHtmlString(null, actionName, controllerName, routeValues, tipe);
        }

        private static MvcHtmlString CreateEncodedUrlActionLinkMvcHtmlString(string routeGroup, string actionName, string controllerName, object routeValues, String tipe)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
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
                ancor.Append("/" + routeGroup);
            }

            if (controllerName != string.Empty)
            {
                ancor.Append("/" + controllerName);
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
            //string key = "jdsg432387#";
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
    }
}
