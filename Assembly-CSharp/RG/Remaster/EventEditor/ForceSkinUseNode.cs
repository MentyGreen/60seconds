using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Remaster.Survival;
using UnityEngine;

namespace RG.Remaster.EventEditor
{
	// Token: 0x02000224 RID: 548
	[Node(false, "Utility Nodes/Force Skin Use Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(Goal)
	})]
	public class ForceSkinUseNode : EventNode
	{
		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0005E457 File Offset: 0x0005C657
		public override string GetID
		{
			get
			{
				return "EE_forceSkinUse";
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0005E460 File Offset: 0x0005C660
		public override Node Create(Vector2 pos)
		{
			ForceSkinUseNode forceSkinUseNode = ScriptableObject.CreateInstance<ForceSkinUseNode>();
			forceSkinUseNode.rect = new Rect(pos.x, pos.y, 250f, 40f);
			forceSkinUseNode.name = "Force Skin Use";
			forceSkinUseNode.CreateMutliInput("In", "Flow");
			forceSkinUseNode.CreateInput("Skin Data List", "SkinDataList");
			forceSkinUseNode.CreateInput("Skin Id", "SkinId");
			forceSkinUseNode.CreateOutput("Out", "Flow");
			return forceSkinUseNode;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0005E4E4 File Offset: 0x0005C6E4
		public override Node Duplicate(Vector2 pos)
		{
			ForceSkinUseNode forceSkinUseNode = (ForceSkinUseNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			forceSkinUseNode._dataList = this._dataList;
			forceSkinUseNode._skinId = this._skinId;
			return forceSkinUseNode;
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0005E533 File Offset: 0x0005C733
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0005E535 File Offset: 0x0005C735
		protected override void OnNodeValidate()
		{
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0005E537 File Offset: 0x0005C737
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0005E53C File Offset: 0x0005C73C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<SkinDataList>(this.Inputs[1], ref this._dataList, canvas);
			base.GetInputValue<SkinId>(this.Inputs[2], ref this._skinId, canvas);
			SkinManager.Instance.ForceSkinUse(this._dataList, this._skinId);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000E28 RID: 3624
		public const string ID = "EE_forceSkinUse";

		// Token: 0x04000E29 RID: 3625
		private const int FLOW_INPUT_INDEX = 0;

		// Token: 0x04000E2A RID: 3626
		private const int FLOW_OUTPUT_INDEX = 0;

		// Token: 0x04000E2B RID: 3627
		private const int DATALIST_INPUT_INDEX = 1;

		// Token: 0x04000E2C RID: 3628
		private const int SKIN_ID_INPUT_INDEX = 2;

		// Token: 0x04000E2D RID: 3629
		private const string NODE_NAME = "Force Skin Use";

		// Token: 0x04000E2E RID: 3630
		private const string FLOW_OUTPUT_NAME = "Out";

		// Token: 0x04000E2F RID: 3631
		private const string FLOW_INPUT_NAME = "In";

		// Token: 0x04000E30 RID: 3632
		private const string DATALIST_INPUT_NAME = "Skin Data List";

		// Token: 0x04000E31 RID: 3633
		private const string SKINID_INPUT_NAME = "Skin Id";

		// Token: 0x04000E32 RID: 3634
		[SerializeField]
		private SkinDataList _dataList;

		// Token: 0x04000E33 RID: 3635
		[SerializeField]
		private SkinId _skinId;
	}
}
