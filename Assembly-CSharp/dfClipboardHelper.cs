using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class dfClipboardHelper
{
	// Token: 0x06000819 RID: 2073 RVA: 0x000238DC File Offset: 0x00021ADC
	private static PropertyInfo GetSystemCopyBufferProperty()
	{
		if (dfClipboardHelper.m_systemCopyBufferProperty == null)
		{
			dfClipboardHelper.m_systemCopyBufferProperty = typeof(GUIUtility).GetProperty("systemCopyBuffer", BindingFlags.Static | BindingFlags.NonPublic);
			if (dfClipboardHelper.m_systemCopyBufferProperty == null)
			{
				throw new Exception("Can't access internal member 'GUIUtility.systemCopyBuffer' it may have been removed / renamed");
			}
		}
		return dfClipboardHelper.m_systemCopyBufferProperty;
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x0600081A RID: 2074 RVA: 0x00023930 File Offset: 0x00021B30
	// (set) Token: 0x0600081B RID: 2075 RVA: 0x0002396C File Offset: 0x00021B6C
	public static string clipBoard
	{
		get
		{
			string result;
			try
			{
				result = (string)dfClipboardHelper.GetSystemCopyBufferProperty().GetValue(null, null);
			}
			catch
			{
				result = "";
			}
			return result;
		}
		set
		{
			try
			{
				dfClipboardHelper.GetSystemCopyBufferProperty().SetValue(null, value, null);
			}
			catch
			{
			}
		}
	}

	// Token: 0x040003DB RID: 987
	private static PropertyInfo m_systemCopyBufferProperty;
}
