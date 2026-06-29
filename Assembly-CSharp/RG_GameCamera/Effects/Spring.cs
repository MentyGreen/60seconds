using System;

namespace RG_GameCamera.Effects
{
	// Token: 0x020001BC RID: 444
	public class Spring
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0005153F File Offset: 0x0004F73F
		public void Setup(float mass, float distance, float springStrength, float damping)
		{
			this.mass = mass;
			this.distance = distance;
			this.springConstant = springStrength;
			this.damping = damping;
			this.velocity = 0f;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00051569 File Offset: 0x0004F769
		public void AddForce(float force)
		{
			this.velocity += force;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0005157C File Offset: 0x0004F77C
		public float Calculate(float timeStep)
		{
			this.springForce = -this.springConstant * this.distance - this.velocity * this.damping;
			this.acceleration = this.springForce / this.mass;
			this.velocity += this.acceleration * timeStep;
			this.distance += this.velocity * timeStep;
			return this.distance;
		}

		// Token: 0x04000C6B RID: 3179
		private float mass;

		// Token: 0x04000C6C RID: 3180
		private float distance;

		// Token: 0x04000C6D RID: 3181
		private float springConstant;

		// Token: 0x04000C6E RID: 3182
		private float damping;

		// Token: 0x04000C6F RID: 3183
		private float acceleration;

		// Token: 0x04000C70 RID: 3184
		private float velocity;

		// Token: 0x04000C71 RID: 3185
		private float springForce;
	}
}
