using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class SurvivalPromptsController : MonoBehaviour
{
	// Token: 0x17000340 RID: 832
	// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00041BCA File Offset: 0x0003FDCA
	// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00041BD1 File Offset: 0x0003FDD1
	public static SurvivalPromptsController Instance { get; private set; }

	// Token: 0x06000FEA RID: 4074 RVA: 0x00041BD9 File Offset: 0x0003FDD9
	public SurvivalPromptsController()
	{
		SurvivalPromptsController.Instance = this;
	}

	// Token: 0x06000FEB RID: 4075 RVA: 0x00041BF2 File Offset: 0x0003FDF2
	private void Awake()
	{
		this._player = ReInput.players.GetPlayer(0);
	}

	// Token: 0x06000FEC RID: 4076 RVA: 0x00041C05 File Offset: 0x0003FE05
	private void Update()
	{
		if (this._player.GetButtonDown(41))
		{
			this.StartShowingPrompts();
		}
		if (this._player.GetButtonUp(41))
		{
			this.StopShowingPrompts();
		}
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x00041C34 File Offset: 0x0003FE34
	private void StartShowingPrompts()
	{
		this.IsShowingPrompts = true;
		for (int i = 0; i < this._prompts.Count; i++)
		{
			this._prompts[i].Show();
		}
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x00041C70 File Offset: 0x0003FE70
	private void StopShowingPrompts()
	{
		this.IsShowingPrompts = false;
		for (int i = 0; i < this._prompts.Count; i++)
		{
			this._prompts[i].Hide();
		}
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x00041CAB File Offset: 0x0003FEAB
	public void RegisterPrompt(SurvivalPrompt prompt)
	{
		if (!this._prompts.Contains(prompt))
		{
			this._prompts.Add(prompt);
			if (this.IsShowingPrompts)
			{
				prompt.Show();
			}
		}
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x00041CD5 File Offset: 0x0003FED5
	public void UnregisterPrompt(SurvivalPrompt prompt)
	{
		if (this._prompts.Contains(prompt))
		{
			this._prompts.Remove(prompt);
			prompt.Hide();
		}
	}

	// Token: 0x040009DB RID: 2523
	[NonSerialized]
	public bool IsShowingPrompts;

	// Token: 0x040009DC RID: 2524
	private List<SurvivalPrompt> _prompts = new List<SurvivalPrompt>();

	// Token: 0x040009DD RID: 2525
	private Player _player;
}
