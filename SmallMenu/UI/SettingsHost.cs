using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Settings;
using SmallMenu.Configuration;
using UnityEngine;
using Zenject;

namespace SmallMenu.UI;

internal class SettingsHost : IInitializable, IDisposable
{
    [UIValue("scale-options")]
    internal readonly MenuScale[] ScaleOptions = [MenuScale.Big, MenuScale.Default, MenuScale.Small, MenuScale.Tiny, MenuScale.Custom];

    [UIObject("custom-scale")]
    internal GameObject customScaleObject;

    [UIComponent("custom-scale")]
    internal SliderSetting customScaleSetting;

    private readonly Settings _settings;
    private readonly BSMLSettings _bsmlSettings;

    public SettingsHost(Settings settings, BSMLSettings bsmlSettings)
    {
        _settings = settings;
        _bsmlSettings = bsmlSettings;
    }

    [UIValue("custom-scale-active")]
    internal bool CustomScaleActive => Scale == MenuScale.Custom;

    [UIValue("custom-scale-value")]
    internal float CustomScale
    {
        get => _settings.Scale;
        set => _settings.Scale = value;
    }

    [UIValue("pos-y-value")]
    internal float PosY
    {
        get => _settings.PosY;
        set => _settings.PosY = value;
    }

    [UIValue("scale-value")]
    internal MenuScale Scale
    {
        get
        {
            if (Mathf.Approximately(_settings.Scale, 1.5f))
            {
                return MenuScale.Default;
            }
            else if (Mathf.Approximately(_settings.Scale, 2f))
            {
                return MenuScale.Big;
            }
            else if (Mathf.Approximately(_settings.Scale, 1f))
            {
                return MenuScale.Small;
            }
            else if (Mathf.Approximately(_settings.Scale, 0.5f))
            {
                return MenuScale.Tiny;
            }
            else
            {
                return MenuScale.Custom;
            }
        }
        set
        {
            switch (value)
            {
                case MenuScale.Default:
                    _settings.Scale = 1.5f;
                    break;

                case MenuScale.Big:
                    _settings.Scale = 2f;
                    break;

                case MenuScale.Small:
                    _settings.Scale = 1f;
                    break;

                case MenuScale.Tiny:
                    _settings.Scale = 0.5f;
                    break;
            }

            customScaleObject.SetActive(value == MenuScale.Custom);
            customScaleSetting.Value = _settings.Scale;
        }
    }

    public void Initialize()
    {
        _bsmlSettings.AddSettingsMenu("SmallMenu", "SmallMenu.UI.Settings.bsml", this);
    }

    public void Dispose()
    {
        _bsmlSettings.RemoveSettingsMenu(this);
    }

    [UIAction("scale-formatter")]
    internal string ScaleFormatter(MenuScale value)
    {
        return value switch
        {
            MenuScale.Default => "Default",
            MenuScale.Big => "Big",
            MenuScale.Small => "Small",
            MenuScale.Tiny => "Tiny",
            MenuScale.Custom => "Custom",
            _ => string.Empty,
        };
    }
}
