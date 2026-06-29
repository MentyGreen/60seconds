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
	// Token: 0x0200027A RID: 634
	[Node(true, "Legacy/Remove Food Node V2", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(Goal)
	})]
	public class RemoveFoodVerTwoNode : ResourceNode
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0006687D File Offset: 0x00064A7D
		public override string GetID
		{
			get
			{
				return "EE_RemoveFoodVerTwoNode";
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00066884 File Offset: 0x00064A84
		public override Node Create(Vector2 pos)
		{
			RemoveFoodVerTwoNode removeFoodVerTwoNode = ScriptableObject.CreateInstance<RemoveFoodVerTwoNode>();
			removeFoodVerTwoNode.rect = new Rect(pos.x, pos.y, 180f, 80f);
			removeFoodVerTwoNode.name = "Remove Food Node";
			removeFoodVerTwoNode.CreateMutliInput("In", "Flow");
			removeFoodVerTwoNode.CreateInput("Amount", "Float");
			removeFoodVerTwoNode.CreateOutput("Out", "Flow");
			removeFoodVerTwoNode.CreateOutput("Current Amount", "Float");
			removeFoodVerTwoNode.CreateOutput("Is Operation Finished", "Bool");
			return removeFoodVerTwoNode;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00066917 File Offset: 0x00064B17
		public override Node Duplicate(Vector2 pos)
		{
			RemoveFoodVerTwoNode removeFoodVerTwoNode = (RemoveFoodVerTwoNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			removeFoodVerTwoNode._amount = this._amount;
			return removeFoodVerTwoNode;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0006694F File Offset: 0x00064B4F
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00066951 File Offset: 0x00064B51
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00066953 File Offset: 0x00064B53
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00066958 File Offset: 0x00064B58
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<int>(this.Inputs[1], ref this._amount, canvas);
			ConsumableRemedium consumableRemedium = (ConsumableRemedium)Singleton<ItemManager>.Instance.GetItem("item_food");
			this._isOperationSuccess = false;
			if (consumableRemedium.RuntimeData.Amount > 0f)
			{
				this._isOperationSuccess = true;
				float num = consumableRemedium.RemoveAndGetRemovedAmount((float)this._amount);
				TextIconJournalContent content = new TextIconJournalContent(consumableRemedium.BaseStaticData.IconTerm, (int)num, EventContentData.ETextIconContentType.SUBTRACTION, 0);
				SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			}
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00066A00 File Offset: 0x00064C00
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

		// Token: 0x040010C5 RID: 4293
		public const string ID = "EE_RemoveFoodVerTwoNode";

		// Token: 0x040010C6 RID: 4294
		private const string NODE_NAME = "Remove Food Node";

		// Token: 0x040010C7 RID: 4295
		private const string INPUT_IN_NAME = "In";

		// Token: 0x040010C8 RID: 4296
		private const string INPUT_AMOUNT_NAME = "Amount";

		// Token: 0x040010C9 RID: 4297
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x040010CA RID: 4298
		private const string OUTPUT_CURRENT_AMOUNT_NAME = "Current Amount";

		// Token: 0x040010CB RID: 4299
		private const string OUTPUT_OPERATION_STATUS = "Is Operation Finished";

		// Token: 0x040010CC RID: 4300
		private const string FOOD_ID = "item_food";

		// Token: 0x040010CD RID: 4301
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x040010CE RID: 4302
		private const int INPUT_AMOUNT_INDEX = 1;

		// Token: 0x040010CF RID: 4303
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x040010D0 RID: 4304
		private const int OUTPUT_CURRENT_AMOUNT_INDEX = 1;

		// Token: 0x040010D1 RID: 4305
		private const int OUTPUT_OPERATION_INDEX = 2;

		// Token: 0x040010D2 RID: 4306
		[SerializeField]
		private int _amount;

		// Token: 0x040010D3 RID: 4307
		private bool _isOperationSuccess;
	}
}
