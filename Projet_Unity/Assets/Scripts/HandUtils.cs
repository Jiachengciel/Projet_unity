using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HandUtils : MonoBehaviour, IEnumerable<GameObject> {

	public GameObject pinchWith;

	protected HashSet<GameObject> contacts;
	protected SphereCollider trigger;
	protected bool pinch;

	public bool Pinch {
		get {
			return pinch;
		}
	}

	public IEnumerator<GameObject> GetEnumerator() {
		return contacts.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	protected void Awake() {
		pinch = false;

		contacts = new HashSet<GameObject>();

		trigger = GetComponent<SphereCollider>();

		trigger.isTrigger = true;
	}

	protected void Update() {
		if(pinchWith != null) {
			pinch = (pinchWith.transform.position - transform.position).magnitude < trigger.radius;
		}
	}

	protected void OnTriggerEnter(Collider other) {
		if(! contacts.Contains(other.gameObject)) {
			contacts.Add(other.gameObject);
		}
	}

	protected void OnTriggerExit(Collider other) {
		if(contacts.Contains(other.gameObject)) {
			contacts.Remove(other.gameObject);
		}
	}
}
