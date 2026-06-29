using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Remaster.Survival;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000223 RID: 547
	[Node(false, "Utility Nodes/Allow Skin Changes Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(Goal)
	})]
	public class AllowSkinChangesNode : EventNode
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0005E342 File Offset: 0x0005C542
		public override string GetID
		{
			get
			{
				return "EE_allowSkinChanges";
			}
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0005E34C File Offset: 0x0005C54C
		public override Node Create(Vector2 pos)
		{
			AllowSkinChangesNode allowSkinChangesNode = ScriptableObject.CreateInstance<AllowSkinChangesNode>();
			allowSkinChangesNode.rect = new Rect(pos.x, pos.y, 250f, 40f);
			allowSkinChangesNode.name = "Allow Skin Changes";
			allowSkinChangesNode.CreateMutliInput("In", "Flow");
			allowSkinChangesNode.CreateInput("Skin Data List", "SkinDataList");
			allowSkinChangesNode.CreateOutput("Out", "Flow");
			return allowSkinChangesNode;
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x0005E3BD File Offset: 0x0005C5BD
		public override Node Duplicate(Vector2 pos)
		{
			AllowSkinChangesNode allowSkinChangesNode = (AllowSkinChangesNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			allowSkinChangesNode._dataList = this._dataList;
			return allowSkinChangesNode;
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0005E3F5 File Offset: 0x0005C5F5
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0005E3F7 File Offset: 0x0005C5F7
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0005E3F9 File Offset: 0x0005C5F9
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0005E3FC File Offset: 0x0005C5FC
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<SkinDataList>(this.Inputs[1], ref this._dataList, canvas);
			SkinManager.Instance.AllowChangingSkins(this._dataList);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000E1F RID: 3615
		public const string ID = "EE_allowSkinChanges";

		// Token: 0x04000E20 RID: 3616
		private const int FLOW_INPUT_INDEX = 0;

		// Token: 0x04000E21 RID: 3617
		private const int FLOW_OUTPUT_INDEX = 0;

		// Token: 0x04000E22 RID: 3618
		private const int DATALIST_INPUT_INDEX = 1;

		// Token: 0x04000E23 RID: 3619
		private const string NODE_NAME = "Allow Skin Changes";

		// Token: 0x04000E24 RID: 3620
		private const string FLOW_OUTPUT_NAME = "Out";

		// Token: 0x04000E25 RID: 3621
		private const string FLOW_INPUT_NAME = "In";

		// Token: 0x04000E26 RID: 3622
		private const string DATALIST_INPUT_NAME = "Skin Data List";

		// Token: 0x04000E27 RID: 3623
		[SerializeField]
		private SkinDataList _dataList;
	}
}
