using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000278 RID: 632
	[Node(false, "Supplies Nodes/Consumables/Remove Consumable Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class RemoveConsumableNode : ResourceNode
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x0006644C File Offset: 0x0006464C
		public override string GetID
		{
			get
			{
				return "EE_RemoveConsumableNode";
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00066454 File Offset: 0x00064654
		public override Node Create(Vector2 pos)
		{
			RemoveConsumableNode removeConsumableNode = ScriptableObject.CreateInstance<RemoveConsumableNode>();
			removeConsumableNode.rect = new Rect(pos.x, pos.y, 230f, 100f);
			removeConsumableNode.name = "Remove Consumable";
			removeConsumableNode.CreateMutliInput("In", "Flow");
			removeConsumableNode.CreateInput("Consumable object", "ConsumableRemedium");
			removeConsumableNode.CreateInput("Amount", "Float");
			removeConsumableNode.CreateInput("Show in Starlog", "Bool");
			removeConsumableNode.CreateOutput("Out", "Flow");
			removeConsumableNode.CreateOutput("Current Amount", "Float");
			removeConsumableNode.CreateOutput("Is Operation Finished", "Bool");
			return removeConsumableNode;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0006650C File Offset: 0x0006470C
		public override Node Duplicate(Vector2 pos)
		{
			RemoveConsumableNode removeConsumableNode = (RemoveConsumableNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeConsumableNode._amount = this._amount;
			removeConsumableNode._consumableRemedium = this._consumableRemedium;
			removeConsumableNode._showStarlogGraphic = this._showStarlogGraphic;
			return removeConsumableNode;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x00066567 File Offset: 0x00064767
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00066569 File Offset: 0x00064769
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0006656B File Offset: 0x0006476B
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00066570 File Offset: 0x00064770
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<float>(this.Inputs[2], ref this._amount, canvas);
			base.GetInputValue<bool>(this.Inputs[3], ref this._showStarlogGraphic, canvas);
			base.GetInputValue<ConsumableRemedium>(this.Inputs[1], ref this._consumableRemedium, canvas);
			this._isOperationSuccess = false;
			if (this._consumableRemedium.RuntimeData.Amount > 0f)
			{
				this._isOperationSuccess = true;
				float num = this._consumableRemedium.RemoveAndGetRemovedAmount(this._amount);
				if (this._showStarlogGraphic)
				{
					TextIconJournalContent content = new TextIconJournalContent(this._consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.SUBTRACTION, 0);
					SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
				}
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0006664C File Offset: 0x0006484C
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output == 1)
			{
				return base.CastValue<T>(this._consumableRemedium.RuntimeData.Amount - this._consumableRemedium.RuntimeData.PlannedConsumption);
			}
			if (output == 2)
			{
				return base.CastValue<T>(this._isOperationSuccess);
			}
			throw new NotExistingOutputException(this.GetID, output);
		}

		// Token: 0x040010A5 RID: 4261
		public const string ID = "EE_RemoveConsumableNode";

		// Token: 0x040010A6 RID: 4262
		private const string NODE_NAME = "Remove Consumable";

		// Token: 0x040010A7 RID: 4263
		private const string INPUT_FLOW_NAME = "In";

		// Token: 0x040010A8 RID: 4264
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x040010A9 RID: 4265
		private const string INPUT_SHOW_GRAPHIC_NAME = "Show in Starlog";

		// Token: 0x040010AA RID: 4266
		private const string INPUT_CONSUMABLE_NAME = "Consumable object";

		// Token: 0x040010AB RID: 4267
		private const string OUTPUT_FLOW_NAME = "Out";

		// Token: 0x040010AC RID: 4268
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x040010AD RID: 4269
		private const string OUTPUT_OPERATION_STATUS = "Is Operation Finished";

		// Token: 0x040010AE RID: 4270
		private const int INPUT_FLOW_INDEX = 0;

		// Token: 0x040010AF RID: 4271
		private const int INPUT_CONSUMABLE_INDEX = 1;

		// Token: 0x040010B0 RID: 4272
		private const int INPUT_AMOUNT_INDEX = 2;

		// Token: 0x040010B1 RID: 4273
		private const int INPUT_SHOW_GRAPHIC_INDEX = 3;

		// Token: 0x040010B2 RID: 4274
		private const int OUTPUT_FLOW_INDEX = 0;

		// Token: 0x040010B3 RID: 4275
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x040010B4 RID: 4276
		private const int OUTPUT_OPERATION_INDEX = 2;

		// Token: 0x040010B5 RID: 4277
		[SerializeField]
		private float _amount;

		// Token: 0x040010B6 RID: 4278
		[SerializeField]
		private ConsumableRemedium _consumableRemedium;

		// Token: 0x040010B7 RID: 4279
		[SerializeField]
		private bool _showStarlogGraphic = true;

		// Token: 0x040010B8 RID: 4280
		private bool _isOperationSuccess;
	}
}
