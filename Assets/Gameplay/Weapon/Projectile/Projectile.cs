using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] private float lifeTime;
	[SerializeField] private float speed;

	private int _damage;
	private Vector3 _direction;
	private float _lifeTime;

	private void OnEnable()
	{
		_lifeTime = lifeTime;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out IDamagable damagable))
		{
			damagable.TakeDamage(_damage);
			gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		transform.Translate(_direction * speed * Time.deltaTime);

		_lifeTime -= Time.deltaTime;
		
		if (_lifeTime < 0)
			gameObject.SetActive(false);
	}

	public void SetupProjectile(Vector3 position, Quaternion rotation, Vector3 direction, int damage)
	{
		transform.position = position;
		transform.rotation = rotation;
		_direction = direction;
		_damage = damage;
	}
}
