using UnityEditor;
using System.IO;
using System.Linq;
using UnityEngine;

// このスクリプトはGeminiを用いて作成されました
// 新規作成されたC#スクリプトのテンプレート内の「#NAMESPACE#」を、ファイルの保存場所に基づいて自動的に置換するエディタースクリプト
public class NamespaceAutomator : AssetModificationProcessor
{
    public static void OnWillCreateAsset(string assetPath)
    {
        assetPath = assetPath.Replace(".meta", "");
        
        if (!assetPath.EndsWith(".cs")) return;

        EditorApplication.delayCall += () =>
        {
            if (!File.Exists(assetPath)) return;

            string content = File.ReadAllText(assetPath);

            if (content.Contains("#NAMESPACE#"))
            {
                string directory = Path.GetDirectoryName(assetPath);
                
                // 1. 無視するフォルダ群
                string[] skipFolders = { "Assets", "Script", "Plugins" };
                
                // 2. ネームスペースに含める階層の「最大深さ」を指定
                int maxDepth = 1; 
                

                string[] parts = directory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                
                // フォルダ名を除外＆整形し、先頭から maxDepth の数だけ取得する (.Take)
                var validFolders = parts
                    .Where(p => !skipFolders.Contains(p))
                    .Select(p => p.Replace(" ", ""))
                    .Take(maxDepth);

                // 置換と保存
                content = content.Replace("#NAMESPACE#", string.Join(".", validFolders));
                File.WriteAllText(assetPath, content);
                
                AssetDatabase.ImportAsset(assetPath);
            }
        };
    }
}