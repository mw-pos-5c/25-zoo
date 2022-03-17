using System.Reflection;

namespace Animals;

public class AnimalFactory
{
    private static readonly Dictionary<string, Animal> baseAnimals = new();
    private static AnimalFactory? instance = null;


    static AnimalFactory()
    {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Animal)));

        foreach (Type type in types)
        {
            if (Activator.CreateInstance(type) is not Animal animal)
            {
                continue;
            }

            baseAnimals[type.Name] = animal;
        }
    }

    public static AnimalFactory GetInstance()
    {
        return instance ??= new AnimalFactory();
    }

    public static string[] GetAnimalNames()
    {
        return baseAnimals.Keys.ToArray();
    }

    public double CalcFoodRequirements(Type type)
    {
        double total = 0;
        foreach (var (animalName, count) in animalCount)
        {
            var baseAnimal = baseAnimals[animalName];
            if (!baseAnimal.GetType().IsSubclassOf(type))
            {
                continue;
            }

            total += baseAnimal.FoodRequirement * count;
        }

        return total;
    }

    public double CalcTotalPrice()
    {
        double total = 0;
        foreach (var (animalName, count) in animalCount)
        {
            var baseAnimal = baseAnimals[animalName];
            total += baseAnimal.Price * count;
        }

        return total;
    }

    public (string, int)[] GetAnimalCount()
    {
        return animalCount.Select(pair => (pair.Key, pair.Value)).ToArray();
    }

    private readonly Dictionary<string, int> animalCount = new();

    private AnimalFactory()
    {
    }

    public IEnumerable<Animal> Create(string type, int amount)
    {
        if (!baseAnimals.TryGetValue(type, out Animal? animal))
        {
            yield break;
        }

        if (!animalCount.TryGetValue(type, out int value))
        {
            value = 0;
        }

        animalCount[type] = value + amount;

        for (var i = 0; i < amount; i++)
        {
            yield return animal.Clone();
        }
    }
}