using System.Collections.Generic;

[System.Serializable]
public class Event
{
    public int eventNumber;
    public string eventName;
    public float eventChance;


    public Event(string name, int number, float chance)
    {
        eventNumber = number;
        eventName = name;
        eventChance = chance;
    }

    private long basePrice
    {
        get
        {
            return (long)(eventNumber + (eventNumber + 1)) * 1000;
        }
    }

    public long price
    {
        get
        {
            return (long)(eventChance - 5 <= 0f ? basePrice : (eventChance - 5) * basePrice);
        }
    }
}