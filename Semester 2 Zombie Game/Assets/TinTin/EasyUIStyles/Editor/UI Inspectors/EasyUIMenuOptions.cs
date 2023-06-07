using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.SceneManagement;
using TMPro.EditorUtilities;

namespace EasyUIStyle.UI
{
    public class EasyUIMenuOptions
    {
        //Menu items are turned off as I found them "too much"

        //[MenuItem("GameObject/UI/Text - Easy UI", false, 1)]
        //private static void AddText(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateText(GetStandardResources());
        //    ReplaceText(go);
        //    go.name = "Text (Easy UI)";

        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Image - Easy UI", false, 2)]
        //private static void AddImage(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateImage(GetStandardResources());
        //    ReplaceImages(go);
        //    go.name = "Image (Easy UI)";
        //    go.GetComponent<Image>().sprite = null;

        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Button - Easy UI", false, 3)]
        //private static void AddButton(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateButton(GetStandardResources());
        //    Object.DestroyImmediate(go.GetComponent<Button>());
        //    ReplaceImages(go);
        //    ReplaceText(go);
        //    go.AddComponent<EasyUIButton>();
        //    go.name = "Button (Easy UI)";
        //    EasyUIText _text = go.GetComponentInChildren<EasyUIText>();
        //    _text.text = "Easy UI Button";
        //    _text.color = Color.black;
        //    _text.alignment = TextAnchor.MiddleCenter;
        //    go.GetComponent<Image>().type = Image.Type.Sliced;
        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Toggle - Easy UI", false, 4)]
        //private static void AddToggle(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateToggle(GetStandardResources());
        //    Object.DestroyImmediate(go.GetComponent<Toggle>());
        //    Toggle toggle = go.AddComponent<EasyUIToggle>();
        //    go.name = "Toggle (Easy UI)";

        //    foreach (Transform child in go.transform)
        //    {
        //        if(child.name == "Background")
        //        {
        //            Object.DestroyImmediate(child.GetComponent<Image>());
        //            Image image = child.gameObject.AddComponent<EasyUIImage>();
        //            toggle.targetGraphic = image;
        //            toggle.isOn = true;
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);

        //            GameObject _go = child.Find("Checkmark").gameObject;
        //            Object.DestroyImmediate(_go.GetComponent<Image>());

        //            EasyUIImage _image = _go.gameObject.AddComponent<EasyUIImage>();
        //            _image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
        //            toggle.graphic = _image;
    
        //        }
        //        else if (child.name == "Label")
        //        {
        //            Object.DestroyImmediate(child.GetComponent<Text>());
        //            child.gameObject.AddComponent<EasyUIText>().text = "Easy UI Toggle";
        //        }
        //    }

        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Slider - Easy UI", false, 5)]
        //private static void AddSlider(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateSlider(GetStandardResources());
        //    Object.DestroyImmediate(go.GetComponent<Slider>());
        //    EasyUISlider slider = go.AddComponent<EasyUISlider>();
        //    go.name = "Slider (Easy UI)";

        //    EasyUIMenuOptions.ReplaceImages(go);

        //    foreach (Image image in go.GetComponentsInChildren<Image>())
        //    {
        //        if (image.name == "Background")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
        //            image.type = Image.Type.Sliced;
        //            image.fillCenter = true;
        //            image.pixelsPerUnitMultiplier = 1;
        //        }
        //        else if (image.name == "Handle")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
        //            slider.targetGraphic = image;
        //        }
        //        else if (image.name == "Fill")
        //        {
        //            image.type = Image.Type.Sliced;
        //            image.fillCenter = true;
        //            image.pixelsPerUnitMultiplier = 1;
        //        }
        //    }

        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Input Field - Easy UI", false, 6)]
        //private static void AddInputField(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateInputField(GetStandardResources());
        //    Object.DestroyImmediate(go.GetComponent<InputField>());


        //    ReplaceImages(go);
        //    ReplaceText(go);

        //    EasyUIInputField newInputField = go.AddComponent<EasyUIInputField>();
        //    go.name = "Input Field (Easy UI)";

        //    foreach (Text text in go.GetComponentsInChildren<Text>())
        //    {
        //        if (text.name == "Placeholder")
        //        {
        //            text.text = "Enter Text...";
        //            newInputField.placeholder = text;
        //        }
        //        else if (text.name == "Text")
        //        {
        //            newInputField.textComponent = text;
        //            text.supportRichText = false;
        //        }
        //    }

        //    Image newImage = newInputField.GetComponent<Image>();
        //    newImage.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
        //    newInputField.targetGraphic = newImage;
        //    newImage.type = Image.Type.Sliced;

        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Dropdown - Easy UI", false, 7)]
        //private static void AddDropdown(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = DefaultControls.CreateDropdown(GetStandardResources());
        //    go.name = "Dropdown (Easy UI)";
        //    go.transform.Find("Template").gameObject.SetActive(true);
        //    Dropdown dropDown = go.GetComponent<Dropdown>();
        //    List<Dropdown.OptionData> options = dropDown.options;
        //    RectTransform template = dropDown.template;

        //    ReplaceImages(go);
        //    ReplaceText(go);

        //    Image bg = go.GetComponent<Image>();
        //    bg.type = Image.Type.Sliced;
        //    bg.fillCenter = true;
        //    bg.pixelsPerUnitMultiplier = 1;

        //    Object.DestroyImmediate(go.GetComponent<Dropdown>());
        //    EasyUIDropdown newDropdown = go.AddComponent<EasyUIDropdown>();
        //    newDropdown.template = template;
        //    newDropdown.targetGraphic = bg;
        //    newDropdown.AddOptions(options);

        //    Transform item = go.GetComponentInChildren<Toggle>().transform;
        //    Object.DestroyImmediate(item.GetComponent<Toggle>());
        //    EasyUIToggle newToggle = item.gameObject.AddComponent<EasyUIToggle>();

        //    foreach (Text text in go.GetComponentsInChildren<Text>())
        //    {
        //        if (text.name == "Label")
        //        {
        //            text.text = "Option A";
        //            newDropdown.captionText = text;
        //        }
        //        else if (text.name == "Item Label")
        //        {
        //            text.text = "Option A";
        //            newDropdown.itemText = text;
        //        }
        //    }

        //    foreach (Image image in go.GetComponentsInChildren<Image>())
        //    {
        //        if (image.name == "Arrow")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
        //        }
        //        else if (image.name == "Viewport")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
        //        }
        //        else if (image.name == "Item Checkmark")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
        //            newToggle.graphic = image;

        //        }
        //        else if (image.name == "Scollbar")
        //        {
        //            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
        //        }
        //        else if(image.name == "Item Background")
        //        {
        //            newToggle.targetGraphic = image;
        //        }
        //    }

        //    go.transform.Find("Template").gameObject.SetActive(false);
        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Text - TMP with Easy UI", false, 2001)]
        //private static void AddTMPText(MenuCommand menuCommand)
        //{
        //    GameObject go = new GameObject();
        //    go.AddComponent<RectTransform>();
        //    go.AddComponent<EasyUITMProText>().text = "TMP with Easy UI";
        //    go.name = "Text (TMP & Easy UI)";
        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Input Field - TMP with Easy UI", false, 2037)]
        //private static void AddTMPInput(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = TMPro.TMP_DefaultControls.CreateInputField(GetStandardResourcesTMP());
        //    Object.DestroyImmediate(go.GetComponent<TMPro.TMP_InputField>());
        //    ReplaceImages(go);
        //    ReplaceTextTMP(go);
        //    EasyUITMProInput inputField = go.AddComponent<EasyUITMProInput>();
        //    go.name = "Input Field (TMP & Easy UI)";

        //    foreach (EasyUITMProText text in go.GetComponentsInChildren<EasyUITMProText>())
        //    {
        //        if (text.name == "Placeholder")
        //        {
        //            text.text = "Enter Text...";
        //            inputField.placeholder = text;
        //        }
        //        else if (text.name == "Text")
        //            inputField.textComponent = text;
        //    }

        //    Image image = inputField.GetComponent<Image>();
        //    image.type = Image.Type.Sliced;
        //    image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
        //    inputField.targetGraphic = image;
        //    PlaceUIElementRoot(go, menuCommand);
        //}

        //[MenuItem("GameObject/UI/Dropdown - TMP with Easy UI", false, 2037)]
        //private static void AddTMPDropdown(MenuCommand menuCommand)
        //{
        //    //Create standard button and replace Button component
        //    GameObject go = TMPro.TMP_DefaultControls.CreateDropdown(GetStandardResourcesTMP());
        //    Object.DestroyImmediate(go.GetComponent<TMPro.TMP_InputField>());
        //    ReplaceImages(go);
        //    ReplaceTextTMP(go);
        //    EasyUITMProInput inputField = go.AddComponent<EasyUITMProInput>();
        //    go.name = "Input Field (TMP & Easy UI)";

        //    foreach (EasyUITMProText text in go.GetComponentsInChildren<EasyUITMProText>())
        //    {
        //        if (text.name == "Placeholder")
        //        {
        //            text.text = "Enter Text...";
        //            inputField.placeholder = text;
        //        }
        //        else if (text.name == "Text")
        //            inputField.textComponent = text;
        //    }

        //    Image image = inputField.GetComponent<Image>();
        //    image.type = Image.Type.Sliced;
        //    image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
        //    inputField.targetGraphic = image;
        //    PlaceUIElementRoot(go, menuCommand);
        //}

        public static void ReplaceImages(GameObject go)
        {
            Image[] imageArray = go.GetComponentsInChildren<Image>();

            for (int i = 0; i < imageArray.Length; i++) 
            {
                GameObject tempGO = imageArray[i].gameObject;
                Object.DestroyImmediate(imageArray[i]);
                EasyUIImage image = tempGO.AddComponent<EasyUIImage>();
                image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
            }
        }

        public static void ReplaceText(GameObject go)
        {
            Text[] textArray = go.GetComponentsInChildren<Text>();

            for (int i = 0; i < textArray.Length; i++)
            {
                GameObject tempGO = textArray[i].gameObject;
                string _text = textArray[i].text;
                Color _color = textArray[i].color;
                Object.DestroyImmediate(textArray[i]);
                EasyUIText text = tempGO.AddComponent<EasyUIText>();
                text.text = _text;
                text.color = _color;
            }

            TMPro.TMP_Text[] _textArray = go.GetComponentsInChildren<TMPro.TMP_Text>();

            for (int i = 0; i < _textArray.Length; i++)
            {
                GameObject tempGO = _textArray[i].gameObject;
                string _text = _textArray[i].text;
                Object.DestroyImmediate(_textArray[i]);
                EasyUITMProText text = tempGO.AddComponent<EasyUITMProText>();
                text.text = _text;
            }
        }

        static private TMPro.TMP_DefaultControls.Resources s_StandardResourcesTMP;

        static private TMPro.TMP_DefaultControls.Resources GetStandardResourcesTMP()
        {
            if (s_StandardResourcesTMP.standard == null)
            {
                s_StandardResourcesTMP.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResourcesTMP.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResourcesTMP.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResourcesTMP.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResourcesTMP.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResourcesTMP.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResourcesTMP.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
            }
            return s_StandardResourcesTMP;
        }

        //below is Unity's UI creation code that is publicly available at https://bitbucket.org/Unity-Technologies/ui/src/2019.1/UnityEditor.UI/UI/MenuOptions.cs
        //minor modifications have been made to work here.
        private static void PlaceUIElementRoot(GameObject element, MenuCommand menuCommand)
        {
            GameObject parent = menuCommand.context as GameObject;
            bool explicitParentChoice = true;
            if (parent == null)
            {
                parent = GetOrCreateCanvasGameObject();
                explicitParentChoice = false;

                // If in Prefab Mode, Canvas has to be part of Prefab contents,
                // otherwise use Prefab root instead.
                PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
                if (prefabStage != null && !prefabStage.IsPartOfPrefabContents(parent))
                    parent = prefabStage.prefabContentsRoot;
            }
            if (parent.GetComponentsInParent<Canvas>(true).Length == 0)
            {
                // Create canvas under context GameObject,
                // and make that be the parent which UI element is added under.
                GameObject canvas = CreateNewUI();
                canvas.transform.SetParent(parent.transform, false);
                parent = canvas;
            }

            // Setting the element to be a child of an element already in the scene should
            // be sufficient to also move the element to that scene.
            // However, it seems the element needs to be already in its destination scene when the
            // RegisterCreatedObjectUndo is performed; otherwise the scene it was created in is dirtied.
            SceneManager.MoveGameObjectToScene(element, parent.scene);

            Undo.RegisterCreatedObjectUndo(element, "Create " + element.name);

            if (element.transform.parent == null)
            {
                Undo.SetTransformParent(element.transform, parent.transform, "Parent " + element.name);
            }

            GameObjectUtility.EnsureUniqueNameForSibling(element);

            // We have to fix up the undo name since the name of the object was only known after reparenting it.
            Undo.SetCurrentGroupName("Create " + element.name);

            GameObjectUtility.SetParentAndAlign(element, parent);
            if (!explicitParentChoice) // not a context click, so center in sceneview
                SetPositionVisibleinSceneView(parent.GetComponent<RectTransform>(), element.GetComponent<RectTransform>());

            Selection.activeGameObject = element;
        }

        // Helper function that returns a Canvas GameObject; preferably a parent of the selection, or other existing Canvas.
        static public GameObject GetOrCreateCanvasGameObject()
        {
            GameObject selectedGo = Selection.activeGameObject;

            // Try to find a gameobject that is the selected GO or one if its parents.
            Canvas canvas = (selectedGo != null) ? selectedGo.GetComponentInParent<Canvas>() : null;
            if (IsValidCanvas(canvas))
                return canvas.gameObject;

            // No canvas in selection or its parents? Then use any valid canvas.
            // We have to find all loaded Canvases, not just the ones in main scenes.
            Canvas[] canvasArray = StageUtility.GetCurrentStageHandle().FindComponentsOfType<Canvas>();
            for (int i = 0; i < canvasArray.Length; i++)
                if (IsValidCanvas(canvasArray[i]))
                    return canvasArray[i].gameObject;

            // No canvas in the scene at all? Then create a new one.
            return CreateNewUI();
        }

        static public GameObject CreateNewUI()
        {
            // Root for the UI
            var root = new GameObject("Canvas");
            root.layer = LayerMask.NameToLayer("UI");
            Canvas canvas = root.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            root.AddComponent<CanvasScaler>();
            root.AddComponent<GraphicRaycaster>();

            // Works for all stages.
            StageUtility.PlaceGameObjectInCurrentStage(root);
            bool customScene = false;
            PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                root.transform.SetParent(prefabStage.prefabContentsRoot.transform, false);
                customScene = true;
            }

            Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);

            // If there is no event system add one...
            // No need to place event system in custom scene as these are temporary anyway.
            // It can be argued for or against placing it in the user scenes,
            // but let's not modify scene user is not currently looking at.
            if (!customScene)
                CreateEventSystem(false);
            return root;
        }

        static bool IsValidCanvas(Canvas canvas)
        {
            if (canvas == null || !canvas.gameObject.activeInHierarchy)
                return false;

            // It's important that the non-editable canvas from a prefab scene won't be rejected,
            // but canvases not visible in the Hierarchy at all do. Don't check for HideAndDontSave.
            if (EditorUtility.IsPersistent(canvas) || (canvas.hideFlags & HideFlags.HideInHierarchy) != 0)
                return false;

            if (StageUtility.GetStageHandle(canvas.gameObject) != StageUtility.GetCurrentStageHandle())
                return false;

            return true;
        }

        private static void SetPositionVisibleinSceneView(RectTransform canvasRTransform, RectTransform itemTransform)
        {
            SceneView sceneView = SceneView.lastActiveSceneView;

            // Couldn't find a SceneView. Don't set position.
            if (sceneView == null || sceneView.camera == null)
                return;

            // Create world space Plane from canvas position.
            Vector2 localPlanePosition;
            Camera camera = sceneView.camera;
            Vector3 position = Vector3.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRTransform, new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2), camera, out localPlanePosition))
            {
                // Adjust for canvas pivot
                localPlanePosition.x = localPlanePosition.x + canvasRTransform.sizeDelta.x * canvasRTransform.pivot.x;
                localPlanePosition.y = localPlanePosition.y + canvasRTransform.sizeDelta.y * canvasRTransform.pivot.y;

                localPlanePosition.x = Mathf.Clamp(localPlanePosition.x, 0, canvasRTransform.sizeDelta.x);
                localPlanePosition.y = Mathf.Clamp(localPlanePosition.y, 0, canvasRTransform.sizeDelta.y);

                // Adjust for anchoring
                position.x = localPlanePosition.x - canvasRTransform.sizeDelta.x * itemTransform.anchorMin.x;
                position.y = localPlanePosition.y - canvasRTransform.sizeDelta.y * itemTransform.anchorMin.y;

                Vector3 minLocalPosition;
                minLocalPosition.x = canvasRTransform.sizeDelta.x * (0 - canvasRTransform.pivot.x) + itemTransform.sizeDelta.x * itemTransform.pivot.x;
                minLocalPosition.y = canvasRTransform.sizeDelta.y * (0 - canvasRTransform.pivot.y) + itemTransform.sizeDelta.y * itemTransform.pivot.y;

                Vector3 maxLocalPosition;
                maxLocalPosition.x = canvasRTransform.sizeDelta.x * (1 - canvasRTransform.pivot.x) - itemTransform.sizeDelta.x * itemTransform.pivot.x;
                maxLocalPosition.y = canvasRTransform.sizeDelta.y * (1 - canvasRTransform.pivot.y) - itemTransform.sizeDelta.y * itemTransform.pivot.y;

                position.x = Mathf.Clamp(position.x, minLocalPosition.x, maxLocalPosition.x);
                position.y = Mathf.Clamp(position.y, minLocalPosition.y, maxLocalPosition.y);
            }

            itemTransform.anchoredPosition = position;
            itemTransform.localRotation = Quaternion.identity;
            itemTransform.localScale = Vector3.one;
        }

        private static void CreateEventSystem(bool select)
        {
            CreateEventSystem(select, null);
        }

        private static void CreateEventSystem(bool select, GameObject parent)
        {
            StageHandle stage = parent == null ? StageUtility.GetCurrentStageHandle() : StageUtility.GetStageHandle(parent);
            var esys = stage.FindComponentOfType<EventSystem>();
            if (esys == null)
            {
                var eventSystem = new GameObject("EventSystem");
                if (parent == null)
                    StageUtility.PlaceGameObjectInCurrentStage(eventSystem);
                else
                    GameObjectUtility.SetParentAndAlign(eventSystem, parent);
                esys = eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();

                Undo.RegisterCreatedObjectUndo(eventSystem, "Create " + eventSystem.name);
            }

            if (select && esys != null)
            {
                Selection.activeGameObject = esys.gameObject;
            }
        }

        static private DefaultControls.Resources s_StandardResources;
        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";
        private const string kBackgroundSpritePath = "UI/Skin/Background.psd";
        private const string kInputFieldBackgroundPath = "UI/Skin/InputFieldBackground.psd";
        private const string kKnobPath = "UI/Skin/Knob.psd";
        private const string kCheckmarkPath = "UI/Skin/Checkmark.psd";
        private const string kDropdownArrowPath = "UI/Skin/DropdownArrow.psd";
        private const string kMaskPath = "UI/Skin/UIMask.psd";

        static private DefaultControls.Resources GetStandardResources()
        {
            if (s_StandardResources.standard == null)
            {
                s_StandardResources.standard = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
                s_StandardResources.background = AssetDatabase.GetBuiltinExtraResource<Sprite>(kBackgroundSpritePath);
                s_StandardResources.inputField = AssetDatabase.GetBuiltinExtraResource<Sprite>(kInputFieldBackgroundPath);
                s_StandardResources.knob = AssetDatabase.GetBuiltinExtraResource<Sprite>(kKnobPath);
                s_StandardResources.checkmark = AssetDatabase.GetBuiltinExtraResource<Sprite>(kCheckmarkPath);
                s_StandardResources.dropdown = AssetDatabase.GetBuiltinExtraResource<Sprite>(kDropdownArrowPath);
                s_StandardResources.mask = AssetDatabase.GetBuiltinExtraResource<Sprite>(kMaskPath);
            }
            return s_StandardResources;
        }

    }
}
