using System;
using System.Collections.Generic;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x0200027F RID: 639
	[Node(false, "Remaster/Player Input/Get Character Result List Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SRGetCharacterResultListNode : PlayerDecisionNode
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0006738A File Offset: 0x0006558A
		public override string GetID
		{
			get
			{
				return "EE_SRGetCharacterResultListNode";
			}
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00067394 File Offset: 0x00065594
		public override Node Create(Vector2 pos)
		{
			SRGetCharacterResultListNode srgetCharacterResultListNode = ScriptableObject.CreateInstance<SRGetCharacterResultListNode>();
			srgetCharacterResultListNode.rect = new Rect(pos.x, pos.y, 200f, 160f);
			srgetCharacterResultListNode.name = "Get Character List Result";
			srgetCharacterResultListNode.CreateMutliInput("In", "Flow");
			srgetCharacterResultListNode.CreateInput("Result", "PlayerDecision");
			srgetCharacterResultListNode.CreateInput("CharacterList", "CharacterList");
			srgetCharacterResultListNode.CreateOutput("Character 1", "Flow");
			srgetCharacterResultListNode.CreateOutput("Character 2", "Flow");
			srgetCharacterResultListNode.CreateOutput("Character 3", "Flow");
			srgetCharacterResultListNode.CreateOutput("Character 4", "Flow");
			srgetCharacterResultListNode.CreateOutput("No Choice", "Flow");
			srgetCharacterResultListNode.CreateOutput("Character", "Character");
			return srgetCharacterResultListNode;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0006746B File Offset: 0x0006566B
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00067492 File Offset: 0x00065692
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00067494 File Offset: 0x00065694
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<PlayerDecision>(this.Inputs[1], ref this._currentDecision, canvas);
			if (!(this._currentDecision is PlayerCharacterDecision))
			{
				throw new WrongDecisionTypeException(typeof(PlayerCharacterDecision), this._currentDecision.GetType());
			}
			Character result = ((PlayerCharacterDecision)this._currentDecision).Result;
			List<Character> inputValue = base.GetInputValue<List<Character>>(this.Inputs[2], canvas);
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
			else if (inputValue.Count >= 1 && result == inputValue[0])
			{
				if (!this.Outputs[0].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 0);
				}
				this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (inputValue.Count >= 2 && result == inputValue[1])
			{
				if (!this.Outputs[1].isConnected)
				{
					throw new NotConnectedOutputException(this.GetID, 1);
				}
				this.Outputs[1].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
				return;
			}
			else if (inputValue.Count >= 3 && result == inputValue[2])
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
				if (inputValue.Count < 4 || !(result == inputValue[3]))
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

		// Token: 0x0600179B RID: 6043 RVA: 0x000676B8 File Offset: 0x000658B8
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

		// Token: 0x04001103 RID: 4355
		private const string ID = "EE_SRGetCharacterResultListNode";

		// Token: 0x04001104 RID: 4356
		private const string INPUT_RESULT_NAME = "Result";

		// Token: 0x04001105 RID: 4357
		private const string INPUT_OUTPUT_CHARACTER_1_NAME = "Character 1";

		// Token: 0x04001106 RID: 4358
		private const string INPUT_OUTPUT_CHARACTER_2_NAME = "Character 2";

		// Token: 0x04001107 RID: 4359
		private const string INPUT_OUTPUT_CHARACTER_3_NAME = "Character 3";

		// Token: 0x04001108 RID: 4360
		private const string INPUT_OUTPUT_CHARACTER_4_NAME = "Character 4";

		// Token: 0x04001109 RID: 4361
		private const string OUTPUT_NO_CHOICE_NAME = "No Choice";

		// Token: 0x0400110A RID: 4362
		private const string INPUT_CHARACTER_LIST_NAME = "CharacterList";

		// Token: 0x0400110B RID: 4363
		private const string OUTPUT_CHARACTER_NAME = "Character";

		// Token: 0x0400110C RID: 4364
		private const string NODE_NAME = "Get Character List Result";

		// Token: 0x0400110D RID: 4365
		private const int INPUT_RESULT_INDEX = 1;

		// Token: 0x0400110E RID: 4366
		private const int INPUT_CHARACTER_LIST_INDEX = 2;

		// Token: 0x0400110F RID: 4367
		private const int OUTPUT_CHARACTER_1_INDEX = 0;

		// Token: 0x04001110 RID: 4368
		private const int OUTPUT_CHARACTER_2_INDEX = 1;

		// Token: 0x04001111 RID: 4369
		private const int OUTPUT_CHARACTER_3_INDEX = 2;

		// Token: 0x04001112 RID: 4370
		private const int OUTPUT_CHARACTER_4_INDEX = 3;

		// Token: 0x04001113 RID: 4371
		private const int OUTPUT_NO_CHOICE_INDEX = 4;

		// Token: 0x04001114 RID: 4372
		private const int OUTPUT_CHARACTER_INDEX = 5;

		// Token: 0x04001115 RID: 4373
		[SerializeField]
		private PlayerDecision _currentDecision;
	}
}
