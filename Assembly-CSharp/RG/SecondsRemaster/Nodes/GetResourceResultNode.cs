using System;
using NodeEditorFramework;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000268 RID: 616
	[Node(false, "Player Input/Get Resource Result Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class GetResourceResultNode : PlayerDecisionNode
	{
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060016D9 RID: 5849 RVA: 0x00064703 File Offset: 0x00062903
		public override string GetID
		{
			get
			{
				return "EE_GetResourceResultNode";
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0006470C File Offset: 0x0006290C
		public override Node Create(Vector2 pos)
		{
			GetResourceResultNode getResourceResultNode = ScriptableObject.CreateInstance<GetResourceResultNode>();
			getResourceResultNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			getResourceResultNode.name = "Get Resource Result";
			getResourceResultNode.CreateMutliInput("In", "Flow");
			getResourceResultNode.CreateInput("Result", "PlayerDecision");
			getResourceResultNode.CreateOutput("Resource 1", "Flow");
			getResourceResultNode.CreateOutput("Resource 2", "Flow");
			getResourceResultNode.CreateOutput("Resource 3", "Flow");
			getResourceResultNode.CreateOutput("No Choice", "Flow");
			getResourceResultNode.CreateOutput("Resource", "Resources");
			return getResourceResultNode;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x000647C1 File Offset: 0x000629C1
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000647E8 File Offset: 0x000629E8
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x000647EA File Offset: 0x000629EA
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x000647EC File Offset: 0x000629EC
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<PlayerDecision>(this.Inputs[1], ref this._currentDecision, canvas);
			if (!(this._currentDecision is PlayerResourceDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerResourceDecision), this._currentDecision.GetType());
			}
			PlayerResourceDecision playerResourceDecision = (PlayerResourceDecision)this._currentDecision;
			GameResource result = playerResourceDecision.Result;
			if (base.ParentEvent is SurvivalEvent)
			{
				((SurvivalEvent)base.ParentEvent).WasEventSuccessful = (playerResourceDecision.ChoosenNumber != 0);
			}
			if (playerResourceDecision.ChoosenNumber == 0)
			{
				if (!this.Outputs[3].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 3);
				}
				this.Outputs[3].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (playerResourceDecision.ChoosenNumber == 3)
			{
				if (playerResourceDecision.UseResource)
				{
					this.UseResource(result);
				}
				if (!this.Outputs[0].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 0);
				}
				this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (playerResourceDecision.ChoosenNumber == 2)
			{
				if (playerResourceDecision.UseResource)
				{
					this.UseResource(result);
				}
				if (!this.Outputs[1].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 1);
				}
				this.Outputs[1].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else
			{
				if (playerResourceDecision.ChoosenNumber != 1)
				{
					throw new WrongDecisionConnectionExcpetion(this.GetID);
				}
				if (playerResourceDecision.UseResource)
				{
					this.UseResource(result);
				}
				if (!this.Outputs[2].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 2);
				}
				this.Outputs[2].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x000649B0 File Offset: 0x00062BB0
		private void UseResource(GameResource gameResource)
		{
			PlayerResources playerResources = Singleton<ItemManager>.Instance.GetPlayerResources();
			playerResources.Unlock(gameResource);
			int num = playerResources.RemoveResourceAndGetRemovedAmount(gameResource.Resource, gameResource.Amount);
			ParsecsEventManager.DisplayTextIconContent(base.ParentCanvas, EventContentData.ETextIconContentType.SUBTRACTION, (float)num, gameResource.Resource, false);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000649F8 File Offset: 0x00062BF8
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 4)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (!(this._currentDecision is PlayerResourceDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerResourceDecision), this._currentDecision.GetType());
			}
			GameResource result = ((PlayerResourceDecision)this._currentDecision).Result;
			return base.CastValue<T>(result);
		}

		// Token: 0x04000FF3 RID: 4083
		private const string ID = "EE_GetResourceResultNode";

		// Token: 0x04000FF4 RID: 4084
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04000FF5 RID: 4085
		private const string INPUT_OUTPUT_RES_1_NAME = "Resource 1";

		// Token: 0x04000FF6 RID: 4086
		private const string INPUT_OUTPUT_RES_2_NAME = "Resource 2";

		// Token: 0x04000FF7 RID: 4087
		private const string INPUT_OUTPUT_RES_3_NAME = "Resource 3";

		// Token: 0x04000FF8 RID: 4088
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x04000FF9 RID: 4089
		private const string OUTPUT_RES_NAME = "Resource";

		// Token: 0x04000FFA RID: 4090
		private const string NODE_NAME = "Get Resource Result";

		// Token: 0x04000FFB RID: 4091
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x04000FFC RID: 4092
		private const int OUTPUT_RES_1_INDEX = 0;

		// Token: 0x04000FFD RID: 4093
		private const int OUTPUT_RES_2_INDEX = 1;

		// Token: 0x04000FFE RID: 4094
		private const int OUTPUT_RES_3_INDEX = 2;

		// Token: 0x04000FFF RID: 4095
		private const int OUTPUT_NO_CHOICE_INDEX = 3;

		// Token: 0x04001000 RID: 4096
		private const int OUTPUT_RES_INDEX = 4;

		// Token: 0x04001001 RID: 4097
		private const int RES_1_CARD_ID = 3;

		// Token: 0x04001002 RID: 4098
		private const int RES_2_CARD_ID = 2;

		// Token: 0x04001003 RID: 4099
		private const int RES_3_CARD_ID = 1;

		// Token: 0x04001004 RID: 4100
		private const int NO_CHOICE_CARD_ID = 0;

		// Token: 0x04001005 RID: 4101
		[SerializeField]
		private PlayerDecision _currentDecision;
	}
}
