using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[Serializable]
public class dfAnchorMargins
{
	// Token: 0x0600061C RID: 1564 RVA: 0x0001CD80 File Offset: 0x0001AF80
	public override string ToString()
	{
		return string.Format("[L:{0},T:{1},R:{2},B:{3}]", new object[]
		{
			this.left,
			this.top,
			this.right,
			this.bottom
		});
	}

	// Token: 0x0400025F RID: 607
	[SerializeField]
	public float left;

	// Token: 0x04000260 RID: 608
	[SerializeField]
	public float top;

	// Token: 0x04000261 RID: 609
	[SerializeField]
	public float right;

	// Token: 0x04000262 RID: 610
	[SerializeField]
	public float bottom;
}
