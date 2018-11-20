using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public GameObject floatingTextPrefab;
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public Stat armor;

	public event System.Action<int, int> OnHealthChanged;

	void Awake() 
	{
		currentHealth = maxHealth;
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.T)) {
			TakeDamage (10);
		}
	}

	public void TakeDamage(int damage) 
	{
		damage -= armor.GetValue ();
		damage = Mathf.Clamp (damage, 0, int.MaxValue);

		currentHealth -= damage;
		Debug.Log (transform.name + " takes " + damage + " damage");

		ShowFloatingText (damage);

		if (OnHealthChanged != null) {
			OnHealthChanged (maxHealth, currentHealth);
		}

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public virtual void Die()
	{
		Debug.Log (transform.name + " died.");
	}

	void ShowFloatingText(int damage) 
	{
		if (floatingTextPrefab == null) {
			return;
		}

		var text = Instantiate (floatingTextPrefab, transform.position, new Quaternion(0, -180, 0, 0), transform);
		text.GetComponent<TextMesh> ().text = damage.ToString ();
	}

}
