using System;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class SetAsSelectable : StateMachineBehaviour
{
	// Token: 0x06000F96 RID: 3990 RVA: 0x00040E24 File Offset: 0x0003F024
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		GamepadButton component = animator.gameObject.GetComponent<GamepadButton>();
		if (component != null)
		{
			component.SelectThisSelectable();
		}
	}
}
