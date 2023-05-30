using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public float moveDistanse;
    public GameObject childObject;
    public Sprite pressedSprite;
    public float animTime = 0.1f;
    
    private Vector3 _childObjectDefaultPos;
    private Image _imageComponent;
    private Sprite _normalSprite;

    private void Awake()
    {
        _imageComponent = GetComponent<Image>();
        _childObjectDefaultPos = childObject.transform.localPosition;
        _normalSprite = _imageComponent.sprite;
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
        _imageComponent.sprite = pressedSprite;
        childObject.transform.localPosition = _childObjectDefaultPos + new Vector3(0, -moveDistanse);
    }
    
    private void SetNormalCondition()
    {
        _imageComponent.sprite = _normalSprite;
        childObject.transform.localPosition = _childObjectDefaultPos;
    }
    
    private IEnumerator AnimationRoutine()
    {
        SetPressedCondition();
        yield return new WaitForSecondsRealtime(animTime);
        SetNormalCondition();
    }
}
