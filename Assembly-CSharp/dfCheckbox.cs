using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[ExecuteInEditMode]
[dfCategory("Basic Controls")]
[dfTooltip("Implements a standard checkbox (or toggle) control")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_checkbox.html")]
[AddComponentMenu("Daikon Forge/User Interface/Checkbox")]
[Serializable]
public class dfCheckbox : dfControl
{
	// Token: 0x14000002 RID: 2
	// (add) Token: 0x0600008B RID: 139 RVA: 0x00003B58 File Offset: 0x00001D58
	// (remove) Token: 0x0600008C RID: 140 RVA: 0x00003B90 File Offset: 0x00001D90
	public event PropertyChangedEventHandler<bool> CheckChanged;

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600008D RID: 141 RVA: 0x00003BC5 File Offset: 0x00001DC5
	// (set) Token: 0x0600008E RID: 142 RVA: 0x00003BCD File Offset: 0x00001DCD
	public bool ClickWhenSpacePressed
	{
		get
		{
			return this.clickWhenSpacePressed;
		}
		set
		{
			this.clickWhenSpacePressed = value;
		}
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x0600008F RID: 143 RVA: 0x00003BD6 File Offset: 0x00001DD6
	// (set) Token: 0x06000090 RID: 144 RVA: 0x00003BDE File Offset: 0x00001DDE
	public bool IsChecked
	{
		get
		{
			return this.isChecked;
		}
		set
		{
			if (value != this.isChecked)
			{
				this.isChecked = value;
				this.OnCheckChanged();
				if (value && this.group != null)
				{
					this.handleGroupedCheckboxChecked();
				}
			}
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000091 RID: 145 RVA: 0x00003C0D File Offset: 0x00001E0D
	// (set) Token: 0x06000092 RID: 146 RVA: 0x00003C15 File Offset: 0x00001E15
	public dfControl CheckIcon
	{
		get
		{
			return this.checkIcon;
		}
		set
		{
			if (value != this.checkIcon)
			{
				this.checkIcon = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000093 RID: 147 RVA: 0x00003C32 File Offset: 0x00001E32
	// (set) Token: 0x06000094 RID: 148 RVA: 0x00003C3A File Offset: 0x00001E3A
	public dfLabel Label
	{
		get
		{
			return this.label;
		}
		set
		{
			if (value != this.label)
			{
				this.label = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000095 RID: 149 RVA: 0x00003C57 File Offset: 0x00001E57
	// (set) Token: 0x06000096 RID: 150 RVA: 0x00003C5F File Offset: 0x00001E5F
	public dfControl GroupContainer
	{
		get
		{
			return this.group;
		}
		set
		{
			if (value != this.group)
			{
				this.group = value;
				this.Invalidate();
			}
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000097 RID: 151 RVA: 0x00003C7C File Offset: 0x00001E7C
	// (set) Token: 0x06000098 RID: 152 RVA: 0x00003C9D File Offset: 0x00001E9D
	public string Text
	{
		get
		{
			if (this.label != null)
			{
				return this.label.Text;
			}
			return "[LABEL NOT SET]";
		}
		set
		{
			if (this.label != null)
			{
				this.label.Text = value;
			}
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000099 RID: 153 RVA: 0x00003CB9 File Offset: 0x00001EB9
	public override bool CanFocus
	{
		get
		{
			return base.IsEnabled && base.IsVisible;
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00003CCB File Offset: 0x00001ECB
	public override void Start()
	{
		base.Start();
		if (this.checkIcon != null)
		{
			this.checkIcon.BringToFront();
			this.checkIcon.IsVisible = this.IsChecked;
		}
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00003D00 File Offset: 0x00001F00
	protected internal override void OnKeyPress(dfKeyEventArgs args)
	{
		if (this.ClickWhenSpacePressed && this.IsInteractive && args.KeyCode == KeyCode.Space)
		{
			this.OnClick(new dfMouseEventArgs(this, dfMouseButtons.Left, 1, default(Ray), Vector2.zero, 0f));
			return;
		}
		base.OnKeyPress(args);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00003D50 File Offset: 0x00001F50
	protected internal override void OnClick(dfMouseEventArgs args)
	{
		base.OnClick(args);
		if (!this.IsInteractive)
		{
			return;
		}
		if (this.group == null)
		{
			this.IsChecked = !this.IsChecked;
		}
		else
		{
			this.handleGroupedCheckboxChecked();
		}
		args.Use();
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00003D90 File Offset: 0x00001F90
	protected internal void OnCheckChanged()
	{
		base.SignalHierarchy("OnCheckChanged", new object[]
		{
			this,
			this.isChecked
		});
		if (this.CheckChanged != null)
		{
			this.CheckChanged(this, this.isChecked);
		}
		if (this.checkIcon != null)
		{
			if (this.IsChecked)
			{
				this.checkIcon.BringToFront();
			}
			this.checkIcon.IsVisible = this.IsChecked;
		}
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00003E10 File Offset: 0x00002010
	private void handleGroupedCheckboxChecked()
	{
		if (this.group == null)
		{
			return;
		}
		foreach (dfCheckbox dfCheckbox in this.group.transform.GetComponentsInChildren<dfCheckbox>())
		{
			if (dfCheckbox != this && dfCheckbox.GroupContainer == this.GroupContainer && dfCheckbox.IsChecked)
			{
				dfCheckbox.IsChecked = false;
			}
		}
		this.IsChecked = true;
	}

	// Token: 0x04000036 RID: 54
	[SerializeField]
	protected bool isChecked;

	// Token: 0x04000037 RID: 55
	[SerializeField]
	protected dfControl checkIcon;

	// Token: 0x04000038 RID: 56
	[SerializeField]
	protected dfLabel label;

	// Token: 0x04000039 RID: 57
	[SerializeField]
	protected dfControl group;

	// Token: 0x0400003A RID: 58
	[SerializeField]
	protected bool clickWhenSpacePressed = true;
}
