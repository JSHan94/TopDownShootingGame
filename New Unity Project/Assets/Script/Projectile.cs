using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	float speed = 10;
    public float damage = 1;
    float lifetime = 3;
    float skinWidth = .1f;
    private void Start()
    {
        Destroy(gameObject, lifetime);
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if( initialCollisions.Length > 0)
        {
            OnHitCollision(initialCollisions[0]);
        }
    }
    public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}


	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance+skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit);
		}
	}
    void OnHitCollision(Collider C)
    {
        IDamagable damagableObject = C.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeDamage(damage);
        }
        GameObject.Destroy(gameObject);
    }
	void OnHitObject(RaycastHit hit) {
        IDamagable damagableObject = hit.collider.GetComponent<IDamagable>();
        if(damagableObject != null)
        {
            damagableObject.TakeHit(damage, hit);
        }
		GameObject.Destroy (gameObject);
	}
}