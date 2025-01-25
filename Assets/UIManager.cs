using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider destructionSlider;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
        SetDefaultValues();
    }
    private void SetDefaultValues()
    {
        destructionSlider.value = 0;
    }
    public void UpdateDestructionSlider()
    {
        destructionSlider.value = gameManager.GetDestructionProgress();
    }
}
