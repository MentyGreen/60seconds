using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000274 RID: 628
	[Node(false, "Player Input/Split Player Input Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SplitPlayerInputNode : PlayerDecisionNode
	{
		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00065BFC File Offset: 0x00063DFC
		public override string GetID
		{
			get
			{
				return "EE_SplitPlayerInputNode";
			}
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x00065C04 File Offset: 0x00063E04
		public override Node Create(Vector2 pos)
		{
			SplitPlayerInputNode splitPlayerInputNode = ScriptableObject.CreateInstance<SplitPlayerInputNode>();
			splitPlayerInputNode.rect = new Rect(pos.x, pos.y, 220f, 160f);
			splitPlayerInputNode.name = "Split Player Input";
			splitPlayerInputNode.CreateMutliInput("In", "Flow");
			splitPlayerInputNode.CreateInput("Decision", "PlayerDecision");
			splitPlayerInputNode.CreateOutput("Result", "PlayerDecision");
			splitPlayerInputNode.CreateOutput("Output", "Flow");
			return splitPlayerInputNode;
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x00065C86 File Offset: 0x00063E86
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00065CAD File Offset: 0x00063EAD
		private void UpdateKnobsCount()
		{
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00065CAF File Offset: 0x00063EAF
		protected virtual void DisplayNodeInputs(NodeInput input)
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00065CB1 File Offset: 0x00063EB1
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00065CB3 File Offset: 0x00063EB3
		public override Rect GetNodeRect()
		{
			return new Rect(this.rect.x, this.rect.y, 300f, (float)(35 + 20 * this.Inputs.Count));
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x00065CE8 File Offset: 0x00063EE8
		public override void Execute(NodeCanvas canvas)
		{
			int i = 1;
			while (i < this.Inputs.Count)
			{
				base.GetInputValue<PlayerDecision>(this.Inputs[i], ref this._currentDecision, canvas);
				if (this._currentDecision != null && this._currentDecision.WasChosen)
				{
					this._currentDecision.WasChosen = false;
					if (!this.Outputs[i].isConnected)
					{
						throw new NotConnectedOutputException(this.GetID, i);
					}
					this.Outputs[i].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
					return;
				}
				else if (i == this.Inputs.Count - 1)
				{
					if (!this.Outputs[i].isConnected)
					{
						throw new NotConnectedOutputException(this.GetID, i);
					}
					this.Outputs[i].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00065DCB File Offset: 0x00063FCB
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(this._currentDecision);
		}

		// Token: 0x04001073 RID: 4211
		private const string ID = "EE_SplitPlayerInputNode";

		// Token: 0x04001074 RID: 4212
		private const string INPUT_DECISION_NAME = "Decision";

		// Token: 0x04001075 RID: 4213
		private const string OUTPUT_DECISION_NAME = "Output";

		// Token: 0x04001076 RID: 4214
		private const string NODE_NAME = "Split Player Input";

		// Token: 0x04001077 RID: 4215
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x04001078 RID: 4216
		[SerializeField]
		private PlayerDecision _currentDecision;
	}
}
