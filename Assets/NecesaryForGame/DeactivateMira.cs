using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateMira : MonoBehaviour
{
    [SerializeField] GameObject mira;
    bool isActive;
    void Start()
    {
        isActive = mira.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isActive = !isActive;
            mira.SetActive(isActive);
        }
    }
}
