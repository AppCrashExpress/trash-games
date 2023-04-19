using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    private abstract class ToolBase
    {
        protected GameObject pointerObj;
        protected readonly Transform attachedObj;
        private float dist = 2.0f;

        public float Distance
        {
            get { return dist; }
            set { dist = value; }
        }

        public ToolBase(GameObject pointerPrefab, Transform attachedObject)
        {
            attachedObj = attachedObject;
            pointerObj = Instantiate(pointerPrefab, GetNewPointerPos(attachedObject), Quaternion.identity);
            pointerObj.SetActive(false);
        }

        public void SetActive(bool active)
        {
            pointerObj.SetActive(active);
        }

        public virtual void Render()
        {
            Vector3 pos = GetNewPointerPos(attachedObj);
            pointerObj.transform.position = pos;
        }

        public abstract void UseMain();
        public abstract void UseAlt();

        private Vector3 GetNewPointerPos(Transform attachedObject)
        {
            return attachedObject.position + attachedObject.forward * dist;
        }

    };

    private class Spawner : ToolBase
    {
        private GameObject spawneePrefab;
        private Material pointerMaterial;
        private SpawnValidation pointerValidator;
        private readonly Color unableSpawn = new Color(1.0f, 0.0f, 0.0f, 0.3f);
        private readonly Color ableSpawn = new Color(0.0f, 1.0f, 0.0f, 0.3f);

        public Spawner(GameObject spawneePrefab, GameObject pointerPrefab, Transform attachedObject) :
            base(pointerPrefab, attachedObject)
        {
            pointerMaterial = pointerObj.GetComponent<Renderer>().material;
            pointerValidator = pointerObj.GetComponent<SpawnValidation>();
            this.spawneePrefab = spawneePrefab;
        }

        public override void Render()
        {
            base.Render();
            if (pointerValidator.CanSpawn())
            {
                pointerMaterial.color = ableSpawn;
            }
            else
            {
                pointerMaterial.color = unableSpawn;
            }
        }

        public override void UseMain()
        {
            if (pointerValidator.CanSpawn())
            {
                Instantiate(spawneePrefab, pointerObj.transform.position, Quaternion.identity);
            }
        }

        public override void UseAlt()
        {

        }
    }

    private class Magnet : ToolBase
    {
        public Magnet(GameObject pointerPrefab, Transform attachedObject) :
            base(pointerPrefab, attachedObject)
        {
        }

        public override void UseMain()
        {
            RaycastHit[] hits = Physics.RaycastAll(attachedObj.position, attachedObj.forward)
                .Where(hit => hit.transform.tag == "Flake").ToArray();
            if (hits.Length == 0)
                return;
            RaycastHit closest = hits[0];
            float current_dist = Vector3.Distance(attachedObj.transform.position, closest.point);
            for (int i = 1; i < hits.Length; ++i)
            {
                float new_dist = Vector3.Distance(attachedObj.transform.position, hits[i].point);
                if (new_dist < current_dist)
                {
                    closest = hits[i];
                    current_dist = new_dist;
                }
            }

            closest.transform.gameObject.GetComponent<FlakeReaction>().MarkFlake();
        }

        public override void UseAlt()
        {
            GameObject[] markedFlakes = GameObject.FindGameObjectsWithTag("MarkedFlake");
            if (markedFlakes.Length == 0)
            {
                return;
            }

            Vector3 pushEnd = base.pointerObj.transform.position;
            foreach (var flake in markedFlakes)
            {
                FlakeReaction flakeScript = flake.GetComponent<FlakeReaction>();
                flakeScript.PushMarked(pushEnd - flake.transform.position);
                flakeScript.UnmarkFlake();
            }
        }
    }

    private class Unknown : ToolBase
    {
        public Unknown(GameObject pointerPrefab, Transform attachedObject) :
            base(pointerPrefab, attachedObject)
        {
        }

        public override void UseMain()
        {
            throw new System.NotImplementedException();
        }
        public override void UseAlt()
        {
            throw new System.NotImplementedException();
        }
    }

    private enum ToolType
    {
        Spawner,
        Magnet
    };

    public GameObject spawnerPointerPrefab;
    public GameObject magnetPointerPrefab;
    public GameObject spawneePrefab;
    public float distanceIncrement = 0.5f;

    private ToolBase[] tools;
    private int toolId = 0;

    // Start is called before the first frame update
    void Start()
    {
        tools = new ToolBase[] {
            getTool(ToolType.Spawner, spawnerPointerPrefab, transform),
            getTool(ToolType.Magnet,  magnetPointerPrefab,  transform)
        };
        tools[toolId].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            tools[toolId].UseMain();
        if (Input.GetMouseButtonDown(1))
            tools[toolId].UseAlt();

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            tools[toolId].Distance += distanceIncrement;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            tools[toolId].Distance -= distanceIncrement;
        }
        tools[toolId].Distance = Mathf.Clamp(tools[toolId].Distance, 2.0f, 15.0f);

        if (Input.GetKeyDown("r"))
        {
            tools[toolId].SetActive(false);
            toolId = (toolId + 1) % tools.Length;
            tools[toolId].SetActive(true);
        }
    }

    void LateUpdate()
    {
        tools[toolId].Render();
    }

    private ToolBase getTool(ToolType neededType, GameObject pointerPrefab, Transform attachedObj)
    {
        switch (neededType)
        {
            case ToolType.Spawner:
                return new Spawner(spawneePrefab, pointerPrefab, attachedObj);
            case ToolType.Magnet:
                return new Magnet(pointerPrefab, attachedObj);
            default:
                return new Unknown(pointerPrefab, attachedObj);
        }
    }
}
