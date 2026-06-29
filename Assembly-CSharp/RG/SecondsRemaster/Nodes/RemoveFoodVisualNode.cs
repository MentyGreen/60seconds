using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200027B RID: 635
	[Node(true, "Legacy/Food/Remove Food Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(Goal)
	})]
	public class RemoveFoodVisualNode : ResourceNode
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00066A73 File Offset: 0x00064C73
		public override string GetID
		{
			get
			{
				return "EE_RemoveFoodVisualNode";
			}
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x00066A7C File Offset: 0x00064C7C
		public override Node Create(Vector2 pos)
		{
			RemoveFoodVisualNode removeFoodVisualNode = ScriptableObject.CreateInstance<RemoveFoodVisualNode>();
			removeFoodVisualNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			removeFoodVisualNode.name = "Remove Food Node";
			removeFoodVisualNode.CreateMutliInput("In", "Flow");
			removeFoodVisualNode.CreateInput("Amount", "Float");
			removeFoodVisualNode.CreateInput("Show in Starlog", "Bool");
			removeFoodVisualNode.CreateOutput("Out", "Flow");
			removeFoodVisualNode.CreateOutput("Current Amount", "Float");
			removeFoodVisualNode.CreateOutput("Is Operation Finished", "Bool");
			return removeFoodVisualNode;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00066B20 File Offset: 0x00064D20
		public override Node Duplicate(Vector2 pos)
		{
			RemoveFoodVisualNode removeFoodVisualNode = (RemoveFoodVisualNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeFoodVisualNode._amount = this._amount;
			removeFoodVisualNode._showStarlogGraphic = this._showStarlogGraphic;
			return removeFoodVisualNode;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00066B6F File Offset: 0x00064D6F
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00066B71 File Offset: 0x00064D71
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00066B73 File Offset: 0x00064D73
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00066B78 File Offset: 0x00064D78
		public override void Execute(NodeCanvas canvas)
		{
			float amount = Convert.ToSingle(this._amount);
			base.GetInputValue<float>(this.Inputs[1], ref amount, canvas);
			base.GetInputValue<bool>(this.Inputs[2], ref this._showStarlogGraphic, canvas);
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			this._isOperationSuccess = false;
			if (consumableRemedium.RuntimeData.Amount > 0f)
			{
				this._isOperationSuccess = true;
				float num = consumableRemedium.RemoveAndGetRemovedAmount(amount);
				if (this._showStarlogGraphic)
				{
					TextIconJournalContent content = new TextIconJournalContent(consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.SUBTRACTION, 0);
					SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
				}
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00066C44 File Offset: 0x00064E44
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output == 1)
			{
				ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
				return base.CastValue<T>(consumableRemedium.RuntimeData.Amount - consumableRemedium.RuntimeData.PlannedConsumption);
			}
			if (output == 2)
			{
				return base.CastValue<T>(this._isOperationSuccess);
			}
			throw new NotExistingOutputException(this.GetID, output);
		}

		// Token: 0x040010D4 RID: 4308
		public const string ID = "EE_RemoveFoodVisualNode";

		// Token: 0x040010D5 RID: 4309
		private const string NODE_NAME = "Remove Food Node";

		// Token: 0x040010D6 RID: 4310
		private const string INPUT_IN_NAME = "In";

		// Token: 0x040010D7 RID: 4311
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x040010D8 RID: 4312
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x040010D9 RID: 4313
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x040010DA RID: 4314
		private const string OUTPUT_OPERATION_STATUS = "Is Operation Finished";

		// Token: 0x040010DB RID: 4315
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x040010DC RID: 4316
		private const string FOOD_ID = "item_food";

		// Token: 0x040010DD RID: 4317
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x040010DE RID: 4318
		private const int INPUT_AMOUNT_INDEX = 1;

		// Token: 0x040010DF RID: 4319
		private const int INPUT_SHOW_GRAPHIC_INDEX = 2;

		// Token: 0x040010E0 RID: 4320
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x040010E1 RID: 4321
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x040010E2 RID: 4322
		private const int OUTPUT_OPERATION_INDEX = 2;

		// Token: 0x040010E3 RID: 4323
		[SerializeField]
		private int _amount;

		// Token: 0x040010E4 RID: 4324
		[SerializeField]
		private bool _showStarlogGraphic = true;

		// Token: 0x040010E5 RID: 4325
		private bool _isOperationSuccess;
	}
}
