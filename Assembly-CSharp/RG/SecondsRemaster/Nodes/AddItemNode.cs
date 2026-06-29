using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200026A RID: 618
	[Node(true, "Legacy/Add Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddItemNode : ResourceNode
	{
		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00064AE5 File Offset: 0x00062CE5
		public override string GetID
		{
			get
			{
				return "EE_AddItemNode";
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00064AEC File Offset: 0x00062CEC
		public override Node Create(Vector2 pos)
		{
			AddItemNode addItemNode = ScriptableObject.CreateInstance<AddItemNode>();
			addItemNode.rect = new Rect(pos.x, pos.y, 180f, 100f);
			addItemNode.name = "Add Item";
			addItemNode.CreateMutliInput("In", "Flow");
			addItemNode.CreateInput("Item", "Item");
			addItemNode.CreateOutput("Out", "Flow");
			return addItemNode;
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00064B5D File Offset: 0x00062D5D
		public override Node Duplicate(Vector2 pos)
		{
			AddItemNode addItemNode = (AddItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addItemNode._item = this._item;
			return addItemNode;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00064B95 File Offset: 0x00062D95
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00064B97 File Offset: 0x00062D97
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00064B99 File Offset: 0x00062D99
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00064B9C File Offset: 0x00062D9C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			bool flag = this._item is ConsumableRemedium;
			if (CraftingManager.IsCraftingOngoing() && !flag)
			{
				if (CraftingManager.InterruptCraftingIfOngoing(this._item))
				{
					EPlannedCraftingAction interruptedOperation = CraftingManager.GetInterruptedOperation();
					if (interruptedOperation == EPlannedCraftingAction.CRAFT || interruptedOperation == EPlannedCraftingAction.RECYCLE || interruptedOperation == EPlannedCraftingAction.REPAIR)
					{
						this.DisplayAdditionTextIconContent();
						this._item.Add();
					}
					else if (interruptedOperation == EPlannedCraftingAction.UPGRADE)
					{
						this.DisplayAdditionTextIconContent();
					}
				}
				else
				{
					if (!this._item.BaseRuntimeData.IsAvailable)
					{
						this.DisplayAdditionTextIconContent();
					}
					this._item.Add();
				}
			}
			else
			{
				if (flag)
				{
					this.DisplayAdditionTextIconContent();
				}
				else if (!this._item.BaseRuntimeData.IsAvailable || this._item.IsDamaged())
				{
					this.DisplayAdditionTextIconContent();
				}
				this._item.Add();
			}
			ItemCollectedStatsEntry itemCollectedStatsEntry = new ItemCollectedStatsEntry();
			itemCollectedStatsEntry.FromExpedition = (this.parentCanvas is ExpeditionEvent);
			itemCollectedStatsEntry.ItemId = this._item.BaseStaticData.ItemId;
			StatsManager.Instance.AddItemCollectedStatsEntry(itemCollectedStatsEntry);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00064CD4 File Offset: 0x00062ED4
		private void DisplayAdditionTextIconContent()
		{
			TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.ADDITION, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x04001006 RID: 4102
		public const string ID = "EE_AddItemNode";

		// Token: 0x04001007 RID: 4103
		[SerializeField]
		private IItem _item;

		// Token: 0x04001008 RID: 4104
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001009 RID: 4105
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x0400100A RID: 4106
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400100B RID: 4107
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400100C RID: 4108
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x0400100D RID: 4109
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400100E RID: 4110
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
