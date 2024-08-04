using System;
using System.Collections.Generic;
using System.Linq;

public class HashJoin
{
    public static List<(string, string)> Join(List<(string, string)> table1, List<(string, string)> table2)
    {
        var result = new List<(string, string)>();
        var hash = new Dictionary<string, List<string>>();

        foreach (var (key, value) in table1)
        {
            if (!hash.ContainsKey(key))
                hash[key] = new List<string>();
            hash[key].Add(value);
        }

        foreach (var (key, value) in table2)
        {
            if (hash.ContainsKey(key))
            {
                foreach (var match in hash[key])
                {
                    result.Add((key, $"{match}, {value}"));
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
    B --> C[Crear tabla hash]
    C --> D[Iterar a través de tabla1]
    D --> E{¿Clave en tabla hash?}
    E -->|Sí| F[Agregar valor a clave existente]
    E -->|No| G[Crear nuevo par clave-valor]
    F --> H[Siguiente elemento en tabla1]
    G --> H
    H --> I{¿Más elementos?}
    I -->|Sí| D
    I -->|No| J[Iterar a través de tabla2]
    J --> K{¿Clave en tabla hash?}
    K -->|Sí| L[Agregar pares coincidentes al resultado]
    K -->|No| M[Siguiente elemento en tabla2]
    L --> M
    M --> N{¿Más elementos?}
    N -->|Sí| J
    N -->|No| O[Fin]
";
    }

}