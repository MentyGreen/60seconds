using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000257 RID: 599
	[Node(false, "Utility Nodes/Get Random VisualId Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(EndGameCanvas)
	})]
	public class GetRandomVisualIdNode : MessageNode
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x0006160A File Offset: 0x0005F80A
		public override string GetID
		{
			get
			{
				return "EE_GetRandomVisualIdNode";
			}
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00061614 File Offset: 0x0005F814
		public override Node Create(Vector2 pos)
		{
			GetRandomVisualIdNode getRandomVisualIdNode = ScriptableObject.CreateInstance<GetRandomVisualIdNode>();
			getRandomVisualIdNode.rect = new Rect(pos.x, pos.y, 150f, 200f);
			getRandomVisualIdNode.name = "Get Random VisualId";
			getRandomVisualIdNode.CreateInput("VisualId 1", "VisualId");
			getRandomVisualIdNode.CreateInput("VisualId 2", "VisualId");
			getRandomVisualIdNode.CreateOutput("Result", "VisualId");
			return getRandomVisualIdNode;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00061685 File Offset: 0x0005F885
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x000616AC File Offset: 0x0005F8AC
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x000616B0 File Offset: 0x0005F8B0
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			int index = Random.Range(0, this.Inputs.Count - 1);
			VisualId obj = null;
			base.GetInputValue<VisualId>(this.Inputs[index], ref obj, canvas);
			return base.CastValue<T>(obj);
		}

		// Token: 0x04000F0E RID: 3854
		public const string ID = "EE_GetRandomVisualIdNode";

		// Token: 0x04000F0F RID: 3855
		[SerializeField]
		private int _inputsCounter;

		// Token: 0x04000F10 RID: 3856
		private const string INPUT_VISUAL_ID_NAME = "VisualId ";

		// Token: 0x04000F11 RID: 3857
		private const string OUTPUT_RESULT_NAME = "Result";
	}
}
