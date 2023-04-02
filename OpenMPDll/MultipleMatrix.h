#pragma once
#include <cstdio>
#include <omp.h>

extern "C" __declspec(dllexport) int Multiple(double* a, int aRow�ount, int aColumn�ount,
    double* b, int bRow�ount, int bColumn�ount, int threadCount, double* result)
{
    // ����� ������� ���������� �������
    omp_set_num_threads(threadCount);

#pragma omp parallel for shared(a, b, result)
    for (int rowIndex = 0; rowIndex < aRow�ount; rowIndex++)
    {
        for (int columnIndex = 0; columnIndex < bColumn�ount; columnIndex++)
        {
            result[rowIndex * aColumn�ount + columnIndex] = 0;
            for (int k = 0; k < aColumn�ount; k++)
            {
                result[rowIndex * aColumn�ount + columnIndex] +=
                    a[rowIndex * aColumn�ount + k] * b[k * bColumn�ount + columnIndex];
            }
        }
    }

    return 0;
}