using System.Collections.Generic;

[System.Serializable]
public class User
{
    public string name = "player";
    public long gyul;
    public long money;
    public long mPs;
    public float backSoundVolume = 1f;
    public float effectSoundVolume = 1f;
    public float gopValue;

    public List<Sell> sellList = new List<Sell>();
    public List<Upgrade> upgradeList = new List<Upgrade>();
    public List<Event> eventList = new List<Event>();

    public float doubleChance
    {
        get
        {
            if(eventList[0].eventChance >= 6f)
            {
                return eventList[0].eventChance;
            }
            return 0;
        }
    }

    public long mouseGpC
    {
        get
        {
            return (long)((1f < TotalgPs ? TotalgPs : 1f) * gopValue);
        }
    }

    public long gPs
    {
        get
        {
            long PerSecond = 0;
            foreach (Upgrade upgrade in upgradeList)
            {
                PerSecond += upgrade.GpS;
            }
            return PerSecond;
        }
    }

    public long TotalgPs
    {
        get
        {
            long total = 0;
            foreach (Upgrade upgrade in upgradeList)
            {
                total += upgrade.GpC;
            }
            return total;
        }
    }

    public long baseGcM()
    {
        long total = 0;
        long count = gyul;
        for (int i = 0; i < upgradeList[6].amount + 1; i++)
        {
            if(count >= 1)
            {
                total++;
                count--;
            }
        }
        return total;
    }

    public long TotalGcM
    {
        get
        {
            return upgradeList[5].amount * baseGcM();
        }
    }
}