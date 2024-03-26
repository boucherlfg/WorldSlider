using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(WorldSlider))]
public class WorldSliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var slider = (WorldSlider)target;
        var value = slider.value;
        if (!slider.fill) return;
        slider.fill.localScale = new Vector3(value, 1, 1);
    }
    [MenuItem("World Slider", menuItem = "GameObject/2D Object/World Slider")]
    public static void CreateGameObject()
    {
        var slider = Resources.Load("Prefabs/Slider");
        var instance = Instantiate(slider, Selection.activeTransform);
        instance.name = slider.name;
        Selection.activeObject = instance;
    }
}
#endif


public class WorldSlider : MonoBehaviour
{
    [Range(0, 1)]
    public float value;
    public Transform background;

    public Transform fill;

    void Update()
    {
        fill.localScale = new Vector3(value, 1, 1);
    }
}
