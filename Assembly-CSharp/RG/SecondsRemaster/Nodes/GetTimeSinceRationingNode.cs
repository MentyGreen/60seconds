using System;
using System.Collections.Generic;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000258 RID: 600
	[Node(false, "Supplies Nodes/Consumables/Get Time Since Rationing Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(Goal),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas),
		typeof(ConditionEvent)
	})]
	public class GetTimeSinceRationingNode : ResourceNode
	{
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00061707 File Offset: 0x0005F907
		public override string GetID
		{
			get
			{
				return "EE_GetTimeSinceRationingNode";
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00061710 File Offset: 0x0005F910
		public override Node Create(Vector2 pos)
		{
			GetTimeSinceRationingNode getTimeSinceRationingNode = ScriptableObject.CreateInstance<GetTimeSinceRationingNode>();
			getTimeSinceRationingNode.rect = new Rect(pos.x, pos.y, 200f, 100f);
			getTimeSinceRationingNode.name = "Get Time Since Rationing";
			getTimeSinceRationingNode.CreateInput("Character", "Character");
			getTimeSinceRationingNode.CreateInput("Consumable Object", "ConsumableRemedium");
			getTimeSinceRationingNode.CreateInput("Day Shift", "Bool");
			getTimeSinceRationingNode.CreateOutput("Last Rationing", "Int");
			return getTimeSinceRationingNode;
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00061794 File Offset: 0x0005F994
		public override Node Duplicate(Vector2 pos)
		{
			GetTimeSinceRationingNode getTimeSinceRationingNode = (GetTimeSinceRationingNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			getTimeSinceRationingNode._character = this._character;
			getTimeSinceRationingNode._consumable = this._consumable;
			getTimeSinceRationingNode._dayShift = this._dayShift;
			return getTimeSinceRationingNode;
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x000617EF File Offset: 0x0005F9EF
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000617F1 File Offset: 0x0005F9F1
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000617F3 File Offset: 0x0005F9F3
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000617F8 File Offset: 0x0005F9F8
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			base.GetInputValue<SecondsCharacter>(this.Inputs[0], ref this._character, canvas);
			base.GetInputValue<SecondsConsumableRemedium>(this.Inputs[1], ref this._consumable, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._dayShift, canvas);
			int num = SecondsRationingManager.Instance.TimeRationing.GetLastRationingTime(this._consumable, this._character);
			if (this._dayShift)
			{
				int num2 = -1;
				CharacterList characterList = CharacterManager.Instance.GetCharacterList();
				for (int i = 0; i < characterList.GetCharacterCount(); i++)
				{
					if (characterList.CharactersInGame[i].Equals(this._character))
					{
						num2 = i;
						break;
					}
				}
				List<Ration> rations = SecondsRationingManager.Instance.RationingData.Rations;
				num++;
				for (int j = 0; j < rations.Count; j++)
				{
					if (rations[j].CharacterIndex == num2 && rations[j].RationedItem.Equals(this._consumable))
					{
						num = 0;
					}
				}
			}
			return base.CastValue<T>(num);
		}

		// Token: 0x04000F12 RID: 3858
		public const string ID = "EE_GetTimeSinceRationingNode";

		// Token: 0x04000F13 RID: 3859
		private const string NODE_NAME = "Get Time Since Rationing";

		// Token: 0x04000F14 RID: 3860
		private const string INPUT_CHARACTER_NAME = "Character";

		// Token: 0x04000F15 RID: 3861
		private const string INPUT_CONSUMABLE_NAME = "Consumable Object";

		// Token: 0x04000F16 RID: 3862
		private const string INPUT_DAY_SHIFT_NAME = "Day Shift";

		// Token: 0x04000F17 RID: 3863
		private const string OUTPUT_LAST_RATIONING_NAME = "Last Rationing";

		// Token: 0x04000F18 RID: 3864
		private const int INPUT_CHARACTER_INDEX = 0;

		// Token: 0x04000F19 RID: 3865
		private const int INPUT_CONSUMABLE_INDEX = 1;

		// Token: 0x04000F1A RID: 3866
		private const int INPUT_INCLUDE_RATIONING_INDEX = 2;

		// Token: 0x04000F1B RID: 3867
		private const int OUTPUT_LAST_RATIONING_INDEX = 0;

		// Token: 0x04000F1C RID: 3868
		[SerializeField]
		private SecondsCharacter _character;

		// Token: 0x04000F1D RID: 3869
		[SerializeField]
		private SecondsConsumableRemedium _consumable;

		// Token: 0x04000F1E RID: 3870
		[SerializeField]
		private bool _dayShift = true;
	}
}
