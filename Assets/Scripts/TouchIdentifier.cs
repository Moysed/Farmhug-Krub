using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class TouchIdentifier : MonoBehaviour {
    public int fingerId;
    public float timeCreated;
    public Vector2 startPosition;
    public Vector3 deltaPosition;
    public Collider2D collider;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        collider = collision;
        if (gameObject.tag == "Ground")
        {
            Debug.Log("Hi");
        }
    }
}
