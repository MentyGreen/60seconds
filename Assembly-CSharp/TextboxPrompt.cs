using System;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[AddComponentMenu("Daikon Forge/Examples/General/Textbox Prompt")]
[ExecuteInEditMode]
public class TextboxPrompt : MonoBehaviour
{
	// Token: 0x06000A3B RID: 2619 RVA: 0x0002C904 File Offset: 0x0002AB04
	public void OnEnable()
	{
		this._textbox = base.GetComponent<dfTextbox>();
		if (string.IsNullOrEmpty(this._textbox.Text) || this._textbox.Text == this.promptText)
		{
			this._textbox.Text = this.promptText;
			this._textbox.TextColor = this.promptColor;
		}
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x0002C969 File Offset: 0x0002AB69
	public void OnDisable()
	{
		if (this._textbox != null && this._textbox.Text == this.promptText)
		{
			this._textbox.Text = "";
		}
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0002C9A1 File Offset: 0x0002ABA1
	public void OnEnterFocus(dfControl control, dfFocusEventArgs args)
	{
		if (this._textbox.Text == this.promptText)
		{
			this._textbox.Text = "";
		}
		this._textbox.TextColor = this.textColor;
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0002C9DC File Offset: 0x0002ABDC
	public void OnLeaveFocus(dfControl control, dfFocusEventArgs args)
	{
		if (string.IsNullOrEmpty(this._textbox.Text))
		{
			this._textbox.Text = this.promptText;
			this._textbox.TextColor = this.promptColor;
		}
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0002CA12 File Offset: 0x0002AC12
	public void OnTextChanged(dfControl control, string value)
	{
		if (value != this.promptText)
		{
			this._textbox.TextColor = this.textColor;
		}
	}

	// Token: 0x040004DF RID: 1247
	public Color32 promptColor = Color.gray;

	// Token: 0x040004E0 RID: 1248
	public Color32 textColor = Color.white;

	// Token: 0x040004E1 RID: 1249
	public string promptText = "(enter some text)";

	// Token: 0x040004E2 RID: 1250
	private dfTextbox _textbox;
}
