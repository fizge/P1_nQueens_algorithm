
public class AlgoritmoDeBusqueda
{
    protected ListaCandidatos lista;

    public AlgoritmoDeBusqueda(ListaCandidatos lista)
    {
        this.lista = lista;
    }

    public virtual int calculo_de_prioridad(Solucion nodoInfo, Func<Solucion, int> calculoHeuristica = null)
    {
        return 0;
    }

    public (Solucion, int) busqueda(List solucion_inicial, Func<Solucion, bool> criterio_parada, Func<Solucion, List<(int, int)>> obtener_vecinos, Func<Solucion, Solucion, int> calculo_coste, Func<Solucion, int> calculo_heuristica = null)
    {
        ListaCandidatos candidatos = lista;
        candidatos.anhadir(new Solucion(solucion_inicial, 0));
        Dictionary<string, int> vistos = new Dictionary<string, int>();
        bool finalizado = false;
        int revisados = 0;

        while (candidatos.Count > 0 && !finalizado)
        {
            int solucion = candidatos.obtener_siguiente();
            vistos[solucion.ToString()] = solucion.coste;
            revisados++;

            if (criterio_parada(solucion))
            {
                finalizado = true;
                break;
            }

            List<(int,int)> vecinos = obtener_vecinos(solucion);
            foreach (ValueTuple<int,int> vecino in vecinos)
            {
                Solucion nueva_solucion = new Solucion(new List<(int, int)>(solucion.Coords) { vecino });
                if (!vistos.ContainsKey(nuevaSolucion.ToString()))
                {
                    nuevaSolucion.Coste = solucion.Coste + calculoCoste(solucion, nuevaSolucion);
                    candidatos.Anhadir(nuevaSolucion, prioridad = CalculoDePrioridad(nuevaSolucion, calculoHeuristica));
                }
            }
        }

        if (!finalizado)
            return (null, revisados);
        return (solucion, revisados);
    }
}

public class AEstrella : AlgoritmoDeBusqueda
{
    public AEstrella() : base(new ColaDePrioridad()) { }

    public override int CalculoDePrioridad(Solucion solucion, Func<Solucion, int> calculoHeuristica = null)
    {
        return solucion.Coste + (calculoHeuristica != null ? calculoHeuristica(solucion) : 0);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var solucionInicial = new List<(int, int)>();
        int reinas = 6;

        int CalculoCoste(Solucion solucion, Solucion nuevaSolucion)
        {
            return 1;
        }

        int CalculoHeuristica(Solucion solucion)
        {
            return 0;
        }

        List<(int, int)> ObtenerVecinos(Solucion solucion)
        {
            int row = solucion.Coords.Count == 0 ? -1 : solucion.Coords[^1].Item1;
            var vecinos = new List<(int, int)>();
            if (row + 1 < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    vecinos.Add((row + 1, j));
                }
            }
            return vecinos;
        }

        bool CriterioParada(Solucion solucion)
        {
            if (solucion.Coords.Count < reinas)
                return false;

            for (int i = 0; i < solucion.Coords.Count; i++)
            {
                var nodoI = solucion.Coords[i];
                for (int j = i + 1; j < solucion.Coords.Count; j++)
                {
                    var nodoJ = solucion.Coords[j];
                    if (nodoJ.Item2 == nodoI.Item2 || Math.Abs(nodoJ.Item2 - nodoI.Item2) == Math.Abs(j - i))
                        return false;
                }
            }
            return true;
        }

        var astar = new AEstrella();
        var (solucion, revisados) = astar.Busqueda(new Solucion(solucionInicial), CriterioParada, ObtenerVecinos, CalculoCoste, CalculoHeuristica);
        Console.WriteLine("Coordenadas: " + string.Join(", ", solucion.Coords));
        Console.WriteLine("Nodos evaluadas: " + revisados);
    }
}
