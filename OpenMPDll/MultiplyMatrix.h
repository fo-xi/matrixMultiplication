#pragma once
#include <cstdio>
#include <omp.h>

extern "C" __declspec(dllexport) int Multiply(double* a, int aRowCount, int aColumnCount,
    double* b, int bRowCount, int bColumnCount, int threadCount, double* result)
{
    // Задание количества потоков
    omp_set_num_threads(threadCount);

    #pragma omp parallel for
    for (int rowIndex = 0; rowIndex < aRowCount; rowIndex++)
    {
        for (int columnIndex = 0; columnIndex < bColumnCount; columnIndex++)
        {
            result[rowIndex * aColumnCount + columnIndex] = 0;
            for (int k = 0; k < aColumnCount; k++)
            {
                result[rowIndex * bColumnCount + columnIndex] +=
                    a[rowIndex * aColumnCount + k] * b[k * bColumnCount + columnIndex];
            }
        }
    }

    return 0;
}