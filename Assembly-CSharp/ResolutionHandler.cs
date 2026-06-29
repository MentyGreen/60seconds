using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000136 RID: 310
public class ResolutionHandler : MonoBehaviour
{
	// Token: 0x06000F28 RID: 3880 RVA: 0x0003E9DB File Offset: 0x0003CBDB
	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		this._nativeAspectRatio = this._nativeWidth / this._nativeHeight;
		this.LoadResolutions();
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0003EA01 File Offset: 0x0003CC01
	private void Start()
	{
	}

	// Token: 0x06000F2A RID: 3882 RVA: 0x0003EA04 File Offset: 0x0003CC04
	private void UpdateCamera()
	{
		this._2dCamera = null;
		this._3dCamera = null;
		if (Camera.main == null)
		{
			return;
		}
		if (Camera.main.orthographic)
		{
			this._2dCamera = Camera.main;
			return;
		}
		this._3dCamera = Camera.main;
		GameObject gameObject = GameObject.FindGameObjectWithTag("UICamera");
		if (gameObject != null)
		{
			this._2dCamera = gameObject.GetComponent<Camera>();
		}
	}

	// Token: 0x06000F2B RID: 3883 RVA: 0x0003EA70 File Offset: 0x0003CC70
	private void OnEnable()
	{
		SceneManager.sceneLoaded += this.OnLevelFinishedLoading;
	}

	// Token: 0x06000F2C RID: 3884 RVA: 0x0003EA83 File Offset: 0x0003CC83
	private void OnDisable()
	{
		SceneManager.sceneLoaded -= this.OnLevelFinishedLoading;
	}

	// Token: 0x06000F2D RID: 3885 RVA: 0x0003EA98 File Offset: 0x0003CC98
	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name.Contains("scavenge") || scene.name.Contains("challenge") || scene.name.Contains("tutorial"))
		{
			this.UpdateCamera();
			this.HandleResolution();
		}
	}

	// Token: 0x06000F2E RID: 3886 RVA: 0x0003EAEC File Offset: 0x0003CCEC
	private void LoadResolutions()
	{
		List<Resolution> list = new List<Resolution>();
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			if (this.FindResolution((float)Screen.resolutions[i].width, (float)Screen.resolutions[i].height))
			{
				list.Add(Screen.resolutions[i]);
			}
		}
		this._availableResolutions = list.ToArray();
	}

	// Token: 0x06000F2F RID: 3887 RVA: 0x0003EB58 File Offset: 0x0003CD58
	public void HandleResolution()
	{
		this.FindResolution((float)Settings.Data.ResX, (float)Settings.Data.ResY, out this._selectedAspectRatio);
		Camera camera = this._2dCamera;
		if (camera == null || (camera != null && !camera.orthographic))
		{
			GameObject gameObject = GameObject.FindGameObjectWithTag("UICamera");
			if (gameObject != null)
			{
				camera = gameObject.GetComponent<Camera>();
			}
		}
		if (camera != null)
		{
			camera.orthographicSize = this._selectedAspectRatio.LocalResolutionFactor;
			float y = ((float)Settings.Data.ResY - (float)Settings.Data.ResX / this._nativeWidth * this._nativeHeight) / 2f * this._selectedAspectRatio.CursorOffset.y;
			float x = ((float)Settings.Data.ResY - (float)Settings.Data.ResX / this._nativeWidth * this._nativeHeight) * this._selectedAspectRatio.CursorOffset.x;
			this._adjustedMousePositionFactor = new Vector2(x, y);
		}
	}

	// Token: 0x06000F30 RID: 3888 RVA: 0x0003EC64 File Offset: 0x0003CE64
	public bool FindResolution(float width, float height)
	{
		if (this._aspectRatios != null)
		{
			for (int i = 0; i < this._aspectRatios.Length; i++)
			{
				if (this._aspectRatios[i].Enabled && this._aspectRatios[i].FindResolution(width, height))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000F31 RID: 3889 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
	public bool FindResolution(float width, float height, out SResolutionAspectRatio data)
	{
		if (this._aspectRatios != null)
		{
			for (int i = 0; i < this._aspectRatios.Length; i++)
			{
				if (this._aspectRatios[i].Enabled && this._aspectRatios[i].FindResolution(width, height))
				{
					data = this._aspectRatios[i];
					return true;
				}
			}
		}
		data = SResolutionAspectRatio.EMPTY;
		return false;
	}

	// Token: 0x06000F32 RID: 3890 RVA: 0x0003ED28 File Offset: 0x0003CF28
	public static bool Is54(float val)
	{
		return Mathf.Approximately(val, 1.25f);
	}

	// Token: 0x06000F33 RID: 3891 RVA: 0x0003ED35 File Offset: 0x0003CF35
	public static bool Is1610(float val)
	{
		return Mathf.Approximately(val, 1.6f);
	}

	// Token: 0x06000F34 RID: 3892 RVA: 0x0003ED42 File Offset: 0x0003CF42
	public static bool Is169(float val)
	{
		return Mathf.Approximately(val, 1.77f);
	}

	// Token: 0x06000F35 RID: 3893 RVA: 0x0003ED4F File Offset: 0x0003CF4F
	public static bool Is43(float val)
	{
		return Mathf.Approximately(val, 1.33f) || Mathf.Approximately(val, 1.25f);
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0003ED6B File Offset: 0x0003CF6B
	public float ResizeRatio
	{
		get
		{
			return this._selectedAspectRatio.AspectRatio / this._nativeAspectRatio;
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003ED80 File Offset: 0x0003CF80
	public Vector2 AdjustedMousePosition
	{
		get
		{
			return new Vector2(Input.mousePosition.x * this._2dCamera.orthographicSize - this._adjustedMousePositionFactor.x, Input.mousePosition.y * this._2dCamera.orthographicSize - this._adjustedMousePositionFactor.y);
		}
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0003EDD6 File Offset: 0x0003CFD6
	public Resolution[] Resolutions
	{
		get
		{
			return this._availableResolutions;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0003EDDE File Offset: 0x0003CFDE
	public SResolutionAspectRatio SelectedAspectRatio
	{
		get
		{
			return this._selectedAspectRatio;
		}
	}

	// Token: 0x04000924 RID: 2340
	[SerializeField]
	private float _nativeWidth = 1920f;

	// Token: 0x04000925 RID: 2341
	[SerializeField]
	private float _nativeHeight = 1080f;

	// Token: 0x04000926 RID: 2342
	private float _nativeAspectRatio = 1f;

	// Token: 0x04000927 RID: 2343
	[SerializeField]
	private SResolutionAspectRatio[] _aspectRatios;

	// Token: 0x04000928 RID: 2344
	private Resolution[] _availableResolutions;

	// Token: 0x04000929 RID: 2345
	private SResolutionAspectRatio _selectedAspectRatio;

	// Token: 0x0400092A RID: 2346
	private Vector2 _adjustedMousePositionFactor = Vector2.zero;

	// Token: 0x0400092B RID: 2347
	private Camera _2dCamera;

	// Token: 0x0400092C RID: 2348
	private Camera _3dCamera;
}
