using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{

	const float G = 667.4f;

	public static List<Attractor> Attractors;

	public Rigidbody rb;
	public Collider athmosfera;
	public Transform playerTransform;
	public Rigidbody playerRigidbody;

	void FixedUpdate(){
		if (this.CompareTag("PlanetTag")){
			foreach (Attractor attractor in Attractors)
			{
				if (attractor != this)
				{
					Attract(attractor);
				}
			}
		}
	}

	void OnEnable(){
		if (Attractors == null) {
			Attractors = new List<Attractor>();
		}
		if (!this.CompareTag("PlanetTag")){
			Attractors.Add(this);
		}
	}

	void OnDisable(){
		if (!this.CompareTag("PlanetTag")){
			Attractors.Remove(this);
		}
	}

	void Attract(Attractor objToAttract) {
		Rigidbody rbToAttract = objToAttract.rb;
		Vector3 direction = rb.position - rbToAttract.position;
		float distance = direction.magnitude;
		float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
		Vector3 force = direction.normalized * forceMagnitude;
		rbToAttract.AddForce(force);
	}

	private void OnTriggerStay(Collider other)
	{

		if(this.tag.Equals("PlanetTag"))
		{
			Vector3 gravityUp = (playerTransform.position - transform.position).normalized;
			Vector3 localUp = playerTransform.up;
			Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * playerTransform.rotation;
			playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 50f * Time.deltaTime);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (this.tag.Equals("PlanetTag") && collision.collider.name.Equals("Player"))
		{
			playerRigidbody.drag = 100;
		}
	}

	private void OnCollisionExit(Collision collision)
	{
		if (this.tag.Equals("Planet") && collision.collider.name.Equals("Player"))
		{
			playerRigidbody.drag = 100;
		}
	}

}
