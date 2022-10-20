using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XPBar : MonoBehaviour
{
    //Cojer el script Del Inventario para recojer la variable de la xp
    public inventory inventory;

    //Experiencia Actual
    [Space(10)]
    public float xpForLevel = 0f;

    [Space(10)]

    //XP que tendra el nivel primer 1
    public float nextNivel = 100f;

    //Nivel Actual
    public float levelXp = 1f;

    //Esta variable sirve para agumentar el nivel maximo del siguinente nivel para que no siempre sea 100"
    public float ExtraXPForMaxlevel = 50f;

    //Expreriencia Acumulada en esta variable
    private float currentXp = 0f;

    private float difrerenciacurrentXP = 0f;
    private float resultadoDifrerenciacurrentXP = 0f;

    //Text to show level
    [Header("Text")]
    [Space(5)]
    public TextMeshProUGUI TextoDownXP;
    public TextMeshProUGUI TextoUpXP;
    [Space(10)]
    public TextMeshProUGUI TextoXpActualNivel;
    public TextMeshProUGUI TextoXpTotalNivel;
    //Barra de XP
    public Image image;

    private void Update()
    {
        currentXp = inventory.experienceInventory;
        
        if (difrerenciacurrentXP < currentXp)
        {
            resultadoDifrerenciacurrentXP = currentXp - difrerenciacurrentXP;
            xpForLevel += resultadoDifrerenciacurrentXP;
        }
        difrerenciacurrentXP = currentXp;

        UpdateXP();
    }
    private void UpdateXP()
    {
        while (xpForLevel >= nextNivel)
        {
            levelXp += 1f;
            nextNivel += ExtraXPForMaxlevel;
            xpForLevel = 0;
        }
        PrintXP();
    }
    void PrintXP()
    {
        TextoDownXP.text = "" + levelXp;
        TextoUpXP.text = "" + (levelXp + 1);
        TextoXpActualNivel.text = "" + xpForLevel;
        TextoXpTotalNivel.text = "" + nextNivel;
        image.fillAmount = xpForLevel / nextNivel;
    }
}