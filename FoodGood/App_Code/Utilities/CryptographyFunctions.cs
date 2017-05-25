using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace FoodGood.Utilities.Security
{
    /// <summary>
    /// Summary description for CryptographyFunctions
    /// </summary>
    public class CryptographyFunctions
    {
        public CryptographyFunctions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string SHA1HashTheString(string stringToHash)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] hashed = sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashed)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}