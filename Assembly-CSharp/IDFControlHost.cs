using System;
using UnityEngine;

// Token: 0x0200001F RID: 31
public interface IDFControlHost
{
	// Token: 0x060004FB RID: 1275
	T AddControl<T>() where T : dfControl;

	// Token: 0x060004FC RID: 1276
	dfControl AddControl(Type controlType);

	// Token: 0x060004FD RID: 1277
	void AddControl(dfControl child);

	// Token: 0x060004FE RID: 1278
	dfControl AddPrefab(GameObject prefab);
}
