namespace ClassLibrary.Coins.Interfaces
{
    public interface ICoin
    {
        int Value { get; set; }
        double Top { get; set; }
        double Left { get; set; }
        string Color { get; set; }
        ICoin Copy();
    }
}
