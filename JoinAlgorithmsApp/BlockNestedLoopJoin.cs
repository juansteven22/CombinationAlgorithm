using System.Collections.Generic;

public class BlockNestedLoopJoin
{
    public static List<(string, string)> Join(List<(string, string)> table1, List<(string, string)> table2, int blockSize)
    {
        var result = new List<(string, string)>();

        for (int i = 0; i < table1.Count; i += blockSize)
        {
            var block = table1.GetRange(i, Math.Min(blockSize, table1.Count - i));
            foreach (var (key2, value2) in table2)
            {
                foreach (var (key1, value1) in block)
                {
                    if (key1 == key2)
                    {
                        result.Add((key1, $"{value1}, {value2}"));
                    }
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
    B --> C[Iterar a través de tabla1 en bloques]
    C --> D[Cargar bloque de tabla1 en memoria]
    D --> E[Iterar a través de tabla2]
    E --> F{¿Clave de tabla2 coincide con alguna en el bloque?}
    F -->|Sí| G[Agregar pares coincidentes al resultado]
    F -->|No| H[Siguiente elemento en tabla2]
    G --> H
    H --> I{¿Más elementos en tabla2?}
    I -->|Sí| E
    I -->|No| J[Siguiente bloque de tabla1]
    J --> K{¿Más bloques en tabla1?}
    K -->|Sí| C
    K -->|No| L[Fin]
";
    }
}