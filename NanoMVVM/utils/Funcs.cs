namespace NanoMVVM.Utils;

internal static class Funcs
{
    public static bool AlwaysTrue<T, U>(T _, U __) => true;
    
    public static void DoNothing<T>(T _) { }
    
    public static bool AlwaysReturnTrue<T>(T _) => true;
}
