using System;
using System.IO;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000184 RID: 388
	public static class IO
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x00048780 File Offset: 0x00046980
		public static string GetFileName(string absolutPath)
		{
			return Path.GetFileName(absolutPath);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00048788 File Offset: 0x00046988
		public static string GetFileNameWithoutExtension(string absolutPath)
		{
			return Path.GetFileNameWithoutExtension(absolutPath);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00048790 File Offset: 0x00046990
		public static string GetExtension(string absolutPath)
		{
			return Path.GetExtension(absolutPath).ToLower();
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x000487A0 File Offset: 0x000469A0
		public static string ReadTextFile(string absolutPath)
		{
			if (File.Exists(absolutPath))
			{
				StreamReader streamReader = new StreamReader(absolutPath);
				string result = streamReader.ReadToEnd();
				streamReader.Close();
				return result;
			}
			return null;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000487CA File Offset: 0x000469CA
		public static void WriteTextFile(string absolutPath, string content)
		{
			StreamWriter streamWriter = new StreamWriter(absolutPath);
			streamWriter.Write(content);
			streamWriter.Close();
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000487DE File Offset: 0x000469DE
		public static bool CopyFile(string src, string dst, bool overwrite)
		{
			if (File.Exists(src) && (!File.Exists(dst) || overwrite))
			{
				File.Copy(src, dst, overwrite);
				return true;
			}
			return false;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00048800 File Offset: 0x00046A00
		public static string ConvertFileSeparators(string path)
		{
			return path.Replace("\\", "/");
		}
	}
}
