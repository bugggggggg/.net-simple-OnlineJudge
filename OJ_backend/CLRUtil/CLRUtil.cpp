#include "pch.h"

#include "CLRUtil.h"




String^ CLRUtil::Class1::Encryption(String^ ori)
{
	int seed = 10;
	for (int i = 'a'; i <= 'z'; i++)
	{
		ori->Replace(i, i + seed);
	}

	return ori;
}