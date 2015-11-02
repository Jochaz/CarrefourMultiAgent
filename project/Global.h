#include <Singleton.h>
#include <string>
using namespace std;
class Global
{
	IMPLEMENT_SINGLETON(Global);
public:
	string hash;
	string algo;
	string alphabet;
	int chunkSize;
};