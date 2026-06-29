using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200026D RID: 621
	[Node(true, "Legacy/Remove Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class RemoveItemNode : ResourceNode
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x00065140 File Offset: 0x00063340
		public override string GetID
		{
			get
			{
				return "EE_RemoveItemNode";
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00065148 File Offset: 0x00063348
		public override Node Create(Vector2 pos)
		{
			RemoveItemNode removeItemNode = ScriptableObject.CreateInstance<RemoveItemNode>();
			removeItemNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			removeItemNode.name = "Remove Item";
			removeItemNode.CreateMutliInput("In", "Flow");
			removeItemNode.CreateInput("Item", "Item");
			removeItemNode.CreateOutput("Out", "Flow");
			return removeItemNode;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000651B9 File Offset: 0x000633B9
		public override Node Duplicate(Vector2 pos)
		{
			RemoveItemNode removeItemNode = (RemoveItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeItemNode._item = this._item;
			return removeItemNode;
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000651F1 File Offset: 0x000633F1
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000651F3 File Offset: 0x000633F3
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000651F5 File Offset: 0x000633F5
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000651F8 File Offset: 0x000633F8
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			if (this._item.BaseRuntimeData.IsAvailable && !this._item.IsDamaged())
			{
				TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			this._item.Remove();
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001027 RID: 4135
		public const string ID = "EE_RemoveItemNode";

		// Token: 0x04001028 RID: 4136
		[SerializeField]
		private IItem _item;

		// Token: 0x04001029 RID: 4137
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400102A RID: 4138
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x0400102B RID: 4139
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400102C RID: 4140
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400102D RID: 4141
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x0400102E RID: 4142
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400102F RID: 4143
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
