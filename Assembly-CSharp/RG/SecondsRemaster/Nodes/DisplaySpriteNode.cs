using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using RG.SecondsRemaster.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000263 RID: 611
	[Node(false, "Utility Nodes/Display Sprite Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent)
	})]
	public class DisplaySpriteNode : EventNode
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00063694 File Offset: 0x00061894
		public override string GetID
		{
			get
			{
				return "displaySpriteNode";
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x0006369C File Offset: 0x0006189C
		public override Node Create(Vector2 pos)
		{
			DisplaySpriteNode displaySpriteNode = ScriptableObject.CreateInstance<DisplaySpriteNode>();
			displaySpriteNode.rect = new Rect(pos.x, pos.y, 250f, 105f);
			displaySpriteNode.name = "Display Sprite";
			displaySpriteNode.CreateMutliInput("In", "Flow");
			displaySpriteNode.CreateInput("Priority", "Int");
			displaySpriteNode.CreateOutput("Out", "Flow");
			return displaySpriteNode;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00063710 File Offset: 0x00061910
		public override Node Duplicate(Vector2 pos)
		{
			DisplaySpriteNode displaySpriteNode = (DisplaySpriteNode)this.Create(this.rect.position + new Vector2(20f, 20f));
			displaySpriteNode._sprite = this._sprite;
			displaySpriteNode._spriteAlign = this._spriteAlign;
			displaySpriteNode._priority = this._priority;
			return displaySpriteNode;
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0006376B File Offset: 0x0006196B
		protected override void NodeEnable()
		{
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0006376D File Offset: 0x0006196D
		protected override void NodeGUI()
		{
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00063770 File Offset: 0x00061970
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<int>(this.Inputs[1], ref this._priority, canvas);
			SpriteJournalContent content = new SpriteJournalContent(this._sprite, this._spriteAlign, this._priority);
			SecondsEventManager.AddJournalContent(base.ParentCanvas, content);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04000FAC RID: 4012
		public const string ID = "displaySpriteNode";

		// Token: 0x04000FAD RID: 4013
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04000FAE RID: 4014
		private const string INPUT_PRIORITY_NAME = "Priority";

		// Token: 0x04000FAF RID: 4015
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04000FB0 RID: 4016
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04000FB1 RID: 4017
		private const int INPUT_PRIORITY_INDEX = 1;

		// Token: 0x04000FB2 RID: 4018
		private const int OUTPUT_OUT_INDEX = 0;

		// Token: 0x04000FB3 RID: 4019
		public const string NODE_NAME = "Display Sprite";

		// Token: 0x04000FB4 RID: 4020
		[SerializeField]
		private Sprite _sprite;

		// Token: 0x04000FB5 RID: 4021
		[SerializeField]
		private int _priority;

		// Token: 0x04000FB6 RID: 4022
		[SerializeField]
		private EventContentData.ESpriteAlign _spriteAlign = EventContentData.ESpriteAlign.CENTER;
	}
}
