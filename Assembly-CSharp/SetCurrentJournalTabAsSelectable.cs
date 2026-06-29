using System;
using RG.SecondsRemaster.Survival;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class SetCurrentJournalTabAsSelectable : StateMachineBehaviour
{
	// Token: 0x06000F98 RID: 3992 RVA: 0x00040E54 File Offset: 0x0003F054
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		JournalController journalController = Object.FindObjectOfType<JournalController>();
		if (journalController != null)
		{
			JournalTabsController tabs = journalController.Tabs;
			if (tabs != null && tabs.CurrentTab != null)
			{
				GamepadButton component = tabs.CurrentTab.GetComponent<GamepadButton>();
				if (component != null)
				{
					component.SelectThisSelectable();
				}
			}
		}
	}
}
