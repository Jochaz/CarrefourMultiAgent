#include "stdafx.h"
#include <iostream>
#include "DemoHash.h"

#include "CException.h"

#include "CHashNone.h"
#include "CHashCrc32.h"
#include "CHashMd5.h"
#include "CHashSha1.h"
#include "CHashSha224.h"
#include "CHashSha256.h"


void test_none( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing None library v" << CHashNone::GetVersion() << " as of " << CHashNone::GetBuildDate() << "." << std::endl;
	CHashNone none;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		none.HashFile( p_fileName );
		std::cout << "- Hash is " << none.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 32 ];

		none.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << none.GetHash() << std::endl;
		none.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}

void test_crc32( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing CRC32 library v" << CHashCrc32::GetVersion() << " as of " << CHashCrc32::GetBuildDate() << "." << std::endl;
	CHashCrc32 crc32;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		crc32.HashFile( p_fileName );
		std::cout << "- Hash is " << crc32.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 8 ];

		crc32.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << crc32.GetHash() << std::endl;
		crc32.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}

void test_md5( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing MD5 library v" << CHashMd5::GetVersion() << " as of " << CHashMd5::GetBuildDate() << "." << std::endl;
	CHashMd5 md5;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		md5.HashFile( p_fileName );
		std::cout << "- Hash is " << md5.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 16 ];

		md5.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << md5.GetHash() << std::endl;
		md5.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}

void test_sha1( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing SHA1 library v" << CHashSha1::GetVersion() << " as of " << CHashSha1::GetBuildDate() << "." << std::endl;
	CHashSha1 sha1;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		sha1.HashFile( p_fileName );
		std::cout << "- Hash is " << sha1.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 20 ];

		sha1.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << sha1.GetHash() << std::endl;
		sha1.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}

void test_sha224( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing SHA-224 library v" << CHashSha224::GetVersion() << " as of " << CHashSha224::GetBuildDate() << "." << std::endl;
	CHashSha224 sha224;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		sha224.HashFile( p_fileName );
		std::cout << "- Hash is " << sha224.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 28 ];

		sha224.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << sha224.GetHash() << std::endl;
		sha224.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}

void test_sha256( const std::string p_fileName, const std::string p_text ) {
	std::cout << "Testing SHA-256 library v" << CHashSha256::getVersion() << " as of " << CHashSha256::getBuildDate() << "." << std::endl;
	CHashSha256 sha256;

	// From file
	std::cout << "- From file..." << std::endl;
	try {
		sha256.HashFile( p_fileName );
		std::cout << "- Hash is " << sha256.GetHash() << std::endl;
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;

	// From buffer
	std::cout << "- From buffer..." << std::endl;
	try {
		unsigned char rawHash[ 32 ];

		sha256.HashBuffer( reinterpret_cast<const unsigned char *>(p_text.c_str()), static_cast<int>(p_text.length()) );
		std::cout << "- Hash is " << sha256.GetHash() << std::endl;
		sha256.GetRawHash( &rawHash[ 0 ] );
	}
	catch (CException &e) {
		std::cerr << "** --- EXCEPTION THROWN ---" << std::endl;
		std::cerr << "** Type: " << e.GetType() << std::endl;
		std::cerr << "** Message: " << e.GetErrorMessage() << std::endl;
		std::cerr << "** Error code: " << e.GetErrorCode() << std::endl;
		std::cerr << "** Fault location: " << e.GetFaultLocation() << std::endl;
	}
	std::cout << std::endl;
}


void TestHashes() {
	const std::string p_fileName = "D:\\Games\\Doom3\\install.log";
	const std::string p_text = "I'm a string ready to be hashed";

	test_none( p_fileName, p_text );
	test_crc32( p_fileName, p_text );
	test_md5( p_fileName, p_text );
	test_sha1( p_fileName, p_text );
	test_sha224( p_fileName, p_text );
	test_sha256( p_fileName, p_text );
	return;
}
