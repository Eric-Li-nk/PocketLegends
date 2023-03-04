using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class LanguageDropdown : MonoBehaviour
{

    public TMP_Dropdown dropdown;

    public IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        var languages = new List<TMP_Dropdown.OptionData>();
        int currentLanguage = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; i++)
        {
            var language = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == language)
                currentLanguage = i;
            languages.Add(new TMP_Dropdown.OptionData(language.name));
        }
        
        dropdown.options = languages;
        dropdown.value = currentLanguage;
        dropdown.onValueChanged.AddListener(LanguageSelected);
    }

    public static void LanguageSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
}
