using System;
using UnityEngine;

// Token: 0x02000031 RID: 49
public interface IInputAdapter
{
	// Token: 0x060005BF RID: 1471
	bool GetKeyDown(KeyCode key);

	// Token: 0x060005C0 RID: 1472
	bool GetKeyUp(KeyCode key);

	// Token: 0x060005C1 RID: 1473
	float GetAxis(string axisName);

	// Token: 0x060005C2 RID: 1474
	Vector2 GetMousePosition();

	// Token: 0x060005C3 RID: 1475
	bool GetMouseButton(int button);

	// Token: 0x060005C4 RID: 1476
	bool GetMouseButtonDown(int button);

	// Token: 0x060005C5 RID: 1477
	bool GetMouseButtonUp(int button);
}
