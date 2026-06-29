using System;
using I2.Loc;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000321 RID: 801
	public class FunctionTextController : MonoBehaviour
	{
		// Token: 0x06001B03 RID: 6915 RVA: 0x0007482F File Offset: 0x00072A2F
		private void Start()
		{
			if (this._refreshOnStart)
			{
				this.RefreshText();
			}
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00074840 File Offset: 0x00072A40
		public void RefreshText()
		{
			if (this._function == null)
			{
				return;
			}
			this._function.Execute(null);
			LocalizedString localizedString = (LocalizedString)this._function.GetOutputValue("Output");
			this._textField.text = localizedString.ToString();
		}

		// Token: 0x040014BA RID: 5306
		[SerializeField]
		private NodeFunction _function;

		// Token: 0x040014BB RID: 5307
		[SerializeField]
		private TextMeshProUGUI _textField;

		// Token: 0x040014BC RID: 5308
		[SerializeField]
		private bool _refreshOnStart;

		// Token: 0x040014BD RID: 5309
		private const string FUNCTION_OUTPUT_NAME = "Output";
	}
}
