using System;

public static class Utils
{
    static Random random = new Random();

    public static int GetRandomInt(int min, int max)
    {
        return random.Next(min, max + 1);
    }

    public static float GetRanomFloat()
    {
        return (float)random.NextDouble();
    }

}

