using System;
using System.Collections.Generic;
using System.Linq;

public class MergeJoin
{
    public static List<(string, string)> Join(List<(string, string)> table1, List<(string, string)> table2, bool sorted = false)
    {
        var result = new List<(string, string)>();

        if (!sorted)
        {
            table1 = table1.OrderBy(t => t.Item1).ToList();
            table2 = table2.OrderBy(t => t.Item1).ToList();
        }

        int i = 0, j = 0;

        while (i < table1.Count && j < table2.Count)
        {
            if (table1[i].Item1.CompareTo(table2[j].Item1) < 0)
                i++;
            else if (table1[i].Item1.CompareTo(table2[j].Item1) > 0)
                j++;
            else
            {
                result.Add((table1[i].Item1, $"{table1[i].Item2}, {table2[j].Item2}"));
                j++;
                if (j == table2.Count || table2[j].Item1 != table1[i].Item1)
                {
                    i++;
                    j = 0;
                }
            }
        }

        return result;
    }

    public static string GetFlowchartDefinition()
    {
        return @"
graph TD
    A[Inicio] --> B[Crear lista de resultados vacía]
    B --> C{¿Tablas ordenadas?}
    C -->|No| D[Ordenar tabla1 y tabla2]
    C -->|Sí| E[Inicializar índices i=0, j=0]
    D --> E
    E --> F{i < tabla1.Count && j < tabla2.Count}
    F -->|Sí| G{Comparar claves}
    G -->|tabla1[i] < tabla2[j]| H[i++]
    G -->|tabla1[i] > tabla2[j]| I[j++]
    G -->|tabla1[i] = tabla2[j]| J[Agregar par al resultado]
    J --> K[j++]
    K --> L{j = tabla2.Count o claves diferentes}
    L -->|Sí| M[i++, j=0]
    L -->|No| N[Volver a comparar]
    H --> F
    I --> F
    M --> F
    N --> G
    F -->|No| O[Fin]
";
    }
}