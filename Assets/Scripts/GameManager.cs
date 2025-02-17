using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public PhysicsMaterial bouncyMaterial; // Assign the Bouncy_Material in the Inspector

    PrimitiveType primitiveToPlace;

    Vector3 nextShapePreviewPos = new Vector3(-7, 1, 1);

    GameObject previewObject;

    void Start()
    {
        GenerateNextShape();

    }

    void GenerateNextShape()
    {
        switch (Random.Range(0, 4)) //0.3 (4 à escluso)
        {
            case 0: primitiveToPlace = PrimitiveType.Cube; break;
            case 1: primitiveToPlace = PrimitiveType.Sphere; break;
            case 2: primitiveToPlace = PrimitiveType.Capsule; break;
            case 3: primitiveToPlace = PrimitiveType.Cylinder; break;
            default: primitiveToPlace = PrimitiveType.Cube; break;
        }

            if (previewObject) Destroy(previewObject);

            previewObject = GameObject.CreatePrimitive(primitiveToPlace);
            previewObject.name = "Preview shape";
            previewObject.transform.position = nextShapePreviewPos;

    }

    // Update is called once per frame
    void Update()
        {
            if (Input.GetMouseButtonDown(1)) //right mouse button
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100)) // 100 METERS/UNITS MAX DISTANCE
                {
                    GameObject go = GameObject.CreatePrimitive(primitiveToPlace);

                    go.transform.localScale = Vector3.one * 0.3f;
                    go.transform.position = hit.point + new Vector3(0, 1f, 0);
                    go.transform.rotation = Random.rotation;

                    go.AddComponent<Rigidbody>();

                    // Apply the Bouncy_Material to the primitive
                    //go.GetComponent<Collider>().material = bouncyMaterial;

                    //Control color randomness
                    Color randomColor = Random.ColorHSV();

                    float H, S, V;
                    Color.RGBToHSV(randomColor, out H, out S, out V);

                    S = 0.8f;
                    V = 0.8f;

                    randomColor = Color.HSVToRGB(H, S, V);

                    go.GetComponent<MeshRenderer>().material.color = randomColor;

                    //MUST BE INSIDE ASSETS/RESOURCES
                    Texture texture = Resources.Load<Texture>("wood_texture");

                    Debug.Log(texture);

                    //go.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", texture);
                    go.GetComponent<MeshRenderer>().material.mainTexture = texture;

                    go.AddComponent<DestroyOnFall>();
                    go.AddComponent<DragWithMouse>();

                    GenerateNextShape();
                }
            }
        }
}
