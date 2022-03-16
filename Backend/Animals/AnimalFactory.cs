using System.Reflection;

namespace Animals;

public class AnimalFactory
{
    private static readonly Dictionary<string, Animal> baseAnimals = new();
    private static AnimalFactory? instance = null;
    
    
    static AnimalFactory()
    {
        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(Animal)));

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

    private readonly Dictionary<string, int> animalCount = new();
    private AnimalFactory() {}
    
    public IEnumerable<Animal> Create(string type, int amount)
    {
        
        if (!baseAnimals.TryGetValue(type, out Animal? animal))
        {
            yield break;
        }

        animalCount[type] += amount;

        for (var i = 0; i < amount; i++)
        {
            yield return animal.Clone();
        }
    }
}
