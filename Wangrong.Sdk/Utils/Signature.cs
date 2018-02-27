using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Wangrong.Sdk.Utils
{
    //public class SignatureObject
    //{
    //    public static string GetSignatureString(RequestBase trans, WangrongGatewayProtocol _gatewayProtocol)
    //    {
    //        trans.MerNo = _gatewayProtocol.MerchantNumber;
    //        trans.AgentId = _gatewayProtocol.AgentId;

    //        var parameters = trans.ToDictionary();

    //        var sorList = new List<string>(parameters.Keys);
    //        sorList.Sort();
    //        var result = new Dictionary<string, string>();
    //        foreach (var key in sorList)
    //        {
    //            var v = parameters[key];
    //            if (!String.IsNullOrEmpty(v))
    //                result.Add(key, v.Trim());
    //        }

    //    }
    //}
    public class Signature
    {
        private readonly RSA _privateKey;
        //private readonly RSA _publicKey;
        public Signature() { }
        public Signature(string filePath, string password)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("找不到证书文件", filePath);
            var certificate = new X509Certificate2(filePath, password, X509KeyStorageFlags.Exportable);

            _privateKey = certificate.GetRSAPrivateKey();
            //_publicKey = certificate.GetRSAPublicKey();
        }

        //public Signature(FileInfo privateKeyPemFilePath, FileInfo publicKeyPemFilePath)
        //{
        //    var priStr = "";
        //    using (var reaeder = new StreamReader(privateKeyPemFilePath.OpenRead()))
        //    {
        //        priStr = reaeder.ReadToEnd();
        //    }
        //    var c = Helpers.GetBytesFromPEM(priStr, PemStringType.RsaPrivateKey);
        //    var certificate = new X509Certificate2(c);
        //    _privateKey = certificate.GetRSAPrivateKey();
        //}

        public string PrivateSignData(IDictionary<string, string> dict)
        {
            var str = HttpRequestHelper.GetSignStr(dict);
            return PrivateSignData(str);
        }

        //public string PublicSignData(IDictionary<string, string> dict)
        //{
        //    var str = HttpRequestHelper.GetSignStr(dict);
        //    return publicSignData(str);
        //}

        //public object PrivateKeyDecrypt(string priKey)
        //{
        //    var bytes = Convert.FromBase64String(priKey);
        //    var c = _privateKey.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
        //    return Encoding.UTF8.GetString(c);
        //}

        //public string PublicKeyEncrypt(string data)
        //{
        //    var bytes = Encoding.UTF8.GetBytes(data);
        //    var encryptData = _publicKey.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
        //    return Convert.ToBase64String(encryptData);
        //}

        //public string PublicKeyDecrypt(string data)
        //{
        //    var bytes = Convert.FromBase64String(data);
        //    var c = _publicKey.Decrypt(bytes, RSAEncryptionPadding.Pkcs1);
        //    return Encoding.UTF8.GetString(c);
        //}

        //public string PrivateKeyEncrypt(string data)
        //{
        //    var bytes = Encoding.UTF8.GetBytes(data);
        //    var f = _privateKey.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
        //    return Convert.ToBase64String(f);
        //}

        public string PrivateSignData(string strEncryptString)
        {
            var bytes = Encoding.UTF8.GetBytes(strEncryptString);
            var provider = new RSACryptoServiceProvider();
            provider.ImportParameters(_privateKey.ExportParameters(true));
            var c = provider.SignData(bytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(c);
        }
        //public string publicSignData(string strEncryptString)
        //{
        //    var bytes = Encoding.UTF8.GetBytes(strEncryptString);
        //    var provider = new RSACryptoServiceProvider();
        //    provider.ImportParameters(_publicKey.ExportParameters(true));
        //    var c = provider.SignData(bytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        //    return Convert.ToBase64String(c);
        //}

        //public bool VerifySigntrue(string data, string hash)
        //{
        //    var bytes = Encoding.UTF8.GetBytes(data);
        //    var signture = Convert.FromBase64String(hash);
        //    return _publicKey.VerifyData(bytes, signture, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

        //    //var c = _publicKey.SignData(bytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        //    //return Convert.ToBase64String(c);
        //}

        //public bool VerifySigntureByPrivateKey(string data, string hash)
        //{
        //    var bytes = Encoding.UTF8.GetBytes(data);
        //    var signture = Convert.FromBase64String(hash);
        //    return _privateKey.VerifyData(bytes, signture, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);

        //    //var c = _publicKey.SignData(bytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        //    //return Convert.ToBase64String(c);
        //}

        //public string PublicKeyDecrypt(string strDecryptString)

        //public string PrivateKeyDecrypt(string strDecryptString)
        //{
        //    var rgb = Convert.FromBase64String(strDecryptString);
        //    var bytes = _privateKey.Decrypt(rgb, RSAEncryptionPadding.Pkcs1);
        //    return new UnicodeEncoding().GetString(bytes);

        //}
        //{
        //    var rgb = Convert.FromBase64String(strDecryptString);
        //    var bytes = _publicKey.Decrypt(rgb, RSAEncryptionPadding.Pkcs1);
        //    return new UnicodeEncoding().GetString(bytes);
        //}
    }
}