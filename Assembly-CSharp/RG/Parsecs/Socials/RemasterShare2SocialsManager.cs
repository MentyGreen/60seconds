using System;
using System.Collections;
using Rewired;
using RG.Parsecs.Common;
using Twitter;
using UnityEngine;
using UnityToolbag;

namespace RG.Parsecs.Socials
{
	// Token: 0x0200020F RID: 527
	public class RemasterShare2SocialsManager : MonoBehaviour
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x0005BBE0 File Offset: 0x00059DE0
		private void Start()
		{
			this._player = ReInput.players.GetPlayer(0);
			if (this.GrabRecorderDataSystem.Ready)
			{
				this.GifPreviewWindow.FacebookButtonInteractable = true;
				this.GifPreviewWindow.TwitterButtonInteractable = true;
				this.GifPreviewWindow.SaveButtonInteractable = true;
				this.GifPreviewWindow.SetFrames(this.GrabRecorderDataSystem.Frames);
				return;
			}
			this.GifPreviewWindow.FacebookButtonInteractable = false;
			this.GifPreviewWindow.TwitterButtonInteractable = false;
			this.GifPreviewWindow.SaveButtonInteractable = false;
			this.GrabRecorderDataSystem.OnReady.Add(new Action(this.OnReady));
			this.GrabRecorderDataSystem.OnProgressChanged.Add(new Action<float>(this.OnProgressChanged));
			this.GifPreviewWindow.SetProgress(this.GrabRecorderDataSystem.Progress);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005BCB8 File Offset: 0x00059EB8
		public void Update()
		{
			if (this._player == null)
			{
				return;
			}
			if (this._player.GetButtonDown(30))
			{
				this.Hide();
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0005BCD8 File Offset: 0x00059ED8
		private void OnProgressChanged(float p)
		{
			Dispatcher.InvokeAsync(delegate
			{
				this.GifPreviewWindow.SetProgress(p);
			});
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005BCFD File Offset: 0x00059EFD
		private void OnReady()
		{
			Dispatcher.InvokeAsync(delegate
			{
				this.GifPreviewWindow.FacebookButtonInteractable = true;
				this.GifPreviewWindow.TwitterButtonInteractable = true;
				this.GifPreviewWindow.SaveButtonInteractable = true;
				this.GifPreviewWindow.SetProgress(1f);
				this.GifPreviewWindow.SetFrames(this.GrabRecorderDataSystem.Frames);
			});
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005BD10 File Offset: 0x00059F10
		private void SetUrl(string url)
		{
			this._url = url;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005BD19 File Offset: 0x00059F19
		public void ShareAdventure()
		{
			this.GifPreviewWindow.Show();
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005BD26 File Offset: 0x00059F26
		public void Hide()
		{
			this.GifPreviewWindow.Hide();
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005BD33 File Offset: 0x00059F33
		private void Imgur_OnImageUploadProgress(object sender, ImgurClient.OnImageUploadProgressEventArgs e)
		{
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005BD35 File Offset: 0x00059F35
		private void Imgur_OnImageUploaded(object sender, ImgurClient.OnImageUploadedEventArgs e)
		{
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005BD38 File Offset: 0x00059F38
		public void ShareToFacebook()
		{
			if (this._facebookPromise != null)
			{
				return;
			}
			this._facebookPromise = new SharePromise();
			this._facebookPromise.OnFailure.AddOneTime(delegate(SharePromise p)
			{
				this._facebookPromise = null;
				this.GifPreviewWindow.FacebookSlider.value = 0f;
			});
			this._facebookPromise.OnSuccess.AddOneTime(delegate(SharePromise p)
			{
				if (this.SpaceSelfieAchievement != null)
				{
					AchievementsSystem.UnlockAchievement(this.SpaceSelfieAchievement);
				}
			});
			ImgurClient imgurClient;
			if (this._useDefaultImgurTitle)
			{
				imgurClient = new ImgurClient("5907300c80d376b", "tHeuZVItCgmsh7IdhrliwZfOlA4Dp1PyR6FjsnnIk71hzAsHUT");
			}
			else
			{
				imgurClient = new ImgurClient("5907300c80d376b", "tHeuZVItCgmsh7IdhrliwZfOlA4Dp1PyR6FjsnnIk71hzAsHUT", this._customImgurTitle);
			}
			imgurClient.OnImageUploaded += delegate(object sender, ImgurClient.OnImageUploadedEventArgs e)
			{
				if (!e.success)
				{
					Dispatcher.InvokeAsync(delegate
					{
						this._facebookPromise.Failure();
					});
					return;
				}
				if (this._useDefaultFbAppId)
				{
					Dispatcher.InvokeAsync(delegate
					{
						Facebook.Share(e.response.data.link, "Name", "Caption", "Description", e.response.data.link, "http://www.facebook.com/");
						this.GifPreviewWindow.FacebookSlider.value = (this._facebookPromise.Progress = 1f);
						this._facebookPromise.Success();
					});
					return;
				}
				Dispatcher.InvokeAsync(delegate
				{
					Facebook.Share(this._customFbAppId, e.response.data.link, "Name", "Caption", "Description", e.response.data.link, "http://www.facebook.com/");
					this.GifPreviewWindow.FacebookSlider.value = (this._facebookPromise.Progress = 1f);
					this._facebookPromise.Success();
				});
			};
			imgurClient.OnImageUploadProgress += delegate(object sender, ImgurClient.OnImageUploadProgressEventArgs e)
			{
				Dispatcher.InvokeAsync(delegate
				{
					if (e.progress > this._facebookPromise.Progress)
					{
						this._facebookPromise.Progress = e.progress;
						this.GifPreviewWindow.FacebookSlider.value = e.progress;
					}
				});
			};
			this.GifPreviewWindow.FacebookSlider.value = (this._facebookPromise.Progress = 0.05f);
			if (!imgurClient.UploadImageFromFilePath(this.GrabRecorderDataSystem.Path))
			{
				this._facebookPromise.Failure();
			}
			base.StartCoroutine(this._facebookFakeProgress(this._facebookPromise));
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0005BE39 File Offset: 0x0005A039
		private IEnumerator _facebookFakeProgress(SharePromise promise)
		{
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.5f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.06666667f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.FacebookSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.7f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.033333335f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.FacebookSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.8f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.02f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.FacebookSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished && promise.Progress < 0.9f)
			{
				promise.Progress += Time.deltaTime * 0.0125f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.FacebookSlider.value = promise.Progress;
				yield return null;
			}
			yield break;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005BE50 File Offset: 0x0005A050
		private void WaitForLogIn(SharePromise promise)
		{
			this.TwitterManager.OnShowPINFieldRequested.AddOneTime(new Action(this.ShowTwitterPinMenu));
			this.TwitterManager.OnAccessTokenResponse.AddOneTime(new Action<bool>(promise.Resolve));
			this.TwitterManager.LogIn();
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0005BEA0 File Offset: 0x0005A0A0
		private void ShowTwitterPinMenu()
		{
			this.GifPreviewWindow.ShowTwitterPinMenu();
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005BEAD File Offset: 0x0005A0AD
		public void PinCancelled()
		{
			if (this.logInPromise != null)
			{
				this.logInPromise.Failure();
			}
			this.GifPreviewWindow.HideTwitterPinMenu();
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0005BECD File Offset: 0x0005A0CD
		public void PinConfirmed()
		{
			this.TwitterManager.EnterPin(this.GifPreviewWindow.TwitterPin.text);
			this.GifPreviewWindow.HideTwitterPinMenu();
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005BEF5 File Offset: 0x0005A0F5
		private void ShowTwitterTextMenu()
		{
			this.GifPreviewWindow.ShowTwitterTextMenu();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0005BF04 File Offset: 0x0005A104
		public void NotifyTextIsReady()
		{
			this._text = this.GifPreviewWindow.TweetText.text;
			if (string.IsNullOrEmpty(this._text))
			{
				this._text = this.DefaultTweet;
			}
			if (this._textIsReady != null)
			{
				this._textIsReady(true);
			}
			this.GifPreviewWindow.HideTwitterTextMenu();
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005BF5F File Offset: 0x0005A15F
		public void TextCancelled()
		{
			if (this._textIsReady != null)
			{
				this._textIsReady(false);
			}
			this.GifPreviewWindow.HideTwitterTextMenu();
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005BF80 File Offset: 0x0005A180
		private IEnumerator WaitForUpload(SharePromise promise)
		{
			this._gifTweet = this.TwitterManager.PrepareTweetGIF(this.GrabRecorderDataSystem.Path);
			this._gifTweet.OnUploadProgressChanged.Add(delegate(float p)
			{
				if (p > promise.Progress)
				{
					promise.Progress = p;
					this.GifPreviewWindow.TwitterSlider.value = p;
				}
			});
			base.StartCoroutine(this._twitterFakeProgress(promise));
			this._gifTweet.OnUploadFinished.AddOneTime(new Action<bool>(promise.Resolve));
			yield return base.StartCoroutine(this._gifTweet.Upload());
			yield break;
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005BF96 File Offset: 0x0005A196
		private IEnumerator _twitterFakeProgress(SharePromise promise)
		{
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.5f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.1f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.TwitterSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.7f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.05f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.TwitterSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished)
			{
				if (promise.Progress >= 0.8f)
				{
					break;
				}
				promise.Progress += Time.deltaTime * 0.025f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.TwitterSlider.value = promise.Progress;
				yield return null;
			}
			while (promise != null && !promise.IsFinished && promise.Progress < 0.9f)
			{
				promise.Progress += Time.deltaTime * 0.0125f;
				promise.Progress = Mathf.Min(promise.Progress, 1f);
				this.GifPreviewWindow.TwitterSlider.value = promise.Progress;
				yield return null;
			}
			yield break;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005BFAC File Offset: 0x0005A1AC
		private void WaitForText(SharePromise promise)
		{
			this._textIsReady = new Action<bool>(promise.Resolve);
			this.ShowTwitterTextMenu();
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005BFC6 File Offset: 0x0005A1C6
		private void Tweet(SharePromise promise)
		{
			this._gifTweet.OnTweetPosted.AddOneTime(new Action<bool>(promise.Resolve));
			base.StartCoroutine(this._gifTweet.Tweet(this._text));
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005BFFC File Offset: 0x0005A1FC
		public void ShareToTwitter()
		{
			if (this._shareToTwitter != null)
			{
				return;
			}
			this._shareToTwitter = new SharePromise(new Action<SharePromise>(this.Tweet));
			this._shareToTwitter.OnSuccess.AddOneTime(delegate(SharePromise p)
			{
				this.GifPreviewWindow.TwitterSlider.value = 1f;
				if (this.SpaceSelfieAchievement != null)
				{
					AchievementsSystem.UnlockAchievement(this.SpaceSelfieAchievement);
				}
			});
			this._shareToTwitter.OnFailure.AddOneTime(delegate(SharePromise p)
			{
				this._shareToTwitter = null;
				this.GifPreviewWindow.TwitterSlider.value = 0f;
				this.GifPreviewWindow.HideTwitterTextMenu();
				this.GifPreviewWindow.HideTwitterPinMenu();
			});
			SharePromise uploadPromise = new SharePromise(new Func<SharePromise, IEnumerator>(this.WaitForUpload), this);
			this._shareToTwitter.Require(uploadPromise);
			SharePromise sharePromise = new SharePromise(new Action<SharePromise>(this.WaitForText));
			this._shareToTwitter.Require(sharePromise);
			sharePromise.OnFailure.AddOneTime(delegate(SharePromise p)
			{
				uploadPromise.Failure();
			});
			uploadPromise.OnFailure.AddOneTime(delegate(SharePromise p)
			{
				this.GifPreviewWindow.HideTwitterTextMenu();
			});
			if (!this.TwitterManager.IsLoggedIn)
			{
				this.logInPromise = new SharePromise(new Action<SharePromise>(this.WaitForLogIn));
				uploadPromise.Require(this.logInPromise);
				sharePromise.Require(this.logInPromise);
				this.logInPromise.Process();
				return;
			}
			uploadPromise.Process();
			sharePromise.Process();
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0005C146 File Offset: 0x0005A346
		public void Save()
		{
			if (!this._saved)
			{
				base.StartCoroutine(this._SaveFakeProgress());
			}
			this._saved = true;
			OpenFileLocation.Open(this.GrabRecorderDataSystem.Path);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005C174 File Offset: 0x0005A374
		private IEnumerator _SaveFakeProgress()
		{
			float progress = 0f;
			while (progress < 1f)
			{
				progress += Time.deltaTime * 3f;
				progress = Mathf.Min(progress, 1f);
				this.GifPreviewWindow.SaveSlider.value = progress;
				yield return null;
			}
			yield break;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0005C183 File Offset: 0x0005A383
		private void OnDestroy()
		{
			if (!this._saved)
			{
				this.GrabRecorderDataSystem.DeleteGIF();
				return;
			}
			this.GrabRecorderDataSystem.Clear();
		}

		// Token: 0x04000D95 RID: 3477
		public GrabRecorderDataSystem GrabRecorderDataSystem;

		// Token: 0x04000D96 RID: 3478
		public RemasterTwitterManager TwitterManager;

		// Token: 0x04000D97 RID: 3479
		public GIFPreviewWindow GifPreviewWindow;

		// Token: 0x04000D98 RID: 3480
		public Achievement SpaceSelfieAchievement;

		// Token: 0x04000D99 RID: 3481
		[SerializeField]
		private bool _useDefaultFbAppId = true;

		// Token: 0x04000D9A RID: 3482
		[SerializeField]
		private string _customFbAppId;

		// Token: 0x04000D9B RID: 3483
		[SerializeField]
		private bool _useDefaultImgurTitle = true;

		// Token: 0x04000D9C RID: 3484
		[SerializeField]
		private string _customImgurTitle;

		// Token: 0x04000D9D RID: 3485
		public string DefaultTweet = "60 Parsecs!";

		// Token: 0x04000D9E RID: 3486
		private bool _saved;

		// Token: 0x04000D9F RID: 3487
		private Player _player;

		// Token: 0x04000DA0 RID: 3488
		private string _url;

		// Token: 0x04000DA1 RID: 3489
		private string _link;

		// Token: 0x04000DA2 RID: 3490
		private SharePromise _facebookPromise;

		// Token: 0x04000DA3 RID: 3491
		private API.GIFTweet _gifTweet;

		// Token: 0x04000DA4 RID: 3492
		private string _text;

		// Token: 0x04000DA5 RID: 3493
		private Action<bool> _textIsReady;

		// Token: 0x04000DA6 RID: 3494
		private SharePromise _shareToTwitter;

		// Token: 0x04000DA7 RID: 3495
		private SharePromise logInPromise;
	}
}
