using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000187 RID: 391
	public class Popup
	{
		// Token: 0x06001160 RID: 4448 RVA: 0x0004904C File Offset: 0x0004724C
		public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, object[] list, GUIStyle listStyle, Popup.ListCallBack callBack)
		{
			return Popup.List(position, ref showList, ref listEntry, buttonContent, list, "button", "box", listStyle, callBack);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0004907C File Offset: 0x0004727C
		public static bool List(Rect position, ref bool showList, ref int listEntry, GUIContent buttonContent, object[] list, GUIStyle buttonStyle, GUIStyle boxStyle, GUIStyle listStyle, Popup.ListCallBack callBack)
		{
			int controlID = GUIUtility.GetControlID(Popup.popupListHash, FocusType.Passive);
			bool flag = false;
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl != EventType.MouseDown)
			{
				if (typeForControl == EventType.MouseUp)
				{
					if (showList)
					{
						flag = true;
						callBack();
					}
				}
			}
			else if (position.Contains(Event.current.mousePosition))
			{
				GUIUtility.hotControl = controlID;
				showList = true;
			}
			GUI.Label(position, buttonContent, buttonStyle);
			if (showList)
			{
				string[] array = new string[list.Length];
				for (int i = 0; i < list.Length; i++)
				{
					array[i] = list[i].ToString();
				}
				Rect position2 = new Rect(position.x, position.y, position.width, (float)(list.Length * 20));
				GUI.Box(position2, "", boxStyle);
				listEntry = GUI.SelectionGrid(position2, listEntry, array, 1, listStyle);
			}
			if (flag)
			{
				showList = false;
			}
			return flag;
		}

		// Token: 0x04000B22 RID: 2850
		private static int popupListHash = "PopupList".GetHashCode();

		// Token: 0x020003DD RID: 989
		// (Invoke) Token: 0x06001E72 RID: 7794
		public delegate void ListCallBack();
	}
}
