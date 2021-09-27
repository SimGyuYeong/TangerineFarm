using System.Collections.Generic;
using System;
using System.Collections;

[System.Serializable]
public class Upgrade
{
    public int upgradeNumber = 0;
    public string upgradeName;
    public int amount = 0;
    public string buttonText;

    public Upgrade() { }
    public Upgrade(string name, int number, string button)
    {
        upgradeNumber = number;
        upgradeName = name;
        buttonText = button;
        
    }

    private long basePrice
    {
        get
        {
            if(upgradeNumber <= 2)
            {
                return (long)(upgradeNumber + (upgradeNumber + 1) * 4.4f / 2.25f * 10);
            }
            else if (upgradeNumber <= 4)
            {
                return (long)(upgradeNumber + (upgradeNumber + 1) * 9.5f / 3f * 10) * 4;
            }
            else
            {
                return (long)(upgradeNumber + (upgradeNumber + 1) * 15f / 4.35f * 10) * 9;
            }
        }
    }

    public long price
    {
        get
        {
            return (long)(amount <= 0f ? basePrice : amount * basePrice) ;
        }
    }

    private int baseGpC
    {
        get
        {
            return upgradeNumber + 1;
        }
    }

    public int GpC
    {
        get
        {
            if(upgradeNumber <= 2)
            {
                return (int)(amount * baseGpC / 3.5f);
            }
            else
            {
                return 0;
            }
        }
    }

    private int baseGpS
    {
        get
        {
            return upgradeNumber - 2;
        }
    }

    public int GpS
    {
        get
        {
            if (upgradeNumber == 3 || upgradeNumber == 4)
            {
                return amount * baseGpS;
            }
            else
            {
                return 0;
            }
        }
    }
}