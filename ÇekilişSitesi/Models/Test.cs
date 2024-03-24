using System;
using System.Collections.Generic;
using System.Linq;

class UniqueRandomNumberGenerator
{
    private List<int> numbers;
    private Random random;

    public UniqueRandomNumberGenerator(int minValue, int maxValue)
    {
        if (minValue > maxValue)
        {
            throw new ArgumentException("Min value should be less than or equal to max value");
        }

        numbers = Enumerable.Range(minValue, maxValue - minValue + 1).ToList();
        random = new Random();
    }

    public int GetUniqueRandomNumber()
    {
        if (numbers.Count == 0)
        {
            throw new InvalidOperationException("No more unique numbers available");
        }

        int index = random.Next(0, numbers.Count);
        int randomNumber = numbers[index];
        numbers.RemoveAt(index);

        return randomNumber;
    }
}