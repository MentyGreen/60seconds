using System;
using RG.Core.GameVersion;
using TMPro;
using UnityEngine;

// Token: 0x02000145 RID: 325
public class VersionSettingController : MonoBehaviour
{
	// Token: 0x06000F9E RID: 3998 RVA: 0x00040EEE File Offset: 0x0003F0EE
	private void OnEnable()
	{
		this._valueField.text = this.GetVersionString(this._versionData);
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x00040F08 File Offset: 0x0003F108
	private string GetVersionString(GameVersion gv)
	{
		if (gv != null)
		{
			int num = (gv.Major > 0) ? gv.Major : 0;
			int num2 = (gv.Minor > 0) ? gv.Minor : 0;
			int num3 = (gv.Build > 0) ? gv.Build : 0;
			int num4 = (gv.Patch > 0) ? gv.Patch : 0;
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				num,
				num2,
				num4,
				num3
			});
		}
		return string.Empty;
	}

	// Token: 0x0400099E RID: 2462
	[SerializeField]
	private TextMeshProUGUI _valueField;

	// Token: 0x0400099F RID: 2463
	[SerializeField]
	private GameVersion _versionData;
}
