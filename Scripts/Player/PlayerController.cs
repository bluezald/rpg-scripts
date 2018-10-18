using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	public Interactable focus;

	Camera camera;
	public LayerMask movementMask;
	PlayerMotor motor;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		motor = GetComponent<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100, movementMask)) {
				motor.MoveToPoint (hit.point);
				//Debug.Log ("We hit" + hit.collider.name + " " + hit.point);
				RemoveFocus();
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			Ray ray = camera.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit;

			if (Physics.Raycast (ray, out hit, 100)) {
				Interactable interactable = hit.collider.GetComponent<Interactable> ();

				if (interactable != null) {
					SetFocus (interactable);
				}
			}
		}

	}

	void SetFocus(Interactable newFocus) {

		if (newFocus != focus) {
			if (focus != null) {
				focus.OnDeFocused ();
			}

			focus = newFocus;
			motor.FollowTarget (newFocus);

		}

		newFocus.OnFocused (transform);
	}

	void RemoveFocus() {

		if (focus != null) {
			focus.OnDeFocused ();
		}
		focus = null;
		motor.StopFollowingTarget ();
	}
}
