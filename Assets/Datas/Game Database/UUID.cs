using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class UUID: SingletonBase<UUID>
{
    public string Generator()
    {
        return Guid.NewGuid().ToString(); // Sinh UUID random
    }
}
