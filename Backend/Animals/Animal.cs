namespace Animals;

public abstract class Animal
{
    public Animal Clone()
    {
        return (Animal)MemberwiseClone();
    }
}
