using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000275 RID: 629
	[Node(false, "Supplies Nodes/Consumables/Add Consumable Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class AddConsumableNode : ResourceNode
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x00065DF1 File Offset: 0x00063FF1
		public override string GetID
		{
			get
			{
				return "EE_AddConsumableNode";
			}
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x00065DF8 File Offset: 0x00063FF8
		public override Node Create(Vector2 pos)
		{
			AddConsumableNode addConsumableNode = ScriptableObject.CreateInstance<AddConsumableNode>();
			addConsumableNode.rect = new Rect(pos.x, pos.y, 180f, 100f);
			addConsumableNode.name = "Add Consumable";
			addConsumableNode.CreateMutliInput("In", "Flow");
			addConsumableNode.CreateInput("Consumable object", "ConsumableRemedium");
			addConsumableNode.CreateInput("Amount", "Float");
			addConsumableNode.CreateInput("Show in Starlog", "Bool");
			addConsumableNode.CreateOutput("Out", "Flow");
			addConsumableNode.CreateOutput("Current Amount", "Float");
			return addConsumableNode;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00065E9C File Offset: 0x0006409C
		public override Node Duplicate(Vector2 pos)
		{
			AddConsumableNode addConsumableNode = (AddConsumableNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			addConsumableNode._amount = this._amount;
			addConsumableNode._consumableRemedium = this._consumableRemedium;
			addConsumableNode._showStarlogGraphic = this._showStarlogGraphic;
			return addConsumableNode;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00065EF7 File Offset: 0x000640F7
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00065EF9 File Offset: 0x000640F9
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00065EFB File Offset: 0x000640FB
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00065F00 File Offset: 0x00064100
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<float>(this.Inputs[2], ref this._amount, canvas);
			base.GetInputValue<bool>(this.Inputs[3], ref this._showStarlogGraphic, canvas);
			base.GetInputValue<ConsumableRemedium>(this.Inputs[1], ref this._consumableRemedium, canvas);
			this._consumableRemedium.Add(this._amount);
			if (this._showStarlogGraphic)
			{
				TextIconJournalContent content = new TextIconJournalContent(this._consumableRemedium.BaseStaticData.IconTerm, (int)this._amount, EventContentData.ETextIconContentType.ADDITION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00065FB9 File Offset: 0x000641B9
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 1)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(this._consumableRemedium.RuntimeData.Amount - this._consumableRemedium.RuntimeData.PlannedConsumption);
		}

		// Token: 0x04001079 RID: 4217
		public const string ID = "EE_AddConsumableNode";

		// Token: 0x0400107A RID: 4218
		private const string NODE_NAME = "Add Consumable";

		// Token: 0x0400107B RID: 4219
		private const string INPUT_FLOW_NAME = "In";

		// Token: 0x0400107C RID: 4220
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x0400107D RID: 4221
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x0400107E RID: 4222
		private const string INPUT_CONSUMABLE_NAME = "Consumable object";

		// Token: 0x0400107F RID: 4223
		private const string OUTPUT_FLOW_NAME = "Out";

		// Token: 0x04001080 RID: 4224
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x04001081 RID: 4225
		private const int INPUT_FLOW_INDEX = 0;

		// Token: 0x04001082 RID: 4226
		private const int INPUT_CONSUMABLE_INDEX = 1;

		// Token: 0x04001083 RID: 4227
		private const int INPUT_AMOUNT_INDEX = 2;

		// Token: 0x04001084 RID: 4228
		private const int INPUT_SHOW_GRAPHIC_INDEX = 3;

		// Token: 0x04001085 RID: 4229
		private const int OUTPUT_FLOW_INDEX = 0;

		// Token: 0x04001086 RID: 4230
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x04001087 RID: 4231
		[SerializeField]
		private float _amount;

		// Token: 0x04001088 RID: 4232
		[SerializeField]
		private ConsumableRemedium _consumableRemedium;

		// Token: 0x04001089 RID: 4233
		[SerializeField]
		private bool _showStarlogGraphic = true;
	}
}
