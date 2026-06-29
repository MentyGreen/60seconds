using System;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002F0 RID: 752
	public class GoalJournalContentDisplayer : JournalContentDisplayer<GoalJournalContent>
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		public TextMeshProUGUI TextField
		{
			get
			{
				return this._textField;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00070BDC File Offset: 0x0006EDDC
		public override int LinesAmount
		{
			get
			{
				return this._textField.textInfo.lineCount;
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00070BF0 File Offset: 0x0006EDF0
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.GOAL)
			{
				return;
			}
			GoalJournalContent goalJournalContent = (GoalJournalContent)content;
			this._textField.text = goalJournalContent.Term;
			goalJournalContent.CheckmarkIndex = Random.Range(0, this._done.Length);
			this._brackets[goalJournalContent.CheckmarkIndex].SetActive(true);
			if (goalJournalContent.IsAchieved)
			{
				this._done[goalJournalContent.CheckmarkIndex].SetActive(true);
			}
			else if (goalJournalContent.IsFailed)
			{
				this._fail[goalJournalContent.CheckmarkIndex].SetActive(true);
			}
			else if (!goalJournalContent.IsFailed && !goalJournalContent.IsAchieved)
			{
				this._fail[goalJournalContent.CheckmarkIndex].SetActive(false);
				this._done[goalJournalContent.CheckmarkIndex].SetActive(false);
			}
			this.ForceTextMeshProUpdate();
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x00070CC1 File Offset: 0x0006EEC1
		private void ForceTextMeshProUpdate()
		{
			Canvas.ForceUpdateCanvases();
			this._textField.ForceMeshUpdate(false, false);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00070CD5 File Offset: 0x0006EED5
		public void AttachPreviousTextMeshPro(TextMeshProUGUI previousTmp)
		{
			previousTmp.overflowMode = TextOverflowModes.Linked;
			previousTmp.linkedTextComponent = this._textField;
			Canvas.ForceUpdateCanvases();
			this._textField.ForceMeshUpdate(false, false);
			previousTmp.ForceMeshUpdate(false, false);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00070D04 File Offset: 0x0006EF04
		public void AppendText(TextJournalContent content, string separator = "\n")
		{
			if (string.IsNullOrEmpty(content.Term.mTerm) || content.Term == null)
			{
				TextMeshProUGUI textField = this._textField;
				textField.text = textField.text + separator + content.PureText;
			}
			else
			{
				content.RegisterManagers();
				TextMeshProUGUI textField2 = this._textField;
				textField2.text = textField2.text + separator + content.Term;
				content.UnregisterManagers();
			}
			this.ForceTextMeshProUpdate();
		}

		// Token: 0x040013E1 RID: 5089
		[SerializeField]
		private TextMeshProUGUI _textField;

		// Token: 0x040013E2 RID: 5090
		[SerializeField]
		private GameObject[] _done;

		// Token: 0x040013E3 RID: 5091
		[SerializeField]
		private GameObject[] _fail;

		// Token: 0x040013E4 RID: 5092
		[SerializeField]
		private GameObject[] _brackets;
	}
}
