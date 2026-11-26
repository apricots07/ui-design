
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayButtonEffect : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    [SerializeField] private TMP_Text buttonText;

    [Header("Hover Animation")]
    [SerializeField] private float hoverScale = 1.16f;
    [SerializeField] private float transitionSpeed = 12f;

    [Header("Dark Red Outline")]
    [SerializeField]
    private Color outlineColor =
        new Color32(80, 0, 8, 255);

    [SerializeField] private float normalOutline = 0.18f;
    [SerializeField] private float hoverOutline = 0.28f;

    [Header("Play Screen")]
    [SerializeField] private GameObject playBlackScreenPanel;

    private Vector3 originalScale;
    private Vector3 targetScale;
    private float targetOutline;

    private Material outlineMaterial;
    private RectTransform textRect;

    private void Awake()
    {
        if (buttonText == null)
            buttonText = GetComponent<TMP_Text>();

        if (buttonText == null)
        {
            Debug.LogError("PLAY: TMP Text is missing!");
            enabled = false;
            return;
        }

        textRect = buttonText.rectTransform;

        originalScale = textRect.localScale;
        targetScale = originalScale;

        outlineMaterial = new Material(buttonText.fontMaterial);
        buttonText.fontMaterial = outlineMaterial;

        if (outlineMaterial.HasProperty("_OutlineColor"))
            outlineMaterial.SetColor("_OutlineColor", outlineColor);

        targetOutline = normalOutline;

        SetOutline(normalOutline);
    }

    private void Update()
    {
        textRect.localScale = Vector3.Lerp(
            textRect.localScale,
            targetScale,
            Time.unscaledDeltaTime * transitionSpeed
        );

        float currentOutline =
            outlineMaterial.GetFloat("_OutlineWidth");

        outlineMaterial.SetFloat(
            "_OutlineWidth",
            Mathf.Lerp(
                currentOutline,
                targetOutline,
                Time.unscaledDeltaTime * transitionSpeed
            )
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        targetOutline = hoverOutline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
        targetOutline = normalOutline;
    }

    public void ShowPlayScreen()
    {
        if (playBlackScreenPanel != null)
            playBlackScreenPanel.SetActive(true);
    }

    public void ClosePlayScreen()
    {
        if (playBlackScreenPanel != null)
            playBlackScreenPanel.SetActive(false);
    }

    private void SetOutline(float width)
    {
        if (outlineMaterial.HasProperty("_OutlineWidth"))
            outlineMaterial.SetFloat("_OutlineWidth", width);
    }

    private void OnDestroy()
    {
        if (outlineMaterial != null)
            Destroy(outlineMaterial);
    }
}
