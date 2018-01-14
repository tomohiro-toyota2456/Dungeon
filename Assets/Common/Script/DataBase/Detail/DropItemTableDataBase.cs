using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemTableDataBase : DataBase
{
  [SerializeField]
  DropTable[] tables;

  public DropTable Search(int id)
  {
    for(int i = 0; i < tables.Length;i++)
    {
      if(tables[i].Id == id)
      {
        return Instantiate(tables[i]);
      }
    }

    return null;
  }

  public void SetData(DropTable[] tables)
  {
    this.tables = tables;
  }
}
