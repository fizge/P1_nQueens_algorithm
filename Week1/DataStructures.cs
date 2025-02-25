public class Solucion : IComparable<Solucion>
{
    public List<(int, int)> coords;
    public int coste;

    public Solucion(List<(int, int)> coords, int coste = 0)
    {
        this.coords = coords;
        this.coste = coste;
    }

    public override bool Equals(object obj)
    {
        if (obj is Solucion other)
        {
            return string.Join(",", coords) == string.Join(",", other.coords);
        }
        return false;
    }

    public int CompareTo(Solucion other)
    {
        return coste.CompareTo(other.coste);
    }

    public override string ToString()
    {
        return string.Join("-", coords.Select(x => $"({x.Item1},{x.Item2})"));
    }
}

public class ListaCandidatos
{
    public virtual void anhadir(Solucion solucion, int prioridad = 0)
    {
    }

    public virtual void borrar(Solucion solucion)
    {
    }

    public virtual Solucion? obtener_siguiente()
    {
        return null;
    }

    public virtual int Count()
    {
        return 0;
    }
}

public class ColaDePrioridad : ListaCandidatos
{
    private PriorityQueue<Solucion, int> cp;
    private Dictionary<string, int> buscador; // Almacenamos la prioridad actual para cada solución.
    private const int REMOVED = int.MaxValue; // Marcador para elementos eliminados.

    public ColaDePrioridad()
    {
        cp = new PriorityQueue<Solucion, int>();
        buscador = new Dictionary<string, int>();
    }

    public override void anhadir(Solucion solucion, int prioridad = 0)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            int prioridadActual = buscador[strSolucion];
            if (prioridadActual <= prioridad)
            {
                return;
            }
            borrar(solucion);
        }
        // Añadimos la solución con su prioridad.
        buscador[strSolucion] = prioridad;
        cp.Enqueue(solucion, prioridad);
    }

    public override void borrar(Solucion solucion)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            buscador.Remove(strSolucion);
            // Marcamos la solución como "eliminada" modificando su coste.
            solucion.coste = REMOVED;
        }
    }

    public override Solucion obtener_siguiente()
    {
        while (cp.Count > 0)
        {
            Solucion solucion = cp.Dequeue();
            // Si la solución aún es válida (no marcada como eliminada), se devuelve.
            if (solucion.coste != REMOVED)
            {
                buscador.Remove(solucion.ToString());
                return solucion;
            }
        }
        throw new InvalidOperationException("No hay siguiente en una cola de prioridad vacía");
    }

    public override int Count() => buscador.Count;
}