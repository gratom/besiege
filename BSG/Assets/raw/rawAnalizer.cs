using System;
using System.ArrayExtensions;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "rawAnalizer", menuName = "Scriptables/anal", order = 51)]
public class rawAnalizer : ScriptableObject
{
    public TextAsset text;
    public string savePath; // Path to save the image in the Unity project

    public bool reloadImages;
    public bool getImagesFromDisk;

    public List<Block> blocks;
    public List<Sprite> sprites;

    [ContextMenu("Parse")]
    public void Parse()
    {
        //blocks = new List<Block>();

        List<string> raw = text.text.Split("<td><a href=").Skip(1).ToList();

        // for (int i = raw.Count - 1; i >= 0; i--)
        // {
        //     if (i % 2 != 0)
        //     {
        //         raw.RemoveAt(i);
        //     }
        // }

        for (int i = 0; i < raw.Count; i++)
        {
            string s = raw[i];
            int start = s.IndexOf("title=\"") + 7;
            int finish = s.IndexOf("\"><img alt=");

            List<string> sList = s.Split("<td>").ToList();
            for (int index = 0; index < sList.Count; index++)
            {
                sList[index] = sList[index].Replace("</td>", "");

                //Debug.Log(sList[index]);
            }

            blocks[i].name = s.Substring(start, finish - start);
            blocks[i].ID = int.Parse(sList[1]);
            blocks[i].material = sList[3].Contains("Yes") ? BlockMaterial.wood : BlockMaterial.steel;

            start = sList[0].IndexOf("src=\"") + 5;
            finish = sList[0].IndexOf("\" decoding=");
            blocks[i].spriteUrl = sList[0].Substring(start, finish - start);

            if (reloadImages)
            {
                // Download the image
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(blocks[i].spriteUrl);
                int j = i;
                www.SendWebRequest().completed += operation =>
                {
                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError("Failed to download image: " + www.error);
                        return;
                    }

                    // Save the image to the specified path
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    SaveTextureToFile(texture, savePath + blocks[j].name + ".png");

                    // Convert the texture to sprite
                    Sprite sprite = SpriteFromTexture(texture);

                    // Set the sprite to the UI Image component
                    blocks[j].sprite = sprite;
                };
            }

            if (getImagesFromDisk)
            {
                blocks[i].sprite = sprites.FirstOrDefault(x => x.name == blocks[i].name);
            }
        }
    }



    // Save texture to file
    private void SaveTextureToFile(Texture2D texture, string filePath)
    {
        byte[] bytes = texture.EncodeToPNG();

        string directoryPath = Path.Combine(Application.dataPath, Path.GetDirectoryName(filePath));

        // Create directory if it doesn't exist
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string fullPath = Path.Combine(Application.dataPath, filePath);

        File.WriteAllBytes(fullPath, bytes);
        Debug.Log("Image saved to: " + fullPath);
    }

    // Convert texture to sprite
    private Sprite SpriteFromTexture(Texture2D texture)
    {
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
        return sprite;
    }


}

[Serializable]
public class Block
{
    public string name;
    public int ID;
    public BlockType blockType;
    public BlockMaterial material;
    public int cost;
    public Sprite sprite;
    public string spriteUrl;
}


// Custom property drawer for the Block class

public enum BlockMaterial
{
    wood,
    steel
}

public enum BlockType
{
    main,
    basic,
    mech,
    other
}
