using System;
using I2.Loc;
using NodeEditorFramework;
using RG.Parsecs.EventEditor;
using RG.Parsecs.GoalEditor;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000313 RID: 787
	public class SecondsEventManager : EventManager
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x000735CC File Offset: 0x000717CC
		public Button JournalButtonNext
		{
			get
			{
				return this._journalButtonNext;
			}
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000735D4 File Offset: 0x000717D4
		private void Awake()
		{
			if (SecondsEventManager._instance == null)
			{
				SecondsEventManager._instance = this;
				EventManager.eventManager = this;
			}
			else
			{
				Object.Destroy(this);
			}
			this._endOfDayListener.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.SetJournalController), "PrepareSystem", 2, this, true);
			this._endOfDayListener.RegisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.RenderPages), "Visuals", 7, this, true);
			this._endOfDayListener.RegisterOnDayStarted(new EndOfDayListenerList.OnDayStarted(this.JournalOnDayStart), "ChangeFinished", 1);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00073660 File Offset: 0x00071860
		private void OnDestroy()
		{
			this._endOfDayListener.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.SetJournalController), "PrepareSystem");
			this._endOfDayListener.UnregisterOnDayStarted(new EndOfDayListenerList.OnDayStarted(this.JournalOnDayStart), "ChangeFinished");
			this._endOfDayListener.UnregisterOnEndOfDay(new EndOfDayListenerList.OnEndOfDay(this.RenderPages), "Visuals");
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000736C1 File Offset: 0x000718C1
		private void SetJournalController()
		{
			this._journalController.SetJournal();
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000736CE File Offset: 0x000718CE
		private void RenderPages()
		{
			this._journalController.RenderPages();
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000736DB File Offset: 0x000718DB
		private void JournalOnDayStart()
		{
			this._journalController.JournalOnDayStart();
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000736E8 File Offset: 0x000718E8
		public static void UnlockChoice()
		{
			ChoiceCardController playerChoice = EventManager.GetPlayerChoice();
			if (playerChoice != null)
			{
				if (playerChoice.Character != null)
				{
					playerChoice.Character.Unlock();
				}
				if (playerChoice.Item != null)
				{
					playerChoice.Item.Unlock();
				}
				if (playerChoice.ActionCondition != null)
				{
					playerChoice.ActionCondition.OnDeselect();
				}
			}
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x0007374F File Offset: 0x0007194F
		public static Button GetReferenceToJournalButtonNext()
		{
			return SecondsEventManager._instance._journalButtonNext;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x0007375B File Offset: 0x0007195B
		public static void SetCurrentChoiceCardsController(ChoiceCardsController choiceCardsController)
		{
			SecondsEventManager._instance._choiceCardsController = choiceCardsController;
			SecondsEventManager.RefreshChoiceCards();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x0007376D File Offset: 0x0007196D
		public static void RefreshChoiceCards()
		{
			if (SecondsEventManager._instance._choiceCardsController != null)
			{
				SecondsEventManager._instance._choiceCardsController.RefreshCardsController();
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00073790 File Offset: 0x00071990
		public static void AddJournalContent(NodeCanvas canvas, JournalContent content)
		{
			EParsecsEventPhase canvasPhase = SecondsEventManager.GetCanvasPhase(canvas);
			TextJournalContent textJournalContent = content as TextJournalContent;
			if (canvasPhase == EParsecsEventPhase.ACTION)
			{
				if (textJournalContent != null)
				{
					textJournalContent.EventPhase = EParsecsEventPhase.ACTION;
				}
				SecondsEventManager._instance._actionContents.AddJournalContent(content);
				return;
			}
			if (canvasPhase != EParsecsEventPhase.REPORT)
			{
				return;
			}
			if (textJournalContent != null)
			{
				textJournalContent.EventPhase = EParsecsEventPhase.REPORT;
			}
			SecondsEventManager._instance._reportContents.AddJournalContent(content);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000737EC File Offset: 0x000719EC
		private static EParsecsEventPhase GetCanvasPhase(NodeCanvas parentCanvas)
		{
			EParsecsEventPhase currentPhase;
			if (parentCanvas is ParsecsEvent)
			{
				currentPhase = (parentCanvas as ParsecsEvent).CurrentPhase;
			}
			else if (parentCanvas is NodeFunction)
			{
				currentPhase = (parentCanvas as NodeFunction).GetOriginalCanvasAs<ParsecsEvent>().CurrentPhase;
			}
			else
			{
				if (parentCanvas is Goal)
				{
					return EParsecsEventPhase.REPORT;
				}
				throw new UnityException("This canvas type can only be executed from canvases that derive from ParsecsEvent or NodeFunctions");
			}
			return currentPhase;
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00073840 File Offset: 0x00071A40
		protected override ChoiceCardController GetChoice()
		{
			if (this._choiceCardsController == null)
			{
				Debug.LogError("Choice Cards Controller jest nullem");
				return null;
			}
			return this._choiceCardsController.GetPlayerChoice();
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00073867 File Offset: 0x00071A67
		protected override void ShowGoalsUpdate(LocalizedString term, LocalizedString goalName)
		{
		}

		// Token: 0x04001478 RID: 5240
		[SerializeField]
		private EndOfDayListenerList _endOfDayListener;

		// Token: 0x04001479 RID: 5241
		[SerializeField]
		private JournalController _journalController;

		// Token: 0x0400147A RID: 5242
		[SerializeField]
		private JournalContentsList _reportContents;

		// Token: 0x0400147B RID: 5243
		[SerializeField]
		private JournalContentsList _actionContents;

		// Token: 0x0400147C RID: 5244
		[SerializeField]
		private Button _journalButtonNext;

		// Token: 0x0400147D RID: 5245
		private static SecondsEventManager _instance;

		// Token: 0x0400147E RID: 5246
		private ChoiceCardsController _choiceCardsController;
	}
}
