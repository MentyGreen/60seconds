using System;

// Token: 0x0200009A RID: 154
[dfMarkupTagInfo("br")]
public class dfMarkupTagBr : dfMarkupTag
{
	// Token: 0x060008F5 RID: 2293 RVA: 0x00027E6F File Offset: 0x0002606F
	public dfMarkupTagBr() : base("br")
	{
		this.IsClosedTag = true;
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00027E83 File Offset: 0x00026083
	public dfMarkupTagBr(dfMarkupTag original) : base(original)
	{
		this.IsClosedTag = true;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00027E93 File Offset: 0x00026093
	protected override void _PerformLayoutImpl(dfMarkupBox container, dfMarkupStyle style)
	{
		container.AddLineBreak();
	}
}
