using System;
using System.Security.Cryptography;
using System.Text;

namespace HelperClass.Cryptogram
{
	// Token: 0x0200000A RID: 10
	public static class HelperMd5
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003C20 File Offset: 0x00001E20
		public static string Md5(string value)
		{
			string text = string.Empty;
			bool flag = string.IsNullOrEmpty(value);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				using (MD5 md = MD5.Create())
				{
					text = HelperMd5.GetMd5Hash(md, value);
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003C74 File Offset: 0x00001E74
		private static string GetMd5Hash(MD5 md5Hash, string input)
		{
			byte[] array = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003CD4 File Offset: 0x00001ED4
		private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
		{
			string md5Hash2 = HelperMd5.GetMd5Hash(md5Hash, input);
			StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
			return ordinalIgnoreCase.Compare(md5Hash2, hash) == 0;
		}
	}
}
