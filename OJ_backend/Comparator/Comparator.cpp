// Comparator.cpp : 定义 DLL 的导出函数。
//

#include "pch.h"
#include "framework.h"
#include "Comparator.h"
#include <fstream>
#include <iostream>


// 这是导出变量的一个示例
COMPARATOR_API int nComparator=0;

// 这是导出函数的一个示例。
COMPARATOR_API int fnComparator(void)
{
    return 0;
}

// 这是已导出类的构造函数。
CComparator::CComparator()
{
    return;
}


bool TestString(char*s)
{
	return true;
}

int TestInt(int s)
{
	return s;
}


bool Compare(char* filePath1, char* filePath2)
{
	std::ifstream file1;
	std::ifstream file2;
	try
	{
		char line1[1024];
		char line2[1024];
		//file1.open("D:/tt/a.txt");
		//file2.open("D:/tt/b.txt");
		file1.open(filePath1);
		file2.open(filePath2);
		std::cout << "open" << "\n";
		file1.seekg(0, std::ios::beg);
		file2.seekg(0, std::ios::beg);
		int cnt = 0;
		while (!file1.eof() && !file2.eof())
		{
			file1.getline(line1, 1024, '\n');
			file2.getline(line2, 1024, '\n');
			int p1 = 0, p2 = 0;
			while (line1[p1] != '\0'&&line2[p2] != '\0')
			{
				while (line1[p1] == '\r')++p1;
				while (line2[p2] == '\r')++p2;
				if (line1[p1] != line2[p2])
				{
					file1.close();
					file2.close();
					return false;
				}
				++p1; ++p2;
			}
			while (line1[p1] != '\0')
			{
				if (line1[p1] != '\r' || line1[p1] != '\n')
				{
					file1.close();
					file2.close();
					return false;
				}
				++p1;
			}
			while (line2[p2] != '\0')
			{
				if (line2[p2] != '\r' || line2[p2] != '\n')
				{
					file1.close();
					file2.close();
					return false;
				}
				++p2;
			}
			//++cnt;
			//std::cout << cnt << "\n";
		}
		while (!file1.eof())
		{
			file1.getline(line1, 1024, '\n');
			int p = 0;
			while (line1[p] != '\0')
			{
				if (line1[p] != '\r' || line1[p] != '\n')
				{
					file1.close();
					file2.close();
					return false;
				}
				++p;
			}
		}
		while (!file2.eof())
		{
			file2.getline(line2, 1024, '\n');
			int p = 0;
			while (line2[p] != '\0')
			{
				if (line2[p] != '\r' || line2[p] != '\n')
				{
					file1.close();
					file2.close();
					return false;
				}
				++p;
			}
		}
	}
	catch (int e) {
		file1.close();
		file2.close();
		return false;
	}
	file1.close();
	file2.close();
	return true;
}