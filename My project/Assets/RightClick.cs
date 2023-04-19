using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClick : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite red;
    public float strength;

    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                return;
            }

            if (gameObject.tag == "Selected ball")
            {
                gameObject.tag = "Ball";
                int rand = Random.Range(0, sprites.Length);
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[rand];

                Debug.Log("a");
                rb.AddForce((hit.point - gameObject.transform.position) * strength, ForceMode.Impulse);
            }
        }
    }
}
