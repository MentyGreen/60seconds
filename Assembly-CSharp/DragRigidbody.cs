using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000131 RID: 305
public class DragRigidbody : MonoBehaviour
{
	// Token: 0x06000EFD RID: 3837 RVA: 0x0003E474 File Offset: 0x0003C674
	private void Update()
	{
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}
		RaycastHit raycastHit;
		if (!Physics.Raycast(this.FindCamera().ScreenPointToRay(Input.mousePosition), out raycastHit, this.maxDistance))
		{
			return;
		}
		if (!raycastHit.rigidbody || raycastHit.rigidbody.isKinematic)
		{
			return;
		}
		if (!this.springJoint)
		{
			GameObject gameObject = new GameObject("Rigidbody dragger");
			gameObject.AddComponent<Rigidbody>().isKinematic = true;
			this.springJoint = gameObject.AddComponent<SpringJoint>();
		}
		this.springJoint.transform.position = raycastHit.point;
		if (this.attachToCenterOfMass)
		{
			Vector3 vector = base.transform.TransformDirection(raycastHit.rigidbody.centerOfMass) + raycastHit.rigidbody.transform.position;
			vector = this.springJoint.transform.InverseTransformPoint(vector);
			this.springJoint.anchor = vector;
		}
		else
		{
			this.springJoint.anchor = Vector3.zero;
		}
		this.springJoint.spring = this.spring;
		this.springJoint.damper = this.damper;
		this.springJoint.maxDistance = this.distance;
		this.springJoint.connectedBody = raycastHit.rigidbody;
		base.StartCoroutine(this.DragObject(raycastHit.distance));
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0003E5CD File Offset: 0x0003C7CD
	private IEnumerator DragObject(float distance)
	{
		float oldDrag = this.springJoint.connectedBody.drag;
		float oldAngularDrag = this.springJoint.connectedBody.angularDrag;
		this.springJoint.connectedBody.drag = this.drag;
		this.springJoint.connectedBody.angularDrag = this.angularDrag;
		Camera cam = this.FindCamera();
		while (Input.GetMouseButton(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			this.springJoint.transform.position = ray.GetPoint(distance);
			yield return null;
		}
		if (this.springJoint.connectedBody)
		{
			this.springJoint.connectedBody.drag = oldDrag;
			this.springJoint.connectedBody.angularDrag = oldAngularDrag;
			this.springJoint.connectedBody = null;
		}
		yield break;
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0003E5E3 File Offset: 0x0003C7E3
	private Camera FindCamera()
	{
		if (base.GetComponent<Camera>())
		{
			return base.GetComponent<Camera>();
		}
		return Camera.main;
	}

	// Token: 0x0400090D RID: 2317
	public float maxDistance = 100f;

	// Token: 0x0400090E RID: 2318
	public float spring = 50f;

	// Token: 0x0400090F RID: 2319
	public float damper = 5f;

	// Token: 0x04000910 RID: 2320
	public float drag = 10f;

	// Token: 0x04000911 RID: 2321
	public float angularDrag = 5f;

	// Token: 0x04000912 RID: 2322
	public float distance = 0.2f;

	// Token: 0x04000913 RID: 2323
	public bool attachToCenterOfMass;

	// Token: 0x04000914 RID: 2324
	private SpringJoint springJoint;
}
