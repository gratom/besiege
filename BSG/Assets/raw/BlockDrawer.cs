#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
[CustomPropertyDrawer(typeof(Block))]
public class BlockDrawer : PropertyDrawer
{
    // Height of a single line in the inspector
    private const float lineHeight = 18f;

    // Vertical spacing between lines
    private const float verticalSpacing = 2f;

    // Width of the preview image
    private const float previewImageWidth = 50;

    // Height of the preview image
    private const float previewImageHeight = 50;

    // Draw the property fields
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        Rect singleFieldRect = new Rect(position.x, position.y, position.width, lineHeight);

        // Draw ID and name fields in the same row
        Rect idRect = new Rect(singleFieldRect.x, singleFieldRect.y, position.width / 2f, lineHeight);
        EditorGUI.PropertyField(idRect, property.FindPropertyRelative("ID"), GUIContent.none);
        Rect nameRect = new Rect(singleFieldRect.x + position.width / 2f, singleFieldRect.y, position.width / 2f, lineHeight);
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
        singleFieldRect.y += lineHeight + verticalSpacing;

        // Draw blockType and material fields in the same row
        Rect blockTypeRect = new Rect(singleFieldRect.x, singleFieldRect.y, position.width / 2f, lineHeight);
        EditorGUI.PropertyField(blockTypeRect, property.FindPropertyRelative("blockType"));
        Rect materialRect = new Rect(singleFieldRect.x + position.width / 2f, singleFieldRect.y, position.width / 2f, lineHeight);
        EditorGUI.PropertyField(materialRect, property.FindPropertyRelative("material"));
        singleFieldRect.y += lineHeight + verticalSpacing;

        // Draw cost field
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("costGold"));
        singleFieldRect.y += lineHeight + verticalSpacing;
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("costWood"));
        singleFieldRect.y += lineHeight + verticalSpacing;
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("costSteel"));
        singleFieldRect.y += lineHeight + verticalSpacing;
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("costFabric"));
        singleFieldRect.y += lineHeight + verticalSpacing;
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("costFuel"));
        singleFieldRect.y += lineHeight + verticalSpacing;


        // Draw sprite field
        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("sprite"));
        singleFieldRect.y += lineHeight + verticalSpacing;

        // Draw sprite preview image
        Rect previewRect = new Rect(singleFieldRect.x, singleFieldRect.y, previewImageWidth, previewImageHeight);
        Sprite spriteTexture = property.FindPropertyRelative("sprite").objectReferenceValue as Sprite;
        if (spriteTexture != null)
        {
            EditorGUI.DrawTextureTransparent(previewRect, spriteTexture.texture, ScaleMode.ScaleToFit);
        }

        // Move down to make space for the preview image
        singleFieldRect.y += previewImageHeight + verticalSpacing;

        EditorGUI.PropertyField(singleFieldRect, property.FindPropertyRelative("spriteUrl"));

        singleFieldRect.y += lineHeight + verticalSpacing + verticalSpacing;

        Rect lineRect = new Rect(position.x, singleFieldRect.y, position.width, 1f);
        EditorGUI.DrawRect(lineRect, Color.black);

        EditorGUI.EndProperty();
    }

    // Calculate the height of the property fields
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Total height of all fields plus the height of the preview image
        return 9 * (lineHeight + verticalSpacing) + previewImageHeight + verticalSpacing * 5;
    }
}
#endif
