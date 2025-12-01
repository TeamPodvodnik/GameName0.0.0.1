using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ClassSelectionButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Class Settings")]
    public CharacterClassData classData;

    [Header("UI Elements")]
    public Image classImage;
    public GameObject descriptionBox;
    public TextMeshProUGUI descriptionText;

    void Start()
    {
        if (classImage != null && classData != null)
        {
            classImage.sprite = classData.classSprite;
        }

        if (descriptionBox != null)
        {
            descriptionBox.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (classData != null)
        {
            PlayerClassManager.Instance.SetSelectedClass(classData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (descriptionBox != null && descriptionText != null)
        {
            descriptionText.text = $"{classData.displayName}\n\n{classData.classDescription}";
            descriptionBox.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (descriptionBox != null)
        {
            descriptionBox.SetActive(false);
        }
    }
}