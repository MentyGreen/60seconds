using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.SecondsRemaster.Core;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000282 RID: 642
	[Node(false, "Supplies Nodes/Consumables/Set Time Since Rationing Node", new Type[]
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
	public class SetTimeSinceRationingNode : ResourceNode
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x00067E89 File Offset: 0x00066089
		public override string GetID
		{
			get
			{
				return "EE_SetTimeSinceRationingNode";
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00067E90 File Offset: 0x00066090
		public override Node Create(Vector2 pos)
		{
			SetTimeSinceRationingNode setTimeSinceRationingNode = ScriptableObject.CreateInstance<SetTimeSinceRationingNode>();
			setTimeSinceRationingNode.rect = new Rect(pos.x, pos.y, 200f, 100f);
			setTimeSinceRationingNode.name = "Set Time Since Rationing";
			setTimeSinceRationingNode.CreateMutliInput("In", "Flow");
			setTimeSinceRationingNode.CreateInput("Character", "Character");
			setTimeSinceRationingNode.CreateInput("Consumable Object", "ConsumableRemedium");
			setTimeSinceRationingNode.CreateInput("Last Rationing", "Int");
			setTimeSinceRationingNode.CreateOutput("Out", "Flow");
			return setTimeSinceRationingNode;
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x00067F24 File Offset: 0x00066124
		public override Node Duplicate(Vector2 pos)
		{
			SetTimeSinceRationingNode setTimeSinceRationingNode = (SetTimeSinceRationingNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			setTimeSinceRationingNode._character = this._character;
			setTimeSinceRationingNode._consumable = this._consumable;
			setTimeSinceRationingNode._days = this._days;
			return setTimeSinceRationingNode;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x00067F7F File Offset: 0x0006617F
		protected override void NodeEnable()
		{
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x00067F81 File Offset: 0x00066181
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00067F83 File Offset: 0x00066183
		protected override void NodeGUI()
		{
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00067F88 File Offset: 0x00066188
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<SecondsCharacter>(this.Inputs[1], ref this._character, canvas);
			base.GetInputValue<SecondsConsumableRemedium>(this.Inputs[2], ref this._consumable, canvas);
			base.GetInputValue<int>(this.Inputs[3], ref this._days, canvas);
			SecondsRationingManager.Instance.TimeRationing.SetLastRationingTime(this._consumable, this._character, this._days);
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400113E RID: 4414
		public const string ID = "EE_SetTimeSinceRationingNode";

		// Token: 0x0400113F RID: 4415
		private const string NODE_NAME = "Set Time Since Rationing";

		// Token: 0x04001140 RID: 4416
		private const string INPUT_FLOW_NAME = "In";

		// Token: 0x04001141 RID: 4417
		private const string INPUT_CHARACTER_NAME = "Character";

		// Token: 0x04001142 RID: 4418
		private const string INPUT_CONSUMABLE_NAME = "Consumable Object";

		// Token: 0x04001143 RID: 4419
		private const string INPUT_LAST_RATIONING_NAME = "Last Rationing";

		// Token: 0x04001144 RID: 4420
		private const string OUTPUT_FLOW_NAME = "Out";

		// Token: 0x04001145 RID: 4421
		private const int INPUT_FLOW_INDEX = 0;

		// Token: 0x04001146 RID: 4422
		private const int INPUT_CHARACTER_INDEX = 1;

		// Token: 0x04001147 RID: 4423
		private const int INPUT_CONSUMABLE_INDEX = 2;

		// Token: 0x04001148 RID: 4424
		private const int INPUT_LAST_RATIONING_INDEX = 3;

		// Token: 0x04001149 RID: 4425
		private const int OUTPUT_FLOW_INDEX = 0;

		// Token: 0x0400114A RID: 4426
		[SerializeField]
		private SecondsCharacter _character;

		// Token: 0x0400114B RID: 4427
		[SerializeField]
		private SecondsConsumableRemedium _consumable;

		// Token: 0x0400114C RID: 4428
		[SerializeField]
		private int _days;
	}
}
