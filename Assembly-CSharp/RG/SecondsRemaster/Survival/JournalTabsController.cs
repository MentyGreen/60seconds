using System;
using UnityEngine;

namespace RG.SecondsRemaster.Survival
{
	// Token: 0x02000333 RID: 819
	public class JournalTabsController : MonoBehaviour
	{
		// Token: 0x06001B6D RID: 7021 RVA: 0x000763AF File Offset: 0x000745AF
		private void Awake()
		{
			this.SetCoverAsLastSibling();
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000763B7 File Offset: 0x000745B7
		private void SetCoverAsLastSibling()
		{
			this._coverObject.SetAsLastSibling();
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x000763C4 File Offset: 0x000745C4
		public bool IsThisTabCurrentTab(JournalTabController tab)
		{
			return this._currentTab == tab;
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x000763D4 File Offset: 0x000745D4
		public void SetTabActive(JournalTabController tab)
		{
			if (this._currentTab != null)
			{
				this._currentTab.transform.SetAsFirstSibling();
				this._currentTab.SetActiveAnimationParameter(false);
			}
			tab.transform.SetSiblingIndex(this._coverObject.GetSiblingIndex() + 1);
			tab.SetActiveAnimationParameter(true);
			this._currentTab = tab;
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00076434 File Offset: 0x00074634
		public void RefreshAllTabs()
		{
			for (int i = 0; i < this._tabs.Length; i++)
			{
				this._tabs[i].RefreshTab();
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00076464 File Offset: 0x00074664
		public void ResetFirstEnabledPage()
		{
			for (int i = 0; i < this._tabs.Length; i++)
			{
				this._tabs[i].ResetFirstEnabledPage();
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00076491 File Offset: 0x00074691
		public JournalTabController CurrentTab
		{
			get
			{
				return this._currentTab;
			}
		}

		// Token: 0x04001533 RID: 5427
		[SerializeField]
		private JournalTabController[] _tabs;

		// Token: 0x04001534 RID: 5428
		[SerializeField]
		private Transform _coverObject;

		// Token: 0x04001535 RID: 5429
		private JournalTabController _currentTab;
	}
}
