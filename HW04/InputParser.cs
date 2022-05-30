public class InputParser
{
    public static int? gatherBitsPerChunk()
    {
        Console.Write("Enter specific bits in chunk (1,2,4 or 8): ");
        var bitsPerChunk = Console.ReadLine();
        int bitsPerChunkInt;
        if (!int.TryParse(bitsPerChunk, out bitsPerChunkInt) ||
            bitsPerChunkInt <= 0 ||
            bitsPerChunkInt > 8 ||
            ((bitsPerChunkInt % 2 != 0 || bitsPerChunkInt % 3 == 0) && bitsPerChunkInt != 1))
        {
            return null;
        }
        return bitsPerChunkInt;
    }

    public static int? gatherParallelizationConfig()
    {
        Console.Write("Enter paralization level (0 for no limit): ");
        var paralelization = Console.ReadLine();
        int paralelizationInt;
        if (!int.TryParse(paralelization, out paralelizationInt) || paralelizationInt < 0)
        {
            return null;
        }

        if (paralelizationInt == 0)
        {
            paralelizationInt = Environment.ProcessorCount;
        }
        return paralelizationInt;
    }
}