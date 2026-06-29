using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000265 RID: 613
	[Node(false, "Player Input/Display Yes-No Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class DisplayYesNoNode : PlayerDecisionNode
	{
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060016BF RID: 5823 RVA: 0x00063B8E File Offset: 0x00061D8E
		public override string GetID
		{
			get
			{
				return "EE_DisplayYesNoNode";
			}
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x00063B98 File Offset: 0x00061D98
		public override Node Create(Vector2 pos)
		{
			DisplayYesNoNode displayYesNoNode = ScriptableObject.CreateInstance<DisplayYesNoNode>();
			displayYesNoNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			displayYesNoNode.name = "Display Yes/No";
			displayYesNoNode.CreateMutliInput("In", "Flow");
			displayYesNoNode.CreateOutput("Result", "PlayerDecision", NodeSide.Bottom);
			return displayYesNoNode;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00063BF9 File Offset: 0x00061DF9
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00063C20 File Offset: 0x00061E20
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00063C24 File Offset: 0x00061E24
		public override void Execute(NodeCanvas canvas)
		{
			this._result.WasChosen = true;
			YesNoChoiceJournalContent content = new YesNoChoiceJournalContent();
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00063C50 File Offset: 0x00061E50
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (this._result == null)
			{
				Debug.LogError("Result in GetValue in DisplayYesNoNode is null");
			}
			if (this._result.WasChosen)
			{
				ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
				if (playerChoice == null)
				{
					Debug.LogError("playerChoiceCard in GetValue in DisplayYesNoNode is null");
				}
				EPlayerChoice choiceType = playerChoice.GetChoiceType();
				if (choiceType != EPlayerChoice.YES)
				{
					if (choiceType != EPlayerChoice.NO)
					{
						throw new ArgumentOutOfRangeException();
					}
					this._result.ChoosenNumber = 0;
					this._result.Result = false;
				}
				else
				{
					this._result.ChoosenNumber = 1;
					this._result.Result = true;
				}
			}
			return base.CastValue<T>(this._result);
		}

		// Token: 0x04000FBD RID: 4029
		private const string ID = "EE_DisplayYesNoNode";

		// Token: 0x04000FBE RID: 4030
		private const int OUTPUT_RESULT_INDEX = 0;

		// Token: 0x04000FBF RID: 4031
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04000FC0 RID: 4032
		private const string NODE_NAME = "Display Yes/No";

		// Token: 0x04000FC1 RID: 4033
		[SerializeField]
		private PlayerYesNoDecision _result = new PlayerYesNoDecision();
	}
}
