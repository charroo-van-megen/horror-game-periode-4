using Unity.VisualScripting;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    private float Range = 5f;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        ClickRange();
    }

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

}
