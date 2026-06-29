using System;

// Token: 0x02000078 RID: 120
public static class dfStringExtensions
{
	// Token: 0x0600081E RID: 2078 RVA: 0x000239A6 File Offset: 0x00021BA6
	public static string MakeRelativePath(this string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			return "";
		}
		return path.Substring(path.IndexOf("Assets/", StringComparison.OrdinalIgnoreCase));
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x000239C8 File Offset: 0x00021BC8
	public static bool Contains(this string value, string pattern, bool caseInsensitive)
	{
		if (caseInsensitive)
		{
			return value.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) != -1;
		}
		return value.IndexOf(pattern) != -1;
	}
}
