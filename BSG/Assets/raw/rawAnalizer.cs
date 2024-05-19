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

    public bool ToIgnore;
    public bool UseIgnore;
    public List<int> Ignore;

    public TextAsset machine;

    private Dictionary<int, Block> blocksDic;

    [ContextMenu("COST")]
    public void CostMachine()
    {
        List<string> prepared = machine.text.Split("<Block id=\"").Skip(1).ToList();
        List<int> blocksIDs = new List<int>();

        int countOfTanks = 0;
        foreach (string s in prepared)
        {
            int pos = s.IndexOf("\" guid");
            int id = int.Parse(s.Substring(0, pos));
            blocksIDs.Add(id);

            if (id == 35) //ballast
            {
                countOfTanks++;
            }
        }

        if (ToIgnore)
        {
            Ignore = blocksIDs;
        }

        if (UseIgnore)
        {
            foreach (int i in Ignore)
            {
                if (blocksIDs.Contains(i))
                {
                    blocksIDs.Remove(i);
                }
                if (i == 35)
                {
                    countOfTanks--;
                }
            }
        }

        blocksDic = new Dictionary<int, Block>();
        foreach (Block block in blocks)
        {
            blocksDic.Add(block.ID, block);
        }

        int sumGold = 0;
        int sumWood = 0;
        int sumSteel = 0;
        int sumFabric = 0;
        float sumFuel = 0;

        int goldForConsumables = 0;

        foreach (int blocksID in blocksIDs)
        {
            if (blocksDic[blocksID].blockType != BlockType.other)
            {
                sumGold += blocksDic[blocksID].costGold;
                sumWood += blocksDic[blocksID].costWood;
                sumSteel += blocksDic[blocksID].costSteel;
                sumFabric += blocksDic[blocksID].costFabric;
                sumFuel += blocksDic[blocksID].costFuel;
            }
            else
            {
                goldForConsumables += blocksDic[blocksID].costGold;
            }
        }

        string strOut = $"Gold : m{sumGold} + c{goldForConsumables} = t{sumGold + goldForConsumables} \nWood : {sumWood}\nSteel : {sumSteel}\nFabric : {sumFabric}\nFuel total : {sumFuel:0.000}\nFuel tanks : {countOfTanks}";
        if (countOfTanks == 0)
        {
            strOut += "\nNo Tanks!!!";
        }
        else
        {
            strOut += "\nFuel result value = " + (Mathf.RoundToInt(sumFuel / countOfTanks * 100f) / 100f).ToString("0.00");
        }
        if (goldForConsumables != 0)
        {
            strOut += "\nConsumables : " + goldForConsumables;
        }
        Debug.Log(strOut);


    }

    [ContextMenu("Parse")]
    public void Parse()
    {

        List<string> raw = text.text.Split("<td><a href=").Skip(1).ToList();

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
    public int costGold;
    public int costWood;
    public int costSteel;
    public int costFabric;
    public float costFuel;
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
