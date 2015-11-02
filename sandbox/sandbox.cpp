#include "stdafx.h"
#include <iostream>
#include <pthread.h>


void sandbox() {
	// Add here all your tests
	return;
}


int main() {
	std::cout << "** Welcome to this sandbox project." << std::endl;
	std::cout << "** You can test libraries in this project without altering the *real* one." << std::endl;

	sandbox();

	std::cout << "** Goodbye" << std::endl;
	system( "pause" );
	return 0;
}
