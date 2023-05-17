using System.Collections;
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

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
        childObjectDefaultPos = childObject.transform.localPosition;
        normalSprite = imageComponent.sprite;
    }
    
    public void PlayAnim()
    {
        StartCoroutine(AnimationRoutine());
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        SetNormalCondition();
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
