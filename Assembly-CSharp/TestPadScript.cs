using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class TestPadScript : MonoBehaviour
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public void Show()
	{
		this._stateVisible = !this._stateVisible;
		if (this._stateVisible)
		{
			this._visible.Hide();
			this._hide.Show();
			return;
		}
		this._hide.Hide();
		this._visible.Show();
	}

	// Token: 0x04000001 RID: 1
	[SerializeField]
	private GamepadPanelCloseable _visible;

	// Token: 0x04000002 RID: 2
	[SerializeField]
	private GamepadPanelCloseable _hide;

	// Token: 0x04000003 RID: 3
	private bool _stateVisible;
}
