using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDataBase : DataBase
{
  int[] addedOptionPer = new int[3] { 50, 40, 20 };

  public EquipmentOptionBase[] CalcWeponOption(float atkParam,int ctParam,int duraParam,int minAtkOpt,int maxAtkOpt,int minCtOpt,int maxCtOpt,int minDuraOpt,int maxDuraOpt)
  {
    EquipmentOptionBase[] options = new EquipmentOptionBase[GameCommon.MaxOptionNum];
    for(int i = 0; i < GameCommon.MaxOptionNum; i++)
    {
      options[i] = new EquipmentOptionBase();
      if (Random.Range(0, 100) >= addedOptionPer[i])
        return options;

      int optJudge = Random.Range(0, 100);

      if(optJudge >= 67)
      {
        int optParam = Random.Range(minDuraOpt, maxDuraOpt + 1);
        int diff = duraParam + optParam;
        optParam = diff <= 0 ? optParam - diff + 1 : optParam;
        duraParam += optParam;

        options[i].Durability = optParam;
        options[i].Name = "耐久";
      }
      else if(optJudge >= 34)
      {
        int optParam = Random.Range(minCtOpt, maxCtOpt + 1);
        int diff = duraParam + optParam;
        optParam = diff <= 0 ? optParam - diff + 1 : optParam;
        ctParam += optParam;

        options[i].Critical = optParam;
        options[i].Name = "クリティカル";
      }
      else
      {
        int optParam = Random.Range(minAtkOpt, maxAtkOpt + 1);
        int diff = (int)atkParam + optParam;

        //数値が0以下になる場合は反転させて１足す
        optParam = diff <= 0 ? optParam-diff + 1 : optParam;
        atkParam += optParam;

        options[i].Atk = optParam;
        options[i].Name = "Attack";
      }
      

    }

    return options;
  }

  public EquipmentOptionBase[] CalcArmorOption(float defParam,int duraParam,int minDefOpt,int maxDefOpt,int minDuraOpt,int maxDuraOpt)
  {
    EquipmentOptionBase[] options = new EquipmentOptionBase[GameCommon.MaxOptionNum];
    for (int i = 0; i < GameCommon.MaxOptionNum; i++)
    {
      options[i] = new EquipmentOptionBase();
      if (Random.Range(0, 100) >= addedOptionPer[i])
        return options;


      int optJudge = Random.Range(0, 100);

      if (optJudge >= 49)
      {
        int optParam = Random.Range(minDuraOpt, maxDuraOpt + 1);
        int diff = duraParam + optParam;
        optParam = diff <= 0 ? optParam - diff + 1 : optParam;
        duraParam += optParam;

        options[i].Durability = optParam;
        options[i].Name = "耐久";
      }
      else
      {
        int optParam = Random.Range(minDefOpt, maxDefOpt + 1);
        int diff = (int)defParam + optParam;

        //数値が0以下になる場合は反転させて１足す
        optParam = diff <= 0 ? optParam - diff + 1 : optParam;
        defParam += optParam;

        options[i].Def = optParam;
        options[i].Name = "Deffence";
      }
    }

    return options;
  }
}
