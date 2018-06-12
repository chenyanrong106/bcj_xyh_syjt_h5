using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Security.Cryptography;

namespace SPACRM.Common
{
    /// <summary>
    /// Common CryptographyManager
    /// </summary>
    public class CryptographyManager
    {
        private static string Password = "^_^PUMAN^_^";

        /// <summary>
        /// Initializes a new instance of the <see cref="CryptographyManager"/> class.
        /// </summary>
        public CryptographyManager()
        {
        }

        /// <summary>
        /// 随机生成密钥
        /// </summary>
        /// <returns></returns>
        public static string GetIv(int n)
        {
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }

        /// <summary>   
        /// 任意选取不重复的num个随机数   
        /// </summary>   
        /// <returns></returns>   
        public static List<int> GenerateRnd(int num)
        {
            //用于保存返回的结果   
            List<int> result = new List<int>(num);
            Random random = new Random();
            int temp = 0;
            //如果返回的结果集合中实际的元素个数小于num个   
            while (result.Count < num)
            {
                //在[100000, 999999]区间任意取一个随机整数   
                temp = random.Next(100000, 999999);
                if (!result.Contains(temp))
                {
                    //如果在结果集合中不存在这个数，则添加这个数   
                    result.Add(temp);
                }
            }
            //result.Sort();//对返回结果进行排序   
            return result;

        }
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            md5.Clear();
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2").PadLeft(2, '0'));
            }
            return sb.ToString();
        }


        /// <summary>
        /// 单向加密方法，提供MD5、SHA1加密算法
        /// </summary>
        /// <param name="encryptString">被加密的字符串</param>
        /// <param name="encryptFormat">加密算法，有"md5"、"sha1"等</param>
        /// <returns>加密后的字符串</returns>
        /// <remarks>
        /// 当参数<paramref name="encryptFormat" />不为"md5"、"sha1"、"时，直接返回参数<paramref name="encryptString" />
        /// </remarks>
        public static string Md5Encrypt(string encryptString)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(encryptString, "md5");
        }


        private static byte[] _slat = new byte[] { 0x53, 0x6e, 0x64, 0x61, 0x20, 0x43, 0x52, 0x4d, 0x20, 0x58, 0x75, 0x61, 0x6e, 0x79, 0x65 };
        /// <summary>
        /// AES 加密函数
        /// </summary>
        /// <param name="toEncrypt">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string AESEncrypt(string toEncrypt)
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Password, _slat, 1024);
            return AESEncrypt(toEncrypt, pdb.GetBytes(32), pdb.GetBytes(16));
        }
        /// <summary>
        /// AES 加密函数
        /// </summary>
        /// <param name="toEncrypt">需要加密的字符串</param>
        /// <param name="keyArray">密钥</param>
        /// <param name="ivArray">向量.</param>
        /// <returns></returns>
        private static string AESEncrypt(string toEncrypt, byte[] keyArray, byte[] ivArray)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="toDecrypt">To decrypt.</param>
        /// <returns></returns>
        public static string AESDecrypt(string toDecrypt)
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(Password, _slat, 1024);
            return AESDecrypt(toDecrypt, pdb.GetBytes(32), pdb.GetBytes(16));
        }
        /// <summary>
        /// AES 解密
        /// </summary>
        /// <param name="toDecrypt">To decrypt.</param>
        /// <param name="keyArray">The key array.</param>
        /// <param name="ivArray">The iv array.</param>
        /// <returns></returns>
        private static string AESDecrypt(string toDecrypt, byte[] keyArray, byte[] ivArray)
        {
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.IV = ivArray;
            rDel.Mode = CipherMode.CBC;
            rDel.Padding = PaddingMode.Zeros;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        private static Rfc2898DeriveBytes RFCDB(string password)
        {
            return new Rfc2898DeriveBytes(password, _slat, 1024);
        }
    }
}
