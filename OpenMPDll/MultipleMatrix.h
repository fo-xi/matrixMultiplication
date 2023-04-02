#pragma once
#include <cstdio>
#include <omp.h>

extern "C" __declspec(dllexport) int Multiple(double* a, int aRowÑount, int aColumnÑount,
    double* b, int bRowÑount, int bColumnÑount, int threadCount, double* result)
{
    // ßâíîå çàäàíèå êîëè÷åñòâà ïîòîêîâ
    omp_set_num_threads(threadCount);

#pragma omp parallel for shared(a, b, result)
    for (int rowIndex = 0; rowIndex < aRowÑount; rowIndex++)
    {
        for (int columnIndex = 0; columnIndex < bColumnÑount; columnIndex++)
        {
            result[rowIndex * aColumnÑount + columnIndex] = 0;
            for (int k = 0; k < aColumnÑount; k++)
            {
                result[rowIndex * aColumnÑount + columnIndex] +=
                    a[rowIndex * aColumnÑount + k] * b[k * bColumnÑount + columnIndex];
            }
        }
    }

    return 0;
}