using System;
using System.Collections.Generic;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002D4 RID: 724
	[Node(true, "Text Nodes/Display Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class DisplayTextNodeV2 : MessageNode
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0006E6CE File Offset: 0x0006C8CE
		public override string GetID
		{
			get
			{
				return "DisplayTextNodeV2";
			}
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x0006E6D8 File Offset: 0x0006C8D8
		public override Node Create(Vector2 pos)
		{
			DisplayTextNodeV2 displayTextNodeV = ScriptableObject.CreateInstance<DisplayTextNodeV2>();
			displayTextNodeV.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayTextNodeV.name = "Display Text";
			displayTextNodeV.CreateMutliInput("In", "Flow");
			displayTextNodeV.CreateInput("Term", "LocalizedString");
			displayTextNodeV.CreateInput("Priority", "Int");
			displayTextNodeV.CreateInput("Character", "Character");
			displayTextNodeV.CreateInput("Item", "Item");
			displayTextNodeV.CreateInput("Variables", ListConnection.ID);
			displayTextNodeV.CreateInput("Terms", ListConnection.ID);
			displayTextNodeV.CreateOutput("Out", "Flow");
			return displayTextNodeV;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0006E7A0 File Offset: 0x0006C9A0
		public override Node Duplicate(Vector2 pos)
		{
			DisplayTextNodeV2 displayTextNodeV = (DisplayTextNodeV2)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayTextNodeV._text = this._text;
			displayTextNodeV._character = this._character;
			displayTextNodeV._item = this._item;
			displayTextNodeV._priority = this._priority;
			return displayTextNodeV;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x0006E807 File Offset: 0x0006CA07
		protected override void NodeEnable()
		{
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0006E809 File Offset: 0x0006CA09
		protected override void NodeGUI()
		{
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0006E80C File Offset: 0x0006CA0C
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._text, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._priority, canvas);
			base.GetInputValue<Character>(this.Inputs[3], ref this._character, canvas);
			base.GetInputValue<IItem>(this.Inputs[4], ref this._item, canvas);
			List<int> inputValue = base.GetInputValue<List<int>>(this.Inputs[5], canvas);
			List<LocalizedString> inputValue2 = base.GetInputValue<List<LocalizedString>>(this.Inputs[6], canvas);
			TextJournalContent textJournalContent = new TextJournalContent(this._text, this._priority);
			if (this._character != null)
			{
				textJournalContent.Characters = new List<Character>
				{
					this._character
				};
			}
			if (this._item != null)
			{
				textJournalContent.Items = new List<IItem>
				{
					this._item
				};
			}
			if (inputValue != null && inputValue.Count > 0)
			{
				textJournalContent.LocalVariablesInts = inputValue;
			}
			if (inputValue2 != null && inputValue2.Count > 0)
			{
				textJournalContent.Terms = inputValue2;
			}
			if (this._text.ToString().Contains("EXPEDITION"))
			{
				textJournalContent.ExpeditionCharacter = ExpeditionManager.Instance.GetExpeditionCharacter();
			}
			SecondsEventManager.AddJournalContent(base.ParentCanvas, textJournalContent);
			base.CheckAreAllFlowOutputsConnected();
			this.Outputs[0].GetCustomNodeAcrossConnection<ParsecsNode>().ExecuteWithErrorHandling(canvas);
		}

		// Token: 0x04001327 RID: 4903
		public const string ID = "DisplayTextNodeV2";

		// Token: 0x04001328 RID: 4904
		[SerializeField]
		private LocalizedString _text;

		// Token: 0x04001329 RID: 4905
		[SerializeField]
		private Character _character;

		// Token: 0x0400132A RID: 4906
		[SerializeField]
		private IItem _item;

		// Token: 0x0400132B RID: 4907
		[SerializeField]
		private int _priority;

		// Token: 0x0400132C RID: 4908
		private const string NODE_NAME = "Display Text";

		// Token: 0x0400132D RID: 4909
		private const string INPUT_IN_NAME = "In";

		// Token: 0x0400132E RID: 4910
		private const string INPUT_TERM_NAME = "Term";

		// Token: 0x0400132F RID: 4911
		private const string INPUT_PRIORITY_NAME = "Priority";

		// Token: 0x04001330 RID: 4912
		private const string INPUT_CHARACTER_NAME = "Character";

		// Token: 0x04001331 RID: 4913
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x04001332 RID: 4914
		private const string INPUT_VARIABLES_NAME = "Variables";

		// Token: 0x04001333 RID: 4915
		private const string INPUT_TERMS_NAME = "Terms";

		// Token: 0x04001334 RID: 4916
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x04001335 RID: 4917
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x04001336 RID: 4918
		private const int INPUT_TERM_INDEX = 1;

		// Token: 0x04001337 RID: 4919
		private const int INPUT_PRIORITY_INDEX = 2;

		// Token: 0x04001338 RID: 4920
		private const int INPUT_CHARACTER_INDEX = 3;

		// Token: 0x04001339 RID: 4921
		private const int INPUT_ITEM_INDEX = 4;

		// Token: 0x0400133A RID: 4922
		private const int INPUT_VARIABLES_INDEX = 5;

		// Token: 0x0400133B RID: 4923
		private const int INPUT_TERMS_LIST_INDEX = 6;

		// Token: 0x0400133C RID: 4924
		private const int OUTPUT_INDEX = 0;
	}
}
