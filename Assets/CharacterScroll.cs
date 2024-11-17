using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScroll : MonoBehaviour
{
    
    [SerializeField] private Button left;
    [SerializeField] private Button right;
    [SerializeField] private List<Transform> positions;
    private ScrollRect scrollRect;
    private int currentOption;
    private int options;
    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        options = positions.Count;
        currentOption = 2;
        scrollRect.normalizedPosition = positions[currentOption].position.normalized;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void ScrollLeft()
    {
        if (currentOption == 0)
        {
            currentOption = options - 1;
        }
        
        currentOption--;
        scrollRect.normalizedPosition = positions[currentOption].position.normalized;
    }

    public void ScrollRight()
    {
        if (currentOption == options - 1)
        {
            currentOption = 0;
        }

        currentOption++;
        scrollRect.normalizedPosition = positions[currentOption].position.normalized;
    }

}
