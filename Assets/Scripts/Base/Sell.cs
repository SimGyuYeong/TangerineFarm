using System.Collections.Generic;

[System.Serializable]
public class Sell
{
    public int sellNumber;
    public string sellName;
    public int amount;
    public long gyul;

    public Sell() { }
    public Sell(string name, int number, int amount)
    {
        sellNumber = number;
        sellName = name;
        gyul = amount;
    }

    private long basePrice
    {
        get
        {
            return (long)((sellNumber + 1) * 12.5 * gyul) * ( 3 / (sellNumber+1) );
        }
    }

    public long price
    {
        get
        {
            return (long)(amount <= 0f ? basePrice : amount * basePrice);
        }
    }

    private long baseMoney
    {
        get
        {
            return gyul + (sellNumber + gyul / 30);
        }
    }

    public long getMoney
    {
        get
        {
            return (long)(amount <= 0f ? baseMoney : amount * baseMoney);
        }
    }

}