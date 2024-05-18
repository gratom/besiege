using UnityEngine;
using UnityEditor;
using System.IO;
public class ANYoTXT : AssetPostprocessor
{
    private static string[] s_extensions = new string[] { "bsg" };

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromPath)
    {
        foreach (string asset in importedAssets)
        {
            string lowered = asset.ToLower();
            bool doConvert = false;
            string foundExt = "";
            foreach (string eachExt in s_extensions)
            {
                if (lowered.EndsWith("." + eachExt))
                {
                    foundExt = eachExt;
                    doConvert = true;
                    break;
                }
            }

            if (doConvert)
            {
                if (!File.Exists(asset))
                {
                    continue;
                }
                Debug.Log("Converting " + foundExt + " to TXT: " + asset.ToLower());

                string destFile = Path.GetDirectoryName(asset) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(asset) + ".txt";

                //Debug.Log(asset + " -> " + destFile);
                File.Copy(asset, destFile, true);
                AssetDatabase.ImportAsset(destFile);
            }
        }
    }
}
