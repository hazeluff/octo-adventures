public interface IUserStatusEffect  {
    bool IsPositive();
    void Apply(IStageUnit unit);
}