namespace Animals;

public abstract class Animal
{
    public Animal Clone()
    {
        return (Animal)MemberwiseClone();
    }
    
    public abstract double Price { get; }
    public abstract double FoodRequirement { get; }
}
