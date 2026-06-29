using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001E RID: 30
[dfCategory("Basic Controls")]
[dfTooltip("Downloads an image from a web URL and displays it on-screen like a sprite")]
[dfHelp("http://www.daikonforge.com/docs/df-gui/classdf_web_sprite.html")]
[ExecuteInEditMode]
[AddComponentMenu("Daikon Forge/User Interface/Sprite/Web")]
[Serializable]
public class dfWebSprite : dfTextureSprite
{
	// Token: 0x17000123 RID: 291
	// (get) Token: 0x060004EF RID: 1263 RVA: 0x000193E9 File Offset: 0x000175E9
	// (set) Token: 0x060004F0 RID: 1264 RVA: 0x000193F1 File Offset: 0x000175F1
	public string URL
	{
		get
		{
			return this.url;
		}
		set
		{
			if (value != this.url)
			{
				this.url = value;
				if (Application.isPlaying && this.AutoDownload)
				{
					this.LoadImage();
				}
			}
		}
	}

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001941D File Offset: 0x0001761D
	// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00019425 File Offset: 0x00017625
	public Texture2D LoadingImage
	{
		get
		{
			return this.loadingImage;
		}
		set
		{
			this.loadingImage = value;
		}
	}

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0001942E File Offset: 0x0001762E
	// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00019436 File Offset: 0x00017636
	public Texture2D ErrorImage
	{
		get
		{
			return this.errorImage;
		}
		set
		{
			this.errorImage = value;
		}
	}

	// Token: 0x17000126 RID: 294
	// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0001943F File Offset: 0x0001763F
	// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00019447 File Offset: 0x00017647
	public bool AutoDownload
	{
		get
		{
			return this.autoDownload;
		}
		set
		{
			this.autoDownload = value;
		}
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00019450 File Offset: 0x00017650
	public override void OnEnable()
	{
		base.OnEnable();
		if (base.Texture == null)
		{
			base.Texture = this.LoadingImage;
		}
		if (base.Texture == null && this.AutoDownload && Application.isPlaying)
		{
			this.LoadImage();
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000194A0 File Offset: 0x000176A0
	public void LoadImage()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.downloadTexture());
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000194B5 File Offset: 0x000176B5
	private IEnumerator downloadTexture()
	{
		base.Texture = this.loadingImage;
		if (string.IsNullOrEmpty(this.url))
		{
			yield break;
		}
		using (WWW request = new WWW(this.url))
		{
			yield return request;
			if (!string.IsNullOrEmpty(request.error))
			{
				base.Texture = (this.errorImage ?? this.loadingImage);
				if (this.DownloadError != null)
				{
					this.DownloadError(this, request.error);
				}
				base.Signal("OnDownloadError", this, request.error);
			}
			else
			{
				base.Texture = request.texture;
				if (this.DownloadComplete != null)
				{
					this.DownloadComplete(this, base.Texture);
				}
				base.Signal("OnDownloadComplete", this, base.Texture);
			}
		}
		WWW request = null;
		yield break;
		yield break;
	}

	// Token: 0x040001AD RID: 429
	public PropertyChangedEventHandler<Texture> DownloadComplete;

	// Token: 0x040001AE RID: 430
	public PropertyChangedEventHandler<string> DownloadError;

	// Token: 0x040001AF RID: 431
	[SerializeField]
	protected string url = "";

	// Token: 0x040001B0 RID: 432
	[SerializeField]
	protected Texture2D loadingImage;

	// Token: 0x040001B1 RID: 433
	[SerializeField]
	protected Texture2D errorImage;

	// Token: 0x040001B2 RID: 434
	[SerializeField]
	protected bool autoDownload = true;
}
