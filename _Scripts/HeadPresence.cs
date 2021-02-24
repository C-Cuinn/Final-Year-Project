using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HeadPresence : MonoBehaviour
{
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public InputDeviceCharacteristics controllerCharacteristics;
    public bool showController = false;
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics,devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if(devices.Count > 0)
        {
            targetDevice = devices[1];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if(prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab,transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if(showController)
            { 
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
            }
        }

    }
}
