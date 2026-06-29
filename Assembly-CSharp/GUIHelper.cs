using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class GUIHelper
{
	// Token: 0x06000E1E RID: 3614 RVA: 0x0003AB84 File Offset: 0x00038D84
	public static void MakeObscurerVisible(bool visible)
	{
		dfPanel dfPanel = (GUIHelper.Obscurer != null) ? GUIHelper.Obscurer.GetComponent<dfPanel>() : null;
		if (dfPanel != null)
		{
			dfPanel.BackgroundColor = (visible ? new Color32(0, 0, 0, byte.MaxValue) : new Color32(0, 0, 0, 0));
			dfPanel.Opacity = (visible ? 255f : 0f);
		}
		dfSprite dfSprite = (GUIHelper.Obscurer != null) ? GUIHelper.Obscurer.GetComponent<dfSprite>() : null;
		if (dfSprite != null)
		{
			dfSprite.Color = (visible ? new Color32(0, 0, 0, byte.MaxValue) : new Color32(0, 0, 0, 0));
			dfSprite.Opacity = (visible ? 255f : 0f);
		}
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x0003AC48 File Offset: 0x00038E48
	private static Vector3 GetCircleFadeSize(bool max)
	{
		if (max)
		{
			ResolutionHandler resolutionHandler = Object.FindObjectOfType<ResolutionHandler>();
			float num = ((resolutionHandler == null) ? 1f : resolutionHandler.ResizeRatio) * 6.25f;
			return new Vector3(num, num, 1f);
		}
		return new Vector3(0f, 0f, 1f);
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x0003AC9A File Offset: 0x00038E9A
	public static IEnumerator DoCircleFade(bool fadeIn, float time, float initalDelay = 0f, bool destroyCircleFader = false, bool obscurerEndDeactivate = true, GUIHelper.Concluder concluder = null, bool concluderFlag = false)
	{
		if (!Mathf.Approximately(initalDelay, 0f))
		{
			yield return new WaitForSeconds(initalDelay);
		}
		if (GUIHelper.CircleFader == null)
		{
			GUIHelper.CircleFader = GameObject.Find("CircleFader");
			if (GUIHelper.CircleFader != null && GUIHelper.CircleFader.transform.parent != null)
			{
				GUIHelper.Obscurer = GUIHelper.CircleFader.transform.parent.gameObject;
			}
		}
		if (GUIHelper.CircleFader != null)
		{
			GUIHelper.CircleFader.GetComponent<Renderer>().enabled = true;
			float startVal = 0f;
			float endVal = 0f;
			if (fadeIn)
			{
				startVal = 0f;
				endVal = 1f;
			}
			else
			{
				startVal = 1f;
				endVal = 0f;
			}
			Material mat = GUIHelper.CircleFader.GetComponent<Renderer>().material;
			float endTime = Time.time + time;
			float currentTime = 0f;
			while (Time.time < endTime)
			{
				float value = Mathf.Lerp(startVal, endVal, currentTime / time);
				mat.SetFloat("_Cutoff", value);
				currentTime += Time.deltaTime;
				yield return null;
			}
			mat.SetFloat("_Cutoff", endVal);
			if (concluder != null)
			{
				concluder(concluderFlag);
			}
			if (destroyCircleFader)
			{
				Object.Destroy(GUIHelper.CircleFader);
				GUIHelper.CircleFader = null;
			}
			else if (fadeIn)
			{
				GUIHelper.CircleFader.GetComponent<Renderer>().enabled = false;
			}
			mat = null;
		}
		yield break;
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0003ACD0 File Offset: 0x00038ED0
	public static void ScaleObject(GameObject obj, Vector3 scale, float time)
	{
		iTween.ScaleTo(obj, new Hashtable
		{
			{
				"scale",
				scale
			},
			{
				"time",
				time
			},
			{
				"looptype",
				iTween.LoopType.none
			},
			{
				"easeType",
				"linear"
			}
		});
	}

	// Token: 0x04000861 RID: 2145
	public static GameObject Obscurer;

	// Token: 0x04000862 RID: 2146
	public static GameObject CircleFader;

	// Token: 0x020003AE RID: 942
	// (Invoke) Token: 0x06001DC2 RID: 7618
	public delegate void Concluder(bool flag);
}
