using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace smartData.Common
{
    public static class CommonFunction
    {
        #region encryption/decryption
        private static byte[] key = { };
        private static byte[] IV = { 38, 55, 206, 48, 28, 64, 20, 16 };
        private static string stringKey = "!5663a#KN";
        #endregion


        #region Encrypt
        /// <summary>
        /// <Decription>encrypt text for storing in db</Decription>

        /// </summary>
        /// <param name="text">Text to encrypt</param>
        public static string Encrypt(string text)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));

                //des = new DESCryptoServiceProvider();
                Byte[] byteArray = Encoding.UTF8.GetBytes(text);

                //MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                des.Dispose();
                memoryStream.Dispose();
            }
        }
        #endregion

        #region Decrypt
        /// <summary>
        /// <Decription>decrypt text for matching with input</Decription>
        /// </summary>
        /// <param name="text">Text To decrypt</param>
        public static string Decrypt(string text)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                key = Encoding.UTF8.GetBytes(stringKey.Substring(0, 8));


                Byte[] byteArray = Convert.FromBase64String(text);


                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                cryptoStream.Write(byteArray, 0, byteArray.Length);
                cryptoStream.FlushFinalBlock();

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                des.Dispose();
                memoryStream.Dispose();
            }
        }
        #endregion
    }
    public static class GlobalVar
    {

        static int _userType;
        public static int userType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
            }
        }
        static int _Insudtry;
        public static int Insudtry
        {
            get
            {
                return _Insudtry;
            }
            set
            {
                _Insudtry = value;
            }
        }


    }
}