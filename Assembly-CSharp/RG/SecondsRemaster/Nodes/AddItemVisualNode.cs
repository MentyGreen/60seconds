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
	// Token: 0x0200026B RID: 619
	[Node(false, "Supplies Nodes/Items/Add Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddItemVisualNode : ResourceNode
	{
		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00064D0E File Offset: 0x00062F0E
		public override string GetID
		{
			get
			{
				return "EE_AddItemVisualNode";
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00064D18 File Offset: 0x00062F18
		public override Node Create(Vector2 pos)
		{
			AddItemVisualNode addItemVisualNode = ScriptableObject.CreateInstance<AddItemVisualNode>();
			addItemVisualNode.rect = new Rect(pos.x, pos.y, 180f, 100f);
			addItemVisualNode.name = "Add Item Visual";
			addItemVisualNode.CreateMutliInput("In", "Flow");
			addItemVisualNode.CreateInput("Item", "Item");
			addItemVisualNode.CreateInput("Show in Starlog", "Bool");
			addItemVisualNode.CreateOutput("Out", "Flow");
			return addItemVisualNode;
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00064D9C File Offset: 0x00062F9C
		public override Node Duplicate(Vector2 pos)
		{
			AddItemVisualNode addItemVisualNode = (AddItemVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addItemVisualNode._item = this._item;
			addItemVisualNode._showStarlogGraphic = this._showStarlogGraphic;
			return addItemVisualNode;
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00064DEB File Offset: 0x00062FEB
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00064DED File Offset: 0x00062FED
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00064DEF File Offset: 0x00062FEF
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00064DF4 File Offset: 0x00062FF4
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._showStarlogGraphic, canvas);
			bool flag = this._item is ConsumableRemedium;
			if (CraftingManager.IsCraftingOngoing() && !flag)
			{
				if (CraftingManager.InterruptCraftingIfOngoing(this._item))
				{
					EPlannedCraftingAction interruptedOperation = CraftingManager.GetInterruptedOperation();
					if (interruptedOperation == EPlannedCraftingAction.CRAFT || interruptedOperation == EPlannedCraftingAction.RECYCLE || interruptedOperation == EPlannedCraftingAction.REPAIR)
					{
						if (this._showStarlogGraphic)
						{
							this.DisplayAdditionTextIconContent();
						}
						this._item.Add();
					}
					else if (interruptedOperation == EPlannedCraftingAction.UPGRADE && this._showStarlogGraphic)
					{
						this.DisplayAdditionTextIconContent();
					}
				}
				else
				{
					if (!this._item.BaseRuntimeData.IsAvailable && this._showStarlogGraphic)
					{
						this.DisplayAdditionTextIconContent();
					}
					this._item.Add();
				}
			}
			else
			{
				if (flag && this._showStarlogGraphic)
				{
					this.DisplayAdditionTextIconContent();
				}
				else if ((!this._item.BaseRuntimeData.IsAvailable || this._item.IsDamaged()) && this._showStarlogGraphic)
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

		// Token: 0x060016F6 RID: 5878 RVA: 0x00064F7C File Offset: 0x0006317C
		private void DisplayAdditionTextIconContent()
		{
			TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.ADDITION, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x0400100F RID: 4111
		public const string ID = "EE_AddItemVisualNode";

		// Token: 0x04001010 RID: 4112
		[SerializeField]
		private IItem _item;

		// Token: 0x04001011 RID: 4113
		[SerializeField]
		private bool _showStarlogGraphic = true;

		// Token: 0x04001012 RID: 4114
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001013 RID: 4115
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001014 RID: 4116
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x04001015 RID: 4117
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001016 RID: 4118
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001017 RID: 4119
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x04001018 RID: 4120
		private const int INPUT_SHOW_GRAPHIC_INDEX = 2;

		// Token: 0x04001019 RID: 4121
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400101A RID: 4122
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
