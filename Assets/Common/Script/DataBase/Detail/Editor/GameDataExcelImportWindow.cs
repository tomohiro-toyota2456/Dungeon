using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using Excel;
using System.Linq;

public class GameDataExcelImportWindow : EditorWindow
{
  WeponExcelImpoter weponImp;
  ArmorExcelImpoter armorImp;
  EnemyExcelImpoter enemyImp;
  EnemyPopExcelImpoter enemyPopImp;
  ItemDropExcelImpoter dropImp;
  DungeonExcelImporter dungeonImp;

  [MenuItem("GameDataExcel/OpenImportWindow")]
  static void OpenWindow()
  {
    GetWindow<GameDataExcelImportWindow>();
  }

  private void OnEnable()
  {
    weponImp = new WeponExcelImpoter();
    armorImp = new ArmorExcelImpoter();
    enemyImp = new EnemyExcelImpoter();
    enemyPopImp = new EnemyPopExcelImpoter();
    dropImp = new ItemDropExcelImpoter();
    dungeonImp = new DungeonExcelImporter();
  }

  private void OnGUI()
  {
    weponImp.OnGUI();
    armorImp.OnGUI();
    enemyImp.OnGUI();
    enemyPopImp.OnGUI();
    dropImp.OnGUI();
    dungeonImp.OnGUI();
  }
}

public class ExcelImporter
{
  protected string excelPath;
  protected string title;

  public void OnGUI()
  {
    using (new EditorGUILayout.HorizontalScope())
    {
      GUILayout.Label(title);
      excelPath = GUILayout.TextField(excelPath);
      if(GUILayout.Button("Create"))
      {
        ExecButton();
      }
    }
  }

  protected virtual void ExecButton()
  {

  }
}

public class WeponExcelImpoter : ExcelImporter
{
  public WeponExcelImpoter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/WeponData.xlsx";
    title = "Wepon";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<WeponParam> list = new List<WeponParam>();

    for(int i = 1; ;i++)
    {
      string id = importer.GetCellData(i,0);

      if (string.IsNullOrEmpty(id))
        break;

      string name =   importer.GetCellData(i, 1);
      string minAtk = importer.GetCellData(i, 2);
      string maxAtk = importer.GetCellData(i, 3);
      string dura =   importer.GetCellData(i, 4);
      string cri = importer.GetCellData(i, 5);
      string eType =  importer.GetCellData(i, 6);
      string wType =  importer.GetCellData(i, 7);
      string imageId =importer.GetCellData(i, 8);

      WeponParam param = ScriptableObject.CreateInstance<WeponParam>();
      param.Id = int.Parse(id);
      param.Name = name;
      param.MinAtk = float.Parse(minAtk);
      param.MaxAtk = float.Parse(maxAtk);
      param.Durability = int.Parse(dura);
      param.Critical = int.Parse(cri);
      param.eType = (WeponParam.EffectType)System.Enum.Parse(typeof(WeponParam.EffectType), eType);
      param.Type = (WeponParam.WeponType)System.Enum.Parse(typeof(WeponParam.WeponType), wType);
      param.ImageId = int.Parse(imageId);

      AssetDatabase.CreateAsset(param,"Assets/SceneData/Game/ScriptableObjectData/Wepon/wepon" + id + ".asset");

      list.Add(param);
      
    }

    string nowScene = SceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    WeponDataBase[] obj = (WeponDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(WeponDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);
  }
}

public class ArmorExcelImpoter : ExcelImporter
{
  public ArmorExcelImpoter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/ArmorData.xlsx";
    title = "Armor";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<ArmorParam> list = new List<ArmorParam>();

    for(int i = 1; ; i++)
    {
      string id = importer.GetCellData(i, 0);
      if (string.IsNullOrEmpty(id))
        break;

      string name =    importer.GetCellData(i, 1);
      string def =     importer.GetCellData(i, 2);
      string dura =    importer.GetCellData(i, 3);
      string imageId = importer.GetCellData(i, 4);

      ArmorParam param = ScriptableObject.CreateInstance<ArmorParam>();
      param.Id = int.Parse(id);
      param.Name = name;
      param.Def = float.Parse(def);
      param.ImageId = int.Parse(imageId);
      param.Durability = int.Parse(dura);
      AssetDatabase.CreateAsset(param, "Assets/SceneData/Game/ScriptableObjectData/Armor/armor" + id + ".asset");

      list.Add(param);
    }

    string nowScene = EditorSceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    ArmorDataBase[] obj = (ArmorDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(ArmorDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);

  }
}

public class EnemyExcelImpoter : ExcelImporter
{
  public EnemyExcelImpoter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/EnemyData.xlsx";
    title = "Enemy";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<EnemyParamBase> list = new List<EnemyParamBase>();

    for (int i = 1; ; i++)
    {
      string id = importer.GetCellData(i, 0);
      if (string.IsNullOrEmpty(id))
        break;

      string name =        importer.GetCellData(i, 1);
      string minAtk =      importer.GetCellData(i, 2);
      string maxAtk =      importer.GetCellData(i, 3);
      string def =         importer.GetCellData(i, 4);
      string cri =         importer.GetCellData(i, 5);
      string hp =          importer.GetCellData(i, 6);
      string dropTableId = importer.GetCellData(i, 7);
      string imageId =     importer.GetCellData(i, 8);
      string drop =        importer.GetCellData(i, 9);
      string etype       = importer.GetCellData(i, 10);

      EnemyParamBase param = ScriptableObject.CreateInstance<EnemyParamBase>();
      param.Id = int.Parse(id);
      param.EnemyName = name;
      param.MinAtk = float.Parse(minAtk);
      param.MaxAtk = float.Parse(maxAtk);
      param.Def = float.Parse(def);
      param.Critical = int.Parse(cri);
      param.MaxHp = int.Parse(hp);
      param.DropTableId = int.Parse(dropTableId);
      param.ImageId = int.Parse(imageId);
      param.DropPer = int.Parse(drop);
      param.Type = (EnemyParamBase.EnemyType)System.Enum.Parse(typeof(EnemyParamBase.EnemyType), etype);

      AssetDatabase.CreateAsset(param, "Assets/SceneData/Game/ScriptableObjectData/Enemy/enemy" + id + ".asset");
      list.Add(param);
    }

    string nowScene = EditorSceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    EnemyDataBase[] obj = (EnemyDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);

  }
}

public class EnemyPopExcelImpoter : ExcelImporter
{
  public EnemyPopExcelImpoter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/EnemyPopTable.xlsx";
    title = "EnemyPopTable";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<EnemyPopTable> list = new List<EnemyPopTable>();

    for (int i = 1; ; i++)
    {
      string id = importer.GetCellData(i, 0);
      if (string.IsNullOrEmpty(id))
        break;

      string enemyids = importer.GetCellData(i, 1);
      int[] ids = CsvParser.ParseInt(enemyids);

      EnemyPopTable param = ScriptableObject.CreateInstance<EnemyPopTable>();
      param.Id = int.Parse(id);
      param.EnemyIds = ids;


      AssetDatabase.CreateAsset(param, "Assets/SceneData/Game/ScriptableObjectData/EnemyPopTable/enemypoptable" + id + ".asset");
      list.Add(param);
    }

    string nowScene = EditorSceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    EnemyPopTableDataBase[] obj = (EnemyPopTableDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(EnemyPopTableDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);
  }
}

public class ItemDropExcelImpoter : ExcelImporter
{
  public ItemDropExcelImpoter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/DropTable.xlsx";
    title = "ItemDropTable";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<DropTable> list = new List<DropTable>();

    for (int i = 1; ; i++)
    {
      string id = importer.GetCellData(i, 0);
      if (string.IsNullOrEmpty(id))
        break;

      int[] weponIds = CsvParser.ParseInt(importer.GetCellData(i, 1));
      int[] armorIds = CsvParser.ParseInt(importer.GetCellData(i, 2));

      DropTable param = ScriptableObject.CreateInstance<DropTable>();

      List<DropTable.Data> dlist = new List<DropTable.Data>();
      if (weponIds != null)
      {
        for (int j = 0; j < weponIds.Length; j++)
        {
          DropTable.Data d = new DropTable.Data();
          d.id = weponIds[j];
          d.dropType = DropTable.DropType.Wepon;
          dlist.Add(d);
        }
      }

      if (armorIds != null)
      {
        for (int j = 0; j < armorIds.Length; j++)
        {
          DropTable.Data d = new DropTable.Data();
          d.id = armorIds[j];
          d.dropType = DropTable.DropType.Armor;
          dlist.Add(d);
        }
      }

      param.Id = int.Parse(id);
      param.DropData = dlist.ToArray();

      AssetDatabase.CreateAsset(param, "Assets/SceneData/Game/ScriptableObjectData/DropItemTable/dropitemtable" + id + ".asset");

      list.Add(param);
    }

    string nowScene = EditorSceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    DropItemTableDataBase[] obj = (DropItemTableDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(DropItemTableDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);
  }
}

public class DungeonExcelImporter : ExcelImporter
{
  public DungeonExcelImporter()
  {
    excelPath = "Assets/SceneData/Game/Editor/Excel/DungeonData.xlsx";
    title = "dungeon";
  }

  protected override void ExecButton()
  {
    Excel.ExcelReader importer = new ExcelReader();
    importer.Open(excelPath);
    importer.SetSheet(0);

    List<DungeonData> list = new List<DungeonData>();

    for (int i = 1; ; i++)
    {
      string id = importer.GetCellData(i, 0);
      if (string.IsNullOrEmpty(id))
        break;

      string name =     importer.GetCellData(i, 1);
      string tableids = importer.GetCellData(i, 2);
      string minAtkOp = importer.GetCellData(i, 3);
      string maxAtkOp = importer.GetCellData(i, 4);
      string minDefOp = importer.GetCellData(i, 5);
      string maxDefOp = importer.GetCellData(i, 6);
      string minCtOp =  importer.GetCellData(i, 7);
      string maxCtOp =  importer.GetCellData(i, 8);
      string minDuraOp = importer.GetCellData(i, 9);
      string maxDuraOp = importer.GetCellData(i, 10);
      string musicId = importer.GetCellData(i, 11);

      DungeonData param = ScriptableObject.CreateInstance<DungeonData>();
      param.DungeonName = name;
      param.Id = int.Parse(id);
      param.MinAtkOp = int.Parse(minAtkOp);
      param.MaxAtkOp = int.Parse(maxAtkOp);
      param.MinDefOp = int.Parse(minDefOp);
      param.MaxDefOp = int.Parse(maxDefOp);
      param.MinCtOp = int.Parse(minCtOp);
      param.MaxCtOp = int.Parse(maxCtOp);
      param.MinDuraOp = int.Parse(minDuraOp);
      param.MaxDuraOp = int.Parse(maxDuraOp);
      param.MusicId = int.Parse(musicId);

      param.AppearanceTableIds = CsvParser.ParseInt(tableids);

      AssetDatabase.CreateAsset(param, "Assets/SceneData/Game/ScriptableObjectData/Dungeon/dungeon" + id + ".asset");

      list.Add(param);
    }

    string nowScene = EditorSceneManager.GetActiveScene().path;
    Scene saveScene = EditorSceneManager.OpenScene("Assets/Scene/SceneManager.unity");

    DungeonDataBase[] obj = (DungeonDataBase[])UnityEngine.Object.FindObjectsOfType(typeof(DungeonDataBase));
    obj[0].SetData(list.ToArray());
    EditorSceneManager.MarkSceneDirty(saveScene);
    EditorSceneManager.SaveOpenScenes();

    EditorSceneManager.OpenScene(nowScene);
  }
}
