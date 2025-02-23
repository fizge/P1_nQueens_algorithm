using System;
using System.Collections.Generic;
using System.Linq;

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

    public virtual Solucion obtener_siguiente()
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
    private List<(int, Solucion)> cp;
    private Dictionary<string, (int, Solucion)> buscador;

    public ColaDePrioridad()
    {
        cp = new List<(int, Solucion)>();
        buscador = new Dictionary<string, (int, Solucion)>();
    }

    public override void anhadir(Solucion solucion, int prioridad = 0)
    {
        string strSolucion = solucion.ToString();
        if (buscador.ContainsKey(strSolucion))
        {
            var solucionBuscador = buscador[strSolucion];
            if (solucionBuscador.Item1 <= prioridad)
            {
                return;
            }
            borrar(solucion);
        }
        var entrada = (prioridad, solucion);
        buscador[strSolucion] = entrada;
        cp.Add(entrada);
        cp = cp.OrderBy(x => x.Item1).ToList();
    }

    public override void borrar(Solucion solucion)
    {
        string strSolucion = solucion.ToString();
        if (buscador.TryGetValue(strSolucion, out var entrada))
        {
            buscador.Remove(strSolucion);
            entrada.Item2.coste = int.MaxValue; // Use int.MaxValue to represent REMOVED
        }
    }

    public override Solucion obtener_siguiente()
    {
        while (cp.Count > 0)
        {
            var (prioridad, solucion) = cp[0];
            cp.RemoveAt(0);
            if (solucion.coste != int.MaxValue)
            {
                buscador.Remove(solucion.ToString());
                return solucion;
            }
        }
        throw new InvalidOperationException("No hay siguiente en una cola de prioridad vacía");
    }

    public override int Count() 
    {
        return buscador.Count;
    }
}
