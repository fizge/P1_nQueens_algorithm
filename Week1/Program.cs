using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        List<(int, int)> solucion_inicial = new List<(int, int)>();
        int reinas = 6;

        int calculo_coste(Solucion solucion, Solucion nueva_solucion)
        {
            return 1;
        }

        int calculo_heuristica(Solucion solucion)
        {
            return 0;
        }

        List<(int, int)> obtener_vecinos(Solucion solucion)
        {
            int row = solucion.coords.Count == 0 ? -1 : solucion.coords[^1].Item1;
            List<(int, int)> vecinos = new List<(int, int)>();
            if (row + 1 < reinas)
            {
                for (int j = 0; j < reinas; j++)
                {
                    vecinos.Add((row + 1, j));
                }
            }
            return vecinos;
        }

        bool criterio_parada(Solucion solucion)
        {
            if (solucion.coords.Count < reinas)
                return false;

            for (int i = 0; i < solucion.coords.Count; i++)
            {
                (int, int) nodo_i = solucion.coords[i];
                for (int j = i + 1; j < solucion.coords.Count; j++)
                {
                    (int, int) nodo_j = solucion.coords[j];
                    if (nodo_j.Item2 == nodo_i.Item2 || Math.Abs(nodo_j.Item2 - nodo_i.Item2) == Math.Abs(j - i))
                        return false;
                }
            }
            return true;
        }

        AEstrella astar = new AEstrella();
        (Solucion solucion, int revisados) = astar.busqueda(solucion_inicial, criterio_parada, obtener_vecinos, calculo_coste, calculo_heuristica);
        Console.WriteLine("Coordenadas:  " + solucion.ToString());
        Console.WriteLine("Nodos evaluadas:  " + revisados);
    }
}
