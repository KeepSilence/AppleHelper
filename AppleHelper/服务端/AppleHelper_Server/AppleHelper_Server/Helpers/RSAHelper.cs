using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AppleHelper_Server
{
    /********************************************************************************
     * 
     *      作者：苦笑
     *      时期：2013/1/28
     *      
     * 
     *******************************************************************************/


    /// <summary>
    /// RSA加密算法辅助类，包括RSA公钥与私钥的产生与保存、RSA加解密、RSA签名验证。
    /// </summary>
    public class RSAHelper
    {
        #region RSA 密钥的产生与保存

        /// <summary>
        /// 随机产生RSA公钥与私钥的XML字符串
        /// </summary>
        /// <param name="xmlPublicKey"></param>
        /// <param name="xmlPrivateKey"></param>
        public static void GetRSAKeys(out string xmlPublicKey, out string xmlPrivateKey)
        {
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                xmlPublicKey = rsaProvider.ToXmlString(false);
                xmlPrivateKey = rsaProvider.ToXmlString(true);
            }
        }

        /// <summary>
        /// 将随机产生的RSA公钥与私钥的XML字符串保存到指定的文件中
        /// </summary>
        /// <param name="publicKeyPath">公钥文件保存路径(包含文件名)</param>
        /// <param name="privateKeyPath">私钥文件保存路径(包含文件名)</param>
        public static void SaveRSAKeysToFile(string publicKeyPath, string privateKeyPath)
        {
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                WriteKey(publicKeyPath, rsaProvider.ToXmlString(false));  //write public key
                WriteKey(privateKeyPath, rsaProvider.ToXmlString(true));  //write private key
            }
        }

        /// <summary>
        /// 将密钥写入到指定的路径(包括文件名)
        /// </summary>
        /// <param name="keyPath">密钥的路径(包括文件名)</param>
        /// <param name="keyValue">密钥的值</param>
        private static void WriteKey(string keyPath, string keyValue)
        {
            using (FileStream fstream = new FileStream(keyPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fstream))
                {
                    writer.WriteLine(keyValue);
                }
            }
        }

        #endregion

        #region  RSA 加解密

        /**************************************************************************************
         * 
         *  说明：
         *       RSA加密函数可加密的最大长度为 RSACryptoServiceProvider.KeySize / 8 - 11 位，
         *       此处的keysize 为密钥的长度。
         *       采用不同的编码方式会造成能加密的最大长度不同。
         *       例如：当keysize = 1024，采用UTF-8与GB2312编码最大长度为117，而Unicode 则为 58.
         *       
        ****************************************************************************************/


        #region 加密长度受限

        /// <summary>
        /// 使用指定的编码对字符串进行RSA加密。 
        /// 如果成功，返回加密后的字符串，否则，返回 null 。
        /// 此加密函数可加密的的长度比较小，且跟所选择的编码方式有关。
        /// 对于大数据的加密，建议使用扩展的加密方法RSAEncryptEx或对称的加密方法。
        /// </summary>
        /// <param name="xmlPublicKey">加密所使用的公钥</param>
        /// <param name="dataToEncrypt">待加密的字符串。</param>
        /// <param name="encodeingType">加密所使用的编码方式，可为 UTF-8、GB2312、UNICODE,默认为UTF-8编码。</param>
        /// <returns></returns>
        public static string RSAEncrypt(string xmlPublicKey, string dataToEncrypt, string encodeingType = "UTF-8")
        {
            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPublicKey);
                    byte[] originalBytes;

                    switch (encodeingType.Trim().ToUpper())
                    {
                        case "UTF-8":
                            originalBytes = (UnicodeEncoding.GetEncoding("UTF-8").GetBytes(dataToEncrypt));
                            break;
                        case "GB2312":
                            originalBytes = (UnicodeEncoding.GetEncoding("GB2312").GetBytes(dataToEncrypt));
                            break;
                        case "UNICODE":
                            originalBytes = (new UnicodeEncoding()).GetBytes(dataToEncrypt);
                            break;
                        default:
                            originalBytes = null;
                            break;
                    }

                    if (originalBytes == null)
                        return null;

                    byte[] resultBytes = rsa.Encrypt(originalBytes, false);   //加密
                    return Convert.ToBase64String(resultBytes);
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 使用指定的编码对字符串进行RSA解密
        /// 如果成功，返回加密后的字符串，否则，返回 null 。
        /// 对于大数据的解密，建议使用扩展的加密方法RSADecryptEx或对称的解密方法。
        /// </summary>
        /// <param name="xmlPrivateKey">解密所使用的私钥</param>
        /// <param name="dataToDecrypt">待解密的字符串</param>
        /// <param name="encodeingType">解密所使用的编码方式，可为 UTF-8、GB2312、UNICODE,默认为UTF-8编码。
        /// 编码应与加密的编码相同</param>
        public static string RSADecrypt(string xmlPrivateKey, string dataToDecrypt, string encodeingType = "UTF-8")
        {
            if (string.IsNullOrEmpty(dataToDecrypt))
                return null;

            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPrivateKey);
                    byte[] rgb = Convert.FromBase64String(dataToDecrypt);
                    byte[] resultBytes = rsa.Decrypt(rgb, false);   //解密
                    string resultStr = string.Empty;

                    switch (encodeingType.Trim().ToUpper())
                    {
                        case "UTF-8":
                            resultStr = (UnicodeEncoding.GetEncoding("UTF-8").GetString(resultBytes));
                            break;
                        case "GB2312":
                            resultStr = (UnicodeEncoding.GetEncoding("GB2312").GetString(resultBytes));
                            break;
                        case "UNICODE":
                            resultStr = (new UnicodeEncoding().GetString(resultBytes));
                            break;
                        default:
                            resultStr = null;
                            break;
                    }

                    return resultStr;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 加密长度不受限制

        /// <summary>
        /// 使用RSA算法对指定的字符串进行加密,此方法不受加密字符串长度的限制.
        /// 返回加密后的字符串.
        /// </summary>
        /// <param name="xmlPublicKey">加密所使用的公钥</param>
        /// <param name="dataToEncrypt">待加密的字符串</param>
        public static string RSAEncryptEx(string xmlPublicKey, string dataToEncrypt)
        {
            if (dataToEncrypt.Length == 0)
            {
                return RSAEncrypt(xmlPublicKey, dataToEncrypt, "UNICODE");
            }

            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPublicKey);
                    byte[] originalBytes = (UnicodeEncoding.GetEncoding("UTF-8").GetBytes(dataToEncrypt));

                    int keySize = rsa.KeySize / 8;
                    int bufferSize = keySize - 11;
                    byte[] buffer = new byte[bufferSize];

                    using (MemoryStream inputStream = new MemoryStream(originalBytes))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            int readLength = inputStream.Read(buffer, 0, bufferSize);
                            while (readLength > 0)
                            {
                                byte[] dataToEnc = new byte[readLength];
                                Array.Copy(buffer, 0, dataToEnc, 0, readLength);
                                byte[] resultBuffer = rsa.Encrypt(dataToEnc, false);
                                outputStream.Write(resultBuffer, 0, resultBuffer.Length);
                                readLength = inputStream.Read(buffer, 0, bufferSize);
                            }
                            byte[] resultBytes = outputStream.ToArray();  //得到加密结果
                            return Convert.ToBase64String(resultBytes);
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// 使用RSA算法对指定字符串进行解密，此方法不受加密字符串长度的限制.
        /// 如果成功，返回加密后的字符串，否则，返回 null 。
        /// </summary>
        /// <param name="xmlPrivateKey">解密所使用的私钥</param>
        /// <param name="dataToDecrypt">待解密的字符串</param>
        public static string RSADecryptEx(string xmlPrivateKey, string dataToDecrypt)
        {
            if (string.IsNullOrEmpty(dataToDecrypt))
                return null;

            try
            {
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPrivateKey);
                    byte[] dataDecBytes = Convert.FromBase64String(dataToDecrypt);

                    int keySize = rsa.KeySize / 8;
                    byte[] buffer = new byte[keySize];

                    using (MemoryStream inputStream = new MemoryStream(dataDecBytes))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            int readLength = inputStream.Read(buffer, 0, keySize);
                            while (readLength > 0)
                            {
                                byte[] dataToDec = new byte[readLength];
                                Array.Copy(buffer, 0, dataToDec, 0, readLength);
                                byte[] resultBuffer = rsa.Decrypt(dataToDec, false);
                                outputStream.Write(resultBuffer, 0, resultBuffer.Length);
                                readLength = inputStream.Read(buffer, 0, keySize);
                            }
                            byte[] resultBytes = outputStream.ToArray();  //得到解密结果
                            return (UnicodeEncoding.GetEncoding("UTF-8").GetString(resultBytes));
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #endregion

        #region Hash 数字摘要

        /// <summary>
        /// 生成指定字符串的数字摘要。
        /// </summary>
        /// <param name="strSource">目标字符串</param>
        public static string Hash(string strSource)
        {
            try
            {
                byte[] originalBytes = Encoding.GetEncoding("UTF-8").GetBytes(strSource);
                using (HashAlgorithm md5 = HashAlgorithm.Create("MD5"))
                {
                    byte[] hashBytes = md5.ComputeHash(originalBytes);
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 生成指定文件流的数字摘要。
        /// </summary>
        /// <param name="sourceFileStream">目标文件流</param>
        public static string Hash(System.IO.FileStream sourceFileStream)
        {
            try
            {
                using (HashAlgorithm md5 = HashAlgorithm.Create("MD5"))
                {
                    byte[] hashBytes = md5.ComputeHash(sourceFileStream);
                    sourceFileStream.Close();
                    return Convert.ToBase64String(hashBytes);
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region RSA签名与验证


        /**************************************************************************************
         * 
         * 签名过程：
         *      发送报文时，发送方用一个哈希函数从报文文本中生成报文摘要,
         *      然后用自己的私人密钥对这个摘要进行加密，
         *      这个加密后的摘要将作为报文的数字签名和报文一起发送给接收方，
         *      
         *************************************************************************************/

        /// <summary>
        /// RSA签名。
        /// 使用私钥对指定的字符串进行签名.
        /// 成功返回签名后的结果,失败返回null.
        /// </summary>
        /// <param name="xmlPrivateKey">签名所使用的私钥</param>
        /// <param name="strToSignature">待签名的字符串</param>
        public static string SignatureFormatter(string xmlPrivateKey, string strToSignature)
        {
            try
            {
                //获取待签名字符串的数字摘要
                byte[] strDigestBytes = Convert.FromBase64String(Hash(strToSignature));

                //加密数字摘要
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPrivateKey);

                    RSAPKCS1SignatureFormatter rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);
                    rsaFormatter.SetHashAlgorithm("MD5");
                    byte[] resultBytes = rsaFormatter.CreateSignature(strDigestBytes);   //执行签名
                    return Convert.ToBase64String(resultBytes);
                }
            }
            catch
            {
                return null;
            }
        }





        /*************************************************************************************
        * 
        * 验证过程：
        *      接收方首先用与发送方一样的哈希函数从接收到的原始报文中计算出报文摘要，
        *      接着再用发送方的公用密钥来对报文附加的数字签名进行解密，
        *      如果这两个摘要相同、那么接收方就能确认该数字签名是发送方的。
        *    
        **************************************************************************************/

        /// <summary>
        /// RSA签名验证
        /// 使用公钥对签名进行解密，并将所得的结果与需要验证的字符串进行对比验证。
        /// 如果相同返回 true,否则返回 false.
        /// </summary>
        /// <param name="xmlPublicKey">解密签名所使用的公钥</param>
        /// <param name="signature">待解密的签名(注册码)</param>
        /// <param name="targetStr">待验证的字符串(机器码)</param>
        /// <returns></returns>
        public static bool SignatureDeformatter(string xmlPublicKey, string signature, string targetStr)
        {
            try
            {
                //待验证字符串的数字摘要与待解密的签名数字
                byte[] targetDigestBytes = Convert.FromBase64String(Hash(targetStr));
                byte[] signatureBytes = Convert.FromBase64String(signature);
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    rsa.FromXmlString(xmlPublicKey);

                    RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                    rsaDeformatter.SetHashAlgorithm("MD5");

                    if (rsaDeformatter.VerifySignature(targetDigestBytes, signatureBytes))  //执行签名验证
                        return true;
                    else
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }


        #endregion
    }
}
