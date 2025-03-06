using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        // Estado inicial: lista vacía, sin ninguna reina colocada.
        List<(int, int)> solucion_inicial = new List<(int, int)>();
        // Número de reinas a colocar (tamaño del tablero).
        int reinas = 15;

        /// <summary>
        /// Calcula el coste entre dos soluciones.
        /// En este ejemplo, el coste es el número de reinas en conflicto.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <param name="nueva_solucion">Nueva solución generada.</param>
        /// <returns>Coste (número de conflictos).</returns>
        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            int conflictos = 0;
            foreach (var reina in nueva_solucion.coords)
            {
                foreach (var otra_reina in nueva_solucion.coords)
                {
                    if (reina != otra_reina)
                    {
                        if (reina.Item2 == otra_reina.Item2 || Math.Abs(reina.Item1 - otra_reina.Item1) == Math.Abs(reina.Item2 - otra_reina.Item2))
                        {
                            conflictos++;
                        }
                    }
                }
            }
            return conflictos;
        }

        /// <summary>
        /// Calcula la heurística para una solución.
        /// En este ejemplo, la heurística es el número de reinas en conflicto.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Valor heurístico (número de conflictos).</returns>
        int calculo_heuristica(Solucion solucion)
        {
            int conflictos = 0;
            foreach (var reina in solucion.coords)
            {
                foreach (var otra_reina in solucion.coords)
                {
                    if (reina != otra_reina)
                    {
                        if (reina.Item2 == otra_reina.Item2 || Math.Abs(reina.Item1 - otra_reina.Item1) == Math.Abs(reina.Item2 - otra_reina.Item2))
                        {
                            conflictos++;
                        }
                    }
                }
            }
            return conflictos;
        }

        /// <summary>
        /// Obtiene los vecinos de una solución.
        /// A partir de la solución actual, se generan todos los posibles estados
        /// añadiendo una reina en cada posición de la siguiente fila.
        /// Solo se añaden vecinos prometedores (sin conflictos con las reinas ya colocadas).
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>Lista de vecinos (nuevas coordenadas posibles).</returns>
        List<(int, int)> obtener_vecinos(Solucion solucion)
        {
            int row = solucion.coords.Count == 0 ? -1 : solucion.coords[^1].Item1;
            List<(int, int)> vecinos = new List<(int, int)>();
            if (row + 1 < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    (int, int) nuevo_nodo = (row + 1, j);
                    
                    vecinos.Add(nuevo_nodo); // Añade la posición (siguiente fila, columna j)
                    
                }
            }
            return vecinos;
        }


        /// <summary>
        /// Verifica si una solución cumple con el criterio de parada.
        /// El algoritmo finaliza cuando:
        /// - Se han colocado todas las reinas (solución completa).
        /// - No existen conflictos: no hay dos reinas en la misma columna o en diagonales conflictivas.
        /// </summary>
        /// <param name="solucion">Solución actual.</param>
        /// <returns>True si la solución es completa y válida; de lo contrario, false.</returns>
        bool criterio_parada(Solucion solucion)
        {
            // Si aún no se han colocado todas las reinas, la solución no puede ser completa.
            if (solucion.coords.Count < reinas)
                return false;
            
            // Comprobar que no existan conflictos entre las reinas.
            for (int i = 0; i < solucion.coords.Count; i++)
            {
                (int, int) nodo_i = solucion.coords[i];
                for (int j = i + 1; j < solucion.coords.Count; j++)
                {
                    (int, int) nodo_j = solucion.coords[j];
                    // Se verifica que no estén en la misma columna o en la misma diagonal.
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(j - i))
                        return false;
                }
            }
            return true;
        }

        /*
        //Búsqueda por anchura
        Console.WriteLine("\n\nBúsqueda por anchura:");

        // Se inicia el cronómetro para medir el tiempo de ejecución
        Stopwatch stopwatchAnch = new Stopwatch();
        stopwatchAnch.Start();

        // Se crea una instancia del algoritmo de búsqueda por anchura 
        BusquedaAnchura bAnchura = new BusquedaAnchura();
        // Se inicia la búsqueda a partir del estado inicial (solución vacía)
        (Solucion solucionAnch, int revisadosAnch) = bAnchura.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

        stopwatchAnch.Stop();
        // Mostrar el tiempo de ejecución
        Console.WriteLine($"Tiempo transcurrido: {stopwatchAnch.ElapsedMilliseconds} ms");

        Console.WriteLine("Coordenadas:  " + solucionAnch.ToString());
        Console.WriteLine("Nodos evaluados:  " + revisadosAnch);


        
        //Búsqueda en profundidad
        Console.WriteLine("\n\nBúsqueda en profundidad:");

        // Se inicia el cronómetro para medir el tiempo de ejecución
        Stopwatch stopwatchProf = new Stopwatch();
        stopwatchProf.Start();

        // Se crea una instancia del algoritmo de búsqueda en profundidad
        BusquedaProfundidad bProfundidad = new BusquedaProfundidad();
        // Se inicia la búsqueda a partir del estado inicial (solución vacía)
        (Solucion solucionProf, int revisadosProf) = bProfundidad.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

        stopwatchProf.Stop();
        // Mostrar el tiempo de ejecución
        Console.WriteLine($"Tiempo transcurrido: {stopwatchProf.ElapsedMilliseconds} ms");

        Console.WriteLine("Coordenadas:  " + solucionProf.ToString());
        Console.WriteLine("Nodos evaluados:  " + revisadosProf);
        */



        //Búsqueda por anchura
        Console.WriteLine("\n\nBúsqueda por A*:");

        // Se inicia el cronómetro para medir el tiempo de ejecución
        Stopwatch stopwatchAstar = new Stopwatch();
        stopwatchAstar.Start();

        // Se crea una instancia del algoritmo de búsqueda por A*
        AEstrella aestrella = new AEstrella();
        // Se inicia la búsqueda a partir del estado inicial (solución vacía)
        (Solucion solucionAstar, int revisadosAstar) = aestrella.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);

        stopwatchAstar.Stop();
        // Mostrar el tiempo de ejecución
        Console.WriteLine($"Tiempo transcurrido: {stopwatchAstar.ElapsedMilliseconds} ms");

        Console.WriteLine("Coordenadas:  " + solucionAstar.ToString());
        Console.WriteLine("Nodos evaluados:  " + revisadosAstar);
    }
}
