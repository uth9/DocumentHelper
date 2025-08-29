using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace DocumentHelper
{
    
    public class Student : INotifyPropertyChanged
    {
        
        private string _StudentName = "";
        public string StudentName
        {
            get => this._StudentName;
            set
            {
                this._StudentName = value;
                this.onPropertyChanged(nameof(this.StudentName));
            }
        }
        private string _StudentNation = "汉族";
        public string StudentNation
        {
            get => this._StudentNation;
            set
            {
                this._StudentNation = value;
                this.onPropertyChanged(nameof(this.StudentNation));
            }
        }
        private string _Pin = "";
        public string Pin
        {
            get => _Pin;
            set
            {
                this._Pin = value;
                this.onPropertyChanged(nameof(this.Pin));
            }
        }
        private string _ReconfirmedPin = "";
        public string ReconfirmedPin
        {
            get => this._ReconfirmedPin;
            set
            {
                this._ReconfirmedPin = value;
                this.onPropertyChanged(nameof(this.ReconfirmedPin));
            }
        }
        private string _MemberId = "";
        public string MemberId
        {
            get => this._MemberId;
            set
            {
                this._MemberId = value;
                this.onPropertyChanged(nameof(this.MemberId));
            }
        }
        private string[] _RegDate = ["2025", "01"];
        public string RegDate
        {
            get => string.Concat(this._RegDate[0], "/", this._RegDate[1]);
            set { _RegDate = value.Split('/'); }
        }
        public string RegYear
        {
            get => this._RegDate[0];
            set
            {
                this._RegDate[0] = value;
                this.onPropertyChanged(nameof(this.RegYear));
                this.onPropertyChanged(nameof(this.RegDate));
            }
        }
        public string RegMonth
        {
            get => this._RegDate[1];
            set
            {
                this._RegDate[1] = value;
                this.onPropertyChanged(nameof(this.RegMonth));
                this.onPropertyChanged(nameof(this.RegDate));
            }
        }
        private string _Tel = "";
        public string Tel
        {
            get => this._Tel;
            set
            {
                this._Tel = value;
                this.onPropertyChanged(nameof(this.Tel));
            }
        }
        private string _Address = "";
        public string Address
        {
            get => this._Address;
            set
            {
                this._Address = value;
                this.onPropertyChanged(nameof(this.Address));
            }
        }
        private bool _VolunteerState = true;
        public bool VolunteerState
        {
            get => this._VolunteerState;
            set
            {
                this._VolunteerState = value;
                this.onPropertyChanged(nameof(VolunteerState));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void onPropertyChanged(string propertyName)
        {
            if (PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        /// <summary>
        /// 加解密部分
        /// </summary>
        private const int KeySize = 256;
        private const int BlockSize = 128;
        private const int DerivationIterations = 1000;
        private const int SaltSize = 16; // 128位盐值

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="plainText">要加密的明文</param>
        /// <param name="key">加密密钥</param>
        /// <returns>Base64编码的加密结果</returns>
        public static string EncryptString(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            try
            {
                // 生成随机盐值
                var salt = GenerateRandomBytes(SaltSize);
                var iv = GenerateRandomBytes(16); // AES块大小是128位(16字节)

                using (var derivedKey = new Rfc2898DeriveBytes(key, salt, DerivationIterations, HashAlgorithmName.SHA256))
                {
                    var keyBytes = derivedKey.GetBytes(KeySize / 8);

                    using (var aes = Aes.Create())
                    {
                        aes.Key = keyBytes;
                        aes.IV = iv;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (var memoryStream = new MemoryStream())
                        {
                            memoryStream.Write(salt, 0, salt.Length);
                            memoryStream.Write(iv, 0, iv.Length);

                            using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                return Convert.ToBase64String(memoryStream.ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException("加密过程中发生错误", ex);
            }
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="cipherText">Base64编码的密文</param>
        /// <param name="key">解密密钥</param>
        /// <returns>解密后的明文</returns>
        private string DecryptString(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException(nameof(cipherText));
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                // 提取盐值和IV
                var salt = new byte[SaltSize];
                var iv = new byte[16];
                Buffer.BlockCopy(fullCipher, 0, salt, 0, SaltSize);
                Buffer.BlockCopy(fullCipher, SaltSize, iv, 0, 16);

                var cipherBytes = new byte[fullCipher.Length - SaltSize - 16];
                Buffer.BlockCopy(fullCipher, SaltSize + 16, cipherBytes, 0, cipherBytes.Length);

                using (var derivedKey = new Rfc2898DeriveBytes(key, salt, DerivationIterations, HashAlgorithmName.SHA256))
                {
                    var keyBytes = derivedKey.GetBytes(KeySize / 8);

                    using (var aes = Aes.Create())
                    {
                        aes.Key = keyBytes;
                        aes.IV = iv;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (var memoryStream = new MemoryStream(cipherBytes))
                        using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("密文格式无效", nameof(cipherText), ex);
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException("解密过程中发生错误，可能是密钥不正确", ex);
            }
            catch (Exception ex)
            {
                throw new CryptographicException("解密过程中发生未知错误", ex);
            }
        }

        /// <summary>
        /// 生成随机字节数组
        /// </summary>
        private static byte[] GenerateRandomBytes(int length)
        {
            var randomBytes = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}

