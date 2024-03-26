using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(WorldSlider))]
public class WorldSliderEditor : Editor
{
    [MenuItem("WorldSlider", menuItem = "GameObject/2D Object/World Slider")]
    public static void CreateWorldSlider()
    {
        
        // create root 
        var slider = new GameObject("WorldSlider");

        if (Selection.activeTransform)
        {
            slider.transform.SetParent(Selection.activeTransform);
        }

        slider.transform.localScale = Vector3.one;
        var sliderComp = slider.AddComponent<WorldSlider>();
        sliderComp.value = 1;


        // create background and make it red
        var background = new GameObject("Background");
        background.transform.SetParent(slider.transform);
        background.transform.localScale = new Vector3(1, 0.1f, 1);
        background.transform.localPosition = Vector3.zero;
        sliderComp.background = background.transform;

        var sprite = Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), Vector2.one / 2);

        var comp = background.AddComponent<SpriteRenderer>();
        comp.sprite = sprite;
        comp.color = Color.black;
        
        // create fill and make it black
        var fill = new GameObject("Fill");
        fill.transform.SetParent(background.transform);
        fill.transform.localScale = Vector3.one;
        fill.transform.localPosition = Vector3.left / 2;
        sliderComp.fill = fill.transform;

        sprite = Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), Vector2.up / 2);
        comp = fill.AddComponent<SpriteRenderer>();
        comp.sprite = sprite;
        comp.color = Color.red;
        comp.sortingOrder = 1;
        EditorUtility.SetDirty(slider);
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var slider = (WorldSlider)target;
        slider.value = Mathf.Clamp(slider.value, 0, 1);
        slider.background.GetComponent<SpriteRenderer>().color = slider.backgroundColor;
        slider.fill.GetComponent<SpriteRenderer>().color = slider.fillColor;
        slider.fill.localScale = new Vector3(slider.value, 1, 1);
    }
}
#endif

public class WorldSlider : MonoBehaviour
{
    [Range(0, 1)]
    public float value = 1;
    public Transform background;
    public Transform fill;

    public Color fillColor = Color.red;
    public Color backgroundColor = Color.black;

    private SpriteRenderer backRenderer;
    private SpriteRenderer fillRenderer;
    private void Start()
    {
        backRenderer = background.GetComponent<SpriteRenderer>();
        fillRenderer = fill.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        value = Mathf.Clamp(value, 0, 1);
        fill.localScale = new Vector3(value, 1, 1);

        backRenderer.color = backgroundColor;
        fillRenderer.color = fillColor;
    }
}
