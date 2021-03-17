using System;
using System.Security.Cryptography;
using System.Text;

namespace HelperClass.Cryptogram
{
	// 
	public static class HelperAES
	{
		// 
		public static string Encrypt(string str)
		{
			return HelperAES.Encrypt(str, HelperAES.PublicKey);
		}

		// 
		public static string Decrypt(string str)
		{
			return HelperAES.Decrypt(str, HelperAES.PublicKey);
		}

		// Token: 
		public static string Encrypt(string str, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			byte[] bytes2 = Encoding.UTF8.GetBytes(str);
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = bytes,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7,
				IV = Encoding.UTF8.GetBytes(HelperAES.Iv)
			}.CreateEncryptor();
			byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
			return Convert.ToBase64String(array, 0, array.Length);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003B8C File Offset: 0x00001D8C
		public static string Decrypt(string str, string key)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			byte[] array = Convert.FromBase64String(str);
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = bytes,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7,
				IV = Encoding.UTF8.GetBytes(HelperAES.Iv)
			}.CreateDecryptor();
			if (array.Length == 0) return "";
			byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			return Encoding.UTF8.GetString(bytes2);
		}

		// 
		public static string PublicKey = "aqw&$5#DB7bw1rH1";

		// Token: 0x04000002 RID: 2
		public static string Iv = "VVuzmT3*7%]4jPCh";
	}
}
