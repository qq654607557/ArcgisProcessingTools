using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace HelperClass.LocalFile
{
	// Token: 0x02000006 RID: 6
	public static class HelperINI
	{
		// Token: 0x0600000E RID: 14
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

		// Token: 0x06000012 RID: 18
		[DllImport("KERNEL32.DLL ", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

		// Token: 0x06000013 RID: 19 RVA: 0x00002A59 File Offset: 0x00000C59
		public static void Write(string Section, string Key, string Value, string path)
		{
			HelperINI.WritePrivateProfileString(Section, Key, Value, path);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A68 File Offset: 0x00000C68
		public static string Read(string Section, string Key, string path)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			int privateProfileString = HelperINI.GetPrivateProfileString(Section, Key, "", stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public static int GetAllSectionNames(out string[] sections, string path)
		{
			int num = 32767;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			int privateProfileSectionNames = HelperINI.GetPrivateProfileSectionNames(intPtr, num, path);
			bool flag = privateProfileSectionNames == 0;
			int result;
			if (flag)
			{
				sections = null;
				result = -1;
			}
			else
			{
				string text = Marshal.PtrToStringAnsi(intPtr, privateProfileSectionNames).ToString();
				Marshal.FreeCoTaskMem(intPtr);
				sections = text.Substring(0, text.Length - 1).Split(new char[1]);
				result = 0;
			}
			return result;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B10 File Offset: 0x00000D10
		public static List<string> GetAllSectionNames(string path)
		{
			List<string> list = new List<string>();
			int num = 32767;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num);
			int privateProfileSectionNames = HelperINI.GetPrivateProfileSectionNames(intPtr, num, path);
			bool flag = privateProfileSectionNames != 0;
			if (flag)
			{
				string text = Marshal.PtrToStringAnsi(intPtr, privateProfileSectionNames).ToString();
				Marshal.FreeCoTaskMem(intPtr);
				list.AddRange(text.Substring(0, text.Length - 1).Split(new char[1]));
			}
			return list;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B88 File Offset: 0x00000D88
		public static int GetAllKeyValues(string section, out string[] keys, out string[] values, string path)
		{
			byte[] array = new byte[65535];
			HelperINI.GetPrivateProfileSection(section, array, array.Length, path);
			string @string = Encoding.Default.GetString(array);
			string[] array2 = @string.Split(new char[1]);
			List<string> list = new List<string>();
			foreach (string text in array2)
			{
				bool flag = text != string.Empty;
				if (flag)
				{
					list.Add(text);
				}
			}
			keys = new string[list.Count];
			values = new string[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				string[] array4 = list[j].Split(new char[]
				{
					'='
				});
				bool flag2 = array4.Length > 2;
				if (flag2)
				{
					keys[j] = array4[0].Trim();
					values[j] = list[j].Substring(keys[j].Length + 1);
				}
				bool flag3 = array4.Length == 2;
				if (flag3)
				{
					keys[j] = array4[0].Trim();
					values[j] = array4[1].Trim();
				}
				else
				{
					bool flag4 = array4.Length == 1;
					if (flag4)
					{
						keys[j] = array4[0].Trim();
						values[j] = "";
					}
					else
					{
						bool flag5 = array4.Length == 0;
						if (flag5)
						{
							keys[j] = "";
							values[j] = "";
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D18 File Offset: 0x00000F18
		public static int GetAllKeys(string section, out string[] keys, string path)
		{
			byte[] array = new byte[65535];
			HelperINI.GetPrivateProfileSection(section, array, array.Length, path);
			string @string = Encoding.Default.GetString(array);
			string[] array2 = @string.Split(new char[1]);
			ArrayList arrayList = new ArrayList();
			foreach (string text in array2)
			{
				bool flag = text != string.Empty;
				if (flag)
				{
					arrayList.Add(text);
				}
			}
			keys = new string[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				string[] array4 = arrayList[j].ToString().Split(new char[]
				{
					'='
				});
				bool flag2 = array4.Length == 2;
				if (flag2)
				{
					keys[j] = array4[0].Trim();
				}
				else
				{
					bool flag3 = array4.Length == 1;
					if (flag3)
					{
						keys[j] = array4[0].Trim();
					}
					else
					{
						bool flag4 = array4.Length == 0;
						if (flag4)
						{
							keys[j] = "";
						}
					}
				}
			}
			return 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E40 File Offset: 0x00001040
		public static List<string> GetAllKeys(string section, string path)
		{
			List<string> list = new List<string>();
			byte[] array = new byte[65535];
			HelperINI.GetPrivateProfileSection(section, array, array.Length, path);
			string @string = Encoding.Default.GetString(array);
			string[] array2 = @string.Split(new char[1]);
			List<string> list2 = new List<string>();
			foreach (string text in array2)
			{
				bool flag = text != string.Empty;
				if (flag)
				{
					list2.Add(text);
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				string[] array4 = list2[j].Split(new char[]
				{
					'='
				});
				bool flag2 = array4.Length == 2 || array4.Length == 1;
				if (flag2)
				{
					list.Add(array4[0].Trim());
				}
				else
				{
					bool flag3 = array4.Length == 0;
					if (flag3)
					{
						list.Add(string.Empty);
					}
				}
			}
			return list;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002F4C File Offset: 0x0000114C
		public static List<string> GetAllValues(string section, string path)
		{
			List<string> list = new List<string>();
			byte[] array = new byte[65535];
			HelperINI.GetPrivateProfileSection(section, array, array.Length, path);
			string @string = Encoding.Default.GetString(array);
			string[] array2 = @string.Split(new char[1]);
			List<string> list2 = new List<string>();
			foreach (string text in array2)
			{
				bool flag = text != string.Empty;
				if (flag)
				{
					list2.Add(text);
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				string[] array4 = list2[j].Split(new char[]
				{
					'='
				});
				bool flag2 = array4.Length == 2 || array4.Length == 1;
				if (flag2)
				{
					list.Add(array4[1].Trim());
				}
				else
				{
					bool flag3 = array4.Length == 0;
					if (flag3)
					{
						list.Add(string.Empty);
					}
				}
			}
			return list;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003058 File Offset: 0x00001258
		public static string GetFirstKeyByValue(string section, string path, string value)
		{
			foreach (string text in HelperINI.GetAllKeys(section, path))
			{
				bool flag = HelperINI.ReadString(section, text, "", path) == value;
				if (flag)
				{
					return text;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000030D0 File Offset: 0x000012D0
		public static List<string> GetKeysByValue(string section, string path, string value)
		{
			List<string> list = new List<string>();
			foreach (string text in HelperINI.GetAllKeys(section, path))
			{
				bool flag = HelperINI.ReadString(section, text, "", path) == value;
				if (flag)
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003150 File Offset: 0x00001350
		public static string ReadString(string sectionName, string keyName, string defaultValue, string path)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			HelperINI.GetPrivateProfileString(sectionName, keyName, defaultValue, stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002A59 File Offset: 0x00000C59
		public static void WriteString(string sectionName, string keyName, string value, string path)
		{
			HelperINI.WritePrivateProfileString(sectionName, keyName, value, path);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003184 File Offset: 0x00001384
		public static int ReadInteger(string sectionName, string keyName, int defaultValue, string path)
		{
			return HelperINI.GetPrivateProfileInt(sectionName, keyName, defaultValue, path);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000319F File Offset: 0x0000139F
		public static void WriteInteger(string sectionName, string keyName, int value, string path)
		{
			HelperINI.WritePrivateProfileString(sectionName, keyName, value.ToString(), path);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000031B4 File Offset: 0x000013B4
		public static bool ReadBoolean(string sectionName, string keyName, bool defaultValue, string path)
		{
			int nDefault = defaultValue ? 1 : 0;
			return HelperINI.GetPrivateProfileInt(sectionName, keyName, nDefault, path) != 0;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000031E0 File Offset: 0x000013E0
		public static void WriteBoolean(string sectionName, string keyName, bool value, string path)
		{
			string val = value ? "1 " : "0 ";
			HelperINI.WritePrivateProfileString(sectionName, keyName, val, path);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003208 File Offset: 0x00001408
		public static void DeleteKey(string sectionName, string keyName, string path)
		{
			HelperINI.WritePrivateProfileString(sectionName, keyName, null, path);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003215 File Offset: 0x00001415
		public static void EraseSection(string sectionName, string path)
		{
			HelperINI.WritePrivateProfileString(sectionName, null, null, path);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003224 File Offset: 0x00001424
		public static bool ExistSection(string section, string fileName)
		{
			string[] array = null;
			HelperINI.GetAllSectionNames(out array, fileName);
			bool flag = array != null;
			if (flag)
			{
				foreach (string a in array)
				{
					bool flag2 = a == section;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000327C File Offset: 0x0000147C
		public static bool ExistKey(string section, string key, string fileName)
		{
			string[] array = null;
			HelperINI.GetAllKeys(section, out array, fileName);
			bool flag = array != null;
			if (flag)
			{
				foreach (string a in array)
				{
					bool flag2 = a == key;
					if (flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		public static bool AddSectionWithKeyValues(string section, List<string> keyList, List<string> valueList, string path)
		{
			bool result = true;
			for (int i = 0; i < keyList.Count; i++)
			{
				HelperINI.WriteString(section, keyList[i], valueList[i], path);
			}
			return result;
		}
	}
}
