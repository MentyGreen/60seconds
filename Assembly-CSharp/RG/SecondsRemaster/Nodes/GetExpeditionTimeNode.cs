using System;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.NodeEditor;
using RG.Parsecs.Survival;
using UnityEngine;

namespace RG.SecondsRemaster.Nodes
{
	// Token: 0x02000256 RID: 598
	[Node(false, "Remaster/Expedition Nodes/Get Expedition Time Node", new Type[]
	{
		typeof(SurvivalEvent),
		typeof(ReportEvent),
		typeof(SystemEvent),
		typeof(NodeFunction),
		typeof(SystemStatusEvent),
		typeof(ExpeditionEvent),
		typeof(ConditionEvent)
	})]
	public class GetExpeditionTimeNode : ExpeditionNode
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00061441 File Offset: 0x0005F641
		public override string GetID
		{
			get
			{
				return "EE_GetExpeditionTimeNode";
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00061448 File Offset: 0x0005F648
		public override Node Create(Vector2 pos)
		{
			GetExpeditionTimeNode getExpeditionTimeNode = ScriptableObject.CreateInstance<GetExpeditionTimeNode>();
			getExpeditionTimeNode.rect = new Rect(pos.x, pos.y, 180f, 90f);
			getExpeditionTimeNode.name = "Get Expedition Time";
			getExpeditionTimeNode.CreateOutput("Expedition Length in Days", "Int");
			getExpeditionTimeNode.CreateOutput("Elapsed Days", "Int");
			getExpeditionTimeNode.CreateOutput("Left Days", "Int");
			getExpeditionTimeNode.CreateOutput("Min Days", "Int");
			getExpeditionTimeNode.CreateOutput("Max Days", "Int");
			return getExpeditionTimeNode;
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000614DB File Offset: 0x0005F6DB
		public override Node Duplicate(Vector2 pos)
		{
			return this.Create(this.rect.position + new Vector2(20f, 20f));
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00061504 File Offset: 0x0005F704
		public override T GetValue<T>(int output, NodeCanvas canvas)
		{
			if (output > 4)
			{
				throw new NotExistingOutputException(this.GetID, output);
			}
			if (!ExpeditionManager.Instance.IsExpeditionOngoing())
			{
				return base.CastValue<T>(0);
			}
			ExpeditionDestination currentDestination = ExpeditionManager.Instance.GetCurrentDestination();
			if (currentDestination == null && currentDestination.Event == null)
			{
				return base.CastValue<T>(0);
			}
			ExpeditionEvent @event = currentDestination.Event;
			switch (output)
			{
			case 0:
				return base.CastValue<T>(@event.ExpeditionLength);
			case 1:
				return base.CastValue<T>(ExpeditionManager.Instance.GetElapsedExpeditionTime());
			case 2:
				return base.CastValue<T>(@event.ExpeditionLength - ExpeditionManager.Instance.GetElapsedExpeditionTime());
			case 3:
				return base.CastValue<T>(@event.MinimumDaysAway);
			case 4:
				return base.CastValue<T>(@event.MaximumDaysAway);
			default:
				throw new NotExistingOutputException(this.GetID, output);
			}
		}

		// Token: 0x04000F02 RID: 3842
		public const string ID = "EE_GetExpeditionTimeNode";

		// Token: 0x04000F03 RID: 3843
		public const string NODE_NAME = "Get Expedition Time";

		// Token: 0x04000F04 RID: 3844
		public const string OUTPUT_TIME_EXPEDITION_LENGTH_NAME = "Expedition Length in Days";

		// Token: 0x04000F05 RID: 3845
		public const int OUTPUT_TIME_EXPEDITION_LENGTH_INDEX = 0;

		// Token: 0x04000F06 RID: 3846
		public const string OUTPUT_TIME_ELAPSED_NAME = "Elapsed Days";

		// Token: 0x04000F07 RID: 3847
		public const int OUTPUT_TIME_ELAPSED_INDEX = 1;

		// Token: 0x04000F08 RID: 3848
		public const string OUTPUT_TIME_LEFT_NAME = "Left Days";

		// Token: 0x04000F09 RID: 3849
		public const int OUTPUT_TIME_LEFT_INDEX = 2;

		// Token: 0x04000F0A RID: 3850
		public const string OUTPUT_TIME_MIN_NAME = "Min Days";

		// Token: 0x04000F0B RID: 3851
		public const int OUTPUT_TIME_MIN_INDEX = 3;

		// Token: 0x04000F0C RID: 3852
		public const string OUTPUT_TIME_MAX_NAME = "Max Days";

		// Token: 0x04000F0D RID: 3853
		public const int OUTPUT_TIME_MAX_INDEX = 4;
	}
}
