using Unity.VisualScripting;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    private float Range = 5f;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    [SerializeField] private Light[] lights;

    private void ClickRange()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Range))
            {
                if (hit.collider.CompareTag("LightSwitch"))
                {
                    ToggleLights();
                }
            }
        }
    }

    private void ToggleLights()
    {
       foreach (Light light in lights)
       {
         light.enabled = !light.enabled;
       }
    }


    // Update is called once per frame
    void Update()
    {
        ClickRange();
    }
}
