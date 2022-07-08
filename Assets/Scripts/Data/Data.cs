using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public int level;
    public int coin;

    public Data()
    {

    }

    public Data(int level)
    {
        this.level = level;
    }

    public Data(int level,int coin)
    {
        this.level = level;
        this.coin = coin;
    }
}
