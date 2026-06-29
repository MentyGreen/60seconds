using System;
using I2.Loc;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

// Token: 0x02000146 RID: 326
[RequireComponent(typeof(TextMeshProUGUI))]
public class MenuStatEntryController : MonoBehaviour
{
	// Token: 0x06000FA1 RID: 4001 RVA: 0x00040FB0 File Offset: 0x0003F1B0
	private void Awake()
	{
		this._text = base.GetComponent<TextMeshProUGUI>();
		this._text.SetText(string.Format(this._term.ToString(), this.GetData()), true);
		LocalizationManager.OnLocalizeEvent += this.LanguageChanged;
	}

	// Token: 0x06000FA2 RID: 4002 RVA: 0x00041002 File Offset: 0x0003F202
	public void LanguageChanged()
	{
		this._text.SetText(string.Format(this._term.ToString(), this.GetData()), true);
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0004102C File Offset: 0x0003F22C
	private object[] GetData()
	{
		if (this._parameter0 != null && this._parameter1 == null)
		{
			return new object[]
			{
				this._parameter0.Value
			};
		}
		if (this._parameter1 != null && this._parameter0 != null)
		{
			return new object[]
			{
				this._parameter0.Value,
				this._parameter1.Value
			};
		}
		if (this._parameterFloat != null)
		{
			return new object[]
			{
				this._parameterFloat.Value
			};
		}
		Debug.LogError("No parameters set up in MenuStatEntryController!", this);
		return null;
	}

	// Token: 0x040009A0 RID: 2464
	private TextMeshProUGUI _text;

	// Token: 0x040009A1 RID: 2465
	[Tooltip("Fill this field if the stat is supposed to be a single or double int value.")]
	[SerializeField]
	private GlobalIntVariable _parameter0;

	// Token: 0x040009A2 RID: 2466
	[Tooltip("Fill this field if the stat is supposed to be a double int value.")]
	[SerializeField]
	private GlobalIntVariable _parameter1;

	// Token: 0x040009A3 RID: 2467
	[Tooltip("Fill this field if the stat is supposed to be a single float value.")]
	[SerializeField]
	private GlobalFloatVariable _parameterFloat;

	// Token: 0x040009A4 RID: 2468
	[SerializeField]
	private LocalizedString _term;
}
