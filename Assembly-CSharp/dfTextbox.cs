using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

// Token: 0x0200001B RID: 27
[dfCategory("Basic Controls")]
[dfTooltip("Implements a text entry control")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_textbox.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Textbox")]
[Serializable]
public class dfTextbox : dfInteractiveBase, IDFMultiRender, IRendersText
{
	// Token: 0x14000037 RID: 55
	// (add) Token: 0x06000447 RID: 1095 RVA: 0x000158B0 File Offset: 0x00013AB0
	// (remove) Token: 0x06000448 RID: 1096 RVA: 0x000158E8 File Offset: 0x00013AE8
	public event PropertyChangedEventHandler<bool> ReadOnlyChanged;

	// Token: 0x14000038 RID: 56
	// (add) Token: 0x06000449 RID: 1097 RVA: 0x00015920 File Offset: 0x00013B20
	// (remove) Token: 0x0600044A RID: 1098 RVA: 0x00015958 File Offset: 0x00013B58
	public event PropertyChangedEventHandler<string> PasswordCharacterChanged;

	// Token: 0x14000039 RID: 57
	// (add) Token: 0x0600044B RID: 1099 RVA: 0x00015990 File Offset: 0x00013B90
	// (remove) Token: 0x0600044C RID: 1100 RVA: 0x000159C8 File Offset: 0x00013BC8
	public event PropertyChangedEventHandler<string> TextChanged;

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x0600044D RID: 1101 RVA: 0x00015A00 File Offset: 0x00013C00
	// (remove) Token: 0x0600044E RID: 1102 RVA: 0x00015A38 File Offset: 0x00013C38
	public event PropertyChangedEventHandler<string> TextSubmitted;

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x0600044F RID: 1103 RVA: 0x00015A70 File Offset: 0x00013C70
	// (remove) Token: 0x06000450 RID: 1104 RVA: 0x00015AA8 File Offset: 0x00013CA8
	public event PropertyChangedEventHandler<string> TextCancelled;

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000451 RID: 1105 RVA: 0x00015AE0 File Offset: 0x00013CE0
	// (set) Token: 0x06000452 RID: 1106 RVA: 0x00015B1D File Offset: 0x00013D1D
	public dfFontBase Font
	{
		get
		{
			if (this.font == null)
			{
				dfGUIManager manager = base.GetManager();
				if (manager != null)
				{
					this.font = manager.DefaultFont;
				}
			}
			return this.font;
		}
		set
		{
			if (value != this.font)
			{
				this.unbindTextureRebuildCallback();
				this.font = value;
				this.bindTextureRebuildCallback();
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06000453 RID: 1107 RVA: 0x00015B46 File Offset: 0x00013D46
	// (set) Token: 0x06000454 RID: 1108 RVA: 0x00015B50 File Offset: 0x00013D50
	public int SelectionStart
	{
		get
		{
			return this.selectionStart;
		}
		set
		{
			if (value != this.selectionStart)
			{
				this.selectionStart = Mathf.Max(0, Mathf.Min(value, this.text.Length));
				this.selectionEnd = Mathf.Max(this.selectionEnd, this.selectionStart);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06000455 RID: 1109 RVA: 0x00015BA0 File Offset: 0x00013DA0
	// (set) Token: 0x06000456 RID: 1110 RVA: 0x00015BA8 File Offset: 0x00013DA8
	public int SelectionEnd
	{
		get
		{
			return this.selectionEnd;
		}
		set
		{
			if (value != this.selectionEnd)
			{
				this.selectionEnd = Mathf.Max(0, Mathf.Min(value, this.text.Length));
				this.selectionStart = Mathf.Max(this.selectionStart, this.selectionEnd);
				this.Invalidate();
			}
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06000457 RID: 1111 RVA: 0x00015BF8 File Offset: 0x00013DF8
	public int SelectionLength
	{
		get
		{
			return this.selectionEnd - this.selectionStart;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06000458 RID: 1112 RVA: 0x00015C07 File Offset: 0x00013E07
	public string SelectedText
	{
		get
		{
			if (this.selectionEnd == this.selectionStart)
			{
				return "";
			}
			return this.text.Substring(this.selectionStart, this.selectionEnd - this.selectionStart);
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06000459 RID: 1113 RVA: 0x00015C3B File Offset: 0x00013E3B
	// (set) Token: 0x0600045A RID: 1114 RVA: 0x00015C43 File Offset: 0x00013E43
	public bool SelectOnFocus
	{
		get
		{
			return this.selectOnFocus;
		}
		set
		{
			this.selectOnFocus = value;
		}
	}

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x0600045B RID: 1115 RVA: 0x00015C4C File Offset: 0x00013E4C
	// (set) Token: 0x0600045C RID: 1116 RVA: 0x00015C67 File Offset: 0x00013E67
	public RectOffset Padding
	{
		get
		{
			if (this.padding == null)
			{
				this.padding = new RectOffset();
			}
			return this.padding;
		}
		set
		{
			value = value.ConstrainPadding();
			if (!object.Equals(value, this.padding))
			{
				this.padding = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x0600045D RID: 1117 RVA: 0x00015C8C File Offset: 0x00013E8C
	// (set) Token: 0x0600045E RID: 1118 RVA: 0x00015C94 File Offset: 0x00013E94
	public bool IsPasswordField
	{
		get
		{
			return this.displayAsPassword;
		}
		set
		{
			if (value != this.displayAsPassword)
			{
				this.displayAsPassword = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x0600045F RID: 1119 RVA: 0x00015CAC File Offset: 0x00013EAC
	// (set) Token: 0x06000460 RID: 1120 RVA: 0x00015CB4 File Offset: 0x00013EB4
	public string PasswordCharacter
	{
		get
		{
			return this.passwordChar;
		}
		set
		{
			if (!string.IsNullOrEmpty(value))
			{
				this.passwordChar = value[0].ToString();
			}
			else
			{
				this.passwordChar = value;
			}
			this.OnPasswordCharacterChanged();
			this.Invalidate();
		}
	}

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06000461 RID: 1121 RVA: 0x00015CF3 File Offset: 0x00013EF3
	// (set) Token: 0x06000462 RID: 1122 RVA: 0x00015CFB File Offset: 0x00013EFB
	public float CursorBlinkTime
	{
		get
		{
			return this.cursorBlinkTime;
		}
		set
		{
			this.cursorBlinkTime = value;
		}
	}

	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06000463 RID: 1123 RVA: 0x00015D04 File Offset: 0x00013F04
	// (set) Token: 0x06000464 RID: 1124 RVA: 0x00015D0C File Offset: 0x00013F0C
	public int CursorWidth
	{
		get
		{
			return this.cursorWidth;
		}
		set
		{
			this.cursorWidth = value;
		}
	}

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06000465 RID: 1125 RVA: 0x00015D15 File Offset: 0x00013F15
	// (set) Token: 0x06000466 RID: 1126 RVA: 0x00015D1D File Offset: 0x00013F1D
	public int CursorIndex
	{
		get
		{
			return this.cursorIndex;
		}
		set
		{
			this.setCursorPos(value);
		}
	}

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000467 RID: 1127 RVA: 0x00015D26 File Offset: 0x00013F26
	// (set) Token: 0x06000468 RID: 1128 RVA: 0x00015D2E File Offset: 0x00013F2E
	public bool ReadOnly
	{
		get
		{
			return this.readOnly;
		}
		set
		{
			if (value != this.readOnly)
			{
				this.readOnly = value;
				this.OnReadOnlyChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000469 RID: 1129 RVA: 0x00015D4C File Offset: 0x00013F4C
	// (set) Token: 0x0600046A RID: 1130 RVA: 0x00015D54 File Offset: 0x00013F54
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			value = (value ?? string.Empty);
			if (value.Length > this.MaxLength)
			{
				value = value.Substring(0, this.MaxLength);
			}
			value = value.Replace("\t", " ");
			if (value != this.text)
			{
				this.text = value;
				this.scrollIndex = (this.cursorIndex = 0);
				this.OnTextChanged();
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x0600046B RID: 1131 RVA: 0x00015DCD File Offset: 0x00013FCD
	// (set) Token: 0x0600046C RID: 1132 RVA: 0x00015DD5 File Offset: 0x00013FD5
	public Color32 TextColor
	{
		get
		{
			return this.textColor;
		}
		set
		{
			this.textColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600046D RID: 1133 RVA: 0x00015DE4 File Offset: 0x00013FE4
	// (set) Token: 0x0600046E RID: 1134 RVA: 0x00015DEC File Offset: 0x00013FEC
	public string SelectionSprite
	{
		get
		{
			return this.selectionSprite;
		}
		set
		{
			if (value != this.selectionSprite)
			{
				this.selectionSprite = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x0600046F RID: 1135 RVA: 0x00015E09 File Offset: 0x00014009
	// (set) Token: 0x06000470 RID: 1136 RVA: 0x00015E11 File Offset: 0x00014011
	public Color32 SelectionBackgroundColor
	{
		get
		{
			return this.selectionBackground;
		}
		set
		{
			this.selectionBackground = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000471 RID: 1137 RVA: 0x00015E20 File Offset: 0x00014020
	// (set) Token: 0x06000472 RID: 1138 RVA: 0x00015E28 File Offset: 0x00014028
	public Color32 CursorColor
	{
		get
		{
			return this.cursorColor;
		}
		set
		{
			this.cursorColor = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06000473 RID: 1139 RVA: 0x00015E37 File Offset: 0x00014037
	// (set) Token: 0x06000474 RID: 1140 RVA: 0x00015E3F File Offset: 0x0001403F
	public float TextScale
	{
		get
		{
			return this.textScale;
		}
		set
		{
			value = Mathf.Max(0.1f, value);
			if (!Mathf.Approximately(this.textScale, value))
			{
				dfFontManager.Invalidate(this.Font);
				this.textScale = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06000475 RID: 1141 RVA: 0x00015E74 File Offset: 0x00014074
	// (set) Token: 0x06000476 RID: 1142 RVA: 0x00015E7C File Offset: 0x0001407C
	public dfTextScaleMode TextScaleMode
	{
		get
		{
			return this.textScaleMode;
		}
		set
		{
			this.textScaleMode = value;
			this.Invalidate();
		}
	}

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06000477 RID: 1143 RVA: 0x00015E8B File Offset: 0x0001408B
	// (set) Token: 0x06000478 RID: 1144 RVA: 0x00015E94 File Offset: 0x00014094
	public int MaxLength
	{
		get
		{
			return this.maxLength;
		}
		set
		{
			if (value != this.maxLength)
			{
				this.maxLength = Mathf.Max(0, value);
				if (this.maxLength < this.text.Length)
				{
					this.Text = this.text.Substring(0, this.maxLength);
				}
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06000479 RID: 1145 RVA: 0x00015EE8 File Offset: 0x000140E8
	// (set) Token: 0x0600047A RID: 1146 RVA: 0x00015EF0 File Offset: 0x000140F0
	public TextAlignment TextAlignment
	{
		get
		{
			return this.textAlign;
		}
		set
		{
			if (value != this.textAlign)
			{
				this.textAlign = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x0600047B RID: 1147 RVA: 0x00015F08 File Offset: 0x00014108
	// (set) Token: 0x0600047C RID: 1148 RVA: 0x00015F10 File Offset: 0x00014110
	public bool Shadow
	{
		get
		{
			return this.shadow;
		}
		set
		{
			if (value != this.shadow)
			{
				this.shadow = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x0600047D RID: 1149 RVA: 0x00015F28 File Offset: 0x00014128
	// (set) Token: 0x0600047E RID: 1150 RVA: 0x00015F30 File Offset: 0x00014130
	public Color32 ShadowColor
	{
		get
		{
			return this.shadowColor;
		}
		set
		{
			if (!value.Equals(this.shadowColor))
			{
				this.shadowColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x0600047F RID: 1151 RVA: 0x00015F59 File Offset: 0x00014159
	// (set) Token: 0x06000480 RID: 1152 RVA: 0x00015F61 File Offset: 0x00014161
	public Vector2 ShadowOffset
	{
		get
		{
			return this.shadowOffset;
		}
		set
		{
			if (value != this.shadowOffset)
			{
				this.shadowOffset = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000481 RID: 1153 RVA: 0x00015F7E File Offset: 0x0001417E
	// (set) Token: 0x06000482 RID: 1154 RVA: 0x00015F86 File Offset: 0x00014186
	public bool UseMobileKeyboard
	{
		get
		{
			return this.useMobileKeyboard;
		}
		set
		{
			this.useMobileKeyboard = value;
		}
	}

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000483 RID: 1155 RVA: 0x00015F8F File Offset: 0x0001418F
	// (set) Token: 0x06000484 RID: 1156 RVA: 0x00015F97 File Offset: 0x00014197
	public bool MobileAutoCorrect
	{
		get
		{
			return this.mobileAutoCorrect;
		}
		set
		{
			this.mobileAutoCorrect = value;
		}
	}

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000485 RID: 1157 RVA: 0x00015FA0 File Offset: 0x000141A0
	// (set) Token: 0x06000486 RID: 1158 RVA: 0x00015FA8 File Offset: 0x000141A8
	public bool HideMobileInputField
	{
		get
		{
			return this.mobileHideInputField;
		}
		set
		{
			this.mobileHideInputField = value;
		}
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x00015FB1 File Offset: 0x000141B1
	// (set) Token: 0x06000488 RID: 1160 RVA: 0x00015FB9 File Offset: 0x000141B9
	public dfMobileKeyboardTrigger MobileKeyboardTrigger
	{
		get
		{
			return this.mobileKeyboardTrigger;
		}
		set
		{
			this.mobileKeyboardTrigger = value;
		}
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x00015FC2 File Offset: 0x000141C2
	protected override void OnTabKeyPressed(dfKeyEventArgs args)
	{
		if (!this.acceptsTab)
		{
			base.OnTabKeyPressed(args);
			return;
		}
		base.OnKeyPress(args);
		if (args.Used)
		{
			return;
		}
		args.Character = '\t';
		this.processKeyPress(args);
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00015FF3 File Offset: 0x000141F3
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.ReadOnly || char.IsControl(args.Character))
		{
			base.OnKeyPress(args);
			return;
		}
		base.OnKeyPress(args);
		if (args.Used)
		{
			return;
		}
		this.processKeyPress(args);
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0001602C File Offset: 0x0001422C
	private void processKeyPress(dfKeyEventArgs args)
	{
		this.DeleteSelection();
		if (this.text.Length < this.MaxLength)
		{
			if (this.cursorIndex == this.text.Length)
			{
				this.text += args.Character.ToString();
			}
			else
			{
				this.text = this.text.Insert(this.cursorIndex, args.Character.ToString());
			}
			this.cursorIndex++;
			this.OnTextChanged();
			this.Invalidate();
		}
		args.Use();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x000160CC File Offset: 0x000142CC
	protected internal override void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.ReadOnly)
		{
			return;
		}
		base.OnKeyDown(args);
		if (args.Used)
		{
			return;
		}
		KeyCode keyCode = args.KeyCode;
		if (keyCode <= KeyCode.A)
		{
			if (keyCode <= KeyCode.Return)
			{
				if (keyCode != KeyCode.Backspace)
				{
					if (keyCode == KeyCode.Return)
					{
						this.OnSubmit();
						goto IL_269;
					}
				}
				else
				{
					if (args.Control)
					{
						this.DeletePreviousWord();
						goto IL_269;
					}
					this.DeletePreviousChar();
					goto IL_269;
				}
			}
			else
			{
				if (keyCode == KeyCode.Escape)
				{
					this.ClearSelection();
					this.cursorIndex = (this.scrollIndex = 0);
					this.Invalidate();
					this.OnCancel();
					goto IL_269;
				}
				if (keyCode == KeyCode.A)
				{
					if (args.Control)
					{
						this.SelectAll();
						goto IL_269;
					}
					goto IL_269;
				}
			}
		}
		else if (keyCode <= KeyCode.V)
		{
			if (keyCode != KeyCode.C)
			{
				if (keyCode == KeyCode.V)
				{
					if (!args.Control)
					{
						goto IL_269;
					}
					string clipBoard = dfClipboardHelper.clipBoard;
					if (!string.IsNullOrEmpty(clipBoard))
					{
						this.PasteAtCursor(clipBoard);
						goto IL_269;
					}
					goto IL_269;
				}
			}
			else
			{
				if (args.Control)
				{
					this.CopySelectionToClipboard();
					goto IL_269;
				}
				goto IL_269;
			}
		}
		else if (keyCode != KeyCode.X)
		{
			if (keyCode != KeyCode.Delete)
			{
				switch (keyCode)
				{
				case KeyCode.RightArrow:
					if (args.Control)
					{
						if (args.Shift)
						{
							this.moveSelectionPointRightWord();
							goto IL_269;
						}
						this.MoveCursorToNextWord();
						goto IL_269;
					}
					else
					{
						if (args.Shift)
						{
							this.moveSelectionPointRight();
							goto IL_269;
						}
						this.MoveCursorToNextChar();
						goto IL_269;
					}
					break;
				case KeyCode.LeftArrow:
					if (args.Control)
					{
						if (args.Shift)
						{
							this.moveSelectionPointLeftWord();
							goto IL_269;
						}
						this.MoveCursorToPreviousWord();
						goto IL_269;
					}
					else
					{
						if (args.Shift)
						{
							this.moveSelectionPointLeft();
							goto IL_269;
						}
						this.MoveCursorToPreviousChar();
						goto IL_269;
					}
					break;
				case KeyCode.Insert:
				{
					if (!args.Shift)
					{
						goto IL_269;
					}
					string clipBoard2 = dfClipboardHelper.clipBoard;
					if (!string.IsNullOrEmpty(clipBoard2))
					{
						this.PasteAtCursor(clipBoard2);
						goto IL_269;
					}
					goto IL_269;
				}
				case KeyCode.Home:
					if (args.Shift)
					{
						this.SelectToStart();
						goto IL_269;
					}
					this.MoveCursorToStart();
					goto IL_269;
				case KeyCode.End:
					if (args.Shift)
					{
						this.SelectToEnd();
						goto IL_269;
					}
					this.MoveCursorToEnd();
					goto IL_269;
				}
			}
			else
			{
				if (this.selectionStart != this.selectionEnd)
				{
					this.DeleteSelection();
					goto IL_269;
				}
				if (args.Control)
				{
					this.DeleteNextWord();
					goto IL_269;
				}
				this.DeleteNextChar();
				goto IL_269;
			}
		}
		else
		{
			if (args.Control)
			{
				this.CutSelectionToClipboard();
				goto IL_269;
			}
			goto IL_269;
		}
		base.OnKeyDown(args);
		return;
		IL_269:
		args.Use();
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x00016348 File Offset: 0x00014548
	public override void OnEnable()
	{
		if (this.padding == null)
		{
			this.padding = new RectOffset();
		}
		base.OnEnable();
		if (this.size.magnitude == 0f)
		{
			base.Size = new Vector2(100f, 20f);
		}
		this.cursorShown = false;
		this.cursorIndex = (this.scrollIndex = 0);
		bool flag = this.Font != null && this.Font.IsValid;
		if (Application.isPlaying && !flag)
		{
			this.Font = base.GetManager().DefaultFont;
		}
		this.bindTextureRebuildCallback();
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x000163EA File Offset: 0x000145EA
	public override void OnDisable()
	{
		base.OnDisable();
		this.unbindTextureRebuildCallback();
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000163F8 File Offset: 0x000145F8
	public override void Awake()
	{
		base.Awake();
		this.startSize = base.Size;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001640C File Offset: 0x0001460C
	protected internal override void OnEnterFocus(dfFocusEventArgs args)
	{
		base.OnEnterFocus(args);
		this.undoText = this.Text;
		if (!this.ReadOnly)
		{
			this.whenGotFocus = Time.realtimeSinceStartup;
			base.StopAllCoroutines();
			base.StartCoroutine(this.doCursorBlink());
			if (this.selectOnFocus)
			{
				this.selectionStart = 0;
				this.selectionEnd = this.text.Length;
			}
			else
			{
				this.selectionStart = (this.selectionEnd = 0);
			}
		}
		this.Invalidate();
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001648A File Offset: 0x0001468A
	protected internal override void OnLeaveFocus(dfFocusEventArgs args)
	{
		base.OnLeaveFocus(args);
		base.StopAllCoroutines();
		this.cursorShown = false;
		this.ClearSelection();
		this.Invalidate();
		this.whenGotFocus = 0f;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000164B8 File Offset: 0x000146B8
	protected internal override void OnDoubleClick(dfMouseEventArgs args)
	{
		this.tripleClickTimer = Time.realtimeSinceStartup;
		if (args.Source != this)
		{
			base.OnDoubleClick(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(dfMouseButtons.Left) && Time.realtimeSinceStartup - this.whenGotFocus > 0.5f)
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			this.SelectWordAtIndex(charIndexOfMouse);
		}
		base.OnDoubleClick(args);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x00016530 File Offset: 0x00014730
	protected internal override void OnMouseDown(dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseDown(args);
			return;
		}
		if (!this.ReadOnly && args.Buttons.IsSet(dfMouseButtons.Left) && ((!this.HasFocus && !this.SelectOnFocus) || Time.realtimeSinceStartup - this.whenGotFocus > 0.25f))
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			if (charIndexOfMouse != this.cursorIndex)
			{
				this.cursorIndex = charIndexOfMouse;
				this.cursorShown = true;
				this.Invalidate();
				args.Use();
			}
			this.mouseSelectionAnchor = this.cursorIndex;
			this.selectionStart = (this.selectionEnd = this.cursorIndex);
			if (Time.realtimeSinceStartup - this.tripleClickTimer < 0.25f)
			{
				this.SelectAll();
				this.tripleClickTimer = 0f;
			}
		}
		base.OnMouseDown(args);
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001660C File Offset: 0x0001480C
	protected internal override void OnMouseMove(dfMouseEventArgs args)
	{
		if (args.Source != this)
		{
			base.OnMouseMove(args);
			return;
		}
		if (!this.ReadOnly && this.HasFocus && args.Buttons.IsSet(dfMouseButtons.Left))
		{
			int charIndexOfMouse = this.getCharIndexOfMouse(args);
			if (charIndexOfMouse != this.cursorIndex)
			{
				this.cursorIndex = charIndexOfMouse;
				this.cursorShown = true;
				this.Invalidate();
				args.Use();
				this.selectionStart = Mathf.Min(this.mouseSelectionAnchor, charIndexOfMouse);
				this.selectionEnd = Mathf.Max(this.mouseSelectionAnchor, charIndexOfMouse);
				return;
			}
		}
		base.OnMouseMove(args);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x000166A4 File Offset: 0x000148A4
	protected internal virtual void OnTextChanged()
	{
		base.SignalHierarchy("OnTextChanged", new object[]
		{
			this,
			this.text
		});
		if (this.TextChanged != null)
		{
			this.TextChanged(this, this.text);
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x000166DF File Offset: 0x000148DF
	protected internal virtual void OnReadOnlyChanged()
	{
		if (this.ReadOnlyChanged != null)
		{
			this.ReadOnlyChanged(this, this.readOnly);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x000166FB File Offset: 0x000148FB
	protected internal virtual void OnPasswordCharacterChanged()
	{
		if (this.PasswordCharacterChanged != null)
		{
			this.PasswordCharacterChanged(this, this.passwordChar);
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00016717 File Offset: 0x00014917
	protected internal virtual void OnSubmit()
	{
		base.SignalHierarchy("OnTextSubmitted", new object[]
		{
			this,
			this.text
		});
		if (this.TextSubmitted != null)
		{
			this.TextSubmitted(this, this.text);
		}
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00016754 File Offset: 0x00014954
	protected internal virtual void OnCancel()
	{
		this.text = this.undoText;
		base.SignalHierarchy("OnTextCancelled", new object[]
		{
			this,
			this.text
		});
		if (this.TextCancelled != null)
		{
			this.TextCancelled(this, this.text);
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x000167A6 File Offset: 0x000149A6
	public void ClearSelection()
	{
		this.selectionStart = 0;
		this.selectionEnd = 0;
		this.mouseSelectionAnchor = 0;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000167BD File Offset: 0x000149BD
	public void SelectAll()
	{
		this.selectionStart = 0;
		this.selectionEnd = this.text.Length;
		this.scrollIndex = 0;
		this.setCursorPos(0);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000167E5 File Offset: 0x000149E5
	private void CutSelectionToClipboard()
	{
		this.CopySelectionToClipboard();
		this.DeleteSelection();
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000167F3 File Offset: 0x000149F3
	private void CopySelectionToClipboard()
	{
		if (this.selectionStart == this.selectionEnd)
		{
			return;
		}
		dfClipboardHelper.clipBoard = this.text.Substring(this.selectionStart, this.selectionEnd - this.selectionStart);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00016828 File Offset: 0x00014A28
	public void PasteAtCursor(string clipData)
	{
		this.DeleteSelection();
		StringBuilder stringBuilder = new StringBuilder(this.text.Length + clipData.Length);
		stringBuilder.Append(this.text);
		foreach (char c in clipData)
		{
			if (c >= ' ')
			{
				StringBuilder stringBuilder2 = stringBuilder;
				int num = this.cursorIndex;
				this.cursorIndex = num + 1;
				stringBuilder2.Insert(num, c);
			}
		}
		stringBuilder.Length = Mathf.Min(stringBuilder.Length, this.maxLength);
		this.text = stringBuilder.ToString();
		this.setCursorPos(this.cursorIndex);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x000168D4 File Offset: 0x00014AD4
	public void SelectWordAtIndex(int index)
	{
		if (string.IsNullOrEmpty(this.text))
		{
			return;
		}
		index = Mathf.Max(Mathf.Min(this.text.Length - 1, index), 0);
		if (!char.IsLetterOrDigit(this.text[index]))
		{
			this.selectionStart = index;
			this.selectionEnd = index + 1;
			this.mouseSelectionAnchor = 0;
		}
		else
		{
			this.selectionStart = index;
			int num = index;
			while (num > 0 && char.IsLetterOrDigit(this.text[num - 1]))
			{
				this.selectionStart--;
				num--;
			}
			this.selectionEnd = index;
			int num2 = index;
			while (num2 < this.text.Length && char.IsLetterOrDigit(this.text[num2]))
			{
				this.selectionEnd = num2 + 1;
				num2++;
			}
		}
		this.cursorIndex = this.selectionStart;
		this.Invalidate();
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x000169B8 File Offset: 0x00014BB8
	public void DeletePreviousChar()
	{
		if (this.selectionStart != this.selectionEnd)
		{
			int cursorPos = this.selectionStart;
			this.DeleteSelection();
			this.setCursorPos(cursorPos);
			return;
		}
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		this.text = this.text.Remove(this.cursorIndex - 1, 1);
		this.cursorIndex--;
		this.cursorShown = true;
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00016A34 File Offset: 0x00014C34
	public void DeletePreviousWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		int num = this.findPreviousWord(this.cursorIndex);
		if (num == this.cursorIndex)
		{
			num = 0;
		}
		this.text = this.text.Remove(num, this.cursorIndex - num);
		this.setCursorPos(num);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00016A98 File Offset: 0x00014C98
	public void DeleteSelection()
	{
		if (this.selectionStart == this.selectionEnd)
		{
			return;
		}
		this.text = this.text.Remove(this.selectionStart, this.selectionEnd - this.selectionStart);
		this.setCursorPos(this.selectionStart);
		this.ClearSelection();
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00016AF8 File Offset: 0x00014CF8
	public void DeleteNextChar()
	{
		this.ClearSelection();
		if (this.cursorIndex >= this.text.Length)
		{
			return;
		}
		this.text = this.text.Remove(this.cursorIndex, 1);
		this.cursorShown = true;
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00016B4C File Offset: 0x00014D4C
	public void DeleteNextWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int num = this.findNextWord(this.cursorIndex);
		if (num == this.cursorIndex)
		{
			num = this.text.Length;
		}
		this.text = this.text.Remove(this.cursorIndex, num - this.cursorIndex);
		this.OnTextChanged();
		this.Invalidate();
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00016BC0 File Offset: 0x00014DC0
	public void SelectToStart()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = this.selectionStart;
		}
		this.selectionStart = 0;
		this.setCursorPos(0);
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00016C1C File Offset: 0x00014E1C
	public void SelectToEnd()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionStart = this.cursorIndex;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = this.selectionEnd;
		}
		this.selectionEnd = this.text.Length;
		this.setCursorPos(this.text.Length);
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00016C98 File Offset: 0x00014E98
	public void MoveCursorToNextWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int cursorPos = this.findNextWord(this.cursorIndex);
		this.setCursorPos(cursorPos);
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00016CD4 File Offset: 0x00014ED4
	public void MoveCursorToPreviousWord()
	{
		this.ClearSelection();
		if (this.cursorIndex == 0)
		{
			return;
		}
		int cursorPos = this.findPreviousWord(this.cursorIndex);
		this.setCursorPos(cursorPos);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00016D04 File Offset: 0x00014F04
	public void MoveCursorToEnd()
	{
		this.ClearSelection();
		this.setCursorPos(this.text.Length);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00016D1D File Offset: 0x00014F1D
	public void MoveCursorToStart()
	{
		this.ClearSelection();
		this.setCursorPos(0);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00016D2C File Offset: 0x00014F2C
	public void MoveCursorToNextChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex + 1);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00016D42 File Offset: 0x00014F42
	public void MoveCursorToPreviousChar()
	{
		this.ClearSelection();
		this.setCursorPos(this.cursorIndex - 1);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00016D58 File Offset: 0x00014F58
	private void moveSelectionPointRightWord()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		int cursorPos = this.findNextWord(this.cursorIndex);
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionStart = this.cursorIndex;
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = cursorPos;
		}
		this.setCursorPos(cursorPos);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00016DDC File Offset: 0x00014FDC
	private void moveSelectionPointLeftWord()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		int cursorPos = this.findPreviousWord(this.cursorIndex);
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
			this.selectionStart = cursorPos;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd = cursorPos;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart = cursorPos;
		}
		this.setCursorPos(cursorPos);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00016E58 File Offset: 0x00015058
	private void moveSelectionPointRight()
	{
		if (this.cursorIndex == this.text.Length)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex + 1;
			this.selectionStart = this.cursorIndex;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd++;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart++;
		}
		this.setCursorPos(this.cursorIndex + 1);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00016EEC File Offset: 0x000150EC
	private void moveSelectionPointLeft()
	{
		if (this.cursorIndex == 0)
		{
			return;
		}
		if (this.selectionEnd == this.selectionStart)
		{
			this.selectionEnd = this.cursorIndex;
			this.selectionStart = this.cursorIndex - 1;
		}
		else if (this.selectionEnd == this.cursorIndex)
		{
			this.selectionEnd--;
		}
		else if (this.selectionStart == this.cursorIndex)
		{
			this.selectionStart--;
		}
		this.setCursorPos(this.cursorIndex - 1);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00016F74 File Offset: 0x00015174
	private void setCursorPos(int index)
	{
		index = Mathf.Max(0, Mathf.Min(this.text.Length, index));
		if (index == this.cursorIndex)
		{
			return;
		}
		this.cursorIndex = index;
		this.cursorShown = this.HasFocus;
		this.scrollIndex = Mathf.Min(this.scrollIndex, this.cursorIndex);
		this.Invalidate();
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00016FD4 File Offset: 0x000151D4
	private int findPreviousWord(int startIndex)
	{
		int i;
		for (i = startIndex; i > 0; i--)
		{
			char c = this.text[i - 1];
			if (!char.IsWhiteSpace(c) && !char.IsSeparator(c) && !char.IsPunctuation(c))
			{
				break;
			}
		}
		for (int j = i; j >= 0; j--)
		{
			if (j == 0)
			{
				i = 0;
				break;
			}
			char c2 = this.text[j - 1];
			if (char.IsWhiteSpace(c2) || char.IsSeparator(c2) || char.IsPunctuation(c2))
			{
				i = j;
				break;
			}
		}
		return i;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00017054 File Offset: 0x00015254
	private int findNextWord(int startIndex)
	{
		int length = this.text.Length;
		int i = startIndex;
		for (int j = i; j < length; j++)
		{
			char c = this.text[j];
			if (char.IsWhiteSpace(c) || char.IsSeparator(c) || char.IsPunctuation(c))
			{
				i = j;
				IL_72:
				while (i < length)
				{
					char c2 = this.text[i];
					if (!char.IsWhiteSpace(c2) && !char.IsSeparator(c2) && !char.IsPunctuation(c2))
					{
						break;
					}
					i++;
				}
				return i;
			}
		}
		goto IL_72;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x000170D8 File Offset: 0x000152D8
	private IEnumerator doCursorBlink()
	{
		if (!Application.isPlaying)
		{
			yield break;
		}
		this.cursorShown = true;
		while (this.ContainsFocus)
		{
			yield return new WaitForSeconds(this.cursorBlinkTime);
			this.cursorShown = !this.cursorShown;
			this.Invalidate();
		}
		this.cursorShown = false;
		yield break;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x000170E8 File Offset: 0x000152E8
	private void renderText(dfRenderData textBuffer)
	{
		float num = base.PixelsToUnits();
		Vector2 vector = new Vector2(this.size.x - (float)this.padding.horizontal, this.size.y - (float)this.padding.vertical);
		Vector3 vector2 = this.pivot.TransformToUpperLeft(base.Size);
		Vector3 vectorOffset = new Vector3(vector2.x + (float)this.padding.left, vector2.y - (float)this.padding.top, 0f) * num;
		string text = (this.IsPasswordField && !string.IsNullOrEmpty(this.passwordChar)) ? this.passwordDisplayText() : this.text;
		Color32 color = base.IsEnabled ? this.TextColor : base.DisabledColor;
		float textScaleMultiplier = this.getTextScaleMultiplier();
		using (dfFontRendererBase dfFontRendererBase = this.font.ObtainRenderer())
		{
			dfFontRendererBase.WordWrap = false;
			dfFontRendererBase.MaxSize = vector;
			dfFontRendererBase.PixelRatio = num;
			dfFontRendererBase.TextScale = this.TextScale * textScaleMultiplier;
			dfFontRendererBase.VectorOffset = vectorOffset;
			dfFontRendererBase.MultiLine = false;
			dfFontRendererBase.TextAlign = TextAlignment.Left;
			dfFontRendererBase.ProcessMarkup = false;
			dfFontRendererBase.DefaultColor = color;
			dfFontRendererBase.BottomColor = new Color32?(color);
			dfFontRendererBase.OverrideMarkupColors = false;
			dfFontRendererBase.Opacity = base.CalculateOpacity();
			dfFontRendererBase.Shadow = this.Shadow;
			dfFontRendererBase.ShadowColor = this.ShadowColor;
			dfFontRendererBase.ShadowOffset = this.ShadowOffset;
			this.cursorIndex = Mathf.Min(this.cursorIndex, text.Length);
			this.scrollIndex = Mathf.Min(Mathf.Min(this.scrollIndex, this.cursorIndex), text.Length);
			this.charWidths = dfFontRendererBase.GetCharacterWidths(text);
			Vector2 vector3 = vector * num;
			this.leftOffset = 0f;
			if (this.textAlign == TextAlignment.Left)
			{
				float num2 = 0f;
				for (int i = this.scrollIndex; i < this.cursorIndex; i++)
				{
					num2 += this.charWidths[i];
				}
				while (num2 >= vector3.x)
				{
					if (this.scrollIndex >= this.cursorIndex)
					{
						break;
					}
					float num3 = num2;
					float[] array = this.charWidths;
					int num4 = this.scrollIndex;
					this.scrollIndex = num4 + 1;
					num2 = num3 - array[num4];
				}
			}
			else
			{
				this.scrollIndex = Mathf.Max(0, Mathf.Min(this.cursorIndex, text.Length - 1));
				float num5 = 0f;
				float num6 = (float)this.font.FontSize * 1.25f * num;
				while (this.scrollIndex > 0 && num5 < vector3.x - num6)
				{
					float num7 = num5;
					float[] array2 = this.charWidths;
					int num4 = this.scrollIndex;
					this.scrollIndex = num4 - 1;
					num5 = num7 + array2[num4];
				}
				float num8 = (text.Length > 0) ? dfFontRendererBase.GetCharacterWidths(text.Substring(this.scrollIndex)).Sum() : 0f;
				TextAlignment textAlignment = this.textAlign;
				if (textAlignment != TextAlignment.Center)
				{
					if (textAlignment == TextAlignment.Right)
					{
						this.leftOffset = Mathf.Max(0f, vector3.x - num8);
					}
				}
				else
				{
					this.leftOffset = Mathf.Max(0f, (vector3.x - num8) * 0.5f);
				}
				vectorOffset.x += this.leftOffset;
				dfFontRendererBase.VectorOffset = vectorOffset;
			}
			if (this.selectionEnd != this.selectionStart)
			{
				this.renderSelection(this.scrollIndex, this.charWidths, this.leftOffset);
			}
			else if (this.cursorShown)
			{
				this.renderCursor(this.scrollIndex, this.cursorIndex, this.charWidths, this.leftOffset);
			}
			dfFontRendererBase.Render(text.Substring(this.scrollIndex), textBuffer);
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x000174DC File Offset: 0x000156DC
	private float getTextScaleMultiplier()
	{
		if (this.textScaleMode == dfTextScaleMode.None || !Application.isPlaying)
		{
			return 1f;
		}
		if (this.textScaleMode == dfTextScaleMode.ScreenResolution)
		{
			return (float)Screen.height / (float)this.cachedManager.FixedHeight;
		}
		return base.Size.y / this.startSize.y;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00017532 File Offset: 0x00015732
	private string passwordDisplayText()
	{
		return new string(this.passwordChar[0], this.text.Length);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00017550 File Offset: 0x00015750
	private void renderSelection(int scrollIndex, float[] charWidths, float leftOffset)
	{
		if (string.IsNullOrEmpty(this.SelectionSprite) || base.Atlas == null)
		{
			return;
		}
		float num = base.PixelsToUnits();
		float num2 = (this.size.x - (float)this.padding.horizontal) * num;
		int num3 = scrollIndex;
		float num4 = 0f;
		for (int i = scrollIndex; i < this.text.Length; i++)
		{
			num3++;
			num4 += charWidths[i];
			if (num4 > num2)
			{
				break;
			}
		}
		if (this.selectionStart > num3 || this.selectionEnd < scrollIndex)
		{
			return;
		}
		int num5 = Mathf.Max(scrollIndex, this.selectionStart);
		if (num5 > num3)
		{
			return;
		}
		int num6 = Mathf.Min(this.selectionEnd, num3);
		if (num6 <= scrollIndex)
		{
			return;
		}
		float num7 = 0f;
		float num8 = 0f;
		num4 = 0f;
		for (int j = scrollIndex; j <= num3; j++)
		{
			if (j == num5)
			{
				num7 = num4;
			}
			if (j == num6)
			{
				num8 = num4;
				break;
			}
			num4 += charWidths[j];
		}
		float num9 = base.Size.y * num;
		this.addQuadIndices(this.renderData.Vertices, this.renderData.Triangles);
		RectOffset selectionPadding = this.getSelectionPadding();
		float num10 = num7 + leftOffset + (float)this.padding.left * num;
		float x = num10 + Mathf.Min(num8 - num7, num2);
		float num11 = (float)(-(float)(selectionPadding.top + 1)) * num;
		float y = num11 - num9 + (float)(selectionPadding.vertical + 2) * num;
		Vector3 b = this.pivot.TransformToUpperLeft(base.Size) * num;
		Vector3 item = new Vector3(num10, num11) + b;
		Vector3 item2 = new Vector3(x, num11) + b;
		Vector3 item3 = new Vector3(num10, y) + b;
		Vector3 item4 = new Vector3(x, y) + b;
		this.renderData.Vertices.Add(item);
		this.renderData.Vertices.Add(item2);
		this.renderData.Vertices.Add(item4);
		this.renderData.Vertices.Add(item3);
		Color32 item5 = base.ApplyOpacity(this.SelectionBackgroundColor);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		this.renderData.Colors.Add(item5);
		dfAtlas.ItemInfo itemInfo = base.Atlas[this.SelectionSprite];
		Rect region = itemInfo.region;
		float num12 = region.width / itemInfo.sizeInPixels.x;
		float num13 = region.height / itemInfo.sizeInPixels.y;
		this.renderData.UV.Add(new Vector2(region.x + num12, region.yMax - num13));
		this.renderData.UV.Add(new Vector2(region.xMax - num12, region.yMax - num13));
		this.renderData.UV.Add(new Vector2(region.xMax - num12, region.y + num13));
		this.renderData.UV.Add(new Vector2(region.x + num12, region.y + num13));
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x000178A8 File Offset: 0x00015AA8
	private RectOffset getSelectionPadding()
	{
		if (base.Atlas == null)
		{
			return this.padding;
		}
		dfAtlas.ItemInfo backgroundSprite = this.getBackgroundSprite();
		if (backgroundSprite == null)
		{
			return this.padding;
		}
		return backgroundSprite.border;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x000178E8 File Offset: 0x00015AE8
	private void renderCursor(int startIndex, int cursorIndex, float[] charWidths, float leftOffset)
	{
		if (string.IsNullOrEmpty(this.SelectionSprite) || base.Atlas == null)
		{
			return;
		}
		float num = 0f;
		for (int i = startIndex; i < cursorIndex; i++)
		{
			num += charWidths[i];
		}
		float num2 = base.PixelsToUnits();
		float num3 = (num + leftOffset + (float)this.padding.left * num2).Quantize(num2);
		float num4 = (float)(-(float)this.padding.top) * num2;
		float num5 = num2 * (float)this.cursorWidth;
		float num6 = (this.size.y - (float)this.padding.vertical) * num2;
		Vector3 a = new Vector3(num3, num4);
		Vector3 a2 = new Vector3(num3 + num5, num4);
		Vector3 a3 = new Vector3(num3 + num5, num4 - num6);
		Vector3 a4 = new Vector3(num3, num4 - num6);
		dfList<Vector3> vertices = this.renderData.Vertices;
		dfList<int> triangles = this.renderData.Triangles;
		dfList<Vector2> uv = this.renderData.UV;
		dfList<Color32> colors = this.renderData.Colors;
		Vector3 b = this.pivot.TransformToUpperLeft(this.size) * num2;
		this.addQuadIndices(vertices, triangles);
		vertices.Add(a + b);
		vertices.Add(a2 + b);
		vertices.Add(a3 + b);
		vertices.Add(a4 + b);
		Color32 item = base.ApplyOpacity(this.CursorColor);
		colors.Add(item);
		colors.Add(item);
		colors.Add(item);
		colors.Add(item);
		Rect region = base.Atlas[this.SelectionSprite].region;
		uv.Add(new Vector2(region.x, region.yMax));
		uv.Add(new Vector2(region.xMax, region.yMax));
		uv.Add(new Vector2(region.xMax, region.y));
		uv.Add(new Vector2(region.x, region.y));
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x00017AF4 File Offset: 0x00015CF4
	private void addQuadIndices(dfList<Vector3> verts, dfList<int> triangles)
	{
		int count = verts.Count;
		int[] array = new int[]
		{
			0,
			1,
			3,
			3,
			1,
			2
		};
		for (int i = 0; i < array.Length; i++)
		{
			triangles.Add(count + array[i]);
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00017B34 File Offset: 0x00015D34
	private int getCharIndexOfMouse(dfMouseEventArgs args)
	{
		Vector2 hitPosition = base.GetHitPosition(args);
		float num = base.PixelsToUnits();
		int num2 = this.scrollIndex;
		float num3 = this.leftOffset / num;
		for (int i = this.scrollIndex; i < this.charWidths.Length; i++)
		{
			num3 += this.charWidths[i] / num;
			if (num3 < hitPosition.x)
			{
				num2++;
			}
		}
		return num2;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x00017B98 File Offset: 0x00015D98
	public dfList<dfRenderData> RenderMultiple()
	{
		if (base.Atlas == null || this.Font == null)
		{
			return null;
		}
		if (!this.isVisible)
		{
			return null;
		}
		if (this.renderData == null)
		{
			this.renderData = dfRenderData.Obtain();
			this.textRenderData = dfRenderData.Obtain();
			this.isControlInvalidated = true;
		}
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		if (!this.isControlInvalidated)
		{
			for (int i = 0; i < this.buffers.Count; i++)
			{
				this.buffers[i].Transform = localToWorldMatrix;
			}
			return this.buffers;
		}
		this.buffers.Clear();
		this.renderData.Clear();
		this.renderData.Material = base.Atlas.Material;
		this.renderData.Transform = localToWorldMatrix;
		this.buffers.Add(this.renderData);
		this.textRenderData.Clear();
		this.textRenderData.Material = base.Atlas.Material;
		this.textRenderData.Transform = localToWorldMatrix;
		this.buffers.Add(this.textRenderData);
		this.renderBackground();
		this.renderText(this.textRenderData);
		this.isControlInvalidated = false;
		base.updateCollider();
		return this.buffers;
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x00017CE4 File Offset: 0x00015EE4
	private void bindTextureRebuildCallback()
	{
		if (this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		if (this.Font is dfDynamicFont)
		{
			Font baseFont = (this.Font as dfDynamicFont).BaseFont;
			baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Combine(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
			this.isFontCallbackAssigned = true;
		}
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x00017D50 File Offset: 0x00015F50
	private void unbindTextureRebuildCallback()
	{
		if (!this.isFontCallbackAssigned || this.Font == null)
		{
			return;
		}
		if (this.Font is dfDynamicFont)
		{
			Font baseFont = (this.Font as dfDynamicFont).BaseFont;
			baseFont.textureRebuildCallback = (Font.FontTextureRebuildCallback)Delegate.Remove(baseFont.textureRebuildCallback, new Font.FontTextureRebuildCallback(this.onFontTextureRebuilt));
		}
		this.isFontCallbackAssigned = false;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x00017DBC File Offset: 0x00015FBC
	private void requestCharacterInfo()
	{
		dfDynamicFont dfDynamicFont = this.Font as dfDynamicFont;
		if (dfDynamicFont == null)
		{
			return;
		}
		if (!dfFontManager.IsDirty(this.Font))
		{
			return;
		}
		if (string.IsNullOrEmpty(this.text))
		{
			return;
		}
		float num = this.TextScale * this.getTextScaleMultiplier();
		int fontSize = Mathf.CeilToInt((float)this.font.FontSize * num);
		dfDynamicFont.AddCharacterRequest(this.text, fontSize, FontStyle.Normal);
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x00017E2B File Offset: 0x0001602B
	private void onFontTextureRebuilt()
	{
		this.requestCharacterInfo();
		this.Invalidate();
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00017E39 File Offset: 0x00016039
	public void UpdateFontInfo()
	{
		this.requestCharacterInfo();
	}

	// Token: 0x04000174 RID: 372
	[SerializeField]
	protected dfFontBase font;

	// Token: 0x04000175 RID: 373
	[SerializeField]
	protected bool acceptsTab;

	// Token: 0x04000176 RID: 374
	[SerializeField]
	protected bool displayAsPassword;

	// Token: 0x04000177 RID: 375
	[SerializeField]
	protected string passwordChar = "*";

	// Token: 0x04000178 RID: 376
	[SerializeField]
	protected bool readOnly;

	// Token: 0x04000179 RID: 377
	[SerializeField]
	protected string text = "";

	// Token: 0x0400017A RID: 378
	[SerializeField]
	protected Color32 textColor = UnityEngine.Color.white;

	// Token: 0x0400017B RID: 379
	[SerializeField]
	protected Color32 selectionBackground = new Color32(0, 105, 210, byte.MaxValue);

	// Token: 0x0400017C RID: 380
	[SerializeField]
	protected Color32 cursorColor = UnityEngine.Color.white;

	// Token: 0x0400017D RID: 381
	[SerializeField]
	protected string selectionSprite = "";

	// Token: 0x0400017E RID: 382
	[SerializeField]
	protected float textScale = 1f;

	// Token: 0x0400017F RID: 383
	[SerializeField]
	protected dfTextScaleMode textScaleMode;

	// Token: 0x04000180 RID: 384
	[SerializeField]
	protected RectOffset padding = new RectOffset();

	// Token: 0x04000181 RID: 385
	[SerializeField]
	protected float cursorBlinkTime = 0.45f;

	// Token: 0x04000182 RID: 386
	[SerializeField]
	protected int cursorWidth = 1;

	// Token: 0x04000183 RID: 387
	[SerializeField]
	protected int maxLength = 1024;

	// Token: 0x04000184 RID: 388
	[SerializeField]
	protected bool selectOnFocus;

	// Token: 0x04000185 RID: 389
	[SerializeField]
	protected bool shadow;

	// Token: 0x04000186 RID: 390
	[SerializeField]
	protected Color32 shadowColor = UnityEngine.Color.black;

	// Token: 0x04000187 RID: 391
	[SerializeField]
	protected Vector2 shadowOffset = new Vector2(1f, -1f);

	// Token: 0x04000188 RID: 392
	[SerializeField]
	protected bool useMobileKeyboard;

	// Token: 0x04000189 RID: 393
	[SerializeField]
	protected int mobileKeyboardType;

	// Token: 0x0400018A RID: 394
	[SerializeField]
	protected bool mobileAutoCorrect;

	// Token: 0x0400018B RID: 395
	[SerializeField]
	protected bool mobileHideInputField;

	// Token: 0x0400018C RID: 396
	[SerializeField]
	protected dfMobileKeyboardTrigger mobileKeyboardTrigger;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	protected TextAlignment textAlign;

	// Token: 0x0400018E RID: 398
	private Vector2 startSize = Vector2.zero;

	// Token: 0x0400018F RID: 399
	private int selectionStart;

	// Token: 0x04000190 RID: 400
	private int selectionEnd;

	// Token: 0x04000191 RID: 401
	private int mouseSelectionAnchor;

	// Token: 0x04000192 RID: 402
	private int scrollIndex;

	// Token: 0x04000193 RID: 403
	private int cursorIndex;

	// Token: 0x04000194 RID: 404
	private float leftOffset;

	// Token: 0x04000195 RID: 405
	private bool cursorShown;

	// Token: 0x04000196 RID: 406
	private float[] charWidths;

	// Token: 0x04000197 RID: 407
	private float whenGotFocus;

	// Token: 0x04000198 RID: 408
	private string undoText = "";

	// Token: 0x04000199 RID: 409
	private float tripleClickTimer;

	// Token: 0x0400019A RID: 410
	private bool isFontCallbackAssigned;

	// Token: 0x0400019B RID: 411
	private dfRenderData textRenderData;

	// Token: 0x0400019C RID: 412
	private dfList<dfRenderData> buffers = dfList<dfRenderData>.Obtain();
}
