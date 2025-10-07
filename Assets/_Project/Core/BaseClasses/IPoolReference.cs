using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolReference
{
    public void ReturnToPool(GameObject obj);
}
