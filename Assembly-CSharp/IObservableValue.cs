using System;

// Token: 0x0200009F RID: 159
public interface IObservableValue
{
	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000957 RID: 2391
	object Value { get; }

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000958 RID: 2392
	bool HasChanged { get; }
}
