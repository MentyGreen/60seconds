using System;
using UnityEngine;

namespace RG_GameCamera.Utils
{
	// Token: 0x02000186 RID: 390
	public static class Math
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x00048BA0 File Offset: 0x00046DA0
		public static float ExpN(float x, float n)
		{
			return Mathf.Exp(n * Mathf.Log(x));
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00048BAF File Offset: 0x00046DAF
		public static float NormalizeValueInRange(float a, float min, float max)
		{
			if (max <= min)
			{
				return 1f;
			}
			return (Mathf.Clamp(a, min, max) - min) / (max - min);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00048BC9 File Offset: 0x00046DC9
		public static float DeNormalizeValueInRange(float a, float min, float max)
		{
			return Mathf.Clamp(a, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00048BE1 File Offset: 0x00046DE1
		public static bool IsInRange(float a, float min, float max)
		{
			return a >= min && a <= max;
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00048BF0 File Offset: 0x00046DF0
		public static void ToPIRange(ref float angle)
		{
			if (angle < -3.1415927f)
			{
				angle += 6.2831855f;
			}
			if (angle > 3.1415927f)
			{
				angle -= 6.2831855f;
			}
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00048C18 File Offset: 0x00046E18
		public static float Sqr(float x)
		{
			return x * x;
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00048C20 File Offset: 0x00046E20
		public static void ToSpherical(Vector3 dir, out float rotX, out float rotZ)
		{
			float x = Mathf.Sqrt(Math.Sqr(dir.x) + Math.Sqr(dir.z));
			rotX = Mathf.Atan2(dir.x, dir.z);
			rotZ = Mathf.Atan2(dir.y, x);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00048C6C File Offset: 0x00046E6C
		public static void ToCartesian(float rotX, float rotZ, out Vector3 dir)
		{
			float y = Mathf.Sin(rotZ);
			float num = Mathf.Cos(rotZ);
			float num2 = Mathf.Sin(rotX);
			float num3 = Mathf.Cos(rotX);
			dir.x = num2 * num;
			dir.y = y;
			dir.z = num3 * num;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x00048CAE File Offset: 0x00046EAE
		public static float ConvergeToValue(float target, float val, float timeRel, float speedPerSec)
		{
			if (val > target)
			{
				val -= timeRel * speedPerSec;
				if (val < target)
				{
					val = target;
				}
			}
			else
			{
				val += timeRel * speedPerSec;
				if (val > target)
				{
					val = target;
				}
			}
			return val;
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00048CD4 File Offset: 0x00046ED4
		public static void Swap<T>(ref T a, ref T b)
		{
			T t = a;
			a = b;
			b = t;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00048CFC File Offset: 0x00046EFC
		public static Vector3 VectorXZ(Vector3 v)
		{
			Vector3 vector = v;
			vector.y = 0f;
			return vector.normalized;
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00048D20 File Offset: 0x00046F20
		public static void CorrectRotationUp(ref Quaternion rot)
		{
			Vector3 forward = rot * Vector3.forward;
			rot = Quaternion.LookRotation(forward, Vector3.up);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00048D50 File Offset: 0x00046F50
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed, float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00048DFC File Offset: 0x00046FFC
		public static Vector3[] GetNearPlanePoints(Camera camera)
		{
			Plane[] array = GeometryUtility.CalculateFrustumPlanes(camera);
			return new Vector3[]
			{
				Math.Intersection3Planes(array[1], array[2], array[4]),
				Math.Intersection3Planes(array[1], array[3], array[4]),
				Math.Intersection3Planes(array[0], array[3], array[4]),
				Math.Intersection3Planes(array[0], array[2], array[4])
			};
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00048E9C File Offset: 0x0004709C
		public static Vector3 Intersection3Planes(Plane p0, Plane p1, Plane p2)
		{
			float d = p0.normal[0] * p1.normal[1] * p2.normal[2] - p0.normal[0] * p1.normal[2] * p2.normal[1] + p1.normal[0] * p2.normal[1] * p0.normal[2] - p1.normal[0] * p0.normal[1] * p2.normal[2] + p2.normal[0] * p0.normal[1] * p1.normal[2] - p2.normal[0] * p1.normal[1] * p0.normal[2];
			return (Vector3.Cross(p1.normal, p2.normal) * -p0.distance + Vector3.Cross(p2.normal, p0.normal) * -p1.distance + Vector3.Cross(p0.normal, p1.normal) * -p2.distance) / d;
		}
	}
}
