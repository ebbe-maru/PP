using static System.Console;
using System.Threading;
using System.Threading.Tasks;

class pfor{
    public static void Main(string[] args){
        int nthreads = 1;
        int nterms = (int)1e8;
        
         foreach (var arg in args){
            var words = arg.Split(':');
            if (words[0]=="-threads") nthreads = int.Parse(words[1]);
            if (words[0]=="-terms") nterms = (int)float.Parse(words[1]);
        }
        
        WriteLine($"nterms={nterms}, nthreads={nthreads}");
        double sum = 0;
        Parallel.For(1, nterms+1, (int i) => {sum += 1.0/i;});
        WriteLine($"sum={sum}");
    }
}