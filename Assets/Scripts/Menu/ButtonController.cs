using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public float moveDistanse;
    public GameObject childObject;
    public Sprite pressedSprite;
    public float animTime = 0.1f;
    
    private Vector3 childObjectDefaultPos;
    private Image imageComponent;
    private Sprite normalSprite;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        childObjectDefaultPos = childObject.transform.localPosition;
        normalSprite = imageComponent.sprite;
    }
    
    public void PlayAnim()
    {
        StartCoroutine(AnimationRoutine());
    }

    private void SetPressedCondition()
    {
        imageComponent.sprite = pressedSprite;
        childObject.transform.localPosition = childObjectDefaultPos + new Vector3(0, -moveDistanse);
    }
    
    private void SetNormalCondition()
    {
        imageComponent.sprite = normalSprite;
        childObject.transform.localPosition = childObjectDefaultPos;
    }
    
    private IEnumerator AnimationRoutine()
    {
        SetPressedCondition();
        yield return new WaitForSecondsRealtime(animTime);
        SetNormalCondition();
    }
}
