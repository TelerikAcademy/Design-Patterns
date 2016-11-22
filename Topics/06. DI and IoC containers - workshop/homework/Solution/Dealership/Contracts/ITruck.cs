namespace Dealership.Contracts
{
    public interface ITruck : IVehicle
    {
        int WeightCapacity { get; }
    }
}
