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
	// Token: 0x020002D5 RID: 725
	[Node(false, "Text Nodes/Display Text Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ReportEvent),
		typeof(Goal)
	})]
	public class DisplayTextNodeV3 : MessageNode
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x0006E985 File Offset: 0x0006CB85
		public override string GetID
		{
			get
			{
				return "DisplayTextNodeV3";
			}
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0006E98C File Offset: 0x0006CB8C
		public override Node Create(Vector2 pos)
		{
			DisplayTextNodeV3 displayTextNodeV = ScriptableObject.CreateInstance<DisplayTextNodeV3>();
			displayTextNodeV.rect = new Rect(pos.x, pos.y, 300f, 105f);
			displayTextNodeV.name = "Display Text";
			displayTextNodeV.CreateMutliInput("In", "Flow");
			displayTextNodeV.CreateInput("Term", "LocalizedString");
			displayTextNodeV.CreateInput("Priority", "Int");
			displayTextNodeV.CreateInput("Group ID", "GroupId");
			displayTextNodeV.CreateInput("Character", "Character");
			displayTextNodeV.CreateInput("Item", "Item");
			displayTextNodeV.CreateInput("Variables", ListConnection.ID);
			displayTextNodeV.CreateInput("Terms", ListConnection.ID);
			displayTextNodeV.CreateOutput("Out", "Flow");
			return displayTextNodeV;
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0006EA64 File Offset: 0x0006CC64
		public override Node Duplicate(Vector2 pos)
		{
			DisplayTextNodeV3 displayTextNodeV = (DisplayTextNodeV3)this.Create(this.rect.position + new Vector2(20f, 20f));
			displayTextNodeV._text = this._text;
			displayTextNodeV._character = this._character;
			displayTextNodeV._item = this._item;
			displayTextNodeV._priority = this._priority;
			displayTextNodeV._groupId = this._groupId;
			return displayTextNodeV;
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0006EAD7 File Offset: 0x0006CCD7
		protected override void NodeEnable()
		{
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0006EAD9 File Offset: 0x0006CCD9
		protected override void NodeGUI()
		{
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0006EADC File Offset: 0x0006CCDC
		public override void Execute(NodeCanvas canvas)
		{
			base.GetInputValue<LocalizedString>(this.Inputs[1], ref this._text, canvas);
			base.GetInputValue<int>(this.Inputs[2], ref this._priority, canvas);
			base.GetInputValue<Character>(this.Inputs[4], ref this._character, canvas);
			base.GetInputValue<IItem>(this.Inputs[5], ref this._item, canvas);
			base.GetInputValue<TextJournalGroupId>(this.Inputs[3], ref this._groupId, canvas);
			List<int> inputValue = base.GetInputValue<List<int>>(this.Inputs[6], canvas);
			List<LocalizedString> inputValue2 = base.GetInputValue<List<LocalizedString>>(this.Inputs[7], canvas);
			TextJournalContent textJournalContent = new TextJournalContent(this._text, this._priority);
			if (this._groupId != null)
			{
				textJournalContent.GroupId = this._groupId;
			}
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

		// Token: 0x0400133D RID: 4925
		public const string ID = "DisplayTextNodeV3";

		// Token: 0x0400133E RID: 4926
		[SerializeField]
		private LocalizedString _text;

		// Token: 0x0400133F RID: 4927
		[SerializeField]
		private Character _character;

		// Token: 0x04001340 RID: 4928
		[SerializeField]
		private IItem _item;

		// Token: 0x04001341 RID: 4929
		[SerializeField]
		private int _priority;

		// Token: 0x04001342 RID: 4930
		[SerializeField]
		private TextJournalGroupId _groupId;

		// Token: 0x04001343 RID: 4931
		private const string NODE_NAME = "Display Text";

		// Token: 0x04001344 RID: 4932
		private const string INPUT_IN_NAME = "In";

		// Token: 0x04001345 RID: 4933
		private const string INPUT_TERM_NAME = "Term";

		// Token: 0x04001346 RID: 4934
		private const string INPUT_PRIORITY_NAME = "Priority";

		// Token: 0x04001347 RID: 4935
		private const string INPUT_GROUP_ID_NAME = "Group ID";

		// Token: 0x04001348 RID: 4936
		private const string INPUT_CHARACTER_NAME = "Character";

		// Token: 0x04001349 RID: 4937
		private const string INPUT_ITEM_NAME = "Item";

		// Token: 0x0400134A RID: 4938
		private const string INPUT_VARIABLES_NAME = "Variables";

		// Token: 0x0400134B RID: 4939
		private const string INPUT_TERMS_NAME = "Terms";

		// Token: 0x0400134C RID: 4940
		private const string OUTPUT_OUT_NAME = "Out";

		// Token: 0x0400134D RID: 4941
		private const int INPUT_IN_INDEX = 0;

		// Token: 0x0400134E RID: 4942
		private const int INPUT_TERM_INDEX = 1;

		// Token: 0x0400134F RID: 4943
		private const int INPUT_PRIORITY_INDEX = 2;

		// Token: 0x04001350 RID: 4944
		private const int INPUT_GROUP_ID_INDEX = 3;

		// Token: 0x04001351 RID: 4945
		private const int INPUT_CHARACTER_INDEX = 4;

		// Token: 0x04001352 RID: 4946
		private const int INPUT_ITEM_INDEX = 5;

		// Token: 0x04001353 RID: 4947
		private const int INPUT_VARIABLES_INDEX = 6;

		// Token: 0x04001354 RID: 4948
		private const int INPUT_TERMS_LIST_INDEX = 7;

		// Token: 0x04001355 RID: 4949
		private const int OUTPUT_INDEX = 0;
	}
}
