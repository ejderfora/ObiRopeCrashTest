using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;
    
    private void Start()
    {
        int ID = 0;
        ID = PlayerPrefs.GetInt("LocaleKey",0);
        ChangeLocale(ID);
    }
    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
        

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeLocale(0);
        }
    }
    public void ChangeLocale(int _localeID)
    {
        if (active==true) 
        {
            return;
        }
        StartCoroutine(SetLocale(_localeID));
        
    }
}
