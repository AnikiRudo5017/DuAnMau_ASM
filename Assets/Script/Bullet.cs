using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dan : MonoBehaviour
{
    public float speed;
    public float lifetime;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = transform.right * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            var name = other.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
        }

    }
}