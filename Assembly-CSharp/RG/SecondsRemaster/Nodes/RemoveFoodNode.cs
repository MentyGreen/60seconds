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
	// Token: 0x02000279 RID: 633
	[Node(true, "Legacy/Food/Remove Food Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class RemoveFoodNode : ResourceNode
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x000666BB File Offset: 0x000648BB
		public override string GetID
		{
			get
			{
				return "EE_RemoveFoodNode";
			}
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000666C4 File Offset: 0x000648C4
		public override Node Create(Vector2 pos)
		{
			RemoveFoodNode removeFoodNode = ScriptableObject.CreateInstance<RemoveFoodNode>();
			removeFoodNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			removeFoodNode.name = "Remove Food Node";
			removeFoodNode.CreateMutliInput("In", "Flow");
			removeFoodNode.CreateInput("Amount", "Float");
			removeFoodNode.CreateOutput("Out", "Flow");
			removeFoodNode.CreateOutput("Current Amount", "Float");
			return removeFoodNode;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00066746 File Offset: 0x00064946
		public override Node Duplicate(Vector2 pos)
		{
			RemoveFoodNode removeFoodNode = (RemoveFoodNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeFoodNode._amount = this._amount;
			return removeFoodNode;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0006677E File Offset: 0x0006497E
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00066780 File Offset: 0x00064980
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00066782 File Offset: 0x00064982
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00066784 File Offset: 0x00064984
		public override void Execute(NodeCanvas canvas)
		{
			float amount = Convert.ToSingle(this._amount);
			base.GetInputValue<float>(this.Inputs[1], ref amount, canvas);
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			if (consumableRemedium.RuntimeData.Amount > 0f)
			{
				float num = consumableRemedium.RemoveAndGetRemovedAmount(amount);
				TextIconJournalContent content = new TextIconJournalContent(consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00066820 File Offset: 0x00064A20
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 1)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			return base.CastValue<T>(consumableRemedium.RuntimeData.Amount - consumableRemedium.RuntimeData.PlannedConsumption);
		}

		// Token: 0x040010B9 RID: 4281
		public const string ID = "EE_RemoveFoodNode";

		// Token: 0x040010BA RID: 4282
		private const string NODE_NAME = "Remove Food Node";

		// Token: 0x040010BB RID: 4283
		private const string INPUT_IN_NAME = "In";

		// Token: 0x040010BC RID: 4284
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x040010BD RID: 4285
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x040010BE RID: 4286
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x040010BF RID: 4287
		private const string FOOD_ID = "item_food";

		// Token: 0x040010C0 RID: 4288
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x040010C1 RID: 4289
		private const int INPUT_AMOUNT_INDEX = 1;

		// Token: 0x040010C2 RID: 4290
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x040010C3 RID: 4291
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x040010C4 RID: 4292
		[SerializeField]
		private int _amount;
	}
}
