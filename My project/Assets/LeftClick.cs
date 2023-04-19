using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftClick : MonoBehaviour
{
    public GameObject ballTemplate;
    public Sprite[] sprites;
    public Sprite red;

    private Camera attachedCamera;
    private float ballRadius;

    void Start()
    {
        attachedCamera = GetComponent<Camera>();
        ballRadius = ballTemplate.GetComponent<SphereCollider>().radius;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = attachedCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(mouseRay, out hit, Mathf.Infinity))
            {
                return;
            }

            int rand;
            switch (hit.transform.tag)
            {
                case "Backwall":
                    Vector3 hitPos = hit.point + hit.normal * (ballRadius / 2);

                    GameObject obj = Instantiate(ballTemplate, hitPos, Quaternion.identity);
                    rand = Random.Range(0, sprites.Length);
                    obj.GetComponent<SpriteRenderer>().sprite = sprites[rand];
                    break;
                case "Ball":
                    hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite = red;
                    hit.transform.tag = "Selected ball";
                    break;

                case "Selected ball":
                    rand = Random.Range(0, sprites.Length);
                    hit.transform.GetComponent<SpriteRenderer>().sprite = sprites[rand];
                    hit.transform.tag = "Ball";
                    break;
            }
        }
    }
}
