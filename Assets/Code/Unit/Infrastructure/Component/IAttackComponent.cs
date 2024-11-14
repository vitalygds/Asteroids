namespace Unit
{
    public interface IAttackComponent : IUnitComponent
    {
        void SetActive(int value, bool isActive);
    }
}