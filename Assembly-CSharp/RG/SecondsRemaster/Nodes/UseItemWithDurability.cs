using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000272 RID: 626
	[Node(false, "Supplies Nodes/Items/Use Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class UseItemWithDurability : ResourceNode
	{
		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001728 RID: 5928 RVA: 0x000658E8 File Offset: 0x00063AE8
		public override string GetID
		{
			get
			{
				return "EE_UseItemWithDurabailityNode";
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000658F0 File Offset: 0x00063AF0
		public override Node Create(Vector2 pos)
		{
			UseItemWithDurability useItemWithDurability = ScriptableObject.CreateInstance<UseItemWithDurability>();
			useItemWithDurability.rect = new Rect(pos.x, pos.y, 180f, 130f);
			useItemWithDurability.name = "Use Item";
			useItemWithDurability.CreateMutliInput("In", "Flow");
			useItemWithDurability.CreateInput("Item", "Item");
			useItemWithDurability.CreateInput("Min", "Int");
			useItemWithDurability.CreateInput("Max", "Int");
			useItemWithDurability.CreateOutput("Out", "Flow");
			return useItemWithDurability;
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00065984 File Offset: 0x00063B84
		public override Node Duplicate(Vector2 pos)
		{
			UseItemWithDurability useItemWithDurability = (UseItemWithDurability)this.Create(this.rect.position + new Vector2(20f, 20f));
			useItemWithDurability._item = this._item;
			useItemWithDurability._max = this._max;
			useItemWithDurability._min = this._min;
			return useItemWithDurability;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000659DF File Offset: 0x00063BDF
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000659E1 File Offset: 0x00063BE1
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000659E3 File Offset: 0x00063BE3
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000659E8 File Offset: 0x00063BE8
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._min, canvas);
			base.GetInputValue<int>(this.Inputs[3], ref this._max, canvas);
			if (!this._item.IsDamaged())
			{
				Item item = this._item as Item;
				bool isAvailable = this._item.BaseRuntimeData.IsAvailable;
				if (item != null)
				{
					item.UseItem(Random.Range(this._min, this._max));
				}
				else
				{
					this._item.Use();
				}
				if (this._item.IsDamaged() || (!this._item.BaseRuntimeData.IsAvailable && isAvailable))
				{
					TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
					SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
				}
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001060 RID: 4192
		public const string ID = "EE_UseItemWithDurabailityNode";

		// Token: 0x04001061 RID: 4193
		[SerializeField]
		private IItem _item;

		// Token: 0x04001062 RID: 4194
		[SerializeField]
		private int _min = 10;

		// Token: 0x04001063 RID: 4195
		[SerializeField]
		private int _max = 20;

		// Token: 0x04001064 RID: 4196
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001065 RID: 4197
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001066 RID: 4198
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001067 RID: 4199
		private const string INPUT_MIN_NAME = "Min";

		// Token: 0x04001068 RID: 4200
		private const string INPUT_MAX_NAME = "Max";

		// Token: 0x04001069 RID: 4201
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400106A RID: 4202
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x0400106B RID: 4203
		private const int INPUT_MIN_INDEX = 2;

		// Token: 0x0400106C RID: 4204
		private const int INPUT_MAX_INDEX = 3;

		// Token: 0x0400106D RID: 4205
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400106E RID: 4206
		private const string NODE_NAME = "Use Item";

		// Token: 0x0400106F RID: 4207
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
