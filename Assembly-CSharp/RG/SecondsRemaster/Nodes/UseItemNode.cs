using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000271 RID: 625
	[Node(true, "Legacy/Use Item Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class UseItemNode : ResourceNode
	{
		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x0006576C File Offset: 0x0006396C
		public override string GetID
		{
			get
			{
				return "EE_UseItemNode";
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00065774 File Offset: 0x00063974
		public override Node Create(Vector2 pos)
		{
			UseItemNode useItemNode = ScriptableObject.CreateInstance<UseItemNode>();
			useItemNode.rect = new Rect(pos.x, pos.y, 180f, 130f);
			useItemNode.name = "Use Item ";
			useItemNode.CreateMutliInput("In", "Flow");
			useItemNode.CreateInput("Item", "Item");
			useItemNode.CreateOutput("Out", "Flow");
			return useItemNode;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000657E5 File Offset: 0x000639E5
		public override Node Duplicate(Vector2 pos)
		{
			UseItemNode useItemNode = (UseItemNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			useItemNode._item = this._item;
			return useItemNode;
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x0006581D File Offset: 0x00063A1D
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0006581F File Offset: 0x00063A1F
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00065821 File Offset: 0x00063A21
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00065824 File Offset: 0x00063A24
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<IItem>(this.Inputs[1], ref this._item, canvas);
			if (!this._item.IsDamaged())
			{
				IItem item = this._item;
				bool isAvailable = this._item.BaseRuntimeData.IsAvailable;
				this._item.Use();
				if (this._item.IsDamaged() || (!this._item.BaseRuntimeData.IsAvailable && isAvailable))
				{
					TextIconJournalContent content = new TextIconJournalContent(this._item.BaseStaticData.IconTerm, 1, EventContentData.ETextIconContentType.SUBTRACTION, 0);
					SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
				}
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001057 RID: 4183
		public const string ID = "EE_UseItemNode";

		// Token: 0x04001058 RID: 4184
		[SerializeField]
		private IItem _item;

		// Token: 0x04001059 RID: 4185
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400105A RID: 4186
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x0400105B RID: 4187
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400105C RID: 4188
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400105D RID: 4189
		private const int INPUT_ITEM_INDEX = 1;

		// Token: 0x0400105E RID: 4190
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x0400105F RID: 4191
		private const string OUTPUT_NOT_CONNECTED_MESSAGE = "Output is not connected";
	}
}
