public interface IBackpackCon
{
    public bool IsHasItem => ItemCount > 0;
    public int ItemCount { get; }
    public bool AddItemRes(PickedRes itemRes);
    public PickedRes OutputItemRes();
}