using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000260 RID: 608
	[Node(false, "Player Input/Display Choose Resource Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayChooseResourceNode : PlayerDecisionNode
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00062C9D File Offset: 0x00060E9D
		public override string GetID
		{
			get
			{
				return "EE_DisplayChooseResourceNode ";
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00062CA4 File Offset: 0x00060EA4
		public override Node Create(Vector2 pos)
		{
			DisplayChooseResourceNode displayChooseResourceNode = ScriptableObject.CreateInstance<DisplayChooseResourceNode>();
			displayChooseResourceNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayChooseResourceNode.name = "Display Choose Resource";
			displayChooseResourceNode.CreateMutliInput("In", "Flow");
			displayChooseResourceNode.CreateInput("Resource 1", "Resources");
			displayChooseResourceNode.CreateInput("Resource 1 Amount", "Int");
			displayChooseResourceNode.CreateInput("Resource 2", "Resources");
			displayChooseResourceNode.CreateInput("Resource 2 Amount", "Int");
			displayChooseResourceNode.CreateInput("Resource 3", "Resources");
			displayChooseResourceNode.CreateInput("Resource 3 Amount", "Int");
			displayChooseResourceNode.CreateInput("Use Resource", "Bool");
			displayChooseResourceNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayChooseResourceNode;
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00062D7C File Offset: 0x00060F7C
		public override Node Duplicate(Vector2 pos)
		{
			DisplayChooseResourceNode displayChooseResourceNode = (DisplayChooseResourceNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayChooseResourceNode._resource1 = new GameResource(this._resource1.Resource, this._resource1.Amount);
			displayChooseResourceNode._resource2 = new GameResource(this._resource2.Resource, this._resource2.Amount);
			displayChooseResourceNode._resource3 = new GameResource(this._resource3.Resource, this._resource3.Amount);
			displayChooseResourceNode._useResource = this._useResource;
			return displayChooseResourceNode;
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00062E22 File Offset: 0x00061022
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00062E24 File Offset: 0x00061024
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00062E28 File Offset: 0x00061028
		protected override void OnNodeValidate()
		{
			if (this._useResource)
			{
				if (this._resource1.Resource != null && this._resource1.Amount < 0 && !this.Inputs[1].isConnected)
				{
					base.LogMessage(string.Format("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", 1, this._resource1.Amount), Node.EMessageType.ERROR);
				}
				if (this._resource2.Resource != null && this._resource2.Amount < 0 && !this.Inputs[3].isConnected)
				{
					base.LogMessage(string.Format("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", 2, this._resource2.Amount), Node.EMessageType.ERROR);
				}
				if (this._resource3.Resource != null && this._resource3.Amount < 0 && !this.Inputs[5].isConnected)
				{
					base.LogMessage(string.Format("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", 3, this._resource3.Amount), Node.EMessageType.ERROR);
				}
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00062F54 File Offset: 0x00061154
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<Resource>(this.Inputs[1], ref this._resource1.Resource, canvas);
			base.GetInputValue<Resource>(this.Inputs[3], ref this._resource2.Resource, canvas);
			base.GetInputValue<Resource>(this.Inputs[5], ref this._resource3.Resource, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._resource1.Amount, canvas);
			base.GetInputValue<int>(this.Inputs[4], ref this._resource2.Amount, canvas);
			base.GetInputValue<int>(this.Inputs[6], ref this._resource3.Amount, canvas);
			base.GetInputValue<bool>(this.Inputs[7], ref this._useResource, canvas);
			if (this._useResource)
			{
				if (this._resource1.Resource != null && this._resource1.Amount < 0)
				{
					Debug.LogErrorFormat("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", new object[]
					{
						1,
						this._resource1.Amount
					});
				}
				if (this._resource2.Resource != null && this._resource2.Amount < 0)
				{
					Debug.LogErrorFormat("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", new object[]
					{
						2,
						this._resource2.Amount
					});
				}
				if (this._resource3.Resource != null && this._resource3.Amount < 0)
				{
					Debug.LogErrorFormat("Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}", new object[]
					{
						3,
						this._resource3.Amount
					});
				}
			}
			this._result.UseResource = this._useResource;
			this._result.WasChosen = true;
			ParsecsEventManager.DisplayChoiceContent(this._useResource, this._resource1, this._resource2, this._resource3);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0006315C File Offset: 0x0006135C
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (this._result.WasChosen)
			{
				ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
				this._result.ChoosenNumber = playerChoice.GetCardId();
				if (playerChoice.GetCardId() == 0)
				{
					this._result.Result = new GameResource(null, 0);
				}
				else if (playerChoice.GetCardId() == 3)
				{
					this._result.Result = new GameResource(this._resource1.Resource, this._resource1.Amount);
				}
				else if (playerChoice.GetCardId() == 2)
				{
					this._result.Result = new GameResource(this._resource2.Resource, this._resource2.Amount);
				}
				else if (playerChoice.GetCardId() == 1)
				{
					this._result.Result = new GameResource(this._resource3.Resource, this._resource3.Amount);
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000F78 RID: 3960
		private const string ID = "EE_DisplayChooseResourceNode ";

		// Token: 0x04000F79 RID: 3961
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000F7A RID: 3962
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000F7B RID: 3963
		private const string NODE_NAME = "Display Choose Resource";

		// Token: 0x04000F7C RID: 3964
		private const string INPUT_RES_1_NAME = "Resource 1";

		// Token: 0x04000F7D RID: 3965
		private const string INPUT_RES_2_NAME = "Resource 2";

		// Token: 0x04000F7E RID: 3966
		private const string INPUT_RES_3_NAME = "Resource 3";

		// Token: 0x04000F7F RID: 3967
		private const string INPUT_RES_1_AMOUNT_NAME = "Resource 1 Amount";

		// Token: 0x04000F80 RID: 3968
		private const string INPUT_RES_2_AMOUNT_NAME = "Resource 2 Amount";

		// Token: 0x04000F81 RID: 3969
		private const string INPUT_RES_3_AMOUNT_NAME = "Resource 3 Amount";

		// Token: 0x04000F82 RID: 3970
		private const string INPUT_USE_RES_NAME = "Use Resource";

		// Token: 0x04000F83 RID: 3971
		private const int INPUT_RES_1_INDEX = 1;

		// Token: 0x04000F84 RID: 3972
		private const int INPUT_RES_1_AMOUNT_INDEX = 2;

		// Token: 0x04000F85 RID: 3973
		private const int INPUT_RES_2_INDEX = 3;

		// Token: 0x04000F86 RID: 3974
		private const int INPUT_RES_2_AMOUNT_INDEX = 4;

		// Token: 0x04000F87 RID: 3975
		private const int INPUT_RES_3_INDEX = 5;

		// Token: 0x04000F88 RID: 3976
		private const int INPUT_RES_3_AMOUNT_INDEX = 6;

		// Token: 0x04000F89 RID: 3977
		private const int INPUT_USE_RES_INDEX = 7;

		// Token: 0x04000F8A RID: 3978
		private const int RES_1_CARD_ID = 3;

		// Token: 0x04000F8B RID: 3979
		private const int RES_2_CARD_ID = 2;

		// Token: 0x04000F8C RID: 3980
		private const int RES_3_CARD_ID = 1;

		// Token: 0x04000F8D RID: 3981
		private const int NO_CHOICE_CARD_ID = 0;

		// Token: 0x04000F8E RID: 3982
		private const string RESOURCE_IS_NEGATIVE_ERROR = "Resource of id: {0} in DisplayChooseResourceNode can't be negative. Current value {1}";

		// Token: 0x04000F8F RID: 3983
		[SerializeField]
		private GameResource _resource1;

		// Token: 0x04000F90 RID: 3984
		[SerializeField]
		private GameResource _resource2;

		// Token: 0x04000F91 RID: 3985
		[SerializeField]
		private GameResource _resource3;

		// Token: 0x04000F92 RID: 3986
		[SerializeField]
		private PlayerResourceDecision _result = new PlayerResourceDecision();

		// Token: 0x04000F93 RID: 3987
		[SerializeField]
		private bool _useResource;
	}
}
