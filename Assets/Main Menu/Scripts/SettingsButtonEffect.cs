
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SettingsButtonEffect : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("Text")]
    [SerializeField] private TMP_Text buttonText;

    [Header("Hover Animation")]
    [SerializeField] private float hoverScale = 1.12f;
    [SerializeField] private float transitionSpeed = 12f;

    [Header("Dark Red Outline")]
    [SerializeField]
    private Color outlineColor =
        new Color32(80, 0, 8, 255);

    [SerializeField] private float normalOutline = 0.18f;
    [SerializeField] private float hoverOutline = 0.28f;

    [Header("Settings Panel")]
    [SerializeField] private GameObject settingsPanel;

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
            Debug.LogError("SETTINGS: TMP Text is missing!");
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

        if (outlineMaterial != null &&
            outlineMaterial.HasProperty("_OutlineWidth"))
        {
            float current = outlineMaterial.GetFloat("_OutlineWidth");

            outlineMaterial.SetFloat(
                "_OutlineWidth",
                Mathf.Lerp(
                    current,
                    targetOutline,
                    Time.unscaledDeltaTime * transitionSpeed
                )
            );
        }
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

    public void OpenSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null)
            settingsPanel.SetActive(false);
    }

    private void SetOutline(float width)
    {
        if (outlineMaterial != null &&
            outlineMaterial.HasProperty("_OutlineWidth"))
        {
            outlineMaterial.SetFloat("_OutlineWidth", width);
        }
    }

    private void OnDestroy()
    {
        if (outlineMaterial != null)
            Destroy(outlineMaterial);
    }
}
