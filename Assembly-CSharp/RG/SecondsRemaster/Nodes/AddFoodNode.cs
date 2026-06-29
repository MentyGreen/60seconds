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
	// Token: 0x02000276 RID: 630
	[Node(true, "Legacy/Add Food Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddFoodNode : ResourceNode
	{
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600174C RID: 5964 RVA: 0x00066007 File Offset: 0x00064207
		public override string GetID
		{
			get
			{
				return "EE_AddFoodNode";
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00066010 File Offset: 0x00064210
		public override Node Create(Vector2 pos)
		{
			AddFoodNode addFoodNode = ScriptableObject.CreateInstance<AddFoodNode>();
			addFoodNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			addFoodNode.name = "Add Food Node";
			addFoodNode.CreateMutliInput("In", "Flow");
			addFoodNode.CreateInput("Amount", "Float");
			addFoodNode.CreateOutput("Out", "Flow");
			addFoodNode.CreateOutput("Current Amount", "Float");
			return addFoodNode;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00066092 File Offset: 0x00064292
		public override Node Duplicate(Vector2 pos)
		{
			AddFoodNode addFoodNode = (AddFoodNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addFoodNode._amount = this._amount;
			return addFoodNode;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000660CA File Offset: 0x000642CA
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000660CC File Offset: 0x000642CC
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000660CE File Offset: 0x000642CE
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000660D0 File Offset: 0x000642D0
		public override void Execute(NodeCanvas canvas)
		{
			float num = Convert.ToSingle(this._amount);
			base.GetInputValue<float>(this.Inputs[1], ref num, canvas);
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			consumableRemedium.Add(num);
			int num2 = Convert.ToInt32(num);
			for (int i = 0; i < num2; i++)
			{
				ItemCollectedStatsEntry itemCollectedStatsEntry = new ItemCollectedStatsEntry();
				itemCollectedStatsEntry.FromExpedition = (this.parentCanvas is ExpeditionEvent);
				itemCollectedStatsEntry.ItemId = "item_food";
				StatsManager.Instance.AddItemCollectedStatsEntry(itemCollectedStatsEntry);
			}
			TextIconJournalContent content = new TextIconJournalContent(consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.ADDITION, 0);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x000661A4 File Offset: 0x000643A4
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 1)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			return base.CastValue<T>(consumableRemedium.RuntimeData.Amount - consumableRemedium.RuntimeData.PlannedConsumption);
		}

		// Token: 0x0400108A RID: 4234
		public const string ID = "EE_AddFoodNode";

		// Token: 0x0400108B RID: 4235
		private const string NODE_NAME = "Add Food Node";

		// Token: 0x0400108C RID: 4236
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400108D RID: 4237
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x0400108E RID: 4238
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400108F RID: 4239
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x04001090 RID: 4240
		private const string FOOD_ID = "item_food";

		// Token: 0x04001091 RID: 4241
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001092 RID: 4242
		private const int INPUT_AMOUNT_INDEX = 1;

		// Token: 0x04001093 RID: 4243
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04001094 RID: 4244
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x04001095 RID: 4245
		[SerializeField]
		private float _amount;
	}
}
