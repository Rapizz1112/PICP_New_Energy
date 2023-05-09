using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ManagerDestroy : NetworkBehaviour
{
    public GameObject[] serverObject;
    public GameObject[] clientObject;
    // Start is called before the first frame update
    void Start()
    {
        if(isServer)
        {
            for(int i = 0; i < clientObject.Length;i++)
            {
                if(clientObject[i] != null) Destroy(clientObject[i]);
            }

        }else
        {
            for (int i = 0; i < serverObject.Length; i++)
            {
                if (serverObject[i] != null) Destroy(serverObject[i]);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
