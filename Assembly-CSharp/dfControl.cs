using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x02000009 RID: 9
[ExecuteInEditMode]
[Serializable]
public abstract class dfControl : MonoBehaviour, IDFControlHost, IComparable<dfControl>
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060000A0 RID: 160 RVA: 0x00003E90 File Offset: 0x00002090
	// (remove) Token: 0x060000A1 RID: 161 RVA: 0x00003EC8 File Offset: 0x000020C8
	[HideInInspector]
	public event ChildControlEventHandler ControlAdded;

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x060000A2 RID: 162 RVA: 0x00003F00 File Offset: 0x00002100
	// (remove) Token: 0x060000A3 RID: 163 RVA: 0x00003F38 File Offset: 0x00002138
	[HideInInspector]
	public event ChildControlEventHandler ControlRemoved;

	// Token: 0x14000005 RID: 5
	// (add) Token: 0x060000A4 RID: 164 RVA: 0x00003F70 File Offset: 0x00002170
	// (remove) Token: 0x060000A5 RID: 165 RVA: 0x00003FA8 File Offset: 0x000021A8
	public event FocusEventHandler GotFocus;

	// Token: 0x14000006 RID: 6
	// (add) Token: 0x060000A6 RID: 166 RVA: 0x00003FE0 File Offset: 0x000021E0
	// (remove) Token: 0x060000A7 RID: 167 RVA: 0x00004018 File Offset: 0x00002218
	public event FocusEventHandler EnterFocus;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x060000A8 RID: 168 RVA: 0x00004050 File Offset: 0x00002250
	// (remove) Token: 0x060000A9 RID: 169 RVA: 0x00004088 File Offset: 0x00002288
	public event FocusEventHandler LostFocus;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x060000AA RID: 170 RVA: 0x000040C0 File Offset: 0x000022C0
	// (remove) Token: 0x060000AB RID: 171 RVA: 0x000040F8 File Offset: 0x000022F8
	public event FocusEventHandler LeaveFocus;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x060000AC RID: 172 RVA: 0x00004130 File Offset: 0x00002330
	// (remove) Token: 0x060000AD RID: 173 RVA: 0x00004168 File Offset: 0x00002368
	public event PropertyChangedEventHandler<bool> ControlShown;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x060000AE RID: 174 RVA: 0x000041A0 File Offset: 0x000023A0
	// (remove) Token: 0x060000AF RID: 175 RVA: 0x000041D8 File Offset: 0x000023D8
	public event PropertyChangedEventHandler<bool> ControlHidden;

	// Token: 0x1400000B RID: 11
	// (add) Token: 0x060000B0 RID: 176 RVA: 0x00004210 File Offset: 0x00002410
	// (remove) Token: 0x060000B1 RID: 177 RVA: 0x00004248 File Offset: 0x00002448
	public event PropertyChangedEventHandler<bool> ControlClippingChanged;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x060000B2 RID: 178 RVA: 0x00004280 File Offset: 0x00002480
	// (remove) Token: 0x060000B3 RID: 179 RVA: 0x000042B8 File Offset: 0x000024B8
	public event PropertyChangedEventHandler<int> TabIndexChanged;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x060000B4 RID: 180 RVA: 0x000042F0 File Offset: 0x000024F0
	// (remove) Token: 0x060000B5 RID: 181 RVA: 0x00004328 File Offset: 0x00002528
	public event PropertyChangedEventHandler<Vector2> PositionChanged;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x060000B6 RID: 182 RVA: 0x00004360 File Offset: 0x00002560
	// (remove) Token: 0x060000B7 RID: 183 RVA: 0x00004398 File Offset: 0x00002598
	public event PropertyChangedEventHandler<Vector2> SizeChanged;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x060000B8 RID: 184 RVA: 0x000043D0 File Offset: 0x000025D0
	// (remove) Token: 0x060000B9 RID: 185 RVA: 0x00004408 File Offset: 0x00002608
	[HideInInspector]
	public event PropertyChangedEventHandler<Color32> ColorChanged;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x060000BA RID: 186 RVA: 0x00004440 File Offset: 0x00002640
	// (remove) Token: 0x060000BB RID: 187 RVA: 0x00004478 File Offset: 0x00002678
	public event PropertyChangedEventHandler<bool> IsVisibleChanged;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x060000BC RID: 188 RVA: 0x000044B0 File Offset: 0x000026B0
	// (remove) Token: 0x060000BD RID: 189 RVA: 0x000044E8 File Offset: 0x000026E8
	public event PropertyChangedEventHandler<bool> IsEnabledChanged;

	// Token: 0x14000012 RID: 18
	// (add) Token: 0x060000BE RID: 190 RVA: 0x00004520 File Offset: 0x00002720
	// (remove) Token: 0x060000BF RID: 191 RVA: 0x00004558 File Offset: 0x00002758
	[HideInInspector]
	public event PropertyChangedEventHandler<float> OpacityChanged;

	// Token: 0x14000013 RID: 19
	// (add) Token: 0x060000C0 RID: 192 RVA: 0x00004590 File Offset: 0x00002790
	// (remove) Token: 0x060000C1 RID: 193 RVA: 0x000045C8 File Offset: 0x000027C8
	[HideInInspector]
	public event PropertyChangedEventHandler<dfAnchorStyle> AnchorChanged;

	// Token: 0x14000014 RID: 20
	// (add) Token: 0x060000C2 RID: 194 RVA: 0x00004600 File Offset: 0x00002800
	// (remove) Token: 0x060000C3 RID: 195 RVA: 0x00004638 File Offset: 0x00002838
	[HideInInspector]
	public event PropertyChangedEventHandler<dfPivotPoint> PivotChanged;

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x060000C4 RID: 196 RVA: 0x00004670 File Offset: 0x00002870
	// (remove) Token: 0x060000C5 RID: 197 RVA: 0x000046A8 File Offset: 0x000028A8
	[HideInInspector]
	public event PropertyChangedEventHandler<int> ZOrderChanged;

	// Token: 0x14000016 RID: 22
	// (add) Token: 0x060000C6 RID: 198 RVA: 0x000046E0 File Offset: 0x000028E0
	// (remove) Token: 0x060000C7 RID: 199 RVA: 0x00004718 File Offset: 0x00002918
	public event DragEventHandler DragStart;

	// Token: 0x14000017 RID: 23
	// (add) Token: 0x060000C8 RID: 200 RVA: 0x00004750 File Offset: 0x00002950
	// (remove) Token: 0x060000C9 RID: 201 RVA: 0x00004788 File Offset: 0x00002988
	public event DragEventHandler DragEnd;

	// Token: 0x14000018 RID: 24
	// (add) Token: 0x060000CA RID: 202 RVA: 0x000047C0 File Offset: 0x000029C0
	// (remove) Token: 0x060000CB RID: 203 RVA: 0x000047F8 File Offset: 0x000029F8
	public event DragEventHandler DragDrop;

	// Token: 0x14000019 RID: 25
	// (add) Token: 0x060000CC RID: 204 RVA: 0x00004830 File Offset: 0x00002A30
	// (remove) Token: 0x060000CD RID: 205 RVA: 0x00004868 File Offset: 0x00002A68
	public event DragEventHandler DragEnter;

	// Token: 0x1400001A RID: 26
	// (add) Token: 0x060000CE RID: 206 RVA: 0x000048A0 File Offset: 0x00002AA0
	// (remove) Token: 0x060000CF RID: 207 RVA: 0x000048D8 File Offset: 0x00002AD8
	public event DragEventHandler DragLeave;

	// Token: 0x1400001B RID: 27
	// (add) Token: 0x060000D0 RID: 208 RVA: 0x00004910 File Offset: 0x00002B10
	// (remove) Token: 0x060000D1 RID: 209 RVA: 0x00004948 File Offset: 0x00002B48
	public event DragEventHandler DragOver;

	// Token: 0x1400001C RID: 28
	// (add) Token: 0x060000D2 RID: 210 RVA: 0x00004980 File Offset: 0x00002B80
	// (remove) Token: 0x060000D3 RID: 211 RVA: 0x000049B8 File Offset: 0x00002BB8
	public event KeyPressHandler KeyPress;

	// Token: 0x1400001D RID: 29
	// (add) Token: 0x060000D4 RID: 212 RVA: 0x000049F0 File Offset: 0x00002BF0
	// (remove) Token: 0x060000D5 RID: 213 RVA: 0x00004A28 File Offset: 0x00002C28
	public event KeyPressHandler KeyDown;

	// Token: 0x1400001E RID: 30
	// (add) Token: 0x060000D6 RID: 214 RVA: 0x00004A60 File Offset: 0x00002C60
	// (remove) Token: 0x060000D7 RID: 215 RVA: 0x00004A98 File Offset: 0x00002C98
	public event KeyPressHandler KeyUp;

	// Token: 0x1400001F RID: 31
	// (add) Token: 0x060000D8 RID: 216 RVA: 0x00004AD0 File Offset: 0x00002CD0
	// (remove) Token: 0x060000D9 RID: 217 RVA: 0x00004B08 File Offset: 0x00002D08
	public event ControlMultiTouchEventHandler MultiTouch;

	// Token: 0x14000020 RID: 32
	// (add) Token: 0x060000DA RID: 218 RVA: 0x00004B40 File Offset: 0x00002D40
	// (remove) Token: 0x060000DB RID: 219 RVA: 0x00004B78 File Offset: 0x00002D78
	public event ControlCallbackHandler MultiTouchEnd;

	// Token: 0x14000021 RID: 33
	// (add) Token: 0x060000DC RID: 220 RVA: 0x00004BB0 File Offset: 0x00002DB0
	// (remove) Token: 0x060000DD RID: 221 RVA: 0x00004BE8 File Offset: 0x00002DE8
	public event MouseEventHandler MouseEnter;

	// Token: 0x14000022 RID: 34
	// (add) Token: 0x060000DE RID: 222 RVA: 0x00004C20 File Offset: 0x00002E20
	// (remove) Token: 0x060000DF RID: 223 RVA: 0x00004C58 File Offset: 0x00002E58
	public event MouseEventHandler MouseMove;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x060000E0 RID: 224 RVA: 0x00004C90 File Offset: 0x00002E90
	// (remove) Token: 0x060000E1 RID: 225 RVA: 0x00004CC8 File Offset: 0x00002EC8
	public event MouseEventHandler MouseHover;

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x060000E2 RID: 226 RVA: 0x00004D00 File Offset: 0x00002F00
	// (remove) Token: 0x060000E3 RID: 227 RVA: 0x00004D38 File Offset: 0x00002F38
	public event MouseEventHandler MouseLeave;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x060000E4 RID: 228 RVA: 0x00004D70 File Offset: 0x00002F70
	// (remove) Token: 0x060000E5 RID: 229 RVA: 0x00004DA8 File Offset: 0x00002FA8
	public event MouseEventHandler MouseDown;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x060000E6 RID: 230 RVA: 0x00004DE0 File Offset: 0x00002FE0
	// (remove) Token: 0x060000E7 RID: 231 RVA: 0x00004E18 File Offset: 0x00003018
	public event MouseEventHandler MouseUp;

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x060000E8 RID: 232 RVA: 0x00004E50 File Offset: 0x00003050
	// (remove) Token: 0x060000E9 RID: 233 RVA: 0x00004E88 File Offset: 0x00003088
	public event MouseEventHandler MouseWheel;

	// Token: 0x14000028 RID: 40
	// (add) Token: 0x060000EA RID: 234 RVA: 0x00004EC0 File Offset: 0x000030C0
	// (remove) Token: 0x060000EB RID: 235 RVA: 0x00004EF8 File Offset: 0x000030F8
	public event MouseEventHandler Click;

	// Token: 0x14000029 RID: 41
	// (add) Token: 0x060000EC RID: 236 RVA: 0x00004F30 File Offset: 0x00003130
	// (remove) Token: 0x060000ED RID: 237 RVA: 0x00004F68 File Offset: 0x00003168
	public event MouseEventHandler DoubleClick;

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x060000EE RID: 238 RVA: 0x00004F9D File Offset: 0x0000319D
	// (set) Token: 0x060000EF RID: 239 RVA: 0x00004FA5 File Offset: 0x000031A5
	public bool CustomWordWrapAllowed
	{
		get
		{
			return this._customWordWrapAllowed;
		}
		set
		{
			this._customWordWrapAllowed = value;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004FAE File Offset: 0x000031AE
	// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004FB6 File Offset: 0x000031B6
	public bool AllowSignalEvents
	{
		get
		{
			return this.allowSignalEvents;
		}
		set
		{
			this.allowSignalEvents = value;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004FBF File Offset: 0x000031BF
	internal bool IsInvalid
	{
		get
		{
			return this.isControlInvalidated;
		}
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004FC7 File Offset: 0x000031C7
	internal bool IsControlClipped
	{
		get
		{
			return this.isControlClipped;
		}
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004FCF File Offset: 0x000031CF
	public dfGUIManager GUIManager
	{
		get
		{
			return this.GetManager();
		}
	}

	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004FD8 File Offset: 0x000031D8
	// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005036 File Offset: 0x00003236
	public bool IsEnabled
	{
		get
		{
			if (!base.enabled)
			{
				return false;
			}
			if (base.gameObject != null && !base.gameObject.activeSelf)
			{
				return false;
			}
			if (!(this.parent != null))
			{
				return this.isEnabled;
			}
			return this.isEnabled && this.parent.IsEnabled;
		}
		set
		{
			if (value != this.isEnabled)
			{
				this.isEnabled = value;
				this.OnIsEnabledChanged();
			}
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000504E File Offset: 0x0000324E
	// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000507C File Offset: 0x0000327C
	[SerializeField]
	public bool IsVisible
	{
		get
		{
			if (!(this.parent == null))
			{
				return this.isVisible && this.parent.IsVisible;
			}
			return this.isVisible;
		}
		set
		{
			if (value != this.isVisible)
			{
				if (Application.isPlaying && !this.IsInteractive)
				{
					base.GetComponent<Collider>().enabled = false;
				}
				else
				{
					base.GetComponent<Collider>().enabled = value;
				}
				this.isVisible = value;
				this.OnIsVisibleChanged();
			}
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060000F9 RID: 249 RVA: 0x000050C8 File Offset: 0x000032C8
	// (set) Token: 0x060000FA RID: 250 RVA: 0x000050D0 File Offset: 0x000032D0
	public virtual bool IsInteractive
	{
		get
		{
			return this.isInteractive;
		}
		set
		{
			if (this.HasFocus && !value)
			{
				dfGUIManager.SetFocus(null);
			}
			this.isInteractive = value;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060000FB RID: 251 RVA: 0x000050EA File Offset: 0x000032EA
	// (set) Token: 0x060000FC RID: 252 RVA: 0x000050F2 File Offset: 0x000032F2
	[SerializeField]
	public string Tooltip
	{
		get
		{
			return this.tooltip;
		}
		set
		{
			if (value != this.tooltip)
			{
				this.tooltip = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060000FD RID: 253 RVA: 0x0000510F File Offset: 0x0000330F
	// (set) Token: 0x060000FE RID: 254 RVA: 0x0000511D File Offset: 0x0000331D
	[SerializeField]
	public dfAnchorStyle Anchor
	{
		get
		{
			this.ensureLayoutExists();
			return this.anchorStyle;
		}
		set
		{
			if (value != this.anchorStyle)
			{
				this.anchorStyle = value;
				this.OnAnchorChanged();
			}
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x060000FF RID: 255 RVA: 0x00005135 File Offset: 0x00003335
	// (set) Token: 0x06000100 RID: 256 RVA: 0x0000514C File Offset: 0x0000334C
	public float Opacity
	{
		get
		{
			return (float)this.color.a / 255f;
		}
		set
		{
			value = Mathf.Max(0f, Mathf.Min(1f, value));
			float b = (float)this.color.a / 255f;
			if (!Mathf.Approximately(value, b))
			{
				this.color.a = (byte)(value * 255f);
				this.OnOpacityChanged();
			}
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000101 RID: 257 RVA: 0x000051A5 File Offset: 0x000033A5
	// (set) Token: 0x06000102 RID: 258 RVA: 0x000051AD File Offset: 0x000033AD
	public Color32 Color
	{
		get
		{
			return this.color;
		}
		set
		{
			value.a = (byte)(this.Opacity * 255f);
			if (!this.color.Equals(value))
			{
				this.color = value;
				this.OnColorChanged();
			}
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000103 RID: 259 RVA: 0x000051E9 File Offset: 0x000033E9
	// (set) Token: 0x06000104 RID: 260 RVA: 0x000051F1 File Offset: 0x000033F1
	public Color32 DisabledColor
	{
		get
		{
			return this.disabledColor;
		}
		set
		{
			if (!value.Equals(this.disabledColor))
			{
				this.disabledColor = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000105 RID: 261 RVA: 0x0000521A File Offset: 0x0000341A
	// (set) Token: 0x06000106 RID: 262 RVA: 0x00005224 File Offset: 0x00003424
	public dfPivotPoint Pivot
	{
		get
		{
			return this.pivot;
		}
		set
		{
			if (value != this.pivot)
			{
				Vector3 position = this.Position;
				this.pivot = value;
				Vector3 b = this.Position - position;
				this.SuspendLayout();
				this.Position = position;
				for (int i = 0; i < this.controls.Count; i++)
				{
					this.controls[i].Position += b;
				}
				this.ResumeLayout();
				this.OnPivotChanged();
			}
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000107 RID: 263 RVA: 0x000052A1 File Offset: 0x000034A1
	// (set) Token: 0x06000108 RID: 264 RVA: 0x000052A9 File Offset: 0x000034A9
	public Vector3 RelativePosition
	{
		get
		{
			return this.getRelativePosition();
		}
		set
		{
			this.setRelativePosition(ref value);
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000109 RID: 265 RVA: 0x000052B3 File Offset: 0x000034B3
	// (set) Token: 0x0600010A RID: 266 RVA: 0x000052E1 File Offset: 0x000034E1
	public Vector3 Position
	{
		get
		{
			return base.transform.localPosition / this.PixelsToUnits() + this.pivot.TransformToUpperLeft(this.Size);
		}
		set
		{
			this.setPositionInternal(value);
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600010B RID: 267 RVA: 0x000052EA File Offset: 0x000034EA
	// (set) Token: 0x0600010C RID: 268 RVA: 0x000052F4 File Offset: 0x000034F4
	public Vector2 Size
	{
		get
		{
			return this.size;
		}
		set
		{
			value = Vector2.Max(this.CalculateMinimumSize(), value);
			value.x = ((this.maxSize.x > 0f) ? Mathf.Min(value.x, this.maxSize.x) : value.x);
			value.y = ((this.maxSize.y > 0f) ? Mathf.Min(value.y, this.maxSize.y) : value.y);
			if ((value - this.size).sqrMagnitude <= 1f)
			{
				return;
			}
			this.size = value;
			this.OnSizeChanged();
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x0600010D RID: 269 RVA: 0x000053A6 File Offset: 0x000035A6
	// (set) Token: 0x0600010E RID: 270 RVA: 0x000053B3 File Offset: 0x000035B3
	public float Width
	{
		get
		{
			return this.size.x;
		}
		set
		{
			this.Size = new Vector2(value, this.size.y);
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600010F RID: 271 RVA: 0x000053CC File Offset: 0x000035CC
	// (set) Token: 0x06000110 RID: 272 RVA: 0x000053D9 File Offset: 0x000035D9
	public float Height
	{
		get
		{
			return this.size.y;
		}
		set
		{
			this.Size = new Vector2(this.size.x, value);
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000111 RID: 273 RVA: 0x000053F2 File Offset: 0x000035F2
	// (set) Token: 0x06000112 RID: 274 RVA: 0x000053FA File Offset: 0x000035FA
	public Vector2 MinimumSize
	{
		get
		{
			return this.minSize;
		}
		set
		{
			value = Vector2.Max(Vector2.zero, value.RoundToInt());
			if (value != this.minSize)
			{
				this.minSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000113 RID: 275 RVA: 0x00005429 File Offset: 0x00003629
	// (set) Token: 0x06000114 RID: 276 RVA: 0x00005431 File Offset: 0x00003631
	public Vector2 MaximumSize
	{
		get
		{
			return this.maxSize;
		}
		set
		{
			value = Vector2.Max(Vector2.zero, value.RoundToInt());
			if (value != this.maxSize)
			{
				this.maxSize = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000115 RID: 277 RVA: 0x00005460 File Offset: 0x00003660
	// (set) Token: 0x06000116 RID: 278 RVA: 0x00005468 File Offset: 0x00003668
	[HideInInspector]
	public int ZOrder
	{
		get
		{
			return this.zindex;
		}
		set
		{
			if (value != this.zindex)
			{
				if (this.parent != null)
				{
					this.parent.SetControlIndex(this, value);
				}
				else
				{
					this.zindex = Mathf.Max(-1, value);
				}
				this.OnZOrderChanged();
			}
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000117 RID: 279 RVA: 0x000054A3 File Offset: 0x000036A3
	// (set) Token: 0x06000118 RID: 280 RVA: 0x000054AB File Offset: 0x000036AB
	[HideInInspector]
	public int TabIndex
	{
		get
		{
			return this.tabIndex;
		}
		set
		{
			if (value != this.tabIndex)
			{
				this.tabIndex = Mathf.Max(-1, value);
				this.OnTabIndexChanged();
			}
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000119 RID: 281 RVA: 0x000054C9 File Offset: 0x000036C9
	public dfList<dfControl> Controls
	{
		get
		{
			return this.controls;
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600011A RID: 282 RVA: 0x000054D1 File Offset: 0x000036D1
	public dfControl Parent
	{
		get
		{
			return this.parent;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600011B RID: 283 RVA: 0x000054D9 File Offset: 0x000036D9
	// (set) Token: 0x0600011C RID: 284 RVA: 0x000054E1 File Offset: 0x000036E1
	public bool ClipChildren
	{
		get
		{
			return this.clipChildren;
		}
		set
		{
			if (value != this.clipChildren)
			{
				this.clipChildren = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600011D RID: 285 RVA: 0x000054F9 File Offset: 0x000036F9
	protected bool IsLayoutSuspended
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsLayoutSuspended);
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x0600011E RID: 286 RVA: 0x0000551A File Offset: 0x0000371A
	protected bool IsPerformingLayout
	{
		get
		{
			return this.performingLayout || (this.layout != null && this.layout.IsPerformingLayout);
		}
	}

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x0600011F RID: 287 RVA: 0x0000553E File Offset: 0x0000373E
	// (set) Token: 0x06000120 RID: 288 RVA: 0x00005546 File Offset: 0x00003746
	public object Tag
	{
		get
		{
			return this.tag;
		}
		set
		{
			this.tag = value;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x06000121 RID: 289 RVA: 0x0000554F File Offset: 0x0000374F
	internal uint Version
	{
		get
		{
			return this.version;
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x06000122 RID: 290 RVA: 0x00005557 File Offset: 0x00003757
	// (set) Token: 0x06000123 RID: 291 RVA: 0x0000555F File Offset: 0x0000375F
	public bool IsLocalized
	{
		get
		{
			return this.isLocalized;
		}
		set
		{
			this.isLocalized = value;
			if (value)
			{
				this.Localize();
			}
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x06000124 RID: 292 RVA: 0x00005571 File Offset: 0x00003771
	// (set) Token: 0x06000125 RID: 293 RVA: 0x00005579 File Offset: 0x00003779
	public Vector2 HotZoneScale
	{
		get
		{
			return this.hotZoneScale;
		}
		set
		{
			this.hotZoneScale = Vector2.Max(value, Vector2.zero);
			this.Invalidate();
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x06000126 RID: 294 RVA: 0x00005592 File Offset: 0x00003792
	// (set) Token: 0x06000127 RID: 295 RVA: 0x0000559A File Offset: 0x0000379A
	public bool AutoFocus
	{
		get
		{
			return this.autoFocus;
		}
		set
		{
			if (value != this.autoFocus)
			{
				this.autoFocus = value;
				if (value && this.IsEnabled && this.CanFocus)
				{
					this.Focus();
				}
			}
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06000128 RID: 296 RVA: 0x000055C5 File Offset: 0x000037C5
	// (set) Token: 0x06000129 RID: 297 RVA: 0x000055D7 File Offset: 0x000037D7
	public virtual bool CanFocus
	{
		get
		{
			return this.canFocus && this.IsInteractive;
		}
		set
		{
			this.canFocus = value;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x0600012A RID: 298 RVA: 0x000055E0 File Offset: 0x000037E0
	public virtual bool ContainsFocus
	{
		get
		{
			return dfGUIManager.ContainsFocus(this);
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600012B RID: 299 RVA: 0x000055E8 File Offset: 0x000037E8
	public virtual bool HasFocus
	{
		get
		{
			return dfGUIManager.HasFocus(this);
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x0600012C RID: 300 RVA: 0x000055F0 File Offset: 0x000037F0
	public bool ContainsMouse
	{
		get
		{
			return this.isMouseHovering;
		}
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000055F8 File Offset: 0x000037F8
	internal void setRenderOrder(ref int order)
	{
		int num = order + 1;
		order = num;
		this.renderOrder = num;
		int count = this.controls.Count;
		dfControl[] items = this.controls.Items;
		for (int i = 0; i < count; i++)
		{
			if (items[i] != null)
			{
				items[i].setRenderOrder(ref order);
			}
		}
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x0600012E RID: 302 RVA: 0x0000564C File Offset: 0x0000384C
	[HideInInspector]
	public int RenderOrder
	{
		get
		{
			return this.renderOrder;
		}
	}

	// Token: 0x0600012F RID: 303 RVA: 0x00005654 File Offset: 0x00003854
	internal virtual void OnDragStart(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragStart", this, args);
			if (!args.Used && this.DragStart != null)
			{
				this.DragStart(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragStart(args);
		}
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000056B0 File Offset: 0x000038B0
	internal virtual void OnDragEnd(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragEnd", this, args);
			if (!args.Used && this.DragEnd != null)
			{
				this.DragEnd(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragEnd(args);
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000570C File Offset: 0x0000390C
	internal virtual void OnDragDrop(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragDrop", this, args);
			if (!args.Used && this.DragDrop != null)
			{
				this.DragDrop(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragDrop(args);
		}
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00005768 File Offset: 0x00003968
	internal virtual void OnDragEnter(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragEnter", this, args);
			if (!args.Used && this.DragEnter != null)
			{
				this.DragEnter(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragEnter(args);
		}
	}

	// Token: 0x06000133 RID: 307 RVA: 0x000057C4 File Offset: 0x000039C4
	internal virtual void OnDragLeave(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragLeave", this, args);
			if (!args.Used && this.DragLeave != null)
			{
				this.DragLeave(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragLeave(args);
		}
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00005820 File Offset: 0x00003A20
	internal virtual void OnDragOver(dfDragEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDragOver", this, args);
			if (!args.Used && this.DragOver != null)
			{
				this.DragOver(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDragOver(args);
		}
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000587A File Offset: 0x00003A7A
	protected internal virtual void OnMultiTouchEnd()
	{
		this.Signal("OnMultiTouchEnd", this);
		if (this.MultiTouchEnd != null)
		{
			this.MultiTouchEnd(this);
		}
		if (this.parent != null)
		{
			this.parent.OnMultiTouchEnd();
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x000058B8 File Offset: 0x00003AB8
	protected internal virtual void OnMultiTouch(dfTouchEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMultiTouch", this, args);
			if (this.MultiTouch != null)
			{
				this.MultiTouch(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMultiTouch(args);
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000590C File Offset: 0x00003B0C
	protected internal virtual void OnMouseEnter(dfMouseEventArgs args)
	{
		this.isMouseHovering = true;
		if (!args.Used)
		{
			this.Signal("OnMouseEnter", this, args);
			if (this.MouseEnter != null)
			{
				this.MouseEnter(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseEnter(args);
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00005968 File Offset: 0x00003B68
	protected internal virtual void OnMouseLeave(dfMouseEventArgs args)
	{
		this.isMouseHovering = false;
		if (!args.Used)
		{
			this.Signal("OnMouseLeave", this, args);
			if (this.MouseLeave != null)
			{
				this.MouseLeave(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseLeave(args);
		}
	}

	// Token: 0x06000139 RID: 313 RVA: 0x000059C4 File Offset: 0x00003BC4
	protected internal virtual void OnMouseMove(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseMove", this, args);
			if (this.MouseMove != null)
			{
				this.MouseMove(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseMove(args);
		}
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00005A18 File Offset: 0x00003C18
	protected internal virtual void OnMouseHover(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseHover", this, args);
			if (this.MouseHover != null)
			{
				this.MouseHover(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseHover(args);
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00005A6C File Offset: 0x00003C6C
	protected internal virtual void OnMouseWheel(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseWheel", this, args);
			if (this.MouseWheel != null)
			{
				this.MouseWheel(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseWheel(args);
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00005AC0 File Offset: 0x00003CC0
	protected internal virtual void OnMouseDown(dfMouseEventArgs args)
	{
		if (this.IsInteractive && this.IsEnabled && this.IsVisible && this.CanFocus && !this.ContainsFocus)
		{
			this.Focus();
		}
		if (!args.Used)
		{
			this.Signal("OnMouseDown", this, args);
			if (this.MouseDown != null)
			{
				this.MouseDown(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseDown(args);
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00005B48 File Offset: 0x00003D48
	protected internal virtual void OnMouseUp(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnMouseUp", this, args);
			if (this.MouseUp != null)
			{
				this.MouseUp(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnMouseUp(args);
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00005B9C File Offset: 0x00003D9C
	protected internal virtual void OnClick(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnClick", this, args);
			if (this.Click != null)
			{
				this.Click(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnClick(args);
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00005BF0 File Offset: 0x00003DF0
	protected internal virtual void OnDoubleClick(dfMouseEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnDoubleClick", this, args);
			if (this.DoubleClick != null)
			{
				this.DoubleClick(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnDoubleClick(args);
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00005C44 File Offset: 0x00003E44
	protected internal virtual void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.IsInteractive && !args.Used)
		{
			this.Signal("OnKeyPress", this, args);
			if (this.KeyPress != null)
			{
				this.KeyPress(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyPress(args);
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00005CA0 File Offset: 0x00003EA0
	protected internal virtual void OnKeyDown(dfKeyEventArgs args)
	{
		if (this.IsInteractive && !args.Used)
		{
			if (args.KeyCode == KeyCode.Tab)
			{
				this.OnTabKeyPressed(args);
			}
			if (!args.Used)
			{
				this.Signal("OnKeyDown", this, args);
				if (this.KeyDown != null)
				{
					this.KeyDown(this, args);
				}
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyDown(args);
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00005D14 File Offset: 0x00003F14
	protected virtual void OnTabKeyPressed(dfKeyEventArgs args)
	{
		List<dfControl> list = (from c in this.GetManager().GetComponentsInChildren<dfControl>()
		where c != this && c.TabIndex >= 0 && c.IsInteractive && c.CanFocus && c.IsVisible
		select c).ToList<dfControl>();
		if (list.Count == 0)
		{
			return;
		}
		list.Sort(delegate(dfControl lhs, dfControl rhs)
		{
			if (lhs.TabIndex == rhs.TabIndex)
			{
				return lhs.RenderOrder.CompareTo(rhs.RenderOrder);
			}
			return lhs.TabIndex.CompareTo(rhs.TabIndex);
		});
		if (!args.Shift)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].TabIndex >= this.TabIndex)
				{
					list[i].Focus();
					args.Use();
					return;
				}
			}
			list[0].Focus();
			args.Use();
			return;
		}
		for (int j = list.Count - 1; j >= 0; j--)
		{
			if (list[j].TabIndex <= this.TabIndex)
			{
				list[j].Focus();
				args.Use();
				return;
			}
		}
		list[list.Count - 1].Focus();
		args.Use();
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00005E18 File Offset: 0x00004018
	protected internal virtual void OnKeyUp(dfKeyEventArgs args)
	{
		if (this.IsInteractive)
		{
			this.Signal("OnKeyUp", this, args);
			if (this.KeyUp != null)
			{
				this.KeyUp(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnKeyUp(args);
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00005E6A File Offset: 0x0000406A
	protected internal virtual void OnEnterFocus(dfFocusEventArgs args)
	{
		this.Signal("OnEnterFocus", this, args);
		if (this.EnterFocus != null)
		{
			this.EnterFocus(this, args);
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00005E8F File Offset: 0x0000408F
	protected internal virtual void OnLeaveFocus(dfFocusEventArgs args)
	{
		this.Signal("OnLeaveFocus", this, args);
		if (this.LeaveFocus != null)
		{
			this.LeaveFocus(this, args);
		}
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00005EB4 File Offset: 0x000040B4
	protected internal virtual void OnGotFocus(dfFocusEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnGotFocus", this, args);
			if (this.GotFocus != null)
			{
				this.GotFocus(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnGotFocus(args);
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00005F08 File Offset: 0x00004108
	protected internal virtual void OnLostFocus(dfFocusEventArgs args)
	{
		if (!args.Used)
		{
			this.Signal("OnLostFocus", this, args);
			if (this.LostFocus != null)
			{
				this.LostFocus(this, args);
			}
		}
		if (this.parent != null)
		{
			this.parent.OnLostFocus(args);
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00005F5A File Offset: 0x0000415A
	protected internal bool Signal(string eventName, object arg)
	{
		dfControl.signal1[0] = arg;
		return this.Signal(base.gameObject, eventName, dfControl.signal1);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00005F76 File Offset: 0x00004176
	protected internal bool Signal(string eventName, object arg1, object arg2)
	{
		dfControl.signal2[0] = arg1;
		dfControl.signal2[1] = arg2;
		return this.Signal(base.gameObject, eventName, dfControl.signal2);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00005F9A File Offset: 0x0000419A
	protected internal bool Signal(string eventName, object arg1, object arg2, object arg3)
	{
		dfControl.signal3[0] = arg1;
		dfControl.signal3[1] = arg2;
		dfControl.signal3[2] = arg3;
		return this.Signal(base.gameObject, eventName, dfControl.signal3);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x00005FC7 File Offset: 0x000041C7
	protected internal bool Signal(string eventName, object[] args)
	{
		return this.Signal(base.gameObject, eventName, args);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x00005FD8 File Offset: 0x000041D8
	protected internal bool SignalHierarchy(string eventName, params object[] args)
	{
		if (!this.allowSignalEvents)
		{
			return false;
		}
		bool flag = false;
		Transform transform = base.transform;
		while (!flag && transform != null)
		{
			flag = this.Signal(transform.gameObject, eventName, args);
			transform = transform.parent;
		}
		return flag;
	}

	// Token: 0x0600014D RID: 333 RVA: 0x0000601D File Offset: 0x0000421D
	[HideInInspector]
	protected internal bool Signal(GameObject target, string eventName, object arg)
	{
		dfControl.signal1[0] = arg;
		return this.Signal(target, eventName, dfControl.signal1);
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00006034 File Offset: 0x00004234
	[HideInInspector]
	protected internal bool Signal(GameObject target, string eventName, object[] args)
	{
		if (!this.allowSignalEvents || target == null || this.shutdownInProgress || !Application.isPlaying)
		{
			return false;
		}
		MonoBehaviour[] components = target.GetComponents<MonoBehaviour>();
		if (components == null || (target == base.gameObject && components.Length == 1))
		{
			return false;
		}
		if (args.Length == 0 || args[0] != this)
		{
			object[] array = new object[args.Length + 1];
			Array.Copy(args, 0, array, 1, args.Length);
			array[0] = this;
			args = array;
		}
		bool result = false;
		foreach (MonoBehaviour monoBehaviour in components)
		{
			if (!(monoBehaviour == null) && !(monoBehaviour.GetType() == null) && !(monoBehaviour == this) && (monoBehaviour == null || monoBehaviour.enabled))
			{
				object obj = null;
				if (dfControl.SignalCache.Invoke(monoBehaviour, eventName, args, out obj))
				{
					result = true;
					if (obj is IEnumerator && monoBehaviour != null)
					{
						monoBehaviour.StartCoroutine((IEnumerator)obj);
					}
				}
			}
		}
		return result;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x0000611F File Offset: 0x0000431F
	internal bool IsTopLevelControl(dfGUIManager manager)
	{
		return this.parent == null && this.cachedManager == manager;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000613D File Offset: 0x0000433D
	internal bool GetIsVisibleRaw()
	{
		return this.isVisible;
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00006145 File Offset: 0x00004345
	public void Localize()
	{
		if (!this.IsLocalized)
		{
			return;
		}
		if (this.languageManager == null)
		{
			this.languageManager = this.GetManager().GetComponent<dfLanguageManager>();
			if (this.languageManager == null)
			{
				return;
			}
		}
		this.OnLocalize();
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00006184 File Offset: 0x00004384
	public void DoClick()
	{
		Camera camera = this.GetCamera();
		Vector3 vector = camera.WorldToScreenPoint(this.GetCenter());
		Ray ray = camera.ScreenPointToRay(vector);
		this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, ray, vector, 0f));
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000061C8 File Offset: 0x000043C8
	[HideInInspector]
	protected internal void RemoveEventHandlers(string eventName)
	{
		FieldInfo fieldInfo = base.GetType().GetAllFields().FirstOrDefault((FieldInfo f) => typeof(Delegate).IsAssignableFrom(f.FieldType) && f.Name == eventName);
		if (fieldInfo != null)
		{
			fieldInfo.SetValue(this, null);
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x00006210 File Offset: 0x00004410
	[HideInInspector]
	internal void RemoveAllEventHandlers()
	{
		FieldInfo[] array = (from f in base.GetType().GetAllFields()
		where typeof(Delegate).IsAssignableFrom(f.FieldType)
		select f).ToArray<FieldInfo>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetValue(this, null);
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000626A File Offset: 0x0000446A
	public void Show()
	{
		this.IsVisible = true;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00006273 File Offset: 0x00004473
	public void Hide()
	{
		this.IsVisible = false;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0000627C File Offset: 0x0000447C
	public void Enable()
	{
		this.IsEnabled = true;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00006285 File Offset: 0x00004485
	public void Disable()
	{
		this.IsEnabled = false;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00006290 File Offset: 0x00004490
	public bool HitTest(Ray ray)
	{
		Plane plane = new Plane(base.transform.TransformDirection(Vector3.back), base.transform.position);
		float d = 0f;
		if (!plane.Raycast(ray, out d))
		{
			return false;
		}
		Vector3 point = ray.origin + ray.direction * d;
		Plane[] array = this.ClipChildren ? this.GetClippingPlanes() : null;
		if (array != null && array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].GetSide(point))
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000632C File Offset: 0x0000452C
	public Vector2 GetHitPosition(Ray ray)
	{
		Vector2 result;
		if (!this.GetHitPosition(ray, out result, false))
		{
			return Vector2.one * float.MinValue;
		}
		return result;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00006356 File Offset: 0x00004556
	public bool GetHitPosition(Ray ray, out Vector2 position)
	{
		return this.GetHitPosition(ray, out position, true);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00006364 File Offset: 0x00004564
	public bool GetHitPosition(Ray ray, out Vector2 position, bool clamp)
	{
		position = Vector2.one * float.MinValue;
		Plane plane = new Plane(base.transform.TransformDirection(Vector3.back), base.transform.position);
		float distance = 0f;
		if (!plane.Raycast(ray, out distance))
		{
			return false;
		}
		Vector3 point = ray.GetPoint(distance);
		Plane[] array = this.ClipChildren ? this.GetClippingPlanes() : null;
		if (array != null && array.Length != 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].GetSide(point))
				{
					return false;
				}
			}
		}
		Vector3[] corners = this.GetCorners();
		Vector3 vector = corners[0];
		Vector3 vector2 = corners[1];
		Vector3 vector3 = corners[2];
		float num = (dfControl.closestPointOnLine(vector, vector2, point, clamp) - vector).magnitude / (vector2 - vector).magnitude;
		float x = this.size.x * num;
		num = (dfControl.closestPointOnLine(vector, vector3, point, clamp) - vector).magnitude / (vector3 - vector).magnitude;
		float y = this.size.y * num;
		position = new Vector2(x, y);
		return true;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000064B4 File Offset: 0x000046B4
	public T Find<T>(string controlName) where T : dfControl
	{
		if (base.name == controlName && this is T)
		{
			return (T)((object)this);
		}
		this.updateControlHierarchy(true);
		for (int i = 0; i < this.controls.Count; i++)
		{
			T t = this.controls[i] as T;
			if (t != null && t.name == controlName)
			{
				return t;
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			T t2 = this.controls[j].Find<T>(controlName);
			if (t2 != null)
			{
				return t2;
			}
		}
		return default(T);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00006578 File Offset: 0x00004778
	public dfControl Find(string controlName)
	{
		if (base.name == controlName)
		{
			return this;
		}
		this.updateControlHierarchy(true);
		for (int i = 0; i < this.controls.Count; i++)
		{
			dfControl dfControl = this.controls[i];
			if (dfControl.name == controlName)
			{
				return dfControl;
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j].Find(controlName);
			if (dfControl2 != null)
			{
				return dfControl2;
			}
		}
		return null;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00006604 File Offset: 0x00004804
	public void Focus()
	{
		if (!this.CanFocus || this.HasFocus || !this.IsEnabled || !this.IsVisible)
		{
			return;
		}
		dfGUIManager.SetFocus(this);
		this.Invalidate();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00006633 File Offset: 0x00004833
	public void Unfocus()
	{
		if (this.ContainsFocus)
		{
			dfGUIManager.SetFocus(null);
		}
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00006644 File Offset: 0x00004844
	public dfControl GetRootContainer()
	{
		dfControl dfControl = this;
		while (dfControl.Parent != null)
		{
			dfControl = dfControl.Parent;
		}
		return dfControl;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000666B File Offset: 0x0000486B
	public virtual void BringToFront()
	{
		if (this.parent == null)
		{
			this.GetManager().BringToFront(this);
		}
		else
		{
			this.parent.SetControlIndex(this, int.MaxValue);
		}
		this.Invalidate();
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000066A0 File Offset: 0x000048A0
	public virtual void SendToBack()
	{
		if (this.parent == null)
		{
			this.GetManager().SendToBack(this);
		}
		else
		{
			this.parent.SetControlIndex(this, 0);
		}
		this.Invalidate();
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000066D4 File Offset: 0x000048D4
	internal dfRenderData Render()
	{
		if (this.rendering)
		{
			return this.renderData;
		}
		dfRenderData result;
		try
		{
			this.rendering = true;
			bool flag = this.isVisible;
			bool flag2 = base.enabled && base.gameObject.activeSelf;
			if (!flag || !flag2)
			{
				result = null;
			}
			else
			{
				if (this.renderData == null)
				{
					this.renderData = dfRenderData.Obtain();
					this.isControlInvalidated = true;
				}
				if (this.isControlInvalidated)
				{
					this.renderData.Clear();
					this.OnRebuildRenderData();
					this.updateCollider();
				}
				this.renderData.Transform = base.transform.localToWorldMatrix;
				result = this.renderData;
			}
		}
		finally
		{
			this.isControlInvalidated = false;
			this.rendering = false;
		}
		return result;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00006798 File Offset: 0x00004998
	[HideInInspector]
	public virtual void Invalidate()
	{
		if (this.shutdownInProgress)
		{
			return;
		}
		this.updateVersion();
		this.isControlInvalidated = true;
		this.cachedBounds = null;
		dfGUIManager manager = this.GetManager();
		if (manager != null)
		{
			manager.Invalidate();
		}
		dfRenderGroup.InvalidateGroupForControl(this);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x000067E3 File Offset: 0x000049E3
	[HideInInspector]
	public void ResetLayout()
	{
		this.ResetLayout(false, false);
	}

	// Token: 0x06000167 RID: 359 RVA: 0x000067F0 File Offset: 0x000049F0
	[HideInInspector]
	public void ResetLayout(bool recursive, bool force)
	{
		if (this.shutdownInProgress)
		{
			return;
		}
		bool flag = this.IsPerformingLayout || this.IsLayoutSuspended;
		if (!force && flag)
		{
			return;
		}
		if (this.layout == null)
		{
			this.layout = new dfControl.AnchorLayout(this.anchorStyle, this);
		}
		else
		{
			this.layout.Attach(this);
			this.layout.Reset(true);
		}
		if (recursive)
		{
			int count = this.controls.Count;
			dfControl[] items = this.controls.Items;
			for (int i = 0; i < count; i++)
			{
				items[i].ResetLayout();
			}
		}
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00006884 File Offset: 0x00004A84
	[HideInInspector]
	public void PerformLayout()
	{
		if (this.shutdownInProgress)
		{
			return;
		}
		if (this.isDisposing || this.performingLayout)
		{
			return;
		}
		try
		{
			this.performingLayout = true;
			this.ensureLayoutExists();
			this.layout.PerformLayout();
			this.Invalidate();
		}
		finally
		{
			this.performingLayout = false;
		}
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000068E4 File Offset: 0x00004AE4
	[HideInInspector]
	public void SuspendLayout()
	{
		this.ensureLayoutExists();
		this.layout.SuspendLayout();
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].SuspendLayout();
		}
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000692C File Offset: 0x00004B2C
	[HideInInspector]
	public void ResumeLayout()
	{
		this.ensureLayoutExists();
		this.layout.ResumeLayout();
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].ResumeLayout();
		}
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00006971 File Offset: 0x00004B71
	public virtual Vector2 CalculateMinimumSize()
	{
		return this.MinimumSize;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00006979 File Offset: 0x00004B79
	[HideInInspector]
	public void MakePixelPerfect()
	{
		this.MakePixelPerfect(true);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00006984 File Offset: 0x00004B84
	[HideInInspector]
	public void MakePixelPerfect(bool recursive)
	{
		this.size = this.size.RoundToInt();
		float d = this.PixelsToUnits();
		base.transform.position = (base.transform.position / d).RoundToInt() * d;
		this.cachedPosition = base.transform.localPosition;
		int num = 0;
		while (num < this.controls.Count && recursive)
		{
			this.controls[num].MakePixelPerfect();
			num++;
		}
		this.Invalidate();
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00006A14 File Offset: 0x00004C14
	public Bounds GetBounds()
	{
		if (this.isInteractive && base.GetComponent<Collider>() != null)
		{
			this.cachedBounds = new Bounds?(base.GetComponent<Collider>().bounds);
			return this.cachedBounds.Value;
		}
		if (this.cachedBounds != null)
		{
			return this.cachedBounds.Value;
		}
		Vector3[] corners = this.GetCorners();
		Vector3 vector = corners[0] + (corners[3] - corners[0]) * 0.5f;
		Vector3 vector2 = vector;
		Vector3 vector3 = vector;
		for (int i = 0; i < corners.Length; i++)
		{
			vector2 = Vector3.Min(vector2, corners[i]);
			vector3 = Vector3.Max(vector3, corners[i]);
		}
		this.cachedBounds = new Bounds?(new Bounds(vector, vector3 - vector2));
		return this.cachedBounds.Value;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00006AFC File Offset: 0x00004CFC
	public Vector3 GetCenter()
	{
		Vector3[] corners = this.GetCorners();
		return corners[0] + (corners[3] - corners[0]) * 0.5f;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00006B3C File Offset: 0x00004D3C
	public Vector3 GetAbsolutePosition()
	{
		Vector3 vector = Vector3.zero;
		dfControl dfControl = this;
		while (dfControl != null)
		{
			vector += dfControl.getRelativePosition();
			dfControl = dfControl.Parent;
		}
		return vector;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00006B74 File Offset: 0x00004D74
	public Vector3[] GetCorners()
	{
		float d = this.PixelsToUnits();
		Matrix4x4 localToWorldMatrix = base.transform.localToWorldMatrix;
		Vector3 a = this.pivot.TransformToUpperLeft(this.size);
		Vector3 a2 = a + new Vector3(this.size.x, 0f);
		Vector3 a3 = a + new Vector3(0f, -this.size.y);
		Vector3 a4 = a2 + new Vector3(0f, -this.size.y);
		if (this.cachedCorners == null)
		{
			this.cachedCorners = new Vector3[4];
		}
		this.cachedCorners[0] = localToWorldMatrix.MultiplyPoint(a * d);
		this.cachedCorners[1] = localToWorldMatrix.MultiplyPoint(a2 * d);
		this.cachedCorners[2] = localToWorldMatrix.MultiplyPoint(a3 * d);
		this.cachedCorners[3] = localToWorldMatrix.MultiplyPoint(a4 * d);
		return this.cachedCorners;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00006C84 File Offset: 0x00004E84
	public Camera GetCamera()
	{
		dfGUIManager manager = this.GetManager();
		if (manager == null)
		{
			Debug.LogError("The Manager hosting this control could not be determined");
			return null;
		}
		return manager.RenderCamera;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00006CB3 File Offset: 0x00004EB3
	protected internal virtual RectOffset GetClipPadding()
	{
		return dfRectOffsetExtensions.Empty;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00006CBC File Offset: 0x00004EBC
	public Rect GetScreenRect()
	{
		Camera camera = this.GetCamera();
		Vector3[] corners = this.GetCorners();
		Vector2 vector = Vector2.one * float.MaxValue;
		Vector2 vector2 = Vector2.one * float.MinValue;
		int num = corners.Length;
		for (int i = 0; i < num; i++)
		{
			Vector3 v = camera.WorldToScreenPoint(corners[i]);
			vector = Vector2.Min(vector, v);
			vector2 = Vector2.Max(vector2, v);
		}
		return new Rect(vector.x, (float)Screen.height - vector2.y, vector2.x - vector.x, vector2.y - vector.y);
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00006D6C File Offset: 0x00004F6C
	public dfGUIManager GetManager()
	{
		if (this.cachedManager != null || !base.gameObject.activeInHierarchy)
		{
			return this.cachedManager;
		}
		if (this.parent != null && this.parent.cachedManager != null)
		{
			return this.cachedManager = this.parent.cachedManager;
		}
		GameObject gameObject = base.gameObject;
		while (gameObject != null)
		{
			dfGUIManager component = gameObject.GetComponent<dfGUIManager>();
			if (component != null)
			{
				return this.cachedManager = component;
			}
			if (gameObject.transform.parent == null)
			{
				break;
			}
			gameObject = gameObject.transform.parent.gameObject;
		}
		return dfGUIManager.ActiveManagers.FirstOrDefault<dfGUIManager>();
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00006E2C File Offset: 0x0000502C
	public float PixelsToUnits()
	{
		if (this.cachedPixelSize > 1E-45f)
		{
			return this.cachedPixelSize;
		}
		dfGUIManager manager = this.GetManager();
		if (manager == null)
		{
			return 0.0026f;
		}
		return this.cachedPixelSize = manager.PixelsToUnits();
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00006E74 File Offset: 0x00005074
	protected internal virtual Plane[] GetClippingPlanes()
	{
		Vector3[] corners = this.GetCorners();
		Vector3 inNormal = base.transform.TransformDirection(Vector3.right);
		Vector3 inNormal2 = base.transform.TransformDirection(Vector3.left);
		Vector3 inNormal3 = base.transform.TransformDirection(Vector3.up);
		Vector3 inNormal4 = base.transform.TransformDirection(Vector3.down);
		this.cachedClippingPlanes[0] = new Plane(inNormal, corners[0]);
		this.cachedClippingPlanes[1] = new Plane(inNormal2, corners[1]);
		this.cachedClippingPlanes[2] = new Plane(inNormal3, corners[2]);
		this.cachedClippingPlanes[3] = new Plane(inNormal4, corners[0]);
		return this.cachedClippingPlanes;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00006F38 File Offset: 0x00005138
	public bool Contains(dfControl child)
	{
		return child != null && child.transform.IsChildOf(base.transform);
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00006F56 File Offset: 0x00005156
	[HideInInspector]
	protected internal virtual void OnLocalize()
	{
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00006F58 File Offset: 0x00005158
	[HideInInspector]
	protected internal string getLocalizedValue(string key)
	{
		if (!this.IsLocalized || !Application.isPlaying)
		{
			return key;
		}
		if (this.languageManager == null)
		{
			if (this.languageManagerChecked)
			{
				return key;
			}
			this.languageManagerChecked = true;
			this.languageManager = this.GetManager().GetComponent<dfLanguageManager>();
			if (this.languageManager == null)
			{
				return key;
			}
		}
		return this.languageManager.GetValue(key);
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00006FC4 File Offset: 0x000051C4
	[HideInInspector]
	protected internal void updateCollider()
	{
		BoxCollider boxCollider = base.GetComponent<Collider>() as BoxCollider;
		if (boxCollider == null)
		{
			if (base.GetComponent<Collider>() != null)
			{
				throw new Exception("Invalid collider type on control: " + base.GetComponent<Collider>().GetType().Name);
			}
			boxCollider = base.gameObject.AddComponent<BoxCollider>();
		}
		if (Application.isPlaying && !this.isInteractive)
		{
			boxCollider.enabled = false;
			return;
		}
		float d = this.PixelsToUnits();
		Vector2 vector = this.size * d;
		Vector3 center = this.pivot.TransformToCenter(vector);
		Vector3 vector2 = new Vector3(vector.x * this.hotZoneScale.x, vector.y * this.hotZoneScale.y, 0.001f);
		bool enabled = base.enabled && this.IsVisible;
		boxCollider.isTrigger = false;
		boxCollider.enabled = enabled;
		boxCollider.size = vector2;
		boxCollider.center = center;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x000070B9 File Offset: 0x000052B9
	[HideInInspector]
	protected virtual void OnRebuildRenderData()
	{
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000070BB File Offset: 0x000052BB
	[HideInInspector]
	protected internal virtual void OnControlAdded(dfControl child)
	{
		this.Invalidate();
		if (this.ControlAdded != null)
		{
			this.ControlAdded(this, child);
		}
		this.Signal("OnControlAdded", this, child);
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000070E6 File Offset: 0x000052E6
	[HideInInspector]
	protected internal virtual void OnControlRemoved(dfControl child)
	{
		this.Invalidate();
		if (this.ControlRemoved != null)
		{
			this.ControlRemoved(this, child);
		}
		this.Signal("OnControlRemoved", this, child);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00007114 File Offset: 0x00005314
	[HideInInspector]
	protected internal virtual void OnPositionChanged()
	{
		this.updateVersion();
		this.GetManager().Invalidate();
		dfRenderGroup.InvalidateGroupForControl(this);
		this.cachedPosition = base.transform.localPosition;
		if (this.isControlInitialized && Application.isPlaying && base.GetComponent<Rigidbody>() == null)
		{
			Rigidbody rigidbody = base.gameObject.AddComponent<Rigidbody>();
			rigidbody.hideFlags = (HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset);
			rigidbody.isKinematic = true;
			base.GetComponent<Rigidbody>().useGravity = false;
			rigidbody.detectCollisions = false;
		}
		this.ResetLayout();
		if (this.PositionChanged != null)
		{
			this.PositionChanged(this, this.Position);
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x000071B8 File Offset: 0x000053B8
	[HideInInspector]
	protected internal virtual void OnSizeChanged()
	{
		this.updateCollider();
		this.Invalidate();
		this.ResetLayout();
		if (this.Anchor.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.PerformLayout();
		}
		this.raiseSizeChangedEvent();
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].PerformLayout();
		}
	}

	// Token: 0x06000181 RID: 385 RVA: 0x0000721C File Offset: 0x0000541C
	[HideInInspector]
	protected internal virtual void OnPivotChanged()
	{
		this.Invalidate();
		if (this.Anchor.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.ResetLayout();
			this.PerformLayout();
		}
		if (this.PivotChanged != null)
		{
			this.PivotChanged(this, this.pivot);
		}
	}

	// Token: 0x06000182 RID: 386 RVA: 0x0000725C File Offset: 0x0000545C
	[HideInInspector]
	protected internal virtual void OnAnchorChanged()
	{
		this.ResetLayout();
		if (this.anchorStyle.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical))
		{
			this.PerformLayout();
		}
		if (this.AnchorChanged != null)
		{
			this.AnchorChanged(this, this.anchorStyle);
		}
		this.Invalidate();
	}

	// Token: 0x06000183 RID: 387 RVA: 0x0000729C File Offset: 0x0000549C
	[HideInInspector]
	protected internal virtual void OnOpacityChanged()
	{
		this.Invalidate();
		float opacity = this.Opacity;
		if (this.OpacityChanged != null)
		{
			this.OpacityChanged(this, opacity);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnOpacityChanged();
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x000072F4 File Offset: 0x000054F4
	[HideInInspector]
	protected internal virtual void OnColorChanged()
	{
		this.Invalidate();
		Color32 value = this.Color;
		if (this.ColorChanged != null)
		{
			this.ColorChanged(this, value);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnColorChanged();
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x0000734A File Offset: 0x0000554A
	[HideInInspector]
	protected internal virtual void OnZOrderChanged()
	{
		if (this.ZOrderChanged != null)
		{
			this.ZOrderChanged(this, this.zindex);
		}
		this.Invalidate();
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0000736C File Offset: 0x0000556C
	[HideInInspector]
	protected internal virtual void OnTabIndexChanged()
	{
		this.Invalidate();
		if (this.TabIndexChanged != null)
		{
			this.TabIndexChanged(this, this.tabIndex);
		}
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0000738E File Offset: 0x0000558E
	[HideInInspector]
	protected virtual void OnControlClippingChanged()
	{
		if (this.ControlClippingChanged != null)
		{
			this.ControlClippingChanged(this, this.isControlClipped);
		}
		this.Signal("OnControlClippingChanged", this, this.isControlClipped);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000073C4 File Offset: 0x000055C4
	[HideInInspector]
	protected internal virtual void OnIsVisibleChanged()
	{
		this.updateCollider();
		bool flag = this.IsVisible;
		if (this.HasFocus && !flag)
		{
			dfGUIManager.SetFocus(null);
		}
		this.Signal("OnIsVisibleChanged", this, flag);
		if (this.IsVisibleChanged != null)
		{
			this.IsVisibleChanged(this, flag);
		}
		dfControl[] items = this.controls.Items;
		int count = this.controls.Count;
		for (int i = 0; i < count; i++)
		{
			items[i].OnIsVisibleChanged();
		}
		if (flag)
		{
			if (this.ControlShown != null)
			{
				this.ControlShown(this, true);
			}
			this.Signal("OnControlShown", this, true);
		}
		else
		{
			if (this.ControlHidden != null)
			{
				this.ControlHidden(this, true);
			}
			this.Signal("OnControlHidden", this, false);
		}
		this.Invalidate();
		if (flag)
		{
			this.doAutoFocus();
			return;
		}
		if (!Application.isPlaying)
		{
			(base.GetComponent<Collider>() as BoxCollider).size = Vector3.zero;
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x000074C4 File Offset: 0x000056C4
	[HideInInspector]
	protected internal virtual void OnIsEnabledChanged()
	{
		if (this.shutdownInProgress)
		{
			return;
		}
		bool flag = this.IsEnabled && base.enabled;
		this.updateCollider();
		if (dfGUIManager.ContainsFocus(this) && !flag)
		{
			dfGUIManager.SetFocus(null);
		}
		this.Invalidate();
		this.Signal("OnIsEnabledChanged", this, flag);
		if (this.IsEnabledChanged != null)
		{
			this.IsEnabledChanged(this, flag);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			this.controls[i].OnIsEnabledChanged();
		}
		this.doAutoFocus();
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0000755E File Offset: 0x0000575E
	protected internal float CalculateOpacity()
	{
		if (this.parent == null)
		{
			return this.Opacity;
		}
		return this.Opacity * this.parent.CalculateOpacity();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00007588 File Offset: 0x00005788
	protected internal Color32 ApplyOpacity(Color32 rawColor)
	{
		float num = this.CalculateOpacity();
		rawColor.a = (byte)((float)rawColor.a * num);
		return rawColor;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x000075B0 File Offset: 0x000057B0
	protected internal Vector2 GetHitPosition(dfMouseEventArgs args)
	{
		Vector2 result;
		this.GetHitPosition(args.Ray, out result);
		return result;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x000075D0 File Offset: 0x000057D0
	protected internal Vector3 getScaledDirection(Vector3 direction)
	{
		Vector3 localScale = this.GetManager().transform.localScale;
		direction = base.transform.TransformDirection(direction);
		return Vector3.Scale(direction, localScale);
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00007604 File Offset: 0x00005804
	protected internal Vector3 transformOffset(Vector3 offset)
	{
		Vector3 a = offset.x * this.getScaledDirection(Vector3.right);
		Vector3 b = offset.y * this.getScaledDirection(Vector3.down);
		return (a + b) * this.PixelsToUnits();
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00007650 File Offset: 0x00005850
	protected internal virtual void OnResolutionChanged(Vector2 previousResolution, Vector2 currentResolution)
	{
		this.updateControlHierarchy();
		this.cachedPixelSize = 0f;
		Vector3 a = base.transform.localPosition / (2f / previousResolution.y);
		this.cachedPosition = (base.transform.localPosition = a * (2f / currentResolution.y));
		if (this.Anchor.IsAnyFlagSet(dfAnchorStyle.CenterHorizontal | dfAnchorStyle.CenterVertical | dfAnchorStyle.Proportional))
		{
			this.PerformLayout();
		}
		this.updateCollider();
		this.Signal("OnResolutionChanged", this, previousResolution, currentResolution);
		this.Invalidate();
	}

	// Token: 0x06000190 RID: 400 RVA: 0x000076F0 File Offset: 0x000058F0
	[HideInInspector]
	public virtual void Awake()
	{
		this.cachedParentTransform = base.transform.parent;
		if (this.anchorStyle == dfAnchorStyle.None && this.layout != null)
		{
			this.anchorStyle = this.layout.AnchorStyle;
		}
		if (base.GetComponent<Collider>() == null)
		{
			base.gameObject.AddComponent<BoxCollider>();
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00007749 File Offset: 0x00005949
	[HideInInspector]
	public virtual void Start()
	{
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000774C File Offset: 0x0000594C
	[HideInInspector]
	public virtual void OnEnable()
	{
		this.cachedManager = null;
		this.cachedBounds = null;
		this.cachedChildCount = 0;
		this.cachedParentTransform = null;
		this.cachedPosition = Vector3.zero;
		this.cachedRelativePosition = Vector3.zero;
		this.cachedRotation = Quaternion.identity;
		this.cachedScale = Vector3.one;
		dfControl.ActiveInstances.Add(this);
		this.initializeControl();
		if (Application.isPlaying && this.IsLocalized)
		{
			this.Localize();
		}
		this.OnIsEnabledChanged();
	}

	// Token: 0x06000193 RID: 403 RVA: 0x000077D2 File Offset: 0x000059D2
	[HideInInspector]
	public virtual void OnApplicationQuit()
	{
		this.shutdownInProgress = true;
		this.RemoveAllEventHandlers();
	}

	// Token: 0x06000194 RID: 404 RVA: 0x000077E4 File Offset: 0x000059E4
	[HideInInspector]
	public virtual void OnDisable()
	{
		dfControl.ActiveInstances.Remove(this);
		try
		{
			this.Invalidate();
			if (dfGUIManager.HasFocus(this))
			{
				dfGUIManager.SetFocus(null);
			}
			else if (dfGUIManager.GetModalControl() == this)
			{
				dfGUIManager.PopModal();
			}
			this.OnIsEnabledChanged();
		}
		catch
		{
		}
		finally
		{
			this.isControlInitialized = false;
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00007854 File Offset: 0x00005A54
	[HideInInspector]
	public virtual void OnDestroy()
	{
		this.isDisposing = true;
		this.isControlInitialized = false;
		if (Application.isPlaying)
		{
			this.RemoveAllEventHandlers();
		}
		if (dfGUIManager.GetModalControl() == this)
		{
			dfGUIManager.PopModal();
		}
		if (this.layout != null)
		{
			this.layout.Dispose();
		}
		if (this.parent != null && this.parent.controls != null && !this.parent.isDisposing && this.parent.controls.Remove(this))
		{
			this.parent.cachedChildCount--;
			this.parent.OnControlRemoved(this);
		}
		for (int i = 0; i < this.controls.Count; i++)
		{
			if (this.controls[i].layout != null)
			{
				this.controls[i].layout.Dispose();
				this.controls[i].layout = null;
			}
			this.controls[i].parent = null;
		}
		this.controls.Release();
		if (this.cachedManager != null)
		{
			this.cachedManager.Invalidate();
		}
		if (this.renderData != null)
		{
			this.renderData.Release();
		}
		this.layout = null;
		this.cachedManager = null;
		this.parent = null;
		this.cachedClippingPlanes = null;
		this.cachedCorners = null;
		this.renderData = null;
		this.controls = null;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000079C6 File Offset: 0x00005BC6
	[HideInInspector]
	public virtual void LateUpdate()
	{
		if (this.layout != null && this.layout.HasPendingLayoutRequest)
		{
			this.layout.PerformLayout();
		}
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000079E8 File Offset: 0x00005BE8
	[HideInInspector]
	public virtual void Update()
	{
		if (!this.isControlInitialized)
		{
			this.initializeControl();
		}
		Transform transform = base.transform;
		if (transform.parent != this.cachedParentTransform)
		{
			this.cachedManager = null;
			this.GetManager();
			this.cachedParentTransform = transform.parent;
			this.ResetLayout(false, true);
		}
		this.updateControlHierarchy();
		if (transform.hasChanged)
		{
			this.cachedBounds = null;
			if (this.cachedScale != transform.localScale)
			{
				this.cachedScale = transform.localScale;
				this.Invalidate();
			}
			if (Vector3.Distance(this.cachedPosition, transform.localPosition) > 1E-45f)
			{
				this.cachedPosition = transform.localPosition;
				this.OnPositionChanged();
			}
			if (this.cachedRotation != transform.localRotation)
			{
				this.cachedRotation = transform.localRotation;
				this.Invalidate();
			}
			transform.hasChanged = false;
		}
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00007AD8 File Offset: 0x00005CD8
	protected internal void SetControlIndex(dfControl child, int zorder)
	{
		if (zorder < 0)
		{
			zorder = int.MaxValue;
		}
		this.controls.Sort();
		this.controls.Remove(child);
		if (zorder >= this.controls.Count)
		{
			this.controls.Add(child);
		}
		else
		{
			this.controls.Insert(zorder, child);
		}
		child.zindex = zorder;
		for (int i = 0; i < this.controls.Count; i++)
		{
			if (this.controls[i].zindex != i)
			{
				dfControl dfControl = this.controls[i];
				dfControl.zindex = i;
				dfControl.OnZOrderChanged();
			}
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00007B7A File Offset: 0x00005D7A
	public T AddControl<T>() where T : dfControl
	{
		return (T)((object)this.AddControl(typeof(T)));
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00007B94 File Offset: 0x00005D94
	public dfControl AddControl(Type controlType)
	{
		if (!typeof(dfControl).IsAssignableFrom(controlType))
		{
			throw new InvalidCastException(string.Format("Type {0} does not inherit from {1}", controlType.Name, typeof(dfControl).Name));
		}
		GameObject gameObject = new GameObject(controlType.Name);
		gameObject.transform.parent = base.transform;
		gameObject.layer = base.gameObject.layer;
		Vector2 vector = this.Size * this.PixelsToUnits() * 0.5f;
		gameObject.transform.localPosition = new Vector3(vector.x, vector.y, 0f);
		dfControl dfControl = gameObject.AddComponent(controlType) as dfControl;
		dfControl.parent = this;
		dfControl.cachedManager = this.cachedManager;
		dfControl.zindex = -1;
		this.AddControl(dfControl);
		return dfControl;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00007C70 File Offset: 0x00005E70
	public void AddControl(dfControl child)
	{
		if (child.transform == null)
		{
			throw new NullReferenceException("The child control does not have a Transform");
		}
		if (child.parent != null && child.parent != this)
		{
			child.parent.RemoveControl(child);
		}
		if (!this.controls.Contains(child))
		{
			this.controls.Add(child);
			child.parent = this;
			child.transform.parent = base.transform;
			child.cachedManager = this.cachedManager;
			child.cachedParentTransform = base.transform;
		}
		if (child.zindex == -1 || child.zindex == 2147483647)
		{
			this.SetControlIndex(child, int.MaxValue);
		}
		else
		{
			this.controls.Sort();
		}
		this.OnControlAdded(child);
		child.Invalidate();
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00007D44 File Offset: 0x00005F44
	public dfControl AddPrefab(GameObject prefab)
	{
		if (prefab.GetComponent<dfControl>() == null)
		{
			throw new InvalidCastException();
		}
		GameObject gameObject = Object.Instantiate<GameObject>(prefab);
		gameObject.transform.parent = base.transform;
		gameObject.layer = base.gameObject.layer;
		dfControl component = gameObject.GetComponent<dfControl>();
		component.parent = this;
		component.zindex = -1;
		this.AddControl(component);
		return component;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00007DAC File Offset: 0x00005FAC
	private int getMaxZOrder()
	{
		int num = -1;
		for (int i = 0; i < this.controls.Count; i++)
		{
			num = Mathf.Max(this.controls[i].zindex, num);
		}
		return num;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00007DEC File Offset: 0x00005FEC
	public void RemoveControl(dfControl child)
	{
		if (this.isDisposing)
		{
			return;
		}
		if (child.Parent == this)
		{
			child.parent = null;
		}
		if (this.controls.Remove(child))
		{
			this.OnControlRemoved(child);
			child.Invalidate();
			this.Invalidate();
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00007E38 File Offset: 0x00006038
	[HideInInspector]
	public void RebuildControlOrder()
	{
		this.controls.Sort();
		for (int i = 0; i < this.controls.Count; i++)
		{
			if (this.controls[i].zindex != i)
			{
				dfControl dfControl = this.controls[i];
				dfControl.zindex = i;
				dfControl.OnZOrderChanged();
			}
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00007E92 File Offset: 0x00006092
	internal void setClippingState(bool isClipped)
	{
		if (isClipped == this.isControlClipped)
		{
			return;
		}
		this.isControlClipped = isClipped;
		this.OnControlClippingChanged();
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00007EAC File Offset: 0x000060AC
	private void doAutoFocus()
	{
		if (Application.isPlaying && this.IsEnabled && base.enabled && this.AutoFocus && this.CanFocus && this.IsVisible && base.gameObject.activeSelf && base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.focusOnNextFrame());
		}
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00007F12 File Offset: 0x00006112
	private IEnumerator focusOnNextFrame()
	{
		yield return null;
		this.Focus();
		yield break;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00007F21 File Offset: 0x00006121
	protected void raiseSizeChangedEvent()
	{
		if (this.SizeChanged != null)
		{
			this.SizeChanged(this, this.Size);
		}
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00007F3D File Offset: 0x0000613D
	protected void raiseMouseDownEvent(dfMouseEventArgs args)
	{
		if (this.MouseDown != null)
		{
			this.MouseDown(this, args);
		}
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00007F54 File Offset: 0x00006154
	protected void raiseMouseMoveEvent(dfMouseEventArgs args)
	{
		if (this.MouseMove != null)
		{
			this.MouseMove(this, args);
		}
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x00007F6B File Offset: 0x0000616B
	protected void raiseMouseWheelEvent(dfMouseEventArgs args)
	{
		if (this.MouseWheel != null)
		{
			this.MouseWheel(this, args);
		}
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x00007F84 File Offset: 0x00006184
	private void initializeControl()
	{
		Transform transform = base.transform.parent;
		if (transform == null || transform.GetComponent(typeof(IDFControlHost)) == null)
		{
			return;
		}
		if (transform != null || this.cachedParentTransform != transform)
		{
			dfControl component = transform.GetComponent<dfControl>();
			if (component != null)
			{
				this.parent = component;
				component.AddControl(this);
			}
			if (this.controls == null)
			{
				this.updateControlHierarchy();
			}
		}
		if (this.renderOrder == -1)
		{
			this.renderOrder = this.ZOrder;
		}
		this.updateCollider();
		this.ensureLayoutExists();
		this.layout.Attach(this);
		if (!Application.isPlaying)
		{
			this.PerformLayout();
		}
		this.Invalidate();
		this.isControlInitialized = true;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00008049 File Offset: 0x00006249
	internal void updateControlHierarchy()
	{
		this.updateControlHierarchy(false);
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x00008054 File Offset: 0x00006254
	internal void updateControlHierarchy(bool force)
	{
		int childCount = base.transform.childCount;
		if (!force && childCount == this.cachedChildCount)
		{
			return;
		}
		this.cachedChildCount = childCount;
		dfList<dfControl> childControls = this.getChildControls();
		for (int i = 0; i < childControls.Count; i++)
		{
			dfControl dfControl = childControls[i];
			if (!this.controls.Contains(dfControl))
			{
				dfControl.parent = this;
				dfControl.cachedParentTransform = base.transform;
				if (!Application.isPlaying)
				{
					dfControl.ResetLayout();
				}
				this.OnControlAdded(dfControl);
				dfControl.updateControlHierarchy();
			}
		}
		for (int j = 0; j < this.controls.Count; j++)
		{
			dfControl dfControl2 = this.controls[j];
			if (dfControl2 == null || !childControls.Contains(dfControl2))
			{
				this.OnControlRemoved(dfControl2);
				if (dfControl2 != null && dfControl2.parent == this)
				{
					dfControl2.parent = null;
				}
			}
		}
		this.controls.Release();
		this.controls = childControls;
		this.RebuildControlOrder();
	}

	// Token: 0x060001AA RID: 426 RVA: 0x0000815C File Offset: 0x0000635C
	private dfList<dfControl> getChildControls()
	{
		int childCount = base.transform.childCount;
		dfList<dfControl> dfList = dfList<dfControl>.Obtain(childCount);
		for (int i = 0; i < childCount; i++)
		{
			Transform child = base.transform.GetChild(i);
			if (child.gameObject.activeSelf)
			{
				dfControl component = child.GetComponent<dfControl>();
				if (component != null)
				{
					dfList.Add(component);
				}
			}
		}
		return dfList;
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000081C0 File Offset: 0x000063C0
	private void ensureLayoutExists()
	{
		if (this.layout == null)
		{
			dfAnchorStyle dfAnchorStyle = (this.anchorStyle != dfAnchorStyle.None) ? this.anchorStyle : (dfAnchorStyle.Top | dfAnchorStyle.Left);
			this.layout = new dfControl.AnchorLayout(dfAnchorStyle, this);
			this.anchorStyle = dfAnchorStyle;
		}
		else
		{
			this.layout.Attach(this);
		}
		int num = 0;
		while (this.Controls != null && num < this.Controls.Count)
		{
			if (this.controls[num] != null)
			{
				this.controls[num].ensureLayoutExists();
			}
			num++;
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000824C File Offset: 0x0000644C
	protected internal void updateVersion()
	{
		this.version = (dfControl.versionCounter += 1U);
	}

	// Token: 0x060001AD RID: 429 RVA: 0x00008264 File Offset: 0x00006464
	private Vector3 getRelativePosition()
	{
		if (this.relativePositionCacheVersion == this.version)
		{
			return this.cachedRelativePosition;
		}
		this.relativePositionCacheVersion = this.version;
		if (base.transform.parent == null)
		{
			return Vector3.zero;
		}
		if (this.parent != null)
		{
			float d = this.PixelsToUnits();
			Vector3 position = base.transform.parent.position;
			Vector3 position2 = base.transform.position;
			Transform transform = base.transform.parent;
			Vector3 vector = transform.InverseTransformPoint(position / d);
			vector += this.parent.pivot.TransformToUpperLeft(this.parent.size);
			Vector3 vector2 = transform.InverseTransformPoint(position2 / d) + this.pivot.TransformToUpperLeft(this.size) - vector;
			return this.cachedRelativePosition = vector2.Scale(1f, -1f, 1f);
		}
		dfGUIManager manager = this.GetManager();
		if (manager == null)
		{
			Debug.LogError("Cannot get position: View not found");
			this.relativePositionCacheVersion = uint.MaxValue;
			return Vector3.zero;
		}
		float num = this.PixelsToUnits();
		Vector3 point = base.transform.position + this.pivot.TransformToUpperLeft(this.size) * num;
		Plane[] clippingPlanes = manager.GetClippingPlanes();
		float x = clippingPlanes[0].GetDistanceToPoint(point) / num;
		float y = clippingPlanes[3].GetDistanceToPoint(point) / num;
		this.cachedRelativePosition = new Vector3(x, y).RoundToInt();
		return this.cachedRelativePosition;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00008408 File Offset: 0x00006608
	private void setPositionInternal(Vector3 value)
	{
		value += this.pivot.UpperLeftToTransform(this.Size);
		value *= this.PixelsToUnits();
		if (Vector3.Distance(value, this.cachedPosition) <= 1E-45f)
		{
			return;
		}
		this.cachedPosition = (base.transform.localPosition = value);
		this.OnPositionChanged();
	}

	// Token: 0x060001AF RID: 431 RVA: 0x0000846C File Offset: 0x0000666C
	private void setRelativePosition(ref Vector3 value)
	{
		if (base.transform.parent == null)
		{
			Debug.LogError("Cannot set relative position without a parent Transform.");
			return;
		}
		if ((value - this.getRelativePosition()).magnitude <= 1E-45f)
		{
			return;
		}
		if (this.parent != null)
		{
			Vector3 vector = value.Scale(1f, -1f, 1f) + this.pivot.UpperLeftToTransform(this.size) - this.parent.pivot.UpperLeftToTransform(this.parent.size);
			vector *= this.PixelsToUnits();
			if ((vector - base.transform.localPosition).sqrMagnitude >= 1E-45f)
			{
				base.transform.localPosition = vector;
				this.cachedPosition = vector;
				this.OnPositionChanged();
			}
			return;
		}
		dfGUIManager manager = this.GetManager();
		if (manager == null)
		{
			Debug.LogError("Cannot get position: View not found");
			return;
		}
		Vector3 a = manager.GetCorners()[0];
		float d = this.PixelsToUnits();
		value = value.Scale(1f, -1f, 1f) * d;
		Vector3 b = this.pivot.UpperLeftToTransform(this.Size) * d;
		Vector3 vector2 = a + manager.transform.TransformDirection(value) + b;
		if (Vector3.Distance(vector2, this.cachedPosition) > 1E-45f)
		{
			base.transform.position = vector2;
			this.cachedPosition = base.transform.localPosition;
			this.OnPositionChanged();
		}
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00008628 File Offset: 0x00006828
	private static Vector3 closestPointOnLine(Vector3 start, Vector3 end, Vector3 test, bool clamp)
	{
		Vector3 rhs = test - start;
		Vector3 vector = (end - start).normalized;
		float magnitude = (end - start).magnitude;
		float num = Vector3.Dot(vector, rhs);
		if (clamp)
		{
			if (num < 0f)
			{
				return start;
			}
			if (num > magnitude)
			{
				return end;
			}
		}
		vector *= num;
		return start + vector;
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000868C File Offset: 0x0000688C
	public int CompareTo(dfControl other)
	{
		if (this.ZOrder >= 0)
		{
			return this.ZOrder.CompareTo(other.ZOrder);
		}
		if (other.ZOrder >= 0)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x04000062 RID: 98
	private const float MINIMUM_OPACITY = 0.0125f;

	// Token: 0x04000063 RID: 99
	private static uint versionCounter;

	// Token: 0x04000064 RID: 100
	[SerializeField]
	protected dfAnchorStyle anchorStyle;

	// Token: 0x04000065 RID: 101
	[SerializeField]
	protected bool isEnabled = true;

	// Token: 0x04000066 RID: 102
	[SerializeField]
	protected bool isVisible = true;

	// Token: 0x04000067 RID: 103
	[SerializeField]
	protected bool isInteractive = true;

	// Token: 0x04000068 RID: 104
	[SerializeField]
	protected string tooltip;

	// Token: 0x04000069 RID: 105
	[SerializeField]
	protected dfPivotPoint pivot;

	// Token: 0x0400006A RID: 106
	[HideInInspector]
	[SerializeField]
	public int zindex = int.MaxValue;

	// Token: 0x0400006B RID: 107
	[SerializeField]
	protected Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x0400006C RID: 108
	[SerializeField]
	protected Color32 disabledColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x0400006D RID: 109
	[SerializeField]
	protected Vector2 size = Vector2.zero;

	// Token: 0x0400006E RID: 110
	[SerializeField]
	protected Vector2 minSize = Vector2.zero;

	// Token: 0x0400006F RID: 111
	[SerializeField]
	protected Vector2 maxSize = Vector2.zero;

	// Token: 0x04000070 RID: 112
	[SerializeField]
	protected bool clipChildren;

	// Token: 0x04000071 RID: 113
	[HideInInspector]
	[SerializeField]
	protected int tabIndex = -1;

	// Token: 0x04000072 RID: 114
	[HideInInspector]
	[SerializeField]
	protected bool canFocus;

	// Token: 0x04000073 RID: 115
	[SerializeField]
	protected bool autoFocus;

	// Token: 0x04000074 RID: 116
	[SerializeField]
	protected bool _customWordWrapAllowed = true;

	// Token: 0x04000075 RID: 117
	[HideInInspector]
	[SerializeField]
	protected dfControl.AnchorLayout layout;

	// Token: 0x04000076 RID: 118
	[HideInInspector]
	[SerializeField]
	protected int renderOrder = -1;

	// Token: 0x04000077 RID: 119
	[SerializeField]
	protected bool isLocalized;

	// Token: 0x04000078 RID: 120
	[SerializeField]
	protected Vector2 hotZoneScale = Vector2.one;

	// Token: 0x04000079 RID: 121
	[SerializeField]
	protected bool allowSignalEvents = true;

	// Token: 0x0400007A RID: 122
	private static object[] signal1 = new object[1];

	// Token: 0x0400007B RID: 123
	private static object[] signal2 = new object[2];

	// Token: 0x0400007C RID: 124
	private static object[] signal3 = new object[3];

	// Token: 0x0400007D RID: 125
	protected bool isControlInvalidated = true;

	// Token: 0x0400007E RID: 126
	protected bool isControlClipped;

	// Token: 0x0400007F RID: 127
	protected dfControl parent;

	// Token: 0x04000080 RID: 128
	protected dfList<dfControl> controls = dfList<dfControl>.Obtain();

	// Token: 0x04000081 RID: 129
	protected dfGUIManager cachedManager;

	// Token: 0x04000082 RID: 130
	protected dfLanguageManager languageManager;

	// Token: 0x04000083 RID: 131
	protected bool languageManagerChecked;

	// Token: 0x04000084 RID: 132
	protected int cachedChildCount;

	// Token: 0x04000085 RID: 133
	protected Vector3 cachedPosition = Vector3.one * float.MinValue;

	// Token: 0x04000086 RID: 134
	protected Quaternion cachedRotation = Quaternion.identity;

	// Token: 0x04000087 RID: 135
	protected Vector3 cachedScale = Vector3.one;

	// Token: 0x04000088 RID: 136
	protected Bounds? cachedBounds;

	// Token: 0x04000089 RID: 137
	protected Transform cachedParentTransform;

	// Token: 0x0400008A RID: 138
	protected float cachedPixelSize;

	// Token: 0x0400008B RID: 139
	protected Vector3 cachedRelativePosition = Vector3.one * float.MinValue;

	// Token: 0x0400008C RID: 140
	protected uint relativePositionCacheVersion = uint.MaxValue;

	// Token: 0x0400008D RID: 141
	protected dfRenderData renderData;

	// Token: 0x0400008E RID: 142
	protected bool isMouseHovering;

	// Token: 0x0400008F RID: 143
	private new object tag;

	// Token: 0x04000090 RID: 144
	protected bool isDisposing;

	// Token: 0x04000091 RID: 145
	private bool performingLayout;

	// Token: 0x04000092 RID: 146
	protected Vector3[] cachedCorners = new Vector3[4];

	// Token: 0x04000093 RID: 147
	protected Plane[] cachedClippingPlanes = new Plane[4];

	// Token: 0x04000094 RID: 148
	private bool shutdownInProgress;

	// Token: 0x04000095 RID: 149
	private uint version;

	// Token: 0x04000096 RID: 150
	protected bool isControlInitialized;

	// Token: 0x04000097 RID: 151
	private bool rendering;

	// Token: 0x04000098 RID: 152
	protected string localizationKey;

	// Token: 0x04000099 RID: 153
	public static readonly dfList<dfControl> ActiveInstances = new dfList<dfControl>();

	// Token: 0x02000351 RID: 849
	protected class SignalCache
	{
		// Token: 0x06001C04 RID: 7172 RVA: 0x00077A74 File Offset: 0x00075C74
		public static bool Invoke(Component target, string eventName, object[] arguments, out object returnValue)
		{
			returnValue = null;
			if (target == null)
			{
				return false;
			}
			Type type = target.GetType();
			dfControl.SignalCache.SignalCacheItem signalCacheItem = dfControl.SignalCache.getItem(type, eventName);
			if (signalCacheItem == null)
			{
				Type[] array = new Type[arguments.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (arguments[i] == null)
					{
						array[i] = typeof(object);
					}
					else
					{
						array[i] = arguments[i].GetType();
					}
				}
				signalCacheItem = new dfControl.SignalCache.SignalCacheItem(type, eventName, array);
				dfControl.SignalCache.cache.Add(signalCacheItem);
			}
			return signalCacheItem.Invoke(target, arguments, out returnValue);
		}

		// Token: 0x06001C05 RID: 7173 RVA: 0x00077AF4 File Offset: 0x00075CF4
		private static dfControl.SignalCache.SignalCacheItem getItem(Type componentType, string eventName)
		{
			for (int i = 0; i < dfControl.SignalCache.cache.Count; i++)
			{
				dfControl.SignalCache.SignalCacheItem signalCacheItem = dfControl.SignalCache.cache[i];
				if (signalCacheItem.ComponentType == componentType && signalCacheItem.EventName == eventName)
				{
					return signalCacheItem;
				}
			}
			return null;
		}

		// Token: 0x06001C06 RID: 7174 RVA: 0x00077B41 File Offset: 0x00075D41
		private static MethodInfo getMethod(Type type, string name, Type[] paramTypes)
		{
			return type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, paramTypes, null);
		}

		// Token: 0x06001C07 RID: 7175 RVA: 0x00077B50 File Offset: 0x00075D50
		private static bool matchesParameterTypes(MethodInfo method, Type[] types)
		{
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length != types.Length)
			{
				return false;
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (!parameters[i].ParameterType.IsAssignableFrom(types[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040015D8 RID: 5592
		private static readonly List<dfControl.SignalCache.SignalCacheItem> cache = new List<dfControl.SignalCache.SignalCacheItem>();

		// Token: 0x02000444 RID: 1092
		private class SignalCacheItem
		{
			// Token: 0x06001FCD RID: 8141 RVA: 0x00083750 File Offset: 0x00081950
			public SignalCacheItem(Type componentType, string eventName, Type[] paramTypes)
			{
				this.ComponentType = componentType;
				this.EventName = eventName;
				MethodInfo left = dfControl.SignalCache.getMethod(componentType, eventName, paramTypes);
				if (left != null)
				{
					this.method = left;
					this.usesParameters = true;
					return;
				}
				this.method = dfControl.SignalCache.getMethod(componentType, eventName, dfReflectionExtensions.EmptyTypes);
				this.usesParameters = false;
			}

			// Token: 0x06001FCE RID: 8142 RVA: 0x000837AB File Offset: 0x000819AB
			public bool Invoke(object target, object[] arguments, out object returnValue)
			{
				if (this.method == null)
				{
					returnValue = null;
					return false;
				}
				if (!this.usesParameters)
				{
					arguments = null;
				}
				returnValue = this.method.Invoke(target, arguments);
				return true;
			}

			// Token: 0x0400193E RID: 6462
			public readonly Type ComponentType;

			// Token: 0x0400193F RID: 6463
			public readonly string EventName;

			// Token: 0x04001940 RID: 6464
			private readonly MethodInfo method;

			// Token: 0x04001941 RID: 6465
			private readonly bool usesParameters;
		}
	}

	// Token: 0x02000352 RID: 850
	[Serializable]
	protected class AnchorLayout
	{
		// Token: 0x06001C0A RID: 7178 RVA: 0x00077BA5 File Offset: 0x00075DA5
		internal AnchorLayout(dfAnchorStyle anchorStyle)
		{
			this.anchorStyle = anchorStyle;
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00077BB4 File Offset: 0x00075DB4
		internal AnchorLayout(dfAnchorStyle anchorStyle, dfControl owner) : this(anchorStyle)
		{
			this.Attach(owner);
			this.Reset();
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00077BCA File Offset: 0x00075DCA
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x00077BD2 File Offset: 0x00075DD2
		internal dfAnchorStyle AnchorStyle
		{
			get
			{
				return this.anchorStyle;
			}
			set
			{
				if (value != this.anchorStyle)
				{
					this.anchorStyle = value;
					this.Reset();
				}
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00077BEA File Offset: 0x00075DEA
		internal bool IsPerformingLayout
		{
			get
			{
				return this.performingLayout;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00077BF2 File Offset: 0x00075DF2
		internal bool IsLayoutSuspended
		{
			get
			{
				return this.suspendLayoutCounter > 0;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00077BFD File Offset: 0x00075DFD
		internal bool HasPendingLayoutRequest
		{
			get
			{
				return this.pendingLayoutRequest;
			}
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00077C05 File Offset: 0x00075E05
		internal void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.owner = null;
			}
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x00077C1D File Offset: 0x00075E1D
		internal void SuspendLayout()
		{
			this.suspendLayoutCounter++;
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x00077C2D File Offset: 0x00075E2D
		internal void ResumeLayout()
		{
			bool flag = this.suspendLayoutCounter > 0;
			this.suspendLayoutCounter = Mathf.Max(0, this.suspendLayoutCounter - 1);
			if (flag && this.suspendLayoutCounter == 0 && this.pendingLayoutRequest)
			{
				this.PerformLayout();
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x00077C64 File Offset: 0x00075E64
		internal void PerformLayout()
		{
			if (this.disposed)
			{
				return;
			}
			if (this.suspendLayoutCounter > 0)
			{
				this.pendingLayoutRequest = true;
				return;
			}
			this.performLayoutInternal();
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00077C86 File Offset: 0x00075E86
		internal void Attach(dfControl ownerControl)
		{
			this.owner = ownerControl;
			if (ownerControl != null)
			{
				this.anchorStyle = ownerControl.anchorStyle;
			}
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00077CA4 File Offset: 0x00075EA4
		internal void Reset()
		{
			this.Reset(false);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00077CB0 File Offset: 0x00075EB0
		internal void Reset(bool force)
		{
			if (this.owner == null || this.owner.transform.parent == null || this.anchorStyle == dfAnchorStyle.None)
			{
				return;
			}
			if ((!force && (this.IsPerformingLayout || this.IsLayoutSuspended)) || this.owner == null || !this.owner.gameObject.activeSelf)
			{
				return;
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Proportional))
			{
				this.resetLayoutProportional();
				return;
			}
			this.resetLayoutAbsolute();
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00077D4C File Offset: 0x00075F4C
		private void resetLayoutProportional()
		{
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = this.owner.Size;
			Vector2 parentSize = this.getParentSize();
			float x = relativePosition.x;
			float y = relativePosition.y;
			float num = x + size.x;
			float num2 = y + size.y;
			if (this.margins == null)
			{
				this.margins = new dfAnchorMargins();
			}
			this.margins.left = x / parentSize.x;
			this.margins.right = num / parentSize.x;
			this.margins.top = y / parentSize.y;
			this.margins.bottom = num2 / parentSize.y;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00077DFC File Offset: 0x00075FFC
		private void resetLayoutAbsolute()
		{
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = this.owner.Size;
			Vector2 parentSize = this.getParentSize();
			float x = relativePosition.x;
			float y = relativePosition.y;
			float right = parentSize.x - size.x - x;
			float bottom = parentSize.y - size.y - y;
			if (this.margins == null)
			{
				this.margins = new dfAnchorMargins();
			}
			this.margins.left = x;
			this.margins.right = right;
			this.margins.top = y;
			this.margins.bottom = bottom;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00077E9C File Offset: 0x0007609C
		protected void performLayoutInternal()
		{
			if (this.anchorStyle == dfAnchorStyle.None)
			{
				return;
			}
			if (this.owner == null || this.owner.transform.parent == null)
			{
				this.pendingLayoutRequest = true;
				return;
			}
			if (this.margins == null || this.IsPerformingLayout || this.IsLayoutSuspended || !this.owner.gameObject.activeSelf)
			{
				return;
			}
			try
			{
				this.performingLayout = true;
				this.pendingLayoutRequest = false;
				Vector2 parentSize = this.getParentSize();
				Vector2 size = this.owner.Size;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Proportional))
				{
					this.performLayoutProportional(parentSize, size);
				}
				else
				{
					this.performLayoutAbsolute(parentSize, size);
				}
			}
			finally
			{
				this.performingLayout = false;
			}
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x00077F74 File Offset: 0x00076174
		private void performLayoutProportional(Vector2 parentSize, Vector2 controlSize)
		{
			float x = this.margins.left * parentSize.x;
			float num = this.margins.right * parentSize.x;
			float y = this.margins.top * parentSize.y;
			float num2 = this.margins.bottom * parentSize.y;
			Vector3 relativePosition = this.owner.RelativePosition;
			Vector2 size = controlSize;
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
			{
				relativePosition.x = x;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
				{
					size.x = (this.margins.right - this.margins.left) * parentSize.x;
				}
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
			{
				relativePosition.x = num - controlSize.x;
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterHorizontal))
			{
				relativePosition.x = (parentSize.x - controlSize.x) * 0.5f;
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
			{
				relativePosition.y = y;
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
				{
					size.y = (this.margins.bottom - this.margins.top) * parentSize.y;
				}
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
			{
				relativePosition.y = num2 - controlSize.y;
			}
			else if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterVertical))
			{
				relativePosition.y = (parentSize.y - controlSize.y) * 0.5f;
			}
			this.owner.Size = size;
			this.owner.RelativePosition = relativePosition;
			dfGUIManager manager = this.owner.GetManager();
			if (manager != null && manager.PixelPerfectMode)
			{
				this.owner.MakePixelPerfect(false);
			}
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x0007814C File Offset: 0x0007634C
		private void performLayoutAbsolute(Vector2 parentSize, Vector2 controlSize)
		{
			float num = this.margins.left;
			float num2 = this.margins.top;
			float num3 = num + controlSize.x;
			float num4 = num2 + controlSize.y;
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterHorizontal))
			{
				num = (float)Mathf.RoundToInt((parentSize.x - controlSize.x) * 0.5f);
				num3 = (float)Mathf.RoundToInt(num + controlSize.x);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
				{
					num = this.margins.left;
					num3 = num + controlSize.x;
				}
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Right))
				{
					num3 = parentSize.x - this.margins.right;
					if (!this.anchorStyle.IsFlagSet(dfAnchorStyle.Left))
					{
						num = num3 - controlSize.x;
					}
				}
			}
			if (this.anchorStyle.IsFlagSet(dfAnchorStyle.CenterVertical))
			{
				num2 = (float)Mathf.RoundToInt((parentSize.y - controlSize.y) * 0.5f);
				num4 = (float)Mathf.RoundToInt(num2 + controlSize.y);
			}
			else
			{
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
				{
					num2 = this.margins.top;
					num4 = num2 + controlSize.y;
				}
				if (this.anchorStyle.IsFlagSet(dfAnchorStyle.Bottom))
				{
					num4 = parentSize.y - this.margins.bottom;
					if (!this.anchorStyle.IsFlagSet(dfAnchorStyle.Top))
					{
						num2 = num4 - controlSize.y;
					}
				}
			}
			Vector2 size = new Vector2(Mathf.Max(0f, num3 - num), Mathf.Max(0f, num4 - num2));
			Vector3 vector = new Vector3(num, num2);
			this.owner.Size = size;
			this.owner.setRelativePosition(ref vector);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000782F4 File Offset: 0x000764F4
		private Vector2 getParentSize()
		{
			dfControl parent = this.owner.parent;
			if (parent != null)
			{
				return parent.Size;
			}
			return this.owner.GetManager().GetScreenSize();
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00078330 File Offset: 0x00076530
		public override string ToString()
		{
			if (this.owner == null)
			{
				return "NO OWNER FOR ANCHOR";
			}
			dfControl parent = this.owner.parent;
			return string.Format("{0}.{1} - {2}", (parent != null) ? parent.name : "SCREEN", this.owner.name, this.margins);
		}

		// Token: 0x040015D9 RID: 5593
		[SerializeField]
		protected dfAnchorStyle anchorStyle;

		// Token: 0x040015DA RID: 5594
		[SerializeField]
		protected dfAnchorMargins margins;

		// Token: 0x040015DB RID: 5595
		[SerializeField]
		protected dfControl owner;

		// Token: 0x040015DC RID: 5596
		private int suspendLayoutCounter;

		// Token: 0x040015DD RID: 5597
		private bool performingLayout;

		// Token: 0x040015DE RID: 5598
		private bool disposed;

		// Token: 0x040015DF RID: 5599
		private bool pendingLayoutRequest;
	}
}
