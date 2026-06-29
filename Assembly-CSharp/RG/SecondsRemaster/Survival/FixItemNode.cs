using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Core;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002DA RID: 730
	[Node(false, "Supplies Nodes/Items/Fix Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class FixItemNode : ResourceNode
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0006F3CF File Offset: 0x0006D5CF
		public override string GetID
		{
			get
			{
				return "EE_FixItemNode";
			}
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x0006F3D8 File Offset: 0x0006D5D8
		public override Node Create(Vector2 pos)
		{
			FixItemNode fixItemNode = ScriptableObject.CreateInstance<FixItemNode>();
			fixItemNode.rect = new Rect(pos.x, pos.y, 180f, 130f);
			fixItemNode.name = "Fix Item";
			fixItemNode.CreateMutliInput("In", "Flow");
			fixItemNode.CreateInput("Item", "Item");
			fixItemNode.CreateOutput("Out", "Flow");
			return fixItemNode;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x0006F449 File Offset: 0x0006D649
		public override Node Duplicate(Vector2 pos)
		{
			FixItemNode fixItemNode = (FixItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			fixItemNode._item = this._item;
			return fixItemNode;
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x0006F481 File Offset: 0x0006D681
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x0006F483 File Offset: 0x0006D683
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006F485 File Offset: 0x0006D685
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x0006F488 File Offset: 0x0006D688
		public override void Execute(NodeCanvas canvas)
		{
			IItem item = this._item;
			base.GetInputValue<IItem>(this.Inputs[1], ref item, canvas);
			bool flag = false;
			bool flag2 = false;
			if (item is Item)
			{
				Item item2 = item as Item;
				flag = item2.RuntimeData.IsDamaged;
				item2.Repair();
				flag2 = item2.RuntimeData.IsDamaged;
			}
			else if (item is SecondsRemedium)
			{
				SecondsRemedium secondsRemedium = item as SecondsRemedium;
				flag = secondsRemedium.SecondsRemediumRuntimeData.IsDamaged;
				secondsRemedium.Repair();
				flag2 = secondsRemedium.SecondsRemediumRuntimeData.IsDamaged;
			}
			else
			{
				item = null;
			}
			if (item != null && !flag2 && flag)
			{
				TextIconJournalContent content = new TextIconJournalContent(item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.ADDITION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0400138A RID: 5002
		public const string ID = "EE_FixItemNode";

		// Token: 0x0400138B RID: 5003
		[SerializeField]
		private Item _item;

		// Token: 0x0400138C RID: 5004
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400138D RID: 5005
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x0400138E RID: 5006
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400138F RID: 5007
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001390 RID: 5008
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x04001391 RID: 5009
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001392 RID: 5010
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
