using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0.0f, 0.0f, 0.1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0.0f, 0.0f, -0.1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f, 0.0f, 0.0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f, 0.0f, 0.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoorProp door = collision.collider.GetComponent<DoorProp>();
        if (door != null)
        {
            Renderer renderer = door.GetComponent<Renderer>();
            if (door.safe)
            {
                renderer.material.SetColor("_Color", new Color(0.0f, 1.0f, 0.0f, 1.0f));
            }
            else if (!door.safe)
            {
                renderer.material.SetColor("_Color", new Color(0.0f, 0.0f, 0.0f, 1.0f));
            }
        }
    }
}
