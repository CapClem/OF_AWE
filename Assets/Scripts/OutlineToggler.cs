using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class OutlineToggler : MonoBehaviour
{
    public Material outlineMaterial;
    //private MeshRenderer mr;
    
    // Start is called before the first frame update
    void Start()
    {
        //mr = GetComponent<MeshRenderer>();
    }

    private List<Material> actMats = new List<Material>();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
         
            if(Physics.Raycast (ray, out hit)){
                MeshRenderer mr = hit.collider.GetComponent<MeshRenderer>();;
                if (mr != null)
                {
                    foreach (var mat in mr.materials)
                    {
                        if (mat.HasProperty("_OutlineActive"))
                        {
                            mat.SetFloat("_OutlineActive", 1);
                            actMats.Add(mat);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (actMats != null && actMats.Count > 0)
            {
                actMats.ForEach(mat =>
                {
                    if (mat.HasProperty("_OutlineActive"))
                        mat.SetFloat("_OutlineActive", 0);
                });
            }
        }
    }
}
