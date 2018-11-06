using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour {

	public GameObject uiPrefab;
	public Transform target;
	float visibleTime = 5;

	float lastMadeVisibleTime;
	Transform uiTransform;
	Image healthSlider;
	Transform cameraTransform;

	void Start() {
		cameraTransform = Camera.main.transform;

		foreach (Canvas c in FindObjectsOfType<Canvas>()) {
			if (c.renderMode == RenderMode.WorldSpace) {
				uiTransform = Instantiate (uiPrefab, c.transform).transform;
				healthSlider = uiTransform.GetChild (0).GetComponent<Image> ();
				uiTransform.gameObject.SetActive (false);
				break;
			}
		}

		GetComponent<CharacterStats> ().OnHealthChanged += OnHealthChanged;
	}

	void OnHealthChanged(int maxHealth, int currentHealth) {
		if (uiTransform != null) {
			uiTransform.gameObject.SetActive (true);
			lastMadeVisibleTime = Time.time;

			healthSlider.fillAmount = (float)currentHealth / maxHealth;

			if (currentHealth <= 0) {
				Destroy (uiTransform.gameObject);
			}
		}
	}

	void LateUpdate() {
		if (uiTransform != null) {
			uiTransform.position = target.position;
			uiTransform.forward = -cameraTransform.forward;

			if (Time.time - lastMadeVisibleTime > visibleTime) {
				uiTransform.gameObject.SetActive (false);
			}
		}
	}


}
