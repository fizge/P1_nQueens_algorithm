/// <summary>
/// Representa una solución en el problema de las N-Reinas.
/// </summary>
public class Solucion : IComparable<Solucion>
{
    public List<(int, int)> coords;
    public int coste;


    /// <summary>
    /// Inicializa una nueva instancia de la clase Solucion.
    /// </summary>
    /// <param name="coords">Lista de coordenadas de las reinas.</param>
    /// <param name="coste">Coste asociado a la solución.</param>
    public Solucion(List<(int, int)> coords, int coste = 0)
    {
        this.coords = coords;
        this.coste = coste;
    }


    /// <summary>
    /// Compara esta instancia con otro objeto y determina si son iguales.
    /// </summary>
    /// <param name="obj">El objeto a comparar con esta instancia.</param>
    /// <returns>True si los objetos son iguales; de lo contrario, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj is Solucion other)
        {
            return string.Join(",", coords) == string.Join(",", other.coords);
        }
        return false;
    }


    /// <summary>
    /// Compara esta instancia con otra instancia de Solucion y determina su orden relativo.
    /// </summary>
    /// <param name="other">La otra instancia de Solucion a comparar.</param>
    /// <returns>Un valor que indica el orden relativo de las instancias comparadas.</returns>

    public int CompareTo(Solucion other)
    {
        return coste.CompareTo(other.coste);
    }

    /// <summary>
    /// Devuelve una representación en cadena de la solución.
    /// </summary>
    /// <returns>Cadena que representa la solución.</returns>
    public override string ToString()
    {
        return string.Join("-", coords.Select(x => $"({x.Item1},{x.Item2})"));
    }
}

/// <summary>
/// Clase base para la lista de candidatos.
/// </summary>
public class ListaCandidatos
{
    /// <summary>
    /// Añade una solución a la lista de candidatos.
    /// </summary>
    /// <param name="solucion">Solución a añadir.</param>
    /// <param name="prioridad">Prioridad de la solución.</param>
    public virtual void anhadir(Solucion solucion, int prioridad = 0)
    {
    }

    /// <summary>
    /// Borra una solución de la lista de candidatos.
    /// </summary>
    /// <param name="solucion">Solución a borrar.</param>
    public virtual void borrar(Solucion solucion)
    {
    }

    /// <summary>
    /// Obtiene la siguiente solución de la lista de candidatos.
    /// </summary>
    /// <returns>La siguiente solución.</returns>
    public virtual Solucion? obtener_siguiente()
    {
        return null;
    }

    /// <summary>
    /// Devuelve el número de soluciones en la lista de candidatos.
    /// </summary>
    /// <returns>Número de soluciones.</returns>
    public virtual int Count()
    {
        return 0;
    }
}

/// <summary>
/// Implementa una cola de prioridad para la lista de candidatos.
/// </summary>
public class ColaDePrioridad : ListaCandidatos
{
    private PriorityQueue<Solucion, int> cp;
    private Dictionary<string, int> buscador; // Almacenamos la prioridad actual para cada solución.
    private const int REMOVED = int.MaxValue; // Marcador para elementos eliminados.

    /// <summary>
    /// Inicializa una nueva instancia de la clase ColaDePrioridad.
    /// </summary>
    public ColaDePrioridad()
    {
        cp = new PriorityQueue<Solucion, int>();
        buscador = new Dictionary<string, int>();
    }

    /// <summary>
    /// Añade una solución a la cola de prioridad.
    /// </summary>
    /// <param name="solucion">Solución a añadir.</param>
    /// <param name="prioridad">Prioridad de la solución.</param>
    public override void anhadir(Solucion solucion, int prioridad = 0)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            int prioridadActual = buscador[strSolucion];
            if (prioridadActual <= prioridad)
            {
                return; // Si la prioridad actual es menor o igual, no se añade.
            }
            borrar(solucion); // Borra la solución existente con menor prioridad.
        }
        // Añadimos la solución con su prioridad.
        buscador[strSolucion] = prioridad;
        cp.Enqueue(solucion, prioridad); // Añade la solución a la cola de prioridad.
    }

    /// <summary>
    /// Borra una solución de la cola de prioridad.
    /// </summary>
    /// <param name="solucion">Solución a borrar.</param>
    public override void borrar(Solucion solucion)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            buscador.Remove(strSolucion); // Elimina la solución del diccionario.
            solucion.coste = REMOVED; // Marca la solución como eliminada.
        }
    }

    /// <summary>
    /// Obtiene la siguiente solución de la cola de prioridad.
    /// </summary>
    /// <returns>La siguiente solución.</returns>
    public override Solucion obtener_siguiente()
    {
        while (cp.Count > 0)
        {
            Solucion solucion = cp.Dequeue(); // Obtiene la solución con mayor prioridad.
            if (solucion.coste != REMOVED)
            {
                buscador.Remove(solucion.ToString()); // Elimina la solución del diccionario.
                return solucion; // Devuelve la solución válida.
            }
        }
        throw new InvalidOperationException("No hay siguiente en una cola de prioridad vacía");
    }

    /// <summary>
    /// Devuelve el número de soluciones en la cola de prioridad.
    /// </summary>
    /// <returns>Número de soluciones.</returns>
    public override int Count() => buscador.Count;
}