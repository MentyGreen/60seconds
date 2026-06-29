using System;
using NodeEditorFramework;
using RG.Parsecs.EndGameEditor;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000259 RID: 601
	[Node(false, "Utility Nodes/Get VisualId Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(EndGameCanvas)
	})]
	public class GetVisualIdNode : MessageNode
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x00061928 File Offset: 0x0005FB28
		public override string GetID
		{
			get
			{
				return "EE_GetVisualIdNode";
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00061930 File Offset: 0x0005FB30
		public override Node Create(Vector2 pos)
		{
			GetVisualIdNode getVisualIdNode = ScriptableObject.CreateInstance<GetVisualIdNode>();
			getVisualIdNode.rect = new Rect(pos.x, pos.y, 300f, 70f);
			getVisualIdNode.name = "Get VisualId";
			getVisualIdNode.CreateOutput("Out", "VisualId");
			return getVisualIdNode;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0006197F File Offset: 0x0005FB7F
		public override Node Duplicate(Vector2 pos)
		{
			GetVisualIdNode getVisualIdNode = (GetVisualIdNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			getVisualIdNode._visualId = this._visualId;
			return getVisualIdNode;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000619B7 File Offset: 0x0005FBB7
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000619B9 File Offset: 0x0005FBB9
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000619BB File Offset: 0x0005FBBB
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000619BD File Offset: 0x0005FBBD
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output != 0)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			return base.CastValue<T>(this._visualId);
		}

		// Token: 0x04000F1F RID: 3871
		public const string ID = "EE_GetVisualIdNode";

		// Token: 0x04000F20 RID: 3872
		[SerializeField]
		private VisualId _visualId;
	}
}
