using System;

// Token: 0x0200009E RID: 158
public interface IDataBindingComponent
{
	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000954 RID: 2388
	bool IsBound { get; }

	// Token: 0x06000955 RID: 2389
	void Bind();

	// Token: 0x06000956 RID: 2390
	void Unbind();
}
