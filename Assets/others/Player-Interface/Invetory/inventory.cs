using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inventory : MonoBehaviour
{
    //Materials for PlayerAtributes
    public int experienceInventory = 0;
    public int scrapInventory = 0;
    //Materials for live
    public int waterInventory = 0;
    public int foodInventory = 0;
    //Materials for upgrades
    public int stoneInventory = 0;
    public int metalInventory = 0;



    //ShowTextMaterials
    //Materials for PlayerAtributes
    [SerializeField] TextMeshProUGUI ExperienceUI;
    [SerializeField] TextMeshProUGUI ScrapUI;
    //Materials for live
    [SerializeField] TextMeshProUGUI WaterUI;
    [SerializeField] TextMeshProUGUI FoodUI;
    //Materials for upgrades
    [SerializeField] TextMeshProUGUI StoneUI;
    [SerializeField] TextMeshProUGUI MetalUI;

    //OpenInvetory
    public GameObject InvetoryUI;
    public bool inventryIsOpen = false;

    void Update()
    {
        ShowMaterials();
        OpenInventory();
        CloseInventory();
    }
    public void ShowMaterials()
    {
        //Materials for PlayerAtributes
        //ExperienceUI.text = "" + experienceInventory;/////////////////////
        //crapUI.text = "" + scrapInventory;////////////////////////////
        //Materials for live
        WaterUI.text = "" + waterInventory;
        FoodUI.text = "" + foodInventory;
        //Materials for upgrades
        StoneUI.text = "" + stoneInventory;
        MetalUI.text = "" + metalInventory;
    }
    public void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventryIsOpen == false)
        {
            Debug.Log("Abierto");
            InvetoryUI.SetActive(true);
            Invoke("Activate", 0.1f);
        }
    }
    public void CloseInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventryIsOpen == true)
        {
            Debug.Log("Cerrado");
            InvetoryUI.SetActive(false);
            Invoke("Deactivate", 0.1f);
        }
    }
    public void Activate()
    {
        inventryIsOpen = true;
    }
    public void Deactivate()
    {
        inventryIsOpen = false;
    }
}
