using System;
using RG.Parsecs.Common;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x020002E3 RID: 739
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(LayoutElement))]
	public abstract class JournalContentDisplayer : MonoBehaviour
	{
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0006FAF0 File Offset: 0x0006DCF0
		public virtual int LinesAmount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0006FAF3 File Offset: 0x0006DCF3
		public RectTransform RectTransform
		{
			get
			{
				if (this._rectTransform == null)
				{
					this._rectTransform = base.GetComponent<RectTransform>();
				}
				return this._rectTransform;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0006FB15 File Offset: 0x0006DD15
		public LayoutElement LayoutElement
		{
			get
			{
				if (this._layoutElement == null)
				{
					this._layoutElement = base.GetComponent<LayoutElement>();
				}
				return this._layoutElement;
			}
		}

		// Token: 0x060019BD RID: 6589
		public abstract void SetContentData(JournalContent content);

		// Token: 0x060019BE RID: 6590 RVA: 0x0006FB38 File Offset: 0x0006DD38
		public void SetTooltipController(RectTransform tooltipTransform)
		{
			if (this._tooltipControllers == null || tooltipTransform == null)
			{
				return;
			}
			for (int i = 0; i < this._tooltipControllers.Length; i++)
			{
				this._tooltipControllers[i].SetTooltipRect(tooltipTransform);
			}
		}

		// Token: 0x040013A9 RID: 5033
		[SerializeField]
		private TooltipController[] _tooltipControllers;

		// Token: 0x040013AA RID: 5034
		public const int NO_LINES_VALUE = -1;

		// Token: 0x040013AB RID: 5035
		private RectTransform _rectTransform;

		// Token: 0x040013AC RID: 5036
		private LayoutElement _layoutElement;
	}
}
