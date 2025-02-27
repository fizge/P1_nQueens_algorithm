/// <summary>
/// Representa una solución en el problema de las N-Reinas.
/// Una solución se define como una lista de coordenadas, donde cada par (fila, columna)
/// indica la posición de una reina en el tablero, y un coste asociado.
/// </summary>
public class Solucion
{
    public List<(int, int)> coords;
    public int coste;

    /// <summary>
    /// Inicializa una nueva instancia de la clase Solucion.
    /// </summary>
    /// <param name="coords">Lista de coordenadas de las reinas.</param>
    /// <param name="coste">Coste asociado a la solución (por defecto 0).</param>
    public Solucion(List<(int, int)> coords, int coste = 0)
    {
        this.coords = coords;
        this.coste = coste;
    }
    
    /// <summary>
    /// Compara esta instancia con otro objeto y determina si son iguales.
    /// Se utiliza la representación en cadena de las coordenadas para la comparación.
    /// </summary>
    /// <param name="obj">El objeto a comparar.</param>
    /// <returns>True si las coordenadas son iguales; de lo contrario, false.</returns>
    public override bool Equals(object obj)
    {
        if (obj is Solucion other)
        {
            return string.Join(",", coords) == string.Join(",", other.coords);
        }
        return false;
    }

    /// <summary>
    /// Compara esta instancia con otra instancia de Solucion basado en el coste.
    /// </summary>
    /// <param name="other">La otra solución a comparar.</param>
    /// <returns>Valor de comparación entre los costes.</returns>
    public int CompareTo(Solucion other)
    {
        return coste.CompareTo(other.coste);
    }

    /// <summary>
    /// Devuelve una representación en cadena de la solución.
    /// Cada coordenada se formatea como (fila,columna) y se separan por guiones.
    /// </summary>
    /// <returns>Cadena que representa la solución.</returns>
    public override string ToString()
    {
        return string.Join("-", coords.Select(x => $"({x.Item1},{x.Item2})"));
    }
}

/// <summary>
/// Clase base para la lista de candidatos a explorar durante la búsqueda.
/// </summary>
public class ListaCandidatos
{
    /// <summary>
    /// Añade una solución a la lista de candidatos.
    /// </summary>
    /// <param name="solucion">Solución a añadir.</param>
    /// <param name="prioridad">Prioridad de la solución (por defecto 0).</param>
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
    /// Obtiene la siguiente solución a explorar de la lista de candidatos.
    /// </summary>
    /// <returns>La siguiente solución o null si no hay candidatos.</returns>
    public virtual Solucion? obtener_siguiente()
    {
        return null;
    }

    /// <summary>
    /// Devuelve el número de soluciones en la lista de candidatos.
    /// </summary>
    /// <returns>Número de soluciones almacenadas.</returns>
    public virtual int? Count()
    {
        return null;
    }
}

/// <summary>
/// Implementa una cola de prioridad para gestionar la lista de candidatos.
/// Esta estructura es esencial para el algoritmo A* (AEstrella), permitiendo:
/// - Extraer el nodo con la menor prioridad (coste acumulado + heurística).
/// - Evitar la inserción de duplicados o estados con coste mayor que uno ya procesado.
/// </summary>
public class ColaDePrioridad : ListaCandidatos
{
    // Cola de prioridad que almacena las soluciones junto a su prioridad.
    private PriorityQueue<Solucion, int> cp;
    
    // Diccionario auxiliar para llevar un registro de las prioridades actuales asociadas a cada solución,
    // utilizando la representación en cadena de la solución como clave.
    private Dictionary<string, int> buscador;
    
    // Constante utilizada para marcar soluciones eliminadas.
    private const int REMOVED = int.MaxValue;

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
    /// Verifica si la solución ya existe:
    /// - Si existe y la prioridad actual es menor o igual, no se añade.
    /// - Si la nueva solución tiene una prioridad mejor, se elimina la existente y se añade la nueva.
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
                return; // La solución existente es mejor o igual, se descarta la nueva.
            }
            borrar(solucion); // La nueva solución es mejor: se elimina la anterior.
        }
        // Se añade la solución al diccionario y se encola con su prioridad.
        buscador[strSolucion] = prioridad;
        cp.Enqueue(solucion, prioridad);
    }

    /// <summary>
    /// Borra una solución de la cola de prioridad.
    /// Se elimina la solución del diccionario y se marca como eliminada asignándole el valor REMOVED.
    /// </summary>
    /// <param name="solucion">Solución a borrar.</param>
    public override void borrar(Solucion solucion)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            buscador.Remove(strSolucion);
            solucion.coste = REMOVED; // Marca la solución para que sea ignorada en la extracción.
        }
    }

    /// <summary>
    /// Obtiene la siguiente solución válida de la cola de prioridad.
    /// Se ignoran las soluciones que hayan sido marcadas como eliminadas.
    /// </summary>
    /// <returns>La siguiente solución válida.</returns>
    public override Solucion obtener_siguiente()
    {
        while (cp.Count > 0)
        {
            Solucion solucion = cp.Dequeue();
            if (solucion.coste != REMOVED)
            {
                buscador.Remove(solucion.ToString());
                return solucion;
            }
        }
        throw new InvalidOperationException("No hay siguiente en una cola de prioridad vacía");
    }

    /// <summary>
    /// Devuelve el número de soluciones activas en la cola de prioridad.
    /// </summary>
    /// <returns>Número de soluciones.</returns>
    public override int? Count() => buscador.Count;
}
