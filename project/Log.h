#include <string>

enum severite{Serreur, Savertissement, Sinfo, Sdebug};

class Log
{
public :
	Log(std::string UnMessage, std::string UneSeverite, std::string UnTitre);
	void Erreur();
	void Avertissement();
	void Info();
	void Debug();
private:
	severite FSeverite;
	std::string FMessage;
	std::string FTitre;
	void EcritureLog(std::string LeMessage);
};
