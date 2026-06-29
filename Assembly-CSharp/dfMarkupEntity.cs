using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000084 RID: 132
public class dfMarkupEntity
{
	// Token: 0x0600087E RID: 2174 RVA: 0x00025D3C File Offset: 0x00023F3C
	public dfMarkupEntity(string entityName, string entityChar)
	{
		this.EntityName = entityName;
		this.EntityChar = entityChar;
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x00025D54 File Offset: 0x00023F54
	public static string Replace(string text)
	{
		dfMarkupEntity.buffer.EnsureCapacity(text.Length);
		dfMarkupEntity.buffer.Length = 0;
		dfMarkupEntity.buffer.Append(text);
		for (int i = 0; i < dfMarkupEntity.HTML_ENTITIES.Count; i++)
		{
			dfMarkupEntity dfMarkupEntity = dfMarkupEntity.HTML_ENTITIES[i];
			dfMarkupEntity.buffer.Replace(dfMarkupEntity.EntityName, dfMarkupEntity.EntityChar);
		}
		return dfMarkupEntity.buffer.ToString();
	}

	// Token: 0x04000413 RID: 1043
	private static List<dfMarkupEntity> HTML_ENTITIES = new List<dfMarkupEntity>
	{
		new dfMarkupEntity("&nbsp;", " "),
		new dfMarkupEntity("&quot;", "\""),
		new dfMarkupEntity("&amp;", "&"),
		new dfMarkupEntity("&lt;", "<"),
		new dfMarkupEntity("&gt;", ">"),
		new dfMarkupEntity("&#39;", "'"),
		new dfMarkupEntity("&trade;", "™"),
		new dfMarkupEntity("&copy;", "©"),
		new dfMarkupEntity("\u00a0", " ")
	};

	// Token: 0x04000414 RID: 1044
	private static StringBuilder buffer = new StringBuilder();

	// Token: 0x04000415 RID: 1045
	public string EntityName;

	// Token: 0x04000416 RID: 1046
	public string EntityChar;
}
