using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class globalMaterials : MonoBehaviour
{
    //Take GameObject To hide
    public GameObject gameObject;
    public float timeToRestetMaterial = 1f;
    [Space(15)]


    //Take Scripts
    public inventory inventory;
    [Space(15)]


    //Basic Variables
    private bool insideTrigger = false;
    private bool alreadyReceived = false;
    public GameObject PressButtonUI;


    //Materials for PlayerAtributes.
    [Header("Experience_XP")]
    [Space(5)]
    public int experience;
    [Space(5)]
    public int minExperienceRecibe = 0;
    public int maxExperienceRecibe = 0;

    [Header("Scap_Money")]
    [Space(5)]
    public int scrap;
    [Space(5)]
    public int minScrapRecibe = 0;
    public int maxScrapRecibe = 0;


    //Materials for live.
    [Header("Water MAT")]
    [Space(5)]
        public int water;
    [Space(5)]
        public int minWaterRecibe = 0;
        public int maxWaterRecibe = 0;

    [Header("Food MAT")]
    [Space(5)]
        public int food;
    [Space(5)]
        public int minFoodRecibe = 0;
        public int maxFoodRecibe = 0;

    //Materials for upgrades
    [Space(25)]

    [Header("Stone MAT")]
    [Space(5)]
        public int stone;
    [Space(5)]
        public int minStoneRecibe = 0;
        public int maxStoneRecibe = 0;

    [Header("Metal MAT")]
    [Space(5)]
        public int metal;
    [Space(5)]
        public int minMetalRecibe = 0;
        public int maxMetalRecibe = 0;

    // Update is called once per frame
    void Update()
    {
        if (insideTrigger == true && alreadyReceived == false)
        {
            PressButtonUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                UpdateMat();
                PressButtonUI.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PressButtonUI.SetActive(false);
            insideTrigger = false;
        }
    }
    void UpdateMat()
    {
        //Materials for PlayerAtributes.
        experience = Random.Range(minExperienceRecibe, maxExperienceRecibe);
        scrap = Random.Range(minScrapRecibe, maxScrapRecibe);

        //Materials for live.
        water = Random.Range(minWaterRecibe, maxWaterRecibe);
        food = Random.Range(minFoodRecibe, maxFoodRecibe);

        //Materials for upgrades

        stone = Random.Range(minStoneRecibe, maxStoneRecibe);
        metal = Random.Range(minMetalRecibe, maxMetalRecibe);

        alreadyReceived = true;
        //Add to Inventory
        UpdateInventory();
    }
    void UpdateInventory()
    {
        //Materials for PlayerAtributes.
        inventory.experienceInventory += experience;
        inventory.scrapInventory += scrap;

        //Materials for live.
        inventory.waterInventory += water;
        inventory.foodInventory += food;

        //Materials for upgrades
        inventory.stoneInventory += stone;
        inventory.metalInventory += metal;

        HideObject();
    }
    void HideObject()
    {
        gameObject.SetActive(false);
        Invoke("RestetMaterial", timeToRestetMaterial);
    }
    void RestetMaterial()
    {
        gameObject.SetActive(true);
        alreadyReceived = false;
        insideTrigger = false;
    }
}
