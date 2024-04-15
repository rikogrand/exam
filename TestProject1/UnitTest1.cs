namespace TestProject1;

public class UnitTest1
{
    public double t;
    public double g = 9.8;
    public double v;

    public void SolutionT()
    {
        t = (2 * v) / g;
    }

    [Fact]
    public void Test1()
    {
        //arrange
        v = 10;
        g = 15;


    //act 
    public double WaitT = 15;
    public double FinalT = 10;

    //assert
    public new static bool Equals(double WaitT, object FinalT)
    {
        Assert.Equal(WaitT, FinalT);

    }

public void Test2()
    {
        //arrange
        v = 10;
        g = 10;


    //act 
    public double WaitT1 = 15;
    public double FinalT1 = 10;

    //assert
    public new static bool Equalss(double WaitT1, object FinalT1)
    {
        Assert.Equal(WaitT1, FinalT1);


    }
}