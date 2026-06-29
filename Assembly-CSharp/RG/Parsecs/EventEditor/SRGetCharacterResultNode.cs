using System;
using NodeEditorFramework;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.Parsecs.EventEditor
{
	// Token: 0x02000214 RID: 532
	[Node(false, "Remaster/Player Input/Get Character Result Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SRGetCharacterResultNode : PlayerDecisionNode
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x0005C6FF File Offset: 0x0005A8FF
		public override string GetID
		{
			get
			{
				return "EE_SRGetCharacterResultNode";
			}
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005C708 File Offset: 0x0005A908
		public override Node Create(Vector2 pos)
		{
			SRGetCharacterResultNode srgetCharacterResultNode = ScriptableObject.CreateInstance<SRGetCharacterResultNode>();
			srgetCharacterResultNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			srgetCharacterResultNode.name = "Get Character Result";
			srgetCharacterResultNode.CreateMutliInput("In", "Flow");
			srgetCharacterResultNode.CreateInput("Result", "PlayerDecision");
			srgetCharacterResultNode.CreateInput("Character 1", "Character");
			srgetCharacterResultNode.CreateInput("Character 2", "Character");
			srgetCharacterResultNode.CreateInput("Character 3", "Character");
			srgetCharacterResultNode.CreateInput("Character 4", "Character");
			srgetCharacterResultNode.CreateOutput("Character 1", "Flow");
			srgetCharacterResultNode.CreateOutput("Character 2", "Flow");
			srgetCharacterResultNode.CreateOutput("Character 3", "Flow");
			srgetCharacterResultNode.CreateOutput("Character 4", "Flow");
			srgetCharacterResultNode.CreateOutput("No Choice", "Flow");
			srgetCharacterResultNode.CreateOutput("Character", "Character");
			return srgetCharacterResultNode;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0005C812 File Offset: 0x0005AA12
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0005C839 File Offset: 0x0005AA39
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0005C83B File Offset: 0x0005AA3B
		protected override void NodeEnable()
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0005C83D File Offset: 0x0005AA3D
		protected override void NodeGUI()
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0005C840 File Offset: 0x0005AA40
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<PlayerDecision>(this.Inputs[1], ref this._currentDecision, canvas);
			if (!(this._currentDecision is PlayerCharacterDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerCharacterDecision), this._currentDecision.GetType());
			}
			Character result = ((PlayerCharacterDecision)this._currentDecision).Result;
			base.GetInputValue<Character>(this.Inputs[2], ref this._character1, canvas);
			base.GetInputValue<Character>(this.Inputs[3], ref this._character2, canvas);
			base.GetInputValue<Character>(this.Inputs[4], ref this._character3, canvas);
			base.GetInputValue<Character>(this.Inputs[5], ref this._character4, canvas);
			if (base.ParentEvent is SurvivalEvent)
			{
				((SurvivalEvent)base.ParentEvent).WasEventSuccessful = (result != null);
			}
			if (result == null)
			{
				if (!this.Outputs[4].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 4);
				}
				this.Outputs[4].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._character1)
			{
				if (!this.Outputs[0].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 0);
				}
				this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._character2)
			{
				if (!this.Outputs[1].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 1);
				}
				this.Outputs[1].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (result == this._character3)
			{
				if (!this.Outputs[2].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 2);
				}
				this.Outputs[2].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else
			{
				if (!(result == this._character4))
				{
					throw new WrongDecisionConnectionExcpetion(this.GetID);
				}
				if (!this.Outputs[3].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 3);
				}
				this.Outputs[3].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0005CA8C File Offset: 0x0005AC8C
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 5)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (!(this._currentDecision is PlayerCharacterDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerCharacterDecision), this._currentDecision.GetType());
			}
			Character result = ((PlayerCharacterDecision)this._currentDecision).Result;
			return base.CastValue<T>(result);
		}

		// Token: 0x04000DBE RID: 3518
		private const string ID = "EE_SRGetCharacterResultNode";

		// Token: 0x04000DBF RID: 3519
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04000DC0 RID: 3520
		private const string INPUT_OUTPUT_CHARACTER_1_NAME = "Character 1";

		// Token: 0x04000DC1 RID: 3521
		private const string INPUT_OUTPUT_CHARACTER_2_NAME = "Character 2";

		// Token: 0x04000DC2 RID: 3522
		private const string INPUT_OUTPUT_CHARACTER_3_NAME = "Character 3";

		// Token: 0x04000DC3 RID: 3523
		private const string INPUT_OUTPUT_CHARACTER_4_NAME = "Character 4";

		// Token: 0x04000DC4 RID: 3524
		private const string OUTPUT_CHARACTER_NAME = "Character";

		// Token: 0x04000DC5 RID: 3525
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x04000DC6 RID: 3526
		private const string NODE_NAME = "Get Character Result";

		// Token: 0x04000DC7 RID: 3527
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x04000DC8 RID: 3528
		private const int INPUT_CHARACTER_1_INDEX = 2;

		// Token: 0x04000DC9 RID: 3529
		private const int INPUT_CHARACTER_2_INDEX = 3;

		// Token: 0x04000DCA RID: 3530
		private const int INPUT_CHARACTER_3_INDEX = 4;

		// Token: 0x04000DCB RID: 3531
		private const int INPUT_CHARACTER_4_INDEX = 5;

		// Token: 0x04000DCC RID: 3532
		private const int OUTPUT_CHARACTER_1_INDEX = 0;

		// Token: 0x04000DCD RID: 3533
		private const int OUTPUT_CHARACTER_2_INDEX = 1;

		// Token: 0x04000DCE RID: 3534
		private const int OUTPUT_CHARACTER_3_INDEX = 2;

		// Token: 0x04000DCF RID: 3535
		private const int OUTPUT_CHARACTER_4_INDEX = 3;

		// Token: 0x04000DD0 RID: 3536
		private const int OUTPUT_NO_CHOICE_INDEX = 4;

		// Token: 0x04000DD1 RID: 3537
		private const int OUTPUT_CHARACTER_INDEX = 5;

		// Token: 0x04000DD2 RID: 3538
		[SerializeField]
		private PlayerDecision _currentDecision;

		// Token: 0x04000DD3 RID: 3539
		[SerializeField]
		private Character _character1;

		// Token: 0x04000DD4 RID: 3540
		[SerializeField]
		private Character _character2;

		// Token: 0x04000DD5 RID: 3541
		[SerializeField]
		private Character _character3;

		// Token: 0x04000DD6 RID: 3542
		[SerializeField]
		private Character _character4;
	}
}
