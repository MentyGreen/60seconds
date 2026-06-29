using System;
using System.Collections.Generic;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x0200030C RID: 780
	public class EventsRendererController : MonoBehaviour
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x0007225C File Offset: 0x0007045C
		public bool CanShowDoodleOnLastPage(float maxSpaceTakenByText = 0.5f)
		{
			float num;
			if (this._choiceWasRendered)
			{
				num = this.CalculateCurrentPageHeightWithoutLastElement();
			}
			else
			{
				num = this.CalculateCurrentPageHeight();
			}
			return num < this._maxPageHeight * maxSpaceTakenByText;
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x00072294 File Offset: 0x00070494
		private float CalculateCurrentPageHeight()
		{
			if (this._rectTransforms == null || this._rectTransforms.Count == 0 || this._rectTransforms[this._currentPageIndex] == null)
			{
				return 0f;
			}
			Canvas.ForceUpdateCanvases();
			float num = 0f;
			for (int i = 0; i < this._rectTransforms[this._currentPageIndex].Count; i++)
			{
				num += this._rectTransforms[this._currentPageIndex][i].sizeDelta.y;
			}
			return num;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x00072320 File Offset: 0x00070520
		private float CalculateCurrentPageHeightWithoutLastElement()
		{
			if (this._rectTransforms == null || this._rectTransforms.Count == 0 || this._rectTransforms[this._currentPageIndex] == null)
			{
				return 0f;
			}
			Canvas.ForceUpdateCanvases();
			float num = 0f;
			for (int i = 0; i < this._rectTransforms[this._currentPageIndex].Count - 1; i++)
			{
				num += this._rectTransforms[this._currentPageIndex][i].sizeDelta.y;
			}
			return num;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000723B0 File Offset: 0x000705B0
		private JournalContentDisplayer GroupTextIconContents(List<JournalContent> journalContents, int index)
		{
			if (journalContents[index].Type == EJournalContentType.TEXT_ICON && index - 1 >= 0)
			{
				int num = index - 1;
				while (num >= 0 && journalContents[num].Type == EJournalContentType.TEXT_ICON)
				{
					TextIconJournalContent textIconJournalContent = journalContents[num] as TextIconJournalContent;
					if (textIconJournalContent != null)
					{
						TextIconJournalContentDisplayer textIconJournalContentDisplayer = textIconJournalContent.Displayer as TextIconJournalContentDisplayer;
						if (textIconJournalContentDisplayer != null)
						{
							TextIconJournalContent textIconJournalContent2 = journalContents[index] as TextIconJournalContent;
							if (textIconJournalContent2 != null && textIconJournalContentDisplayer.TryToJoinNextTextIconContent(textIconJournalContent2))
							{
								return textIconJournalContentDisplayer;
							}
						}
					}
					num--;
				}
			}
			return null;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x00072430 File Offset: 0x00070630
		private JournalContentDisplayer GroupTextContents(List<JournalContent> journalContents, int index)
		{
			if (index - 1 >= 0 && journalContents[index].GroupId != null && journalContents[index - 1].GroupId != null && journalContents[index].GroupId == journalContents[index - 1].GroupId && journalContents[index].Type == EJournalContentType.TEXT && journalContents[index - 1].Type == EJournalContentType.TEXT)
			{
				TextJournalContent textJournalContent = journalContents[index - 1] as TextJournalContent;
				if (textJournalContent != null)
				{
					TextJournalContentDisplayer textJournalContentDisplayer = textJournalContent.Displayer as TextJournalContentDisplayer;
					if (textJournalContentDisplayer != null)
					{
						TextJournalContent textJournalContent2 = journalContents[index] as TextJournalContent;
						if (textJournalContent2 != null)
						{
							textJournalContent2.Displayer = textJournalContent.Displayer;
							textJournalContentDisplayer.AppendText(textJournalContent2, Environment.NewLine);
							textJournalContentDisplayer.GetComponent<LayoutElement>().CalculateLayoutInputVertical();
							textJournalContentDisplayer.GetComponent<LayoutElement>().preferredHeight += this._linesDescriptions.GetLinesDescriptionForCurrentLanguage().TextMargin;
							return textJournalContentDisplayer;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x0007253C File Offset: 0x0007073C
		private JournalContentDisplayer SpawnNewContentDisplayer(List<JournalContent> journalContents, int index)
		{
			JournalContentDisplayer journalContentDisplayer = this.SpawnContent(journalContents[index]);
			journalContents[index].Displayer = journalContentDisplayer;
			Canvas.ForceUpdateCanvases();
			if (journalContents[index].Type == EJournalContentType.TEXT_ICON)
			{
				if (this._currentTextIconGroup == null)
				{
					this._currentTextIconGroup = Object.Instantiate<RectTransform>(this._textIconGroupPrefab, this._currentContentHolder);
				}
				journalContentDisplayer.RectTransform.SetParent(this._currentTextIconGroup);
				this._currentTextIconGroup.GetComponent<LayoutElement>().CalculateLayoutInputVertical();
				this._currentTextIconGroup.GetComponent<LayoutElement>().preferredHeight += this._linesDescriptions.GetLinesDescriptionForCurrentLanguage().TextMargin;
			}
			else if (journalContents[index].Type == EJournalContentType.YESNO_CHOICE || journalContents[index].Type == EJournalContentType.CHARACTER_CHOICE || journalContents[index].Type == EJournalContentType.CUSTOM_CHOICE || journalContents[index].Type == EJournalContentType.ITEM_CHOICE)
			{
				journalContents[index].Displayer.LayoutElement.preferredHeight = this._maxPageHeight - this._currentPageHeight;
				journalContents[index].Displayer.SetTooltipController(this._actionChoiceTooltipTransform);
				this._choiceWasRendered = true;
			}
			else if (journalContents[index].Type == EJournalContentType.TEXT)
			{
				TextJournalContentDisplayer textJournalContentDisplayer = journalContentDisplayer as TextJournalContentDisplayer;
				if (textJournalContentDisplayer != null)
				{
					if (index == 0)
					{
						textJournalContentDisplayer.TextField.margin = new Vector4(textJournalContentDisplayer.TextField.margin.x, 0f, textJournalContentDisplayer.TextField.margin.z, textJournalContentDisplayer.TextField.margin.w);
					}
					else
					{
						textJournalContentDisplayer.TextField.margin = new Vector4(textJournalContentDisplayer.TextField.margin.x, this._linesDescriptions.GetLinesDescriptionForCurrentLanguage().TextMargin, textJournalContentDisplayer.TextField.margin.z, textJournalContentDisplayer.TextField.margin.w);
						textJournalContentDisplayer.GetComponent<LayoutElement>().CalculateLayoutInputVertical();
						textJournalContentDisplayer.GetComponent<LayoutElement>().preferredHeight += this._linesDescriptions.GetLinesDescriptionForCurrentLanguage().TextMargin;
					}
				}
			}
			if (journalContents[index].Type != EJournalContentType.TEXT_ICON)
			{
				this._currentTextIconGroup = null;
			}
			return journalContentDisplayer;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00072770 File Offset: 0x00070970
		private JournalContentDisplayer MoveContentToNextPage(List<JournalContent> journalContents, int index, JournalContentDisplayer currentDisplayer)
		{
			float y = currentDisplayer.RectTransform.sizeDelta.y;
			float num = this._currentPageHeight - this._maxPageHeight;
			float currentPageHeight = this._currentPageHeight;
			JournalContentDisplayer journalContentDisplayer;
			if (journalContents[index].Type == EJournalContentType.TEXT)
			{
				this.SpawnNewSubPage(false);
				if (currentDisplayer.LinesAmount < this._minimumLinesAmountEligibleForBreaking)
				{
					currentDisplayer.RectTransform.SetParent(this._currentContentHolder);
					journalContentDisplayer = currentDisplayer;
				}
				else
				{
					TextJournalContentDisplayer textJournalContentDisplayer = (TextJournalContentDisplayer)Object.Instantiate<JournalContentDisplayer>(this._contentsDisplayers.GetContentDisplayer(EJournalContentType.TEXT), this._currentContentHolder);
					TextJournalContentDisplayer textJournalContentDisplayer2 = (TextJournalContentDisplayer)currentDisplayer;
					textJournalContentDisplayer.AttachPreviousTextMeshPro(textJournalContentDisplayer2.TextField, textJournalContentDisplayer2);
					textJournalContentDisplayer.GetComponent<Localize>().LocalizeEvent.AddListener(delegate()
					{
						WordwrapLanguageSetup.StaticAsianWordWrap();
					});
					LinesDescription.LineDescription linesDescriptionForCurrentLanguage = this._linesDescriptions.GetLinesDescriptionForCurrentLanguage();
					float num2 = Mathf.Ceil(this._maxPageHeight - (currentPageHeight - y));
					if (num < (float)linesDescriptionForCurrentLanguage.LineHeight)
					{
						num2 -= (float)linesDescriptionForCurrentLanguage.LineHeight - linesDescriptionForCurrentLanguage.TextMargin;
						num = (float)linesDescriptionForCurrentLanguage.FirstLineHeight;
					}
					currentDisplayer.LayoutElement.preferredHeight = num2;
					textJournalContentDisplayer.LayoutElement.preferredHeight = num;
					textJournalContentDisplayer.TextField.margin = new Vector4(textJournalContentDisplayer.TextField.margin.x, 0f, textJournalContentDisplayer.TextField.margin.z, textJournalContentDisplayer.TextField.margin.w);
					journalContentDisplayer = textJournalContentDisplayer;
					journalContents[index].Displayer = journalContentDisplayer;
					this.UpdateTMPMeshes(textJournalContentDisplayer2.TextField, textJournalContentDisplayer.TextField);
					int num3 = linesDescriptionForCurrentLanguage.FirstLineHeight + linesDescriptionForCurrentLanguage.LineHeight * (textJournalContentDisplayer.LinesAmount - 1);
					textJournalContentDisplayer.LayoutElement.preferredHeight = (float)num3;
					this.UpdateTMPMeshes(textJournalContentDisplayer2.TextField, textJournalContentDisplayer.TextField);
				}
			}
			else if (journalContents[index].Type == EJournalContentType.TEXT_ICON)
			{
				this.SpawnNewSubPage(false);
				this._currentTextIconGroup = Object.Instantiate<RectTransform>(this._textIconGroupPrefab, this._currentContentHolder);
				currentDisplayer.RectTransform.SetParent(this._currentTextIconGroup);
				journalContentDisplayer = currentDisplayer;
			}
			else
			{
				this.SpawnNewSubPage(false);
				currentDisplayer.RectTransform.SetParent(this._currentContentHolder);
				journalContentDisplayer = currentDisplayer;
			}
			return journalContentDisplayer;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000729B4 File Offset: 0x00070BB4
		public void RenderContents()
		{
			this._choiceWasRendered = false;
			this.ClearRenderedPages();
			this.SpawnNewSubPage(true);
			List<JournalContent> sortedJournalContents = this._contents.GetSortedJournalContents();
			for (int i = 0; i < sortedJournalContents.Count; i++)
			{
				JournalContentDisplayer journalContentDisplayer = null;
				this._currentPageHeight = this.CalculateCurrentPageHeight();
				if (sortedJournalContents[i].Type == EJournalContentType.TEXT_ICON)
				{
					journalContentDisplayer = this.GroupTextIconContents(sortedJournalContents, i);
				}
				else if (sortedJournalContents[i].Type == EJournalContentType.TEXT)
				{
					journalContentDisplayer = this.GroupTextContents(sortedJournalContents, i);
				}
				if (journalContentDisplayer == null)
				{
					journalContentDisplayer = this.SpawnNewContentDisplayer(sortedJournalContents, i);
				}
				this.AddContentDisplayerToPageTransformsList(journalContentDisplayer);
				this._currentPageHeight = this.CalculateCurrentPageHeight();
				while (this._currentPageHeight > this._maxPageHeight)
				{
					journalContentDisplayer = this.MoveContentToNextPage(sortedJournalContents, i, journalContentDisplayer);
					this.AddContentDisplayerToPageTransformsList(journalContentDisplayer);
					this._currentPageHeight = this.CalculateCurrentPageHeight();
				}
			}
			this.RefreshPages();
			this._contents.ClearJournalContentsList();
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00072A9C File Offset: 0x00070C9C
		private void AddContentDisplayerToPageTransformsList(JournalContentDisplayer displayer)
		{
			if (this._rectTransforms == null)
			{
				return;
			}
			if (this._rectTransforms.Count <= this._currentPageIndex)
			{
				this._rectTransforms.Add(new List<RectTransform>());
			}
			RectTransform item;
			if (displayer is TextIconJournalContentDisplayer && this._currentTextIconGroup != null)
			{
				item = this._currentTextIconGroup;
			}
			else
			{
				item = displayer.RectTransform;
			}
			if (!this._rectTransforms[this._currentPageIndex].Contains(item))
			{
				this._rectTransforms[this._currentPageIndex].Add(item);
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00072B2B File Offset: 0x00070D2B
		private void ClearRenderedPages()
		{
			this._pageController.ClearSubpages();
			if (this._rectTransforms == null)
			{
				this._rectTransforms = new List<List<RectTransform>>();
			}
			else
			{
				this._rectTransforms.Clear();
			}
			this._currentPageIndex = 0;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00072B5F File Offset: 0x00070D5F
		private void UpdateTMPMeshes(TextMeshProUGUI firstTmp, TextMeshProUGUI secondTmp)
		{
			firstTmp.enabled = false;
			Canvas.ForceUpdateCanvases();
			firstTmp.enabled = true;
			Canvas.ForceUpdateCanvases();
			firstTmp.ForceMeshUpdate(false, false);
			secondTmp.ForceMeshUpdate(false, false);
			Canvas.ForceUpdateCanvases();
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00072B90 File Offset: 0x00070D90
		private void SpawnNewSubPage(bool firstPage = false)
		{
			SubPageController subPageController = Object.Instantiate<SubPageController>(this._subPagePrefab, this._pageHolder);
			this._currentContentHolder = subPageController.GetComponent<RectTransform>();
			this._pageController.AddNewSubPage(subPageController);
			this._currentPageHeight = 0f;
			if (!firstPage)
			{
				this._currentPageIndex++;
			}
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00072BE3 File Offset: 0x00070DE3
		private JournalContentDisplayer SpawnContent(JournalContent content)
		{
			JournalContentDisplayer journalContentDisplayer = Object.Instantiate<JournalContentDisplayer>(this._contentsDisplayers.GetContentDisplayer(content.Type), this._currentContentHolder);
			journalContentDisplayer.SetContentData(content);
			return journalContentDisplayer;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00072C08 File Offset: 0x00070E08
		private void RefreshPages()
		{
			List<PageController> pages = this._pageController.GetSubpagesList().Pages;
			for (int i = 0; i < pages.Count; i++)
			{
				pages[i].gameObject.SetActive(i == 0);
			}
		}

		// Token: 0x04001429 RID: 5161
		[SerializeField]
		private JournalContentsList _contents;

		// Token: 0x0400142A RID: 5162
		[SerializeField]
		private JournalContentsDisplayerList _contentsDisplayers;

		// Token: 0x0400142B RID: 5163
		[SerializeField]
		private PageController _pageController;

		// Token: 0x0400142C RID: 5164
		[SerializeField]
		private SubPageController _subPagePrefab;

		// Token: 0x0400142D RID: 5165
		[SerializeField]
		private RectTransform _textIconGroupPrefab;

		// Token: 0x0400142E RID: 5166
		[SerializeField]
		private float _maxPageHeight;

		// Token: 0x0400142F RID: 5167
		[SerializeField]
		private int _minimumLinesAmountEligibleForBreaking = 5;

		// Token: 0x04001430 RID: 5168
		[SerializeField]
		private RectTransform _pageHolder;

		// Token: 0x04001431 RID: 5169
		[SerializeField]
		private LinesDescription _linesDescriptions;

		// Token: 0x04001432 RID: 5170
		[SerializeField]
		private RectTransform _actionChoiceTooltipTransform;

		// Token: 0x04001433 RID: 5171
		private RectTransform _currentContentHolder;

		// Token: 0x04001434 RID: 5172
		private float _currentPageHeight;

		// Token: 0x04001435 RID: 5173
		private float _previousTextIconGroupHeight;

		// Token: 0x04001436 RID: 5174
		private RectTransform _currentTextIconGroup;

		// Token: 0x04001437 RID: 5175
		private List<List<RectTransform>> _rectTransforms;

		// Token: 0x04001438 RID: 5176
		private int _currentPageIndex;

		// Token: 0x04001439 RID: 5177
		private bool _choiceWasRendered;
	}
}
