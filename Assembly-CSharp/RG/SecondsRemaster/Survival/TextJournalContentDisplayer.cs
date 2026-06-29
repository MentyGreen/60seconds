using System;
using System.Collections;
using RG.Parsecs.EventEditor;
using TMPro;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000301 RID: 769
	public class TextJournalContentDisplayer : JournalContentDisplayer<TextJournalContent>
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00071C87 File Offset: 0x0006FE87
		public TextMeshProUGUI TextField
		{
			get
			{
				return this._textField;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x00071C8F File Offset: 0x0006FE8F
		public override int LinesAmount
		{
			get
			{
				return this._textField.textInfo.lineCount;
			}
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x00071CA4 File Offset: 0x0006FEA4
		public override void SetContentData(JournalContent content)
		{
			if (content.Type != EJournalContentType.TEXT)
			{
				return;
			}
			TextJournalContent textJournalContent = (TextJournalContent)content;
			if (string.IsNullOrEmpty(textJournalContent.Term.mTerm) || textJournalContent.Term == null)
			{
				this._textField.text = textJournalContent.PureText;
			}
			else
			{
				textJournalContent.RegisterManagers();
				this._textField.text = textJournalContent.Term;
				textJournalContent.UnregisterManagers();
			}
			this.SetLinesData(textJournalContent);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x00071D1C File Offset: 0x0006FF1C
		private void ForceTextMeshProUpdate()
		{
			Canvas.ForceUpdateCanvases();
			this._textField.ForceMeshUpdate(false, false);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x00071D30 File Offset: 0x0006FF30
		private void SetLinesData(TextJournalContent content)
		{
			LinesDescription.LineDescription linesDescriptionForCurrentLanguage = this._linesDescription.GetLinesDescriptionForCurrentLanguage();
			if (content.EventPhase == EParsecsEventPhase.ACTION)
			{
				this._textField.fontSize = linesDescriptionForCurrentLanguage.ActionPageTextSize;
			}
			else if (content.EventPhase == EParsecsEventPhase.REPORT)
			{
				this._textField.fontSize = linesDescriptionForCurrentLanguage.ReportPageTextSize;
			}
			this.ForceTextMeshProUpdate();
			base.LayoutElement.preferredHeight = (float)(linesDescriptionForCurrentLanguage.FirstLineHeight + linesDescriptionForCurrentLanguage.LineHeight * (this.LinesAmount - 1));
			this._textField.lineSpacing = linesDescriptionForCurrentLanguage.LineSpacing;
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x00071DB8 File Offset: 0x0006FFB8
		public void AttachPreviousTextMeshPro(TextMeshProUGUI previousTmp, TextJournalContentDisplayer previousDisplayer)
		{
			this._previousDisplayer = previousDisplayer;
			previousTmp.overflowMode = TextOverflowModes.Linked;
			previousTmp.linkedTextComponent = this._textField;
			this._textField.lineSpacing = previousTmp.lineSpacing;
			Canvas.ForceUpdateCanvases();
			this._textField.ForceMeshUpdate(false, false);
			previousTmp.ForceMeshUpdate(false, false);
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x00071E0F File Offset: 0x0007000F
		public void TryToFixLinkedText()
		{
			if (this._textField != null && this._textField.linkedTextComponent != null)
			{
				base.StartCoroutine(this.EnableAndDisablePreviousPage());
			}
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x00071E3F File Offset: 0x0007003F
		private IEnumerator EnableAndDisablePreviousPage()
		{
			Transform previousPage = null;
			int siblingIndex = base.transform.parent.GetSiblingIndex();
			if (siblingIndex > 0)
			{
				previousPage = base.transform.parent.parent.GetChild(siblingIndex - 1);
			}
			if (previousPage != null)
			{
				CanvasGroup pageCanvasGroup = previousPage.GetComponent<CanvasGroup>();
				if (pageCanvasGroup != null)
				{
					pageCanvasGroup.alpha = 0f;
					previousPage.gameObject.SetActive(true);
					Canvas.ForceUpdateCanvases();
					yield return new WaitForEndOfFrame();
					yield return new WaitForEndOfFrame();
					if (previousPage != null)
					{
						previousPage.gameObject.SetActive(false);
					}
					if (pageCanvasGroup != null)
					{
						pageCanvasGroup.alpha = 1f;
					}
					Canvas.ForceUpdateCanvases();
				}
				pageCanvasGroup = null;
			}
			yield break;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x00071E50 File Offset: 0x00070050
		public void AppendText(TextJournalContent content, string separator = "\n")
		{
			TextMeshProUGUI textField = this.GetParentDisplayer().TextField;
			if (string.IsNullOrEmpty(content.Term.mTerm) || content.Term == null)
			{
				TextMeshProUGUI textMeshProUGUI = textField;
				textMeshProUGUI.text = textMeshProUGUI.text + separator + content.PureText;
			}
			else
			{
				content.RegisterManagers();
				TextMeshProUGUI textMeshProUGUI2 = textField;
				textMeshProUGUI2.text = textMeshProUGUI2.text + separator + content.Term;
				content.UnregisterManagers();
			}
			this.SetLinesData(content);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00071ED4 File Offset: 0x000700D4
		public TextJournalContentDisplayer GetParentDisplayer()
		{
			if (this._previousDisplayer == null)
			{
				return this;
			}
			return this._previousDisplayer.GetParentDisplayer();
		}

		// Token: 0x04001413 RID: 5139
		[SerializeField]
		private TextMeshProUGUI _textField;

		// Token: 0x04001414 RID: 5140
		[SerializeField]
		private LinesDescription _linesDescription;

		// Token: 0x04001415 RID: 5141
		private TextJournalContentDisplayer _previousDisplayer;
	}
}
