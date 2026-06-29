using System;
using RG.Core.SaveSystem;
using RG.Parsecs.Scavenge;
using RG.SecondsRemaster.EventEditor;
using Twitter;
using UnityEngine;

// Token: 0x0200014B RID: 331
public class RemasterTwitterManager : MonoBehaviour
{
	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x000411DD File Offset: 0x0003F3DD
	// (set) Token: 0x06000FB1 RID: 4017 RVA: 0x000411E5 File Offset: 0x0003F3E5
	public bool IsLoggedIn { get; protected set; }

	// Token: 0x06000FB2 RID: 4018 RVA: 0x000411EE File Offset: 0x0003F3EE
	private void Start()
	{
		this.LoadTwitterUserInfo();
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000411F6 File Offset: 0x0003F3F6
	public void LogIn()
	{
		base.StartCoroutine(API.GetRequestToken(this.CONSUMER_KEY, this.CONSUMER_SECRET, new RequestTokenCallback(this.OnRequestTokenCallback)));
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x0004121C File Offset: 0x0003F41C
	public void EnterPin(string pin)
	{
		base.StartCoroutine(API.GetAccessToken(this.CONSUMER_KEY, this.CONSUMER_SECRET, this.m_RequestTokenResponse.Token, pin, new AccessTokenCallback(this.OnAccessTokenCallback)));
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0004124E File Offset: 0x0003F44E
	public void PostTweet(string text)
	{
		base.StartCoroutine(API.PostTweet(text, this.CONSUMER_KEY, this.CONSUMER_SECRET, this.m_AccessTokenResponse, new PostTweetCallback(this.OnPostTweet)));
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0004127B File Offset: 0x0003F47B
	public void PostTweetGIF(string text, string path)
	{
		base.StartCoroutine(API.PostGIFTweet(path, text, this.CONSUMER_KEY, this.CONSUMER_SECRET, this.m_AccessTokenResponse, new PostTweetCallback(this.OnPostTweet)));
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x000412A9 File Offset: 0x0003F4A9
	public API.GIFTweet PrepareTweetGIF(string path)
	{
		return API.PrepareGIFTweet(path, this.CONSUMER_KEY, this.CONSUMER_SECRET, this.m_AccessTokenResponse, new PostTweetCallback(this.OnPostTweet));
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x000412D0 File Offset: 0x0003F4D0
	private void LoadTwitterUserInfo()
	{
		this.m_AccessTokenResponse = new AccessTokenResponse();
		this.m_AccessTokenResponse.UserId = this._twitterUserId.Value;
		this.m_AccessTokenResponse.ScreenName = this._twitterUserScreenName.Value;
		this.m_AccessTokenResponse.Token = this._twitterUserToken.Value;
		this.m_AccessTokenResponse.TokenSecret = this._twitterUserTokenSecret.Value;
		if (!string.IsNullOrEmpty(this.m_AccessTokenResponse.Token) && !string.IsNullOrEmpty(this.m_AccessTokenResponse.ScreenName) && !string.IsNullOrEmpty(this.m_AccessTokenResponse.Token) && !string.IsNullOrEmpty(this.m_AccessTokenResponse.TokenSecret))
		{
			MonoBehaviour.print("LoadTwitterUserInfo - succeeded");
			this.IsLoggedIn = true;
		}
	}

	// Token: 0x06000FB9 RID: 4025 RVA: 0x00041399 File Offset: 0x0003F599
	private void OnRequestTokenCallback(bool success, RequestTokenResponse response)
	{
		if (success)
		{
			MonoBehaviour.print("OnRequestTokenCallback - succeeded");
			this.m_RequestTokenResponse = response;
			this.OnShowPINFieldRequested.Invoke();
			API.OpenAuthorizationPage(response.Token);
			return;
		}
		MonoBehaviour.print("OnRequestTokenCallback - failed.");
	}

	// Token: 0x06000FBA RID: 4026 RVA: 0x000413D0 File Offset: 0x0003F5D0
	private void OnAccessTokenCallback(bool success, AccessTokenResponse response)
	{
		if (success)
		{
			MonoBehaviour.print("OnAccessTokenCallback - succeeded");
			this.m_AccessTokenResponse = response;
			this._twitterUserId.Value = response.UserId;
			this._twitterUserScreenName.Value = response.ScreenName;
			this._twitterUserToken.Value = response.Token;
			this._twitterUserTokenSecret.Value = response.TokenSecret;
			if (StorageDataManager.TheInstance != null)
			{
				StorageDataManager.TheInstance.Save("settings", delegate()
				{
					Debug.Log("done");
				}, null, true, false);
			}
			this.IsLoggedIn = true;
		}
		else
		{
			MonoBehaviour.print("OnAccessTokenCallback - failed.");
		}
		this.OnAccessTokenResponse.Invoke(success);
	}

	// Token: 0x06000FBB RID: 4027 RVA: 0x00041498 File Offset: 0x0003F698
	private void OnPostTweet(bool success, int code)
	{
		if (!success)
		{
			if (code <= 89)
			{
				if (code <= 50)
				{
					if (code != 32 && code != 50)
					{
						goto IL_6E;
					}
				}
				else if (code - 63 > 1)
				{
					switch (code)
					{
					case 87:
					case 89:
						break;
					case 88:
						goto IL_6E;
					default:
						goto IL_6E;
					}
				}
			}
			else if (code <= 130)
			{
				if (code != 99)
				{
					if (code != 130)
					{
						goto IL_6E;
					}
					goto IL_6E;
				}
			}
			else if (code == 185 || code == 187 || code != 215)
			{
				goto IL_6E;
			}
			this.UnlinkTwitter();
		}
		IL_6E:
		MonoBehaviour.print("OnPostTweet - " + (success ? "succedded." : "failed."));
	}

	// Token: 0x06000FBC RID: 4028 RVA: 0x00041534 File Offset: 0x0003F734
	private void UnlinkTwitter()
	{
		this.IsLoggedIn = false;
		this.m_AccessTokenResponse = new AccessTokenResponse();
		this._twitterUserId.Value = string.Empty;
		this._twitterUserScreenName.Value = string.Empty;
		this._twitterUserToken.Value = string.Empty;
		this._twitterUserTokenSecret.Value = string.Empty;
		if (StorageDataManager.TheInstance != null)
		{
			StorageDataManager.TheInstance.Save("settings", delegate()
			{
				Debug.Log("done");
			}, null, true, false);
		}
	}

	// Token: 0x040009AB RID: 2475
	[SerializeField]
	private GlobalStringVariable _twitterUserId;

	// Token: 0x040009AC RID: 2476
	[SerializeField]
	private GlobalStringVariable _twitterUserScreenName;

	// Token: 0x040009AD RID: 2477
	[SerializeField]
	private GlobalStringVariable _twitterUserToken;

	// Token: 0x040009AE RID: 2478
	[SerializeField]
	private GlobalStringVariable _twitterUserTokenSecret;

	// Token: 0x040009AF RID: 2479
	public GenericEvent OnShowPINFieldRequested = new GenericEvent();

	// Token: 0x040009B0 RID: 2480
	public GenericEvent<bool> OnAccessTokenResponse = new GenericEvent<bool>();

	// Token: 0x040009B1 RID: 2481
	public string CONSUMER_KEY = "ZTIGErlLad0vDNU3FywuuAUh0";

	// Token: 0x040009B2 RID: 2482
	public string CONSUMER_SECRET = "jhrOOy3p3X9PaL1Ocuqo1I1D3xOousXwa5hyiudIYa2vleL51r ";

	// Token: 0x040009B3 RID: 2483
	private const string PLAYER_PREFS_TWITTER_USER_ID = "TwitterUserID";

	// Token: 0x040009B4 RID: 2484
	private const string PLAYER_PREFS_TWITTER_USER_SCREEN_NAME = "TwitterUserScreenName";

	// Token: 0x040009B5 RID: 2485
	private const string PLAYER_PREFS_TWITTER_USER_TOKEN = "TwitterUserToken";

	// Token: 0x040009B6 RID: 2486
	private const string PLAYER_PREFS_TWITTER_USER_TOKEN_SECRET = "TwitterUserTokenSecret";

	// Token: 0x040009B7 RID: 2487
	private RequestTokenResponse m_RequestTokenResponse;

	// Token: 0x040009B8 RID: 2488
	private AccessTokenResponse m_AccessTokenResponse;

	// Token: 0x020003CD RID: 973
	private enum TwitterErrorCodes
	{
		// Token: 0x040017B4 RID: 6068
		CouldNotAuthenticate = 32,
		// Token: 0x040017B5 RID: 6069
		UserNotFound = 50,
		// Token: 0x040017B6 RID: 6070
		UserSuspended = 63,
		// Token: 0x040017B7 RID: 6071
		UserSuspendedAndNotPermitted,
		// Token: 0x040017B8 RID: 6072
		ClientNotPermittedThisAction = 87,
		// Token: 0x040017B9 RID: 6073
		InvalidOrExpiredToken = 89,
		// Token: 0x040017BA RID: 6074
		UnableToVerifyCredentials = 99,
		// Token: 0x040017BB RID: 6075
		BadAuthenticationData = 215,
		// Token: 0x040017BC RID: 6076
		RateLimit = 88,
		// Token: 0x040017BD RID: 6077
		TwitterIsOverCapacity = 130,
		// Token: 0x040017BE RID: 6078
		PostingLimitationReached = 185,
		// Token: 0x040017BF RID: 6079
		StatusIsDuplicate = 187
	}
}
