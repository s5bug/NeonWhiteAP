using HarmonyLib;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeonWhiteAP.Patches;

[HarmonyPatch(typeof(MenuScreenTitle))]
public class MenuScreenTitlePatch {
    [HarmonyPrefix]
    [HarmonyPatch(nameof(MenuScreenTitle.OnSetVisible))]
    private static void OnSetVisible(MenuScreenTitle __instance, bool animate) {
        var mainMenu = __instance.transform.parent.parent;
        var titleButtons = mainMenu.Find("Panel/Title Panel/Title Buttons");

        var alreadyInjected = titleButtons.Find("Archipelago Button");
        if(alreadyInjected != null) return;
        
        var quitButtonHolder = titleButtons.Find("Quit Button");
        var quitBhRt = quitButtonHolder.GetComponent<RectTransform>();
        var quitAnimator = quitButtonHolder.GetComponent<Animator>();
        var quitButton = quitButtonHolder.Find("Button");
        var quitBRt = quitButton.GetComponent<RectTransform>();
        var quitButtonImgComponent = quitButton.GetComponent<Image>();
        var quitMbl = quitButton.GetComponent<MenuButtonLockable>();
        var quitImage = quitButton.Find("Image");
        var quitImgBRt = quitImage.GetComponent<RectTransform>();
        var quitImgComponent = quitImage.GetComponent<Image>();
        var quitText = quitButton.Find("Text");
        var quitTextRt = quitText.GetComponent<RectTransform>();
        var quitTmp = quitText.GetComponent<TextMeshProUGUI>();

        var apButtonHolder = new GameObject();
        apButtonHolder.SetActive(false);
        apButtonHolder.name = "Archipelago Button";
        apButtonHolder.transform.parent = titleButtons;
        apButtonHolder.transform.localScale = quitButtonHolder.transform.localScale;
        apButtonHolder.layer = quitButtonHolder.gameObject.layer;
        var apBhRt = apButtonHolder.AddComponent<RectTransform>();
        apBhRt.anchorMin = quitBhRt.anchorMin;
        apBhRt.anchorMax = quitBhRt.anchorMax;
        apBhRt.anchoredPosition = quitBhRt.anchoredPosition;
        apBhRt.sizeDelta = quitBhRt.sizeDelta;
        apButtonHolder.AddComponent<CanvasRenderer>();
        var apMbh = apButtonHolder.AddComponent<MenuButtonHolder>();
        apMbh.onClickEvent = new Button.ButtonClickedEvent();
        var animator = apButtonHolder.AddComponent<Animator>();
        animator.runtimeAnimatorController = quitAnimator.runtimeAnimatorController;
        apMbh.animatorRef = animator;

        var apButton = new GameObject();
        apButton.name = "Button";
        apButton.transform.parent = apButtonHolder.transform;
        apButton.transform.localScale = quitButton.transform.localScale;
        apButton.layer = quitButton.gameObject.layer;
        var apBRt = apButton.AddComponent<RectTransform>();
        apBRt.anchorMin = quitBRt.anchorMin;
        apBRt.anchorMax = quitBRt.anchorMax;
        apBRt.anchoredPosition = quitBRt.anchoredPosition;
        apBRt.sizeDelta = quitBRt.sizeDelta;
        apButton.AddComponent<CanvasRenderer>();
        var apButtonImgComponent = apButton.AddComponent<Image>();
        apButtonImgComponent.enabled = false;
        apButtonImgComponent.sprite = quitButtonImgComponent.sprite;
        apButtonImgComponent.type = quitButtonImgComponent.type;
        var apButtonComponent = apButton.AddComponent<Button>();
        var mbl = apButton.AddComponent<MenuButtonLockable>();
        mbl._button = apButtonComponent;
        mbl._lockedStatusImage = quitMbl._lockedStatusImage;
        apButton.AddComponent<MenuButtonBase>();
        
        var apButtonImage = new GameObject();
        apButtonImage.name = "Image";
        apButtonImage.transform.parent = apButton.transform;
        apButtonImage.transform.localScale = quitImage.transform.localScale;
        apButtonImage.layer = quitImage.gameObject.layer;
        var apImgBRt = apButtonImage.AddComponent<RectTransform>();
        apImgBRt.anchorMin = quitImgBRt.anchorMin;
        apImgBRt.anchorMax = quitImgBRt.anchorMax;
        apImgBRt.anchoredPosition = quitImgBRt.anchoredPosition;
        apImgBRt.sizeDelta = quitImgBRt.sizeDelta;
        apButtonImage.AddComponent<CanvasRenderer>();
        var apImgComponent = apButtonImage.AddComponent<Image>();
        apImgComponent.sprite = quitImgComponent.sprite;
        apImgComponent.type = quitImgComponent.type;
        var apBUiCI = apButtonImage.AddComponent<UICopyImage>();
        apBUiCI.enabled = false;
        var apBUiCC = apButtonImage.AddComponent<UIColorCopy>();
        apBUiCC.Source = apButtonImgComponent;
        
        var apText = new GameObject();
        apText.name = "Text";
        apText.transform.parent = apButton.transform;
        apText.transform.localScale = quitText.transform.localScale;
        apText.layer = quitText.gameObject.layer;
        var apTextRt = apText.AddComponent<RectTransform>();
        apTextRt.anchorMin = quitTextRt.anchorMin;
        apTextRt.anchorMax = quitTextRt.anchorMax;
        apTextRt.anchoredPosition = quitTextRt.anchoredPosition;
        apTextRt.sizeDelta = quitTextRt.sizeDelta;
        apText.AddComponent<CanvasRenderer>();
        var apTmp = apText.AddComponent<TextMeshProUGUI>();
        apTmp.alignment = quitTmp.alignment;
        apTmp.alpha = quitTmp.alpha;
        apTmp.characterWidthAdjustment = quitTmp.characterWidthAdjustment;
        apTmp.font = quitTmp.font;
        apTmp.color = quitTmp.color;
        apTmp.enableAutoSizing = quitTmp.enableAutoSizing;
        apTmp.enableWordWrapping = quitTmp.enableWordWrapping;
        apTmp.fontSize = quitTmp.fontSize;
        apTmp.fontSizeMax = quitTmp.fontSizeMax;
        apTmp.fontSizeMin = quitTmp.fontSizeMin;
        apTmp.margin = quitTmp.margin;
        apTmp.text = "Archipelago";
        
        apButtonHolder.SetActive(true);
        __instance.buttonsToLoad.Add(apMbh);
    }
}
