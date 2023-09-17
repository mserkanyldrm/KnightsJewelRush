using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    private CanvasGroup maskCanvas;
    public Image fill;
    public Color color;

    public bool isLow;

    private float defaultAlphaValue;
    private float maxAlphaValue = 250f;

    // Start is called before the first frame update
    void Start()
    {
        maskCanvas = mask.gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();

        if (isLow)
        {
            maskCanvas.alpha = Mathf.Lerp(0.2f, 1f, Mathf.Abs( Mathf.Cos(Time.time * 2.5f)));
        }
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        //mask.fillAmount = Mathf.Lerp(mask.fillAmount,fillAmount,0.4f);
        fill.DOFillAmount(fillAmount, 0.2f).SetEase(Ease.OutQuad);

        fill.color = color;
    }
}
