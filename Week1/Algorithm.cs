/// <summary>
/// Clase base para los algoritmos de búsqueda.
/// </summary>
public class AlgoritmoDeBusqueda
{
    protected ListaCandidatos lista;

    /// <summary>
    /// Inicializa una nueva instancia de la clase AlgoritmoDeBusqueda.
    /// </summary>
    /// <param name="lista">Lista de candidatos.</param>
    public AlgoritmoDeBusqueda(ListaCandidatos lista)
    {
        this.lista = lista;
    }

    /// <summary>
    /// Calcula la prioridad de una solución.
    /// </summary>
    /// <param name="nodo_info">Información del nodo.</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Prioridad calculada.</returns>
    public virtual int calculo_de_prioridad(Solucion nodo_info, Func<Solucion, int>? calculo_heuristica = null)
    {
        return 0;
    }

    /// <summary>
    /// Realiza la búsqueda de una solución.
    /// </summary>
    /// <param name="solucion_inicial">Solución inicial.</param>
    /// <param name="criterio_parada">Función que determina el criterio de parada.</param>
    /// <param name="obtener_vecinos">Función que obtiene los vecinos de una solución.</param>
    /// <param name="calculo_coste">Función que calcula el coste entre soluciones.</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Tupla con la solución encontrada y el número de nodos revisados.</returns>
    public (Solucion, int) busqueda(List<(int, int)> solucion_inicial, Func<Solucion, bool> criterio_parada, Func<Solucion, List<(int, int)>> obtener_vecinos, Func<Solucion, Solucion, int> calculo_coste, Func<Solucion, int>? calculo_heuristica = null)
    {
        ListaCandidatos candidatos = lista;
        candidatos.anhadir(new Solucion(solucion_inicial, 0));
        Dictionary<string, int> vistos = new Dictionary<string, int>();
        bool finalizado = false;
        int revisados = 0;
        Solucion? solucion = null; // Inicializar solucion fuera del bucle while para poder acceder a ella después en el return de la función
        while (candidatos.Count() > 0 && !finalizado)
        {
            solucion = candidatos.obtener_siguiente();
            vistos[solucion.ToString()] = solucion.coste;
            revisados++;

            if (criterio_parada(solucion))
            {
                finalizado = true;
                break;
            }

            List<(int, int)> vecinos = obtener_vecinos(solucion);
            foreach ((int, int) vecino in vecinos)
            {
                List<(int, int)> nuevas_coords = solucion.coords.ToList();
                nuevas_coords.Add(vecino);

                Solucion nueva_solucion = new Solucion(nuevas_coords);

                if (!vistos.ContainsKey(nueva_solucion.ToString()))
                {
                    nueva_solucion.coste = solucion.coste + calculo_coste(solucion, nueva_solucion);
                    candidatos.anhadir(nueva_solucion, prioridad: calculo_de_prioridad(nueva_solucion, calculo_heuristica));
                }
            }
        }

        if (!finalizado)
            return (null, revisados);
        return (solucion, revisados);
    }
}

/// <summary>
/// Implementa el algoritmo A* para la búsqueda de soluciones.
/// </summary>
public class AEstrella : AlgoritmoDeBusqueda
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase AEstrella.
    /// </summary>
    public AEstrella() : base(new ColaDePrioridad()) { }

    /// <summary>
    /// Calcula la prioridad de una solución utilizando la heurística.
    /// </summary>
    /// <param name="solucion">Solución actual.</param>
    /// <param name="calculo_heuristica">Función opcional para calcular la heurística.</param>
    /// <returns>Prioridad calculada.</returns>
    public override int calculo_de_prioridad(Solucion solucion, Func<Solucion, int>? calculo_heuristica = null)
    {
        if(calculo_heuristica==null)
        {
            return solucion.coste;
        }
        return solucion.coste + calculo_heuristica(solucion);
    }
}
