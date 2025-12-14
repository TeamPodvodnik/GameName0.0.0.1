using UnityEngine;

public class ApplyPlayerClass : MonoBehaviour
{
    private void Start()
    {
        SetupPlayerClass();
    }

    private void SetupPlayerClass()
    {
        CharacterClassData selectedClass = PlayerClassManager.Instance.GetSelectedClass();

        if (selectedClass == null)
        {
            return;
        }

        ChangePlayerLook(selectedClass);
        GiveClassWeapon(selectedClass);
    }

    private void ChangePlayerLook(CharacterClassData classInfo)
    {
        SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
        if (playerSprite != null && classInfo.classSprite != null)
        {
            playerSprite.sprite = classInfo.classSprite;
        }
    }

    private void GiveClassWeapon(CharacterClassData classInfo)
    {
        if (classInfo.weaponPrefab != null)
        {
            Transform weaponParent = transform.Find("ActiveWeapon");
            if (weaponParent != null)
            {
                GameObject weaponObject = Instantiate(classInfo.weaponPrefab, weaponParent.position, Quaternion.identity);
                weaponObject.transform.SetParent(weaponParent);

                ClassWeapon weaponScript = weaponObject.GetComponent<ClassWeapon>();
                if (weaponScript != null)
                {
                    weaponScript.SetupWeapon(classInfo, gameObject);
                }
            }
        }
    }
}