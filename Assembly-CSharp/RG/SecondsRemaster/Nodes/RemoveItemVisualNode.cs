using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200026E RID: 622
	[Node(false, "Supplies Nodes/Items/Remove Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class RemoveItemVisualNode : ResourceNode
	{
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00065292 File Offset: 0x00063492
		public override string GetID
		{
			get
			{
				return "EE_RemoveItemVisualNode";
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0006529C File Offset: 0x0006349C
		public override Node Create(Vector2 pos)
		{
			RemoveItemVisualNode removeItemVisualNode = ScriptableObject.CreateInstance<RemoveItemVisualNode>();
			removeItemVisualNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			removeItemVisualNode.name = "Remove Item Visual";
			removeItemVisualNode.CreateMutliInput("In", "Flow");
			removeItemVisualNode.CreateInput("Item", "Item");
			removeItemVisualNode.CreateInput("Show in Starlog", "Bool");
			removeItemVisualNode.CreateOutput("Out", "Flow");
			return removeItemVisualNode;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0006531E File Offset: 0x0006351E
		public override Node Duplicate(Vector2 pos)
		{
			RemoveItemVisualNode removeItemVisualNode = (RemoveItemVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeItemVisualNode._item = this._item;
			return removeItemVisualNode;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00065356 File Offset: 0x00063556
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00065358 File Offset: 0x00063558
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0006535A File Offset: 0x0006355A
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0006535C File Offset: 0x0006355C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._showStarlogGraphic, canvas);
			if (this._item.BaseRuntimeData.IsAvailable && this._showStarlogGraphic && !this._item.IsDamaged())
			{
				TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			this._item.Remove();
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001030 RID: 4144
		public const string ID = "EE_RemoveItemVisualNode";

		// Token: 0x04001031 RID: 4145
		[SerializeField]
		private IItem _item;

		// Token: 0x04001032 RID: 4146
		[SerializeField]
		private bool _showStarlogGraphic = true;

		// Token: 0x04001033 RID: 4147
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001034 RID: 4148
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001035 RID: 4149
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x04001036 RID: 4150
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001037 RID: 4151
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001038 RID: 4152
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x04001039 RID: 4153
		private const int INPUT_SHOW_GRAPHIC_INDEX = 2;

		// Token: 0x0400103A RID: 4154
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400103B RID: 4155
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
