using System;
using RG.Parsecs.Loading;
using RG.Parsecs.Survival;
using UnityEngine;
using UnityEngine.UI;

namespace RG.SecondsRemaster.Loading
{
	// Token: 0x020002BE RID: 702
	public class DisplayLoading : MonoBehaviour
	{
		// Token: 0x060018E5 RID: 6373 RVA: 0x0006CEA3 File Offset: 0x0006B0A3
		private void Start()
		{
			if (MissionManager.Instance.IsMissionOngoing() && this._missionManagerData.CurrentMission.Poster != null)
			{
				this.SetMissionPoster();
				return;
			}
			this.SetRandomPoster();
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0006CED6 File Offset: 0x0006B0D6
		private void SetBackgroundColors(LoadingScreen screen)
		{
			this.SetColorToImages(this._backgroundImages, screen.BackgroundColor);
			this.SetColorToImages(this._spikesImages, screen.SpikesColor);
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0006CEFC File Offset: 0x0006B0FC
		private void SetColorToImages(Image[] images, Color color)
		{
			if (images == null)
			{
				return;
			}
			for (int i = 0; i < images.Length; i++)
			{
				if (images[i] != null)
				{
					images[i].color = color;
				}
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x0006CF30 File Offset: 0x0006B130
		private void SetRandomPoster()
		{
			int num = Random.Range(0, this._loadingScreens.Posters.Length);
			LoadingScreen loadingScreen = this._loadingScreens.Posters[num] as LoadingScreen;
			if (loadingScreen == null)
			{
				return;
			}
			this._loadingScreenController.SetLoadingScreen(loadingScreen);
			this.SetBackgroundColors(loadingScreen);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0006CF84 File Offset: 0x0006B184
		private void SetMissionPoster()
		{
			LoadingScreen loadingScreen = (this._missionManagerData.CurrentMission.Poster != null) ? (this._missionManagerData.CurrentMission.Poster as LoadingScreen) : null;
			if (loadingScreen == null)
			{
				return;
			}
			this._loadingScreenController.SetLoadingScreen(loadingScreen);
			this.SetBackgroundColors(loadingScreen);
		}

		// Token: 0x040012AF RID: 4783
		[SerializeField]
		private PosterList _loadingScreens;

		// Token: 0x040012B0 RID: 4784
		[SerializeField]
		private LoadingController _loadingScreenController;

		// Token: 0x040012B1 RID: 4785
		[SerializeField]
		private MissionManagerData _missionManagerData;

		// Token: 0x040012B2 RID: 4786
		[SerializeField]
		private Image[] _backgroundImages;

		// Token: 0x040012B3 RID: 4787
		[SerializeField]
		private Image[] _spikesImages;
	}
}
