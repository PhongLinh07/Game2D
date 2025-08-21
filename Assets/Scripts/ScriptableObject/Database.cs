using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Database<T> where T : ConfigItem<T>// Cant container: Object ScriptableObject, ...
{
    public List<T> datas = new List<T>();

    public T GetById(int id) 
    {
        
        return datas.Find(x => x.Id == id); 
    }
}


