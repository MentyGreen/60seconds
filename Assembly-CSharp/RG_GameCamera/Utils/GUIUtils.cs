using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000183 RID: 387
	internal static class GUIUtils
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x00048494 File Offset: 0x00046694
		public static bool SliderEdit(string label, float min, float max, ref float value)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			float num = value;
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(GUIUtils.labelMaxWidth)
			});
			value = GUILayout.HorizontalSlider(value, min, max, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			value = Mathf.Clamp(value, min, max);
			return num != value;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000484F0 File Offset: 0x000466F0
		public static bool SliderEdit(string label, int min, int max, ref int value)
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			int num = value;
			GUILayout.Label(label, Array.Empty<GUILayoutOption>());
			value = (int)GUILayout.HorizontalSlider((float)value, (float)min, (float)max, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			value = Mathf.Clamp(value, min, max);
			return num != value;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00048540 File Offset: 0x00046740
		public static bool Toggle(string label, ref bool value)
		{
			bool flag = value;
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(GUIUtils.labelMaxWidth)
			});
			value = GUILayout.Toggle(value, string.Empty, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			return flag != value;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00048593 File Offset: 0x00046793
		public static bool Selection(string label, string[] labels, ref int index)
		{
			return false;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00048596 File Offset: 0x00046796
		public static Enum EnumSelection(string label, Enum selected, ref bool changed)
		{
			return null;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0004859C File Offset: 0x0004679C
		public static bool String(string label, ref string input)
		{
			string b = input;
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(GUIUtils.labelMaxWidth)
			});
			input = GUILayout.TextField(input, Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			return input != b;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000485EC File Offset: 0x000467EC
		public static bool Vector2(string label, ref Vector2 input)
		{
			Vector2 rhs = input;
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(GUIUtils.labelMaxWidth)
			});
			string value = GUILayout.TextField(input.x.ToString(), Array.Empty<GUILayoutOption>());
			string value2 = GUILayout.TextField(input.y.ToString(), Array.Empty<GUILayoutOption>());
			try
			{
				input.x = Convert.ToSingle(value);
				input.y = Convert.ToSingle(value2);
			}
			catch
			{
			}
			GUILayout.EndHorizontal();
			return input != rhs;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00048690 File Offset: 0x00046890
		public static bool Vector3(string label, ref Vector3 input)
		{
			Vector3 rhs = input;
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			GUILayout.Label(label, new GUILayoutOption[]
			{
				GUILayout.Width(GUIUtils.labelMaxWidth)
			});
			string value = GUILayout.TextField(input.x.ToString(), Array.Empty<GUILayoutOption>());
			string value2 = GUILayout.TextField(input.y.ToString(), Array.Empty<GUILayoutOption>());
			string value3 = GUILayout.TextField(input.z.ToString(), Array.Empty<GUILayoutOption>());
			try
			{
				input.x = Convert.ToSingle(value);
				input.y = Convert.ToSingle(value2);
				input.z = Convert.ToSingle(value3);
			}
			catch
			{
			}
			GUILayout.EndHorizontal();
			return input != rhs;
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00048754 File Offset: 0x00046954
		public static void Separator(string label, float height)
		{
			GUILayout.Box(label, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(true),
				GUILayout.Height(height)
			});
		}

		// Token: 0x04000B21 RID: 2849
		private static float labelMaxWidth = 130f;
	}
}
