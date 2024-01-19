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

        var archipelagoButtonHolder = Object.Instantiate(quitButtonHolder, titleButtons.transform, true);
        archipelagoButtonHolder.name = "Archipelago Button";
        var archipelagoButtonHolderComponent = archipelagoButtonHolder.GetComponent<MenuButtonHolder>();
        archipelagoButtonHolderComponent.onClickEvent = new Button.ButtonClickedEvent();
        var archipelagoButtonText = archipelagoButtonHolder.Find("Button/Text");
        Object.Destroy(archipelagoButtonText.GetComponent<AxKLocalizedText>());
        var archipelagoTmpUgui = archipelagoButtonText.GetComponent<TextMeshProUGUI>();
        archipelagoTmpUgui.text = "Archipelago";

        var yesNoPopup = MainMenu.Instance()._popup;
        var customPopup = Object.Instantiate(yesNoPopup, yesNoPopup.transform.parent, true);
        customPopup.name = "Archipelago Popup";

        var customPopupContentHolder =
            customPopup.transform.Find("Window Holder/Popup Scaler/Popup Content Holder");

        var customPopupButtons =
            customPopupContentHolder.Find("Popup Buttons");
        var customPopupButtonsRt = customPopupButtons.GetComponent<RectTransform>();

        var archipelagoServerSettingsGroup = new GameObject();
        archipelagoServerSettingsGroup.SetActive(false);
        archipelagoServerSettingsGroup.name = "Server Settings";
        archipelagoServerSettingsGroup.layer = customPopupContentHolder.gameObject.layer;
        archipelagoServerSettingsGroup.transform.parent = customPopupContentHolder.transform;
        archipelagoServerSettingsGroup.transform.localScale = Vector3.one;
        archipelagoServerSettingsGroup.transform.localRotation = Quaternion.identity;
        var apSsgRt = archipelagoServerSettingsGroup.AddComponent<RectTransform>();
        apSsgRt.anchorMin = customPopupButtonsRt.anchorMin;
        apSsgRt.anchorMax = customPopupButtonsRt.anchorMax;
        apSsgRt.anchoredPosition = customPopupButtonsRt.anchoredPosition;
        apSsgRt.sizeDelta = customPopupButtonsRt.sizeDelta;
        archipelagoServerSettingsGroup.AddComponent<CanvasRenderer>();
        archipelagoServerSettingsGroup.AddComponent<HorizontalLayoutGroup>();

        var ipTextInput = new GameObject();
        ipTextInput.name = "IP Address Input";
        ipTextInput.layer = archipelagoServerSettingsGroup.layer;
        ipTextInput.transform.parent = archipelagoServerSettingsGroup.transform;
        ipTextInput.transform.localScale = Vector3.one / 2.0f;
        ipTextInput.transform.localRotation = Quaternion.identity;
        var ipTextInputRt = ipTextInput.AddComponent<RectTransform>();
        ipTextInputRt.anchorMin = apSsgRt.anchorMin;
        ipTextInputRt.anchorMax = apSsgRt.anchorMax;
        ipTextInputRt.anchoredPosition = apSsgRt.anchoredPosition;
        ipTextInputRt.sizeDelta = apSsgRt.sizeDelta;
        ipTextInput.AddComponent<CanvasRenderer>();
        var inputField = ipTextInput.AddComponent<TMP_InputField>();

        var ipTextArea = new GameObject();
        ipTextArea.name = "Text Area";
        ipTextArea.layer = ipTextInput.layer;
        ipTextArea.transform.parent = ipTextInput.transform;
        ipTextArea.transform.localScale = Vector3.one;
        ipTextArea.transform.localRotation = Quaternion.identity;
        var ipTextAreaRt = ipTextArea.AddComponent<RectTransform>();
        ipTextAreaRt.anchorMin = ipTextInputRt.anchorMin;
        ipTextAreaRt.anchorMax = ipTextInputRt.anchorMax;
        ipTextAreaRt.anchoredPosition = ipTextInputRt.anchoredPosition;
        ipTextAreaRt.sizeDelta = ipTextInputRt.sizeDelta;
        ipTextArea.transform.localPosition = Vector3.zero;

        var ipTextPlaceholder = new GameObject();
        ipTextPlaceholder.name = "Placeholder";
        ipTextPlaceholder.layer = ipTextArea.layer;
        ipTextPlaceholder.transform.parent = ipTextArea.transform;
        ipTextPlaceholder.transform.localScale = Vector3.one;
        ipTextPlaceholder.transform.localRotation = Quaternion.identity;
        var textPlaceholderRt = ipTextPlaceholder.AddComponent<RectTransform>();
        textPlaceholderRt.anchorMin = ipTextAreaRt.anchorMin;
        textPlaceholderRt.anchorMax = ipTextAreaRt.anchorMax;
        textPlaceholderRt.anchoredPosition = ipTextAreaRt.anchoredPosition;
        textPlaceholderRt.sizeDelta = ipTextAreaRt.sizeDelta;
        var textPlaceholderTmpUgui = ipTextPlaceholder.AddComponent<TextMeshProUGUI>();
        textPlaceholderTmpUgui.text = "archipelago.gg:1234";
        textPlaceholderTmpUgui.color = Color.gray;
        textPlaceholderTmpUgui.font = archipelagoTmpUgui.font;
        textPlaceholderTmpUgui.margin = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        var ipTextText = new GameObject();
        ipTextText.name = "Text";
        ipTextText.layer = ipTextArea.layer;
        ipTextText.transform.parent = ipTextArea.transform;
        ipTextText.transform.localScale = Vector3.one;
        ipTextText.transform.localRotation = Quaternion.identity;
        var textTextRt = ipTextText.AddComponent<RectTransform>();
        textTextRt.anchorMin = ipTextAreaRt.anchorMin;
        textTextRt.anchorMax = ipTextAreaRt.anchorMax;
        textTextRt.anchoredPosition = ipTextAreaRt.anchoredPosition;
        textTextRt.sizeDelta = ipTextAreaRt.sizeDelta;
        var textTextTmpUgui = ipTextText.AddComponent<TextMeshProUGUI>();
        textTextTmpUgui.text = "";
        textTextTmpUgui.color = Color.white;
        textTextTmpUgui.font = archipelagoTmpUgui.font;
        textTextTmpUgui.margin = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        inputField.textViewport = ipTextAreaRt;
        inputField.textComponent = textTextTmpUgui;
        inputField.fontAsset = archipelagoTmpUgui.font;
        inputField.placeholder = textPlaceholderTmpUgui;
        archipelagoServerSettingsGroup.SetActive(true);
        
        archipelagoButtonHolderComponent.onClickEvent.AddListener(() => {
            customPopup.SetPopup(
                "Connect to Archipelago?",
                () => { NeonWhiteAP.Log.LogInfo($"Confirm Connect: {inputField.text}"); },
                () => { NeonWhiteAP.Log.LogInfo("Cancel Connect"); }
            );
        });
        
        __instance.buttonsToLoad.Add(archipelagoButtonHolderComponent);
    }
}
