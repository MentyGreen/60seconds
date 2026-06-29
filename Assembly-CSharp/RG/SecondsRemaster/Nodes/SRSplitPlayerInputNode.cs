using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000281 RID: 641
	[Node(false, "Remaster/Player Input/Split Player Input Node", new Type[]
	{
		typeof(SurvivalEvent)
	})]
	public class SRSplitPlayerInputNode : PlayerDecisionNode
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x00067C92 File Offset: 0x00065E92
		public override string GetID
		{
			get
			{
				return "EE_SRSplitPlayerInputNode";
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00067C9C File Offset: 0x00065E9C
		public override Node Create(Vector2 pos)
		{
			SRSplitPlayerInputNode srsplitPlayerInputNode = ScriptableObject.CreateInstance<SRSplitPlayerInputNode>();
			srsplitPlayerInputNode.rect = new Rect(pos.x, pos.y, 220f, 160f);
			srsplitPlayerInputNode.name = "Split Player Input";
			srsplitPlayerInputNode.CreateMutliInput("In", "Flow");
			srsplitPlayerInputNode.CreateInput("Decision", "PlayerDecision");
			srsplitPlayerInputNode.CreateOutput("Result", "PlayerDecision");
			srsplitPlayerInputNode.CreateOutput("Output", "Flow");
			return srsplitPlayerInputNode;
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00067D1E File Offset: 0x00065F1E
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00067D45 File Offset: 0x00065F45
		private void UpdateKnobsCount()
		{
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00067D47 File Offset: 0x00065F47
		protected override void NodeGUI()
		{
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00067D49 File Offset: 0x00065F49
		public override Rect GetNodeRect()
		{
			return new Rect(this.rect.x, this.rect.y, 300f, (float)(35 + 20 * this.Inputs.Count));
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x00067D80 File Offset: 0x00065F80
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

		// Token: 0x060017AE RID: 6062 RVA: 0x00067E63 File Offset: 0x00066063
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(this._currentDecision);
		}

		// Token: 0x04001138 RID: 4408
		private const string ID = "EE_SRSplitPlayerInputNode";

		// Token: 0x04001139 RID: 4409
		private const string INPUT_DECISION_NAME = "Decision";

		// Token: 0x0400113A RID: 4410
		private const string OUTPUT_DECISION_NAME = "Output";

		// Token: 0x0400113B RID: 4411
		private const string NODE_NAME = "Split Player Input";

		// Token: 0x0400113C RID: 4412
		private const string OUTPUT_RESULT_NAME = "Result";

		// Token: 0x0400113D RID: 4413
		[SerializeField]
		private PlayerDecision _currentDecision;
	}
}
