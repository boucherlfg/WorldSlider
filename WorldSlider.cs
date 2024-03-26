using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(WorldSlider))]
public class WorldSliderEditor : Editor
{
    [MenuItem("WorldSlider", menuItem = "GameObject/2D Object/World Slider")]
    public static void CreateWorldSlider()
    {
        var sprite = Sprite.Create(new Texture2D(100, 100), new Rect(0, 0, 100, 100), Vector2.up / 2);

        // create root 
        var slider = new GameObject("WorldSlider");
        slider.transform.localScale = new Vector3(1, 0.1f, 1);
        var sliderComp = slider.AddComponent<WorldSlider>();
        sliderComp.value = 1;

        // create background and make it red
        var background = new GameObject("Background");
        background.transform.SetParent(slider.transform);
        background.transform.localScale = Vector3.one;
        sliderComp.background = background.transform;

        var comp = background.AddComponent<SpriteRenderer>();
        comp.sprite = sprite;
        comp.color = Color.black;
        
        // create fill and make it black
        var fill = new GameObject("Fill");
        fill.transform.SetParent(background.transform);
        fill.transform.localScale = Vector3.one;
        sliderComp.fill = fill.transform;

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
