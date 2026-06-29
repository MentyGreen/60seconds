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
	// Token: 0x02000277 RID: 631
	[Node(true, "Supplies Nodes/Food/Add Food Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddFoodVisualNode : ResourceNode
	{
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001755 RID: 5973 RVA: 0x00066201 File Offset: 0x00064401
		public override string GetID
		{
			get
			{
				return "EE_AddFoodVisualNode";
			}
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x00066208 File Offset: 0x00064408
		public override Node Create(Vector2 pos)
		{
			AddFoodVisualNode addFoodVisualNode = ScriptableObject.CreateInstance<AddFoodVisualNode>();
			addFoodVisualNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			addFoodVisualNode.name = "Add Food Node";
			addFoodVisualNode.CreateMutliInput("In", "Flow");
			addFoodVisualNode.CreateInput("Amount", "Float");
			addFoodVisualNode.CreateInput("Show in Starlog", "Bool");
			addFoodVisualNode.CreateOutput("Out", "Flow");
			addFoodVisualNode.CreateOutput("Current Amount", "Float");
			return addFoodVisualNode;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0006629C File Offset: 0x0006449C
		public override Node Duplicate(Vector2 pos)
		{
			AddFoodVisualNode addFoodVisualNode = (AddFoodVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addFoodVisualNode._amount = this._amount;
			addFoodVisualNode._showStarlogGraphic = this._showStarlogGraphic;
			return addFoodVisualNode;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000662EB File Offset: 0x000644EB
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000662ED File Offset: 0x000644ED
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000662EF File Offset: 0x000644EF
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000662F4 File Offset: 0x000644F4
		public override void Execute(NodeCanvas canvas)
		{
			float num = Convert.ToSingle(this._amount);
			base.GetInputValue<float>(this.Inputs[1], ref num, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._showStarlogGraphic, canvas);
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
			if (this._showStarlogGraphic)
			{
				TextIconJournalContent content = new TextIconJournalContent(consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.ADDITION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000663E8 File Offset: 0x000645E8
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 1)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			return base.CastValue<T>(consumableRemedium.RuntimeData.Amount - consumableRemedium.RuntimeData.PlannedConsumption);
		}

		// Token: 0x04001096 RID: 4246
		public const string ID = "EE_AddFoodVisualNode";

		// Token: 0x04001097 RID: 4247
		private const string NODE_NAME = "Add Food Node";

		// Token: 0x04001098 RID: 4248
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001099 RID: 4249
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x0400109A RID: 4250
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400109B RID: 4251
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x0400109C RID: 4252
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x0400109D RID: 4253
		private const string FOOD_ID = "item_food";

		// Token: 0x0400109E RID: 4254
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400109F RID: 4255
		private const int INPUT_AMOUNT_INDEX = 1;

		// Token: 0x040010A0 RID: 4256
		private const int INPUT_SHOW_GRAPHIC_INDEX = 2;

		// Token: 0x040010A1 RID: 4257
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x040010A2 RID: 4258
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x040010A3 RID: 4259
		[SerializeField]
		private float _amount;

		// Token: 0x040010A4 RID: 4260
		[SerializeField]
		private bool _showStarlogGraphic = true;
	}
}
