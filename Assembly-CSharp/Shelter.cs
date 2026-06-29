using System;
using System.Collections;
using RG.Parsecs.Common;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class Shelter : MonoBehaviour
{
	// Token: 0x06000EA7 RID: 3751 RVA: 0x0003CF10 File Offset: 0x0003B110
	private void Start()
	{
		if (this._rangeMarkerPositioner != null && this._rangeMarkerTemplate != null)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this._rangeMarkerTemplate);
			this._rangeMarker = gameObject.transform;
			this._rangeMarker.transform.parent = this._rangeMarkerPositioner.transform.parent;
			this._rangeMarker.position = this._rangeMarkerPositioner.transform.position;
			this._rangeMarker.rotation = this._rangeMarkerPositioner.transform.rotation;
			Object.Destroy(this._rangeMarkerPositioner);
		}
		this._playerTransform = GlobalTools.GetPlayer().transform;
		this._cameraTransform = Camera.main.transform;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0003CFD5 File Offset: 0x0003B1D5
	public void OpenHatch(bool open, float time)
	{
		iTween.RotateBy(this._hatch.gameObject, new Vector3(open ? (-this._swingFactor) : this._swingFactor, 0f, 0f), time);
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0003D009 File Offset: 0x0003B209
	public void SetGuider()
	{
		this._guider = GameObject.Find("pointer");
		this._guiderTransform = this._guider.transform;
		this.ShowGuider(false);
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0003D033 File Offset: 0x0003B233
	public void ShowGuider(bool show)
	{
		if (this._guider != null)
		{
			this._guider.SetActive(show);
		}
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0003D050 File Offset: 0x0003B250
	public void ShowRange(bool show)
	{
		if (this._rangeMarker != null)
		{
			this._rangeMarker.GetComponent<Renderer>().material.color = (show ? this._rangeMarkerFlashColorHigh : this._rangeMarkerFlashColorLow);
			if (!show)
			{
				iTween.Stop(base.gameObject);
			}
		}
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
	public void Flash()
	{
		if (this._guider != null)
		{
			this.ShowGuider(true);
			Hashtable hashtable = new Hashtable();
			hashtable.Add("amount", this._guiderScaleDelta);
			hashtable.Add("time", this._guiderLoopTime);
			hashtable.Add("easetype", iTween.EaseType.spring);
			hashtable.Add("looptype", iTween.LoopType.pingPong);
			iTween.ScaleBy(this._guider.gameObject, hashtable);
		}
		Hashtable hashtable2 = new Hashtable();
		hashtable2.Add("from", this._rangeMarkerFlashColorLow);
		hashtable2.Add("to", this._rangeMarkerFlashColorHigh);
		hashtable2.Add("time", this._rangeMarkerFlashTime);
		hashtable2.Add("looptype", iTween.LoopType.pingPong);
		hashtable2.Add("onupdate", "OnRangeUpdated");
		iTween.ValueTo(base.gameObject, hashtable2);
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0003D19D File Offset: 0x0003B39D
	private void OnRangeUpdated(Color color)
	{
		this._rangeMarker.GetComponent<Renderer>().material.color = color;
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
	private void Update()
	{
		if (this._guider != null && this._guider.activeSelf)
		{
			Vector3 forward = this._cameraTransform.position - this._guiderTransform.position;
			forward.y = 0f;
			Quaternion rotation = Quaternion.LookRotation(forward);
			this._guiderTransform.rotation = rotation;
		}
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x0003D21B File Offset: 0x0003B41B
	public void DropIntoShelter(string dropSound)
	{
		base.GetComponent<Effector>().Activate();
		AudioManager.PlaySound(dropSound, 1f, 1f, 0f);
	}

	// Token: 0x040008D3 RID: 2259
	[SerializeField]
	private Transform _hatch;

	// Token: 0x040008D4 RID: 2260
	[SerializeField]
	private float _guiderLoopTime = 1f;

	// Token: 0x040008D5 RID: 2261
	[SerializeField]
	private Vector3 _guiderScaleDelta = Vector3.zero;

	// Token: 0x040008D6 RID: 2262
	[SerializeField]
	private GameObject _rangeMarkerPositioner;

	// Token: 0x040008D7 RID: 2263
	[SerializeField]
	private GameObject _rangeMarkerTemplate;

	// Token: 0x040008D8 RID: 2264
	[SerializeField]
	private float _rangeMarkerFlashTime = 2f;

	// Token: 0x040008D9 RID: 2265
	[SerializeField]
	private Color _rangeMarkerFlashColorLow = new Color(255f, 255f, 255f, 0f);

	// Token: 0x040008DA RID: 2266
	[SerializeField]
	private Color _rangeMarkerFlashColorHigh = new Color(255f, 255f, 255f, 100f);

	// Token: 0x040008DB RID: 2267
	[SerializeField]
	private float _swingFactor = 0.275f;

	// Token: 0x040008DC RID: 2268
	private Transform _rangeMarker;

	// Token: 0x040008DD RID: 2269
	private GameObject _guider;

	// Token: 0x040008DE RID: 2270
	private Transform _playerTransform;

	// Token: 0x040008DF RID: 2271
	private Transform _cameraTransform;

	// Token: 0x040008E0 RID: 2272
	private Transform _guiderTransform;
}
