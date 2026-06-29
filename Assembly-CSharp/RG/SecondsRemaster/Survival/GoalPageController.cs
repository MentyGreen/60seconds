using System;
using System.Collections.Generic;
using RG.Parsecs.Common;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000328 RID: 808
	public class GoalPageController : PageController
	{
		// Token: 0x06001B2B RID: 6955 RVA: 0x000752D0 File Offset: 0x000734D0
		public void RenderPages()
		{
			if (this._endGameData.RuntimeData.ShouldEndGame)
			{
				this.AddGoalList(GoalManager.Instance.AchievedGoals, false, true);
				this.AddGoalList(GoalManager.Instance.ActiveGoals, true, false);
				this.AddGoalList(GoalManager.Instance.FailedGoals, true, false);
			}
			else
			{
				this.AddGoalList(GoalManager.Instance.ActiveGoals, false, false);
				this.AddGoalList(GoalManager.Instance.AchievedGoals, false, true);
				this.AddGoalList(GoalManager.Instance.FailedGoals, true, false);
			}
			this._eventsRendererController.RenderContents();
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x00075368 File Offset: 0x00073568
		public override void SetPageData(bool visible)
		{
			if (visible && this._attentionVariable != null && this._attentionVariable.Value)
			{
				this._attentionVariable.Value = false;
			}
			if (!base.CanRefreshPageToday() || GoalManager.Instance == null)
			{
				return;
			}
			base.SetPageNotRefreshableToday();
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000753BB File Offset: 0x000735BB
		public override bool CanBeDisplayed()
		{
			return true;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000753C0 File Offset: 0x000735C0
		private void AddGoalList(List<Goal> goals, bool isFailed, bool isAchieved)
		{
			for (int i = 0; i < goals.Count; i++)
			{
				Goal goal = goals[i];
				if (goal.IsVisible)
				{
					this._journalContentsList.AddJournalContent(new GoalJournalContent(goal, isFailed, isAchieved, 0));
				}
			}
		}

		// Token: 0x040014F1 RID: 5361
		[SerializeField]
		private GlobalBoolVariable _attentionVariable;

		// Token: 0x040014F2 RID: 5362
		[SerializeField]
		private JournalContentsList _journalContentsList;

		// Token: 0x040014F3 RID: 5363
		[SerializeField]
		private EventsRendererController _eventsRendererController;
	}
}
