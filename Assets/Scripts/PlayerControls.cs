using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
   [SerializeField] 
    [Range(-1f,1f)]  float xOffset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");

        Vector3 oldPosition = transform.localPosition;
        float newXPos = oldPosition.x + xOffset;

        transform.localPosition = new  Vector3 (newXPos , oldPosition.y, oldPosition.z);
    }
}
