using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public GameObject uiPrefab;
	public Transform target;

	Transform uiTransform;
	Image healthSlider;
	Transform cameraTransform;

	void Start() {
		cameraTransform = Camera.main.transform;

		foreach (Canvas c in FindObjectsOfType<Canvas>()) {
			if (c.renderMode == RenderMode.WorldSpace) {
				uiTransform = Instantiate (uiPrefab, c.transform).transform;
				healthSlider = uiTransform.GetChild (0).GetComponent<Image> ();
				break;
			}
		}
	}

	void LateUpdate() {
		uiTransform.position = target.position;
		uiTransform.forward = -cameraTransform.forward;
	}


}
