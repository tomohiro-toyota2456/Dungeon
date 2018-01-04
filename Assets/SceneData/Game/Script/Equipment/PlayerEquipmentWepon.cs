/**********************************************************
 * PlayerEquipmentWepon
 * Author harada
 * *******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**********************************************************
 * PlayerEquipmentWepon
 * 武器制御
 * *******************************************************/
public class PlayerEquipmentWepon
{
  WeponParam.WeponType weponType;
  WeponParam wepon;

  //装備オプション
  EquipmentOptionBase[] options = new EquipmentOptionBase[GameCommon.MaxOptionNum];

  WeponParam Wepon
  {
    set
    {
      if(value.Type != weponType)
      {
        throw new System.FormatException(weponType.ToString() + "に別のタイプの武器をセットしようとしています");
      }

      wepon = value;
    }
  }

  //コンストラクタで武器タイプを決める
  public PlayerEquipmentWepon(WeponParam.WeponType type)
  {
    weponType = type;
  }

  //装備装着
  public void Equip(WeponParam wepon,EquipmentOptionBase opt1, EquipmentOptionBase opt2, EquipmentOptionBase opt3)
  {
    Wepon = wepon;
    options[0] = opt1;
    options[1] = opt2;
    options[2] = opt3;
   }

  public int Id { get { return wepon.Id; } }
  public int ImageId { get { return wepon.ImageId; } }
  public WeponParam.EffectType EffectType { get { return wepon.eType; } }


  //最大使用回数計算
  public int CalcDurability()
  {
    int durability = wepon.Durability;
    for (int i = 0; i < options.Length; i++)
    {
      durability += options[i] != null ? options[i].Durability : 0;
    }
    return durability;
  }

  //威力計算
  public float CalcAtkRandomMinToMax()
  {
    float atk = Random.Range(wepon.MinAtk, wepon.MaxAtk);
    for(int i = 0; i < options.Length;i++)
    {
      atk += options[i]!=null ? options[i].Atk : 0;
    }

    return atk;
  }

  //クリティカル値計算
  public int CalcCritical()
  {
    int cri = 0;
    for (int i = 0; i < options.Length; i++)
    {
      cri += options[i] != null ? options[i].Critical : 0;
    }

    return cri;
  }        
}
